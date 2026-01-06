using System;

namespace Steamworks
{
	// Token: 0x020001A3 RID: 419
	[Serializable]
	public struct SNetListenSocket_t : IEquatable<SNetListenSocket_t>, IComparable<SNetListenSocket_t>
	{
		// Token: 0x06000A23 RID: 2595 RVA: 0x0000F2BB File Offset: 0x0000D4BB
		public SNetListenSocket_t(uint value)
		{
			this.m_SNetListenSocket = value;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
		public override string ToString()
		{
			return this.m_SNetListenSocket.ToString();
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0000F2D1 File Offset: 0x0000D4D1
		public override bool Equals(object other)
		{
			return other is SNetListenSocket_t && this == (SNetListenSocket_t)other;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0000F2EE File Offset: 0x0000D4EE
		public override int GetHashCode()
		{
			return this.m_SNetListenSocket.GetHashCode();
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000F2FB File Offset: 0x0000D4FB
		public static bool operator ==(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return x.m_SNetListenSocket == y.m_SNetListenSocket;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000F30B File Offset: 0x0000D50B
		public static bool operator !=(SNetListenSocket_t x, SNetListenSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000F317 File Offset: 0x0000D517
		public static explicit operator SNetListenSocket_t(uint value)
		{
			return new SNetListenSocket_t(value);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000F31F File Offset: 0x0000D51F
		public static explicit operator uint(SNetListenSocket_t that)
		{
			return that.m_SNetListenSocket;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000F327 File Offset: 0x0000D527
		public bool Equals(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket == other.m_SNetListenSocket;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000F337 File Offset: 0x0000D537
		public int CompareTo(SNetListenSocket_t other)
		{
			return this.m_SNetListenSocket.CompareTo(other.m_SNetListenSocket);
		}

		// Token: 0x04000A76 RID: 2678
		public uint m_SNetListenSocket;
	}
}
