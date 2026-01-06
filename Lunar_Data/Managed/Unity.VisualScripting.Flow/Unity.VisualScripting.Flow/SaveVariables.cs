using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000153 RID: 339
	[UnitCategory("Variables")]
	public sealed class SaveVariables : Unit
	{
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x000100BF File Offset: 0x0000E2BF
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x000100C7 File Offset: 0x0000E2C7
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x000100D0 File Offset: 0x0000E2D0
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x000100D8 File Offset: 0x0000E2D8
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x060008C6 RID: 2246 RVA: 0x000100E4 File Offset: 0x0000E2E4
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Enter));
			this.exit = base.ControlOutput("exit");
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00010131 File Offset: 0x0000E331
		private ControlOutput Enter(Flow arg)
		{
			SavedVariables.SaveDeclarations(SavedVariables.merged);
			return this.exit;
		}
	}
}
