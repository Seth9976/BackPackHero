using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000B9 RID: 185
	[UnitCategory("Logic")]
	[UnitTitle("Equality Comparison")]
	[UnitSurtitle("Equality")]
	[UnitShortTitle("Comparison")]
	[UnitOrder(4)]
	[Obsolete("Use the Comparison node instead.")]
	public sealed class EqualityComparison : Unit
	{
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000B85E File Offset: 0x00009A5E
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x0000B866 File Offset: 0x00009A66
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0000B86F File Offset: 0x00009A6F
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0000B877 File Offset: 0x00009A77
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000B880 File Offset: 0x00009A80
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0000B888 File Offset: 0x00009A88
		[DoNotSerialize]
		[PortLabel("A = B")]
		public ValueOutput equal { get; private set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000B891 File Offset: 0x00009A91
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x0000B899 File Offset: 0x00009A99
		[DoNotSerialize]
		[PortLabel("A ≠ B")]
		public ValueOutput notEqual { get; private set; }

		// Token: 0x06000596 RID: 1430 RVA: 0x0000B8A4 File Offset: 0x00009AA4
		protected override void Definition()
		{
			this.a = base.ValueInput<object>("a").AllowsNull();
			this.b = base.ValueInput<object>("b").AllowsNull();
			this.equal = base.ValueOutput<bool>("equal", new Func<Flow, bool>(this.Equal)).Predictable();
			this.notEqual = base.ValueOutput<bool>("notEqual", new Func<Flow, bool>(this.NotEqual)).Predictable();
			base.Requirement(this.a, this.equal);
			base.Requirement(this.b, this.equal);
			base.Requirement(this.a, this.notEqual);
			base.Requirement(this.b, this.notEqual);
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0000B969 File Offset: 0x00009B69
		private bool Equal(Flow flow)
		{
			return OperatorUtility.Equal(flow.GetValue(this.a), flow.GetValue(this.b));
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0000B988 File Offset: 0x00009B88
		private bool NotEqual(Flow flow)
		{
			return OperatorUtility.NotEqual(flow.GetValue(this.a), flow.GetValue(this.b));
		}
	}
}
