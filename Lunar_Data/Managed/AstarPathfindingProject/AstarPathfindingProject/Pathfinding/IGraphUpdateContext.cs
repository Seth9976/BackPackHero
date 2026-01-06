using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009B RID: 155
	public interface IGraphUpdateContext
	{
		// Token: 0x060004E8 RID: 1256
		void DirtyBounds(Bounds bounds);
	}
}
