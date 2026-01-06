using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000095 RID: 149
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_single_node_blocker.php")]
	public class SingleNodeBlocker : VersionedMonoBehaviour
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0002AA83 File Offset: 0x00028C83
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x0002AA8B File Offset: 0x00028C8B
		public GraphNode lastBlocked { get; private set; }

		// Token: 0x06000713 RID: 1811 RVA: 0x0002AA94 File Offset: 0x00028C94
		public void BlockAtCurrentPosition()
		{
			this.BlockAt(base.transform.position);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0002AAA8 File Offset: 0x00028CA8
		public void BlockAt(Vector3 position)
		{
			this.Unblock();
			GraphNode node = AstarPath.active.GetNearest(position, NNConstraint.None).node;
			if (node != null)
			{
				this.Block(node);
			}
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0002AADB File Offset: 0x00028CDB
		public void Block(GraphNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			this.manager.InternalBlock(node, this);
			this.lastBlocked = node;
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0002AAFF File Offset: 0x00028CFF
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

		// Token: 0x04000410 RID: 1040
		public BlockManager manager;
	}
}
