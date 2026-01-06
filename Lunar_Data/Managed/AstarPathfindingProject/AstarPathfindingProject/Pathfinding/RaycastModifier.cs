using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000114 RID: 276
	[AddComponentMenu("Pathfinding/Modifiers/Raycast Modifier")]
	[RequireComponent(typeof(Seeker))]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/raycastmodifier.html")]
	[Serializable]
	public class RaycastModifier : MonoModifier
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060008A8 RID: 2216 RVA: 0x0002C0EE File Offset: 0x0002A2EE
		public override int Order
		{
			get
			{
				return 40;
			}
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x0002DF14 File Offset: 0x0002C114
		public override void Apply(Path p)
		{
			if (!this.useRaycasting && !this.useGraphRaycasting)
			{
				return;
			}
			List<Vector3> list = p.vectorPath;
			this.cachedFilter.path = p;
			this.cachedNNConstraint.graphMask = p.nnConstraint.graphMask;
			if (this.ValidateLine(null, null, p.vectorPath[0], p.vectorPath[p.vectorPath.Count - 1], this.cachedFilter.cachedDelegate, this.cachedNNConstraint))
			{
				Vector3 vector = p.vectorPath[0];
				Vector3 vector2 = p.vectorPath[p.vectorPath.Count - 1];
				list.ClearFast<Vector3>();
				list.Add(vector);
				list.Add(vector2);
			}
			else
			{
				int num = RaycastModifier.iterationsByQuality[(int)this.quality];
				for (int i = 0; i < num; i++)
				{
					if (i != 0)
					{
						Polygon.Subdivide(list, RaycastModifier.buffer, 3);
						Memory.Swap<List<Vector3>>(ref RaycastModifier.buffer, ref list);
						RaycastModifier.buffer.ClearFast<Vector3>();
						list.Reverse();
					}
					list = ((this.quality >= RaycastModifier.Quality.High) ? this.ApplyDP(p, list, this.cachedFilter.cachedDelegate, this.cachedNNConstraint) : this.ApplyGreedy(p, list, this.cachedFilter.cachedDelegate, this.cachedNNConstraint));
				}
				if (num % 2 == 0)
				{
					list.Reverse();
				}
			}
			p.vectorPath = list;
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x0002E074 File Offset: 0x0002C274
		private List<Vector3> ApplyGreedy(Path p, List<Vector3> points, Func<GraphNode, bool> filter, NNConstraint nnConstraint)
		{
			bool flag = points.Count == p.path.Count;
			int i = 0;
			while (i < points.Count)
			{
				Vector3 vector = points[i];
				GraphNode graphNode = ((flag && points[i] == (Vector3)p.path[i].position) ? p.path[i] : null);
				RaycastModifier.buffer.Add(vector);
				int num = 1;
				int num2 = 2;
				for (;;)
				{
					int num3 = i + num2;
					if (num3 >= points.Count)
					{
						goto Block_2;
					}
					Vector3 vector2 = points[num3];
					GraphNode graphNode2 = ((flag && vector2 == (Vector3)p.path[num3].position) ? p.path[num3] : null);
					if (!this.ValidateLine(graphNode, graphNode2, vector, vector2, filter, nnConstraint))
					{
						break;
					}
					num = num2;
					num2 *= 2;
				}
				IL_014F:
				while (num + 1 < num2)
				{
					int num4 = (num + num2) / 2;
					int num5 = i + num4;
					Vector3 vector3 = points[num5];
					GraphNode graphNode3 = ((flag && vector3 == (Vector3)p.path[num5].position) ? p.path[num5] : null);
					if (this.ValidateLine(graphNode, graphNode3, vector, vector3, filter, nnConstraint))
					{
						num = num4;
					}
					else
					{
						num2 = num4;
					}
				}
				i += num;
				continue;
				Block_2:
				num2 = points.Count - i;
				goto IL_014F;
			}
			Memory.Swap<List<Vector3>>(ref RaycastModifier.buffer, ref points);
			RaycastModifier.buffer.ClearFast<Vector3>();
			return points;
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0002E200 File Offset: 0x0002C400
		private List<Vector3> ApplyDP(Path p, List<Vector3> points, Func<GraphNode, bool> filter, NNConstraint nnConstraint)
		{
			if (RaycastModifier.DPCosts.Length < points.Count)
			{
				RaycastModifier.DPCosts = new float[points.Count];
				RaycastModifier.DPParents = new int[points.Count];
			}
			for (int i = 0; i < RaycastModifier.DPParents.Length; i++)
			{
				RaycastModifier.DPCosts[i] = (float)(RaycastModifier.DPParents[i] = -1);
			}
			bool flag = points.Count == p.path.Count;
			for (int j = 0; j < points.Count; j++)
			{
				float num = RaycastModifier.DPCosts[j];
				Vector3 vector = points[j];
				bool flag2 = flag && vector == (Vector3)p.path[j].position;
				for (int k = j + 1; k < points.Count; k++)
				{
					float num2 = num + (points[k] - vector).magnitude + 0.0001f;
					if (RaycastModifier.DPParents[k] == -1 || num2 < RaycastModifier.DPCosts[k])
					{
						bool flag3 = flag && points[k] == (Vector3)p.path[k].position;
						if (k != j + 1 && !this.ValidateLine(flag2 ? p.path[j] : null, flag3 ? p.path[k] : null, vector, points[k], filter, nnConstraint))
						{
							break;
						}
						RaycastModifier.DPCosts[k] = num2;
						RaycastModifier.DPParents[k] = j;
					}
				}
			}
			for (int num3 = points.Count - 1; num3 != -1; num3 = RaycastModifier.DPParents[num3])
			{
				RaycastModifier.buffer.Add(points[num3]);
			}
			RaycastModifier.buffer.Reverse();
			Memory.Swap<List<Vector3>>(ref RaycastModifier.buffer, ref points);
			RaycastModifier.buffer.ClearFast<Vector3>();
			return points;
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0002E3F4 File Offset: 0x0002C5F4
		protected bool ValidateLine(GraphNode n1, GraphNode n2, Vector3 v1, Vector3 v2, Func<GraphNode, bool> filter, NNConstraint nnConstraint)
		{
			if (this.useRaycasting)
			{
				if (this.use2DPhysics)
				{
					if (this.thickRaycast && this.thickRaycastRadius > 0f && Physics2D.CircleCast(v1 + this.raycastOffset, this.thickRaycastRadius, v2 - v1, (v2 - v1).magnitude, this.mask))
					{
						return false;
					}
					if (Physics2D.Linecast(v1 + this.raycastOffset, v2 + this.raycastOffset, this.mask))
					{
						return false;
					}
				}
				else
				{
					if (Physics.Linecast(v1 + this.raycastOffset, v2 + this.raycastOffset, this.mask))
					{
						return false;
					}
					if (this.thickRaycast && this.thickRaycastRadius > 0f)
					{
						if (Physics.CheckSphere(v1 + this.raycastOffset + (v2 - v1).normalized * this.thickRaycastRadius, this.thickRaycastRadius, this.mask))
						{
							return false;
						}
						if (Physics.SphereCast(new Ray(v1 + this.raycastOffset, v2 - v1), this.thickRaycastRadius, (v2 - v1).magnitude, this.mask))
						{
							return false;
						}
					}
				}
			}
			if (this.useGraphRaycasting)
			{
				bool flag = n1 != null && n2 != null;
				if (n1 == null)
				{
					n1 = AstarPath.active.GetNearest(v1, nnConstraint).node;
				}
				if (n2 == null)
				{
					n2 = AstarPath.active.GetNearest(v2, nnConstraint).node;
				}
				if (n1 != null && n2 != null)
				{
					NavGraph graph = n1.Graph;
					NavGraph graph2 = n2.Graph;
					if (graph != graph2)
					{
						return false;
					}
					IRaycastableGraph raycastableGraph = graph as IRaycastableGraph;
					GridGraph gridGraph = graph as GridGraph;
					if (flag && gridGraph != null)
					{
						return !gridGraph.Linecast(n1 as GridNodeBase, n2 as GridNodeBase, filter);
					}
					if (raycastableGraph != null)
					{
						GraphHitInfo graphHitInfo;
						return !raycastableGraph.Linecast(v1, v2, out graphHitInfo, null, filter);
					}
				}
			}
			return true;
		}

		// Token: 0x040005BA RID: 1466
		public bool useRaycasting;

		// Token: 0x040005BB RID: 1467
		public LayerMask mask = -1;

		// Token: 0x040005BC RID: 1468
		[Tooltip("Checks around the line between two points, not just the exact line.\nMake sure the ground is either too far below or is not inside the mask since otherwise the raycast might always hit the ground.")]
		public bool thickRaycast;

		// Token: 0x040005BD RID: 1469
		[Tooltip("Distance from the ray which will be checked for colliders")]
		public float thickRaycastRadius;

		// Token: 0x040005BE RID: 1470
		[Tooltip("Check for intersections with 2D colliders instead of 3D colliders.")]
		public bool use2DPhysics;

		// Token: 0x040005BF RID: 1471
		[Tooltip("Offset from the original positions to perform the raycast.\nCan be useful to avoid the raycast intersecting the ground or similar things you do not want to it intersect")]
		public Vector3 raycastOffset = Vector3.zero;

		// Token: 0x040005C0 RID: 1472
		[Tooltip("Use raycasting on the graphs. Only currently works with GridGraph and NavmeshGraph and RecastGraph. This is a pro version feature.")]
		public bool useGraphRaycasting = true;

		// Token: 0x040005C1 RID: 1473
		[Tooltip("When using the high quality mode the script will try harder to find a shorter path. This is significantly slower than the greedy low quality approach.")]
		public RaycastModifier.Quality quality = RaycastModifier.Quality.Medium;

		// Token: 0x040005C2 RID: 1474
		private static readonly int[] iterationsByQuality = new int[] { 1, 2, 1, 3 };

		// Token: 0x040005C3 RID: 1475
		private static List<Vector3> buffer = new List<Vector3>();

		// Token: 0x040005C4 RID: 1476
		private static float[] DPCosts = new float[16];

		// Token: 0x040005C5 RID: 1477
		private static int[] DPParents = new int[16];

		// Token: 0x040005C6 RID: 1478
		private RaycastModifier.Filter cachedFilter = new RaycastModifier.Filter();

		// Token: 0x040005C7 RID: 1479
		private NNConstraint cachedNNConstraint = NNConstraint.None;

		// Token: 0x02000115 RID: 277
		public enum Quality
		{
			// Token: 0x040005C9 RID: 1481
			Low,
			// Token: 0x040005CA RID: 1482
			Medium,
			// Token: 0x040005CB RID: 1483
			High,
			// Token: 0x040005CC RID: 1484
			Highest
		}

		// Token: 0x02000116 RID: 278
		private class Filter
		{
			// Token: 0x060008AF RID: 2223 RVA: 0x0002E6B8 File Offset: 0x0002C8B8
			public Filter()
			{
				this.cachedDelegate = new Func<GraphNode, bool>(this.CanTraverse);
			}

			// Token: 0x060008B0 RID: 2224 RVA: 0x0002E6D2 File Offset: 0x0002C8D2
			private bool CanTraverse(GraphNode node)
			{
				return this.path.CanTraverse(node);
			}

			// Token: 0x040005CD RID: 1485
			public Path path;

			// Token: 0x040005CE RID: 1486
			public readonly Func<GraphNode, bool> cachedDelegate;
		}
	}
}
