using System;

namespace Pathfinding
{
	// Token: 0x02000089 RID: 137
	public struct AstarWorkItem
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x00016878 File Offset: 0x00014A78
		public AstarWorkItem(Func<bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = null;
			this.update = update;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00016896 File Offset: 0x00014A96
		public AstarWorkItem(Func<IWorkItemContext, bool, bool> update)
		{
			this.init = null;
			this.initWithContext = null;
			this.updateWithContext = update;
			this.update = null;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000168B4 File Offset: 0x00014AB4
		public AstarWorkItem(Action init, Func<bool, bool> update = null)
		{
			this.init = init;
			this.initWithContext = null;
			this.update = update;
			this.updateWithContext = null;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000168D2 File Offset: 0x00014AD2
		public AstarWorkItem(Action<IWorkItemContext> init, Func<IWorkItemContext, bool, bool> update = null)
		{
			this.init = null;
			this.initWithContext = init;
			this.update = null;
			this.updateWithContext = update;
		}

		// Token: 0x040002EC RID: 748
		public Action init;

		// Token: 0x040002ED RID: 749
		public Action<IWorkItemContext> initWithContext;

		// Token: 0x040002EE RID: 750
		public Func<bool, bool> update;

		// Token: 0x040002EF RID: 751
		public Func<IWorkItemContext, bool, bool> updateWithContext;
	}
}
