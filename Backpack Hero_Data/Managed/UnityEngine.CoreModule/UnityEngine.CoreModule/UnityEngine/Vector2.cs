using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001C9 RID: 457
	[Il2CppEagerStaticClassConstruction]
	[NativeClass("Vector2f")]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	public struct Vector2 : IEquatable<Vector2>, IFormattable
	{
		// Token: 0x17000424 RID: 1060
		public float this[int index]
		{
			[MethodImpl(256)]
			get
			{
				float num;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					num = this.y;
				}
				else
				{
					num = this.x;
				}
				return num;
			}
			[MethodImpl(256)]
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00020426 File Offset: 0x0001E626
		[MethodImpl(256)]
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00020426 File Offset: 0x0001E626
		[MethodImpl(256)]
		public void Set(float newX, float newY)
		{
			this.x = newX;
			this.y = newY;
		}

		// Token: 0x060014D0 RID: 5328 RVA: 0x00020438 File Offset: 0x0001E638
		[MethodImpl(256)]
		public static Vector2 Lerp(Vector2 a, Vector2 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x060014D1 RID: 5329 RVA: 0x00020484 File Offset: 0x0001E684
		[MethodImpl(256)]
		public static Vector2 LerpUnclamped(Vector2 a, Vector2 b, float t)
		{
			return new Vector2(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t);
		}

		// Token: 0x060014D2 RID: 5330 RVA: 0x000204C8 File Offset: 0x0001E6C8
		[MethodImpl(256)]
		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = num * num + num2 * num2;
			bool flag = num3 == 0f || (maxDistanceDelta >= 0f && num3 <= maxDistanceDelta * maxDistanceDelta);
			Vector2 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num4 = (float)Math.Sqrt((double)num3);
				vector = new Vector2(current.x + num / num4 * maxDistanceDelta, current.y + num2 / num4 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x060014D3 RID: 5331 RVA: 0x00020550 File Offset: 0x0001E750
		[MethodImpl(256)]
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x00020581 File Offset: 0x0001E781
		[MethodImpl(256)]
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x000205AC File Offset: 0x0001E7AC
		[MethodImpl(256)]
		public void Normalize()
		{
			float magnitude = this.magnitude;
			bool flag = magnitude > 1E-05f;
			if (flag)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x060014D6 RID: 5334 RVA: 0x000205EC File Offset: 0x0001E7EC
		public Vector2 normalized
		{
			[MethodImpl(256)]
			get
			{
				Vector2 vector = new Vector2(this.x, this.y);
				vector.Normalize();
				return vector;
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0002061C File Offset: 0x0001E81C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x00020638 File Offset: 0x0001E838
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x00020654 File Offset: 0x0001E854
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F2";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x000206BC File Offset: 0x0001E8BC
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x000206E8 File Offset: 0x0001E8E8
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2);
			return !flag && this.Equals((Vector2)other);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0002071C File Offset: 0x0001E91C
		[MethodImpl(256)]
		public bool Equals(Vector2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x00020750 File Offset: 0x0001E950
		[MethodImpl(256)]
		public static Vector2 Reflect(Vector2 inDirection, Vector2 inNormal)
		{
			float num = -2f * Vector2.Dot(inNormal, inDirection);
			return new Vector2(num * inNormal.x + inDirection.x, num * inNormal.y + inDirection.y);
		}

		// Token: 0x060014DE RID: 5342 RVA: 0x00020794 File Offset: 0x0001E994
		[MethodImpl(256)]
		public static Vector2 Perpendicular(Vector2 inDirection)
		{
			return new Vector2(-inDirection.y, inDirection.x);
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x000207B8 File Offset: 0x0001E9B8
		[MethodImpl(256)]
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x060014E0 RID: 5344 RVA: 0x000207E8 File Offset: 0x0001E9E8
		public float magnitude
		{
			[MethodImpl(256)]
			get
			{
				return (float)Math.Sqrt((double)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x0002081C File Offset: 0x0001EA1C
		public float sqrMagnitude
		{
			[MethodImpl(256)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x060014E2 RID: 5346 RVA: 0x0002084C File Offset: 0x0001EA4C
		[MethodImpl(256)]
		public static float Angle(Vector2 from, Vector2 to)
		{
			float num = (float)Math.Sqrt((double)(from.sqrMagnitude * to.sqrMagnitude));
			bool flag = num < 1E-15f;
			float num2;
			if (flag)
			{
				num2 = 0f;
			}
			else
			{
				float num3 = Mathf.Clamp(Vector2.Dot(from, to) / num, -1f, 1f);
				num2 = (float)Math.Acos((double)num3) * 57.29578f;
			}
			return num2;
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x000208B0 File Offset: 0x0001EAB0
		[MethodImpl(256)]
		public static float SignedAngle(Vector2 from, Vector2 to)
		{
			float num = Vector2.Angle(from, to);
			float num2 = Mathf.Sign(from.x * to.y - from.y * to.x);
			return num * num2;
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x000208F0 File Offset: 0x0001EAF0
		[MethodImpl(256)]
		public static float Distance(Vector2 a, Vector2 b)
		{
			float num = a.x - b.x;
			float num2 = a.y - b.y;
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x0002092C File Offset: 0x0001EB2C
		[MethodImpl(256)]
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			float sqrMagnitude = vector.sqrMagnitude;
			bool flag = sqrMagnitude > maxLength * maxLength;
			Vector2 vector2;
			if (flag)
			{
				float num = (float)Math.Sqrt((double)sqrMagnitude);
				float num2 = vector.x / num;
				float num3 = vector.y / num;
				vector2 = new Vector2(num2 * maxLength, num3 * maxLength);
			}
			else
			{
				vector2 = vector;
			}
			return vector2;
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00020980 File Offset: 0x0001EB80
		[MethodImpl(256)]
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x000209B0 File Offset: 0x0001EBB0
		[MethodImpl(256)]
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x000209E0 File Offset: 0x0001EBE0
		[MethodImpl(256)]
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00020A1C File Offset: 0x0001EC1C
		[MethodImpl(256)]
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00020A58 File Offset: 0x0001EC58
		[ExcludeFromDocs]
		[MethodImpl(256)]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x060014EB RID: 5355 RVA: 0x00020A7C File Offset: 0x0001EC7C
		[ExcludeFromDocs]
		[MethodImpl(256)]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x060014EC RID: 5356 RVA: 0x00020AA8 File Offset: 0x0001ECA8
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current.x - target.x;
			float num5 = current.y - target.y;
			Vector2 vector = target;
			float num6 = maxSpeed * smoothTime;
			float num7 = num6 * num6;
			float num8 = num4 * num4 + num5 * num5;
			bool flag = num8 > num7;
			if (flag)
			{
				float num9 = (float)Math.Sqrt((double)num8);
				num4 = num4 / num9 * num6;
				num5 = num5 / num9 * num6;
			}
			target.x = current.x - num4;
			target.y = current.y - num5;
			float num10 = (currentVelocity.x + num * num4) * deltaTime;
			float num11 = (currentVelocity.y + num * num5) * deltaTime;
			currentVelocity.x = (currentVelocity.x - num * num10) * num3;
			currentVelocity.y = (currentVelocity.y - num * num11) * num3;
			float num12 = target.x + (num4 + num10) * num3;
			float num13 = target.y + (num5 + num11) * num3;
			float num14 = vector.x - current.x;
			float num15 = vector.y - current.y;
			float num16 = num12 - vector.x;
			float num17 = num13 - vector.y;
			bool flag2 = num14 * num16 + num15 * num17 > 0f;
			if (flag2)
			{
				num12 = vector.x;
				num13 = vector.y;
				currentVelocity.x = (num12 - vector.x) / deltaTime;
				currentVelocity.y = (num13 - vector.y) / deltaTime;
			}
			return new Vector2(num12, num13);
		}

		// Token: 0x060014ED RID: 5357 RVA: 0x00020C74 File Offset: 0x0001EE74
		[MethodImpl(256)]
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		[MethodImpl(256)]
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x060014EF RID: 5359 RVA: 0x00020CDC File Offset: 0x0001EEDC
		[MethodImpl(256)]
		public static Vector2 operator *(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x060014F0 RID: 5360 RVA: 0x00020D10 File Offset: 0x0001EF10
		[MethodImpl(256)]
		public static Vector2 operator /(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x / b.x, a.y / b.y);
		}

		// Token: 0x060014F1 RID: 5361 RVA: 0x00020D44 File Offset: 0x0001EF44
		[MethodImpl(256)]
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x060014F2 RID: 5362 RVA: 0x00020D6C File Offset: 0x0001EF6C
		[MethodImpl(256)]
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060014F3 RID: 5363 RVA: 0x00020D94 File Offset: 0x0001EF94
		[MethodImpl(256)]
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x060014F4 RID: 5364 RVA: 0x00020DBC File Offset: 0x0001EFBC
		[MethodImpl(256)]
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x00020DE4 File Offset: 0x0001EFE4
		[MethodImpl(256)]
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			return num * num + num2 * num2 < 9.9999994E-11f;
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x00020E20 File Offset: 0x0001F020
		[MethodImpl(256)]
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014F7 RID: 5367 RVA: 0x00020E3C File Offset: 0x0001F03C
		[MethodImpl(256)]
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x060014F8 RID: 5368 RVA: 0x00020E60 File Offset: 0x0001F060
		[MethodImpl(256)]
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x00020E88 File Offset: 0x0001F088
		public static Vector2 zero
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.zeroVector;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060014FA RID: 5370 RVA: 0x00020EA0 File Offset: 0x0001F0A0
		public static Vector2 one
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.oneVector;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x00020EB8 File Offset: 0x0001F0B8
		public static Vector2 up
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.upVector;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060014FC RID: 5372 RVA: 0x00020ED0 File Offset: 0x0001F0D0
		public static Vector2 down
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.downVector;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00020EE8 File Offset: 0x0001F0E8
		public static Vector2 left
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.leftVector;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00020F00 File Offset: 0x0001F100
		public static Vector2 right
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.rightVector;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00020F18 File Offset: 0x0001F118
		public static Vector2 positiveInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.positiveInfinityVector;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x00020F30 File Offset: 0x0001F130
		public static Vector2 negativeInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector2.negativeInfinityVector;
			}
		}

		// Token: 0x04000779 RID: 1913
		public float x;

		// Token: 0x0400077A RID: 1914
		public float y;

		// Token: 0x0400077B RID: 1915
		private static readonly Vector2 zeroVector = new Vector2(0f, 0f);

		// Token: 0x0400077C RID: 1916
		private static readonly Vector2 oneVector = new Vector2(1f, 1f);

		// Token: 0x0400077D RID: 1917
		private static readonly Vector2 upVector = new Vector2(0f, 1f);

		// Token: 0x0400077E RID: 1918
		private static readonly Vector2 downVector = new Vector2(0f, -1f);

		// Token: 0x0400077F RID: 1919
		private static readonly Vector2 leftVector = new Vector2(-1f, 0f);

		// Token: 0x04000780 RID: 1920
		private static readonly Vector2 rightVector = new Vector2(1f, 0f);

		// Token: 0x04000781 RID: 1921
		private static readonly Vector2 positiveInfinityVector = new Vector2(float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x04000782 RID: 1922
		private static readonly Vector2 negativeInfinityVector = new Vector2(float.NegativeInfinity, float.NegativeInfinity);

		// Token: 0x04000783 RID: 1923
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000784 RID: 1924
		public const float kEpsilonNormalSqrt = 1E-15f;
	}
}
