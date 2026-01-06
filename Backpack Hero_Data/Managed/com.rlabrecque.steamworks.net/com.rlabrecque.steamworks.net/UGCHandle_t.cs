using System;

namespace Steamworks
{
	// Token: 0x020001B6 RID: 438
	[Serializable]
	public struct UGCHandle_t : IEquatable<UGCHandle_t>, IComparable<UGCHandle_t>
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x0000FBB0 File Offset: 0x0000DDB0
		public UGCHandle_t(ulong value)
		{
			this.m_UGCHandle = value;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0000FBB9 File Offset: 0x0000DDB9
		public override string ToString()
		{
			return this.m_UGCHandle.ToString();
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0000FBC6 File Offset: 0x0000DDC6
		public override bool Equals(object other)
		{
			return other is UGCHandle_t && this == (UGCHandle_t)other;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0000FBE3 File Offset: 0x0000DDE3
		public override int GetHashCode()
		{
			return this.m_UGCHandle.GetHashCode();
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x0000FBF0 File Offset: 0x0000DDF0
		public static bool operator ==(UGCHandle_t x, UGCHandle_t y)
		{
			return x.m_UGCHandle == y.m_UGCHandle;
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0000FC00 File Offset: 0x0000DE00
		public static bool operator !=(UGCHandle_t x, UGCHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0000FC0C File Offset: 0x0000DE0C
		public static explicit operator UGCHandle_t(ulong value)
		{
			return new UGCHandle_t(value);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0000FC14 File Offset: 0x0000DE14
		public static explicit operator ulong(UGCHandle_t that)
		{
			return that.m_UGCHandle;
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x0000FC1C File Offset: 0x0000DE1C
		public bool Equals(UGCHandle_t other)
		{
			return this.m_UGCHandle == other.m_UGCHandle;
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		public int CompareTo(UGCHandle_t other)
		{
			return this.m_UGCHandle.CompareTo(other.m_UGCHandle);
		}

		// Token: 0x04000AA2 RID: 2722
		public static readonly UGCHandle_t Invalid = new UGCHandle_t(ulong.MaxValue);

		// Token: 0x04000AA3 RID: 2723
		public ulong m_UGCHandle;
	}
}
