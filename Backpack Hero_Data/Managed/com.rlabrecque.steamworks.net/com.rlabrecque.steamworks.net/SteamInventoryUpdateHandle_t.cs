using System;

namespace Steamworks
{
	// Token: 0x0200019E RID: 414
	[Serializable]
	public struct SteamInventoryUpdateHandle_t : IEquatable<SteamInventoryUpdateHandle_t>, IComparable<SteamInventoryUpdateHandle_t>
	{
		// Token: 0x060009EE RID: 2542 RVA: 0x0000EFC3 File Offset: 0x0000D1C3
		public SteamInventoryUpdateHandle_t(ulong value)
		{
			this.m_SteamInventoryUpdateHandle = value;
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public override string ToString()
		{
			return this.m_SteamInventoryUpdateHandle.ToString();
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0000EFD9 File Offset: 0x0000D1D9
		public override bool Equals(object other)
		{
			return other is SteamInventoryUpdateHandle_t && this == (SteamInventoryUpdateHandle_t)other;
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0000EFF6 File Offset: 0x0000D1F6
		public override int GetHashCode()
		{
			return this.m_SteamInventoryUpdateHandle.GetHashCode();
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0000F003 File Offset: 0x0000D203
		public static bool operator ==(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return x.m_SteamInventoryUpdateHandle == y.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0000F013 File Offset: 0x0000D213
		public static bool operator !=(SteamInventoryUpdateHandle_t x, SteamInventoryUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0000F01F File Offset: 0x0000D21F
		public static explicit operator SteamInventoryUpdateHandle_t(ulong value)
		{
			return new SteamInventoryUpdateHandle_t(value);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0000F027 File Offset: 0x0000D227
		public static explicit operator ulong(SteamInventoryUpdateHandle_t that)
		{
			return that.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0000F02F File Offset: 0x0000D22F
		public bool Equals(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle == other.m_SteamInventoryUpdateHandle;
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0000F03F File Offset: 0x0000D23F
		public int CompareTo(SteamInventoryUpdateHandle_t other)
		{
			return this.m_SteamInventoryUpdateHandle.CompareTo(other.m_SteamInventoryUpdateHandle);
		}

		// Token: 0x04000A6D RID: 2669
		public static readonly SteamInventoryUpdateHandle_t Invalid = new SteamInventoryUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000A6E RID: 2670
		public ulong m_SteamInventoryUpdateHandle;
	}
}
