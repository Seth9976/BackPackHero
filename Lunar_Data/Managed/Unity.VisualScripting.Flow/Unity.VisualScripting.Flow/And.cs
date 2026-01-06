using System;

namespace Unity.VisualScripting
{
	// Token: 0x020000B4 RID: 180
	[UnitCategory("Logic")]
	[UnitOrder(0)]
	public sealed class And : Unit
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000AFBB File Offset: 0x000091BB
		// (set) Token: 0x0600053D RID: 1341 RVA: 0x0000AFC3 File Offset: 0x000091C3
		[DoNotSerialize]
		public ValueInput a { get; private set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000AFCC File Offset: 0x000091CC
		// (set) Token: 0x0600053F RID: 1343 RVA: 0x0000AFD4 File Offset: 0x000091D4
		[DoNotSerialize]
		public ValueInput b { get; private set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000AFDD File Offset: 0x000091DD
		// (set) Token: 0x06000541 RID: 1345 RVA: 0x0000AFE5 File Offset: 0x000091E5
		[DoNotSerialize]
		[PortLabel("A & B")]
		public ValueOutput result { get; private set; }

		// Token: 0x06000542 RID: 1346 RVA: 0x0000AFF0 File Offset: 0x000091F0
		protected override void Definition()
		{
			this.a = base.ValueInput<bool>("a");
			this.b = base.ValueInput<bool>("b");
			this.result = base.ValueOutput<bool>("result", new Func<Flow, bool>(this.Operation)).Predictable();
			base.Requirement(this.a, this.result);
			base.Requirement(this.b, this.result);
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x0000B065 File Offset: 0x00009265
		public bool Operation(Flow flow)
		{
			return flow.GetValue<bool>(this.a) && flow.GetValue<bool>(this.b);
		}
	}
}
