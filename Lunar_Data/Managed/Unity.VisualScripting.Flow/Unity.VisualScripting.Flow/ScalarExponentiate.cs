using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E0 RID: 224
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Exponentiate")]
	[UnitOrder(105)]
	public sealed class ScalarExponentiate : Unit
	{
		// Token: 0x17000273 RID: 627
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000D1CD File Offset: 0x0000B3CD
		// (set) Token: 0x060006B7 RID: 1719 RVA: 0x0000D1D5 File Offset: 0x0000B3D5
		[DoNotSerialize]
		[PortLabel("x")]
		public ValueInput @base { get; private set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0000D1DE File Offset: 0x0000B3DE
		// (set) Token: 0x060006B9 RID: 1721 RVA: 0x0000D1E6 File Offset: 0x0000B3E6
		[DoNotSerialize]
		[PortLabel("n")]
		public ValueInput exponent { get; private set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x0000D1EF File Offset: 0x0000B3EF
		// (set) Token: 0x060006BB RID: 1723 RVA: 0x0000D1F7 File Offset: 0x0000B3F7
		[DoNotSerialize]
		[PortLabel("xⁿ")]
		public ValueOutput power { get; private set; }

		// Token: 0x060006BC RID: 1724 RVA: 0x0000D200 File Offset: 0x0000B400
		protected override void Definition()
		{
			this.@base = base.ValueInput<float>("base", 1f);
			this.exponent = base.ValueInput<float>("exponent", 2f);
			this.power = base.ValueOutput<float>("power", new Func<Flow, float>(this.Exponentiate));
			base.Requirement(this.@base, this.power);
			base.Requirement(this.exponent, this.power);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0000D27A File Offset: 0x0000B47A
		public float Exponentiate(Flow flow)
		{
			return Mathf.Pow(flow.GetValue<float>(this.@base), flow.GetValue<float>(this.exponent));
		}
	}
}
