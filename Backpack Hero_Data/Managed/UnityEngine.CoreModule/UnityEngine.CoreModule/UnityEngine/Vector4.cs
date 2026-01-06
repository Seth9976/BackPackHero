using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	[Il2CppEagerStaticClassConstruction]
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeClass("Vector4f")]
	[NativeHeader("Runtime/Math/Vector4.h")]
	public struct Vector4 : IEquatable<Vector4>, IFormattable
	{
		// Token: 0x17000449 RID: 1097
		public float this[int index]
		{
			[MethodImpl(256)]
			get
			{
				float num;
				switch (index)
				{
				case 0:
					num = this.x;
					break;
				case 1:
					num = this.y;
					break;
				case 2:
					num = this.z;
					break;
				case 3:
					num = this.w;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
				return num;
			}
			[MethodImpl(256)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x00022195 File Offset: 0x00020395
		[MethodImpl(256)]
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x000221B5 File Offset: 0x000203B5
		[MethodImpl(256)]
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000221D8 File Offset: 0x000203D8
		[MethodImpl(256)]
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x00022195 File Offset: 0x00020395
		[MethodImpl(256)]
		public void Set(float newX, float newY, float newZ, float newW)
		{
			this.x = newX;
			this.y = newY;
			this.z = newZ;
			this.w = newW;
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00022200 File Offset: 0x00020400
		[MethodImpl(256)]
		public static Vector4 Lerp(Vector4 a, Vector4 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001564 RID: 5476 RVA: 0x00022278 File Offset: 0x00020478
		[MethodImpl(256)]
		public static Vector4 LerpUnclamped(Vector4 a, Vector4 b, float t)
		{
			return new Vector4(a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t);
		}

		// Token: 0x06001565 RID: 5477 RVA: 0x000222E8 File Offset: 0x000204E8
		[MethodImpl(256)]
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			float num = target.x - current.x;
			float num2 = target.y - current.y;
			float num3 = target.z - current.z;
			float num4 = target.w - current.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			bool flag = num5 == 0f || (maxDistanceDelta >= 0f && num5 <= maxDistanceDelta * maxDistanceDelta);
			Vector4 vector;
			if (flag)
			{
				vector = target;
			}
			else
			{
				float num6 = (float)Math.Sqrt((double)num5);
				vector = new Vector4(current.x + num / num6 * maxDistanceDelta, current.y + num2 / num6 * maxDistanceDelta, current.z + num3 / num6 * maxDistanceDelta, current.w + num4 / num6 * maxDistanceDelta);
			}
			return vector;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x000223B8 File Offset: 0x000205B8
		[MethodImpl(256)]
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x00022404 File Offset: 0x00020604
		[MethodImpl(256)]
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x00022460 File Offset: 0x00020660
		[MethodImpl(256)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x000224A8 File Offset: 0x000206A8
		[MethodImpl(256)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector4);
			return !flag && this.Equals((Vector4)other);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x000224DC File Offset: 0x000206DC
		[MethodImpl(256)]
		public bool Equals(Vector4 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z && this.w == other.w;
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0002252C File Offset: 0x0002072C
		[MethodImpl(256)]
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			bool flag = num > 1E-05f;
			Vector4 vector;
			if (flag)
			{
				vector = a / num;
			}
			else
			{
				vector = Vector4.zero;
			}
			return vector;
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00022560 File Offset: 0x00020760
		[MethodImpl(256)]
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			bool flag = num > 1E-05f;
			if (flag)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x000225A8 File Offset: 0x000207A8
		public Vector4 normalized
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x000225C8 File Offset: 0x000207C8
		[MethodImpl(256)]
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00022614 File Offset: 0x00020814
		[MethodImpl(256)]
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * (Vector4.Dot(a, b) / Vector4.Dot(b, b));
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0002263C File Offset: 0x0002083C
		[MethodImpl(256)]
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0002265C File Offset: 0x0002085C
		[MethodImpl(256)]
		public static float Magnitude(Vector4 a)
		{
			return (float)Math.Sqrt((double)Vector4.Dot(a, a));
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06001572 RID: 5490 RVA: 0x0002267C File Offset: 0x0002087C
		public float magnitude
		{
			[MethodImpl(256)]
			get
			{
				return (float)Math.Sqrt((double)Vector4.Dot(this, this));
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x000226A8 File Offset: 0x000208A8
		public float sqrMagnitude
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x000226CC File Offset: 0x000208CC
		[MethodImpl(256)]
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00022728 File Offset: 0x00020928
		[MethodImpl(256)]
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x00022784 File Offset: 0x00020984
		public static Vector4 zero
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.zeroVector;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001577 RID: 5495 RVA: 0x0002279C File Offset: 0x0002099C
		public static Vector4 one
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.oneVector;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x000227B4 File Offset: 0x000209B4
		public static Vector4 positiveInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.positiveInfinityVector;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001579 RID: 5497 RVA: 0x000227CC File Offset: 0x000209CC
		public static Vector4 negativeInfinity
		{
			[MethodImpl(256)]
			get
			{
				return Vector4.negativeInfinityVector;
			}
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x000227E4 File Offset: 0x000209E4
		[MethodImpl(256)]
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00022830 File Offset: 0x00020A30
		[MethodImpl(256)]
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0002287C File Offset: 0x00020A7C
		[MethodImpl(256)]
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x000228B0 File Offset: 0x00020AB0
		[MethodImpl(256)]
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x000228E8 File Offset: 0x00020AE8
		[MethodImpl(256)]
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x00022920 File Offset: 0x00020B20
		[MethodImpl(256)]
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x00022958 File Offset: 0x00020B58
		[MethodImpl(256)]
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			float num = lhs.x - rhs.x;
			float num2 = lhs.y - rhs.y;
			float num3 = lhs.z - rhs.z;
			float num4 = lhs.w - rhs.w;
			float num5 = num * num + num2 * num2 + num3 * num3 + num4 * num4;
			return num5 < 9.9999994E-11f;
		}

		// Token: 0x06001581 RID: 5505 RVA: 0x000229C0 File Offset: 0x00020BC0
		[MethodImpl(256)]
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001582 RID: 5506 RVA: 0x000229DC File Offset: 0x00020BDC
		[MethodImpl(256)]
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06001583 RID: 5507 RVA: 0x00022A0C File Offset: 0x00020C0C
		[MethodImpl(256)]
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06001584 RID: 5508 RVA: 0x00022A38 File Offset: 0x00020C38
		[MethodImpl(256)]
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06001585 RID: 5509 RVA: 0x00022A68 File Offset: 0x00020C68
		[MethodImpl(256)]
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00022A8C File Offset: 0x00020C8C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00022AA8 File Offset: 0x00020CA8
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00022AC4 File Offset: 0x00020CC4
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
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider),
				this.w.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x00022B4C File Offset: 0x00020D4C
		[MethodImpl(256)]
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x0600158A RID: 5514 RVA: 0x00022B68 File Offset: 0x00020D68
		[MethodImpl(256)]
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x04000798 RID: 1944
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000799 RID: 1945
		public float x;

		// Token: 0x0400079A RID: 1946
		public float y;

		// Token: 0x0400079B RID: 1947
		public float z;

		// Token: 0x0400079C RID: 1948
		public float w;

		// Token: 0x0400079D RID: 1949
		private static readonly Vector4 zeroVector = new Vector4(0f, 0f, 0f, 0f);

		// Token: 0x0400079E RID: 1950
		private static readonly Vector4 oneVector = new Vector4(1f, 1f, 1f, 1f);

		// Token: 0x0400079F RID: 1951
		private static readonly Vector4 positiveInfinityVector = new Vector4(float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity, float.PositiveInfinity);

		// Token: 0x040007A0 RID: 1952
		private static readonly Vector4 negativeInfinityVector = new Vector4(float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity, float.NegativeInfinity);
	}
}
