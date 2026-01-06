using System;

namespace Steamworks
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public struct SteamNetworkingMicroseconds : IEquatable<SteamNetworkingMicroseconds>, IComparable<SteamNetworkingMicroseconds>
	{
		// Token: 0x06000A85 RID: 2693 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public SteamNetworkingMicroseconds(long value)
		{
			this.m_SteamNetworkingMicroseconds = value;
		}

		// Token: 0x06000A86 RID: 2694 RVA: 0x0000F835 File Offset: 0x0000DA35
		public override string ToString()
		{
			return this.m_SteamNetworkingMicroseconds.ToString();
		}

		// Token: 0x06000A87 RID: 2695 RVA: 0x0000F842 File Offset: 0x0000DA42
		public override bool Equals(object other)
		{
			return other is SteamNetworkingMicroseconds && this == (SteamNetworkingMicroseconds)other;
		}

		// Token: 0x06000A88 RID: 2696 RVA: 0x0000F85F File Offset: 0x0000DA5F
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingMicroseconds.GetHashCode();
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x0000F86C File Offset: 0x0000DA6C
		public static bool operator ==(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return x.m_SteamNetworkingMicroseconds == y.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000A8A RID: 2698 RVA: 0x0000F87C File Offset: 0x0000DA7C
		public static bool operator !=(SteamNetworkingMicroseconds x, SteamNetworkingMicroseconds y)
		{
			return !(x == y);
		}

		// Token: 0x06000A8B RID: 2699 RVA: 0x0000F888 File Offset: 0x0000DA88
		public static explicit operator SteamNetworkingMicroseconds(long value)
		{
			return new SteamNetworkingMicroseconds(value);
		}

		// Token: 0x06000A8C RID: 2700 RVA: 0x0000F890 File Offset: 0x0000DA90
		public static explicit operator long(SteamNetworkingMicroseconds that)
		{
			return that.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000A8D RID: 2701 RVA: 0x0000F898 File Offset: 0x0000DA98
		public bool Equals(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds == other.m_SteamNetworkingMicroseconds;
		}

		// Token: 0x06000A8E RID: 2702 RVA: 0x0000F8A8 File Offset: 0x0000DAA8
		public int CompareTo(SteamNetworkingMicroseconds other)
		{
			return this.m_SteamNetworkingMicroseconds.CompareTo(other.m_SteamNetworkingMicroseconds);
		}

		// Token: 0x04000A99 RID: 2713
		public long m_SteamNetworkingMicroseconds;
	}
}
