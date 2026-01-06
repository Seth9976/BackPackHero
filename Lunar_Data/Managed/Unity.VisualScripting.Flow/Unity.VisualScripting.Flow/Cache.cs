using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000035 RID: 53
	[UnitCategory("Control")]
	[UnitOrder(15)]
	public sealed class Cache : Unit
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600020B RID: 523 RVA: 0x000066ED File Offset: 0x000048ED
		// (set) Token: 0x0600020C RID: 524 RVA: 0x000066F5 File Offset: 0x000048F5
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000066FE File Offset: 0x000048FE
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006706 File Offset: 0x00004906
		[DoNotSerialize]
		[PortLabelHidden]
		public ValueInput input { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000670F File Offset: 0x0000490F
		// (set) Token: 0x06000210 RID: 528 RVA: 0x00006717 File Offset: 0x00004917
		[DoNotSerialize]
		[PortLabel("Cached")]
		[PortLabelHidden]
		public ValueOutput output { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00006720 File Offset: 0x00004920
		// (set) Token: 0x06000212 RID: 530 RVA: 0x00006728 File Offset: 0x00004928
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x06000213 RID: 531 RVA: 0x00006734 File Offset: 0x00004934
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Store));
			this.input = base.ValueInput<object>("input");
			this.output = base.ValueOutput<object>("output");
			this.exit = base.ControlOutput("exit");
			base.Requirement(this.input, this.enter);
			base.Assignment(this.enter, this.output);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000067C7 File Offset: 0x000049C7
		private ControlOutput Store(Flow flow)
		{
			flow.SetValue(this.output, flow.GetValue(this.input));
			return this.exit;
		}
	}
}
