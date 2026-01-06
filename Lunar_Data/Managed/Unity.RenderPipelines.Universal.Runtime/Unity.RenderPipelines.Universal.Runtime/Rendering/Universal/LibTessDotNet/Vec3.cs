using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000F3 RID: 243
	internal struct Vec3
	{
		// Token: 0x170001C6 RID: 454
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.X;
				}
				if (index == 1)
				{
					return this.Y;
				}
				if (index == 2)
				{
					return this.Z;
				}
				throw new IndexOutOfRangeException();
			}
			set
			{
				if (index == 0)
				{
					this.X = value;
					return;
				}
				if (index == 1)
				{
					this.Y = value;
					return;
				}
				if (index == 2)
				{
					this.Z = value;
					return;
				}
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x00027FC6 File Offset: 0x000261C6
		public static void Sub(ref Vec3 lhs, ref Vec3 rhs, out Vec3 result)
		{
			result.X = lhs.X - rhs.X;
			result.Y = lhs.Y - rhs.Y;
			result.Z = lhs.Z - rhs.Z;
		}

		// Token: 0x06000707 RID: 1799 RVA: 0x00028001 File Offset: 0x00026201
		public static void Neg(ref Vec3 v)
		{
			v.X = -v.X;
			v.Y = -v.Y;
			v.Z = -v.Z;
		}

		// Token: 0x06000708 RID: 1800 RVA: 0x0002802A File Offset: 0x0002622A
		public static void Dot(ref Vec3 u, ref Vec3 v, out float dot)
		{
			dot = u.X * v.X + u.Y * v.Y + u.Z * v.Z;
		}

		// Token: 0x06000709 RID: 1801 RVA: 0x00028058 File Offset: 0x00026258
		public static void Normalize(ref Vec3 v)
		{
			float num = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
			num = 1f / (float)Math.Sqrt((double)num);
			v.X *= num;
			v.Y *= num;
			v.Z *= num;
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x000280C0 File Offset: 0x000262C0
		public static int LongAxis(ref Vec3 v)
		{
			int num = 0;
			if (Math.Abs(v.Y) > Math.Abs(v.X))
			{
				num = 1;
			}
			if (Math.Abs(v.Z) > Math.Abs((num == 0) ? v.X : v.Y))
			{
				num = 2;
			}
			return num;
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0002810F File Offset: 0x0002630F
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.X, this.Y, this.Z);
		}

		// Token: 0x040006A9 RID: 1705
		public static readonly Vec3 Zero;

		// Token: 0x040006AA RID: 1706
		public float X;

		// Token: 0x040006AB RID: 1707
		public float Y;

		// Token: 0x040006AC RID: 1708
		public float Z;
	}
}
