using System;

namespace Steamworks
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public struct PartyBeaconID_t : IEquatable<PartyBeaconID_t>, IComparable<PartyBeaconID_t>
	{
		// Token: 0x06000AFA RID: 2810 RVA: 0x0000FEB0 File Offset: 0x0000E0B0
		public PartyBeaconID_t(ulong value)
		{
			this.m_PartyBeaconID = value;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x0000FEB9 File Offset: 0x0000E0B9
		public override string ToString()
		{
			return this.m_PartyBeaconID.ToString();
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x0000FEC6 File Offset: 0x0000E0C6
		public override bool Equals(object other)
		{
			return other is PartyBeaconID_t && this == (PartyBeaconID_t)other;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x0000FEE3 File Offset: 0x0000E0E3
		public override int GetHashCode()
		{
			return this.m_PartyBeaconID.GetHashCode();
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x0000FEF0 File Offset: 0x0000E0F0
		public static bool operator ==(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return x.m_PartyBeaconID == y.m_PartyBeaconID;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x0000FF00 File Offset: 0x0000E100
		public static bool operator !=(PartyBeaconID_t x, PartyBeaconID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x0000FF0C File Offset: 0x0000E10C
		public static explicit operator PartyBeaconID_t(ulong value)
		{
			return new PartyBeaconID_t(value);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x0000FF14 File Offset: 0x0000E114
		public static explicit operator ulong(PartyBeaconID_t that)
		{
			return that.m_PartyBeaconID;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x0000FF1C File Offset: 0x0000E11C
		public bool Equals(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID == other.m_PartyBeaconID;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x0000FF2C File Offset: 0x0000E12C
		public int CompareTo(PartyBeaconID_t other)
		{
			return this.m_PartyBeaconID.CompareTo(other.m_PartyBeaconID);
		}

		// Token: 0x04000AAB RID: 2731
		public static readonly PartyBeaconID_t Invalid = new PartyBeaconID_t(0UL);

		// Token: 0x04000AAC RID: 2732
		public ulong m_PartyBeaconID;
	}
}
