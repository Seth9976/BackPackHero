using System;

namespace Pathfinding
{
	// Token: 0x02000048 RID: 72
	public struct AstarWorkItem
	{
		// Token: 0x06000364 RID: 868 RVA: 0x000131D0 File Offset: 0x000113D0
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = null;
			this.update = update;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x000131EE File Offset: 0x000113EE
		public AstarWorkItem(Func<IWorkItemContext, bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = update;
			this.update = null;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001320C File Offset: 0x0001140C
		public AstarWorkItem(Action init, Func<bool, bool> update = null)
		{
			this.init = init;
			this.initWithContext = null;
			this.update = update;
			this.updateWithContext = null;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x0001322A File Offset: 0x0001142A
		public AstarWorkItem(Action<IWorkItemContext> init, Func<IWorkItemContext, bool, bool> update = null)
		{
			this.init = null;
			this.initWithContext = init;
			this.update = null;
			this.updateWithContext = update;
		}

		// Token: 0x04000224 RID: 548
		public Action init;

		// Token: 0x04000225 RID: 549
		public Action<IWorkItemContext> initWithContext;

		// Token: 0x04000226 RID: 550
		public Func<bool, bool> update;

		// Token: 0x04000227 RID: 551
		public Func<IWorkItemContext, bool, bool> updateWithContext;
	}
}
