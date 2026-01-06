using System;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x0200005C RID: 92
	public struct IntBounds
	{
		// Token: 0x06000352 RID: 850 RVA: 0x0000F90C File Offset: 0x0000DB0C
		public IntBounds(int xmin, int ymin, int zmin, int xmax, int ymax, int zmax)
		{
			this.min = new int3(xmin, ymin, zmin);
			this.max = new int3(xmax, ymax, zmax);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000F92D File Offset: 0x0000DB2D
		public IntBounds(int3 min, int3 max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000354 RID: 852 RVA: 0x0000F93D File Offset: 0x0000DB3D
		public int3 size
		{
			get
			{
				return this.max - this.min;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000F950 File Offset: 0x0000DB50
		public int volume
		{
			get
			{
				int3 size = this.size;
				return size.x * size.y * size.z;
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000F978 File Offset: 0x0000DB78
		public static IntBounds Intersection(IntBounds a, IntBounds b)
		{
			return new IntBounds(math.max(a.min, b.min), math.min(a.max, b.max));
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000F9A1 File Offset: 0x0000DBA1
		public IntBounds Offset(int3 offset)
		{
			return new IntBounds(this.min + offset, this.max + offset);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000F9C0 File Offset: 0x0000DBC0
		public bool Contains(IntBounds other)
		{
			return math.all((other.min >= this.min) & (other.max <= this.max));
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000F9F0 File Offset: 0x0000DBF0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"(",
				this.min.ToString(),
				" <= x < ",
				this.max.ToString(),
				")"
			});
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FA48 File Offset: 0x0000DC48
		public override bool Equals(object _b)
		{
			IntBounds intBounds = (IntBounds)_b;
			return this == intBounds;
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000FA68 File Offset: 0x0000DC68
		public override int GetHashCode()
		{
			return this.min.GetHashCode() ^ (this.max.GetHashCode() << 2);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FA8F File Offset: 0x0000DC8F
		public static bool operator ==(IntBounds a, IntBounds b)
		{
			return math.all((a.min == b.min) & (a.max == b.max));
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FABD File Offset: 0x0000DCBD
		public static bool operator !=(IntBounds a, IntBounds b)
		{
			return !(a == b);
		}

		// Token: 0x040001F0 RID: 496
		public int3 min;

		// Token: 0x040001F1 RID: 497
		public int3 max;
	}
}
