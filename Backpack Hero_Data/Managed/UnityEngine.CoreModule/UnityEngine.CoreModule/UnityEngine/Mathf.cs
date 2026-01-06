using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020001C7 RID: 455
	[Il2CppEagerStaticClassConstruction]
	[NativeHeader("Runtime/Math/FloatConversion.h")]
	[NativeHeader("Runtime/Utilities/BitUtility.h")]
	[NativeHeader("Runtime/Math/ColorSpaceConversion.h")]
	[NativeHeader("Runtime/Math/PerlinNoise.h")]
	public struct Mathf
	{
		// Token: 0x0600148B RID: 5259
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int ClosestPowerOfTwo(int value);

		// Token: 0x0600148C RID: 5260
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern bool IsPowerOfTwo(int value);

		// Token: 0x0600148D RID: 5261
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern int NextPowerOfTwo(int value);

		// Token: 0x0600148E RID: 5262
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x0600148F RID: 5263
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x06001490 RID: 5264 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		[FreeFunction(IsThreadSafe = true)]
		public static Color CorrelatedColorTemperatureToRGB(float kelvin)
		{
			Color color;
			Mathf.CorrelatedColorTemperatureToRGB_Injected(kelvin, out color);
			return color;
		}

		// Token: 0x06001491 RID: 5265
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern ushort FloatToHalf(float val);

		// Token: 0x06001492 RID: 5266
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float HalfToFloat(ushort val);

		// Token: 0x06001493 RID: 5267
		[FreeFunction("PerlinNoise::NoiseNormalized", IsThreadSafe = true)]
		[MethodImpl(4096)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x06001494 RID: 5268 RVA: 0x0001F8C4 File Offset: 0x0001DAC4
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0001F8E0 File Offset: 0x0001DAE0
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06001496 RID: 5270 RVA: 0x0001F8FC File Offset: 0x0001DAFC
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06001497 RID: 5271 RVA: 0x0001F918 File Offset: 0x0001DB18
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x06001498 RID: 5272 RVA: 0x0001F934 File Offset: 0x0001DB34
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0001F950 File Offset: 0x0001DB50
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0001F96C File Offset: 0x0001DB6C
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0001F988 File Offset: 0x0001DB88
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0001F9A4 File Offset: 0x0001DBA4
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0001F9BC File Offset: 0x0001DBBC
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0001F9D4 File Offset: 0x0001DBD4
		public static float Min(float a, float b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0001F9F0 File Offset: 0x0001DBF0
		public static float Min(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0001FA48 File Offset: 0x0001DC48
		public static int Min(int a, int b)
		{
			return (a < b) ? a : b;
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0001FA64 File Offset: 0x0001DC64
		public static int Min(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] < num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0001FAB8 File Offset: 0x0001DCB8
		public static float Max(float a, float b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0001FAD4 File Offset: 0x0001DCD4
		public static float Max(params float[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0001FB2C File Offset: 0x0001DD2C
		public static int Max(int a, int b)
		{
			return (a > b) ? a : b;
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x0001FB48 File Offset: 0x0001DD48
		public static int Max(params int[] values)
		{
			int num = values.Length;
			bool flag = num == 0;
			int num2;
			if (flag)
			{
				num2 = 0;
			}
			else
			{
				int num3 = values[0];
				for (int i = 1; i < num; i++)
				{
					bool flag2 = values[i] > num3;
					if (flag2)
					{
						num3 = values[i];
					}
				}
				num2 = num3;
			}
			return num2;
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x0001FB9C File Offset: 0x0001DD9C
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x0001FBB8 File Offset: 0x0001DDB8
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x0001FBD4 File Offset: 0x0001DDD4
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x0001FC0C File Offset: 0x0001DE0C
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0001FC28 File Offset: 0x0001DE28
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0001FC44 File Offset: 0x0001DE44
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0001FC60 File Offset: 0x0001DE60
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0001FC98 File Offset: 0x0001DE98
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0001FCB4 File Offset: 0x0001DEB4
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x0001FCD0 File Offset: 0x0001DED0
		[MethodImpl(256)]
		public static float Sign(float f)
		{
			return (f >= 0f) ? 1f : (-1f);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x0001FCF8 File Offset: 0x0001DEF8
		public static float Clamp(float value, float min, float max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x0001FD24 File Offset: 0x0001DF24
		public static int Clamp(int value, int min, int max)
		{
			bool flag = value < min;
			if (flag)
			{
				value = min;
			}
			else
			{
				bool flag2 = value > max;
				if (flag2)
				{
					value = max;
				}
			}
			return value;
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x0001FD50 File Offset: 0x0001DF50
		public static float Clamp01(float value)
		{
			bool flag = value < 0f;
			float num;
			if (flag)
			{
				num = 0f;
			}
			else
			{
				bool flag2 = value > 1f;
				if (flag2)
				{
					num = 1f;
				}
				else
				{
					num = value;
				}
			}
			return num;
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x0001FD8C File Offset: 0x0001DF8C
		public static float Lerp(float a, float b, float t)
		{
			return a + (b - a) * Mathf.Clamp01(t);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0001FDAC File Offset: 0x0001DFAC
		public static float LerpUnclamped(float a, float b, float t)
		{
			return a + (b - a) * t;
		}

		// Token: 0x060014B7 RID: 5303 RVA: 0x0001FDC8 File Offset: 0x0001DFC8
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x060014B8 RID: 5304 RVA: 0x0001FE08 File Offset: 0x0001E008
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			bool flag = Mathf.Abs(target - current) <= maxDelta;
			float num;
			if (flag)
			{
				num = target;
			}
			else
			{
				num = current + Mathf.Sign(target - current) * maxDelta;
			}
			return num;
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0001FE3C File Offset: 0x0001E03C
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			float num = Mathf.DeltaAngle(current, target);
			bool flag = -maxDelta < num && num < maxDelta;
			float num2;
			if (flag)
			{
				num2 = target;
			}
			else
			{
				target = current + num;
				num2 = Mathf.MoveTowards(current, target, maxDelta);
			}
			return num2;
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0001FE78 File Offset: 0x0001E078
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x0001FEB8 File Offset: 0x0001E0B8
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = value < 0f;
			float num = Mathf.Abs(value);
			bool flag2 = num > absmax;
			float num2;
			if (flag2)
			{
				num2 = (flag ? (-num) : num);
			}
			else
			{
				float num3 = Mathf.Pow(num / absmax, gamma) * absmax;
				num2 = (flag ? (-num3) : num3);
			}
			return num2;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x0001FF04 File Offset: 0x0001E104
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), Mathf.Epsilon * 8f);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x0001FF48 File Offset: 0x0001E148
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x0001FF6C File Offset: 0x0001E16C
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x0001FF98 File Offset: 0x0001E198
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			bool flag = num5 - current > 0f == num8 > num5;
			if (flag)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00020054 File Offset: 0x0001E254
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00020078 File Offset: 0x0001E278
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x000200A4 File Offset: 0x0001E2A4
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x000200D0 File Offset: 0x0001E2D0
		public static float Repeat(float t, float length)
		{
			return Mathf.Clamp(t - Mathf.Floor(t / length) * length, 0f, length);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000200FC File Offset: 0x0001E2FC
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00020128 File Offset: 0x0001E328
		public static float InverseLerp(float a, float b, float value)
		{
			bool flag = a != b;
			float num;
			if (flag)
			{
				num = Mathf.Clamp01((value - a) / (b - a));
			}
			else
			{
				num = 0f;
			}
			return num;
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0002015C File Offset: 0x0001E35C
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			bool flag = num > 180f;
			if (flag)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x00020194 File Offset: 0x0001E394
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				result.x = p1.x + num8 * num;
				result.y = p1.y + num8 * num2;
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00020250 File Offset: 0x0001E450
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			bool flag = num5 == 0f;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				float num6 = p3.x - p1.x;
				float num7 = p3.y - p1.y;
				float num8 = (num6 * num4 - num7 * num3) / num5;
				bool flag3 = num8 < 0f || num8 > 1f;
				if (flag3)
				{
					flag2 = false;
				}
				else
				{
					float num9 = (num6 * num2 - num7 * num) / num5;
					bool flag4 = num9 < 0f || num9 > 1f;
					if (flag4)
					{
						flag2 = false;
					}
					else
					{
						result.x = p1.x + num8 * num;
						result.y = p1.y + num8 * num2;
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00020360 File Offset: 0x0001E560
		internal static long RandomToLong(Random r)
		{
			byte[] array = new byte[8];
			r.NextBytes(array);
			return (long)(BitConverter.ToUInt64(array, 0) & 9223372036854775807UL);
		}

		// Token: 0x060014CB RID: 5323
		[MethodImpl(4096)]
		private static extern void CorrelatedColorTemperatureToRGB_Injected(float kelvin, out Color ret);

		// Token: 0x04000772 RID: 1906
		public const float PI = 3.1415927f;

		// Token: 0x04000773 RID: 1907
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x04000774 RID: 1908
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04000775 RID: 1909
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x04000776 RID: 1910
		public const float Rad2Deg = 57.29578f;

		// Token: 0x04000777 RID: 1911
		public static readonly float Epsilon = (MathfInternal.IsFlushToZeroEnabled ? MathfInternal.FloatMinNormal : MathfInternal.FloatMinDenormal);
	}
}
