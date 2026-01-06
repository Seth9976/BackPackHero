using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012F RID: 303
	[UnitCategory("Time")]
	[UnitOrder(7)]
	public sealed class Timer : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener
	{
		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x0000ED37 File Offset: 0x0000CF37
		// (set) Token: 0x060007F7 RID: 2039 RVA: 0x0000ED3F File Offset: 0x0000CF3F
		[DoNotSerialize]
		public ControlInput start { get; private set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0000ED48 File Offset: 0x0000CF48
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0000ED50 File Offset: 0x0000CF50
		[DoNotSerialize]
		public ControlInput pause { get; private set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0000ED59 File Offset: 0x0000CF59
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0000ED61 File Offset: 0x0000CF61
		[DoNotSerialize]
		public ControlInput resume { get; private set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0000ED72 File Offset: 0x0000CF72
		[DoNotSerialize]
		public ControlInput toggle { get; private set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x0000ED7B File Offset: 0x0000CF7B
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x0000ED83 File Offset: 0x0000CF83
		[DoNotSerialize]
		public ValueInput duration { get; private set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0000ED94 File Offset: 0x0000CF94
		[DoNotSerialize]
		[PortLabel("Unscaled")]
		public ValueInput unscaledTime { get; private set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0000ED9D File Offset: 0x0000CF9D
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0000EDA5 File Offset: 0x0000CFA5
		[DoNotSerialize]
		public ControlOutput started { get; private set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0000EDB6 File Offset: 0x0000CFB6
		[DoNotSerialize]
		public ControlOutput tick { get; private set; }

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0000EDBF File Offset: 0x0000CFBF
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0000EDC7 File Offset: 0x0000CFC7
		[DoNotSerialize]
		public ControlOutput completed { get; private set; }

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0000EDD8 File Offset: 0x0000CFD8
		[DoNotSerialize]
		[PortLabel("Elapsed")]
		public ValueOutput elapsedSeconds { get; private set; }

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x0000EDE9 File Offset: 0x0000CFE9
		[DoNotSerialize]
		[PortLabel("Elapsed %")]
		public ValueOutput elapsedRatio { get; private set; }

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		// (set) Token: 0x0600080D RID: 2061 RVA: 0x0000EDFA File Offset: 0x0000CFFA
		[DoNotSerialize]
		[PortLabel("Remaining")]
		public ValueOutput remainingSeconds { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x0000EE03 File Offset: 0x0000D003
		// (set) Token: 0x0600080F RID: 2063 RVA: 0x0000EE0B File Offset: 0x0000D00B
		[DoNotSerialize]
		[PortLabel("Remaining %")]
		public ValueOutput remainingRatio { get; private set; }

		// Token: 0x06000810 RID: 2064 RVA: 0x0000EE14 File Offset: 0x0000D014
		protected override void Definition()
		{
			this.isControlRoot = true;
			this.start = base.ControlInput("start", new Func<Flow, ControlOutput>(this.Start));
			this.pause = base.ControlInput("pause", new Func<Flow, ControlOutput>(this.Pause));
			this.resume = base.ControlInput("resume", new Func<Flow, ControlOutput>(this.Resume));
			this.toggle = base.ControlInput("toggle", new Func<Flow, ControlOutput>(this.Toggle));
			this.duration = base.ValueInput<float>("duration", 1f);
			this.unscaledTime = base.ValueInput<bool>("unscaledTime", false);
			this.started = base.ControlOutput("started");
			this.tick = base.ControlOutput("tick");
			this.completed = base.ControlOutput("completed");
			this.elapsedSeconds = base.ValueOutput<float>("elapsedSeconds");
			this.elapsedRatio = base.ValueOutput<float>("elapsedRatio");
			this.remainingSeconds = base.ValueOutput<float>("remainingSeconds");
			this.remainingRatio = base.ValueOutput<float>("remainingRatio");
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0000EF3B File Offset: 0x0000D13B
		public IGraphElementData CreateData()
		{
			return new Timer.Data();
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0000EF44 File Offset: 0x0000D144
		public void StartListening(GraphStack stack)
		{
			Timer.Data elementData = stack.GetElementData<Timer.Data>(this);
			if (elementData.isListening)
			{
				return;
			}
			GraphReference reference = stack.ToReference();
			EventHook eventHook = new EventHook("Update", stack.machine, null);
			Action<EmptyEventArgs> action = delegate(EmptyEventArgs args)
			{
				this.TriggerUpdate(reference);
			};
			EventBus.Register<EmptyEventArgs>(eventHook, action);
			elementData.update = action;
			elementData.isListening = true;
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0000EFB0 File Offset: 0x0000D1B0
		public void StopListening(GraphStack stack)
		{
			Timer.Data elementData = stack.GetElementData<Timer.Data>(this);
			if (!elementData.isListening)
			{
				return;
			}
			EventBus.Unregister(new EventHook("Update", stack.machine, null), elementData.update);
			elementData.update = null;
			elementData.isListening = false;
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0000EFF8 File Offset: 0x0000D1F8
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<Timer.Data>(this).isListening;
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0000F008 File Offset: 0x0000D208
		private void TriggerUpdate(GraphReference reference)
		{
			using (Flow flow = Flow.New(reference))
			{
				this.Update(flow);
			}
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0000F040 File Offset: 0x0000D240
		private ControlOutput Start(Flow flow)
		{
			Timer.Data elementData = flow.stack.GetElementData<Timer.Data>(this);
			elementData.elapsed = 0f;
			elementData.duration = flow.GetValue<float>(this.duration);
			elementData.active = true;
			elementData.paused = false;
			elementData.unscaled = flow.GetValue<bool>(this.unscaledTime);
			this.AssignMetrics(flow, elementData);
			return this.started;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0000F0A5 File Offset: 0x0000D2A5
		private ControlOutput Pause(Flow flow)
		{
			flow.stack.GetElementData<Timer.Data>(this).paused = true;
			return null;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0000F0BA File Offset: 0x0000D2BA
		private ControlOutput Resume(Flow flow)
		{
			flow.stack.GetElementData<Timer.Data>(this).paused = false;
			return null;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		private ControlOutput Toggle(Flow flow)
		{
			Timer.Data elementData = flow.stack.GetElementData<Timer.Data>(this);
			if (!elementData.active)
			{
				return this.Start(flow);
			}
			elementData.paused = !elementData.paused;
			return null;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0000F10C File Offset: 0x0000D30C
		private void AssignMetrics(Flow flow, Timer.Data data)
		{
			flow.SetValue(this.elapsedSeconds, data.elapsed);
			flow.SetValue(this.elapsedRatio, Mathf.Clamp01(data.elapsed / data.duration));
			flow.SetValue(this.remainingSeconds, Mathf.Max(0f, data.duration - data.elapsed));
			flow.SetValue(this.remainingRatio, Mathf.Clamp01((data.duration - data.elapsed) / data.duration));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0000F1A8 File Offset: 0x0000D3A8
		public void Update(Flow flow)
		{
			Timer.Data elementData = flow.stack.GetElementData<Timer.Data>(this);
			if (!elementData.active || elementData.paused)
			{
				return;
			}
			elementData.elapsed += (elementData.unscaled ? Time.unscaledDeltaTime : Time.deltaTime);
			elementData.elapsed = Mathf.Min(elementData.elapsed, elementData.duration);
			this.AssignMetrics(flow, elementData);
			GraphStack graphStack = flow.PreserveStack();
			flow.Invoke(this.tick);
			if (elementData.elapsed >= elementData.duration)
			{
				elementData.active = false;
				flow.RestoreStack(graphStack);
				flow.Invoke(this.completed);
			}
			flow.DisposePreservedStack(graphStack);
		}

		// Token: 0x020001C2 RID: 450
		public sealed class Data : IGraphElementData
		{
			// Token: 0x040003C1 RID: 961
			public float elapsed;

			// Token: 0x040003C2 RID: 962
			public float duration;

			// Token: 0x040003C3 RID: 963
			public bool active;

			// Token: 0x040003C4 RID: 964
			public bool paused;

			// Token: 0x040003C5 RID: 965
			public bool unscaled;

			// Token: 0x040003C6 RID: 966
			public Delegate update;

			// Token: 0x040003C7 RID: 967
			public bool isListening;
		}
	}
}
