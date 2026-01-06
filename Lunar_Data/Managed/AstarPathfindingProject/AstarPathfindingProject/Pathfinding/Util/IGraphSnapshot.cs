using System;

namespace Pathfinding.Util
{
	// Token: 0x0200025A RID: 602
	public interface IGraphSnapshot : IDisposable
	{
		// Token: 0x06000E43 RID: 3651
		void Restore(IGraphUpdateContext ctx);
	}
}
