using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000E9 RID: 233
	[UnitCategory("Math/Scalar")]
	[UnitTitle("Root")]
	[UnitOrder(106)]
	public sealed class ScalarRoot : Unit
	{
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x0000D373 File Offset: 0x0000B573
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x0000D37B File Offset: 0x0000B57B
		[DoNotSerialize]
		[PortLabel("x")]
		public ValueInput radicand { get; private set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x060006DA RID: 1754 RVA: 0x0000D384 File Offset: 0x0000B584
		// (set) Token: 0x060006DB RID: 1755 RVA: 0x0000D38C File Offset: 0x0000B58C
		[DoNotSerialize]
		[PortLabel("n")]
		public ValueInput degree { get; private set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000D395 File Offset: 0x0000B595
		// (set) Token: 0x060006DD RID: 1757 RVA: 0x0000D39D File Offset: 0x0000B59D
		[DoNotSerialize]
		[PortLabel("ⁿ√x")]
		public ValueOutput root { get; private set; }

		// Token: 0x060006DE RID: 1758 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		protected override void Definition()
		{
			this.radicand = base.ValueInput<float>("radicand", 1f);
			this.degree = base.ValueInput<float>("degree", 2f);
			this.root = base.ValueOutput<float>("root", new Func<Flow, float>(this.Root));
			base.Requirement(this.radicand, this.root);
			base.Requirement(this.degree, this.root);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000D424 File Offset: 0x0000B624
		public float Root(Flow flow)
		{
			float value = flow.GetValue<float>(this.degree);
			float value2 = flow.GetValue<float>(this.radicand);
			if (value == 2f)
			{
				return Mathf.Sqrt(value2);
			}
			return Mathf.Pow(value2, 1f / value);
		}
	}
}
