using System;

namespace Steamworks
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public struct HSteamNetPollGroup : IEquatable<HSteamNetPollGroup>, IComparable<HSteamNetPollGroup>
	{
		// Token: 0x06000A55 RID: 2645 RVA: 0x0000F53C File Offset: 0x0000D73C
		public HSteamNetPollGroup(uint value)
		{
			this.m_HSteamNetPollGroup = value;
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x0000F545 File Offset: 0x0000D745
		public override string ToString()
		{
			return this.m_HSteamNetPollGroup.ToString();
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x0000F552 File Offset: 0x0000D752
		public override bool Equals(object other)
		{
			return other is HSteamNetPollGroup && this == (HSteamNetPollGroup)other;
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x0000F56F File Offset: 0x0000D76F
		public override int GetHashCode()
		{
			return this.m_HSteamNetPollGroup.GetHashCode();
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x0000F57C File Offset: 0x0000D77C
		public static bool operator ==(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return x.m_HSteamNetPollGroup == y.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x0000F58C File Offset: 0x0000D78C
		public static bool operator !=(HSteamNetPollGroup x, HSteamNetPollGroup y)
		{
			return !(x == y);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x0000F598 File Offset: 0x0000D798
		public static explicit operator HSteamNetPollGroup(uint value)
		{
			return new HSteamNetPollGroup(value);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public static explicit operator uint(HSteamNetPollGroup that)
		{
			return that.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public bool Equals(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup == other.m_HSteamNetPollGroup;
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		public int CompareTo(HSteamNetPollGroup other)
		{
			return this.m_HSteamNetPollGroup.CompareTo(other.m_HSteamNetPollGroup);
		}

		// Token: 0x04000A7C RID: 2684
		public static readonly HSteamNetPollGroup Invalid = new HSteamNetPollGroup(0U);

		// Token: 0x04000A7D RID: 2685
		public uint m_HSteamNetPollGroup;
	}
}
