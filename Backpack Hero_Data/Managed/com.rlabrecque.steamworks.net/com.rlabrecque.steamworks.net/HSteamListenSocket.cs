using System;

namespace Steamworks
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public struct HSteamListenSocket : IEquatable<HSteamListenSocket>, IComparable<HSteamListenSocket>
	{
		// Token: 0x06000A3F RID: 2623 RVA: 0x0000F404 File Offset: 0x0000D604
		public HSteamListenSocket(uint value)
		{
			this.m_HSteamListenSocket = value;
		}

		// Token: 0x06000A40 RID: 2624 RVA: 0x0000F40D File Offset: 0x0000D60D
		public override string ToString()
		{
			return this.m_HSteamListenSocket.ToString();
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0000F41A File Offset: 0x0000D61A
		public override bool Equals(object other)
		{
			return other is HSteamListenSocket && this == (HSteamListenSocket)other;
		}

		// Token: 0x06000A42 RID: 2626 RVA: 0x0000F437 File Offset: 0x0000D637
		public override int GetHashCode()
		{
			return this.m_HSteamListenSocket.GetHashCode();
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x0000F444 File Offset: 0x0000D644
		public static bool operator ==(HSteamListenSocket x, HSteamListenSocket y)
		{
			return x.m_HSteamListenSocket == y.m_HSteamListenSocket;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x0000F454 File Offset: 0x0000D654
		public static bool operator !=(HSteamListenSocket x, HSteamListenSocket y)
		{
			return !(x == y);
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0000F460 File Offset: 0x0000D660
		public static explicit operator HSteamListenSocket(uint value)
		{
			return new HSteamListenSocket(value);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x0000F468 File Offset: 0x0000D668
		public static explicit operator uint(HSteamListenSocket that)
		{
			return that.m_HSteamListenSocket;
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0000F470 File Offset: 0x0000D670
		public bool Equals(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket == other.m_HSteamListenSocket;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x0000F480 File Offset: 0x0000D680
		public int CompareTo(HSteamListenSocket other)
		{
			return this.m_HSteamListenSocket.CompareTo(other.m_HSteamListenSocket);
		}

		// Token: 0x04000A78 RID: 2680
		public static readonly HSteamListenSocket Invalid = new HSteamListenSocket(0U);

		// Token: 0x04000A79 RID: 2681
		public uint m_HSteamListenSocket;
	}
}
