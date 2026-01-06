using System;

namespace Steamworks
{
	// Token: 0x020001B2 RID: 434
	[Serializable]
	public struct RemotePlaySessionID_t : IEquatable<RemotePlaySessionID_t>, IComparable<RemotePlaySessionID_t>
	{
		// Token: 0x06000A99 RID: 2713 RVA: 0x0000F94A File Offset: 0x0000DB4A
		public RemotePlaySessionID_t(uint value)
		{
			this.m_RemotePlaySessionID = value;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0000F953 File Offset: 0x0000DB53
		public override string ToString()
		{
			return this.m_RemotePlaySessionID.ToString();
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0000F960 File Offset: 0x0000DB60
		public override bool Equals(object other)
		{
			return other is RemotePlaySessionID_t && this == (RemotePlaySessionID_t)other;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0000F97D File Offset: 0x0000DB7D
		public override int GetHashCode()
		{
			return this.m_RemotePlaySessionID.GetHashCode();
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0000F98A File Offset: 0x0000DB8A
		public static bool operator ==(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return x.m_RemotePlaySessionID == y.m_RemotePlaySessionID;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0000F99A File Offset: 0x0000DB9A
		public static bool operator !=(RemotePlaySessionID_t x, RemotePlaySessionID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0000F9A6 File Offset: 0x0000DBA6
		public static explicit operator RemotePlaySessionID_t(uint value)
		{
			return new RemotePlaySessionID_t(value);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0000F9AE File Offset: 0x0000DBAE
		public static explicit operator uint(RemotePlaySessionID_t that)
		{
			return that.m_RemotePlaySessionID;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0000F9B6 File Offset: 0x0000DBB6
		public bool Equals(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID == other.m_RemotePlaySessionID;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
		public int CompareTo(RemotePlaySessionID_t other)
		{
			return this.m_RemotePlaySessionID.CompareTo(other.m_RemotePlaySessionID);
		}

		// Token: 0x04000A9B RID: 2715
		public uint m_RemotePlaySessionID;
	}
}
