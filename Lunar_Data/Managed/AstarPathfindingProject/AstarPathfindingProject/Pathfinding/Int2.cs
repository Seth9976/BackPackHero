using System;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x0200005B RID: 91
	public struct Int2 : IEquatable<Int2>
	{
		// Token: 0x06000341 RID: 833 RVA: 0x0000F6E6 File Offset: 0x0000D8E6
		public Int2(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000342 RID: 834 RVA: 0x0000F6F6 File Offset: 0x0000D8F6
		public long sqrMagnitudeLong
		{
			get
			{
				return (long)this.x * (long)this.x + (long)this.y * (long)this.y;
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000F717 File Offset: 0x0000D917
		public static explicit operator int2(Int2 a)
		{
			return new int2(a.x, a.y);
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000F72A File Offset: 0x0000D92A
		public static Int2 operator -(Int2 lhs)
		{
			lhs.x = -lhs.x;
			lhs.y = -lhs.y;
			return lhs;
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000F749 File Offset: 0x0000D949
		public static Int2 operator +(Int2 a, Int2 b)
		{
			return new Int2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000F76A File Offset: 0x0000D96A
		public static Int2 operator -(Int2 a, Int2 b)
		{
			return new Int2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000F78B File Offset: 0x0000D98B
		public static bool operator ==(Int2 a, Int2 b)
		{
			return a.x == b.x && a.y == b.y;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000F7AB File Offset: 0x0000D9AB
		public static bool operator !=(Int2 a, Int2 b)
		{
			return a.x != b.x || a.y != b.y;
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000F7CE File Offset: 0x0000D9CE
		public static long DotLong(Int2 a, Int2 b)
		{
			return (long)a.x * (long)b.x + (long)a.y * (long)b.y;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000F7F0 File Offset: 0x0000D9F0
		public override bool Equals(object o)
		{
			if (!(o is Int2))
			{
				return false;
			}
			Int2 @int = (Int2)o;
			return this.x == @int.x && this.y == @int.y;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F78B File Offset: 0x0000D98B
		public bool Equals(Int2 other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public override int GetHashCode()
		{
			return this.x * 49157 + this.y * 98317;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000F847 File Offset: 0x0000DA47
		public static Int2 Min(Int2 a, Int2 b)
		{
			return new Int2(Math.Min(a.x, b.x), Math.Min(a.y, b.y));
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000F870 File Offset: 0x0000DA70
		public static Int2 Max(Int2 a, Int2 b)
		{
			return new Int2(Math.Max(a.x, b.x), Math.Max(a.y, b.y));
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000F899 File Offset: 0x0000DA99
		public static Int2 FromInt3XZ(Int3 o)
		{
			return new Int2(o.x, o.z);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		public static Int3 ToInt3XZ(Int2 o)
		{
			return new Int3(o.x, 0, o.y);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000F8C0 File Offset: 0x0000DAC0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				this.x.ToString(),
				", ",
				this.y.ToString(),
				")"
			});
		}

		// Token: 0x040001EE RID: 494
		public int x;

		// Token: 0x040001EF RID: 495
		public int y;
	}
}
