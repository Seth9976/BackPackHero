using System;

namespace Steamworks
{
	// Token: 0x020001A9 RID: 425
	[Serializable]
	public struct HSteamNetConnection : IEquatable<HSteamNetConnection>, IComparable<HSteamNetConnection>
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x0000F4A0 File Offset: 0x0000D6A0
		public HSteamNetConnection(uint value)
		{
			this.m_HSteamNetConnection = value;
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x0000F4A9 File Offset: 0x0000D6A9
		public override string ToString()
		{
			return this.m_HSteamNetConnection.ToString();
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0000F4B6 File Offset: 0x0000D6B6
		public override bool Equals(object other)
		{
			return other is HSteamNetConnection && this == (HSteamNetConnection)other;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x0000F4D3 File Offset: 0x0000D6D3
		public override int GetHashCode()
		{
			return this.m_HSteamNetConnection.GetHashCode();
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x0000F4E0 File Offset: 0x0000D6E0
		public static bool operator ==(HSteamNetConnection x, HSteamNetConnection y)
		{
			return x.m_HSteamNetConnection == y.m_HSteamNetConnection;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		public static bool operator !=(HSteamNetConnection x, HSteamNetConnection y)
		{
			return !(x == y);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0000F4FC File Offset: 0x0000D6FC
		public static explicit operator HSteamNetConnection(uint value)
		{
			return new HSteamNetConnection(value);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0000F504 File Offset: 0x0000D704
		public static explicit operator uint(HSteamNetConnection that)
		{
			return that.m_HSteamNetConnection;
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x0000F50C File Offset: 0x0000D70C
		public bool Equals(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection == other.m_HSteamNetConnection;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x0000F51C File Offset: 0x0000D71C
		public int CompareTo(HSteamNetConnection other)
		{
			return this.m_HSteamNetConnection.CompareTo(other.m_HSteamNetConnection);
		}

		// Token: 0x04000A7A RID: 2682
		public static readonly HSteamNetConnection Invalid = new HSteamNetConnection(0U);

		// Token: 0x04000A7B RID: 2683
		public uint m_HSteamNetConnection;
	}
}
