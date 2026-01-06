using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004C1 RID: 1217
	internal class ServicePointScheduler
	{
		// Token: 0x17000846 RID: 2118
		// (get) Token: 0x0600277D RID: 10109 RVA: 0x00091FBE File Offset: 0x000901BE
		// (set) Token: 0x0600277E RID: 10110 RVA: 0x00091FC6 File Offset: 0x000901C6
		private ServicePoint ServicePoint { get; set; }

		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00091FCF File Offset: 0x000901CF
		// (set) Token: 0x06002780 RID: 10112 RVA: 0x00091FD7 File Offset: 0x000901D7
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				if (value < -1 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.maxIdleTime)
				{
					return;
				}
				this.maxIdleTime = value;
				this.Run();
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x00092002 File Offset: 0x00090202
		// (set) Token: 0x06002782 RID: 10114 RVA: 0x0009200A File Offset: 0x0009020A
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException();
				}
				if (value == this.connectionLimit)
				{
					return;
				}
				this.connectionLimit = value;
				this.Run();
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00092030 File Offset: 0x00090230
		public ServicePointScheduler(ServicePoint servicePoint, int connectionLimit, int maxIdleTime)
		{
			this.ServicePoint = servicePoint;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.schedulerEvent = new ServicePointScheduler.AsyncManualResetEvent(false);
			this.defaultGroup = new ServicePointScheduler.ConnectionGroup(this, string.Empty);
			this.operations = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>>();
			this.idleConnections = new LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>>();
			this.idleSince = DateTime.UtcNow;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_WEB_DEBUG")]
		private void Debug(string message)
		{
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x06002785 RID: 10117 RVA: 0x000920B4 File Offset: 0x000902B4
		public int CurrentConnections
		{
			get
			{
				return this.currentConnections;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002786 RID: 10118 RVA: 0x000920BC File Offset: 0x000902BC
		public DateTime IdleSince
		{
			get
			{
				return this.idleSince;
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000920C4 File Offset: 0x000902C4
		internal string ME { get; }

		// Token: 0x06002788 RID: 10120 RVA: 0x000920CC File Offset: 0x000902CC
		public void Run()
		{
			if (Interlocked.CompareExchange(ref this.running, 1, 0) == 0)
			{
				Task.Run(() => this.RunScheduler());
			}
			this.schedulerEvent.Set();
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000920FC File Offset: 0x000902FC
		private async Task RunScheduler()
		{
			this.idleSince = DateTime.UtcNow + TimeSpan.FromDays(3650.0);
			for (;;)
			{
				List<Task> taskList = new List<Task>();
				bool finalCleanup = false;
				ServicePoint servicePoint = this.ServicePoint;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[] operationArray;
				ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[] idleArray;
				Task<bool> schedulerTask;
				lock (servicePoint)
				{
					this.Cleanup();
					operationArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>[this.operations.Count];
					this.operations.CopyTo(operationArray, 0);
					idleArray = new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>[this.idleConnections.Count];
					this.idleConnections.CopyTo(idleArray, 0);
					schedulerTask = this.schedulerEvent.WaitAsync(this.maxIdleTime);
					taskList.Add(schedulerTask);
					if (this.groups == null && this.defaultGroup.IsEmpty() && this.operations.Count == 0 && this.idleConnections.Count == 0)
					{
						this.idleSince = DateTime.UtcNow;
						finalCleanup = true;
					}
					else
					{
						foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple in operationArray)
						{
							taskList.Add(valueTuple.Item2.Finished.Task);
						}
						foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple2 in idleArray)
						{
							taskList.Add(valueTuple2.Item3);
						}
					}
				}
				Task task = await Task.WhenAny(taskList).ConfigureAwait(false);
				servicePoint = this.ServicePoint;
				lock (servicePoint)
				{
					bool flag2 = false;
					if (finalCleanup)
					{
						if (!schedulerTask.Result)
						{
							this.FinalCleanup();
							break;
						}
						flag2 = true;
					}
					else if (task == taskList[0])
					{
						flag2 = true;
					}
					foreach (ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation> valueTuple3 in operationArray)
					{
						if (valueTuple3.Item2.Finished.CurrentResult != null)
						{
							this.operations.Remove(valueTuple3);
							flag2 |= this.OperationCompleted(valueTuple3.Item1, valueTuple3.Item2);
						}
					}
					if (flag2)
					{
						this.RunSchedulerIteration();
					}
					int num = -1;
					for (int k = 0; k < idleArray.Length; k++)
					{
						if (task == taskList[k + 1 + operationArray.Length])
						{
							num = k;
							break;
						}
					}
					if (num >= 0)
					{
						ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task> valueTuple4 = idleArray[num];
						this.idleConnections.Remove(valueTuple4);
						this.CloseIdleConnection(valueTuple4.Item1, valueTuple4.Item2);
					}
				}
				operationArray = null;
				idleArray = null;
				taskList = null;
				schedulerTask = null;
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00092140 File Offset: 0x00090340
		private void Cleanup()
		{
			if (this.groups != null)
			{
				string[] array = new string[this.groups.Count];
				this.groups.Keys.CopyTo(array, 0);
				foreach (string text in array)
				{
					if (this.groups.ContainsKey(text) && this.groups[text].IsEmpty())
					{
						this.groups.Remove(text);
					}
				}
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x000921D0 File Offset: 0x000903D0
		private void RunSchedulerIteration()
		{
			this.schedulerEvent.Reset();
			bool flag;
			do
			{
				flag = this.SchedulerIteration(this.defaultGroup);
				if (this.groups != null)
				{
					foreach (KeyValuePair<string, ServicePointScheduler.ConnectionGroup> keyValuePair in this.groups)
					{
						flag |= this.SchedulerIteration(keyValuePair.Value);
					}
				}
			}
			while (flag);
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00092250 File Offset: 0x00090450
		private bool OperationCompleted(ServicePointScheduler.ConnectionGroup group, WebOperation operation)
		{
			WebCompletionSource<ValueTuple<bool, WebOperation>>.Result currentResult = operation.Finished.CurrentResult;
			bool flag;
			WebOperation webOperation;
			if (!currentResult.Success)
			{
				flag = false;
				webOperation = null;
			}
			else
			{
				ValueTuple<bool, WebOperation> argument = currentResult.Argument;
				flag = argument.Item1;
				webOperation = argument.Item2;
			}
			if (!flag || !operation.Connection.Continue(webOperation))
			{
				group.RemoveConnection(operation.Connection);
				if (webOperation == null)
				{
					return true;
				}
				flag = false;
			}
			if (webOperation == null)
			{
				if (flag)
				{
					Task task = Task.Delay(this.MaxIdleTime);
					this.idleConnections.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>(group, operation.Connection, task));
				}
				return true;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, webOperation));
			if (flag)
			{
				this.RemoveIdleConnection(operation.Connection);
				return false;
			}
			group.Cleanup();
			group.CreateOrReuseConnection(webOperation, true);
			return false;
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00092313 File Offset: 0x00090513
		private void CloseIdleConnection(ServicePointScheduler.ConnectionGroup group, WebConnection connection)
		{
			group.RemoveConnection(connection);
			this.RemoveIdleConnection(connection);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x00092324 File Offset: 0x00090524
		private bool SchedulerIteration(ServicePointScheduler.ConnectionGroup group)
		{
			group.Cleanup();
			WebOperation nextOperation = group.GetNextOperation();
			if (nextOperation == null)
			{
				return false;
			}
			WebConnection item = group.CreateOrReuseConnection(nextOperation, false).Item1;
			if (item == null)
			{
				return false;
			}
			this.operations.AddLast(new ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>(group, nextOperation));
			this.RemoveIdleConnection(item);
			return true;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x00092374 File Offset: 0x00090574
		private void RemoveOperation(WebOperation operation)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode = this.operations.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == operation)
				{
					this.operations.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000923B8 File Offset: 0x000905B8
		private void RemoveIdleConnection(WebConnection connection)
		{
			LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode = this.idleConnections.First;
			while (linkedListNode != null)
			{
				LinkedListNode<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> linkedListNode2 = linkedListNode;
				linkedListNode = linkedListNode.Next;
				if (linkedListNode2.Value.Item2 == connection)
				{
					this.idleConnections.Remove(linkedListNode2);
				}
			}
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000923F9 File Offset: 0x000905F9
		private void FinalCleanup()
		{
			this.groups = null;
			this.operations = null;
			this.idleConnections = null;
			this.defaultGroup = null;
			this.ServicePoint.FreeServicePoint();
			ServicePointManager.RemoveServicePoint(this.ServicePoint);
			this.ServicePoint = null;
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x00092434 File Offset: 0x00090634
		public void SendRequest(WebOperation operation, string groupName)
		{
			ServicePoint servicePoint = this.ServicePoint;
			lock (servicePoint)
			{
				this.GetConnectionGroup(groupName).EnqueueOperation(operation);
				this.Run();
			}
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x00092484 File Offset: 0x00090684
		public bool CloseConnectionGroup(string groupName)
		{
			ServicePointScheduler.ConnectionGroup connectionGroup;
			if (string.IsNullOrEmpty(groupName))
			{
				connectionGroup = this.defaultGroup;
			}
			else if (this.groups == null || !this.groups.TryGetValue(groupName, out connectionGroup))
			{
				return false;
			}
			if (connectionGroup != this.defaultGroup)
			{
				this.groups.Remove(groupName);
				if (this.groups.Count == 0)
				{
					this.groups = null;
				}
			}
			connectionGroup.Close();
			this.Run();
			return true;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x000924F4 File Offset: 0x000906F4
		private ServicePointScheduler.ConnectionGroup GetConnectionGroup(string name)
		{
			ServicePoint servicePoint = this.ServicePoint;
			ServicePointScheduler.ConnectionGroup connectionGroup;
			lock (servicePoint)
			{
				if (string.IsNullOrEmpty(name))
				{
					connectionGroup = this.defaultGroup;
				}
				else
				{
					if (this.groups == null)
					{
						this.groups = new Dictionary<string, ServicePointScheduler.ConnectionGroup>();
					}
					ServicePointScheduler.ConnectionGroup connectionGroup2;
					if (this.groups.TryGetValue(name, out connectionGroup2))
					{
						connectionGroup = connectionGroup2;
					}
					else
					{
						connectionGroup2 = new ServicePointScheduler.ConnectionGroup(this, name);
						this.groups.Add(name, connectionGroup2);
						connectionGroup = connectionGroup2;
					}
				}
			}
			return connectionGroup;
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00092580 File Offset: 0x00090780
		private void OnConnectionCreated(WebConnection connection)
		{
			Interlocked.Increment(ref this.currentConnections);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0009258E File Offset: 0x0009078E
		private void OnConnectionClosed(WebConnection connection)
		{
			this.RemoveIdleConnection(connection);
			Interlocked.Decrement(ref this.currentConnections);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x000925A4 File Offset: 0x000907A4
		public static async Task<bool> WaitAsync(Task workerTask, int millisecondTimeout)
		{
			CancellationTokenSource cts = new CancellationTokenSource();
			bool flag;
			try
			{
				Task timeoutTask = Task.Delay(millisecondTimeout, cts.Token);
				ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter = Task.WhenAny(new Task[] { workerTask, timeoutTask }).ConfigureAwait(false).GetAwaiter();
				if (!configuredTaskAwaiter.IsCompleted)
				{
					await configuredTaskAwaiter;
					ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter configuredTaskAwaiter2;
					configuredTaskAwaiter = configuredTaskAwaiter2;
					configuredTaskAwaiter2 = default(ConfiguredTaskAwaitable<Task>.ConfiguredTaskAwaiter);
				}
				flag = configuredTaskAwaiter.GetResult() != timeoutTask;
			}
			finally
			{
				cts.Cancel();
				cts.Dispose();
			}
			return flag;
		}

		// Token: 0x040016DE RID: 5854
		private int running;

		// Token: 0x040016DF RID: 5855
		private int maxIdleTime = 100000;

		// Token: 0x040016E0 RID: 5856
		private ServicePointScheduler.AsyncManualResetEvent schedulerEvent;

		// Token: 0x040016E1 RID: 5857
		private ServicePointScheduler.ConnectionGroup defaultGroup;

		// Token: 0x040016E2 RID: 5858
		private Dictionary<string, ServicePointScheduler.ConnectionGroup> groups;

		// Token: 0x040016E3 RID: 5859
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebOperation>> operations;

		// Token: 0x040016E4 RID: 5860
		private LinkedList<ValueTuple<ServicePointScheduler.ConnectionGroup, WebConnection, Task>> idleConnections;

		// Token: 0x040016E5 RID: 5861
		private int currentConnections;

		// Token: 0x040016E6 RID: 5862
		private int connectionLimit;

		// Token: 0x040016E7 RID: 5863
		private DateTime idleSince;

		// Token: 0x040016E8 RID: 5864
		private static int nextId;

		// Token: 0x040016E9 RID: 5865
		public readonly int ID = ++ServicePointScheduler.nextId;

		// Token: 0x020004C2 RID: 1218
		private class ConnectionGroup
		{
			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x06002799 RID: 10137 RVA: 0x000925F7 File Offset: 0x000907F7
			public ServicePointScheduler Scheduler { get; }

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x0600279A RID: 10138 RVA: 0x000925FF File Offset: 0x000907FF
			public string Name { get; }

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x0600279B RID: 10139 RVA: 0x00092607 File Offset: 0x00090807
			public bool IsDefault
			{
				get
				{
					return string.IsNullOrEmpty(this.Name);
				}
			}

			// Token: 0x0600279C RID: 10140 RVA: 0x00092614 File Offset: 0x00090814
			public ConnectionGroup(ServicePointScheduler scheduler, string name)
			{
				this.Scheduler = scheduler;
				this.Name = name;
				this.connections = new LinkedList<WebConnection>();
				this.queue = new LinkedList<WebOperation>();
			}

			// Token: 0x0600279D RID: 10141 RVA: 0x00092653 File Offset: 0x00090853
			public bool IsEmpty()
			{
				return this.connections.Count == 0 && this.queue.Count == 0;
			}

			// Token: 0x0600279E RID: 10142 RVA: 0x00092672 File Offset: 0x00090872
			public void RemoveConnection(WebConnection connection)
			{
				this.connections.Remove(connection);
				connection.Dispose();
				this.Scheduler.OnConnectionClosed(connection);
			}

			// Token: 0x0600279F RID: 10143 RVA: 0x00092694 File Offset: 0x00090894
			public void Cleanup()
			{
				LinkedListNode<WebConnection> linkedListNode = this.connections.First;
				while (linkedListNode != null)
				{
					WebConnection value = linkedListNode.Value;
					LinkedListNode<WebConnection> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (value.Closed)
					{
						this.connections.Remove(linkedListNode2);
						this.Scheduler.OnConnectionClosed(value);
					}
				}
			}

			// Token: 0x060027A0 RID: 10144 RVA: 0x000926E4 File Offset: 0x000908E4
			public void Close()
			{
				foreach (WebOperation webOperation in this.queue)
				{
					webOperation.Abort();
					this.Scheduler.RemoveOperation(webOperation);
				}
				this.queue.Clear();
				foreach (WebConnection webConnection in this.connections)
				{
					webConnection.Dispose();
					this.Scheduler.OnConnectionClosed(webConnection);
				}
				this.connections.Clear();
			}

			// Token: 0x060027A1 RID: 10145 RVA: 0x000927A8 File Offset: 0x000909A8
			public void EnqueueOperation(WebOperation operation)
			{
				this.queue.AddLast(operation);
			}

			// Token: 0x060027A2 RID: 10146 RVA: 0x000927B8 File Offset: 0x000909B8
			public WebOperation GetNextOperation()
			{
				LinkedListNode<WebOperation> linkedListNode = this.queue.First;
				while (linkedListNode != null)
				{
					WebOperation value = linkedListNode.Value;
					LinkedListNode<WebOperation> linkedListNode2 = linkedListNode;
					linkedListNode = linkedListNode.Next;
					if (!value.Aborted)
					{
						return value;
					}
					this.queue.Remove(linkedListNode2);
					this.Scheduler.RemoveOperation(value);
				}
				return null;
			}

			// Token: 0x060027A3 RID: 10147 RVA: 0x0009280C File Offset: 0x00090A0C
			public WebConnection FindIdleConnection(WebOperation operation)
			{
				WebConnection webConnection = null;
				foreach (WebConnection webConnection2 in this.connections)
				{
					if (webConnection2.CanReuseConnection(operation) && (webConnection == null || webConnection2.IdleSince > webConnection.IdleSince))
					{
						webConnection = webConnection2;
					}
				}
				if (webConnection != null && webConnection.StartOperation(operation, true))
				{
					this.queue.Remove(operation);
					return webConnection;
				}
				foreach (WebConnection webConnection3 in this.connections)
				{
					if (webConnection3.StartOperation(operation, true))
					{
						this.queue.Remove(operation);
						return webConnection3;
					}
				}
				return null;
			}

			// Token: 0x060027A4 RID: 10148 RVA: 0x000928F4 File Offset: 0x00090AF4
			[return: TupleElementNames(new string[] { "connection", "created" })]
			public ValueTuple<WebConnection, bool> CreateOrReuseConnection(WebOperation operation, bool force)
			{
				WebConnection webConnection = this.FindIdleConnection(operation);
				if (webConnection != null)
				{
					return new ValueTuple<WebConnection, bool>(webConnection, false);
				}
				if (force || this.Scheduler.ServicePoint.ConnectionLimit > this.connections.Count || this.connections.Count == 0)
				{
					webConnection = new WebConnection(this.Scheduler.ServicePoint);
					webConnection.StartOperation(operation, false);
					this.connections.AddFirst(webConnection);
					this.Scheduler.OnConnectionCreated(webConnection);
					this.queue.Remove(operation);
					return new ValueTuple<WebConnection, bool>(webConnection, true);
				}
				return new ValueTuple<WebConnection, bool>(null, false);
			}

			// Token: 0x040016ED RID: 5869
			private static int nextId;

			// Token: 0x040016EE RID: 5870
			public readonly int ID = ++ServicePointScheduler.ConnectionGroup.nextId;

			// Token: 0x040016EF RID: 5871
			private LinkedList<WebConnection> connections;

			// Token: 0x040016F0 RID: 5872
			private LinkedList<WebOperation> queue;
		}

		// Token: 0x020004C3 RID: 1219
		private class AsyncManualResetEvent
		{
			// Token: 0x060027A5 RID: 10149 RVA: 0x00092990 File Offset: 0x00090B90
			public Task WaitAsync()
			{
				return this.m_tcs.Task;
			}

			// Token: 0x060027A6 RID: 10150 RVA: 0x0009299F File Offset: 0x00090B9F
			public bool WaitOne(int millisecondTimeout)
			{
				return this.m_tcs.Task.Wait(millisecondTimeout);
			}

			// Token: 0x060027A7 RID: 10151 RVA: 0x000929B4 File Offset: 0x00090BB4
			public Task<bool> WaitAsync(int millisecondTimeout)
			{
				return ServicePointScheduler.WaitAsync(this.m_tcs.Task, millisecondTimeout);
			}

			// Token: 0x060027A8 RID: 10152 RVA: 0x000929CC File Offset: 0x00090BCC
			public void Set()
			{
				TaskCompletionSource<bool> tcs = this.m_tcs;
				Task.Factory.StartNew<bool>((object s) => ((TaskCompletionSource<bool>)s).TrySetResult(true), tcs, CancellationToken.None, TaskCreationOptions.PreferFairness, TaskScheduler.Default);
				tcs.Task.Wait();
			}

			// Token: 0x060027A9 RID: 10153 RVA: 0x00092A24 File Offset: 0x00090C24
			public void Reset()
			{
				TaskCompletionSource<bool> tcs;
				do
				{
					tcs = this.m_tcs;
				}
				while (tcs.Task.IsCompleted && Interlocked.CompareExchange<TaskCompletionSource<bool>>(ref this.m_tcs, new TaskCompletionSource<bool>(), tcs) != tcs);
			}

			// Token: 0x060027AA RID: 10154 RVA: 0x00092A5B File Offset: 0x00090C5B
			public AsyncManualResetEvent(bool state)
			{
				if (state)
				{
					this.Set();
				}
			}

			// Token: 0x040016F1 RID: 5873
			private volatile TaskCompletionSource<bool> m_tcs = new TaskCompletionSource<bool>();
		}
	}
}
