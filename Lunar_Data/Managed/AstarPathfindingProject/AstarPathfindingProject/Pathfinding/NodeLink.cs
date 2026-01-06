using System;
using Pathfinding.Drawing;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000078 RID: 120
	[AddComponentMenu("Pathfinding/Link")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/nodelink.html")]
	public class NodeLink : GraphModifier
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x000144ED File Offset: 0x000126ED
		public Transform Start
		{
			get
			{
				return base.transform;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x000144F5 File Offset: 0x000126F5
		public Transform End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x000144FD File Offset: 0x000126FD
		public override void OnGraphsPostUpdateBeforeAreaRecalculation()
		{
			this.Apply();
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00014508 File Offset: 0x00012708
		public static void DrawArch(Vector3 a, Vector3 b, Vector3 up, Color color)
		{
			Vector3 vector = b - a;
			if (vector == Vector3.zero)
			{
				return;
			}
			Vector3 vector2 = Vector3.Cross(up, vector);
			Vector3 vector3 = Vector3.Cross(vector, vector2).normalized * vector.magnitude * 0.1f;
			Draw.Bezier(a, a + vector3, b + vector3, b, color);
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014584 File Offset: 0x00012784
		public virtual void Apply()
		{
			if (this.Start == null || this.End == null || AstarPath.active == null)
			{
				return;
			}
			GraphNode node = AstarPath.active.GetNearest(this.Start.position).node;
			GraphNode node2 = AstarPath.active.GetNearest(this.End.position).node;
			if (node == null || node2 == null)
			{
				return;
			}
			if (this.deleteConnection)
			{
				GraphNode.Disconnect(node, node2);
				return;
			}
			uint num = (uint)Math.Round((double)((float)(node.position - node2.position).costMagnitude * this.costFactor));
			GraphNode.Connect(node, node2, num, this.oneWay ? OffMeshLinks.Directionality.OneWay : OffMeshLinks.Directionality.TwoWay);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014648 File Offset: 0x00012848
		public override void DrawGizmos()
		{
			if (this.Start == null || this.End == null)
			{
				return;
			}
			NodeLink.DrawArch(this.Start.position, this.End.position, Vector3.up, this.deleteConnection ? Color.red : Color.green);
		}

		// Token: 0x0400029A RID: 666
		public Transform end;

		// Token: 0x0400029B RID: 667
		public float costFactor = 1f;

		// Token: 0x0400029C RID: 668
		public bool oneWay;

		// Token: 0x0400029D RID: 669
		public bool deleteConnection;
	}
}
