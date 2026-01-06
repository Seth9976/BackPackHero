using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200013C RID: 316
	[HelpURL("https://arongranberg.com/astar/documentation/stable/singlenodeblocker.html")]
	public class SingleNodeBlocker : VersionedMonoBehaviour
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600096C RID: 2412 RVA: 0x00033A7A File Offset: 0x00031C7A
		// (set) Token: 0x0600096D RID: 2413 RVA: 0x00033A82 File Offset: 0x00031C82
		public GraphNode lastBlocked { get; private set; }

		// Token: 0x0600096E RID: 2414 RVA: 0x00033A8B File Offset: 0x00031C8B
		public void BlockAtCurrentPosition()
		{
			this.BlockAt(base.transform.position);
		}

		// Token: 0x0600096F RID: 2415 RVA: 0x00033AA0 File Offset: 0x00031CA0
		public void BlockAt(Vector3 position)
		{
			this.Unblock();
			GraphNode node = AstarPath.active.GetNearest(position, NNConstraint.None).node;
			if (node != null)
			{
				this.Block(node);
			}
		}

		// Token: 0x06000970 RID: 2416 RVA: 0x00033AD3 File Offset: 0x00031CD3
		public void Block(GraphNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.manager.InternalBlock(node, this);
			this.lastBlocked = node;
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x00033AF7 File Offset: 0x00031CF7
		public void Unblock()
		{
			if (this.lastBlocked == null || this.lastBlocked.Destroyed)
			{
				this.lastBlocked = null;
				return;
			}
			this.manager.InternalUnblock(this.lastBlocked, this);
			this.lastBlocked = null;
		}

		// Token: 0x0400067A RID: 1658
		public BlockManager manager;
	}
}
