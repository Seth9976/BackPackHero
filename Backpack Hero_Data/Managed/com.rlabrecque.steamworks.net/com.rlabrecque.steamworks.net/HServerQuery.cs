using System;

namespace Steamworks
{
	// Token: 0x020001A2 RID: 418
	[Serializable]
	public struct HServerQuery : IEquatable<HServerQuery>, IComparable<HServerQuery>
	{
		// Token: 0x06000A18 RID: 2584 RVA: 0x0000F21F File Offset: 0x0000D41F
		public HServerQuery(int value)
		{
			this.m_HServerQuery = value;
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0000F228 File Offset: 0x0000D428
		public override string ToString()
		{
			return this.m_HServerQuery.ToString();
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0000F235 File Offset: 0x0000D435
		public override bool Equals(object other)
		{
			return other is HServerQuery && this == (HServerQuery)other;
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0000F252 File Offset: 0x0000D452
		public override int GetHashCode()
		{
			return this.m_HServerQuery.GetHashCode();
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0000F25F File Offset: 0x0000D45F
		public static bool operator ==(HServerQuery x, HServerQuery y)
		{
			return x.m_HServerQuery == y.m_HServerQuery;
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0000F26F File Offset: 0x0000D46F
		public static bool operator !=(HServerQuery x, HServerQuery y)
		{
			return !(x == y);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0000F27B File Offset: 0x0000D47B
		public static explicit operator HServerQuery(int value)
		{
			return new HServerQuery(value);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0000F283 File Offset: 0x0000D483
		public static explicit operator int(HServerQuery that)
		{
			return that.m_HServerQuery;
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0000F28B File Offset: 0x0000D48B
		public bool Equals(HServerQuery other)
		{
			return this.m_HServerQuery == other.m_HServerQuery;
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0000F29B File Offset: 0x0000D49B
		public int CompareTo(HServerQuery other)
		{
			return this.m_HServerQuery.CompareTo(other.m_HServerQuery);
		}

		// Token: 0x04000A74 RID: 2676
		public static readonly HServerQuery Invalid = new HServerQuery(-1);

		// Token: 0x04000A75 RID: 2677
		public int m_HServerQuery;
	}
}
