using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000034 RID: 52
	public class GraphUpdateShape
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000CA80 File Offset: 0x0000AC80
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000CA88 File Offset: 0x0000AC88
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

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000CA9F File Offset: 0x0000AC9F
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000CAA7 File Offset: 0x0000ACA7
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

		// Token: 0x06000279 RID: 633 RVA: 0x0000CAC6 File Offset: 0x0000ACC6
		public GraphUpdateShape()
		{
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000CAF0 File Offset: 0x0000ACF0
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

		// Token: 0x0600027B RID: 635 RVA: 0x0000CBA3 File Offset: 0x0000ADA3
		private void CalculateConvexHull()
		{
			this._convexPoints = ((this.points != null) ? Polygon.ConvexHullXZ(this.points) : null);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000CBC1 File Offset: 0x0000ADC1
		public Bounds GetBounds()
		{
			return GraphUpdateShape.GetBounds(this.convex ? this._convexPoints : this.points, this.right, this.up, this.forward, this.origin, this.minimumHeight);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000CBFC File Offset: 0x0000ADFC
		public static Bounds GetBounds(Vector3[] points, Matrix4x4 matrix, float minimumHeight)
		{
			Vector3 vector = matrix.MultiplyPoint3x4(Vector3.zero);
			Vector3 vector2 = matrix.MultiplyPoint3x4(Vector3.right) - vector;
			Vector3 vector3 = matrix.MultiplyPoint3x4(Vector3.up) - vector;
			Vector3 vector4 = matrix.MultiplyPoint3x4(Vector3.forward) - vector;
			return GraphUpdateShape.GetBounds(points, vector2, vector3, vector4, vector, minimumHeight);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000CC5C File Offset: 0x0000AE5C
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

		// Token: 0x0600027F RID: 639 RVA: 0x0000CDDA File Offset: 0x0000AFDA
		public bool Contains(GraphNode node)
		{
			return this.Contains((Vector3)node.position);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
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

		// Token: 0x0400017B RID: 379
		private Vector3[] _points;

		// Token: 0x0400017C RID: 380
		private Vector3[] _convexPoints;

		// Token: 0x0400017D RID: 381
		private bool _convex;

		// Token: 0x0400017E RID: 382
		private Vector3 right = Vector3.right;

		// Token: 0x0400017F RID: 383
		private Vector3 forward = Vector3.forward;

		// Token: 0x04000180 RID: 384
		private Vector3 up = Vector3.up;

		// Token: 0x04000181 RID: 385
		private Vector3 origin;

		// Token: 0x04000182 RID: 386
		public float minimumHeight;
	}
}
