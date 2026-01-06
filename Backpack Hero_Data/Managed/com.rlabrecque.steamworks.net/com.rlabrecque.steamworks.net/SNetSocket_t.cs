using System;

namespace Steamworks
{
	// Token: 0x020001A4 RID: 420
	[Serializable]
	public struct SNetSocket_t : IEquatable<SNetSocket_t>, IComparable<SNetSocket_t>
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0000F34A File Offset: 0x0000D54A
		public SNetSocket_t(uint value)
		{
			this.m_SNetSocket = value;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0000F353 File Offset: 0x0000D553
		public override string ToString()
		{
			return this.m_SNetSocket.ToString();
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0000F360 File Offset: 0x0000D560
		public override bool Equals(object other)
		{
			return other is SNetSocket_t && this == (SNetSocket_t)other;
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0000F37D File Offset: 0x0000D57D
		public override int GetHashCode()
		{
			return this.m_SNetSocket.GetHashCode();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0000F38A File Offset: 0x0000D58A
		public static bool operator ==(SNetSocket_t x, SNetSocket_t y)
		{
			return x.m_SNetSocket == y.m_SNetSocket;
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0000F39A File Offset: 0x0000D59A
		public static bool operator !=(SNetSocket_t x, SNetSocket_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0000F3A6 File Offset: 0x0000D5A6
		public static explicit operator SNetSocket_t(uint value)
		{
			return new SNetSocket_t(value);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0000F3AE File Offset: 0x0000D5AE
		public static explicit operator uint(SNetSocket_t that)
		{
			return that.m_SNetSocket;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x0000F3B6 File Offset: 0x0000D5B6
		public bool Equals(SNetSocket_t other)
		{
			return this.m_SNetSocket == other.m_SNetSocket;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0000F3C6 File Offset: 0x0000D5C6
		public int CompareTo(SNetSocket_t other)
		{
			return this.m_SNetSocket.CompareTo(other.m_SNetSocket);
		}

		// Token: 0x04000A77 RID: 2679
		public uint m_SNetSocket;
	}
}
