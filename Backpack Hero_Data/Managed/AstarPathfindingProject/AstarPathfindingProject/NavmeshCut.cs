using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200007F RID: 127
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	[HelpURL("http://arongranberg.com/astar/documentation/stable/class_pathfinding_1_1_navmesh_cut.php")]
	public class NavmeshCut : NavmeshClipper
	{
		// Token: 0x06000672 RID: 1650 RVA: 0x00026AB3 File Offset: 0x00024CB3
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x00026AC7 File Offset: 0x00024CC7
		protected override void OnEnable()
		{
			base.OnEnable();
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
			this.lastRotation = this.tr.rotation;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x00026AFA File Offset: 0x00024CFA
		public override void ForceUpdate()
		{
			this.lastPosition = new Vector3(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x00026B18 File Offset: 0x00024D18
		public override bool RequiresUpdate()
		{
			return (this.tr.position - this.lastPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(this.lastRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x00026B7C File Offset: 0x00024D7C
		public virtual void UsedForCut()
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00026B7E File Offset: 0x00024D7E
		internal override void NotifyUpdated()
		{
			this.lastPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				this.lastRotation = this.tr.rotation;
			}
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00026BAC File Offset: 0x00024DAC
		private void CalculateMeshContour()
		{
			if (this.mesh == null)
			{
				return;
			}
			NavmeshCut.edges.Clear();
			NavmeshCut.pointers.Clear();
			Vector3[] vertices = this.mesh.vertices;
			int[] triangles = this.mesh.triangles;
			for (int i = 0; i < triangles.Length; i += 3)
			{
				if (VectorMath.IsClockwiseXZ(vertices[triangles[i]], vertices[triangles[i + 1]], vertices[triangles[i + 2]]))
				{
					int num = triangles[i];
					triangles[i] = triangles[i + 2];
					triangles[i + 2] = num;
				}
				NavmeshCut.edges[new Int2(triangles[i], triangles[i + 1])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 1], triangles[i + 2])] = i;
				NavmeshCut.edges[new Int2(triangles[i + 2], triangles[i])] = i;
			}
			for (int j = 0; j < triangles.Length; j += 3)
			{
				for (int k = 0; k < 3; k++)
				{
					if (!NavmeshCut.edges.ContainsKey(new Int2(triangles[j + (k + 1) % 3], triangles[j + k % 3])))
					{
						NavmeshCut.pointers[triangles[j + k % 3]] = triangles[j + (k + 1) % 3];
					}
				}
			}
			List<Vector3[]> list = new List<Vector3[]>();
			List<Vector3> list2 = ListPool<Vector3>.Claim();
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					list2.Clear();
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						list2.Add(vertices[num2]);
						num2 = num3;
						if (num2 == -1)
						{
							goto Block_9;
						}
					}
					while (num2 != l);
					IL_01E4:
					if (list2.Count > 0)
					{
						list.Add(list2.ToArray());
						goto IL_01F9;
					}
					goto IL_01F9;
					Block_9:
					Debug.LogError("Invalid Mesh '" + this.mesh.name + " in " + base.gameObject.name);
					goto IL_01E4;
				}
				IL_01F9:;
			}
			ListPool<Vector3>.Release(ref list2);
			this.contours = list.ToArray();
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00026DD8 File Offset: 0x00024FD8
		public override Rect GetBounds(GraphTransform inverseTransform)
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Rect rect = default(Rect);
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = inverseTransform.InverseTransform(list2[j]);
					if (j == 0)
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
			}
			ListPool<List<Vector3>>.Release(ref list);
			return rect;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00026EDC File Offset: 0x000250DC
		public void GetContour(List<List<Vector3>> buffer)
		{
			if (this.circleResolution < 3)
			{
				this.circleResolution = 3;
			}
			switch (this.type)
			{
			case NavmeshCut.MeshType.Rectangle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim();
				list.Add(new Vector3(-this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, -this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				list.Add(new Vector3(-this.rectangleSize.x, 0f, this.rectangleSize.y) * 0.5f);
				bool flag = (this.rectangleSize.x < 0f) ^ (this.rectangleSize.y < 0f);
				this.TransformBuffer(list, flag);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.Circle:
			{
				List<Vector3> list = ListPool<Vector3>.Claim(this.circleResolution);
				for (int i = 0; i < this.circleResolution; i++)
				{
					list.Add(new Vector3(Mathf.Cos((float)(i * 2) * 3.1415927f / (float)this.circleResolution), 0f, Mathf.Sin((float)(i * 2) * 3.1415927f / (float)this.circleResolution)) * this.circleRadius);
				}
				bool flag = this.circleRadius < 0f;
				this.TransformBuffer(list, flag);
				buffer.Add(list);
				return;
			}
			case NavmeshCut.MeshType.CustomMesh:
				if (this.mesh != this.lastMesh || this.contours == null)
				{
					this.CalculateMeshContour();
					this.lastMesh = this.mesh;
				}
				if (this.contours != null)
				{
					bool flag = this.meshScale < 0f;
					for (int j = 0; j < this.contours.Length; j++)
					{
						Vector3[] array = this.contours[j];
						List<Vector3> list = ListPool<Vector3>.Claim(array.Length);
						for (int k = 0; k < array.Length; k++)
						{
							list.Add(array[k] * this.meshScale);
						}
						this.TransformBuffer(list, flag);
						buffer.Add(list);
					}
				}
				return;
			default:
				return;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00027144 File Offset: 0x00025344
		private void TransformBuffer(List<Vector3> buffer, bool reverse)
		{
			Vector3 vector = this.center;
			if (this.useRotationAndScale)
			{
				Matrix4x4 localToWorldMatrix = this.tr.localToWorldMatrix;
				for (int i = 0; i < buffer.Count; i++)
				{
					buffer[i] = localToWorldMatrix.MultiplyPoint3x4(buffer[i] + vector);
				}
				reverse ^= VectorMath.ReversesFaceOrientationsXZ(localToWorldMatrix);
			}
			else
			{
				vector += this.tr.position;
				for (int j = 0; j < buffer.Count; j++)
				{
					int num = j;
					buffer[num] += vector;
				}
			}
			if (reverse)
			{
				buffer.Reverse();
			}
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x000271F0 File Offset: 0x000253F0
		public void OnDrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Gizmos.color = NavmeshCut.GizmoColor;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = list2[j];
					Vector3 vector2 = list2[(j + 1) % list2.Count];
					Gizmos.DrawLine(vector, vector2);
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0002727F File Offset: 0x0002547F
		internal float GetY(GraphTransform transform)
		{
			return transform.InverseTransform(this.useRotationAndScale ? this.tr.TransformPoint(this.center) : (this.tr.position + this.center)).y;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x000272C0 File Offset: 0x000254C0
		public void OnDrawGizmosSelected()
		{
			List<List<Vector3>> list = ListPool<List<Vector3>>.Claim();
			this.GetContour(list);
			Color color = Color.Lerp(NavmeshCut.GizmoColor, Color.white, 0.5f);
			color.a *= 0.5f;
			Gizmos.color = color;
			NavmeshBase navmeshBase = ((AstarPath.active != null) ? (AstarPath.active.data.recastGraph ?? AstarPath.active.data.navmesh) : null);
			GraphTransform graphTransform = ((navmeshBase != null) ? navmeshBase.transform : GraphTransform.identityTransform);
			float y = this.GetY(graphTransform);
			float num = y - this.height * 0.5f;
			float num2 = y + this.height * 0.5f;
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector3> list2 = list[i];
				for (int j = 0; j < list2.Count; j++)
				{
					Vector3 vector = graphTransform.InverseTransform(list2[j]);
					Vector3 vector2 = graphTransform.InverseTransform(list2[(j + 1) % list2.Count]);
					Vector3 vector3 = vector;
					Vector3 vector4 = vector2;
					Vector3 vector5 = vector;
					Vector3 vector6 = vector2;
					vector3.y = (vector4.y = num);
					vector5.y = (vector6.y = num2);
					Gizmos.DrawLine(graphTransform.Transform(vector3), graphTransform.Transform(vector4));
					Gizmos.DrawLine(graphTransform.Transform(vector5), graphTransform.Transform(vector6));
					Gizmos.DrawLine(graphTransform.Transform(vector3), graphTransform.Transform(vector5));
				}
			}
			ListPool<List<Vector3>>.Release(ref list);
		}

		// Token: 0x040003BD RID: 957
		[Tooltip("Shape of the cut")]
		public NavmeshCut.MeshType type;

		// Token: 0x040003BE RID: 958
		[Tooltip("The contour(s) of the mesh will be extracted. This mesh should only be a 2D surface, not a volume (see documentation).")]
		public Mesh mesh;

		// Token: 0x040003BF RID: 959
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x040003C0 RID: 960
		public float circleRadius = 1f;

		// Token: 0x040003C1 RID: 961
		public int circleResolution = 6;

		// Token: 0x040003C2 RID: 962
		public float height = 1f;

		// Token: 0x040003C3 RID: 963
		[Tooltip("Scale of the custom mesh")]
		public float meshScale = 1f;

		// Token: 0x040003C4 RID: 964
		public Vector3 center;

		// Token: 0x040003C5 RID: 965
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x040003C6 RID: 966
		[Tooltip("Only makes a split in the navmesh, but does not remove the geometry to make a hole")]
		public bool isDual;

		// Token: 0x040003C7 RID: 967
		public bool cutsAddedGeom = true;

		// Token: 0x040003C8 RID: 968
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x040003C9 RID: 969
		[Tooltip("Includes rotation in calculations. This is slower since a lot more matrix multiplications are needed but gives more flexibility.")]
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x040003CA RID: 970
		private Vector3[][] contours;

		// Token: 0x040003CB RID: 971
		protected Transform tr;

		// Token: 0x040003CC RID: 972
		private Mesh lastMesh;

		// Token: 0x040003CD RID: 973
		private Vector3 lastPosition;

		// Token: 0x040003CE RID: 974
		private Quaternion lastRotation;

		// Token: 0x040003CF RID: 975
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x040003D0 RID: 976
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x040003D1 RID: 977
		public static readonly Color GizmoColor = new Color(0.14509805f, 0.72156864f, 0.9372549f);

		// Token: 0x0200013D RID: 317
		public enum MeshType
		{
			// Token: 0x04000747 RID: 1863
			Rectangle,
			// Token: 0x04000748 RID: 1864
			Circle,
			// Token: 0x04000749 RID: 1865
			CustomMesh
		}
	}
}
