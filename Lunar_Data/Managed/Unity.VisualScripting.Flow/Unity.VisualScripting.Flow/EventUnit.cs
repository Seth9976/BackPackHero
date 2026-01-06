using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200005B RID: 91
	[SerializationVersion("A", new Type[] { })]
	[SpecialUnit]
	public abstract class EventUnit<TArgs> : Unit, IEventUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener, IGraphElementWithData, IGraphEventHandler<TArgs>
	{
		// Token: 0x06000362 RID: 866 RVA: 0x000089E5 File Offset: 0x00006BE5
		public virtual IGraphElementData CreateData()
		{
			return new EventUnit<TArgs>.Data();
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000363 RID: 867 RVA: 0x000089EC File Offset: 0x00006BEC
		// (set) Token: 0x06000364 RID: 868 RVA: 0x000089F4 File Offset: 0x00006BF4
		[Serialize]
		[Inspectable]
		[InspectorExpandTooltip]
		public bool coroutine { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000089FD File Offset: 0x00006BFD
		// (set) Token: 0x06000366 RID: 870 RVA: 0x00008A05 File Offset: 0x00006C05
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput trigger { get; private set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000367 RID: 871
		[DoNotSerialize]
		protected abstract bool register { get; }

		// Token: 0x06000368 RID: 872 RVA: 0x00008A0E File Offset: 0x00006C0E
		protected override void Definition()
		{
			this.isControlRoot = true;
			this.trigger = base.ControlOutput("trigger");
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00008A28 File Offset: 0x00006C28
		public virtual EventHook GetHook(GraphReference reference)
		{
			throw new InvalidImplementationException(string.Format("Missing event hook for '{0}'.", this));
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00008A3C File Offset: 0x00006C3C
		public virtual void StartListening(GraphStack stack)
		{
			EventUnit<TArgs>.Data elementData = stack.GetElementData<EventUnit<TArgs>.Data>(this);
			if (elementData.isListening)
			{
				return;
			}
			if (this.register)
			{
				GraphReference reference = stack.ToReference();
				EventHook hook = this.GetHook(reference);
				Action<TArgs> action = delegate(TArgs args)
				{
					this.Trigger(reference, args);
				};
				EventBus.Register<TArgs>(hook, action);
				elementData.hook = hook;
				elementData.handler = action;
			}
			elementData.isListening = true;
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00008AB4 File Offset: 0x00006CB4
		public virtual void StopListening(GraphStack stack)
		{
			EventUnit<TArgs>.Data elementData = stack.GetElementData<EventUnit<TArgs>.Data>(this);
			if (!elementData.isListening)
			{
				return;
			}
			foreach (Flow flow in elementData.activeCoroutines)
			{
				flow.StopCoroutine(false);
			}
			if (this.register)
			{
				EventBus.Unregister(elementData.hook, elementData.handler);
				elementData.handler = null;
			}
			elementData.isListening = false;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00008B40 File Offset: 0x00006D40
		public override void Uninstantiate(GraphReference instance)
		{
			EventUnit<TArgs>.StopAllCoroutines(instance.GetElementData<EventUnit<TArgs>.Data>(this).activeCoroutines.ToHashSetPooled<Flow>());
			base.Uninstantiate(instance);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00008B60 File Offset: 0x00006D60
		private static void StopAllCoroutines(HashSet<Flow> activeCoroutines)
		{
			foreach (Flow flow in activeCoroutines)
			{
				flow.StopCoroutineImmediate();
			}
			activeCoroutines.Free<Flow>();
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00008BB4 File Offset: 0x00006DB4
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.hasData && pointer.GetElementData<EventUnit<TArgs>.Data>(this).isListening;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00008BCC File Offset: 0x00006DCC
		public void Trigger(GraphReference reference, TArgs args)
		{
			Flow flow = Flow.New(reference);
			if (!this.ShouldTrigger(flow, args))
			{
				flow.Dispose();
				return;
			}
			this.AssignArguments(flow, args);
			this.Run(flow);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00008C00 File Offset: 0x00006E00
		protected virtual bool ShouldTrigger(Flow flow, TArgs args)
		{
			return true;
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00008C03 File Offset: 0x00006E03
		protected virtual void AssignArguments(Flow flow, TArgs args)
		{
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00008C08 File Offset: 0x00006E08
		private void Run(Flow flow)
		{
			if (flow.enableDebug)
			{
				IUnitDebugData elementDebugData = flow.stack.GetElementDebugData<IUnitDebugData>(this);
				elementDebugData.lastInvokeFrame = EditorTimeBinding.frame;
				elementDebugData.lastInvokeTime = EditorTimeBinding.time;
			}
			if (this.coroutine)
			{
				flow.StartCoroutine(this.trigger, flow.stack.GetElementData<EventUnit<TArgs>.Data>(this).activeCoroutines);
				return;
			}
			flow.Run(this.trigger);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00008C70 File Offset: 0x00006E70
		protected static bool CompareNames(Flow flow, ValueInput namePort, string calledName)
		{
			Ensure.That("calledName").IsNotNull(calledName);
			string text = calledName.Trim();
			string value = flow.GetValue<string>(namePort);
			return text.Equals((value != null) ? value.Trim() : null, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00008CA9 File Offset: 0x00006EA9
		FlowGraph IUnit.get_graph()
		{
			return base.graph;
		}

		// Token: 0x020001B4 RID: 436
		public class Data : IGraphElementData
		{
			// Token: 0x040003A2 RID: 930
			public EventHook hook;

			// Token: 0x040003A3 RID: 931
			public Delegate handler;

			// Token: 0x040003A4 RID: 932
			public bool isListening;

			// Token: 0x040003A5 RID: 933
			public HashSet<Flow> activeCoroutines = new HashSet<Flow>();
		}
	}
}
