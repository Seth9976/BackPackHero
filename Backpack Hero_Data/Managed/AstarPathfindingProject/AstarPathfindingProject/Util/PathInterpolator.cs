using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x020000C2 RID: 194
	public class PathInterpolator
	{
		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x0003751C File Offset: 0x0003571C
		public virtual Vector3 position
		{
			get
			{
				float num = ((this.currentSegmentLength > 0.0001f) ? ((this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength) : 0f);
				return Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], num);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x0003757C File Offset: 0x0003577C
		public Vector3 endPoint
		{
			get
			{
				return this.path[this.path.Count - 1];
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00037596 File Offset: 0x00035796
		public Vector3 tangent
		{
			get
			{
				return this.path[this.segmentIndex + 1] - this.path[this.segmentIndex];
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x000375C1 File Offset: 0x000357C1
		// (set) Token: 0x0600084D RID: 2125 RVA: 0x000375D0 File Offset: 0x000357D0
		public float remainingDistance
		{
			get
			{
				return this.totalDistance - this.distance;
			}
			set
			{
				this.distance = this.totalDistance - value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x000375E0 File Offset: 0x000357E0
		// (set) Token: 0x0600084F RID: 2127 RVA: 0x000375E8 File Offset: 0x000357E8
		public float distance
		{
			get
			{
				return this.currentDistance;
			}
			set
			{
				this.currentDistance = value;
				while (this.currentDistance < this.distanceToSegmentStart)
				{
					if (this.segmentIndex <= 0)
					{
						break;
					}
					this.PrevSegment();
				}
				while (this.currentDistance > this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.path.Count - 2)
				{
					this.NextSegment();
				}
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x0003764D File Offset: 0x0003584D
		// (set) Token: 0x06000851 RID: 2129 RVA: 0x00037655 File Offset: 0x00035855
		public int segmentIndex { get; private set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0003765E File Offset: 0x0003585E
		public bool valid
		{
			get
			{
				return this.path != null;
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x0003766C File Offset: 0x0003586C
		public void GetRemainingPath(List<Vector3> buffer)
		{
			if (!this.valid)
			{
				throw new Exception("PathInterpolator is not valid");
			}
			buffer.Add(this.position);
			for (int i = this.segmentIndex + 1; i < this.path.Count; i++)
			{
				buffer.Add(this.path[i]);
			}
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000376C8 File Offset: 0x000358C8
		public void SetPath(List<Vector3> path)
		{
			this.path = path;
			this.currentDistance = 0f;
			this.segmentIndex = 0;
			this.distanceToSegmentStart = 0f;
			if (path == null)
			{
				this.totalDistance = float.PositiveInfinity;
				this.currentSegmentLength = float.PositiveInfinity;
				return;
			}
			if (path.Count < 2)
			{
				throw new ArgumentException("Path must have a length of at least 2");
			}
			this.currentSegmentLength = (path[1] - path[0]).magnitude;
			this.totalDistance = 0f;
			Vector3 vector = path[0];
			for (int i = 1; i < path.Count; i++)
			{
				Vector3 vector2 = path[i];
				this.totalDistance += (vector2 - vector).magnitude;
				vector = vector2;
			}
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00037794 File Offset: 0x00035994
		public void MoveToSegment(int index, float fractionAlongSegment)
		{
			if (this.path == null)
			{
				return;
			}
			if (index < 0 || index >= this.path.Count - 1)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			while (this.segmentIndex > index)
			{
				this.PrevSegment();
			}
			while (this.segmentIndex < index)
			{
				this.NextSegment();
			}
			this.distance = this.distanceToSegmentStart + Mathf.Clamp01(fractionAlongSegment) * this.currentSegmentLength;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x00037804 File Offset: 0x00035A04
		public void MoveToClosestPoint(Vector3 point)
		{
			if (this.path == null)
			{
				return;
			}
			float num = float.PositiveInfinity;
			float num2 = 0f;
			int num3 = 0;
			for (int i = 0; i < this.path.Count - 1; i++)
			{
				float num4 = VectorMath.ClosestPointOnLineFactor(this.path[i], this.path[i + 1], point);
				Vector3 vector = Vector3.Lerp(this.path[i], this.path[i + 1], num4);
				float sqrMagnitude = (point - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num = sqrMagnitude;
					num2 = num4;
					num3 = i;
				}
			}
			this.MoveToSegment(num3, num2);
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x000378B0 File Offset: 0x00035AB0
		public void MoveToLocallyClosestPoint(Vector3 point, bool allowForwards = true, bool allowBackwards = true)
		{
			if (this.path == null)
			{
				return;
			}
			while (allowForwards && this.segmentIndex < this.path.Count - 2)
			{
				if ((this.path[this.segmentIndex + 1] - point).sqrMagnitude > (this.path[this.segmentIndex] - point).sqrMagnitude)
				{
					break;
				}
				this.NextSegment();
			}
			while (allowBackwards && this.segmentIndex > 0 && (this.path[this.segmentIndex - 1] - point).sqrMagnitude <= (this.path[this.segmentIndex] - point).sqrMagnitude)
			{
				this.PrevSegment();
			}
			float num = 0f;
			float num2 = 0f;
			float num3 = float.PositiveInfinity;
			float num4 = float.PositiveInfinity;
			if (this.segmentIndex > 0)
			{
				num = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], point);
				num3 = (Vector3.Lerp(this.path[this.segmentIndex - 1], this.path[this.segmentIndex], num) - point).sqrMagnitude;
			}
			if (this.segmentIndex < this.path.Count - 1)
			{
				num2 = VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], point);
				num4 = (Vector3.Lerp(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], num2) - point).sqrMagnitude;
			}
			if (num3 < num4)
			{
				this.MoveToSegment(this.segmentIndex - 1, num);
				return;
			}
			this.MoveToSegment(this.segmentIndex, num2);
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00037AA4 File Offset: 0x00035CA4
		public void MoveToCircleIntersection2D(Vector3 circleCenter3D, float radius, IMovementPlane transform)
		{
			if (this.path == null)
			{
				return;
			}
			while (this.segmentIndex < this.path.Count - 2 && VectorMath.ClosestPointOnLineFactor(this.path[this.segmentIndex], this.path[this.segmentIndex + 1], circleCenter3D) > 1f)
			{
				this.NextSegment();
			}
			Vector2 vector = transform.ToPlane(circleCenter3D);
			while (this.segmentIndex < this.path.Count - 2 && (transform.ToPlane(this.path[this.segmentIndex + 1]) - vector).sqrMagnitude <= radius * radius)
			{
				this.NextSegment();
			}
			float num = VectorMath.LineCircleIntersectionFactor(vector, transform.ToPlane(this.path[this.segmentIndex]), transform.ToPlane(this.path[this.segmentIndex + 1]), radius);
			this.MoveToSegment(this.segmentIndex, num);
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x00037BAC File Offset: 0x00035DAC
		protected virtual void PrevSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex - 1;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
			this.distanceToSegmentStart -= this.currentSegmentLength;
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x00037C14 File Offset: 0x00035E14
		protected virtual void NextSegment()
		{
			int segmentIndex = this.segmentIndex;
			this.segmentIndex = segmentIndex + 1;
			this.distanceToSegmentStart += this.currentSegmentLength;
			this.currentSegmentLength = (this.path[this.segmentIndex + 1] - this.path[this.segmentIndex]).magnitude;
		}

		// Token: 0x040004D7 RID: 1239
		private List<Vector3> path;

		// Token: 0x040004D8 RID: 1240
		private float distanceToSegmentStart;

		// Token: 0x040004D9 RID: 1241
		private float currentDistance;

		// Token: 0x040004DA RID: 1242
		private float currentSegmentLength = float.PositiveInfinity;

		// Token: 0x040004DB RID: 1243
		private float totalDistance = float.PositiveInfinity;
	}
}
