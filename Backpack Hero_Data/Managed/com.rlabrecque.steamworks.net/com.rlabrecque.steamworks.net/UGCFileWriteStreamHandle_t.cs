using System;

namespace Steamworks
{
	// Token: 0x020001B5 RID: 437
	[Serializable]
	public struct UGCFileWriteStreamHandle_t : IEquatable<UGCFileWriteStreamHandle_t>, IComparable<UGCFileWriteStreamHandle_t>
	{
		// Token: 0x06000AB9 RID: 2745 RVA: 0x0000FB13 File Offset: 0x0000DD13
		public UGCFileWriteStreamHandle_t(ulong value)
		{
			this.m_UGCFileWriteStreamHandle = value;
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x0000FB1C File Offset: 0x0000DD1C
		public override string ToString()
		{
			return this.m_UGCFileWriteStreamHandle.ToString();
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0000FB29 File Offset: 0x0000DD29
		public override bool Equals(object other)
		{
			return other is UGCFileWriteStreamHandle_t && this == (UGCFileWriteStreamHandle_t)other;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x0000FB46 File Offset: 0x0000DD46
		public override int GetHashCode()
		{
			return this.m_UGCFileWriteStreamHandle.GetHashCode();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0000FB53 File Offset: 0x0000DD53
		public static bool operator ==(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return x.m_UGCFileWriteStreamHandle == y.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0000FB63 File Offset: 0x0000DD63
		public static bool operator !=(UGCFileWriteStreamHandle_t x, UGCFileWriteStreamHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0000FB6F File Offset: 0x0000DD6F
		public static explicit operator UGCFileWriteStreamHandle_t(ulong value)
		{
			return new UGCFileWriteStreamHandle_t(value);
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0000FB77 File Offset: 0x0000DD77
		public static explicit operator ulong(UGCFileWriteStreamHandle_t that)
		{
			return that.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0000FB7F File Offset: 0x0000DD7F
		public bool Equals(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle == other.m_UGCFileWriteStreamHandle;
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x0000FB8F File Offset: 0x0000DD8F
		public int CompareTo(UGCFileWriteStreamHandle_t other)
		{
			return this.m_UGCFileWriteStreamHandle.CompareTo(other.m_UGCFileWriteStreamHandle);
		}

		// Token: 0x04000AA0 RID: 2720
		public static readonly UGCFileWriteStreamHandle_t Invalid = new UGCFileWriteStreamHandle_t(ulong.MaxValue);

		// Token: 0x04000AA1 RID: 2721
		public ulong m_UGCFileWriteStreamHandle;
	}
}
