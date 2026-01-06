using System;

namespace Steamworks
{
	// Token: 0x020001A0 RID: 416
	[Serializable]
	public struct SteamItemInstanceID_t : IEquatable<SteamItemInstanceID_t>, IComparable<SteamItemInstanceID_t>
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0000F0EF File Offset: 0x0000D2EF
		public SteamItemInstanceID_t(ulong value)
		{
			this.m_SteamItemInstanceID = value;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public override string ToString()
		{
			return this.m_SteamItemInstanceID.ToString();
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0000F105 File Offset: 0x0000D305
		public override bool Equals(object other)
		{
			return other is SteamItemInstanceID_t && this == (SteamItemInstanceID_t)other;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0000F122 File Offset: 0x0000D322
		public override int GetHashCode()
		{
			return this.m_SteamItemInstanceID.GetHashCode();
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0000F12F File Offset: 0x0000D32F
		public static bool operator ==(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return x.m_SteamItemInstanceID == y.m_SteamItemInstanceID;
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0000F13F File Offset: 0x0000D33F
		public static bool operator !=(SteamItemInstanceID_t x, SteamItemInstanceID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0000F14B File Offset: 0x0000D34B
		public static explicit operator SteamItemInstanceID_t(ulong value)
		{
			return new SteamItemInstanceID_t(value);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0000F153 File Offset: 0x0000D353
		public static explicit operator ulong(SteamItemInstanceID_t that)
		{
			return that.m_SteamItemInstanceID;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0000F15B File Offset: 0x0000D35B
		public bool Equals(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID == other.m_SteamItemInstanceID;
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0000F16B File Offset: 0x0000D36B
		public int CompareTo(SteamItemInstanceID_t other)
		{
			return this.m_SteamItemInstanceID.CompareTo(other.m_SteamItemInstanceID);
		}

		// Token: 0x04000A70 RID: 2672
		public static readonly SteamItemInstanceID_t Invalid = new SteamItemInstanceID_t(ulong.MaxValue);

		// Token: 0x04000A71 RID: 2673
		public ulong m_SteamItemInstanceID;
	}
}
