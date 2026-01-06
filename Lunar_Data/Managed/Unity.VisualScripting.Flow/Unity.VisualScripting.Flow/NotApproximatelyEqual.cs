using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000C0 RID: 192
	[UnitCategory("Logic")]
	[UnitShortTitle("Not Equal")]
	[UnitSubtitle("(Approximately)")]
	[UnitOrder(8)]
	[Obsolete("Use the Not Equal node with Numeric enabled instead.")]
	public sealed class NotApproximatelyEqual : Unit
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000BB97 File Offset: 0x00009D97
		// (set) Token: 0x060005BB RID: 1467 RVA: 0x0000BB9F File Offset: 0x00009D9F
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000BBA8 File Offset: 0x00009DA8
		// (set) Token: 0x060005BD RID: 1469 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000BBB9 File Offset: 0x00009DB9
		// (set) Token: 0x060005BF RID: 1471 RVA: 0x0000BBC1 File Offset: 0x00009DC1
		[DoNotSerialize]
		[PortLabel("A ≉ B")]
		public ValueOutput notEqual { get; private set; }

		// Token: 0x060005C0 RID: 1472 RVA: 0x0000BBCC File Offset: 0x00009DCC
		protected override void Definition()
		{
			this.a = base.ValueInput<float>("a");
			this.b = base.ValueInput<float>("b", 0f);
			this.notEqual = base.ValueOutput<bool>("notEqual", new Func<Flow, bool>(this.Comparison)).Predictable();
			base.Requirement(this.a, this.notEqual);
			base.Requirement(this.b, this.notEqual);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x0000BC46 File Offset: 0x00009E46
		public bool Comparison(Flow flow)
		{
			return !Mathf.Approximately(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b));
		}
	}
}
