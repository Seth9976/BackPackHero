using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000016 RID: 22
	public sealed class Flow : IPoolable, IDisposable
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000028DD File Offset: 0x00000ADD
		// (set) Token: 0x06000070 RID: 112 RVA: 0x000028E5 File Offset: 0x00000AE5
		public GraphStack stack { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000028EE File Offset: 0x00000AEE
		// (set) Token: 0x06000072 RID: 114 RVA: 0x000028F6 File Offset: 0x00000AF6
		public MonoBehaviour coroutineRunner { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000028FF File Offset: 0x00000AFF
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002907 File Offset: 0x00000B07
		public bool isCoroutine { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002910 File Offset: 0x00000B10
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00002918 File Offset: 0x00000B18
		public bool isPrediction { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002921 File Offset: 0x00000B21
		public bool enableDebug
		{
			get
			{
				return !this.isPrediction && this.stack.hasDebugData;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000293D File Offset: 0x00000B3D
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002944 File Offset: 0x00000B44
		public static Func<GraphPointer, bool> isInspectedBinding { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007A RID: 122 RVA: 0x0000294C File Offset: 0x00000B4C
		public bool isInspected
		{
			get
			{
				Func<GraphPointer, bool> isInspectedBinding = Flow.isInspectedBinding;
				return isInspectedBinding != null && isInspectedBinding(this.stack);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002964 File Offset: 0x00000B64
		private Flow()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000029A0 File Offset: 0x00000BA0
		public static Flow New(GraphReference reference)
		{
			Ensure.That("reference").IsNotNull<GraphReference>(reference);
			Flow flow = GenericPool<Flow>.New(() => new Flow());
			flow.stack = reference.ToStackPooled();
			return flow;
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000029ED File Offset: 0x00000BED
		void IPoolable.New()
		{
			this.disposed = false;
			this.recursion = Recursion<Flow.RecursionNode>.New();
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002A01 File Offset: 0x00000C01
		public void Dispose()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(this.ToString());
			}
			GenericPool<Flow>.Free(this);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002A20 File Offset: 0x00000C20
		void IPoolable.Free()
		{
			GraphStack stack = this.stack;
			if (stack != null)
			{
				stack.Dispose();
			}
			Recursion<Flow.RecursionNode> recursion = this.recursion;
			if (recursion != null)
			{
				recursion.Dispose();
			}
			this.locals.Clear();
			this.loops.Clear();
			this.variables.Clear();
			foreach (GraphStack graphStack in this.preservedStacks)
			{
				graphStack.Dispose();
			}
			this.preservedStacks.Clear();
			this.loopIdentifier = -1;
			this.stack = null;
			this.recursion = null;
			this.isCoroutine = false;
			this.coroutineEnumerator = null;
			this.coroutineRunner = null;
			ICollection<Flow> collection = this.activeCoroutinesRegistry;
			if (collection != null)
			{
				collection.Remove(this);
			}
			this.activeCoroutinesRegistry = null;
			this.coroutineStopRequested = false;
			this.isPrediction = false;
			this.disposed = true;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002B18 File Offset: 0x00000D18
		public GraphStack PreserveStack()
		{
			GraphStack graphStack = this.stack.Clone();
			this.preservedStacks.Add(graphStack);
			return graphStack;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002B3F File Offset: 0x00000D3F
		public void RestoreStack(GraphStack stack)
		{
			this.stack.CopyFrom(stack);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00002B4D File Offset: 0x00000D4D
		public void DisposePreservedStack(GraphStack stack)
		{
			stack.Dispose();
			this.preservedStacks.Remove(stack);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002B62 File Offset: 0x00000D62
		public int currentLoop
		{
			get
			{
				if (this.loops.Count > 0)
				{
					return this.loops.Peek();
				}
				return -1;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00002B7F File Offset: 0x00000D7F
		public bool LoopIsNotBroken(int loop)
		{
			return this.currentLoop == loop;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00002B8C File Offset: 0x00000D8C
		public int EnterLoop()
		{
			int num = this.loopIdentifier + 1;
			this.loopIdentifier = num;
			int num2 = num;
			this.loops.Push(num2);
			return num2;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00002BB8 File Offset: 0x00000DB8
		public void BreakLoop()
		{
			if (this.currentLoop < 0)
			{
				throw new InvalidOperationException("No active loop to break.");
			}
			this.loops.Pop();
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002BDA File Offset: 0x00000DDA
		public void ExitLoop(int loop)
		{
			if (loop != this.currentLoop)
			{
				return;
			}
			this.loops.Pop();
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002BF2 File Offset: 0x00000DF2
		public void Run(ControlOutput port)
		{
			this.Invoke(port);
			this.Dispose();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002C04 File Offset: 0x00000E04
		public void StartCoroutine(ControlOutput port, ICollection<Flow> registry = null)
		{
			this.isCoroutine = true;
			this.coroutineRunner = this.stack.component;
			if (this.coroutineRunner == null)
			{
				this.coroutineRunner = CoroutineRunner.instance;
			}
			this.activeCoroutinesRegistry = registry;
			ICollection<Flow> collection = this.activeCoroutinesRegistry;
			if (collection != null)
			{
				collection.Add(this);
			}
			this.coroutineEnumerator = this.Coroutine(port);
			this.coroutineRunner.StartCoroutine(this.coroutineEnumerator);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00002C7A File Offset: 0x00000E7A
		public void StopCoroutine(bool disposeInstantly)
		{
			if (!this.isCoroutine)
			{
				throw new NotSupportedException("Stop may only be called on coroutines.");
			}
			if (disposeInstantly)
			{
				this.StopCoroutineImmediate();
				return;
			}
			this.coroutineStopRequested = true;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00002CA0 File Offset: 0x00000EA0
		internal void StopCoroutineImmediate()
		{
			if (this.coroutineRunner && this.coroutineEnumerator != null)
			{
				this.coroutineRunner.StopCoroutine(this.coroutineEnumerator);
				((IDisposable)this.coroutineEnumerator).Dispose();
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00002CD8 File Offset: 0x00000ED8
		private IEnumerator Coroutine(ControlOutput startPort)
		{
			try
			{
				foreach (object obj in this.InvokeCoroutine(startPort))
				{
					if (this.coroutineStopRequested)
					{
						yield break;
					}
					yield return obj;
					if (this.coroutineStopRequested)
					{
						yield break;
					}
				}
				IEnumerator enumerator = null;
			}
			finally
			{
				if (!this.disposed)
				{
					this.Dispose();
				}
			}
			yield break;
			yield break;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public void Invoke(ControlOutput output)
		{
			Ensure.That("output").IsNotNull<ControlOutput>(output);
			ControlConnection connection = output.connection;
			if (connection == null)
			{
				return;
			}
			ControlInput destination = connection.destination;
			Flow.RecursionNode recursionNode = new Flow.RecursionNode(output, this.stack);
			this.BeforeInvoke(output, recursionNode);
			try
			{
				ControlOutput controlOutput = this.InvokeDelegate(destination);
				if (controlOutput != null)
				{
					this.Invoke(controlOutput);
				}
			}
			finally
			{
				this.AfterInvoke(output, recursionNode);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002D64 File Offset: 0x00000F64
		private IEnumerable InvokeCoroutine(ControlOutput output)
		{
			ControlConnection connection = output.connection;
			if (connection == null)
			{
				yield break;
			}
			ControlInput destination = connection.destination;
			Flow.RecursionNode recursionNode = new Flow.RecursionNode(output, this.stack);
			this.BeforeInvoke(output, recursionNode);
			if (destination.supportsCoroutine)
			{
				foreach (object obj in this.InvokeCoroutineDelegate(destination))
				{
					if (obj is ControlOutput)
					{
						foreach (object obj2 in this.InvokeCoroutine((ControlOutput)obj))
						{
							yield return obj2;
						}
						IEnumerator enumerator2 = null;
					}
					else
					{
						yield return obj;
					}
				}
				IEnumerator enumerator = null;
			}
			else
			{
				ControlOutput controlOutput = this.InvokeDelegate(destination);
				if (controlOutput != null)
				{
					foreach (object obj3 in this.InvokeCoroutine(controlOutput))
					{
						yield return obj3;
					}
					IEnumerator enumerator = null;
				}
			}
			this.AfterInvoke(output, recursionNode);
			yield break;
			yield break;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002D7C File Offset: 0x00000F7C
		private Flow.RecursionNode BeforeInvoke(ControlOutput output, Flow.RecursionNode recursionNode)
		{
			try
			{
				Recursion<Flow.RecursionNode> recursion = this.recursion;
				if (recursion != null)
				{
					recursion.Enter(recursionNode);
				}
			}
			catch (StackOverflowException ex)
			{
				output.unit.HandleException(this.stack, ex);
				throw;
			}
			ControlConnection connection = output.connection;
			ControlInput destination = connection.destination;
			if (this.enableDebug)
			{
				IUnitConnectionDebugData elementDebugData = this.stack.GetElementDebugData<IUnitConnectionDebugData>(connection);
				IUnitDebugData elementDebugData2 = this.stack.GetElementDebugData<IUnitDebugData>(destination.unit);
				elementDebugData.lastInvokeFrame = EditorTimeBinding.frame;
				elementDebugData.lastInvokeTime = EditorTimeBinding.time;
				elementDebugData2.lastInvokeFrame = EditorTimeBinding.frame;
				elementDebugData2.lastInvokeTime = EditorTimeBinding.time;
			}
			return recursionNode;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002E24 File Offset: 0x00001024
		private void AfterInvoke(ControlOutput output, Flow.RecursionNode recursionNode)
		{
			Recursion<Flow.RecursionNode> recursion = this.recursion;
			if (recursion == null)
			{
				return;
			}
			recursion.Exit(recursionNode);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002E38 File Offset: 0x00001038
		private ControlOutput InvokeDelegate(ControlInput input)
		{
			ControlOutput controlOutput;
			try
			{
				if (input.requiresCoroutine)
				{
					throw new InvalidOperationException(string.Format("Port '{0}' on '{1}' can only be triggered in a coroutine.", input.key, input.unit));
				}
				controlOutput = input.action(this);
			}
			catch (Exception ex)
			{
				input.unit.HandleException(this.stack, ex);
				throw;
			}
			return controlOutput;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002EA0 File Offset: 0x000010A0
		private IEnumerable InvokeCoroutineDelegate(ControlInput input)
		{
			IEnumerator instructions = input.coroutineAction(this);
			for (;;)
			{
				object obj;
				try
				{
					if (!instructions.MoveNext())
					{
						yield break;
					}
					obj = instructions.Current;
				}
				catch (Exception ex)
				{
					input.unit.HandleException(this.stack, ex);
					throw;
				}
				yield return obj;
			}
			yield break;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00002EB7 File Offset: 0x000010B7
		public bool IsLocal(IUnitValuePort port)
		{
			Ensure.That("port").IsNotNull<IUnitValuePort>(port);
			return this.locals.ContainsKey(port);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002ED8 File Offset: 0x000010D8
		public void SetValue(IUnitValuePort port, object value)
		{
			Ensure.That("port").IsNotNull<IUnitValuePort>(port);
			Ensure.That("value").IsOfType<object>(value, port.type);
			if (this.locals.ContainsKey(port))
			{
				this.locals[port] = value;
				return;
			}
			this.locals.Add(port, value);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00002F34 File Offset: 0x00001134
		public object GetValue(ValueInput input)
		{
			object obj;
			if (this.locals.TryGetValue(input, out obj))
			{
				return obj;
			}
			ValueConnection connection = input.connection;
			if (connection != null)
			{
				if (this.enableDebug)
				{
					IUnitConnectionDebugData elementDebugData = this.stack.GetElementDebugData<IUnitConnectionDebugData>(connection);
					elementDebugData.lastInvokeFrame = EditorTimeBinding.frame;
					elementDebugData.lastInvokeTime = EditorTimeBinding.time;
				}
				ValueOutput source = connection.source;
				object value = this.GetValue(source);
				if (this.enableDebug)
				{
					ValueConnection.DebugData elementDebugData2 = this.stack.GetElementDebugData<ValueConnection.DebugData>(connection);
					elementDebugData2.lastValue = value;
					elementDebugData2.assignedLastValue = true;
				}
				return value;
			}
			object obj2;
			if (this.TryGetDefaultValue(input, out obj2))
			{
				return obj2;
			}
			throw new MissingValuePortInputException(input.key);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00002FD4 File Offset: 0x000011D4
		private object GetValue(ValueOutput output)
		{
			object obj;
			if (this.locals.TryGetValue(output, out obj))
			{
				return obj;
			}
			if (!output.supportsFetch)
			{
				throw new InvalidOperationException(string.Format("The value of '{0}' on '{1}' cannot be fetched dynamically, it must be assigned.", output.key, output.unit));
			}
			Flow.RecursionNode recursionNode = new Flow.RecursionNode(output, this.stack);
			try
			{
				Recursion<Flow.RecursionNode> recursion = this.recursion;
				if (recursion != null)
				{
					recursion.Enter(recursionNode);
				}
			}
			catch (StackOverflowException ex)
			{
				output.unit.HandleException(this.stack, ex);
				throw;
			}
			object valueDelegate;
			try
			{
				if (this.enableDebug)
				{
					IUnitDebugData elementDebugData = this.stack.GetElementDebugData<IUnitDebugData>(output.unit);
					elementDebugData.lastInvokeFrame = EditorTimeBinding.frame;
					elementDebugData.lastInvokeTime = EditorTimeBinding.time;
				}
				valueDelegate = this.GetValueDelegate(output);
			}
			finally
			{
				Recursion<Flow.RecursionNode> recursion2 = this.recursion;
				if (recursion2 != null)
				{
					recursion2.Exit(recursionNode);
				}
			}
			return valueDelegate;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000030B8 File Offset: 0x000012B8
		public object GetValue(ValueInput input, Type type)
		{
			return ConversionUtility.Convert(this.GetValue(input), type);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000030C7 File Offset: 0x000012C7
		public T GetValue<T>(ValueInput input)
		{
			return (T)((object)this.GetValue(input, typeof(T)));
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000030DF File Offset: 0x000012DF
		public object GetConvertedValue(ValueInput input)
		{
			return this.GetValue(input, input.type);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000030F0 File Offset: 0x000012F0
		private object GetDefaultValue(ValueInput input)
		{
			object obj;
			if (!this.TryGetDefaultValue(input, out obj))
			{
				throw new InvalidOperationException("Value input port does not have a default value.");
			}
			return obj;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003114 File Offset: 0x00001314
		public bool TryGetDefaultValue(ValueInput input, out object defaultValue)
		{
			if (!input.unit.defaultValues.TryGetValue(input.key, out defaultValue))
			{
				return false;
			}
			if (input.nullMeansSelf && defaultValue == null)
			{
				defaultValue = this.stack.self;
			}
			return true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000314C File Offset: 0x0000134C
		private object GetValueDelegate(ValueOutput output)
		{
			object obj;
			try
			{
				obj = output.getValue(this);
			}
			catch (Exception ex)
			{
				output.unit.HandleException(this.stack, ex);
				throw;
			}
			return obj;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003190 File Offset: 0x00001390
		public static object FetchValue(ValueInput input, GraphReference reference)
		{
			Flow flow = Flow.New(reference);
			object value = flow.GetValue(input);
			flow.Dispose();
			return value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000031B1 File Offset: 0x000013B1
		public static object FetchValue(ValueInput input, Type type, GraphReference reference)
		{
			return ConversionUtility.Convert(Flow.FetchValue(input, reference), type);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000031C0 File Offset: 0x000013C0
		public static T FetchValue<T>(ValueInput input, GraphReference reference)
		{
			return (T)((object)Flow.FetchValue(input, typeof(T), reference));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000031D8 File Offset: 0x000013D8
		public static bool CanPredict(IUnitValuePort port, GraphReference reference)
		{
			Ensure.That("port").IsNotNull<IUnitValuePort>(port);
			Flow flow = Flow.New(reference);
			flow.isPrediction = true;
			bool flag;
			if (port is ValueInput)
			{
				flag = flow.CanPredict((ValueInput)port);
			}
			else
			{
				if (!(port is ValueOutput))
				{
					throw new NotSupportedException();
				}
				flag = flow.CanPredict((ValueOutput)port);
			}
			flow.Dispose();
			return flag;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003240 File Offset: 0x00001440
		private bool CanPredict(ValueInput input)
		{
			if (!input.hasValidConnection)
			{
				object obj;
				if (!this.TryGetDefaultValue(input, out obj))
				{
					return false;
				}
				if (typeof(Component).IsAssignableFrom(input.type))
				{
					obj = ((obj != null) ? obj.ConvertTo(input.type) : null);
				}
				return input.allowsNull || obj != null;
			}
			else
			{
				ValueOutput valueOutput = input.validConnectedPorts.Single<ValueOutput>();
				if (!this.CanPredict(valueOutput))
				{
					return false;
				}
				object obj2 = this.GetValue(valueOutput);
				if (!ConversionUtility.CanConvert(obj2, input.type, false))
				{
					return false;
				}
				if (typeof(Component).IsAssignableFrom(input.type))
				{
					obj2 = ((obj2 != null) ? obj2.ConvertTo(input.type) : null);
				}
				return input.allowsNull || obj2 != null;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003304 File Offset: 0x00001504
		private bool CanPredict(ValueOutput output)
		{
			if (!output.supportsPrediction)
			{
				return false;
			}
			Flow.RecursionNode recursionNode = new Flow.RecursionNode(output, this.stack);
			Recursion<Flow.RecursionNode> recursion = this.recursion;
			if (recursion != null && !recursion.TryEnter(recursionNode))
			{
				return false;
			}
			foreach (IUnitRelation unitRelation in output.unit.relations.WithDestination(output))
			{
				if (unitRelation.source is ValueInput)
				{
					ValueInput valueInput = (ValueInput)unitRelation.source;
					if (!this.CanPredict(valueInput))
					{
						Recursion<Flow.RecursionNode> recursion2 = this.recursion;
						if (recursion2 != null)
						{
							recursion2.Exit(recursionNode);
						}
						return false;
					}
				}
			}
			bool flag = this.CanPredictDelegate(output);
			Recursion<Flow.RecursionNode> recursion3 = this.recursion;
			if (recursion3 == null)
			{
				return flag;
			}
			recursion3.Exit(recursionNode);
			return flag;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000033DC File Offset: 0x000015DC
		private bool CanPredictDelegate(ValueOutput output)
		{
			bool flag;
			try
			{
				flag = output.canPredictValue(this);
			}
			catch (Exception ex)
			{
				Debug.LogWarning(string.Format("Prediction check failed for '{0}' on '{1}':\n{2}", output.key, output.unit, ex));
				flag = false;
			}
			return flag;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000342C File Offset: 0x0000162C
		public static object Predict(IUnitValuePort port, GraphReference reference)
		{
			Ensure.That("port").IsNotNull<IUnitValuePort>(port);
			Flow flow = Flow.New(reference);
			flow.isPrediction = true;
			object obj;
			if (port is ValueInput)
			{
				obj = flow.GetValue((ValueInput)port);
			}
			else
			{
				if (!(port is ValueOutput))
				{
					throw new NotSupportedException();
				}
				obj = flow.GetValue((ValueOutput)port);
			}
			flow.Dispose();
			return obj;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003492 File Offset: 0x00001692
		public static object Predict(IUnitValuePort port, GraphReference reference, Type type)
		{
			return ConversionUtility.Convert(Flow.Predict(port, reference), type);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000034A1 File Offset: 0x000016A1
		public static T Predict<T>(IUnitValuePort port, GraphReference pointer)
		{
			return (T)((object)Flow.Predict(port, pointer, typeof(T)));
		}

		// Token: 0x04000018 RID: 24
		private Recursion<Flow.RecursionNode> recursion;

		// Token: 0x04000019 RID: 25
		private readonly Dictionary<IUnitValuePort, object> locals = new Dictionary<IUnitValuePort, object>();

		// Token: 0x0400001A RID: 26
		public readonly VariableDeclarations variables = new VariableDeclarations();

		// Token: 0x0400001B RID: 27
		private readonly Stack<int> loops = new Stack<int>();

		// Token: 0x0400001C RID: 28
		private readonly HashSet<GraphStack> preservedStacks = new HashSet<GraphStack>();

		// Token: 0x0400001E RID: 30
		private ICollection<Flow> activeCoroutinesRegistry;

		// Token: 0x0400001F RID: 31
		private bool coroutineStopRequested;

		// Token: 0x04000021 RID: 33
		private IEnumerator coroutineEnumerator;

		// Token: 0x04000023 RID: 35
		private bool disposed;

		// Token: 0x04000025 RID: 37
		public int loopIdentifier = -1;

		// Token: 0x020001A0 RID: 416
		private struct RecursionNode : IEquatable<Flow.RecursionNode>
		{
			// Token: 0x170003BF RID: 959
			// (get) Token: 0x06000B74 RID: 2932 RVA: 0x0001A3C6 File Offset: 0x000185C6
			public readonly IUnitPort port { get; }

			// Token: 0x170003C0 RID: 960
			// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001A3CE File Offset: 0x000185CE
			public readonly IGraphParent context { get; }

			// Token: 0x06000B76 RID: 2934 RVA: 0x0001A3D6 File Offset: 0x000185D6
			public RecursionNode(IUnitPort port, GraphPointer pointer)
			{
				this.port = port;
				this.context = pointer.parent;
			}

			// Token: 0x06000B77 RID: 2935 RVA: 0x0001A3EB File Offset: 0x000185EB
			public bool Equals(Flow.RecursionNode other)
			{
				return other.port == this.port && other.context == this.context;
			}

			// Token: 0x06000B78 RID: 2936 RVA: 0x0001A410 File Offset: 0x00018610
			public override bool Equals(object obj)
			{
				if (obj is Flow.RecursionNode)
				{
					Flow.RecursionNode recursionNode = (Flow.RecursionNode)obj;
					return this.Equals(recursionNode);
				}
				return false;
			}

			// Token: 0x06000B79 RID: 2937 RVA: 0x0001A435 File Offset: 0x00018635
			public override int GetHashCode()
			{
				return HashUtility.GetHashCode<IUnitPort, IGraphParent>(this.port, this.context);
			}
		}
	}
}
