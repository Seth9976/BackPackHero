using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000094 RID: 148
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_block_manager.php")]
	public class BlockManager : VersionedMonoBehaviour
	{
		// Token: 0x0600070B RID: 1803 RVA: 0x0002A913 File Offset: 0x00028B13
		private void Start()
		{
			if (!AstarPath.active)
			{
				throw new Exception("No AstarPath object in the scene");
			}
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0002A92C File Offset: 0x00028B2C
		public bool NodeContainsAnyOf(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0002A984 File Offset: 0x00028B84
		public bool NodeContainsAnyExcept(GraphNode node, List<SingleNodeBlocker> selector)
		{
			List<SingleNodeBlocker> list;
			if (!this.blocked.TryGetValue(node, out list))
			{
				return false;
			}
			for (int i = 0; i < list.Count; i++)
			{
				SingleNodeBlocker singleNodeBlocker = list[i];
				bool flag = false;
				for (int j = 0; j < selector.Count; j++)
				{
					if (singleNodeBlocker == selector[j])
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0002A9E8 File Offset: 0x00028BE8
		public void InternalBlock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate
			{
				List<SingleNodeBlocker> list;
				if (!this.blocked.TryGetValue(node, out list))
				{
					list = (this.blocked[node] = ListPool<SingleNodeBlocker>.Claim());
				}
				list.Add(blocker);
			}, null));
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0002AA2C File Offset: 0x00028C2C
		public void InternalUnblock(GraphNode node, SingleNodeBlocker blocker)
		{
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate
			{
				List<SingleNodeBlocker> list;
				if (this.blocked.TryGetValue(node, out list))
				{
					list.Remove(blocker);
					if (list.Count == 0)
					{
						this.blocked.Remove(node);
						ListPool<SingleNodeBlocker>.Release(ref list);
					}
				}
			}, null));
		}

		// Token: 0x0400040E RID: 1038
		private Dictionary<GraphNode, List<SingleNodeBlocker>> blocked = new Dictionary<GraphNode, List<SingleNodeBlocker>>();

		// Token: 0x02000140 RID: 320
		public enum BlockMode
		{
			// Token: 0x04000755 RID: 1877
			AllExceptSelector,
			// Token: 0x04000756 RID: 1878
			OnlySelector
		}

		// Token: 0x02000141 RID: 321
		public class TraversalProvider : ITraversalProvider
		{
			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06000AED RID: 2797 RVA: 0x0004498C File Offset: 0x00042B8C
			// (set) Token: 0x06000AEE RID: 2798 RVA: 0x00044994 File Offset: 0x00042B94
			public BlockManager.BlockMode mode { get; private set; }

			// Token: 0x06000AEF RID: 2799 RVA: 0x0004499D File Offset: 0x00042B9D
			public TraversalProvider(BlockManager blockManager, BlockManager.BlockMode mode, List<SingleNodeBlocker> selector)
			{
				if (blockManager == null)
				{
					throw new ArgumentNullException("blockManager");
				}
				if (selector == null)
				{
					throw new ArgumentNullException("selector");
				}
				this.blockManager = blockManager;
				this.mode = mode;
				this.selector = selector;
			}

			// Token: 0x06000AF0 RID: 2800 RVA: 0x000449DC File Offset: 0x00042BDC
			public bool CanTraverse(Path path, GraphNode node)
			{
				if (!node.Walkable || ((path.enabledTags >> (int)node.Tag) & 1) == 0)
				{
					return false;
				}
				if (this.mode == BlockManager.BlockMode.OnlySelector)
				{
					return !this.blockManager.NodeContainsAnyOf(node, this.selector);
				}
				return !this.blockManager.NodeContainsAnyExcept(node, this.selector);
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x00044A3B File Offset: 0x00042C3B
			public uint GetTraversalCost(Path path, GraphNode node)
			{
				return path.GetTagPenalty((int)node.Tag) + node.Penalty;
			}

			// Token: 0x04000757 RID: 1879
			private readonly BlockManager blockManager;

			// Token: 0x04000759 RID: 1881
			private readonly List<SingleNodeBlocker> selector;
		}
	}
}
