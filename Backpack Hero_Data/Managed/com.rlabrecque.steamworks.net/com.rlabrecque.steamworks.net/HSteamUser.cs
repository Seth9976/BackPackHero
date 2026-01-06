using System;

namespace Steamworks
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	public struct HSteamUser : IEquatable<HSteamUser>, IComparable<HSteamUser>
	{
		// Token: 0x06000B53 RID: 2899 RVA: 0x00010541 File Offset: 0x0000E741
		public HSteamUser(int value)
		{
			this.m_HSteamUser = value;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001054A File Offset: 0x0000E74A
		public override string ToString()
		{
			return this.m_HSteamUser.ToString();
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00010557 File Offset: 0x0000E757
		public override bool Equals(object other)
		{
			return other is HSteamUser && this == (HSteamUser)other;
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00010574 File Offset: 0x0000E774
		public override int GetHashCode()
		{
			return this.m_HSteamUser.GetHashCode();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00010581 File Offset: 0x0000E781
		public static bool operator ==(HSteamUser x, HSteamUser y)
		{
			return x.m_HSteamUser == y.m_HSteamUser;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00010591 File Offset: 0x0000E791
		public static bool operator !=(HSteamUser x, HSteamUser y)
		{
			return !(x == y);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001059D File Offset: 0x0000E79D
		public static explicit operator HSteamUser(int value)
		{
			return new HSteamUser(value);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000105A5 File Offset: 0x0000E7A5
		public static explicit operator int(HSteamUser that)
		{
			return that.m_HSteamUser;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000105AD File Offset: 0x0000E7AD
		public bool Equals(HSteamUser other)
		{
			return this.m_HSteamUser == other.m_HSteamUser;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000105BD File Offset: 0x0000E7BD
		public int CompareTo(HSteamUser other)
		{
			return this.m_HSteamUser.CompareTo(other.m_HSteamUser);
		}

		// Token: 0x04000ABA RID: 2746
		public int m_HSteamUser;
	}
}
