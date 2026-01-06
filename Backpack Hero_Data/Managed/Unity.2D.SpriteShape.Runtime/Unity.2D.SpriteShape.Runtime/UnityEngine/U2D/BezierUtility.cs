using System;
using Unity.Collections;
using Unity.Mathematics;

namespace UnityEngine.U2D
{
	// Token: 0x02000012 RID: 18
	public static class BezierUtility
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00006498 File Offset: 0x00004698
		public static Vector3 BezierPoint(Vector3 startRightTangent, Vector3 startPosition, Vector3 endPosition, Vector3 endLeftTangent, float t)
		{
			float num = 1f - t;
			float num2 = 3f * num * t;
			return num * num * num * startPosition + num2 * num * startRightTangent + num2 * t * endLeftTangent + t * t * t * endPosition;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000064F4 File Offset: 0x000046F4
		internal static float GetSpritePixelWidth(Sprite sprite)
		{
			float4 @float = new float4(sprite.pixelsPerUnit, sprite.pivot.y / sprite.textureRect.height, sprite.rect.width, sprite.rect.height);
			float4 float2 = new float4(sprite.border.x, sprite.border.y, sprite.border.z, sprite.border.w);
			float num = 1f / @float.x;
			ref float2 ptr = new float2(@float.z, @float.w) * num;
			float2 *= num;
			float x = float2.x;
			return ptr.x - float2.z - x;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000065BC File Offset: 0x000047BC
		internal static float BezierLength(NativeArray<ShapeControlPoint> shapePoints, int splineDetail, ref float smallestSegment)
		{
			int num = shapePoints.Length - 1;
			float num2 = 0f;
			float num3 = (float)(splineDetail - 1);
			for (int i = 0; i < num; i++)
			{
				int num4 = i + 1;
				ShapeControlPoint shapeControlPoint = shapePoints[i];
				ShapeControlPoint shapeControlPoint2 = shapePoints[num4];
				Vector3 position = shapeControlPoint.position;
				Vector3 position2 = shapeControlPoint2.position;
				Vector3 vector = position;
				Vector3 vector2 = position + shapeControlPoint.rightTangent;
				Vector3 vector3 = position2 + shapeControlPoint2.leftTangent;
				for (int j = 1; j < splineDetail; j++)
				{
					float num5 = (float)j / num3;
					Vector3 vector4 = BezierUtility.BezierPoint(vector2, position, position2, vector3, num5);
					float num6 = math.distance(vector4, vector);
					num2 += num6;
					vector = vector4;
				}
			}
			float num7 = num3 * (float)num;
			float num8 = num2 / (num7 * 1.08f);
			smallestSegment = math.min(num8, smallestSegment);
			return num2;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000066A4 File Offset: 0x000048A4
		public static Vector3 ClosestPointOnCurve(Vector3 point, Vector3 startPosition, Vector3 endPosition, Vector3 startTangent, Vector3 endTangent, out float t)
		{
			Vector3 vector = endPosition - startPosition;
			Vector3 vector2 = startTangent - startPosition;
			Vector3 vector3 = endTangent - endPosition;
			float num = 0.001f;
			if (BezierUtility.Colinear(vector2, vector, num) && BezierUtility.Colinear(vector3, vector, num))
			{
				return BezierUtility.ClosestPointToSegment(point, startPosition, endPosition, out t);
			}
			float num2 = 0f;
			float num3 = 0.5f;
			float num4 = 0.5f;
			float num5 = 1f;
			Vector3 vector4;
			Vector3 vector5;
			Vector3 vector6;
			Vector3 vector7;
			Vector3 vector8;
			Vector3 vector9;
			Vector3 vector10;
			Vector3 vector11;
			BezierUtility.SplitBezier(0.5f, startPosition, endPosition, startTangent, endTangent, out vector4, out vector5, out vector6, out vector7, out vector8, out vector9, out vector10, out vector11);
			Vector3 vector12 = BezierUtility.ClosestPointOnCurveIterative(point, vector4, vector5, vector6, vector7, num, ref num2, ref num3);
			Vector3 vector13 = BezierUtility.ClosestPointOnCurveIterative(point, vector8, vector9, vector10, vector11, num, ref num4, ref num5);
			if ((point - vector12).sqrMagnitude < (point - vector13).sqrMagnitude)
			{
				t = num2;
				return vector12;
			}
			t = num4;
			return vector13;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00006784 File Offset: 0x00004984
		public static Vector3 ClosestPointOnCurveFast(Vector3 point, Vector3 startPosition, Vector3 endPosition, Vector3 startTangent, Vector3 endTangent, out float t)
		{
			float num = 0.001f;
			float num2 = 0f;
			float num3 = 1f;
			Vector3 vector = BezierUtility.ClosestPointOnCurveIterative(point, startPosition, endPosition, startTangent, endTangent, num, ref num2, ref num3);
			t = num2;
			return vector;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000067B8 File Offset: 0x000049B8
		private static Vector3 ClosestPointOnCurveIterative(Vector3 point, Vector3 startPosition, Vector3 endPosition, Vector3 startTangent, Vector3 endTangent, float sqrError, ref float startT, ref float endT)
		{
			while ((startPosition - endPosition).sqrMagnitude > sqrError)
			{
				Vector3 vector = endPosition - startPosition;
				Vector3 vector2 = startTangent - startPosition;
				Vector3 vector3 = endTangent - endPosition;
				if (BezierUtility.Colinear(vector2, vector, sqrError) && BezierUtility.Colinear(vector3, vector, sqrError))
				{
					float num;
					Vector3 vector4 = BezierUtility.ClosestPointToSegment(point, startPosition, endPosition, out num);
					num *= endT - startT;
					startT += num;
					endT -= num;
					return vector4;
				}
				Vector3 vector5;
				Vector3 vector6;
				Vector3 vector7;
				Vector3 vector8;
				Vector3 vector9;
				Vector3 vector10;
				Vector3 vector11;
				Vector3 vector12;
				BezierUtility.SplitBezier(0.5f, startPosition, endPosition, startTangent, endTangent, out vector5, out vector6, out vector7, out vector8, out vector9, out vector10, out vector11, out vector12);
				BezierUtility.s_TempPoints[0] = vector5;
				BezierUtility.s_TempPoints[1] = vector7;
				BezierUtility.s_TempPoints[2] = vector8;
				float num2 = BezierUtility.SqrDistanceToPolyLine(point, BezierUtility.s_TempPoints);
				BezierUtility.s_TempPoints[0] = vector10;
				BezierUtility.s_TempPoints[1] = vector12;
				BezierUtility.s_TempPoints[2] = vector11;
				float num3 = BezierUtility.SqrDistanceToPolyLine(point, BezierUtility.s_TempPoints);
				if (num2 < num3)
				{
					startPosition = vector5;
					endPosition = vector6;
					startTangent = vector7;
					endTangent = vector8;
					endT -= (endT - startT) * 0.5f;
				}
				else
				{
					startPosition = vector9;
					endPosition = vector10;
					startTangent = vector11;
					endTangent = vector12;
					startT += (endT - startT) * 0.5f;
				}
			}
			return endPosition;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00006908 File Offset: 0x00004B08
		public static void SplitBezier(float t, Vector3 startPosition, Vector3 endPosition, Vector3 startRightTangent, Vector3 endLeftTangent, out Vector3 leftStartPosition, out Vector3 leftEndPosition, out Vector3 leftStartTangent, out Vector3 leftEndTangent, out Vector3 rightStartPosition, out Vector3 rightEndPosition, out Vector3 rightStartTangent, out Vector3 rightEndTangent)
		{
			Vector3 vector = startRightTangent - startPosition;
			Vector3 vector2 = endLeftTangent - endPosition;
			Vector3 vector3 = endLeftTangent - startRightTangent;
			Vector3 vector4 = startPosition + vector * t;
			Vector3 vector5 = endPosition + vector2 * (1f - t);
			Vector3 vector6 = startRightTangent + vector3 * t;
			Vector3 vector7 = vector4 + (vector6 - vector4) * t;
			Vector3 vector8 = vector5 + (vector6 - vector5) * (1f - t);
			Vector3 vector9 = vector8 - vector7;
			Vector3 vector10 = vector7 + vector9 * t;
			leftStartPosition = startPosition;
			leftEndPosition = vector10;
			leftStartTangent = vector4;
			leftEndTangent = vector7;
			rightStartPosition = vector10;
			rightEndPosition = endPosition;
			rightStartTangent = vector8;
			rightEndTangent = vector5;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000069F8 File Offset: 0x00004BF8
		private static Vector3 ClosestPointToSegment(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd, out float t)
		{
			Vector3 vector = point - segmentStart;
			Vector3 vector2 = segmentEnd - segmentStart;
			Vector3 normalized = vector2.normalized;
			float magnitude = vector2.magnitude;
			float num = Vector3.Dot(vector, normalized);
			if (num <= 0f)
			{
				num = 0f;
			}
			else if (num >= magnitude)
			{
				num = magnitude;
			}
			t = num / magnitude;
			return segmentStart + vector2 * t;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00006A54 File Offset: 0x00004C54
		private static float SqrDistanceToPolyLine(Vector3 point, Vector3[] points)
		{
			float num = float.MaxValue;
			for (int i = 0; i < points.Length - 1; i++)
			{
				float num2 = BezierUtility.SqrDistanceToSegment(point, points[i], points[i + 1]);
				if (num2 < num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00006A98 File Offset: 0x00004C98
		private static float SqrDistanceToSegment(Vector3 point, Vector3 segmentStart, Vector3 segmentEnd)
		{
			Vector3 vector = point - segmentStart;
			Vector3 vector2 = segmentEnd - segmentStart;
			Vector3 normalized = vector2.normalized;
			float magnitude = vector2.magnitude;
			float num = Vector3.Dot(vector, normalized);
			if (num <= 0f)
			{
				return (point - segmentStart).sqrMagnitude;
			}
			if (num >= magnitude)
			{
				return (point - segmentEnd).sqrMagnitude;
			}
			return Vector3.Cross(vector, normalized).sqrMagnitude;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006B10 File Offset: 0x00004D10
		private static bool Colinear(Vector3 v1, Vector3 v2, float error = 0.0001f)
		{
			return Mathf.Abs(v1.x * v2.y - v1.y * v2.x + v1.x * v2.z - v1.z * v2.x + v1.y * v2.z - v1.z * v2.y) < error;
		}

		// Token: 0x04000053 RID: 83
		private static Vector3[] s_TempPoints = new Vector3[3];
	}
}
