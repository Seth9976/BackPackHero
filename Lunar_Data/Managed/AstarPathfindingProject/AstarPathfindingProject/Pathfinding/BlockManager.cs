using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000137 RID: 311
	[HelpURL("https://arongranberg.com/astar/documentation/stable/blockmanager.html")]
	public class BlockManager : VersionedMonoBehaviour
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00033786 File Offset: 0x00031986
		private void Start()
		{
			if (!AstarPath.active)
			{
				throw new Exception("No AstarPath object in the scene");
			}
		}

		// Token: 0x0600095C RID: 2396 RVA: 0x000337A0 File Offset: 0x000319A0
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

		// Token: 0x0600095D RID: 2397 RVA: 0x000337F8 File Offset: 0x000319F8
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

		// Token: 0x0600095E RID: 2398 RVA: 0x0003385C File Offset: 0x00031A5C
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

		// Token: 0x0600095F RID: 2399 RVA: 0x000338A0 File Offset: 0x00031AA0
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

		// Token: 0x0400066C RID: 1644
		private Dictionary<GraphNode, List<SingleNodeBlocker>> blocked = new Dictionary<GraphNode, List<SingleNodeBlocker>>();

		// Token: 0x02000138 RID: 312
		public enum BlockMode
		{
			// Token: 0x0400066E RID: 1646
			AllExceptSelector,
			// Token: 0x0400066F RID: 1647
			OnlySelector
		}

		// Token: 0x02000139 RID: 313
		public class TraversalProvider : ITraversalProvider
		{
			// Token: 0x17000174 RID: 372
			// (get) Token: 0x06000961 RID: 2401 RVA: 0x000338F7 File Offset: 0x00031AF7
			// (set) Token: 0x06000962 RID: 2402 RVA: 0x000338FF File Offset: 0x00031AFF
			public BlockManager.BlockMode mode { get; private set; }

			// Token: 0x06000963 RID: 2403 RVA: 0x00033908 File Offset: 0x00031B08
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

			// Token: 0x06000964 RID: 2404 RVA: 0x00033948 File Offset: 0x00031B48
			public bool CanTraverse(Path path, GraphNode node)
			{
				if (!node.Walkable || (path != null && ((path.enabledTags >> (int)node.Tag) & 1) == 0))
				{
					return false;
				}
				if (this.mode == BlockManager.BlockMode.OnlySelector)
				{
					return !this.blockManager.NodeContainsAnyOf(node, this.selector);
				}
				return !this.blockManager.NodeContainsAnyExcept(node, this.selector);
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x000339AA File Offset: 0x00031BAA
			public bool CanTraverse(Path path, GraphNode from, GraphNode to)
			{
				return this.CanTraverse(path, to);
			}

			// Token: 0x06000966 RID: 2406 RVA: 0x000339B4 File Offset: 0x00031BB4
			public uint GetTraversalCost(Path path, GraphNode node)
			{
				return path.GetTagPenalty((int)node.Tag) + node.Penalty;
			}

			// Token: 0x17000175 RID: 373
			// (get) Token: 0x06000967 RID: 2407 RVA: 0x00016F22 File Offset: 0x00015122
			public bool filterDiagonalGridConnections
			{
				get
				{
					return true;
				}
			}

			// Token: 0x04000670 RID: 1648
			private readonly BlockManager blockManager;

			// Token: 0x04000672 RID: 1650
			private readonly List<SingleNodeBlocker> selector;
		}
	}
}
