using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000C2 RID: 194
	[UnitCategory("Logic")]
	[UnitTitle("Numeric Comparison")]
	[UnitSurtitle("Numeric")]
	[UnitShortTitle("Comparison")]
	[UnitOrder(99)]
	[Obsolete("Use the Comparison node with Numeric enabled instead.")]
	public sealed class NumericComparison : Unit
	{
		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x0000BCA3 File Offset: 0x00009EA3
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000BCAB File Offset: 0x00009EAB
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0000BCBC File Offset: 0x00009EBC
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0000BCCD File Offset: 0x00009ECD
		[DoNotSerialize]
		[PortLabel("A < B")]
		public ValueOutput aLessThanB { get; private set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x0000BCDE File Offset: 0x00009EDE
		[DoNotSerialize]
		[PortLabel("A ≤ B")]
		public ValueOutput aLessThanOrEqualToB { get; private set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x0000BCEF File Offset: 0x00009EEF
		[DoNotSerialize]
		[PortLabel("A = B")]
		public ValueOutput aEqualToB { get; private set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x0000BD00 File Offset: 0x00009F00
		[DoNotSerialize]
		[PortLabel("A ≥ B")]
		public ValueOutput aGreaterThanOrEqualToB { get; private set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0000BD09 File Offset: 0x00009F09
		// (set) Token: 0x060005D5 RID: 1493 RVA: 0x0000BD11 File Offset: 0x00009F11
		[DoNotSerialize]
		[PortLabel("A > B")]
		public ValueOutput aGreatherThanB { get; private set; }

		// Token: 0x060005D6 RID: 1494 RVA: 0x0000BD1C File Offset: 0x00009F1C
		protected override void Definition()
		{
			this.a = base.ValueInput<float>("a");
			this.b = base.ValueInput<float>("b", 0f);
			this.aLessThanB = base.ValueOutput<bool>("aLessThanB", new Func<Flow, bool>(this.Less)).Predictable();
			this.aLessThanOrEqualToB = base.ValueOutput<bool>("aLessThanOrEqualToB", new Func<Flow, bool>(this.LessOrEqual)).Predictable();
			this.aEqualToB = base.ValueOutput<bool>("aEqualToB", new Func<Flow, bool>(this.Equal)).Predictable();
			this.aGreaterThanOrEqualToB = base.ValueOutput<bool>("aGreaterThanOrEqualToB", new Func<Flow, bool>(this.GreaterOrEqual)).Predictable();
			this.aGreatherThanB = base.ValueOutput<bool>("aGreatherThanB", new Func<Flow, bool>(this.Greater)).Predictable();
			base.Requirement(this.a, this.aLessThanB);
			base.Requirement(this.b, this.aLessThanB);
			base.Requirement(this.a, this.aLessThanOrEqualToB);
			base.Requirement(this.b, this.aLessThanOrEqualToB);
			base.Requirement(this.a, this.aEqualToB);
			base.Requirement(this.b, this.aEqualToB);
			base.Requirement(this.a, this.aGreaterThanOrEqualToB);
			base.Requirement(this.b, this.aGreaterThanOrEqualToB);
			base.Requirement(this.a, this.aGreatherThanB);
			base.Requirement(this.b, this.aGreatherThanB);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x0000BEAE File Offset: 0x0000A0AE
		private bool Less(Flow flow)
		{
			return flow.GetValue<float>(this.a) < flow.GetValue<float>(this.b);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x0000BECC File Offset: 0x0000A0CC
		private bool LessOrEqual(Flow flow)
		{
			float value = flow.GetValue<float>(this.a);
			float value2 = flow.GetValue<float>(this.b);
			return value < value2 || Mathf.Approximately(value, value2);
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x0000BF00 File Offset: 0x0000A100
		private bool Equal(Flow flow)
		{
			return Mathf.Approximately(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b));
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x0000BF20 File Offset: 0x0000A120
		private bool GreaterOrEqual(Flow flow)
		{
			float value = flow.GetValue<float>(this.a);
			float value2 = flow.GetValue<float>(this.b);
			return value > value2 || Mathf.Approximately(value, value2);
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x0000BF54 File Offset: 0x0000A154
		private bool Greater(Flow flow)
		{
			return flow.GetValue<float>(this.a) < flow.GetValue<float>(this.b);
		}
	}
}
