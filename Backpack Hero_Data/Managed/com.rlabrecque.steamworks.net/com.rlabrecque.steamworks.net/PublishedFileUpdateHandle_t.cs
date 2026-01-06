using System;

namespace Steamworks
{
	// Token: 0x020001B4 RID: 436
	[Serializable]
	public struct PublishedFileUpdateHandle_t : IEquatable<PublishedFileUpdateHandle_t>, IComparable<PublishedFileUpdateHandle_t>
	{
		// Token: 0x06000AAE RID: 2734 RVA: 0x0000FA76 File Offset: 0x0000DC76
		public PublishedFileUpdateHandle_t(ulong value)
		{
			this.m_PublishedFileUpdateHandle = value;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x0000FA7F File Offset: 0x0000DC7F
		public override string ToString()
		{
			return this.m_PublishedFileUpdateHandle.ToString();
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0000FA8C File Offset: 0x0000DC8C
		public override bool Equals(object other)
		{
			return other is PublishedFileUpdateHandle_t && this == (PublishedFileUpdateHandle_t)other;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x0000FAA9 File Offset: 0x0000DCA9
		public override int GetHashCode()
		{
			return this.m_PublishedFileUpdateHandle.GetHashCode();
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000FAB6 File Offset: 0x0000DCB6
		public static bool operator ==(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return x.m_PublishedFileUpdateHandle == y.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000FAC6 File Offset: 0x0000DCC6
		public static bool operator !=(PublishedFileUpdateHandle_t x, PublishedFileUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x0000FAD2 File Offset: 0x0000DCD2
		public static explicit operator PublishedFileUpdateHandle_t(ulong value)
		{
			return new PublishedFileUpdateHandle_t(value);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x0000FADA File Offset: 0x0000DCDA
		public static explicit operator ulong(PublishedFileUpdateHandle_t that)
		{
			return that.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x0000FAE2 File Offset: 0x0000DCE2
		public bool Equals(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle == other.m_PublishedFileUpdateHandle;
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x0000FAF2 File Offset: 0x0000DCF2
		public int CompareTo(PublishedFileUpdateHandle_t other)
		{
			return this.m_PublishedFileUpdateHandle.CompareTo(other.m_PublishedFileUpdateHandle);
		}

		// Token: 0x04000A9E RID: 2718
		public static readonly PublishedFileUpdateHandle_t Invalid = new PublishedFileUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000A9F RID: 2719
		public ulong m_PublishedFileUpdateHandle;
	}
}
