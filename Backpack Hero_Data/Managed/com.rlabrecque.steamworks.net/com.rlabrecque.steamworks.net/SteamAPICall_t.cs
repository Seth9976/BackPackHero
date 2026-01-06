using System;

namespace Steamworks
{
	// Token: 0x020001BD RID: 445
	[Serializable]
	public struct SteamAPICall_t : IEquatable<SteamAPICall_t>, IComparable<SteamAPICall_t>
	{
		// Token: 0x06000B0F RID: 2831 RVA: 0x0000FFDC File Offset: 0x0000E1DC
		public SteamAPICall_t(ulong value)
		{
			this.m_SteamAPICall = value;
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0000FFE5 File Offset: 0x0000E1E5
		public override string ToString()
		{
			return this.m_SteamAPICall.ToString();
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
		public override bool Equals(object other)
		{
			return other is SteamAPICall_t && this == (SteamAPICall_t)other;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0001000F File Offset: 0x0000E20F
		public override int GetHashCode()
		{
			return this.m_SteamAPICall.GetHashCode();
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0001001C File Offset: 0x0000E21C
		public static bool operator ==(SteamAPICall_t x, SteamAPICall_t y)
		{
			return x.m_SteamAPICall == y.m_SteamAPICall;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001002C File Offset: 0x0000E22C
		public static bool operator !=(SteamAPICall_t x, SteamAPICall_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x00010038 File Offset: 0x0000E238
		public static explicit operator SteamAPICall_t(ulong value)
		{
			return new SteamAPICall_t(value);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x00010040 File Offset: 0x0000E240
		public static explicit operator ulong(SteamAPICall_t that)
		{
			return that.m_SteamAPICall;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x00010048 File Offset: 0x0000E248
		public bool Equals(SteamAPICall_t other)
		{
			return this.m_SteamAPICall == other.m_SteamAPICall;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00010058 File Offset: 0x0000E258
		public int CompareTo(SteamAPICall_t other)
		{
			return this.m_SteamAPICall.CompareTo(other.m_SteamAPICall);
		}

		// Token: 0x04000AAE RID: 2734
		public static readonly SteamAPICall_t Invalid = new SteamAPICall_t(0UL);

		// Token: 0x04000AAF RID: 2735
		public ulong m_SteamAPICall;
	}
}
