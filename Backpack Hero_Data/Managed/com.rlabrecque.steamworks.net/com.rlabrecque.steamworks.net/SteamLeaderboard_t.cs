using System;

namespace Steamworks
{
	// Token: 0x020001C2 RID: 450
	[Serializable]
	public struct SteamLeaderboard_t : IEquatable<SteamLeaderboard_t>, IComparable<SteamLeaderboard_t>
	{
		// Token: 0x06000B3F RID: 2879 RVA: 0x00010423 File Offset: 0x0000E623
		public SteamLeaderboard_t(ulong value)
		{
			this.m_SteamLeaderboard = value;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001042C File Offset: 0x0000E62C
		public override string ToString()
		{
			return this.m_SteamLeaderboard.ToString();
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00010439 File Offset: 0x0000E639
		public override bool Equals(object other)
		{
			return other is SteamLeaderboard_t && this == (SteamLeaderboard_t)other;
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00010456 File Offset: 0x0000E656
		public override int GetHashCode()
		{
			return this.m_SteamLeaderboard.GetHashCode();
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x00010463 File Offset: 0x0000E663
		public static bool operator ==(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return x.m_SteamLeaderboard == y.m_SteamLeaderboard;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00010473 File Offset: 0x0000E673
		public static bool operator !=(SteamLeaderboard_t x, SteamLeaderboard_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x0001047F File Offset: 0x0000E67F
		public static explicit operator SteamLeaderboard_t(ulong value)
		{
			return new SteamLeaderboard_t(value);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00010487 File Offset: 0x0000E687
		public static explicit operator ulong(SteamLeaderboard_t that)
		{
			return that.m_SteamLeaderboard;
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001048F File Offset: 0x0000E68F
		public bool Equals(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard == other.m_SteamLeaderboard;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x0001049F File Offset: 0x0000E69F
		public int CompareTo(SteamLeaderboard_t other)
		{
			return this.m_SteamLeaderboard.CompareTo(other.m_SteamLeaderboard);
		}

		// Token: 0x04000AB8 RID: 2744
		public ulong m_SteamLeaderboard;
	}
}
