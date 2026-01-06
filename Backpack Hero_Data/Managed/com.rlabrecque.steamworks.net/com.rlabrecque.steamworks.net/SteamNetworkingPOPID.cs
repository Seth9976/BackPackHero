using System;

namespace Steamworks
{
	// Token: 0x020001B1 RID: 433
	[Serializable]
	public struct SteamNetworkingPOPID : IEquatable<SteamNetworkingPOPID>, IComparable<SteamNetworkingPOPID>
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x0000F8BB File Offset: 0x0000DABB
		public SteamNetworkingPOPID(uint value)
		{
			this.m_SteamNetworkingPOPID = value;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x0000F8C4 File Offset: 0x0000DAC4
		public override string ToString()
		{
			return this.m_SteamNetworkingPOPID.ToString();
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x0000F8D1 File Offset: 0x0000DAD1
		public override bool Equals(object other)
		{
			return other is SteamNetworkingPOPID && this == (SteamNetworkingPOPID)other;
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x0000F8EE File Offset: 0x0000DAEE
		public override int GetHashCode()
		{
			return this.m_SteamNetworkingPOPID.GetHashCode();
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0000F8FB File Offset: 0x0000DAFB
		public static bool operator ==(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return x.m_SteamNetworkingPOPID == y.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0000F90B File Offset: 0x0000DB0B
		public static bool operator !=(SteamNetworkingPOPID x, SteamNetworkingPOPID y)
		{
			return !(x == y);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0000F917 File Offset: 0x0000DB17
		public static explicit operator SteamNetworkingPOPID(uint value)
		{
			return new SteamNetworkingPOPID(value);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0000F91F File Offset: 0x0000DB1F
		public static explicit operator uint(SteamNetworkingPOPID that)
		{
			return that.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0000F927 File Offset: 0x0000DB27
		public bool Equals(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID == other.m_SteamNetworkingPOPID;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x0000F937 File Offset: 0x0000DB37
		public int CompareTo(SteamNetworkingPOPID other)
		{
			return this.m_SteamNetworkingPOPID.CompareTo(other.m_SteamNetworkingPOPID);
		}

		// Token: 0x04000A9A RID: 2714
		public uint m_SteamNetworkingPOPID;
	}
}
