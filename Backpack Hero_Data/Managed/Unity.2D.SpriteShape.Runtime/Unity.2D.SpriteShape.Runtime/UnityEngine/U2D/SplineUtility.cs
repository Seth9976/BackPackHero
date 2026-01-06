using System;

namespace UnityEngine.U2D
{
	// Token: 0x02000015 RID: 21
	public class SplineUtility
	{
		// Token: 0x060000B1 RID: 177 RVA: 0x00006F94 File Offset: 0x00005194
		public static float SlopeAngle(Vector2 start, Vector2 end)
		{
			Vector2 vector = start - end;
			vector.Normalize();
			Vector2 vector2 = new Vector2(0f, 1f);
			Vector2 vector3 = new Vector2(1f, 0f);
			float num = Vector2.Dot(vector, vector3);
			float num2 = Vector2.Dot(vector, vector2);
			float num3 = Mathf.Acos(num2);
			float num4 = ((num >= 0f) ? 1f : (-1f));
			float num5 = num3 * 57.29578f * num4;
			num5 = ((num2 != 1f) ? num5 : 0f);
			return (num2 != -1f) ? num5 : (-180f);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00007030 File Offset: 0x00005230
		public static void CalculateTangents(Vector3 point, Vector3 prevPoint, Vector3 nextPoint, Vector3 forward, float scale, out Vector3 rightTangent, out Vector3 leftTangent)
		{
			Vector3 normalized = (prevPoint - point).normalized;
			Vector3 normalized2 = (nextPoint - point).normalized;
			Vector3 vector = normalized + normalized2;
			Vector3 vector2 = forward;
			if (prevPoint != nextPoint)
			{
				if (Mathf.Abs(normalized.x * normalized2.y - normalized.y * normalized2.x + normalized.x * normalized2.z - normalized.z * normalized2.x + normalized.y * normalized2.z - normalized.z * normalized2.y) < 0.01f)
				{
					rightTangent = normalized2 * scale;
					leftTangent = normalized * scale;
					return;
				}
				vector2 = Vector3.Cross(normalized, normalized2);
			}
			rightTangent = Vector3.Cross(vector2, vector).normalized * scale;
			leftTangent = -rightTangent;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000712E File Offset: 0x0000532E
		public static int NextIndex(int index, int pointCount)
		{
			return SplineUtility.Mod(index + 1, pointCount);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00007139 File Offset: 0x00005339
		public static int PreviousIndex(int index, int pointCount)
		{
			return SplineUtility.Mod(index - 1, pointCount);
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00007144 File Offset: 0x00005344
		private static int Mod(int x, int m)
		{
			int num = x % m;
			if (num >= 0)
			{
				return num;
			}
			return num + m;
		}
	}
}
