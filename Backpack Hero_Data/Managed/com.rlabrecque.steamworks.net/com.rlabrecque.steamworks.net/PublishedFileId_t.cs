using System;

namespace Steamworks
{
	// Token: 0x020001B3 RID: 435
	[Serializable]
	public struct PublishedFileId_t : IEquatable<PublishedFileId_t>, IComparable<PublishedFileId_t>
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x0000F9D9 File Offset: 0x0000DBD9
		public PublishedFileId_t(ulong value)
		{
			this.m_PublishedFileId = value;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0000F9E2 File Offset: 0x0000DBE2
		public override string ToString()
		{
			return this.m_PublishedFileId.ToString();
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0000F9EF File Offset: 0x0000DBEF
		public override bool Equals(object other)
		{
			return other is PublishedFileId_t && this == (PublishedFileId_t)other;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0000FA0C File Offset: 0x0000DC0C
		public override int GetHashCode()
		{
			return this.m_PublishedFileId.GetHashCode();
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x0000FA19 File Offset: 0x0000DC19
		public static bool operator ==(PublishedFileId_t x, PublishedFileId_t y)
		{
			return x.m_PublishedFileId == y.m_PublishedFileId;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x0000FA29 File Offset: 0x0000DC29
		public static bool operator !=(PublishedFileId_t x, PublishedFileId_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x0000FA35 File Offset: 0x0000DC35
		public static explicit operator PublishedFileId_t(ulong value)
		{
			return new PublishedFileId_t(value);
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x0000FA3D File Offset: 0x0000DC3D
		public static explicit operator ulong(PublishedFileId_t that)
		{
			return that.m_PublishedFileId;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0000FA45 File Offset: 0x0000DC45
		public bool Equals(PublishedFileId_t other)
		{
			return this.m_PublishedFileId == other.m_PublishedFileId;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0000FA55 File Offset: 0x0000DC55
		public int CompareTo(PublishedFileId_t other)
		{
			return this.m_PublishedFileId.CompareTo(other.m_PublishedFileId);
		}

		// Token: 0x04000A9C RID: 2716
		public static readonly PublishedFileId_t Invalid = new PublishedFileId_t(0UL);

		// Token: 0x04000A9D RID: 2717
		public ulong m_PublishedFileId;
	}
}
