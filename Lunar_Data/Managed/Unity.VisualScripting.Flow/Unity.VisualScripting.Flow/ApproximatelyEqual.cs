using System;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x020000B5 RID: 181
	[UnitCategory("Logic")]
	[UnitShortTitle("Equal")]
	[UnitSubtitle("(Approximately)")]
	[UnitOrder(7)]
	[Obsolete("Use the Equal node with Numeric enabled instead.")]
	public sealed class ApproximatelyEqual : Unit
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000B08B File Offset: 0x0000928B
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000B093 File Offset: 0x00009293
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000B09C File Offset: 0x0000929C
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000B0A4 File Offset: 0x000092A4
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000B0AD File Offset: 0x000092AD
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000B0B5 File Offset: 0x000092B5
		[DoNotSerialize]
		[PortLabel("A ≈ B")]
		public ValueOutput equal { get; private set; }

		// Token: 0x0600054B RID: 1355 RVA: 0x0000B0C0 File Offset: 0x000092C0
		protected override void Definition()
		{
			this.a = base.ValueInput<float>("a");
			this.b = base.ValueInput<float>("b", 0f);
			this.equal = base.ValueOutput<bool>("equal", new Func<Flow, bool>(this.Comparison)).Predictable();
			base.Requirement(this.a, this.equal);
			base.Requirement(this.b, this.equal);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x0000B13A File Offset: 0x0000933A
		public bool Comparison(Flow flow)
		{
			return Mathf.Approximately(flow.GetValue<float>(this.a), flow.GetValue<float>(this.b));
		}
	}
}
