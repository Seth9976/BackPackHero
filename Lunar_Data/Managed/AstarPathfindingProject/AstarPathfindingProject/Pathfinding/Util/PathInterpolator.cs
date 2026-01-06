using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Util
{
	// Token: 0x0200025C RID: 604
	public class PathInterpolator
	{
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x00058F3B File Offset: 0x0005713B
		public bool valid
		{
			get
			{
				return this.path != null;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x00058F46 File Offset: 0x00057146
		public PathInterpolator.Cursor start
		{
			get
			{
				return PathInterpolator.Cursor.StartOfPath(this);
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00058F50 File Offset: 0x00057150
		public PathInterpolator.Cursor AtDistanceFromStart(float distance)
		{
			PathInterpolator.Cursor start = this.start;
			start.distance = distance;
			return start;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00058F70 File Offset: 0x00057170
		public void SetPath(List<Vector3> path)
		{
			this.version++;
			if (this.path == null)
			{
				this.path = new List<Vector3>();
			}
			this.path.Clear();
			if (path == null)
			{
				this.totalDistance = float.PositiveInfinity;
				return;
			}
			if (path.Count < 2)
			{
				throw new ArgumentException("Path must have a length of at least 2");
			}
			Vector3 vector = path[0];
			this.totalDistance = 0f;
			this.path.Capacity = Mathf.Max(this.path.Capacity, path.Count);
			this.path.Add(path[0]);
			for (int i = 1; i < path.Count; i++)
			{
				Vector3 vector2 = path[i];
				if (vector2 != vector)
				{
					this.totalDistance += (vector2 - vector).magnitude;
					this.path.Add(vector2);
					vector = vector2;
				}
			}
			if (this.path.Count < 2)
			{
				this.path.Add(path[0]);
			}
			if (float.IsNaN(this.totalDistance))
			{
				throw new ArgumentException("Path contains NaN values");
			}
		}

		// Token: 0x04000AE5 RID: 2789
		private List<Vector3> path;

		// Token: 0x04000AE6 RID: 2790
		private int version = 1;

		// Token: 0x04000AE7 RID: 2791
		private float totalDistance;

		// Token: 0x0200025D RID: 605
		public struct Cursor
		{
			// Token: 0x170001F2 RID: 498
			// (get) Token: 0x06000E4C RID: 3660 RVA: 0x000590A5 File Offset: 0x000572A5
			// (set) Token: 0x06000E4D RID: 3661 RVA: 0x000590AD File Offset: 0x000572AD
			private int segmentIndex { readonly get; set; }

			// Token: 0x170001F3 RID: 499
			// (get) Token: 0x06000E4E RID: 3662 RVA: 0x000590B6 File Offset: 0x000572B6
			public int segmentCount
			{
				get
				{
					this.AssertValid();
					return this.interpolator.path.Count - 1;
				}
			}

			// Token: 0x170001F4 RID: 500
			// (get) Token: 0x06000E4F RID: 3663 RVA: 0x000590D0 File Offset: 0x000572D0
			public Vector3 endPoint
			{
				get
				{
					this.AssertValid();
					return this.interpolator.path[this.interpolator.path.Count - 1];
				}
			}

			// Token: 0x170001F5 RID: 501
			// (get) Token: 0x06000E50 RID: 3664 RVA: 0x000590FA File Offset: 0x000572FA
			// (set) Token: 0x06000E51 RID: 3665 RVA: 0x00059123 File Offset: 0x00057323
			public float fractionAlongCurrentSegment
			{
				get
				{
					if (this.currentSegmentLength <= 0f)
					{
						return 1f;
					}
					return (this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength;
				}
				set
				{
					this.currentDistance = this.distanceToSegmentStart + Mathf.Clamp01(value) * this.currentSegmentLength;
				}
			}

			// Token: 0x06000E52 RID: 3666 RVA: 0x00059140 File Offset: 0x00057340
			public static PathInterpolator.Cursor StartOfPath(PathInterpolator interpolator)
			{
				if (!interpolator.valid)
				{
					throw new InvalidOperationException("PathInterpolator has no path set");
				}
				return new PathInterpolator.Cursor
				{
					interpolator = interpolator,
					version = interpolator.version,
					segmentIndex = 0,
					currentDistance = 0f,
					distanceToSegmentStart = 0f,
					currentSegmentLength = (interpolator.path[1] - interpolator.path[0]).magnitude
				};
			}

			// Token: 0x170001F6 RID: 502
			// (get) Token: 0x06000E53 RID: 3667 RVA: 0x000591CA File Offset: 0x000573CA
			public bool valid
			{
				get
				{
					return this.interpolator != null && this.interpolator.version == this.version;
				}
			}

			// Token: 0x170001F7 RID: 503
			// (get) Token: 0x06000E54 RID: 3668 RVA: 0x000591E9 File Offset: 0x000573E9
			public Vector3 tangent
			{
				get
				{
					this.AssertValid();
					return this.interpolator.path[this.segmentIndex + 1] - this.interpolator.path[this.segmentIndex];
				}
			}

			// Token: 0x170001F8 RID: 504
			// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00059224 File Offset: 0x00057424
			// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0005923E File Offset: 0x0005743E
			public float remainingDistance
			{
				get
				{
					this.AssertValid();
					return this.interpolator.totalDistance - this.distance;
				}
				set
				{
					this.AssertValid();
					this.distance = this.interpolator.totalDistance - value;
				}
			}

			// Token: 0x170001F9 RID: 505
			// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00059259 File Offset: 0x00057459
			// (set) Token: 0x06000E58 RID: 3672 RVA: 0x00059264 File Offset: 0x00057464
			public float distance
			{
				get
				{
					return this.currentDistance;
				}
				set
				{
					this.AssertValid();
					this.currentDistance = value;
					while (this.currentDistance < this.distanceToSegmentStart)
					{
						if (this.segmentIndex <= 0)
						{
							break;
						}
						this.PrevSegment();
					}
					while (this.currentDistance > this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.interpolator.path.Count - 2)
					{
						this.NextSegment();
					}
				}
			}

			// Token: 0x170001FA RID: 506
			// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000592D4 File Offset: 0x000574D4
			public Vector3 position
			{
				get
				{
					this.AssertValid();
					float num = ((this.currentSegmentLength > 0.0001f) ? ((this.currentDistance - this.distanceToSegmentStart) / this.currentSegmentLength) : 0f);
					return Vector3.Lerp(this.interpolator.path[this.segmentIndex], this.interpolator.path[this.segmentIndex + 1], num);
				}
			}

			// Token: 0x06000E5A RID: 3674 RVA: 0x00059344 File Offset: 0x00057544
			public void GetRemainingPath(List<Vector3> buffer)
			{
				this.AssertValid();
				buffer.Add(this.position);
				for (int i = this.segmentIndex + 1; i < this.interpolator.path.Count; i++)
				{
					buffer.Add(this.interpolator.path[i]);
				}
			}

			// Token: 0x06000E5B RID: 3675 RVA: 0x0005939C File Offset: 0x0005759C
			private void AssertValid()
			{
				if (!this.valid)
				{
					throw new InvalidOperationException("The cursor has been invalidated because SetPath has been called on the interpolator. Please create a new cursor.");
				}
			}

			// Token: 0x06000E5C RID: 3676 RVA: 0x000593B4 File Offset: 0x000575B4
			public void GetTangents(out Vector3 t1, out Vector3 t2)
			{
				this.AssertValid();
				bool flag = this.currentDistance <= this.distanceToSegmentStart + 0.001f;
				bool flag2 = this.currentDistance >= this.distanceToSegmentStart + this.currentSegmentLength - 0.001f;
				if (flag || flag2)
				{
					int num;
					int num2;
					if (flag)
					{
						num = ((this.segmentIndex > 0) ? (this.segmentIndex - 1) : this.segmentIndex);
						num2 = this.segmentIndex;
					}
					else
					{
						num = this.segmentIndex;
						num2 = ((this.segmentIndex < this.interpolator.path.Count - 2) ? (this.segmentIndex + 1) : this.segmentIndex);
					}
					t1 = this.interpolator.path[num + 1] - this.interpolator.path[num];
					t2 = this.interpolator.path[num2 + 1] - this.interpolator.path[num2];
					return;
				}
				t1 = this.tangent;
				t2 = t1;
			}

			// Token: 0x170001FB RID: 507
			// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000594D8 File Offset: 0x000576D8
			public Vector3 curvatureDirection
			{
				get
				{
					Vector3 vector;
					Vector3 vector2;
					this.GetTangents(out vector, out vector2);
					Vector3 vector3 = Vector3.Cross(vector, vector2);
					if (vector3.sqrMagnitude > 1E-06f)
					{
						return vector3;
					}
					return Vector3.zero;
				}
			}

			// Token: 0x06000E5E RID: 3678 RVA: 0x0005950C File Offset: 0x0005770C
			public void MoveToNextCorner()
			{
				this.AssertValid();
				List<Vector3> path = this.interpolator.path;
				while (this.currentDistance >= this.distanceToSegmentStart + this.currentSegmentLength)
				{
					if (this.segmentIndex >= path.Count - 2)
					{
						break;
					}
					this.NextSegment();
				}
				while (this.segmentIndex < path.Count - 2 && VectorMath.IsColinear(path[this.segmentIndex], path[this.segmentIndex + 1], path[this.segmentIndex + 2]))
				{
					this.NextSegment();
				}
				this.currentDistance = this.distanceToSegmentStart + this.currentSegmentLength;
			}

			// Token: 0x06000E5F RID: 3679 RVA: 0x000595B4 File Offset: 0x000577B4
			public bool MoveToClosestIntersectionWithLineSegment(Vector3 origin, Vector3 direction, Vector2 range)
			{
				this.AssertValid();
				float num = float.PositiveInfinity;
				float num2 = float.PositiveInfinity;
				float num3 = 0f;
				for (int i = 0; i < this.interpolator.path.Count - 1; i++)
				{
					Vector3 vector = this.interpolator.path[i];
					Vector3 vector2 = this.interpolator.path[i + 1];
					float magnitude = (vector2 - vector).magnitude;
					float num4;
					float num5;
					if (VectorMath.LineLineIntersectionFactors(vector.xz, (vector2 - vector).xz, origin.xz, direction.xz, out num4, out num5) && num4 >= 0f && num4 <= 1f && num5 >= range.x && num5 <= range.y)
					{
						float num6 = num3 + num4 * magnitude;
						float num7 = Mathf.Abs(num6 - this.currentDistance);
						if (num7 < num2)
						{
							num = num6;
							num2 = num7;
						}
					}
					num3 += magnitude;
				}
				if (num2 != float.PositiveInfinity)
				{
					this.distance = num;
					return true;
				}
				return false;
			}

			// Token: 0x06000E60 RID: 3680 RVA: 0x000596EC File Offset: 0x000578EC
			private void MoveToSegment(int index, float fractionAlongSegment)
			{
				this.AssertValid();
				if (index < 0 || index >= this.interpolator.path.Count - 1)
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
				this.currentDistance = this.distanceToSegmentStart + Mathf.Clamp01(fractionAlongSegment) * this.currentSegmentLength;
			}

			// Token: 0x06000E61 RID: 3681 RVA: 0x00059760 File Offset: 0x00057960
			public void MoveToClosestPoint(Vector3 point)
			{
				this.AssertValid();
				float num = float.PositiveInfinity;
				float num2 = 0f;
				int num3 = 0;
				List<Vector3> path = this.interpolator.path;
				for (int i = 0; i < path.Count - 1; i++)
				{
					float num4 = VectorMath.ClosestPointOnLineFactor(path[i], path[i + 1], point);
					Vector3 vector = Vector3.Lerp(path[i], path[i + 1], num4);
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

			// Token: 0x06000E62 RID: 3682 RVA: 0x00059804 File Offset: 0x00057A04
			public void MoveToLocallyClosestPoint(Vector3 point, bool allowForwards = true, bool allowBackwards = true)
			{
				this.AssertValid();
				List<Vector3> path = this.interpolator.path;
				this.segmentIndex = Mathf.Min(this.segmentIndex, path.Count - 2);
				float num;
				for (;;)
				{
					int segmentIndex = this.segmentIndex;
					num = VectorMath.ClosestPointOnLineFactor(path[segmentIndex], path[segmentIndex + 1], point);
					if (num > 1f && allowForwards && this.segmentIndex < path.Count - 2)
					{
						this.NextSegment();
						allowBackwards = false;
					}
					else
					{
						if (num >= 0f || !allowBackwards || this.segmentIndex <= 0)
						{
							break;
						}
						this.PrevSegment();
						allowForwards = false;
					}
				}
				if (num > 0.5f && this.segmentIndex < path.Count - 2)
				{
					this.NextSegment();
				}
				float num2 = 0f;
				float num3 = float.PositiveInfinity;
				if (this.segmentIndex > 0)
				{
					int num4 = this.segmentIndex - 1;
					num2 = VectorMath.ClosestPointOnLineFactor(path[num4], path[num4 + 1], point);
					num3 = (Vector3.Lerp(path[num4], path[num4 + 1], num2) - point).sqrMagnitude;
				}
				float num5 = VectorMath.ClosestPointOnLineFactor(path[this.segmentIndex], path[this.segmentIndex + 1], point);
				float sqrMagnitude = (Vector3.Lerp(path[this.segmentIndex], path[this.segmentIndex + 1], num5) - point).sqrMagnitude;
				if (num3 < sqrMagnitude)
				{
					this.MoveToSegment(this.segmentIndex - 1, num2);
					return;
				}
				this.MoveToSegment(this.segmentIndex, num5);
			}

			// Token: 0x06000E63 RID: 3683 RVA: 0x0005999C File Offset: 0x00057B9C
			public void MoveToCircleIntersection2D<T>(Vector3 circleCenter3D, float radius, T transform) where T : IMovementPlane
			{
				this.AssertValid();
				List<Vector3> path = this.interpolator.path;
				while (this.segmentIndex < path.Count - 2 && VectorMath.ClosestPointOnLineFactor(path[this.segmentIndex], path[this.segmentIndex + 1], circleCenter3D) > 1f)
				{
					this.NextSegment();
				}
				Vector2 vector = transform.ToPlane(circleCenter3D);
				while (this.segmentIndex < path.Count - 2 && (transform.ToPlane(path[this.segmentIndex + 1]) - vector).sqrMagnitude <= radius * radius)
				{
					this.NextSegment();
				}
				float num = VectorMath.LineCircleIntersectionFactor(vector, transform.ToPlane(path[this.segmentIndex]), transform.ToPlane(path[this.segmentIndex + 1]), radius);
				this.MoveToSegment(this.segmentIndex, num);
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00059AA8 File Offset: 0x00057CA8
			private static float IntegrateSmoothingKernel(float a, float b, float smoothingDistance)
			{
				if (smoothingDistance <= 0f)
				{
					return (float)((a <= 0f && b > 0f) ? 1 : 0);
				}
				float num = ((a < 0f) ? Mathf.Exp(a / smoothingDistance) : (2f - Mathf.Exp(-a / smoothingDistance)));
				float num2 = ((b < 0f) ? Mathf.Exp(b / smoothingDistance) : (2f - Mathf.Exp(-b / smoothingDistance)));
				return 0.5f * (num2 - num);
			}

			// Token: 0x06000E65 RID: 3685 RVA: 0x00059B20 File Offset: 0x00057D20
			private static float IntegrateSmoothingKernel2(float a, float b, float smoothingDistance)
			{
				if (smoothingDistance <= 0f)
				{
					return 0f;
				}
				float num = -Mathf.Exp(-a / smoothingDistance) * smoothingDistance;
				float num2 = -Mathf.Exp(-b / smoothingDistance) * (smoothingDistance + b - a);
				return 0.5f * (num2 - num);
			}

			// Token: 0x06000E66 RID: 3686 RVA: 0x00059B64 File Offset: 0x00057D64
			private static Vector3 IntegrateSmoothTangent(Vector3 p1, Vector3 p2, ref Vector3 tangent, ref float distance, float expectedRadius, float smoothingDistance)
			{
				Vector3 vector = p2 - p1;
				float magnitude = vector.magnitude;
				if (magnitude <= 1E-05f)
				{
					return Vector3.zero;
				}
				Vector3 vector2 = vector * (1f / magnitude);
				float num = Vector3.Angle(tangent, vector2) * 0.017453292f;
				float num2 = expectedRadius * Mathf.Abs(num);
				Vector3 vector3 = Vector3.zero;
				if (num2 > 1E-45f)
				{
					Vector3 vector4 = tangent * PathInterpolator.Cursor.IntegrateSmoothingKernel(distance, distance + num2, smoothingDistance) + (vector2 - tangent) * PathInterpolator.Cursor.IntegrateSmoothingKernel2(distance, distance + num2, smoothingDistance) / num2;
					vector3 += vector4;
					distance += num2;
				}
				vector3 += vector2 * PathInterpolator.Cursor.IntegrateSmoothingKernel(distance, distance + magnitude, smoothingDistance);
				tangent = vector2;
				distance += magnitude;
				return vector3;
			}

			// Token: 0x06000E67 RID: 3687 RVA: 0x00059C50 File Offset: 0x00057E50
			public Vector3 EstimateSmoothTangent(Vector3 normalizedTangent, float smoothingDistance, float expectedRadius, Vector3 beforePathStartContribution, bool forward = true, bool backward = true)
			{
				this.AssertValid();
				if (expectedRadius <= 1E-45f || smoothingDistance <= 0f)
				{
					return normalizedTangent;
				}
				List<Vector3> path = this.interpolator.path;
				Vector3 vector = Vector3.zero;
				while (this.currentDistance >= this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.interpolator.path.Count - 2)
				{
					this.NextSegment();
				}
				if (forward)
				{
					float num = 0f;
					Vector3 vector2 = this.position;
					Vector3 vector3 = normalizedTangent;
					for (int i = this.segmentIndex + 1; i < path.Count; i++)
					{
						vector += PathInterpolator.Cursor.IntegrateSmoothTangent(vector2, path[i], ref vector3, ref num, expectedRadius, smoothingDistance);
						vector2 = path[i];
					}
				}
				if (backward)
				{
					float num2 = 0f;
					Vector3 vector4 = -normalizedTangent;
					Vector3 vector5 = this.position;
					for (int j = this.segmentIndex; j >= 0; j--)
					{
						vector -= PathInterpolator.Cursor.IntegrateSmoothTangent(vector5, path[j], ref vector4, ref num2, expectedRadius, smoothingDistance);
						vector5 = path[j];
					}
					vector += beforePathStartContribution * PathInterpolator.Cursor.IntegrateSmoothingKernel(float.NegativeInfinity, -this.currentDistance, smoothingDistance);
				}
				return vector;
			}

			// Token: 0x06000E68 RID: 3688 RVA: 0x00059D8C File Offset: 0x00057F8C
			public Vector3 EstimateSmoothCurvature(Vector3 tangent, float smoothingDistance, float expectedRadius)
			{
				this.AssertValid();
				if (expectedRadius <= 1E-45f)
				{
					return Vector3.zero;
				}
				List<Vector3> path = this.interpolator.path;
				tangent = tangent.normalized;
				Vector3 vector = Vector3.zero;
				while (this.currentDistance >= this.distanceToSegmentStart + this.currentSegmentLength && this.segmentIndex < this.interpolator.path.Count - 2)
				{
					this.NextSegment();
				}
				float num = 0f;
				Vector3 vector2 = this.position;
				Vector3 vector3 = tangent.normalized;
				for (int i = this.segmentIndex + 1; i < path.Count; i++)
				{
					Vector3 vector4 = path[i] - vector2;
					Vector3 normalized = vector4.normalized;
					float num2 = Vector3.Angle(vector3, normalized) * 0.017453292f;
					Vector3 normalized2 = Vector3.Cross(vector3, normalized).normalized;
					float num3 = 1f / expectedRadius;
					float num4 = expectedRadius * Mathf.Abs(num2);
					float num5 = num3 * PathInterpolator.Cursor.IntegrateSmoothingKernel(num, num + num4, smoothingDistance);
					vector -= num5 * normalized2;
					vector3 = normalized;
					num += num4;
					num += vector4.magnitude;
					vector2 = path[i];
				}
				num = float.Epsilon;
				vector3 = -tangent.normalized;
				vector2 = this.position;
				for (int j = this.segmentIndex; j >= 0; j--)
				{
					Vector3 vector5 = path[j] - vector2;
					if (!(vector5 == Vector3.zero))
					{
						Vector3 normalized3 = vector5.normalized;
						float num6 = Vector3.Angle(vector3, normalized3) * 0.017453292f;
						Vector3 normalized4 = Vector3.Cross(vector3, normalized3).normalized;
						float num7 = 1f / expectedRadius;
						float num8 = expectedRadius * Mathf.Abs(num6);
						float num9 = num7 * PathInterpolator.Cursor.IntegrateSmoothingKernel(num, num + num8, smoothingDistance);
						vector += num9 * normalized4;
						vector3 = normalized3;
						num += num8;
						num += vector5.magnitude;
						vector2 = path[j];
					}
				}
				return vector;
			}

			// Token: 0x06000E69 RID: 3689 RVA: 0x00059F90 File Offset: 0x00058190
			public void MoveWithTurningSpeed(float time, float speed, float turningSpeed, ref Vector3 tangent)
			{
				if (turningSpeed <= 0f)
				{
					throw new ArgumentException("turningSpeed must be greater than zero");
				}
				if (speed <= 0f)
				{
					throw new ArgumentException("speed must be greater than zero");
				}
				this.AssertValid();
				float num = speed / turningSpeed;
				float num2 = time * speed;
				int num3 = 0;
				while (num2 > 0f && this.currentDistance >= this.distanceToSegmentStart + this.currentSegmentLength)
				{
					if (this.segmentIndex >= this.interpolator.path.Count - 2)
					{
						break;
					}
					this.NextSegment();
				}
				while (num2 < 0f && this.currentDistance <= this.distanceToSegmentStart)
				{
					if (this.segmentIndex <= 0)
					{
						break;
					}
					this.PrevSegment();
				}
				while (num2 != 0f)
				{
					num3++;
					if (num3 > 100)
					{
						throw new Exception("Infinite Loop " + num2.ToString() + " " + time.ToString());
					}
					Vector3 tangent2 = this.tangent;
					if (tangent != tangent2 && this.currentSegmentLength > 0f)
					{
						float num4 = Vector3.Angle(tangent, tangent2) * 0.017453292f * num;
						if (Mathf.Abs(num2) <= num4)
						{
							tangent = Vector3.Slerp(tangent, tangent2, Mathf.Abs(num2) / num4);
							return;
						}
						num2 -= num4 * Mathf.Sign(num2);
						tangent = tangent2;
					}
					if (num2 > 0f)
					{
						float num5 = this.currentSegmentLength - (this.currentDistance - this.distanceToSegmentStart);
						if (num2 < num5)
						{
							this.currentDistance += num2;
							return;
						}
						num2 -= num5;
						if (this.segmentIndex + 1 >= this.interpolator.path.Count - 1)
						{
							this.MoveToSegment(this.segmentIndex, 1f);
							return;
						}
						this.MoveToSegment(this.segmentIndex + 1, 0f);
					}
					else
					{
						float num6 = this.currentDistance - this.distanceToSegmentStart;
						if (-num2 <= num6)
						{
							this.currentDistance += num2;
							return;
						}
						num2 += num6;
						if (this.segmentIndex - 1 < 0)
						{
							this.MoveToSegment(this.segmentIndex, 0f);
							return;
						}
						this.MoveToSegment(this.segmentIndex - 1, 1f);
					}
				}
			}

			// Token: 0x06000E6A RID: 3690 RVA: 0x0005A1C8 File Offset: 0x000583C8
			private void PrevSegment()
			{
				int segmentIndex = this.segmentIndex;
				this.segmentIndex = segmentIndex - 1;
				this.currentSegmentLength = (this.interpolator.path[this.segmentIndex + 1] - this.interpolator.path[this.segmentIndex]).magnitude;
				this.distanceToSegmentStart -= this.currentSegmentLength;
			}

			// Token: 0x06000E6B RID: 3691 RVA: 0x0005A23C File Offset: 0x0005843C
			private void NextSegment()
			{
				int segmentIndex = this.segmentIndex;
				this.segmentIndex = segmentIndex + 1;
				this.distanceToSegmentStart += this.currentSegmentLength;
				this.currentSegmentLength = (this.interpolator.path[this.segmentIndex + 1] - this.interpolator.path[this.segmentIndex]).magnitude;
			}

			// Token: 0x04000AE8 RID: 2792
			private PathInterpolator interpolator;

			// Token: 0x04000AE9 RID: 2793
			private int version;

			// Token: 0x04000AEA RID: 2794
			private float currentDistance;

			// Token: 0x04000AEB RID: 2795
			private float distanceToSegmentStart;

			// Token: 0x04000AEC RID: 2796
			private float currentSegmentLength;
		}
	}
}
