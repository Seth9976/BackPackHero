using System;

namespace Steamworks
{
	// Token: 0x0200019F RID: 415
	[Serializable]
	public struct SteamItemDef_t : IEquatable<SteamItemDef_t>, IComparable<SteamItemDef_t>
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0000F060 File Offset: 0x0000D260
		public SteamItemDef_t(int value)
		{
			this.m_SteamItemDef = value;
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0000F069 File Offset: 0x0000D269
		public override string ToString()
		{
			return this.m_SteamItemDef.ToString();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0000F076 File Offset: 0x0000D276
		public override bool Equals(object other)
		{
			return other is SteamItemDef_t && this == (SteamItemDef_t)other;
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0000F093 File Offset: 0x0000D293
		public override int GetHashCode()
		{
			return this.m_SteamItemDef.GetHashCode();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0000F0A0 File Offset: 0x0000D2A0
		public static bool operator ==(SteamItemDef_t x, SteamItemDef_t y)
		{
			return x.m_SteamItemDef == y.m_SteamItemDef;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0000F0B0 File Offset: 0x0000D2B0
		public static bool operator !=(SteamItemDef_t x, SteamItemDef_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		public static explicit operator SteamItemDef_t(int value)
		{
			return new SteamItemDef_t(value);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0000F0C4 File Offset: 0x0000D2C4
		public static explicit operator int(SteamItemDef_t that)
		{
			return that.m_SteamItemDef;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		public bool Equals(SteamItemDef_t other)
		{
			return this.m_SteamItemDef == other.m_SteamItemDef;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public int CompareTo(SteamItemDef_t other)
		{
			return this.m_SteamItemDef.CompareTo(other.m_SteamItemDef);
		}

		// Token: 0x04000A6F RID: 2671
		public int m_SteamItemDef;
	}
}
