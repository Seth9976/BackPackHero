using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000034 RID: 52
	[UnitTitle("Break Loop")]
	[UnitCategory("Control")]
	[UnitOrder(13)]
	public class Break : Unit
	{
		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000206 RID: 518 RVA: 0x000066AC File Offset: 0x000048AC
		// (set) Token: 0x06000207 RID: 519 RVA: 0x000066B4 File Offset: 0x000048B4
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x06000208 RID: 520 RVA: 0x000066BD File Offset: 0x000048BD
		protected override void Definition()
		{
			this.enter = base.ControlInput("enter", new Func<Flow, ControlOutput>(this.Operation));
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000066DC File Offset: 0x000048DC
		public ControlOutput Operation(Flow flow)
		{
			flow.BreakLoop();
			return null;
		}
	}
}
