using System;
using System.Collections.Generic;
using Pathfinding.Graphs.Util;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001CB RID: 459
	[Serializable]
	public class NavmeshUpdates
	{
		// Token: 0x06000BEF RID: 3055 RVA: 0x00046350 File Offset: 0x00044550
		internal void OnEnable()
		{
			NavmeshClipper.AddEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0004636F File Offset: 0x0004456F
		internal void OnDisable()
		{
			NavmeshClipper.RemoveEnableCallback(new Action<NavmeshClipper>(this.HandleOnEnableCallback), new Action<NavmeshClipper>(this.HandleOnDisableCallback));
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00046390 File Offset: 0x00044590
		public void ForceUpdateAround(NavmeshClipper clipper)
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.Dirty(clipper);
				}
			}
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000463D4 File Offset: 0x000445D4
		public void DiscardPending()
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.DiscardPending();
				}
			}
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00046418 File Offset: 0x00044618
		private void HandleOnEnableCallback(NavmeshClipper obj)
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
			for (int i = 0; i < graphs.Length; i++)
			{
				NavmeshBase navmeshBase = graphs[i] as NavmeshBase;
				if (navmeshBase != null)
				{
					navmeshBase.navmeshUpdateData.AddClipper(obj);
				}
			}
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0004645C File Offset: 0x0004465C
		private void HandleOnDisableCallback(NavmeshClipper obj)
		{
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
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

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000464AC File Offset: 0x000446AC
		internal void Update()
		{
			if (this.astar.isScanning)
			{
				return;
			}
			bool flag = false;
			NavGraph[] graphs = this.astar.graphs;
			if (graphs != null)
			{
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
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00046540 File Offset: 0x00044740
		public void ForceUpdate()
		{
			this.lastUpdateTime = Time.realtimeSinceStartup;
			NavGraph[] graphs = this.astar.graphs;
			if (graphs == null)
			{
				return;
			}
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
								if (root.obj.RequiresUpdate(root))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								goto IL_016D;
							}
						}
						handler.StartBatchLoad();
						for (int j = 0; j < forcedReloadRects.Count; j++)
						{
							handler.ReloadInBounds(forcedReloadRects[j]);
						}
						forcedReloadRects.ClearFast<IntRect>();
						float navmeshCuttingCharacterRadius = handler.graph.NavmeshCuttingCharacterRadius;
						for (GridLookup<NavmeshClipper>.Root root2 = allItems; root2 != null; root2 = root2.next)
						{
							if (root2.obj.RequiresUpdate(root2))
							{
								handler.ReloadInBounds(root2.previousBounds);
								Rect bounds = root2.obj.GetBounds(handler.graph.transform, navmeshCuttingCharacterRadius);
								IntRect touchingTilesInGraphSpace = handler.graph.GetTouchingTilesInGraphSpace(bounds);
								handler.cuts.Move(root2.obj, touchingTilesInGraphSpace);
								handler.ReloadInBounds(touchingTilesInGraphSpace);
								root2.obj.NotifyUpdated(root2);
							}
						}
						handler.EndBatchLoad();
					}
				}
				IL_016D:;
			}
		}

		// Token: 0x04000869 RID: 2153
		public float updateInterval;

		// Token: 0x0400086A RID: 2154
		internal AstarPath astar;

		// Token: 0x0400086B RID: 2155
		private float lastUpdateTime = float.NegativeInfinity;

		// Token: 0x020001CC RID: 460
		public class NavmeshUpdateSettings
		{
			// Token: 0x06000BF8 RID: 3064 RVA: 0x000466DA File Offset: 0x000448DA
			public NavmeshUpdateSettings(NavmeshBase graph)
			{
				this.graph = graph;
			}

			// Token: 0x06000BF9 RID: 3065 RVA: 0x000466F4 File Offset: 0x000448F4
			public void ReloadAllTiles()
			{
				if (this.handler != null)
				{
					this.handler.ReloadInBounds(new IntRect(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue));
				}
			}

			// Token: 0x06000BFA RID: 3066 RVA: 0x00046724 File Offset: 0x00044924
			public void Refresh(bool forceCreate = false)
			{
				if (!this.graph.enableNavmeshCutting)
				{
					if (this.handler != null)
					{
						this.handler.cuts.Clear();
						this.ReloadAllTiles();
						this.graph.active.FlushGraphUpdates();
						this.graph.active.FlushWorkItems();
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

			// Token: 0x06000BFB RID: 3067 RVA: 0x00046828 File Offset: 0x00044A28
			public void DiscardPending()
			{
				if (this.handler != null)
				{
					for (int i = 0; i < NavmeshClipper.allEnabled.Count; i++)
					{
						NavmeshClipper navmeshClipper = NavmeshClipper.allEnabled[i];
						GridLookup<NavmeshClipper>.Root root = this.handler.cuts.GetRoot(navmeshClipper);
						if (root != null)
						{
							navmeshClipper.NotifyUpdated(root);
						}
					}
				}
				this.forcedReloadRects.Clear();
			}

			// Token: 0x06000BFC RID: 3068 RVA: 0x00046888 File Offset: 0x00044A88
			public void OnResized(IntRect newTileBounds)
			{
				if (this.handler == null)
				{
					return;
				}
				this.handler.Resize(newTileBounds);
				float navmeshCuttingCharacterRadius = this.graph.NavmeshCuttingCharacterRadius;
				for (GridLookup<NavmeshClipper>.Root root = this.handler.cuts.AllItems; root != null; root = root.next)
				{
					Rect bounds = root.obj.GetBounds(this.handler.graph.transform, navmeshCuttingCharacterRadius);
					IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
					if (root.previousBounds != touchingTilesInGraphSpace)
					{
						this.handler.cuts.Dirty(root.obj);
						this.handler.cuts.Move(root.obj, touchingTilesInGraphSpace);
					}
				}
			}

			// Token: 0x06000BFD RID: 3069 RVA: 0x0004693D File Offset: 0x00044B3D
			public void OnRecalculatedTiles(NavmeshTile[] tiles)
			{
				this.Refresh(false);
				if (this.handler != null)
				{
					this.handler.OnRecalculatedTiles(tiles);
				}
				if (this.graph.GetTiles().Length == tiles.Length)
				{
					this.DiscardPending();
				}
			}

			// Token: 0x06000BFE RID: 3070 RVA: 0x00046972 File Offset: 0x00044B72
			public void Dirty(NavmeshClipper obj)
			{
				if (this.handler == null)
				{
					return;
				}
				this.handler.cuts.Dirty(obj);
			}

			// Token: 0x06000BFF RID: 3071 RVA: 0x00046990 File Offset: 0x00044B90
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
				float navmeshCuttingCharacterRadius = this.graph.NavmeshCuttingCharacterRadius;
				Rect bounds = obj.GetBounds(this.graph.transform, navmeshCuttingCharacterRadius);
				IntRect touchingTilesInGraphSpace = this.handler.graph.GetTouchingTilesInGraphSpace(bounds);
				this.handler.cuts.Add(obj, touchingTilesInGraphSpace);
			}

			// Token: 0x06000C00 RID: 3072 RVA: 0x00046A0C File Offset: 0x00044C0C
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

			// Token: 0x0400086C RID: 2156
			public TileHandler handler;

			// Token: 0x0400086D RID: 2157
			public readonly List<IntRect> forcedReloadRects = new List<IntRect>();

			// Token: 0x0400086E RID: 2158
			private readonly NavmeshBase graph;
		}
	}
}
