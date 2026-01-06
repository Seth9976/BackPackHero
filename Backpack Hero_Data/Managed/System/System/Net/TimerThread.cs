using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200044D RID: 1101
	internal static class TimerThread
	{
		// Token: 0x060022C0 RID: 8896 RVA: 0x0007F4FC File Offset: 0x0007D6FC
		static TimerThread()
		{
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007F574 File Offset: 0x0007D774
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (linkedList)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference weakReference = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(weakReference);
			}
			return timerQueue;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007F5E4 File Offset: 0x0007D7E4
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
				lock (linkedList)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007F788 File Offset: 0x0007D988
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007F7BC File Offset: 0x0007D9BC
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> linkedList = TimerThread.s_Queues;
			lock (linkedList)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> linkedList2 = TimerThread.s_NewQueues;
									lock (linkedList2)
									{
										for (LinkedListNode<WeakReference> linkedListNode = TimerThread.s_NewQueues.First; linkedListNode != null; linkedListNode = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(linkedListNode);
											TimerThread.s_Queues.AddLast(linkedListNode);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode2 = TimerThread.s_Queues.First;
								while (linkedListNode2 != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode2.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode2.Next;
										TimerThread.s_Queues.Remove(linkedListNode2);
										linkedListNode2 = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode2 = linkedListNode2.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int num3 = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num4 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, num3, false);
								if (num4 == 0)
								{
									break;
								}
								if (num4 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_01A8;
									}
								}
							}
							flag2 = false;
							continue;
							IL_01A8:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							bool on = Logging.On;
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x0007F9F0 File Offset: 0x0007DBF0
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x0007FA09 File Offset: 0x0007DC09
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x0007FA28 File Offset: 0x0007DC28
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x04001434 RID: 5172
		private const int c_ThreadIdleTimeoutMilliseconds = 30000;

		// Token: 0x04001435 RID: 5173
		private const int c_CacheScanPerIterations = 32;

		// Token: 0x04001436 RID: 5174
		private const int c_TickCountResolution = 15;

		// Token: 0x04001437 RID: 5175
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x04001438 RID: 5176
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x04001439 RID: 5177
		private static int s_ThreadState = 0;

		// Token: 0x0400143A RID: 5178
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x0400143B RID: 5179
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x0400143C RID: 5180
		private static WaitHandle[] s_ThreadEvents = new WaitHandle[]
		{
			TimerThread.s_ThreadShutdownEvent,
			TimerThread.s_ThreadReadyEvent
		};

		// Token: 0x0400143D RID: 5181
		private static int s_CacheScanIteration;

		// Token: 0x0400143E RID: 5182
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x0200044E RID: 1102
		internal abstract class Queue
		{
			// Token: 0x060022C8 RID: 8904 RVA: 0x0007FA50 File Offset: 0x0007DC50
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x170006E0 RID: 1760
			// (get) Token: 0x060022C9 RID: 8905 RVA: 0x0007FA5F File Offset: 0x0007DC5F
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x060022CA RID: 8906 RVA: 0x0007FA67 File Offset: 0x0007DC67
			internal TimerThread.Timer CreateTimer()
			{
				return this.CreateTimer(null, null);
			}

			// Token: 0x060022CB RID: 8907
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x0400143F RID: 5183
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x0200044F RID: 1103
		internal abstract class Timer : IDisposable
		{
			// Token: 0x060022CC RID: 8908 RVA: 0x0007FA71 File Offset: 0x0007DC71
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x170006E1 RID: 1761
			// (get) Token: 0x060022CD RID: 8909 RVA: 0x0007FA8B File Offset: 0x0007DC8B
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x170006E2 RID: 1762
			// (get) Token: 0x060022CE RID: 8910 RVA: 0x0007FA93 File Offset: 0x0007DC93
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x170006E3 RID: 1763
			// (get) Token: 0x060022CF RID: 8911 RVA: 0x0007FA9B File Offset: 0x0007DC9B
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x170006E4 RID: 1764
			// (get) Token: 0x060022D0 RID: 8912 RVA: 0x0007FAAC File Offset: 0x0007DCAC
			internal int TimeRemaining
			{
				get
				{
					if (this.HasExpired)
					{
						return 0;
					}
					if (this.Duration == -1)
					{
						return -1;
					}
					int tickCount = Environment.TickCount;
					int num = (int)(TimerThread.IsTickBetween(this.StartTime, this.Expiration, tickCount) ? Math.Min((uint)(this.Expiration - tickCount), 2147483647U) : 0U);
					if (num >= 2)
					{
						return num;
					}
					return num + 1;
				}
			}

			// Token: 0x060022D1 RID: 8913
			internal abstract bool Cancel();

			// Token: 0x170006E5 RID: 1765
			// (get) Token: 0x060022D2 RID: 8914
			internal abstract bool HasExpired { get; }

			// Token: 0x060022D3 RID: 8915 RVA: 0x0007FB07 File Offset: 0x0007DD07
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04001440 RID: 5184
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04001441 RID: 5185
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x02000450 RID: 1104
		// (Invoke) Token: 0x060022D5 RID: 8917
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x02000451 RID: 1105
		private enum TimerThreadState
		{
			// Token: 0x04001443 RID: 5187
			Idle,
			// Token: 0x04001444 RID: 5188
			Running,
			// Token: 0x04001445 RID: 5189
			Stopped
		}

		// Token: 0x02000452 RID: 1106
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x060022D8 RID: 8920 RVA: 0x0007FB10 File Offset: 0x0007DD10
			internal TimerQueue(int durationMilliseconds)
				: base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x060022D9 RID: 8921 RVA: 0x0007FB48 File Offset: 0x0007DD48
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x060022DA RID: 8922 RVA: 0x0007FC14 File Offset: 0x0007DE14
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode timerNode;
				do
				{
					timerNode = this.m_Timers.Next;
					if (timerNode == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							timerNode = this.m_Timers.Next;
							if (timerNode == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (timerNode.Fire());
				nextExpiration = timerNode.Expiration;
				return true;
			}

			// Token: 0x04001446 RID: 5190
			private IntPtr m_ThisHandle;

			// Token: 0x04001447 RID: 5191
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x02000453 RID: 1107
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x060022DB RID: 8923 RVA: 0x0007FCC8 File Offset: 0x0007DEC8
			internal InfiniteTimerQueue()
				: base(-1)
			{
			}

			// Token: 0x060022DC RID: 8924 RVA: 0x0007FCD1 File Offset: 0x0007DED1
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x02000454 RID: 1108
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x060022DD RID: 8925 RVA: 0x0007FCD8 File Offset: 0x0007DED8
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock)
				: base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x060022DE RID: 8926 RVA: 0x0007FD01 File Offset: 0x0007DF01
			internal TimerNode()
				: base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x170006E6 RID: 1766
			// (get) Token: 0x060022DF RID: 8927 RVA: 0x0007FD11 File Offset: 0x0007DF11
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x170006E7 RID: 1767
			// (get) Token: 0x060022E0 RID: 8928 RVA: 0x0007FD1C File Offset: 0x0007DF1C
			// (set) Token: 0x060022E1 RID: 8929 RVA: 0x0007FD24 File Offset: 0x0007DF24
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x170006E8 RID: 1768
			// (get) Token: 0x060022E2 RID: 8930 RVA: 0x0007FD2D File Offset: 0x0007DF2D
			// (set) Token: 0x060022E3 RID: 8931 RVA: 0x0007FD35 File Offset: 0x0007DF35
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x060022E4 RID: 8932 RVA: 0x0007FD40 File Offset: 0x0007DF40
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x060022E5 RID: 8933 RVA: 0x0007FDD8 File Offset: 0x0007DFD8
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = this.m_Callback != null;
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						bool on = Logging.On;
					}
				}
				return true;
			}

			// Token: 0x04001448 RID: 5192
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04001449 RID: 5193
			private TimerThread.Callback m_Callback;

			// Token: 0x0400144A RID: 5194
			private object m_Context;

			// Token: 0x0400144B RID: 5195
			private object m_QueueLock;

			// Token: 0x0400144C RID: 5196
			private TimerThread.TimerNode next;

			// Token: 0x0400144D RID: 5197
			private TimerThread.TimerNode prev;

			// Token: 0x02000455 RID: 1109
			private enum TimerState
			{
				// Token: 0x0400144F RID: 5199
				Ready,
				// Token: 0x04001450 RID: 5200
				Fired,
				// Token: 0x04001451 RID: 5201
				Cancelled,
				// Token: 0x04001452 RID: 5202
				Sentinel
			}
		}

		// Token: 0x02000456 RID: 1110
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x060022E6 RID: 8934 RVA: 0x0007FECC File Offset: 0x0007E0CC
			internal InfiniteTimer()
				: base(-1)
			{
			}

			// Token: 0x170006E9 RID: 1769
			// (get) Token: 0x060022E7 RID: 8935 RVA: 0x00003062 File Offset: 0x00001262
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060022E8 RID: 8936 RVA: 0x0007FED5 File Offset: 0x0007E0D5
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04001453 RID: 5203
			private int cancelled;
		}
	}
}
