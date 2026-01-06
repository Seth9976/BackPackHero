using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000072 RID: 114
	[AddComponentMenu("Pathfinding/Navmesh/RecastMeshObj")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_recast_mesh_obj.php")]
	public class RecastMeshObj : VersionedMonoBehaviour
	{
		// Token: 0x0600061A RID: 1562 RVA: 0x0002405C File Offset: 0x0002225C
		public static void GetAllInBounds(List<RecastMeshObj> buffer, Bounds bounds)
		{
			if (!Application.isPlaying)
			{
				RecastMeshObj[] array = Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int i = 0; i < array.Length; i++)
				{
					array[i].RecalculateBounds();
					if (array[i].GetBounds().Intersects(bounds))
					{
						buffer.Add(array[i]);
					}
				}
				return;
			}
			if (Time.timeSinceLevelLoad == 0f)
			{
				RecastMeshObj[] array2 = Object.FindObjectsOfType(typeof(RecastMeshObj)) as RecastMeshObj[];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j].Register();
				}
			}
			for (int k = 0; k < RecastMeshObj.dynamicMeshObjs.Count; k++)
			{
				if (RecastMeshObj.dynamicMeshObjs[k].GetBounds().Intersects(bounds))
				{
					buffer.Add(RecastMeshObj.dynamicMeshObjs[k]);
				}
			}
			Rect rect = Rect.MinMaxRect(bounds.min.x, bounds.min.z, bounds.max.x, bounds.max.z);
			RecastMeshObj.tree.QueryInBounds(rect, buffer);
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x00024180 File Offset: 0x00022380
		private void OnEnable()
		{
			this.Register();
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x00024188 File Offset: 0x00022388
		private void Register()
		{
			if (this.registered)
			{
				return;
			}
			this.registered = true;
			this.area = Mathf.Clamp(this.area, -1, 33554432);
			Renderer component = base.GetComponent<Renderer>();
			Collider component2 = base.GetComponent<Collider>();
			if (component == null && component2 == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component3 = base.GetComponent<MeshFilter>();
			if (component != null && component3 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((component != null) ? component.bounds : component2.bounds);
			this._dynamic = this.dynamic;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Add(this);
				return;
			}
			RecastMeshObj.tree.Insert(this);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x00024254 File Offset: 0x00022454
		private void RecalculateBounds()
		{
			Renderer component = base.GetComponent<Renderer>();
			Collider collider = this.GetCollider();
			if (component == null && collider == null)
			{
				throw new Exception("A renderer or a collider should be attached to the GameObject");
			}
			MeshFilter component2 = base.GetComponent<MeshFilter>();
			if (component != null && component2 == null)
			{
				throw new Exception("A renderer was attached but no mesh filter");
			}
			this.bounds = ((component != null) ? component.bounds : collider.bounds);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x000242CD File Offset: 0x000224CD
		public Bounds GetBounds()
		{
			if (this._dynamic)
			{
				this.RecalculateBounds();
			}
			return this.bounds;
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x000242E3 File Offset: 0x000224E3
		public MeshFilter GetMeshFilter()
		{
			return base.GetComponent<MeshFilter>();
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x000242EB File Offset: 0x000224EB
		public Collider GetCollider()
		{
			return base.GetComponent<Collider>();
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x000242F4 File Offset: 0x000224F4
		private void OnDisable()
		{
			this.registered = false;
			if (this._dynamic)
			{
				RecastMeshObj.dynamicMeshObjs.Remove(this);
			}
			else if (!RecastMeshObj.tree.Remove(this))
			{
				throw new Exception("Could not remove RecastMeshObj from tree even though it should exist in it. Has the object moved without being marked as dynamic?");
			}
			this._dynamic = this.dynamic;
		}

		// Token: 0x04000370 RID: 880
		protected static RecastBBTree tree = new RecastBBTree();

		// Token: 0x04000371 RID: 881
		protected static List<RecastMeshObj> dynamicMeshObjs = new List<RecastMeshObj>();

		// Token: 0x04000372 RID: 882
		[HideInInspector]
		public Bounds bounds;

		// Token: 0x04000373 RID: 883
		public bool dynamic = true;

		// Token: 0x04000374 RID: 884
		public int area;

		// Token: 0x04000375 RID: 885
		private bool _dynamic;

		// Token: 0x04000376 RID: 886
		private bool registered;
	}
}
