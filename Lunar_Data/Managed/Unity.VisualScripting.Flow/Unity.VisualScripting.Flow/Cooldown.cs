using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x0200012E RID: 302
	[UnitCategory("Time")]
	[TypeIcon(typeof(Timer))]
	[UnitOrder(8)]
	public sealed class Cooldown : Unit, IGraphElementWithData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener
	{
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0000E8D4 File Offset: 0x0000CAD4
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0000E8DC File Offset: 0x0000CADC
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0000E8E5 File Offset: 0x0000CAE5
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0000E8ED File Offset: 0x0000CAED
		[DoNotSerialize]
		public ControlInput reset { get; private set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0000E8F6 File Offset: 0x0000CAF6
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0000E8FE File Offset: 0x0000CAFE
		[DoNotSerialize]
		public ValueInput duration { get; private set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0000E907 File Offset: 0x0000CB07
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0000E90F File Offset: 0x0000CB0F
		[DoNotSerialize]
		[PortLabel("Unscaled")]
		public ValueInput unscaledTime { get; private set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0000E918 File Offset: 0x0000CB18
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0000E920 File Offset: 0x0000CB20
		[DoNotSerialize]
		[PortLabel("Ready")]
		public ControlOutput exitReady { get; private set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0000E929 File Offset: 0x0000CB29
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0000E931 File Offset: 0x0000CB31
		[DoNotSerialize]
		[PortLabel("Not Ready")]
		public ControlOutput exitNotReady { get; private set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0000E93A File Offset: 0x0000CB3A
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0000E942 File Offset: 0x0000CB42
		[DoNotSerialize]
		public ControlOutput tick { get; private set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0000E94B File Offset: 0x0000CB4B
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0000E953 File Offset: 0x0000CB53
		[DoNotSerialize]
		[PortLabel("Completed")]
		public ControlOutput becameReady { get; private set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x0000E95C File Offset: 0x0000CB5C
		// (set) Token: 0x060007E8 RID: 2024 RVA: 0x0000E964 File Offset: 0x0000CB64
		[DoNotSerialize]
		[PortLabel("Remaining")]
		public ValueOutput remainingSeconds { get; private set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060007E9 RID: 2025 RVA: 0x0000E96D File Offset: 0x0000CB6D
		// (set) Token: 0x060007EA RID: 2026 RVA: 0x0000E975 File Offset: 0x0000CB75
		[DoNotSerialize]
		[PortLabel("Remaining %")]
		public ValueOutput remainingRatio { get; private set; }

		// Token: 0x060007EB RID: 2027 RVA: 0x0000E980 File Offset: 0x0000CB80
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.reset = base.ControlInput("reset", new Func<Flow, ControlOutput>(this.Reset));
			this.duration = base.ValueInput<float>("duration", 1f);
			this.unscaledTime = base.ValueInput<bool>("unscaledTime", false);
			this.exitReady = base.ControlOutput("exitReady");
			this.exitNotReady = base.ControlOutput("exitNotReady");
			this.tick = base.ControlOutput("tick");
			this.becameReady = base.ControlOutput("becameReady");
			this.remainingSeconds = base.ValueOutput<float>("remainingSeconds");
			this.remainingRatio = base.ValueOutput<float>("remainingRatio");
			base.Requirement(this.duration, this.enter);
			base.Requirement(this.unscaledTime, this.enter);
			base.Succession(this.enter, this.exitReady);
			base.Succession(this.enter, this.exitNotReady);
			base.Succession(this.enter, this.tick);
			base.Succession(this.enter, this.becameReady);
			base.Assignment(this.enter, this.remainingSeconds);
			base.Assignment(this.enter, this.remainingRatio);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x0000EAE5 File Offset: 0x0000CCE5
		public IGraphElementData CreateData()
		{
			return new Cooldown.Data();
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0000EAEC File Offset: 0x0000CCEC
		public void StartListening(GraphStack stack)
		{
			Cooldown.Data elementData = stack.GetElementData<Cooldown.Data>(this);
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

		// Token: 0x060007EE RID: 2030 RVA: 0x0000EB58 File Offset: 0x0000CD58
		public void StopListening(GraphStack stack)
		{
			Cooldown.Data elementData = stack.GetElementData<Cooldown.Data>(this);
			if (!elementData.isListening)
			{
				return;
			}
			EventBus.Unregister(new EventHook("Update", stack.machine, null), elementData.update);
			elementData.update = null;
			elementData.isListening = false;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0000EBA0 File Offset: 0x0000CDA0
		public bool IsListening(GraphPointer pointer)
		{
			return pointer.GetElementData<Cooldown.Data>(this).isListening;
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0000EBB0 File Offset: 0x0000CDB0
		private void TriggerUpdate(GraphReference reference)
		{
			using (Flow flow = Flow.New(reference))
			{
				this.Update(flow);
			}
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		private ControlOutput Enter(Flow flow)
		{
			if (flow.stack.GetElementData<Cooldown.Data>(this).isReady)
			{
				return this.Reset(flow);
			}
			return this.exitNotReady;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		private ControlOutput Reset(Flow flow)
		{
			Cooldown.Data elementData = flow.stack.GetElementData<Cooldown.Data>(this);
			elementData.duration = flow.GetValue<float>(this.duration);
			elementData.remaining = elementData.duration;
			elementData.unscaled = flow.GetValue<bool>(this.unscaledTime);
			return this.exitReady;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0000EC5A File Offset: 0x0000CE5A
		private void AssignMetrics(Flow flow, Cooldown.Data data)
		{
			flow.SetValue(this.remainingSeconds, data.remaining);
			flow.SetValue(this.remainingRatio, Mathf.Clamp01(data.remaining / data.duration));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0000EC98 File Offset: 0x0000CE98
		public void Update(Flow flow)
		{
			Cooldown.Data elementData = flow.stack.GetElementData<Cooldown.Data>(this);
			if (elementData.isReady)
			{
				return;
			}
			elementData.remaining -= (elementData.unscaled ? Time.unscaledDeltaTime : Time.deltaTime);
			elementData.remaining = Mathf.Max(0f, elementData.remaining);
			this.AssignMetrics(flow, elementData);
			GraphStack graphStack = flow.PreserveStack();
			flow.Invoke(this.tick);
			if (elementData.isReady)
			{
				flow.RestoreStack(graphStack);
				flow.Invoke(this.becameReady);
			}
			flow.DisposePreservedStack(graphStack);
		}

		// Token: 0x020001C0 RID: 448
		public sealed class Data : IGraphElementData
		{
			// Token: 0x170003D3 RID: 979
			// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x0001B619 File Offset: 0x00019819
			public bool isReady
			{
				get
				{
					return this.remaining <= 0f;
				}
			}

			// Token: 0x040003BA RID: 954
			public float remaining;

			// Token: 0x040003BB RID: 955
			public float duration;

			// Token: 0x040003BC RID: 956
			public bool unscaled;

			// Token: 0x040003BD RID: 957
			public Delegate update;

			// Token: 0x040003BE RID: 958
			public bool isListening;
		}
	}
}
