using System;

namespace Steamworks
{
	// Token: 0x020001C3 RID: 451
	[Serializable]
	public struct HSteamPipe : IEquatable<HSteamPipe>, IComparable<HSteamPipe>
	{
		// Token: 0x06000B49 RID: 2889 RVA: 0x000104B2 File Offset: 0x0000E6B2
		public HSteamPipe(int value)
		{
			this.m_HSteamPipe = value;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000104BB File Offset: 0x0000E6BB
		public override string ToString()
		{
			return this.m_HSteamPipe.ToString();
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000104C8 File Offset: 0x0000E6C8
		public override bool Equals(object other)
		{
			return other is HSteamPipe && this == (HSteamPipe)other;
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000104E5 File Offset: 0x0000E6E5
		public override int GetHashCode()
		{
			return this.m_HSteamPipe.GetHashCode();
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000104F2 File Offset: 0x0000E6F2
		public static bool operator ==(HSteamPipe x, HSteamPipe y)
		{
			return x.m_HSteamPipe == y.m_HSteamPipe;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00010502 File Offset: 0x0000E702
		public static bool operator !=(HSteamPipe x, HSteamPipe y)
		{
			return !(x == y);
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001050E File Offset: 0x0000E70E
		public static explicit operator HSteamPipe(int value)
		{
			return new HSteamPipe(value);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00010516 File Offset: 0x0000E716
		public static explicit operator int(HSteamPipe that)
		{
			return that.m_HSteamPipe;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001051E File Offset: 0x0000E71E
		public bool Equals(HSteamPipe other)
		{
			return this.m_HSteamPipe == other.m_HSteamPipe;
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x0001052E File Offset: 0x0000E72E
		public int CompareTo(HSteamPipe other)
		{
			return this.m_HSteamPipe.CompareTo(other.m_HSteamPipe);
		}

		// Token: 0x04000AB9 RID: 2745
		public int m_HSteamPipe;
	}
}
