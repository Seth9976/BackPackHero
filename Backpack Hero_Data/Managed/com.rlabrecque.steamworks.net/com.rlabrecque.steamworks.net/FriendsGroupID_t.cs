using System;

namespace Steamworks
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	public struct FriendsGroupID_t : IEquatable<FriendsGroupID_t>, IComparable<FriendsGroupID_t>
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x0000EA7B File Offset: 0x0000CC7B
		public FriendsGroupID_t(short value)
		{
			this.m_FriendsGroupID = value;
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public override string ToString()
		{
			return this.m_FriendsGroupID.ToString();
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x0000EA91 File Offset: 0x0000CC91
		public override bool Equals(object other)
		{
			return other is FriendsGroupID_t && this == (FriendsGroupID_t)other;
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x0000EAAE File Offset: 0x0000CCAE
		public override int GetHashCode()
		{
			return this.m_FriendsGroupID.GetHashCode();
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x0000EABB File Offset: 0x0000CCBB
		public static bool operator ==(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return x.m_FriendsGroupID == y.m_FriendsGroupID;
		}

		// Token: 0x06000990 RID: 2448 RVA: 0x0000EACB File Offset: 0x0000CCCB
		public static bool operator !=(FriendsGroupID_t x, FriendsGroupID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000991 RID: 2449 RVA: 0x0000EAD7 File Offset: 0x0000CCD7
		public static explicit operator FriendsGroupID_t(short value)
		{
			return new FriendsGroupID_t(value);
		}

		// Token: 0x06000992 RID: 2450 RVA: 0x0000EADF File Offset: 0x0000CCDF
		public static explicit operator short(FriendsGroupID_t that)
		{
			return that.m_FriendsGroupID;
		}

		// Token: 0x06000993 RID: 2451 RVA: 0x0000EAE7 File Offset: 0x0000CCE7
		public bool Equals(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID == other.m_FriendsGroupID;
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0000EAF7 File Offset: 0x0000CCF7
		public int CompareTo(FriendsGroupID_t other)
		{
			return this.m_FriendsGroupID.CompareTo(other.m_FriendsGroupID);
		}

		// Token: 0x04000A5C RID: 2652
		public static readonly FriendsGroupID_t Invalid = new FriendsGroupID_t(-1);

		// Token: 0x04000A5D RID: 2653
		public short m_FriendsGroupID;
	}
}
