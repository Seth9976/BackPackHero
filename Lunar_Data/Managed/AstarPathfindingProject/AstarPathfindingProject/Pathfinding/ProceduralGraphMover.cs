using System;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000162 RID: 354
	[AddComponentMenu("Pathfinding/Procedural Graph Mover")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/proceduralgraphmover.html")]
	public class ProceduralGraphMover : VersionedMonoBehaviour
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0003ADDA File Offset: 0x00038FDA
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0003ADE2 File Offset: 0x00038FE2
		public bool updatingGraph { get; private set; }

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003ADEC File Offset: 0x00038FEC
		private void Start()
		{
			if (AstarPath.active == null)
			{
				throw new Exception("There is no AstarPath object in the scene");
			}
			if (this.graph == null)
			{
				if (this.graphIndex < 0)
				{
					throw new Exception("Graph index should not be negative");
				}
				if (this.graphIndex >= AstarPath.active.data.graphs.Length)
				{
					throw new Exception(string.Concat(new string[]
					{
						"The ProceduralGraphMover was configured to use graph index ",
						this.graphIndex.ToString(),
						", but only ",
						AstarPath.active.data.graphs.Length.ToString(),
						" graphs exist"
					}));
				}
				this.graph = AstarPath.active.data.graphs[this.graphIndex];
				if (!(this.graph is GridGraph) && !(this.graph is RecastGraph))
				{
					throw new Exception("The ProceduralGraphMover was configured to use graph index " + this.graphIndex.ToString() + " but that graph either does not exist or is not a GridGraph, LayerGridGraph or RecastGraph");
				}
				RecastGraph recastGraph = this.graph as RecastGraph;
				if (recastGraph != null && !recastGraph.useTiles)
				{
					Debug.LogWarning("The ProceduralGraphMover component only works with tiled recast graphs. Enable tiling in the recast graph inspector.", this);
				}
			}
			this.UpdateGraph(true);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0003AF1B File Offset: 0x0003911B
		private void OnDisable()
		{
			if (AstarPath.active != null)
			{
				AstarPath.active.FlushWorkItems();
			}
			this.updatingGraph = false;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0003AF3C File Offset: 0x0003913C
		private void Update()
		{
			if (AstarPath.active == null || this.graph == null || !this.graph.isScanned)
			{
				return;
			}
			GridGraph gridGraph = this.graph as GridGraph;
			if (gridGraph != null)
			{
				Vector3 vector = gridGraph.transform.InverseTransform(gridGraph.center);
				Vector3 vector2 = gridGraph.transform.InverseTransform(this.target.position);
				if (VectorMath.SqrDistanceXZ(vector, vector2) > this.updateDistance * this.updateDistance)
				{
					this.UpdateGraph(true);
					return;
				}
				return;
			}
			else
			{
				if (this.graph is RecastGraph)
				{
					this.UpdateGraph(true);
					return;
				}
				throw new Exception("ProceduralGraphMover cannot be used with graphs of type " + this.graph.GetType().Name);
			}
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0003AFF8 File Offset: 0x000391F8
		public void UpdateGraph(bool async = true)
		{
			if (!base.enabled)
			{
				throw new InvalidOperationException("This component has been disabled");
			}
			if (this.updatingGraph)
			{
				return;
			}
			GridGraph gridGraph = this.graph as GridGraph;
			if (gridGraph != null)
			{
				this.UpdateGridGraph(gridGraph, async);
				return;
			}
			RecastGraph recastGraph = this.graph as RecastGraph;
			if (recastGraph != null)
			{
				Int2 @int = ProceduralGraphMover.RecastGraphTileShift(recastGraph, this.target.position);
				if (@int.x != 0 || @int.y != 0)
				{
					this.updatingGraph = true;
					this.UpdateRecastGraph(recastGraph, @int, async);
				}
			}
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0003B07C File Offset: 0x0003927C
		private void UpdateGridGraph(GridGraph graph, bool async)
		{
			this.updatingGraph = true;
			List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>> promises = new List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>>();
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext ctx)
			{
				Vector3 vector = graph.transform.InverseTransformVector(this.target.position - graph.center);
				int num = Mathf.RoundToInt(vector.x);
				int num2 = Mathf.RoundToInt(vector.z);
				if (num != 0 || num2 != 0)
				{
					IGraphUpdatePromise graphUpdatePromise = graph.TranslateInDirection(num, num2);
					promises.Add(new ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>(graphUpdatePromise, graphUpdatePromise.Prepare()));
				}
			}, delegate(IWorkItemContext ctx, bool force)
			{
				if (GraphUpdateProcessor.ProcessGraphUpdatePromises(promises, ctx, force))
				{
					this.updatingGraph = false;
					return true;
				}
				return false;
			}));
			if (!async)
			{
				AstarPath.active.FlushWorkItems();
			}
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x0003B0E4 File Offset: 0x000392E4
		private static Int2 RecastGraphTileShift(RecastGraph graph, Vector3 targetCenter)
		{
			Vector3 vector = graph.transform.InverseTransform(targetCenter) - graph.transform.InverseTransform(graph.forcedBoundsCenter);
			if (Mathf.Abs(vector.x) > Mathf.Abs(vector.z))
			{
				vector.z = 0f;
			}
			else
			{
				vector.x = 0f;
			}
			return new Int2((int)(Mathf.Max(0f, Mathf.Abs(vector.x) / graph.TileWorldSizeX + 0.5f - 0.2f) * Mathf.Sign(vector.x)), (int)(Mathf.Max(0f, Mathf.Abs(vector.z) / graph.TileWorldSizeZ + 0.5f - 0.2f) * Mathf.Sign(vector.z)));
		}

		// Token: 0x06000A55 RID: 2645 RVA: 0x0003B1B8 File Offset: 0x000393B8
		private void UpdateRecastGraph(RecastGraph graph, Int2 delta, bool async)
		{
			this.updatingGraph = true;
			List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>> promises = new List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>>();
			AstarPath.active.AddWorkItem(new AstarWorkItem(delegate(IWorkItemContext ctx)
			{
				IGraphUpdatePromise graphUpdatePromise = graph.TranslateInDirection(delta.x, delta.y);
				promises.Add(new ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>(graphUpdatePromise, graphUpdatePromise.Prepare()));
			}, delegate(IWorkItemContext ctx, bool force)
			{
				if (GraphUpdateProcessor.ProcessGraphUpdatePromises(promises, ctx, force))
				{
					this.updatingGraph = false;
					return true;
				}
				return false;
			}));
			if (!async)
			{
				AstarPath.active.FlushWorkItems();
			}
		}

		// Token: 0x040006F3 RID: 1779
		public float updateDistance = 10f;

		// Token: 0x040006F4 RID: 1780
		public Transform target;

		// Token: 0x040006F6 RID: 1782
		public NavGraph graph;

		// Token: 0x040006F7 RID: 1783
		[HideInInspector]
		public int graphIndex;
	}
}
