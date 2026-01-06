using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000A7 RID: 167
	[UnitCategory("Events/Time")]
	[Obsolete("Use Wait For Seconds or Timer instead.")]
	public sealed class OnTimerElapsed : MachineEventUnit<EmptyEventArgs>
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0000A07C File Offset: 0x0000827C
		public override IGraphElementData CreateData()
		{
			return new OnTimerElapsed.Data();
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0000A083 File Offset: 0x00008283
		protected override string hookName
		{
			get
			{
				return "Update";
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000A08A File Offset: 0x0000828A
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000A092 File Offset: 0x00008292
		[DoNotSerialize]
		[PortLabel("Delay")]
		public ValueInput seconds { get; private set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000A09B File Offset: 0x0000829B
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000A0A3 File Offset: 0x000082A3
		[DoNotSerialize]
		[PortLabel("Unscaled")]
		public ValueInput unscaledTime { get; private set; }

		// Token: 0x060004D5 RID: 1237 RVA: 0x0000A0AC File Offset: 0x000082AC
		protected override void Definition()
		{
			base.Definition();
			this.seconds = base.ValueInput<float>("seconds", 0f);
			this.unscaledTime = base.ValueInput<bool>("unscaledTime", false);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0000A0DC File Offset: 0x000082DC
		public override void StartListening(GraphStack stack)
		{
			base.StartListening(stack);
			OnTimerElapsed.Data elementData = stack.GetElementData<OnTimerElapsed.Data>(this);
			elementData.triggered = false;
			elementData.time = 0f;
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x0000A100 File Offset: 0x00008300
		protected override bool ShouldTrigger(Flow flow, EmptyEventArgs args)
		{
			OnTimerElapsed.Data elementData = flow.stack.GetElementData<OnTimerElapsed.Data>(this);
			if (elementData.triggered)
			{
				return false;
			}
			float num = (flow.GetValue<bool>(this.unscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime);
			float value = flow.GetValue<float>(this.seconds);
			elementData.time += num;
			if (elementData.time >= value)
			{
				elementData.triggered = true;
				return true;
			}
			return false;
		}

		// Token: 0x020001B7 RID: 439
		public new class Data : EventUnit<EmptyEventArgs>.Data
		{
			// Token: 0x040003A9 RID: 937
			public float time;

			// Token: 0x040003AA RID: 938
			public bool triggered;
		}
	}
}
