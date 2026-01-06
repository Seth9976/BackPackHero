using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000119 RID: 281
	[Serializable]
	public class StartEndModifier : PathModifier
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060008B9 RID: 2233 RVA: 0x00018013 File Offset: 0x00016213
		public override int Order
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0002F020 File Offset: 0x0002D220
		public override void Apply(Path _p)
		{
			ABPath abpath = _p as ABPath;
			if (abpath == null || abpath.vectorPath.Count == 0)
			{
				return;
			}
			bool flag = false;
			if (abpath.vectorPath.Count == 1 && !this.addPoints)
			{
				abpath.vectorPath.Add(abpath.vectorPath[0]);
				flag = true;
			}
			bool flag2;
			int num;
			Vector3 vector = this.Snap(abpath, this.exactStartPoint, true, out flag2, out num);
			bool flag3;
			int num2;
			Vector3 vector2 = this.Snap(abpath, this.exactEndPoint, false, out flag3, out num2);
			if (flag)
			{
				if (num == num2)
				{
					flag2 = false;
					flag3 = false;
				}
				else
				{
					flag2 = false;
				}
			}
			if ((flag2 || this.addPoints) && this.exactStartPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Insert(0, vector);
			}
			else
			{
				abpath.vectorPath[0] = vector;
			}
			if ((flag3 || this.addPoints) && this.exactEndPoint != StartEndModifier.Exactness.SnapToNode)
			{
				abpath.vectorPath.Add(vector2);
				return;
			}
			abpath.vectorPath[abpath.vectorPath.Count - 1] = vector2;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0002F11C File Offset: 0x0002D31C
		private Vector3 Snap(ABPath path, StartEndModifier.Exactness mode, bool start, out bool forceAddPoint, out int closestConnectionIndex)
		{
			int num = (start ? 0 : (path.path.Count - 1));
			GraphNode graphNode = path.path[num];
			Vector3 vector = (Vector3)graphNode.position;
			closestConnectionIndex = 0;
			forceAddPoint = false;
			switch (mode)
			{
			case StartEndModifier.Exactness.SnapToNode:
				return vector;
			case StartEndModifier.Exactness.Original:
			case StartEndModifier.Exactness.Interpolate:
			case StartEndModifier.Exactness.NodeConnection:
			{
				Vector3 vector2;
				if (start)
				{
					vector2 = ((this.adjustStartPoint != null) ? this.adjustStartPoint() : path.originalStartPoint);
				}
				else
				{
					vector2 = path.originalEndPoint;
				}
				switch (mode)
				{
				case StartEndModifier.Exactness.Original:
					return this.GetClampedPoint(vector, vector2, graphNode);
				case StartEndModifier.Exactness.Interpolate:
				{
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : (-1)), 0, path.path.Count - 1)];
					return VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode2.position, vector2);
				}
				case StartEndModifier.Exactness.NodeConnection:
				{
					this.connectionBuffer = this.connectionBuffer ?? new List<GraphNode>();
					Action<GraphNode> action;
					if ((action = this.connectionBufferAddDelegate) == null)
					{
						action = new Action<GraphNode>(this.connectionBuffer.Add);
					}
					this.connectionBufferAddDelegate = action;
					GraphNode graphNode2 = path.path[Mathf.Clamp(num + (start ? 1 : (-1)), 0, path.path.Count - 1)];
					graphNode.GetConnections(this.connectionBufferAddDelegate, 32);
					Vector3 vector3 = vector;
					float num2 = float.PositiveInfinity;
					for (int i = this.connectionBuffer.Count - 1; i >= 0; i--)
					{
						GraphNode graphNode3 = this.connectionBuffer[i];
						if (path.CanTraverse(graphNode3))
						{
							Vector3 vector4 = VectorMath.ClosestPointOnSegment(vector, (Vector3)graphNode3.position, vector2);
							float sqrMagnitude = (vector4 - vector2).sqrMagnitude;
							if (sqrMagnitude < num2)
							{
								vector3 = vector4;
								num2 = sqrMagnitude;
								closestConnectionIndex = i;
								forceAddPoint = graphNode3 != graphNode2;
							}
						}
					}
					this.connectionBuffer.Clear();
					return vector3;
				}
				}
				throw new ArgumentException("Cannot reach this point, but the compiler is not smart enough to realize that.");
			}
			case StartEndModifier.Exactness.ClosestOnNode:
				if (!start)
				{
					return path.endPoint;
				}
				return path.startPoint;
			default:
				throw new ArgumentException("Invalid mode");
			}
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002F334 File Offset: 0x0002D534
		protected Vector3 GetClampedPoint(Vector3 from, Vector3 to, GraphNode hint)
		{
			Vector3 vector = to;
			RaycastHit raycastHit;
			if (this.useRaycasting && Physics.Linecast(from, to, out raycastHit, this.mask))
			{
				vector = raycastHit.point;
			}
			if (this.useGraphRaycasting && hint != null)
			{
				IRaycastableGraph raycastableGraph = AstarData.GetGraph(hint) as IRaycastableGraph;
				GraphHitInfo graphHitInfo;
				if (raycastableGraph != null && raycastableGraph.Linecast(from, vector, out graphHitInfo, null, null))
				{
					vector = graphHitInfo.point;
				}
			}
			return vector;
		}

		// Token: 0x040005DD RID: 1501
		public bool addPoints;

		// Token: 0x040005DE RID: 1502
		public StartEndModifier.Exactness exactStartPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x040005DF RID: 1503
		public StartEndModifier.Exactness exactEndPoint = StartEndModifier.Exactness.ClosestOnNode;

		// Token: 0x040005E0 RID: 1504
		public Func<Vector3> adjustStartPoint;

		// Token: 0x040005E1 RID: 1505
		public bool useRaycasting;

		// Token: 0x040005E2 RID: 1506
		public LayerMask mask = -1;

		// Token: 0x040005E3 RID: 1507
		public bool useGraphRaycasting;

		// Token: 0x040005E4 RID: 1508
		private List<GraphNode> connectionBuffer;

		// Token: 0x040005E5 RID: 1509
		private Action<GraphNode> connectionBufferAddDelegate;

		// Token: 0x0200011A RID: 282
		public enum Exactness
		{
			// Token: 0x040005E7 RID: 1511
			SnapToNode,
			// Token: 0x040005E8 RID: 1512
			Original,
			// Token: 0x040005E9 RID: 1513
			Interpolate,
			// Token: 0x040005EA RID: 1514
			ClosestOnNode,
			// Token: 0x040005EB RID: 1515
			NodeConnection
		}
	}
}
