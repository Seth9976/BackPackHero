using System;
using System.Collections.Generic;

namespace Pathfinding.Util
{
	// Token: 0x0200025B RID: 603
	public struct GraphSnapshot : IGraphSnapshot, IDisposable
	{
		// Token: 0x06000E44 RID: 3652 RVA: 0x00058EB6 File Offset: 0x000570B6
		internal GraphSnapshot(List<IGraphSnapshot> inner)
		{
			this.inner = inner;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x00058EC0 File Offset: 0x000570C0
		public void Restore(IGraphUpdateContext ctx)
		{
			for (int i = 0; i < this.inner.Count; i++)
			{
				this.inner[i].Restore(ctx);
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x00058EF8 File Offset: 0x000570F8
		public void Dispose()
		{
			if (this.inner != null)
			{
				for (int i = 0; i < this.inner.Count; i++)
				{
					this.inner[i].Dispose();
				}
				this.inner = null;
			}
		}

		// Token: 0x04000AE4 RID: 2788
		private List<IGraphSnapshot> inner;
	}
}
