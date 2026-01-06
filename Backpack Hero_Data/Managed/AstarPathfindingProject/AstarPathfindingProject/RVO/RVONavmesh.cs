using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.RVO
{
	// Token: 0x020000D7 RID: 215
	[AddComponentMenu("Pathfinding/Local Avoidance/RVO Navmesh")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_r_v_o_1_1_r_v_o_navmesh.php")]
	public class RVONavmesh : GraphModifier
	{
		// Token: 0x0600093B RID: 2363 RVA: 0x0003CDB1 File Offset: 0x0003AFB1
		public override void OnPostCacheLoad()
		{
			this.OnLatePostScan();
		}

		// Token: 0x0600093C RID: 2364 RVA: 0x0003CDB9 File Offset: 0x0003AFB9
		public override void OnGraphsPostUpdate()
		{
			this.OnLatePostScan();
		}

		// Token: 0x0600093D RID: 2365 RVA: 0x0003CDC4 File Offset: 0x0003AFC4
		public override void OnLatePostScan()
		{
			if (!Application.isPlaying)
			{
				return;
			}
			this.RemoveObstacles();
			NavGraph[] graphs = AstarPath.active.graphs;
			RVOSimulator active = RVOSimulator.active;
			if (active == null)
			{
				throw new NullReferenceException("No RVOSimulator could be found in the scene. Please add one to any GameObject");
			}
			this.lastSim = active.GetSimulator();
			for (int i = 0; i < graphs.Length; i++)
			{
				RecastGraph recastGraph = graphs[i] as RecastGraph;
				INavmesh navmesh = graphs[i] as INavmesh;
				GridGraph gridGraph = graphs[i] as GridGraph;
				if (recastGraph != null)
				{
					foreach (NavmeshTile navmeshTile in recastGraph.GetTiles())
					{
						this.AddGraphObstacles(this.lastSim, navmeshTile);
					}
				}
				else if (navmesh != null)
				{
					this.AddGraphObstacles(this.lastSim, navmesh);
				}
				else if (gridGraph != null)
				{
					this.AddGraphObstacles(this.lastSim, gridGraph);
				}
			}
		}

		// Token: 0x0600093E RID: 2366 RVA: 0x0003CE99 File Offset: 0x0003B099
		protected override void OnDisable()
		{
			base.OnDisable();
			this.RemoveObstacles();
		}

		// Token: 0x0600093F RID: 2367 RVA: 0x0003CEA8 File Offset: 0x0003B0A8
		public void RemoveObstacles()
		{
			if (this.lastSim != null)
			{
				for (int i = 0; i < this.obstacles.Count; i++)
				{
					this.lastSim.RemoveObstacle(this.obstacles[i]);
				}
				this.lastSim = null;
			}
			this.obstacles.Clear();
		}

		// Token: 0x06000940 RID: 2368 RVA: 0x0003CEFC File Offset: 0x0003B0FC
		private void AddGraphObstacles(Simulator sim, GridGraph grid)
		{
			bool reverse = Vector3.Dot(grid.transform.TransformVector(Vector3.up), (sim.movementPlane == MovementPlane.XY) ? Vector3.back : Vector3.up) > 0f;
			GraphUtilities.GetContours(grid, delegate(Vector3[] vertices)
			{
				if (reverse)
				{
					Array.Reverse<Vector3>(vertices);
				}
				this.obstacles.Add(sim.AddObstacle(vertices, this.wallHeight, true));
			}, this.wallHeight * 0.4f, null);
		}

		// Token: 0x06000941 RID: 2369 RVA: 0x0003CF78 File Offset: 0x0003B178
		private void AddGraphObstacles(Simulator simulator, INavmesh navmesh)
		{
			GraphUtilities.GetContours(navmesh, delegate(List<Int3> vertices, bool cycle)
			{
				Vector3[] array = new Vector3[vertices.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = (Vector3)vertices[i];
				}
				ListPool<Int3>.Release(vertices);
				this.obstacles.Add(simulator.AddObstacle(array, this.wallHeight, cycle));
			});
		}

		// Token: 0x0400056A RID: 1386
		public float wallHeight = 5f;

		// Token: 0x0400056B RID: 1387
		private readonly List<ObstacleVertex> obstacles = new List<ObstacleVertex>();

		// Token: 0x0400056C RID: 1388
		private Simulator lastSim;
	}
}
