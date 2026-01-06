using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000134 RID: 308
	[UnitCategory("Time")]
	public abstract class WaitUnit : Unit
	{
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0000F529 File Offset: 0x0000D729
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0000F531 File Offset: 0x0000D731
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000F53A File Offset: 0x0000D73A
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0000F542 File Offset: 0x0000D742
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlOutput exit { get; private set; }

		// Token: 0x0600083D RID: 2109 RVA: 0x0000F54C File Offset: 0x0000D74C
		protected override void Definition()
		{
			this.enter = base.ControlInputCoroutine("enter", new Func<Flow, IEnumerator>(this.Await));
			this.exit = base.ControlOutput("exit");
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x0600083E RID: 2110
		protected abstract IEnumerator Await(Flow flow);
	}
}
