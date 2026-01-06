using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x0200003B RID: 59
	public abstract class LoopUnit : Unit
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00006DFC File Offset: 0x00004FFC
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00006E04 File Offset: 0x00005004
		[DoNotSerialize]
		[PortLabelHidden]
		public ControlInput enter { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00006E0D File Offset: 0x0000500D
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00006E15 File Offset: 0x00005015
		[DoNotSerialize]
		public ControlOutput exit { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00006E1E File Offset: 0x0000501E
		// (set) Token: 0x06000249 RID: 585 RVA: 0x00006E26 File Offset: 0x00005026
		[DoNotSerialize]
		public ControlOutput body { get; private set; }

		// Token: 0x0600024A RID: 586 RVA: 0x00006E30 File Offset: 0x00005030
		protected override void Definition()
		{
			this.enter = base.ControlInputCoroutine("enter", new Func<Flow, ControlOutput>(this.Loop), new Func<Flow, IEnumerator>(this.LoopCoroutine));
			this.exit = base.ControlOutput("exit");
			this.body = base.ControlOutput("body");
			base.Succession(this.enter, this.body);
			base.Succession(this.enter, this.exit);
		}

		// Token: 0x0600024B RID: 587
		protected abstract ControlOutput Loop(Flow flow);

		// Token: 0x0600024C RID: 588
		protected abstract IEnumerator LoopCoroutine(Flow flow);
	}
}
