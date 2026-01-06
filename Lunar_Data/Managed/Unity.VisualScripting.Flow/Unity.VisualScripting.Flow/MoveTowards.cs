using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000D6 RID: 214
	[UnitOrder(502)]
	public abstract class MoveTowards<T> : Unit
	{
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000CC09 File Offset: 0x0000AE09
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000CC11 File Offset: 0x0000AE11
		[DoNotSerialize]
		public ValueInput current { get; private set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0000CC22 File Offset: 0x0000AE22
		[DoNotSerialize]
		public ValueInput target { get; private set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000CC2B File Offset: 0x0000AE2B
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0000CC33 File Offset: 0x0000AE33
		[DoNotSerialize]
		public ValueInput maxDelta { get; private set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000CC3C File Offset: 0x0000AE3C
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0000CC44 File Offset: 0x0000AE44
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueOutput result { get; private set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000CC4D File Offset: 0x0000AE4D
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0000CC55 File Offset: 0x0000AE55
		[Serialize]
		[Inspectable]
		[UnitHeaderInspectable("Per Second")]
		[InspectorToggleLeft]
		public bool perSecond { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000CC60 File Offset: 0x0000AE60
		[DoNotSerialize]
		protected virtual T defaultCurrent
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000CC78 File Offset: 0x0000AE78
		[DoNotSerialize]
		protected virtual T defaultTarget
		{
			get
			{
				return default(T);
			}
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0000CC90 File Offset: 0x0000AE90
		protected override void Definition()
		{
			this.current = base.ValueInput<T>("current", this.defaultCurrent);
			this.target = base.ValueInput<T>("target", this.defaultTarget);
			this.maxDelta = base.ValueInput<float>("maxDelta", 0f);
			this.result = base.ValueOutput<T>("result", new Func<Flow, T>(this.Operation));
			base.Requirement(this.current, this.result);
			base.Requirement(this.target, this.result);
			base.Requirement(this.maxDelta, this.result);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000CD34 File Offset: 0x0000AF34
		private T Operation(Flow flow)
		{
			return this.Operation(flow.GetValue<T>(this.current), flow.GetValue<T>(this.target), flow.GetValue<float>(this.maxDelta) * (this.perSecond ? Time.deltaTime : 1f));
		}

		// Token: 0x06000677 RID: 1655
		public abstract T Operation(T current, T target, float maxDelta);
	}
}
