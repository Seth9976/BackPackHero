using System;
using System.Collections.Generic;
using UnityEngine;

namespace Febucci.UI.Core
{
	// Token: 0x02000046 RID: 70
	public static class TextUtilities
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00006F1E File Offset: 0x0000511E
		public static Vector3[] FakeRandoms
		{
			get
			{
				return TextUtilities.fakeRandoms;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006F28 File Offset: 0x00005128
		internal static void Initialize()
		{
			if (TextUtilities.initialized)
			{
				return;
			}
			TextUtilities.initialized = true;
			List<Vector3> list = new List<Vector3>();
			for (float num = 0f; num < 360f; num += 14f)
			{
				float num2 = num * 0.017453292f;
				list.Add(new Vector3(Mathf.Sin(num2), Mathf.Cos(num2)).normalized);
			}
			TextUtilities.fakeRandoms = new Vector3[25];
			for (int i = 0; i < TextUtilities.fakeRandoms.Length; i++)
			{
				int num3 = Random.Range(0, list.Count);
				TextUtilities.fakeRandoms[i] = list[num3];
				list.RemoveAt(num3);
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006FD4 File Offset: 0x000051D4
		public static Vector3 RotateAround(this Vector3 vec, Vector2 center, float rotDegrees)
		{
			rotDegrees *= 0.017453292f;
			float num = vec.x - center.x;
			float num2 = vec.y - center.y;
			float num3 = num * Mathf.Cos(rotDegrees) - num2 * Mathf.Sin(rotDegrees);
			float num4 = num * Mathf.Sin(rotDegrees) + num2 * Mathf.Cos(rotDegrees);
			vec.x = num3 + center.x;
			vec.y = num4 + center.y;
			return vec;
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007048 File Offset: 0x00005248
		public static void MoveChar(this Vector3[] vec, Vector3 dir)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] += dir;
				b += 1;
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000707C File Offset: 0x0000527C
		public static void SetChar(this Vector3[] vec, Vector3 pos)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = pos;
				b += 1;
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000070A0 File Offset: 0x000052A0
		public static void LerpUnclamped(this Vector3[] vec, Vector3 target, float pct)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = Vector3.LerpUnclamped(vec[(int)b], target, pct);
				b += 1;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x000070D1 File Offset: 0x000052D1
		public static Vector3 GetMiddlePos(this Vector3[] vec)
		{
			return (vec[0] + vec[2]) / 2f;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x000070F0 File Offset: 0x000052F0
		public static void RotateChar(this Vector3[] vec, float angle)
		{
			Vector3 middlePos = vec.GetMiddlePos();
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = vec[(int)b].RotateAround(middlePos, angle);
				b += 1;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00007130 File Offset: 0x00005330
		public static void RotateChar(this Vector3[] vec, float angle, Vector3 pivot)
		{
			byte b = 0;
			while ((int)b < vec.Length)
			{
				vec[(int)b] = vec[(int)b].RotateAround(pivot, angle);
				b += 1;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007168 File Offset: 0x00005368
		public static void SetColor(this Color32[] col, Color32 target)
		{
			byte b = 0;
			while ((int)b < col.Length)
			{
				col[(int)b] = target;
				b += 1;
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000718C File Offset: 0x0000538C
		public static void LerpUnclamped(this Color32[] col, Color32 target, float pct)
		{
			byte b = 0;
			while ((int)b < col.Length)
			{
				col[(int)b] = Color32.LerpUnclamped(col[(int)b], target, pct);
				b += 1;
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x000071BD File Offset: 0x000053BD
		public static float CalculateCurveDuration(this AnimationCurve curve)
		{
			if (curve.keys.Length != 0)
			{
				return curve.keys[curve.length - 1].time;
			}
			return 0f;
		}

		// Token: 0x04000105 RID: 261
		public const int verticesPerChar = 4;

		// Token: 0x04000106 RID: 262
		public const int fakeRandomsCount = 25;

		// Token: 0x04000107 RID: 263
		internal static Vector3[] fakeRandoms;

		// Token: 0x04000108 RID: 264
		private static bool initialized;
	}
}
