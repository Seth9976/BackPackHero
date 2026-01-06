using System;
using Pathfinding.Drawing;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x020001CD RID: 461
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/relevantgraphsurface.html")]
	public class RelevantGraphSurface : VersionedMonoBehaviour
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000C01 RID: 3073 RVA: 0x00046A60 File Offset: 0x00044C60
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00046A68 File Offset: 0x00044C68
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000C03 RID: 3075 RVA: 0x00046A70 File Offset: 0x00044C70
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000C04 RID: 3076 RVA: 0x00046A78 File Offset: 0x00044C78
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x00046A7F File Offset: 0x00044C7F
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00046A92 File Offset: 0x00044C92
		private void OnEnable()
		{
			this.UpdatePosition();
			if (RelevantGraphSurface.root == null)
			{
				RelevantGraphSurface.root = this;
				return;
			}
			this.next = RelevantGraphSurface.root;
			RelevantGraphSurface.root.prev = this;
			RelevantGraphSurface.root = this;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x00046ACC File Offset: 0x00044CCC
		private void OnDisable()
		{
			if (RelevantGraphSurface.root == this)
			{
				RelevantGraphSurface.root = this.next;
				if (RelevantGraphSurface.root != null)
				{
					RelevantGraphSurface.root.prev = null;
				}
			}
			else
			{
				if (this.prev != null)
				{
					this.prev.next = this.next;
				}
				if (this.next != null)
				{
					this.next.prev = this.prev;
				}
			}
			this.prev = null;
			this.next = null;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00046B58 File Offset: 0x00044D58
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x00046B84 File Offset: 0x00044D84
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = UnityCompatibility.FindObjectsByTypeUnsorted<RelevantGraphSurface>();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00046BB8 File Offset: 0x00044DB8
		public override void DrawGizmos()
		{
			Color color = new Color(0.22352941f, 0.827451f, 0.18039216f);
			if (!GizmoContext.InActiveSelection(this))
			{
				color.a *= 0.4f;
			}
			Draw.Line(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange, color);
		}

		// Token: 0x0400086F RID: 2159
		private static RelevantGraphSurface root;

		// Token: 0x04000870 RID: 2160
		public float maxRange = 1f;

		// Token: 0x04000871 RID: 2161
		private RelevantGraphSurface prev;

		// Token: 0x04000872 RID: 2162
		private RelevantGraphSurface next;

		// Token: 0x04000873 RID: 2163
		private Vector3 position;
	}
}
