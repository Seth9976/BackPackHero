using System;

namespace Steamworks
{
	// Token: 0x020001C1 RID: 449
	[Serializable]
	public struct SteamLeaderboardEntries_t : IEquatable<SteamLeaderboardEntries_t>, IComparable<SteamLeaderboardEntries_t>
	{
		// Token: 0x06000B35 RID: 2869 RVA: 0x00010394 File Offset: 0x0000E594
		public SteamLeaderboardEntries_t(ulong value)
		{
			this.m_SteamLeaderboardEntries = value;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x0001039D File Offset: 0x0000E59D
		public override string ToString()
		{
			return this.m_SteamLeaderboardEntries.ToString();
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000103AA File Offset: 0x0000E5AA
		public override bool Equals(object other)
		{
			return other is SteamLeaderboardEntries_t && this == (SteamLeaderboardEntries_t)other;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000103C7 File Offset: 0x0000E5C7
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboardEntries.GetHashCode();
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000103D4 File Offset: 0x0000E5D4
		public static bool operator ==(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return x.m_SteamLeaderboardEntries == y.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000103E4 File Offset: 0x0000E5E4
		public static bool operator !=(SteamLeaderboardEntries_t x, SteamLeaderboardEntries_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000103F0 File Offset: 0x0000E5F0
		public static explicit operator SteamLeaderboardEntries_t(ulong value)
		{
			return new SteamLeaderboardEntries_t(value);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000103F8 File Offset: 0x0000E5F8
		public static explicit operator ulong(SteamLeaderboardEntries_t that)
		{
			return that.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00010400 File Offset: 0x0000E600
		public bool Equals(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries == other.m_SteamLeaderboardEntries;
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x00010410 File Offset: 0x0000E610
		public int CompareTo(SteamLeaderboardEntries_t other)
		{
			return this.m_SteamLeaderboardEntries.CompareTo(other.m_SteamLeaderboardEntries);
		}

		// Token: 0x04000AB7 RID: 2743
		public ulong m_SteamLeaderboardEntries;
	}
}
