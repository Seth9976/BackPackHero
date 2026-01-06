using System;

namespace Steamworks
{
	// Token: 0x020001B9 RID: 441
	[Serializable]
	public struct AppId_t : IEquatable<AppId_t>, IComparable<AppId_t>
	{
		// Token: 0x06000AE4 RID: 2788 RVA: 0x0000FD78 File Offset: 0x0000DF78
		public AppId_t(uint value)
		{
			this.m_AppId = value;
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x0000FD81 File Offset: 0x0000DF81
		public override string ToString()
		{
			return this.m_AppId.ToString();
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x0000FD8E File Offset: 0x0000DF8E
		public override bool Equals(object other)
		{
			return other is AppId_t && this == (AppId_t)other;
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x0000FDAB File Offset: 0x0000DFAB
		public override int GetHashCode()
		{
			return this.m_AppId.GetHashCode();
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0000FDB8 File Offset: 0x0000DFB8
		public static bool operator ==(AppId_t x, AppId_t y)
		{
			return x.m_AppId == y.m_AppId;
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x0000FDC8 File Offset: 0x0000DFC8
		public static bool operator !=(AppId_t x, AppId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0000FDD4 File Offset: 0x0000DFD4
		public static explicit operator AppId_t(uint value)
		{
			return new AppId_t(value);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x0000FDDC File Offset: 0x0000DFDC
		public static explicit operator uint(AppId_t that)
		{
			return that.m_AppId;
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x0000FDE4 File Offset: 0x0000DFE4
		public bool Equals(AppId_t other)
		{
			return this.m_AppId == other.m_AppId;
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x0000FDF4 File Offset: 0x0000DFF4
		public int CompareTo(AppId_t other)
		{
			return this.m_AppId.CompareTo(other.m_AppId);
		}

		// Token: 0x04000AA7 RID: 2727
		public static readonly AppId_t Invalid = new AppId_t(0U);

		// Token: 0x04000AA8 RID: 2728
		public uint m_AppId;
	}
}
