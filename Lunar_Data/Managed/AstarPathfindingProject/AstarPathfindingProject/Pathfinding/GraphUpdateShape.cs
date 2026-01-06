using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005E RID: 94
	public class GraphUpdateShape
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00010444 File Offset: 0x0000E644
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0001044C File Offset: 0x0000E64C
		public Vector3[] points
		{
			get
			{
				return this._points;
			}
			set
			{
				this._points = value;
				if (this.convex)
				{
					this.CalculateConvexHull();
				}
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00010463 File Offset: 0x0000E663
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0001046B File Offset: 0x0000E66B
		public bool convex
		{
			get
			{
				return this._convex;
			}
			set
			{
				if (this._convex != value && value)
				{
					this.CalculateConvexHull();
				}
				this._convex = value;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001048A File Offset: 0x0000E68A
		public GraphUpdateShape()
		{
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000104B4 File Offset: 0x0000E6B4
		public GraphUpdateShape(Vector3[] points, bool convex, Matrix4x4 matrix, float minimumHeight)
		{
			this.convex = convex;
			this.points = points;
			this.origin = matrix.MultiplyPoint3x4(Vector3.zero);
			this.right = matrix.MultiplyPoint3x4(Vector3.right) - this.origin;
			this.up = matrix.MultiplyPoint3x4(Vector3.up) - this.origin;
			this.forward = matrix.MultiplyPoint3x4(Vector3.forward) - this.origin;
			this.minimumHeight = minimumHeight;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010567 File Offset: 0x0000E767
		private void CalculateConvexHull()
		{
			this._convexPoints = ((this.points != null) ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010585 File Offset: 0x0000E785
		public Bounds GetBounds()
		{
			return GraphUpdateShape.GetBounds(this.convex ? this._convexPoints : this.points, this.right, this.up, this.forward, this.origin, this.minimumHeight);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public static Bounds GetBounds(Vector3[] points, Matrix4x4 matrix, float minimumHeight)
		{
			Vector3 vector = matrix.MultiplyPoint3x4(Vector3.zero);
			Vector3 vector2 = matrix.MultiplyPoint3x4(Vector3.right) - vector;
			Vector3 vector3 = matrix.MultiplyPoint3x4(Vector3.up) - vector;
			Vector3 vector4 = matrix.MultiplyPoint3x4(Vector3.forward) - vector;
			return GraphUpdateShape.GetBounds(points, vector2, vector3, vector4, vector, minimumHeight);
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010620 File Offset: 0x0000E820
		private static Bounds GetBounds(Vector3[] points, Vector3 right, Vector3 up, Vector3 forward, Vector3 origin, float minimumHeight)
		{
			if (points == null || points.Length == 0)
			{
				return default(Bounds);
			}
			float num = points[0].y;
			float num2 = points[0].y;
			for (int i = 0; i < points.Length; i++)
			{
				num = Mathf.Min(num, points[i].y);
				num2 = Mathf.Max(num2, points[i].y);
			}
			float num3 = Mathf.Max(minimumHeight - (num2 - num), 0f) * 0.5f;
			num -= num3;
			num2 += num3;
			Vector3 vector = right * points[0].x + up * points[0].y + forward * points[0].z;
			Vector3 vector2 = vector;
			for (int j = 0; j < points.Length; j++)
			{
				Vector3 vector3 = right * points[j].x + forward * points[j].z;
				Vector3 vector4 = vector3 + up * num;
				Vector3 vector5 = vector3 + up * num2;
				vector = Vector3.Min(vector, vector4);
				vector = Vector3.Min(vector, vector5);
				vector2 = Vector3.Max(vector2, vector4);
				vector2 = Vector3.Max(vector2, vector5);
			}
			return new Bounds((vector + vector2) * 0.5f + origin, vector2 - vector);
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001079E File Offset: 0x0000E99E
		public bool Contains(GraphNode node)
		{
			return this.Contains((Vector3)node.position);
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public bool Contains(Vector3 point)
		{
			point -= this.origin;
			Vector3 vector = new Vector3(Vector3.Dot(point, this.right) / this.right.sqrMagnitude, 0f, Vector3.Dot(point, this.forward) / this.forward.sqrMagnitude);
			if (!this.convex)
			{
				return this._points != null && Polygon.ContainsPointXZ(this._points, vector);
			}
			if (this._convexPoints == null)
			{
				return false;
			}
			int i = 0;
			int num = this._convexPoints.Length - 1;
			while (i < this._convexPoints.Length)
			{
				if (VectorMath.RightOrColinearXZ(this._convexPoints[i], this._convexPoints[num], vector))
				{
					return false;
				}
				num = i;
				i++;
			}
			return true;
		}

		// Token: 0x04000205 RID: 517
		private Vector3[] _points;

		// Token: 0x04000206 RID: 518
		private Vector3[] _convexPoints;

		// Token: 0x04000207 RID: 519
		private bool _convex;

		// Token: 0x04000208 RID: 520
		private Vector3 right = Vector3.right;

		// Token: 0x04000209 RID: 521
		private Vector3 forward = Vector3.forward;

		// Token: 0x0400020A RID: 522
		private Vector3 up = Vector3.up;

		// Token: 0x0400020B RID: 523
		private Vector3 origin;

		// Token: 0x0400020C RID: 524
		public float minimumHeight;

		// Token: 0x0200005F RID: 95
		public struct BurstShape
		{
			// Token: 0x06000375 RID: 885 RVA: 0x00010878 File Offset: 0x0000EA78
			public BurstShape(GraphUpdateShape scene, Allocator allocator)
			{
				Vector3[] array = (scene.convex ? scene._convexPoints : scene._points);
				if (array == null)
				{
					this.points = new NativeArray<Vector3>(0, allocator, NativeArrayOptions.ClearMemory);
				}
				else
				{
					this.points = new NativeArray<Vector3>(array, allocator);
				}
				this.origin = scene.origin;
				this.right = scene.right;
				this.forward = scene.forward;
				float num = scene.right.sqrMagnitude;
				if (num > 0f)
				{
					this.right /= num;
				}
				num = scene.forward.sqrMagnitude;
				if (num > 0f)
				{
					this.forward /= num;
				}
				this.containsEverything = false;
			}

			// Token: 0x170000A8 RID: 168
			// (get) Token: 0x06000376 RID: 886 RVA: 0x00010944 File Offset: 0x0000EB44
			public static GraphUpdateShape.BurstShape Everything
			{
				get
				{
					return new GraphUpdateShape.BurstShape
					{
						points = new NativeArray<Vector3>(0, Allocator.Persistent, NativeArrayOptions.ClearMemory),
						origin = float3.zero,
						right = float3.zero,
						forward = float3.zero,
						containsEverything = true
					};
				}
			}

			// Token: 0x06000377 RID: 887 RVA: 0x00010998 File Offset: 0x0000EB98
			public bool Contains(float3 point)
			{
				if (this.containsEverything)
				{
					return true;
				}
				point -= this.origin;
				float3 @float = new float3(math.dot(point, this.right), 0f, math.dot(point, this.forward));
				int num = this.points.Length - 1;
				bool flag = false;
				int i = 0;
				while (i < this.points.Length)
				{
					if (((this.points[i].z <= @float.z && @float.z < this.points[num].z) || (this.points[num].z <= @float.z && @float.z < this.points[i].z)) && @float.x < (this.points[num].x - this.points[i].x) * (@float.z - this.points[i].z) / (this.points[num].z - this.points[i].z) + this.points[i].x)
					{
						flag = !flag;
					}
					num = i++;
				}
				return flag;
			}

			// Token: 0x0400020D RID: 525
			[DeallocateOnJobCompletion]
			private NativeArray<Vector3> points;

			// Token: 0x0400020E RID: 526
			private float3 origin;

			// Token: 0x0400020F RID: 527
			private float3 right;

			// Token: 0x04000210 RID: 528
			private float3 forward;

			// Token: 0x04000211 RID: 529
			private bool containsEverything;
		}
	}
}
