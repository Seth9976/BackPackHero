using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000081 RID: 129
	[AddComponentMenu("Pathfinding/Navmesh/RelevantGraphSurface")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_relevant_graph_surface.php")]
	public class RelevantGraphSurface : VersionedMonoBehaviour
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0002787F File Offset: 0x00025A7F
		public Vector3 Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x00027887 File Offset: 0x00025A87
		public RelevantGraphSurface Next
		{
			get
			{
				return this.next;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0002788F File Offset: 0x00025A8F
		public RelevantGraphSurface Prev
		{
			get
			{
				return this.prev;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x00027897 File Offset: 0x00025A97
		public static RelevantGraphSurface Root
		{
			get
			{
				return RelevantGraphSurface.root;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0002789E File Offset: 0x00025A9E
		public void UpdatePosition()
		{
			this.position = base.transform.position;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x000278B1 File Offset: 0x00025AB1
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

		// Token: 0x0600068F RID: 1679 RVA: 0x000278EC File Offset: 0x00025AEC
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

		// Token: 0x06000690 RID: 1680 RVA: 0x00027978 File Offset: 0x00025B78
		public static void UpdateAllPositions()
		{
			RelevantGraphSurface relevantGraphSurface = RelevantGraphSurface.root;
			while (relevantGraphSurface != null)
			{
				relevantGraphSurface.UpdatePosition();
				relevantGraphSurface = relevantGraphSurface.Next;
			}
		}

		// Token: 0x06000691 RID: 1681 RVA: 0x000279A4 File Offset: 0x00025BA4
		public static void FindAllGraphSurfaces()
		{
			RelevantGraphSurface[] array = Object.FindObjectsOfType(typeof(RelevantGraphSurface)) as RelevantGraphSurface[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i].OnDisable();
				array[i].OnEnable();
			}
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x000279E4 File Offset: 0x00025BE4
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(0.22352941f, 0.827451f, 0.18039216f, 0.4f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00027A54 File Offset: 0x00025C54
		public void OnDrawGizmosSelected()
		{
			Gizmos.color = new Color(0.22352941f, 0.827451f, 0.18039216f);
			Gizmos.DrawLine(base.transform.position - Vector3.up * this.maxRange, base.transform.position + Vector3.up * this.maxRange);
		}

		// Token: 0x040003D4 RID: 980
		private static RelevantGraphSurface root;

		// Token: 0x040003D5 RID: 981
		public float maxRange = 1f;

		// Token: 0x040003D6 RID: 982
		private RelevantGraphSurface prev;

		// Token: 0x040003D7 RID: 983
		private RelevantGraphSurface next;

		// Token: 0x040003D8 RID: 984
		private Vector3 position;
	}
}
