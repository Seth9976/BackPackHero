using System;
using System.Collections.Generic;
using Pathfinding.Drawing;
using Pathfinding.Graphs.Util;
using Pathfinding.Serialization;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pathfinding
{
	// Token: 0x0200011E RID: 286
	[AddComponentMenu("Pathfinding/Navmesh/Navmesh Cut")]
	[ExecuteAlways]
	[HelpURL("https://arongranberg.com/astar/documentation/stable/navmeshcut.html")]
	public class NavmeshCut : NavmeshClipper
	{
		// Token: 0x060008D3 RID: 2259 RVA: 0x0002FA43 File Offset: 0x0002DC43
		protected override void Awake()
		{
			base.Awake();
			this.tr = base.transform;
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x0002FA57 File Offset: 0x0002DC57
		protected override void OnDisable()
		{
			if (this.meshContourVertices.IsCreated)
			{
				this.meshContourVertices.Dispose();
			}
			if (this.meshContours.IsCreated)
			{
				this.meshContours.Dispose();
			}
			this.lastMesh = null;
			base.OnDisable();
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002FA96 File Offset: 0x0002DC96
		public override void ForceUpdate()
		{
			if (AstarPath.active != null)
			{
				AstarPath.active.navmeshUpdates.ForceUpdateAround(this);
			}
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002FAB8 File Offset: 0x0002DCB8
		public override bool RequiresUpdate(GridLookup<NavmeshClipper>.Root previousState)
		{
			return (this.tr.position - previousState.previousPosition).sqrMagnitude > this.updateDistance * this.updateDistance || (this.useRotationAndScale && Quaternion.Angle(previousState.previousRotation, this.tr.rotation) > this.updateRotationDistance);
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000033F6 File Offset: 0x000015F6
		public virtual void UsedForCut()
		{
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0002FB1C File Offset: 0x0002DD1C
		internal override void NotifyUpdated(GridLookup<NavmeshClipper>.Root previousState)
		{
			previousState.previousPosition = this.tr.position;
			if (this.useRotationAndScale)
			{
				previousState.previousRotation = this.tr.rotation;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x0002FB48 File Offset: 0x0002DD48
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
			NativeList<float3> nativeList = new NativeList<float3>(Allocator.Persistent);
			NativeList<NavmeshCut.ContourBurst> nativeList2 = new NativeList<NavmeshCut.ContourBurst>(Allocator.Persistent);
			for (int l = 0; l < vertices.Length; l++)
			{
				if (NavmeshCut.pointers.ContainsKey(l))
				{
					int length = nativeList.Length;
					int num2 = l;
					do
					{
						int num3 = NavmeshCut.pointers[num2];
						if (num3 == -1)
						{
							break;
						}
						NavmeshCut.pointers[num2] = -1;
						float3 @float = vertices[num2];
						nativeList.Add(in @float);
						num2 = num3;
					}
					while (num2 != l);
					if (nativeList.Length != length)
					{
						NavmeshCut.ContourBurst contourBurst = default(NavmeshCut.ContourBurst);
						contourBurst.startIndex = length;
						contourBurst.endIndex = nativeList.Length;
						contourBurst.ymin = 0f;
						contourBurst.ymax = 0f;
						nativeList2.Add(in contourBurst);
					}
				}
			}
			if (this.meshContourVertices.IsCreated)
			{
				this.meshContourVertices.Dispose();
			}
			if (this.meshContours.IsCreated)
			{
				this.meshContours.Dispose();
			}
			this.meshContourVertices = nativeList;
			this.meshContours = nativeList2;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x0002FDBC File Offset: 0x0002DFBC
		public override Rect GetBounds(GraphTransform inverseTransform, float radiusMargin)
		{
			List<NavmeshCut.Contour> list = ListPool<NavmeshCut.Contour>.Claim();
			this.GetContour(list, inverseTransform.inverseMatrix, radiusMargin);
			Rect rect = default(Rect);
			for (int i = 0; i < list.Count; i++)
			{
				List<Vector2> contour = list[i].contour;
				for (int j = 0; j < contour.Count; j++)
				{
					Vector2 vector = contour[j];
					if (j == 0 && i == 0)
					{
						rect = new Rect(vector.x, vector.y, 0f, 0f);
					}
					else
					{
						rect.xMax = Math.Max(rect.xMax, vector.x);
						rect.yMax = Math.Max(rect.yMax, vector.y);
						rect.xMin = Math.Min(rect.xMin, vector.x);
						rect.yMin = Math.Min(rect.yMin, vector.y);
					}
				}
				ListPool<Vector2>.Release(ref contour);
			}
			ListPool<NavmeshCut.Contour>.Release(ref list);
			return rect;
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x0002FED0 File Offset: 0x0002E0D0
		private Matrix4x4 contourTransformationMatrix
		{
			get
			{
				if (this.useRotationAndScale)
				{
					return this.tr.localToWorldMatrix * Matrix4x4.Translate(this.center);
				}
				return Matrix4x4.Translate(this.tr.position + this.center);
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0002FF1C File Offset: 0x0002E11C
		public unsafe void GetContour(List<NavmeshCut.Contour> buffer, Matrix4x4 matrix, float radiusMargin)
		{
			UnsafeList<float2> unsafeList = new UnsafeList<float2>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			UnsafeList<NavmeshCut.ContourBurst> unsafeList2 = new UnsafeList<NavmeshCut.ContourBurst>(1, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			this.GetContourBurst(&unsafeList, &unsafeList2, matrix, radiusMargin);
			for (int i = 0; i < unsafeList2.Length; i++)
			{
				List<Vector2> list = ListPool<Vector2>.Claim();
				NavmeshCut.ContourBurst contourBurst = unsafeList2[i];
				for (int j = contourBurst.startIndex; j < contourBurst.endIndex; j++)
				{
					list.Add(unsafeList[j]);
				}
				buffer.Add(new NavmeshCut.Contour
				{
					ymin = contourBurst.ymin,
					ymax = contourBurst.ymax,
					contour = list
				});
			}
			unsafeList.Dispose();
			unsafeList2.Dispose();
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002FFEC File Offset: 0x0002E1EC
		public unsafe void GetContourBurst(UnsafeList<float2>* outputVertices, UnsafeList<NavmeshCut.ContourBurst>* outputContours, Matrix4x4 matrix, float radiusMargin)
		{
			if (this.radiusExpansionMode == NavmeshCut.RadiusExpansionMode.DontExpand)
			{
				radiusMargin = 0f;
			}
			if (this.type == NavmeshCut.MeshType.CustomMesh && (this.mesh != this.lastMesh || !this.meshContours.IsCreated || !this.meshContourVertices.IsCreated))
			{
				this.CalculateMeshContour();
				this.lastMesh = this.mesh;
			}
			NavmeshCutJobs.JobCalculateContour jobCalculateContour = new NavmeshCutJobs.JobCalculateContour
			{
				outputVertices = outputVertices,
				outputContours = outputContours,
				matrix = matrix,
				localToWorldMatrix = this.contourTransformationMatrix,
				radiusMargin = radiusMargin,
				circleResolution = this.circleResolution,
				circleRadius = this.circleRadius,
				rectangleSize = this.rectangleSize,
				height = this.height,
				meshType = this.type,
				meshContours = this.meshContours.GetUnsafeList(),
				meshContourVertices = this.meshContourVertices.GetUnsafeList(),
				meshScale = this.meshScale
			};
			NavmeshCutJobsCached.CalculateContourBurst(&jobCalculateContour);
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00030118 File Offset: 0x0002E318
		public unsafe override void DrawGizmos()
		{
			if (this.tr == null)
			{
				this.tr = base.transform;
			}
			bool flag = GizmoContext.InActiveSelection(this.tr);
			NavmeshBase navmeshBase = ((AstarPath.active != null) ? (AstarPath.active.data.recastGraph ?? AstarPath.active.data.navmesh) : null);
			GraphTransform graphTransform = ((navmeshBase != null) ? navmeshBase.transform : GraphTransform.identityTransform);
			float num = ((navmeshBase != null) ? navmeshBase.NavmeshCuttingCharacterRadius : 0f);
			UnsafeList<float2> unsafeList = new UnsafeList<float2>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			UnsafeList<NavmeshCut.ContourBurst> unsafeList2 = new UnsafeList<NavmeshCut.ContourBurst>(0, Allocator.Temp, NativeArrayOptions.UninitializedMemory);
			this.GetContourBurst(&unsafeList, &unsafeList2, graphTransform.inverseMatrix, num);
			Color color = Color.Lerp(NavmeshCut.GizmoColor, Color.white, 0.5f);
			color.a *= 0.5f;
			using (Draw.WithColor(color))
			{
				for (int i = 0; i < unsafeList2.Length; i++)
				{
					NavmeshCut.ContourBurst contourBurst = unsafeList2[i];
					float num2 = (contourBurst.ymin + contourBurst.ymax) * 0.5f;
					int num3 = contourBurst.endIndex - contourBurst.startIndex;
					for (int j = 0; j < num3; j++)
					{
						float2 @float = unsafeList[contourBurst.startIndex + j];
						float2 float2 = unsafeList[contourBurst.startIndex + (j + 1) % num3];
						Vector3 vector = new Vector3(@float.x, num2, @float.y);
						Vector3 vector2 = new Vector3(float2.x, num2, float2.y);
						Draw.Line(graphTransform.Transform(vector), graphTransform.Transform(vector2), NavmeshCut.GizmoColor);
						if (flag)
						{
							Vector3 vector3 = vector;
							Vector3 vector4 = vector2;
							Vector3 vector5 = vector;
							Vector3 vector6 = vector2;
							vector3.y = (vector4.y = contourBurst.ymin);
							vector5.y = (vector6.y = contourBurst.ymax);
							Draw.Line(graphTransform.Transform(vector3), graphTransform.Transform(vector4));
							Draw.Line(graphTransform.Transform(vector5), graphTransform.Transform(vector6));
							Draw.Line(graphTransform.Transform(vector3), graphTransform.Transform(vector5));
						}
					}
				}
			}
			if (flag)
			{
				switch (this.type)
				{
				case NavmeshCut.MeshType.CustomMesh:
					goto IL_047E;
				case NavmeshCut.MeshType.Box:
					using (Draw.WithMatrix(this.contourTransformationMatrix * Matrix4x4.Scale(new Vector3(this.rectangleSize.x, this.height, this.rectangleSize.y))))
					{
						Draw.WireBox(Vector3.zero, Vector3.one, NavmeshCut.GizmoColor2);
						goto IL_04D3;
					}
					break;
				case NavmeshCut.MeshType.Sphere:
				{
					float num4 = (this.useRotationAndScale ? math.cmax(this.tr.lossyScale) : 1f);
					using (Draw.WithMatrix(Matrix4x4.TRS(this.tr.position, this.useRotationAndScale ? this.tr.rotation : Quaternion.identity, Vector3.one * num4) * Matrix4x4.Translate(this.center)))
					{
						Draw.WireSphere(Vector3.zero, this.circleRadius, NavmeshCut.GizmoColor2);
						goto IL_04D3;
					}
					goto IL_047E;
				}
				case NavmeshCut.MeshType.Capsule:
					break;
				default:
					goto IL_04D3;
				}
				Matrix4x4 contourTransformationMatrix = this.contourTransformationMatrix;
				float num5 = Mathf.Max(this.height, this.circleRadius * 2f);
				float num6 = math.length(contourTransformationMatrix.GetColumn(0));
				float num7 = math.length(contourTransformationMatrix.GetColumn(2));
				float num8 = this.circleRadius * math.max(num6, num7);
				Vector3 normalized = contourTransformationMatrix.GetColumn(1).normalized;
				Vector3 vector7 = this.contourTransformationMatrix.MultiplyPoint3x4(new Vector3(0f, num5 * 0.5f, 0f)) - normalized * num8;
				Vector3 vector8 = this.contourTransformationMatrix.MultiplyPoint3x4(-new Vector3(0f, num5 * 0.5f, 0f)) + normalized * num8;
				Draw.WireCapsule(vector7, vector8, num8, NavmeshCut.GizmoColor2);
				goto IL_04D3;
				IL_047E:
				if (this.mesh != null)
				{
					using (Draw.WithMatrix(this.contourTransformationMatrix * Matrix4x4.Scale(Vector3.one * this.meshScale)))
					{
						Draw.WireMesh(this.mesh, NavmeshCut.GizmoColor2);
					}
				}
			}
			IL_04D3:
			unsafeList.Dispose();
			unsafeList2.Dispose();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0003066C File Offset: 0x0002E86C
		protected override void OnUpgradeSerializedData(ref Migrations migrations, bool unityThread)
		{
			int num;
			if (migrations.TryMigrateFromLegacyFormat(out num) && num < 2)
			{
				this.radiusExpansionMode = NavmeshCut.RadiusExpansionMode.DontExpand;
			}
		}

		// Token: 0x04000600 RID: 1536
		[Tooltip("Shape of the cut")]
		public NavmeshCut.MeshType type = NavmeshCut.MeshType.Box;

		// Token: 0x04000601 RID: 1537
		[Tooltip("The contour(s) of the mesh will be extracted. This mesh should only be a 2D surface, not a volume (see documentation).")]
		public Mesh mesh;

		// Token: 0x04000602 RID: 1538
		public Vector2 rectangleSize = new Vector2(1f, 1f);

		// Token: 0x04000603 RID: 1539
		public float circleRadius = 1f;

		// Token: 0x04000604 RID: 1540
		public int circleResolution = 6;

		// Token: 0x04000605 RID: 1541
		public float height = 1f;

		// Token: 0x04000606 RID: 1542
		[Tooltip("Scale of the custom mesh")]
		public float meshScale = 1f;

		// Token: 0x04000607 RID: 1543
		public Vector3 center;

		// Token: 0x04000608 RID: 1544
		[Tooltip("Distance between positions to require an update of the navmesh\nA smaller distance gives better accuracy, but requires more updates when moving the object over time, so it is often slower.")]
		public float updateDistance = 0.4f;

		// Token: 0x04000609 RID: 1545
		[Tooltip("Only makes a split in the navmesh, but does not remove the geometry to make a hole")]
		public bool isDual;

		// Token: 0x0400060A RID: 1546
		public NavmeshCut.RadiusExpansionMode radiusExpansionMode = NavmeshCut.RadiusExpansionMode.ExpandByAgentRadius;

		// Token: 0x0400060B RID: 1547
		public bool cutsAddedGeom = true;

		// Token: 0x0400060C RID: 1548
		[Tooltip("How many degrees rotation that is required for an update to the navmesh. Should be between 0 and 180.")]
		public float updateRotationDistance = 10f;

		// Token: 0x0400060D RID: 1549
		[Tooltip("Includes rotation in calculations. This is slower since a lot more matrix multiplications are needed but gives more flexibility.")]
		[FormerlySerializedAs("useRotation")]
		public bool useRotationAndScale;

		// Token: 0x0400060E RID: 1550
		private NativeList<float3> meshContourVertices;

		// Token: 0x0400060F RID: 1551
		private NativeList<NavmeshCut.ContourBurst> meshContours;

		// Token: 0x04000610 RID: 1552
		protected Transform tr;

		// Token: 0x04000611 RID: 1553
		private Mesh lastMesh;

		// Token: 0x04000612 RID: 1554
		private static readonly Dictionary<Int2, int> edges = new Dictionary<Int2, int>();

		// Token: 0x04000613 RID: 1555
		private static readonly Dictionary<int, int> pointers = new Dictionary<int, int>();

		// Token: 0x04000614 RID: 1556
		public static readonly Color GizmoColor = new Color(0.14509805f, 0.72156864f, 0.9372549f);

		// Token: 0x04000615 RID: 1557
		public static readonly Color GizmoColor2 = new Color(0.6627451f, 0.36078432f, 0.9490196f);

		// Token: 0x0200011F RID: 287
		public enum MeshType
		{
			// Token: 0x04000617 RID: 1559
			Rectangle,
			// Token: 0x04000618 RID: 1560
			Circle,
			// Token: 0x04000619 RID: 1561
			CustomMesh,
			// Token: 0x0400061A RID: 1562
			Box,
			// Token: 0x0400061B RID: 1563
			Sphere,
			// Token: 0x0400061C RID: 1564
			Capsule
		}

		// Token: 0x02000120 RID: 288
		public enum RadiusExpansionMode
		{
			// Token: 0x0400061E RID: 1566
			DontExpand,
			// Token: 0x0400061F RID: 1567
			ExpandByAgentRadius
		}

		// Token: 0x02000121 RID: 289
		public struct Contour
		{
			// Token: 0x04000620 RID: 1568
			public float ymin;

			// Token: 0x04000621 RID: 1569
			public float ymax;

			// Token: 0x04000622 RID: 1570
			public List<Vector2> contour;
		}

		// Token: 0x02000122 RID: 290
		public struct ContourBurst
		{
			// Token: 0x04000623 RID: 1571
			public int startIndex;

			// Token: 0x04000624 RID: 1572
			public int endIndex;

			// Token: 0x04000625 RID: 1573
			public float ymin;

			// Token: 0x04000626 RID: 1574
			public float ymax;
		}
	}
}
