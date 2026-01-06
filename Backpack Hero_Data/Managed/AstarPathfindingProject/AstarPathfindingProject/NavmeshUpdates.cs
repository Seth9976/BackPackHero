using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000080 RID: 128
	[Serializable]
	public class NavmeshUpdates
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x000274F4 File Offset: 0x000256F4
		internal void OnEnable()
		{
			NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00027513 File Offset: 0x00025713
		internal void OnDisable()
		{
			NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00027534 File Offset: 0x00025734
		public void DiscardPending()
		{
			for (int i = 0; i < NavmeshClipper.allEnabled.Count; i++)
			{
				NavmeshClipper.allEnabled[i].NotifyUpdated();
			}
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int j = 0; j < graphs.Length; j++)
			{
				NavmeshBase navmeshBase = graphs[j] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.forcedReloadRects.Clear();
				}
			}
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x0002759C File Offset: 0x0002579C
		private void HandleOnEnableCallback(NavmeshClipper obj)
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.AddClipper(obj);
				}
			}
			obj.ForceUpdate();
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000275E0 File Offset: 0x000257E0
		private void HandleOnDisableCallback(NavmeshClipper obj)
		{
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.RemoveClipper(obj);
				}
			}
			this.lastUpdateTime = float.NegativeInfinity;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0002762C File Offset: 0x0002582C
		internal void Update()
		{
			if (AstarPath.active.isScanning)
			{
				return;
			}
			bool flag = false;
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.Refresh(false);
					flag = navmeshBase.navmeshUpdateData.forcedReloadRects.Count > 0;
				}
			}
			if ((this.updateInterval >= 0f && Time.realtimeSinceStartup - this.lastUpdateTime > this.updateInterval) || flag)
			{
				this.ForceUpdate();
			}
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x000276BC File Offset: 0x000258BC
		public void ForceUpdate()
		{
			this.lastUpdateTime = Time.realtimeSinceStartup;
			List<NavmeshClipper> list = null;
			NavGraph[] graphs = AstarPath.active.graphs;
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.Refresh(false);
					TileHandler handler = navmeshBase.navmeshUpdateData.handler;
					if (handler != null)
					{
						List<IntRect> forcedReloadRects = navmeshBase.navmeshUpdateData.forcedReloadRects;
						GridLookup<NavmeshClipper>.Root allItems = handler.cuts.AllItems;
						if (forcedReloadRects.Count == 0)
						{
							bool flag = false;
							for (GridLookup<NavmeshClipper>.Root root = allItems; root != null; root = root.next)
							{
								if (root.obj.RequiresUpdate())
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								goto IL_016A;
							}
						}
						handler.StartBatchLoad();
						for (int j = 0; j < forcedReloadRects.Count; j++)
						{
							handler.ReloadInBounds(forcedReloadRects[j]);
						}
						forcedReloadRects.ClearFast<IntRect>();
						if (list == null)
						{
							list = ListPool<NavmeshClipper>.Claim();
						}
						for (GridLookup<NavmeshClipper>.Root root2 = allItems; root2 != null; root2 = root2.next)
						{
							if (root2.obj.RequiresUpdate())
							{
								handler.ReloadInBounds(root2.previousBounds);
								Rect bounds = root2.obj.GetBounds(handler.graph.transform);
								IntRect touchingTilesInGraphSpace = handler.graph.GetTouchingTilesInGraphSpace(bounds);
								handler.cuts.Move(root2.obj, touchingTilesInGraphSpace);
								handler.ReloadInBounds(touchingTilesInGraphSpace);
								list.Add(root2.obj);
							}
						}
						handler.EndBatchLoad();
					}
				}
				IL_016A:;
			}
			if (list != null)
			{
				for (int k = 0; k < list.Count; k++)
				{
					list[k].NotifyUpdated();
				}
				ListPool<NavmeshClipper>.Release(ref list);
			}
		}

		// Token: 0x040003D2 RID: 978
		public float updateInterval;

		// Token: 0x040003D3 RID: 979
		private float lastUpdateTime = float.NegativeInfinity;

		// Token: 0x0200013E RID: 318
		internal class NavmeshUpdateSettings
		{
			// Token: 0x06000AE8 RID: 2792 RVA: 0x00044779 File Offset: 0x00042979
			public NavmeshUpdateSettings(NavmeshBase graph)
			{
				this.graph = graph;
			}

			// Token: 0x06000AE9 RID: 2793 RVA: 0x00044794 File Offset: 0x00042994
			public void Refresh(bool forceCreate = false)
			{
				if (!this.graph.enableNavmeshCutting)
				{
					if (this.handler != null)
					{
						this.handler.cuts.Clear();
						this.handler.ReloadInBounds(new IntRect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue));
						AstarPath.active.FlushGraphUpdates();
						AstarPath.active.FlushWorkItems();
						this.forcedReloadRects.ClearFast<IntRect>();
						this.handler = null;
						return;
					}
				}
				else if ((this.handler == null && (forceCreate || NavmeshClipper.allEnabled.Count > 0)) || (this.handler != null && !this.handler.isValid))
				{
					this.handler = new TileHandler(this.graph);
					for (int i = 0; i < NavmeshClipper.allEnabled.Count; i++)
					{
						this.AddClipper(NavmeshClipper.allEnabled[i]);
					}
					this.handler.CreateTileTypesFromGraph();
					this.forcedReloadRects.Add(new IntRect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue));
				}
			}

			// Token: 0x06000AEA RID: 2794 RVA: 0x000448A7 File Offset: 0x00042AA7
			public void OnRecalculatedTiles(NavmeshTile[] tiles)
			{
				this.Refresh(false);
				if (this.handler != null)
				{
					this.handler.OnRecalculatedTiles(tiles);
				}
			}

			// Token: 0x06000AEB RID: 2795 RVA: 0x000448C4 File Offset: 0x00042AC4
			public void AddClipper(NavmeshClipper obj)
			{
				if (!obj.graphMask.Contains((int)this.graph.graphIndex))
				{
					return;
				}
				this.Refresh(true);
				if (this.handler == null)
				{
					return;
				}
				Rect bounds = obj.GetBounds(this.handler.graph.transform);
				IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
				this.handler.cuts.Add(obj, touchingTilesInGraphSpace);
			}

			// Token: 0x06000AEC RID: 2796 RVA: 0x00044938 File Offset: 0x00042B38
			public void RemoveClipper(NavmeshClipper obj)
			{
				this.Refresh(false);
				if (this.handler == null)
				{
					return;
				}
				GridLookup<NavmeshClipper>.Root root = this.handler.cuts.GetRoot(obj);
				if (root != null)
				{
					this.forcedReloadRects.Add(root.previousBounds);
					this.handler.cuts.Remove(obj);
				}
			}

			// Token: 0x0400074A RID: 1866
			public TileHandler handler;

			// Token: 0x0400074B RID: 1867
			public readonly List<IntRect> forcedReloadRects = new List<IntRect>();

			// Token: 0x0400074C RID: 1868
			private readonly NavmeshBase graph;
		}
	}
}
