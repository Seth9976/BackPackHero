using System;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200007D RID: 125
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_navmesh_add.php")]
	public class NavmeshAdd : NavmeshClipper
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x00026434 File Offset: 0x00024634
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00026498 File Offset: 0x00024698
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x000264B4 File Offset: 0x000246B4
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x000264C8 File Offset: 0x000246C8
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x000264F4 File Offset: 0x000246F4
		public Vector3 Center
		{
			get
			{
				return this.tr.position + (this.useRotationAndScale ? this.tr.TransformPoint(this.center) : this.center);
			}
		}

		// Token: 0x06000662 RID: 1634 RVA: 0x00026528 File Offset: 0x00024728
		[ContextMenu("Rebuild Mesh")]
		public void RebuildMesh()
		{
			if (this.type != NavmeshAdd.MeshType.CustomMesh)
			{
				if (this.verts == null || this.verts.Length != 4 || this.tris == null || this.tris.Length != 6)
				{
					this.verts = new Vector3[4];
					this.tris = new int[6];
				}
				this.tris[0] = 0;
				this.tris[1] = 1;
				this.tris[2] = 2;
				this.tris[3] = 0;
				this.tris[4] = 2;
				this.tris[5] = 3;
				this.verts[0] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[1] = new Vector3(this.rectangleSize.x * 0.5f, 0f, -this.rectangleSize.y * 0.5f);
				this.verts[2] = new Vector3(this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				this.verts[3] = new Vector3(-this.rectangleSize.x * 0.5f, 0f, this.rectangleSize.y * 0.5f);
				return;
			}
			if (this.mesh == null)
			{
				this.verts = null;
				this.tris = null;
				return;
			}
			this.verts = this.mesh.vertices;
			this.tris = this.mesh.triangles;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x000266D8 File Offset: 0x000248D8
		public override Rect GetBounds(GraphTransform inverseTransform)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			Int3[] array = ArrayPool<Int3>.Claim((this.verts != null) ? this.verts.Length : 0);
			int[] array2;
			this.GetMesh(ref array, out array2, inverseTransform);
			Rect rect = default(Rect);
			for (int i = 0; i < array2.Length; i++)
			{
				Vector3 vector = (Vector3)array[array2[i]];
				if (i == 0)
				{
					rect = new Rect(vector.x, vector.z, 0f, 0f);
				}
				else
				{
					rect.xMax = Math.Max(rect.xMax, vector.x);
					rect.yMax = Math.Max(rect.yMax, vector.z);
					rect.xMin = Math.Min(rect.xMin, vector.x);
					rect.yMin = Math.Min(rect.yMin, vector.z);
				}
			}
			ArrayPool<Int3>.Release(ref array, false);
			return rect;
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x000267D8 File Offset: 0x000249D8
		public void GetMesh(ref Int3[] vbuffer, out int[] tbuffer, GraphTransform inverseTransform = null)
		{
			if (this.verts == null)
			{
				this.RebuildMesh();
			}
			if (this.verts == null)
			{
				tbuffer = ArrayPool<int>.Claim(0);
				return;
			}
			if (vbuffer == null || vbuffer.Length < this.verts.Length)
			{
				if (vbuffer != null)
				{
					ArrayPool<Int3>.Release(ref vbuffer, false);
				}
				vbuffer = ArrayPool<Int3>.Claim(this.verts.Length);
			}
			tbuffer = this.tris;
			if (this.useRotationAndScale)
			{
				Matrix4x4 matrix4x = Matrix4x4.TRS(this.tr.position + this.center, this.tr.rotation, this.tr.localScale * this.meshScale);
				for (int i = 0; i < this.verts.Length; i++)
				{
					Vector3 vector = matrix4x.MultiplyPoint3x4(this.verts[i]);
					if (inverseTransform != null)
					{
						vector = inverseTransform.InverseTransform(vector);
					}
					vbuffer[i] = (Int3)vector;
				}
				return;
			}
			Vector3 vector2 = this.tr.position + this.center;
			for (int j = 0; j < this.verts.Length; j++)
			{
				Vector3 vector3 = vector2 + this.verts[j] * this.meshScale;
				if (inverseTransform != null)
				{
					vector3 = inverseTransform.InverseTransform(vector3);
				}
				vbuffer[j] = (Int3)vector3;
			}
		}

		// Token: 0x040003AA RID: 938
		public NavmeshAdd.MeshType type;

		// Token: 0x040003AB RID: 939
		public Mesh mesh;

		// Token: 0x040003AC RID: 940
		private Vector3[] verts;

		// Token: 0x040003AD RID: 941
		private int[] tris;

		// Token: 0x040003AE RID: 942
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x040003AF RID: 943
		public float meshScale = 1f;

		// Token: 0x040003B0 RID: 944
		public Vector3 center;

		// Token: 0x040003B1 RID: 945
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x040003B2 RID: 946
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x040003B3 RID: 947
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x040003B4 RID: 948
		protected Transform tr;

		// Token: 0x040003B5 RID: 949
		private Vector3 lastPosition;

		// Token: 0x040003B6 RID: 950
		private Quaternion lastRotation;

		// Token: 0x040003B7 RID: 951
		public static readonly Color GizmoColor = new Color(0.6039216f, 0.13725491f, 0.9372549f);

		// Token: 0x0200013C RID: 316
		public enum MeshType
		{
			// Token: 0x04000744 RID: 1860
			Rectangle,
			// Token: 0x04000745 RID: 1861
			CustomMesh
		}
	}
}
