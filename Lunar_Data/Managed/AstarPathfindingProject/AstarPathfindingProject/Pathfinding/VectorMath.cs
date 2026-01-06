using System;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200004B RID: 75
	public static class VectorMath
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000B697 File Offset: 0x00009897
		public static Vector2 ComplexMultiply(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000B6D4 File Offset: 0x000098D4
		public static float2 ComplexMultiply(float2 a, float2 b)
		{
			return a.x * b + a.y * new float2(-b.y, b.x);
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000B704 File Offset: 0x00009904
		public static float2 ComplexMultiplyConjugate(float2 a, float2 b)
		{
			return new float2(a.x * b.x + a.y * b.y, a.y * b.x - a.x * b.y);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000B741 File Offset: 0x00009941
		public static Vector2 ComplexMultiplyConjugate(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x + a.y * b.y, a.y * b.x - a.x * b.y);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000B780 File Offset: 0x00009980
		public static Vector3 ClosestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
			float num = Vector3.Dot(point - lineStart, vector);
			return lineStart + num * vector;
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000B7B8 File Offset: 0x000099B8
		public static float ClosestPointOnLineFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = lineEnd - lineStart;
			float sqrMagnitude = vector.sqrMagnitude;
			if (sqrMagnitude <= 1E-06f)
			{
				return 0f;
			}
			return Vector3.Dot(point - lineStart, vector) / sqrMagnitude;
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000B7F4 File Offset: 0x000099F4
		public static float ClosestPointOnLineFactor(float3 lineStart, float3 lineEnd, float3 point)
		{
			float3 @float = lineEnd - lineStart;
			float num = math.dot(@float, @float);
			return math.select(0f, math.dot(point - lineStart, @float) / num, num > 1E-06f);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x0000B834 File Offset: 0x00009A34
		public static float ClosestPointOnLineFactor(Int3 lineStart, Int3 lineEnd, Int3 point)
		{
			Int3 @int = lineEnd - lineStart;
			float sqrMagnitude = @int.sqrMagnitude;
			float num = (float)Int3.DotLong(point - lineStart, @int);
			if (sqrMagnitude != 0f)
			{
				num /= sqrMagnitude;
			}
			return num;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000B870 File Offset: 0x00009A70
		public static float ClosestPointOnLineFactor(Int2 lineStart, Int2 lineEnd, Int2 point)
		{
			Int2 @int = lineEnd - lineStart;
			double num = (double)@int.sqrMagnitudeLong;
			double num2 = (double)Int2.DotLong(point - lineStart, @int);
			if (num != 0.0)
			{
				num2 /= num;
			}
			return (float)num2;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		public static Vector3 ClosestPointOnSegment(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = lineEnd - lineStart;
			float sqrMagnitude = vector.sqrMagnitude;
			if ((double)sqrMagnitude <= 1E-06)
			{
				return lineStart;
			}
			float num = Vector3.Dot(point - lineStart, vector) / sqrMagnitude;
			return lineStart + Mathf.Clamp01(num) * vector;
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000B900 File Offset: 0x00009B00
		public static Vector3 ClosestPointOnSegmentXZ(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			lineStart.y = point.y;
			lineEnd.y = point.y;
			Vector3 vector = lineEnd - lineStart;
			vector.y = 0f;
			float magnitude = vector.magnitude;
			Vector3 vector2 = ((magnitude > float.Epsilon) ? (vector / magnitude) : Vector3.zero);
			float num = Vector3.Dot(point - lineStart, vector2);
			return lineStart + Mathf.Clamp(num, 0f, vector.magnitude) * vector2;
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000B988 File Offset: 0x00009B88
		public static float SqrDistancePointSegmentApproximate(int x, int z, int px, int pz, int qx, int qz)
		{
			float num = (float)(qx - px);
			float num2 = (float)(qz - pz);
			float num3 = (float)(x - px);
			float num4 = (float)(z - pz);
			float num5 = num * num + num2 * num2;
			float num6 = num * num3 + num2 * num4;
			if (num5 > 0f)
			{
				num6 /= num5;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			num3 = (float)px + num6 * num - (float)x;
			num4 = (float)pz + num6 * num2 - (float)z;
			return num3 * num3 + num4 * num4;
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public static float SqrDistancePointSegmentApproximate(Int3 a, Int3 b, Int3 p)
		{
			float num = (float)(b.x - a.x);
			float num2 = (float)(b.z - a.z);
			float num3 = (float)(p.x - a.x);
			float num4 = (float)(p.z - a.z);
			float num5 = num * num + num2 * num2;
			float num6 = num * num3 + num2 * num4;
			if (num5 > 0f)
			{
				num6 /= num5;
			}
			if (num6 < 0f)
			{
				num6 = 0f;
			}
			else if (num6 > 1f)
			{
				num6 = 1f;
			}
			num3 = (float)a.x + num6 * num - (float)p.x;
			num4 = (float)a.z + num6 * num2 - (float)p.z;
			return num3 * num3 + num4 * num4;
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000BACC File Offset: 0x00009CCC
		public static float SqrDistancePointSegment(Vector3 a, Vector3 b, Vector3 p)
		{
			return (VectorMath.ClosestPointOnSegment(a, b, p) - p).sqrMagnitude;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000BAF0 File Offset: 0x00009CF0
		public static float SqrDistanceSegmentSegment(Vector3 s1, Vector3 e1, Vector3 s2, Vector3 e2)
		{
			Vector3 vector = e1 - s1;
			Vector3 vector2 = e2 - s2;
			Vector3 vector3 = s1 - s2;
			double num = (double)Vector3.Dot(vector, vector);
			double num2 = (double)Vector3.Dot(vector, vector2);
			double num3 = (double)Vector3.Dot(vector2, vector2);
			double num4 = (double)Vector3.Dot(vector, vector3);
			double num5 = (double)Vector3.Dot(vector2, vector3);
			double num7;
			double num6;
			double num8;
			double num9;
			if ((num6 = (num7 = num * num3 - num2 * num2)) < 1E-06 * num * num3)
			{
				num8 = 0.0;
				num7 = 1.0;
				num9 = num5;
				num6 = num3;
			}
			else
			{
				num8 = num2 * num5 - num3 * num4;
				num9 = num * num5 - num2 * num4;
				if (num8 < 0.0)
				{
					num8 = 0.0;
					num9 = num5;
					num6 = num3;
				}
				else if (num8 > num7)
				{
					num8 = num7;
					num9 = num5 + num2;
					num6 = num3;
				}
			}
			if (num9 < 0.0)
			{
				num9 = 0.0;
				if (-num4 < 0.0)
				{
					num8 = 0.0;
				}
				else if (-num4 > num)
				{
					num8 = num7;
				}
				else
				{
					num8 = -num4;
					num7 = num;
				}
			}
			else if (num9 > num6)
			{
				num9 = num6;
				if (-num4 + num2 < 0.0)
				{
					num8 = 0.0;
				}
				else if (-num4 + num2 > num)
				{
					num8 = num7;
				}
				else
				{
					num8 = -num4 + num2;
					num7 = num;
				}
			}
			double num10 = ((Math.Abs(num8) < 9.999999747378752E-06) ? 0.0 : (num8 / num7));
			double num11 = ((Math.Abs(num9) < 9.999999747378752E-06) ? 0.0 : (num9 / num6));
			return (vector3 + (float)num10 * vector - (float)num11 * vector2).sqrMagnitude;
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000BCCF File Offset: 0x00009ECF
		public static float Determinant(float2 c1, float2 c2)
		{
			return c1.x * c2.y - c1.y * c2.x;
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public static float SqrDistanceXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return vector.x * vector.x + vector.z * vector.z;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000BD1C File Offset: 0x00009F1C
		public static long SignedTriangleAreaTimes2XZ(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000BD59 File Offset: 0x00009F59
		public static float SignedTriangleAreaTimes2XZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000BD92 File Offset: 0x00009F92
		public static bool RightXZ(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) < -1E-45f;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000BDD4 File Offset: 0x00009FD4
		public static bool RightXZ(Int3 a, Int3 b, Int3 p)
		{
			return (long)(b.x - a.x) * (long)(p.z - a.z) - (long)(p.x - a.x) * (long)(b.z - a.z) < 0L;
		}

		// Token: 0x0600028D RID: 653 RVA: 0x0000BE20 File Offset: 0x0000A020
		public static Side SideXZ(Int3 a, Int3 b, Int3 p)
		{
			long num = (long)(b.x - a.x) * (long)(p.z - a.z) - (long)(p.x - a.x) * (long)(b.z - a.z);
			if (num > 0L)
			{
				return Side.Left;
			}
			if (num >= 0L)
			{
				return Side.Colinear;
			}
			return Side.Right;
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000BE78 File Offset: 0x0000A078
		public static bool RightOrColinear(Vector2 a, Vector2 b, Vector2 p)
		{
			return (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y) <= 0f;
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000BEC8 File Offset: 0x0000A0C8
		public static bool RightOrColinear(Int2 a, Int2 b, Int2 p)
		{
			return (long)(b.x - a.x) * (long)(p.y - a.y) - (long)(p.x - a.x) * (long)(b.y - a.y) <= 0L;
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000BF18 File Offset: 0x0000A118
		public static bool RightOrColinearXZ(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) <= 0f;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000BF68 File Offset: 0x0000A168
		public static bool RightOrColinearXZ(Int3 a, Int3 b, Int3 p)
		{
			return (long)(b.x - a.x) * (long)(p.z - a.z) - (long)(p.x - a.x) * (long)(b.z - a.z) <= 0L;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000BFB8 File Offset: 0x0000A1B8
		public static bool IsClockwiseMarginXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) <= float.Epsilon;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000C006 File Offset: 0x0000A206
		public static bool IsClockwiseXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) < 0f;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C046 File Offset: 0x0000A246
		public static bool IsClockwiseXZ(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.RightXZ(a, b, c);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C050 File Offset: 0x0000A250
		public static bool IsClockwiseOrColinearXZ(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.RightOrColinearXZ(a, b, c);
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C05A File Offset: 0x0000A25A
		public static bool IsClockwiseOrColinear(Int2 a, Int2 b, Int2 c)
		{
			return VectorMath.RightOrColinear(a, b, c);
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C064 File Offset: 0x0000A264
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			float num = vector.y * vector2.z - vector.z * vector2.y;
			float num2 = vector.z * vector2.x - vector.x * vector2.z;
			float num3 = vector.x * vector2.y - vector.y * vector2.x;
			float num4 = num * num + num2 * num2 + num3 * num3;
			float num5 = vector.sqrMagnitude * vector2.sqrMagnitude;
			return num4 <= math.sqrt(num5) * 0.0001f || num5 == 0f;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000C10C File Offset: 0x0000A30C
		public static bool IsColinear(Vector2 a, Vector2 b, Vector2 c)
		{
			float num = (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);
			return num <= 0.0001f && num >= -0.0001f;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000C168 File Offset: 0x0000A368
		public static bool IsColinearXZ(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) == 0L;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C1B4 File Offset: 0x0000A3B4
		public static bool IsColinearXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			float num = (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
			return num <= 1E-07f && num >= -1E-07f;
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C210 File Offset: 0x0000A410
		public static bool IsColinearAlmostXZ(Int3 a, Int3 b, Int3 c)
		{
			long num = (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
			return num > -1L && num < 1L;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C265 File Offset: 0x0000A465
		public static bool SegmentsIntersect(Int2 start1, Int2 end1, Int2 start2, Int2 end2)
		{
			return VectorMath.RightOrColinear(start1, end1, start2) != VectorMath.RightOrColinear(start1, end1, end2) && VectorMath.RightOrColinear(start2, end2, start1) != VectorMath.RightOrColinear(start2, end2, end1);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000C290 File Offset: 0x0000A490
		public static bool SegmentsIntersectXZ(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.RightOrColinearXZ(start1, end1, start2) != VectorMath.RightOrColinearXZ(start1, end1, end2) && VectorMath.RightOrColinearXZ(start2, end2, start1) != VectorMath.RightOrColinearXZ(start2, end2, end1);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public static bool SegmentsIntersectXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			return num4 >= 0f && num4 <= 1f && num5 >= 0f && num5 <= 1f;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000C384 File Offset: 0x0000A584
		public static float2 CapsuleLineIntersectionFactors(float2 capsuleStart, float2 capsuleDir, float capsuleLength, float2 lineStart, float2 lineDir, float radius)
		{
			float num = math.dot(capsuleDir, lineDir);
			float num2 = math.sqrt(1f - num * num);
			float num3 = float.PositiveInfinity;
			float num4 = float.NegativeInfinity;
			float num5;
			float num6;
			if (VectorMath.LineCircleIntersectionFactors(lineStart - capsuleStart, lineDir, radius, out num5, out num6))
			{
				num3 = math.min(num3, num5);
				num4 = math.max(num4, num6);
			}
			float num7;
			float num8;
			if (VectorMath.LineCircleIntersectionFactors(lineStart - (capsuleStart + capsuleDir * capsuleLength), lineDir, radius, out num7, out num8))
			{
				num3 = math.min(num3, num7);
				num4 = math.max(num4, num8);
			}
			float num9;
			if (VectorMath.LineLineIntersectionFactor(capsuleStart, capsuleDir, lineStart, lineDir, out num9))
			{
				float2 @float = new float2(-capsuleDir.y, capsuleDir.x);
				float num10 = radius * num / num2;
				float num11 = math.sign(capsuleDir.y * lineDir.x - capsuleDir.x * lineDir.y);
				float num12 = num9 + num10 * num11;
				float num13 = num9 - num10 * num11;
				if (num12 >= 0f && num12 <= capsuleLength)
				{
					float num14 = math.dot(capsuleStart + capsuleDir * num12 - @float * radius - lineStart, lineDir);
					num3 = math.min(num3, num14);
					num4 = math.max(num4, num14);
				}
				if (num13 >= 0f && num13 <= capsuleLength)
				{
					float num15 = math.dot(capsuleStart + capsuleDir * num13 + @float * radius - lineStart, lineDir);
					num3 = math.min(num3, num15);
					num4 = math.max(num4, num15);
				}
			}
			return new float2(num3, num4);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C514 File Offset: 0x0000A714
		public static bool LineLineIntersectionFactor(float2 start1, float2 dir1, float2 start2, float2 dir2, out float t)
		{
			float num = dir2.y * dir1.x - dir2.x * dir1.y;
			if (math.abs(num) < 0.0001f)
			{
				t = 0f;
				return false;
			}
			float num2 = dir2.x * (start1.y - start2.y) - dir2.y * (start1.x - start2.x);
			t = num2 / num;
			return true;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C588 File Offset: 0x0000A788
		public static bool LineLineIntersectionFactors(float2 start1, float2 dir1, float2 start2, float2 dir2, out float factor1, out float factor2)
		{
			float num = dir2.y * dir1.x - dir2.x * dir1.y;
			if (math.abs(num) < 0.0001f)
			{
				factor1 = (factor2 = 0f);
				return false;
			}
			float num2 = dir2.x * (start1.y - start2.y) - dir2.y * (start1.x - start2.x);
			float num3 = dir1.x * (start1.y - start2.y) - dir1.y * (start1.x - start2.x);
			factor1 = num2 / num;
			factor2 = num3 / num;
			return true;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C630 File Offset: 0x0000A830
		public static Vector3 LineDirIntersectionPointXZ(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				return start1;
			}
			float num2 = (dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x)) / num;
			return start1 + dir1 * num2;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C69C File Offset: 0x0000A89C
		public static Vector3 LineDirIntersectionPointXZ(Vector3 start1, Vector3 dir1, Vector3 start2, Vector3 dir2, out bool intersects)
		{
			float num = dir2.z * dir1.x - dir2.x * dir1.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = (dir2.x * (start1.z - start2.z) - dir2.z * (start1.x - start2.x)) / num;
			intersects = true;
			return start1 + dir1 * num2;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C710 File Offset: 0x0000A910
		public static bool RaySegmentIntersectXZ(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			long num = (long)(int2.z * @int.x - int2.x * @int.z);
			if (num == 0L)
			{
				return false;
			}
			long num2 = (long)(int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x));
			long num3 = (long)(@int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x));
			return ((num2 < 0L) ^ (num < 0L)) && ((num3 < 0L) ^ (num < 0L)) && (num < 0L || num3 <= num) && (num >= 0L || num3 > num);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C7D8 File Offset: 0x0000A9D8
		public static bool LineIntersectionFactorXZ(Int3 start1, Int3 end1, Int3 start2, Int3 end2, out float factor1, out float factor2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			long num = (long)(int2.z * @int.x - int2.x * @int.z);
			if (num == 0L)
			{
				factor1 = 0f;
				factor2 = 0f;
				return false;
			}
			long num2 = (long)(int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x));
			long num3 = (long)(@int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x));
			factor1 = (float)num2 / (float)num;
			factor2 = (float)num3 / (float)num;
			return true;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C890 File Offset: 0x0000AA90
		public static bool LineIntersectionFactorXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out float factor1, out float factor2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num <= 1E-05f && num >= -1E-05f)
			{
				factor1 = 0f;
				factor2 = 0f;
				return false;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			factor1 = num4;
			factor2 = num5;
			return true;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C954 File Offset: 0x0000AB54
		public static float LineRayIntersectionFactorXZ(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			Int3 @int = end1 - start1;
			Int3 int2 = end2 - start2;
			int num = int2.z * @int.x - int2.x * @int.z;
			if (num == 0)
			{
				return float.NaN;
			}
			int num2 = int2.x * (start1.z - start2.z) - int2.z * (start1.x - start2.x);
			if ((float)(@int.x * (start1.z - start2.z) - @int.z * (start1.x - start2.x)) / (float)num < 0f)
			{
				return float.NaN;
			}
			return (float)num2 / (float)num;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000CA00 File Offset: 0x0000AC00
		public static float LineIntersectionFactorXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				return -1f;
			}
			return (vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x)) / num;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000CA74 File Offset: 0x0000AC74
		public static Vector3 LineIntersectionPointXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			bool flag;
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2, out flag);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000CA8C File Offset: 0x0000AC8C
		public static Vector3 LineIntersectionPointXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = (vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x)) / num;
			intersects = true;
			return start1 + vector * num2;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000CB10 File Offset: 0x0000AD10
		public static Vector2 LineIntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			bool flag;
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2, out flag);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000CB28 File Offset: 0x0000AD28
		public static Vector2 LineIntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2, out bool intersects)
		{
			Vector2 vector = end1 - start1;
			Vector2 vector2 = end2 - start2;
			float num = vector2.y * vector.x - vector2.x * vector.y;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = (vector2.x * (start1.y - start2.y) - vector2.y * (start1.x - start2.x)) / num;
			intersects = true;
			return start1 + vector * num2;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000CBAC File Offset: 0x0000ADAC
		public static Vector3 SegmentIntersectionPointXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2, out bool intersects)
		{
			Vector3 vector = end1 - start1;
			Vector3 vector2 = end2 - start2;
			float num = vector2.z * vector.x - vector2.x * vector.z;
			if (num == 0f)
			{
				intersects = false;
				return start1;
			}
			float num2 = vector2.x * (start1.z - start2.z) - vector2.z * (start1.x - start2.x);
			float num3 = vector.x * (start1.z - start2.z) - vector.z * (start1.x - start2.x);
			float num4 = num2 / num;
			float num5 = num3 / num;
			if (num4 < 0f || num4 > 1f || num5 < 0f || num5 > 1f)
			{
				intersects = false;
				return start1;
			}
			intersects = true;
			return start1 + vector * num4;
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000CC8C File Offset: 0x0000AE8C
		public static bool SegmentIntersectsBounds(Bounds bounds, Vector3 a, Vector3 b)
		{
			a -= bounds.center;
			b -= bounds.center;
			Vector3 vector = (a + b) * 0.5f;
			Vector3 vector2 = a - vector;
			Vector3 vector3 = new Vector3(Math.Abs(vector2.x), Math.Abs(vector2.y), Math.Abs(vector2.z));
			Vector3 extents = bounds.extents;
			return Math.Abs(vector.x) <= extents.x + vector3.x && Math.Abs(vector.y) <= extents.y + vector3.y && Math.Abs(vector.z) <= extents.z + vector3.z && Math.Abs(vector.y * vector2.z - vector.z * vector2.y) <= extents.y * vector3.z + extents.z * vector3.y && Math.Abs(vector.x * vector2.z - vector.z * vector2.x) <= extents.x * vector3.z + extents.z * vector3.x && Math.Abs(vector.x * vector2.y - vector.y * vector2.x) <= extents.x * vector3.y + extents.y * vector3.x;
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000CE14 File Offset: 0x0000B014
		public static bool LineCircleIntersectionFactors(float2 point, float2 direction, float radius, out float t1, out float t2)
		{
			float num = math.dot(point, direction);
			float num2 = math.lengthsq(point) - num * num;
			float num3 = radius * radius - num2;
			if (num3 < 0f)
			{
				t1 = float.PositiveInfinity;
				t2 = float.NegativeInfinity;
				return false;
			}
			float num4 = math.sqrt(num3);
			t1 = -num - num4;
			t2 = -num + num4;
			return true;
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000CE68 File Offset: 0x0000B068
		public static bool SegmentCircleIntersectionFactors(float2 point1, float2 point2, float radiusSq, out float t1, out float t2)
		{
			float2 @float = point2 - point1;
			float num = math.lengthsq(@float);
			float num2 = math.dot(point1, @float) / num;
			float num3 = math.lengthsq(point1) / num - num2 * num2;
			float num4 = radiusSq / num - num3;
			if (num4 < 0f)
			{
				t1 = float.PositiveInfinity;
				t2 = float.NegativeInfinity;
				return false;
			}
			float num5 = math.sqrt(num4);
			t1 = -num2 - num5;
			t2 = -num2 + num5;
			t1 = math.max(0f, t1);
			t2 = math.min(1f, t2);
			return t1 < 1f && t2 > 0f;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000CF08 File Offset: 0x0000B108
		public static float LineCircleIntersectionFactor(Vector3 circleCenter, Vector3 linePoint1, Vector3 linePoint2, float radius)
		{
			float num;
			Vector3 vector = VectorMath.Normalize(linePoint2 - linePoint1, out num);
			Vector3 vector2 = linePoint1 - circleCenter;
			float num2 = Vector3.Dot(vector2, vector);
			float num3 = num2 * num2 - (vector2.sqrMagnitude - radius * radius);
			if (num3 < 0f)
			{
				num3 = 0f;
			}
			float num4 = -num2 + Mathf.Sqrt(num3);
			if (num <= 1E-05f)
			{
				return 1f;
			}
			return num4 / num;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000CF70 File Offset: 0x0000B170
		public static bool ReversesFaceOrientations(Matrix4x4 matrix)
		{
			Vector3 vector = matrix.MultiplyVector(new Vector3(1f, 0f, 0f));
			Vector3 vector2 = matrix.MultiplyVector(new Vector3(0f, 1f, 0f));
			Vector3 vector3 = matrix.MultiplyVector(new Vector3(0f, 0f, 1f));
			return Vector3.Dot(Vector3.Cross(vector, vector2), vector3) < 0f;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		public static Vector3 Normalize(Vector3 v, out float magnitude)
		{
			magnitude = v.magnitude;
			if (magnitude > 1E-05f)
			{
				return v / magnitude;
			}
			return Vector3.zero;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000D005 File Offset: 0x0000B205
		public static Vector2 Normalize(Vector2 v, out float magnitude)
		{
			magnitude = v.magnitude;
			if (magnitude > 1E-05f)
			{
				return v / magnitude;
			}
			return Vector2.zero;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x0000D028 File Offset: 0x0000B228
		public static Vector3 ClampMagnitudeXZ(Vector3 v, float maxMagnitude)
		{
			float num = v.x * v.x + v.z * v.z;
			if (num > maxMagnitude * maxMagnitude && maxMagnitude > 0f)
			{
				float num2 = maxMagnitude / Mathf.Sqrt(num);
				v.x *= num2;
				v.z *= num2;
			}
			return v;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000D081 File Offset: 0x0000B281
		public static float MagnitudeXZ(Vector3 v)
		{
			return Mathf.Sqrt(v.x * v.x + v.z * v.z);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D0A3 File Offset: 0x0000B2A3
		public static float QuaternionAngle(quaternion rot)
		{
			return 2f * math.atan2(math.length(rot.value.xyz), rot.value.w);
		}
	}
}
