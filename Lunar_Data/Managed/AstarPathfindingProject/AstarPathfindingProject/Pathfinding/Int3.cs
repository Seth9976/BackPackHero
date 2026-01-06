using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200005A RID: 90
	public struct Int3 : IEquatable<Int3>
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000F098 File Offset: 0x0000D298
		public static Int3 zero
		{
			get
			{
				return default(Int3);
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		[MethodImpl(256)]
		public Int3(Vector3 position)
		{
			this.x = (int)Math.Round((double)(position.x * 1000f));
			this.y = (int)Math.Round((double)(position.y * 1000f));
			this.z = (int)Math.Round((double)(position.z * 1000f));
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000F108 File Offset: 0x0000D308
		[MethodImpl(256)]
		public Int3(int _x, int _y, int _z)
		{
			this.x = _x;
			this.y = _y;
			this.z = _z;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000F11F File Offset: 0x0000D31F
		[MethodImpl(256)]
		public static bool operator ==(Int3 lhs, Int3 rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000F14D File Offset: 0x0000D34D
		[MethodImpl(256)]
		public static bool operator !=(Int3 lhs, Int3 rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.z != rhs.z;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000F17E File Offset: 0x0000D37E
		[MethodImpl(256)]
		public static explicit operator Int3(Vector3 ob)
		{
			return new Int3((int)Math.Round((double)(ob.x * 1000f)), (int)Math.Round((double)(ob.y * 1000f)), (int)Math.Round((double)(ob.z * 1000f)));
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0000F1BE File Offset: 0x0000D3BE
		[MethodImpl(256)]
		public static explicit operator Vector3(Int3 ob)
		{
			return new Vector3((float)ob.x * 0.001f, (float)ob.y * 0.001f, (float)ob.z * 0.001f);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000F1EC File Offset: 0x0000D3EC
		[MethodImpl(256)]
		public static explicit operator float3(Int3 ob)
		{
			return (int3)ob * 0.001f;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000F203 File Offset: 0x0000D403
		[MethodImpl(256)]
		public static explicit operator int3(Int3 ob)
		{
			return new int3(ob.x, ob.y, ob.z);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000F21C File Offset: 0x0000D41C
		[MethodImpl(256)]
		public static Int3 operator -(Int3 lhs, Int3 rhs)
		{
			lhs.x -= rhs.x;
			lhs.y -= rhs.y;
			lhs.z -= rhs.z;
			return lhs;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0000F252 File Offset: 0x0000D452
		[MethodImpl(256)]
		public static Int3 operator -(Int3 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			lhs.z = -lhs.z;
			return lhs;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000F27F File Offset: 0x0000D47F
		[MethodImpl(256)]
		public static Int3 operator +(Int3 lhs, Int3 rhs)
		{
			lhs.x += rhs.x;
			lhs.y += rhs.y;
			lhs.z += rhs.z;
			return lhs;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000F2B5 File Offset: 0x0000D4B5
		[MethodImpl(256)]
		public static Int3 operator *(Int3 lhs, int rhs)
		{
			lhs.x *= rhs;
			lhs.y *= rhs;
			lhs.z *= rhs;
			return lhs;
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0000F2DC File Offset: 0x0000D4DC
		public static Int3 operator *(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x * rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y * rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z * rhs));
			return lhs;
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0000F330 File Offset: 0x0000D530
		public static Int3 operator *(Int3 lhs, double rhs)
		{
			lhs.x = (int)Math.Round((double)lhs.x * rhs);
			lhs.y = (int)Math.Round((double)lhs.y * rhs);
			lhs.z = (int)Math.Round((double)lhs.z * rhs);
			return lhs;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000F380 File Offset: 0x0000D580
		public static Int3 operator /(Int3 lhs, float rhs)
		{
			lhs.x = (int)Math.Round((double)((float)lhs.x / rhs));
			lhs.y = (int)Math.Round((double)((float)lhs.y / rhs));
			lhs.z = (int)Math.Round((double)((float)lhs.z / rhs));
			return lhs;
		}

		// Token: 0x1700009E RID: 158
		public int this[int i]
		{
			[MethodImpl(256)]
			get
			{
				if (i == 0)
				{
					return this.x;
				}
				if (i != 1)
				{
					return this.z;
				}
				return this.y;
			}
			[MethodImpl(256)]
			set
			{
				if (i == 0)
				{
					this.x = value;
					return;
				}
				if (i == 1)
				{
					this.y = value;
					return;
				}
				this.z = value;
			}
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000F410 File Offset: 0x0000D610
		[MethodImpl(256)]
		public static Int3 Max(Int3 lhs, Int3 rhs)
		{
			return new Int3(Math.Max(lhs.x, rhs.x), Math.Max(lhs.y, rhs.y), Math.Max(lhs.z, rhs.z));
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000F44A File Offset: 0x0000D64A
		[MethodImpl(256)]
		public static Int3 Min(Int3 lhs, Int3 rhs)
		{
			return new Int3(Math.Min(lhs.x, rhs.x), Math.Min(lhs.y, rhs.y), Math.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000F484 File Offset: 0x0000D684
		public static float Angle(Int3 lhs, Int3 rhs)
		{
			double num = (double)Int3.Dot(lhs, rhs) / ((double)lhs.magnitude * (double)rhs.magnitude);
			num = ((num < -1.0) ? (-1.0) : ((num > 1.0) ? 1.0 : num));
			return (float)Math.Acos(num);
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000F4E3 File Offset: 0x0000D6E3
		public static int Dot(Int3 lhs, Int3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000F50E File Offset: 0x0000D70E
		public static long DotLong(Int3 lhs, Int3 rhs)
		{
			return (long)lhs.x * (long)rhs.x + (long)lhs.y * (long)rhs.y + (long)lhs.z * (long)rhs.z;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000F53F File Offset: 0x0000D73F
		public Int3 Normal2D()
		{
			return new Int3(this.z, this.y, -this.x);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0000F55C File Offset: 0x0000D75C
		public float magnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)Math.Sqrt(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000F590 File Offset: 0x0000D790
		public int costMagnitude
		{
			get
			{
				return (int)Math.Round((double)this.magnitude);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public float sqrMagnitude
		{
			get
			{
				double num = (double)this.x;
				double num2 = (double)this.y;
				double num3 = (double)this.z;
				return (float)(num * num + num2 * num2 + num3 * num3);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000F5D0 File Offset: 0x0000D7D0
		public long sqrMagnitudeLong
		{
			get
			{
				long num = (long)this.x;
				long num2 = (long)this.y;
				long num3 = (long)this.z;
				return num * num + num2 * num2 + num3 * num3;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000F5FE File Offset: 0x0000D7FE
		public static implicit operator string(Int3 obj)
		{
			return obj.ToString();
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000F610 File Offset: 0x0000D810
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"( ",
				this.x.ToString(),
				", ",
				this.y.ToString(),
				", ",
				this.z.ToString(),
				")"
			});
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000F674 File Offset: 0x0000D874
		public override bool Equals(object obj)
		{
			if (!(obj is Int3))
			{
				return false;
			}
			Int3 @int = (Int3)obj;
			return this.x == @int.x && this.y == @int.y && this.z == @int.z;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000F11F File Offset: 0x0000D31F
		public bool Equals(Int3 other)
		{
			return this.x == other.x && this.y == other.y && this.z == other.z;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000F6BE File Offset: 0x0000D8BE
		public override int GetHashCode()
		{
			return (this.x * 73856093) ^ (this.y * 19349669) ^ (this.z * 83492791);
		}

		// Token: 0x040001E8 RID: 488
		public int x;

		// Token: 0x040001E9 RID: 489
		public int y;

		// Token: 0x040001EA RID: 490
		public int z;

		// Token: 0x040001EB RID: 491
		public const int Precision = 1000;

		// Token: 0x040001EC RID: 492
		public const float FloatPrecision = 1000f;

		// Token: 0x040001ED RID: 493
		public const float PrecisionFactor = 0.001f;
	}
}
