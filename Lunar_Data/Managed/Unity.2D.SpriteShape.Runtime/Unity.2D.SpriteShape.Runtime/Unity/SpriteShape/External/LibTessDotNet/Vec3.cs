using System;

namespace Unity.SpriteShape.External.LibTessDotNet
{
	// Token: 0x02000007 RID: 7
	internal struct Vec3
	{
		// Token: 0x17000007 RID: 7
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

		// Token: 0x06000030 RID: 48 RVA: 0x0000322A File Offset: 0x0000142A
		public static void Sub(ref Vec3 lhs, ref Vec3 rhs, out Vec3 result)
		{
			result.X = lhs.X - rhs.X;
			result.Y = lhs.Y - rhs.Y;
			result.Z = lhs.Z - rhs.Z;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003265 File Offset: 0x00001465
		public static void Neg(ref Vec3 v)
		{
			v.X = -v.X;
			v.Y = -v.Y;
			v.Z = -v.Z;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000328E File Offset: 0x0000148E
		public static void Dot(ref Vec3 u, ref Vec3 v, out float dot)
		{
			dot = u.X * v.X + u.Y * v.Y + u.Z * v.Z;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000032BC File Offset: 0x000014BC
		public static void Normalize(ref Vec3 v)
		{
			float num = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
			num = 1f / (float)Math.Sqrt((double)num);
			v.X *= num;
			v.Y *= num;
			v.Z *= num;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00003324 File Offset: 0x00001524
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

		// Token: 0x06000035 RID: 53 RVA: 0x00003373 File Offset: 0x00001573
		public override string ToString()
		{
			return string.Format("{0}, {1}, {2}", this.X, this.Y, this.Z);
		}

		// Token: 0x04000018 RID: 24
		public static readonly Vec3 Zero;

		// Token: 0x04000019 RID: 25
		public float X;

		// Token: 0x0400001A RID: 26
		public float Y;

		// Token: 0x0400001B RID: 27
		public float Z;
	}
}
