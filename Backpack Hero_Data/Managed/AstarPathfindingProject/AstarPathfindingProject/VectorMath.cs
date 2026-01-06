using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000030 RID: 48
	public static class VectorMath
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00009D17 File Offset: 0x00007F17
		public static Vector2 ComplexMultiply(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x - a.y * b.y, a.x * b.y + a.y * b.x);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009D54 File Offset: 0x00007F54
		public static Vector2 ComplexMultiplyConjugate(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x + a.y * b.y, a.y * b.x - a.x * b.y);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009D94 File Offset: 0x00007F94
		public static Vector3 ClosestPointOnLine(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = Vector3.Normalize(lineEnd - lineStart);
			float num = Vector3.Dot(point - lineStart, vector);
			return lineStart + num * vector;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009DCC File Offset: 0x00007FCC
		public static float ClosestPointOnLineFactor(Vector3 lineStart, Vector3 lineEnd, Vector3 point)
		{
			Vector3 vector = lineEnd - lineStart;
			float sqrMagnitude = vector.sqrMagnitude;
			if ((double)sqrMagnitude <= 1E-06)
			{
				return 0f;
			}
			return Vector3.Dot(point - lineStart, vector) / sqrMagnitude;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009E0C File Offset: 0x0000800C
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

		// Token: 0x06000221 RID: 545 RVA: 0x00009E48 File Offset: 0x00008048
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

		// Token: 0x06000222 RID: 546 RVA: 0x00009E88 File Offset: 0x00008088
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

		// Token: 0x06000223 RID: 547 RVA: 0x00009ED8 File Offset: 0x000080D8
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

		// Token: 0x06000224 RID: 548 RVA: 0x00009F60 File Offset: 0x00008160
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

		// Token: 0x06000225 RID: 549 RVA: 0x00009FE4 File Offset: 0x000081E4
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

		// Token: 0x06000226 RID: 550 RVA: 0x0000A0A4 File Offset: 0x000082A4
		public static float SqrDistancePointSegment(Vector3 a, Vector3 b, Vector3 p)
		{
			return (VectorMath.ClosestPointOnSegment(a, b, p) - p).sqrMagnitude;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000A0C8 File Offset: 0x000082C8
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
			if ((num6 = (num7 = num * num3 - num2 * num2)) < 1E-05)
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

		// Token: 0x06000228 RID: 552 RVA: 0x0000A2A4 File Offset: 0x000084A4
		public static float SqrDistanceXZ(Vector3 a, Vector3 b)
		{
			Vector3 vector = a - b;
			return vector.x * vector.x + vector.z * vector.z;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000A2D4 File Offset: 0x000084D4
		public static long SignedTriangleAreaTimes2XZ(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000A311 File Offset: 0x00008511
		public static float SignedTriangleAreaTimes2XZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000A34A File Offset: 0x0000854A
		public static bool RightXZ(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) < -1E-45f;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000A38C File Offset: 0x0000858C
		public static bool RightXZ(Int3 a, Int3 b, Int3 p)
		{
			return (long)(b.x - a.x) * (long)(p.z - a.z) - (long)(p.x - a.x) * (long)(b.z - a.z) < 0L;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000A3D8 File Offset: 0x000085D8
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

		// Token: 0x0600022E RID: 558 RVA: 0x0000A430 File Offset: 0x00008630
		public static bool RightOrColinear(Vector2 a, Vector2 b, Vector2 p)
		{
			return (b.x - a.x) * (p.y - a.y) - (p.x - a.x) * (b.y - a.y) <= 0f;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000A480 File Offset: 0x00008680
		public static bool RightOrColinear(Int2 a, Int2 b, Int2 p)
		{
			return (long)(b.x - a.x) * (long)(p.y - a.y) - (long)(p.x - a.x) * (long)(b.y - a.y) <= 0L;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000A4D0 File Offset: 0x000086D0
		public static bool RightOrColinearXZ(Vector3 a, Vector3 b, Vector3 p)
		{
			return (b.x - a.x) * (p.z - a.z) - (p.x - a.x) * (b.z - a.z) <= 0f;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A520 File Offset: 0x00008720
		public static bool RightOrColinearXZ(Int3 a, Int3 b, Int3 p)
		{
			return (long)(b.x - a.x) * (long)(p.z - a.z) - (long)(p.x - a.x) * (long)(b.z - a.z) <= 0L;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A570 File Offset: 0x00008770
		public static bool IsClockwiseMarginXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) <= float.Epsilon;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A5BE File Offset: 0x000087BE
		public static bool IsClockwiseXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			return (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z) < 0f;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A5FE File Offset: 0x000087FE
		public static bool IsClockwiseXZ(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.RightXZ(a, b, c);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A608 File Offset: 0x00008808
		public static bool IsClockwiseOrColinearXZ(Int3 a, Int3 b, Int3 c)
		{
			return VectorMath.RightOrColinearXZ(a, b, c);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000A612 File Offset: 0x00008812
		public static bool IsClockwiseOrColinear(Int2 a, Int2 b, Int2 c)
		{
			return VectorMath.RightOrColinear(a, b, c);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000A61C File Offset: 0x0000881C
		public static bool IsColinear(Vector3 a, Vector3 b, Vector3 c)
		{
			Vector3 vector = b - a;
			Vector3 vector2 = c - a;
			float num = vector.y * vector2.z - vector.z * vector2.y;
			float num2 = vector.z * vector2.x - vector.x * vector2.z;
			float num3 = vector.x * vector2.y - vector.y * vector2.x;
			return num * num + num2 * num2 + num3 * num3 <= 0.0001f;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public static bool IsColinear(Vector2 a, Vector2 b, Vector2 c)
		{
			float num = (b.x - a.x) * (c.y - a.y) - (c.x - a.x) * (b.y - a.y);
			return num <= 0.0001f && num >= -0.0001f;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000A6FC File Offset: 0x000088FC
		public static bool IsColinearXZ(Int3 a, Int3 b, Int3 c)
		{
			return (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z) == 0L;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000A748 File Offset: 0x00008948
		public static bool IsColinearXZ(Vector3 a, Vector3 b, Vector3 c)
		{
			float num = (b.x - a.x) * (c.z - a.z) - (c.x - a.x) * (b.z - a.z);
			return num <= 1E-07f && num >= -1E-07f;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000A7A4 File Offset: 0x000089A4
		public static bool IsColinearAlmostXZ(Int3 a, Int3 b, Int3 c)
		{
			long num = (long)(b.x - a.x) * (long)(c.z - a.z) - (long)(c.x - a.x) * (long)(b.z - a.z);
			return num > -1L && num < 1L;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000A7F9 File Offset: 0x000089F9
		public static bool SegmentsIntersect(Int2 start1, Int2 end1, Int2 start2, Int2 end2)
		{
			return VectorMath.RightOrColinear(start1, end1, start2) != VectorMath.RightOrColinear(start1, end1, end2) && VectorMath.RightOrColinear(start2, end2, start1) != VectorMath.RightOrColinear(start2, end2, end1);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0000A824 File Offset: 0x00008A24
		public static bool SegmentsIntersectXZ(Int3 start1, Int3 end1, Int3 start2, Int3 end2)
		{
			return VectorMath.RightOrColinearXZ(start1, end1, start2) != VectorMath.RightOrColinearXZ(start1, end1, end2) && VectorMath.RightOrColinearXZ(start2, end2, start1) != VectorMath.RightOrColinearXZ(start2, end2, end1);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000A850 File Offset: 0x00008A50
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

		// Token: 0x0600023F RID: 575 RVA: 0x0000A918 File Offset: 0x00008B18
		public static bool LineLineIntersectionFactor(Vector2 start1, Vector2 dir1, Vector2 start2, Vector2 dir2, out float t)
		{
			float num = dir2.y * dir1.x - dir2.x * dir1.y;
			if (Mathf.Abs(num) < 0.0001f)
			{
				t = 0f;
				return false;
			}
			float num2 = dir2.x * (start1.y - start2.y) - dir2.y * (start1.x - start2.x);
			t = num2 / num;
			return true;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000A98C File Offset: 0x00008B8C
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

		// Token: 0x06000241 RID: 577 RVA: 0x0000A9F8 File Offset: 0x00008BF8
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

		// Token: 0x06000242 RID: 578 RVA: 0x0000AA6C File Offset: 0x00008C6C
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

		// Token: 0x06000243 RID: 579 RVA: 0x0000AB34 File Offset: 0x00008D34
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

		// Token: 0x06000244 RID: 580 RVA: 0x0000ABEC File Offset: 0x00008DEC
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

		// Token: 0x06000245 RID: 581 RVA: 0x0000ACB0 File Offset: 0x00008EB0
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

		// Token: 0x06000246 RID: 582 RVA: 0x0000AD5C File Offset: 0x00008F5C
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

		// Token: 0x06000247 RID: 583 RVA: 0x0000ADD0 File Offset: 0x00008FD0
		public static Vector3 LineIntersectionPointXZ(Vector3 start1, Vector3 end1, Vector3 start2, Vector3 end2)
		{
			bool flag;
			return VectorMath.LineIntersectionPointXZ(start1, end1, start2, end2, out flag);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000ADE8 File Offset: 0x00008FE8
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

		// Token: 0x06000249 RID: 585 RVA: 0x0000AE6C File Offset: 0x0000906C
		public static Vector2 LineIntersectionPoint(Vector2 start1, Vector2 end1, Vector2 start2, Vector2 end2)
		{
			bool flag;
			return VectorMath.LineIntersectionPoint(start1, end1, start2, end2, out flag);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000AE84 File Offset: 0x00009084
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

		// Token: 0x0600024B RID: 587 RVA: 0x0000AF08 File Offset: 0x00009108
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

		// Token: 0x0600024C RID: 588 RVA: 0x0000AFE8 File Offset: 0x000091E8
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

		// Token: 0x0600024D RID: 589 RVA: 0x0000B170 File Offset: 0x00009370
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

		// Token: 0x0600024E RID: 590 RVA: 0x0000B1D8 File Offset: 0x000093D8
		public static bool ReversesFaceOrientations(Matrix4x4 matrix)
		{
			Vector3 vector = matrix.MultiplyVector(new Vector3(1f, 0f, 0f));
			Vector3 vector2 = matrix.MultiplyVector(new Vector3(0f, 1f, 0f));
			Vector3 vector3 = matrix.MultiplyVector(new Vector3(0f, 0f, 1f));
			return Vector3.Dot(Vector3.Cross(vector, vector2), vector3) < 0f;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x0000B24C File Offset: 0x0000944C
		public static bool ReversesFaceOrientationsXZ(Matrix4x4 matrix)
		{
			Vector3 vector = matrix.MultiplyVector(new Vector3(1f, 0f, 0f));
			Vector3 vector2 = matrix.MultiplyVector(new Vector3(0f, 0f, 1f));
			return vector.x * vector2.z - vector2.x * vector.z < 0f;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000B2B3 File Offset: 0x000094B3
		public static Vector3 Normalize(Vector3 v, out float magnitude)
		{
			magnitude = v.magnitude;
			if (magnitude > 1E-05f)
			{
				return v / magnitude;
			}
			return Vector3.zero;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x0000B2D5 File Offset: 0x000094D5
		public static Vector2 Normalize(Vector2 v, out float magnitude)
		{
			magnitude = v.magnitude;
			if (magnitude > 1E-05f)
			{
				return v / magnitude;
			}
			return Vector2.zero;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000B2F8 File Offset: 0x000094F8
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

		// Token: 0x06000253 RID: 595 RVA: 0x0000B351 File Offset: 0x00009551
		public static float MagnitudeXZ(Vector3 v)
		{
			return Mathf.Sqrt(v.x * v.x + v.z * v.z);
		}
	}
}
