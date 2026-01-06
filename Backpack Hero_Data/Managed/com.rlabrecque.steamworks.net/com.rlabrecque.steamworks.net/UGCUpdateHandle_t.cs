using System;

namespace Steamworks
{
	// Token: 0x020001C0 RID: 448
	[Serializable]
	public struct UGCUpdateHandle_t : IEquatable<UGCUpdateHandle_t>, IComparable<UGCUpdateHandle_t>
	{
		// Token: 0x06000B2A RID: 2858 RVA: 0x000102F7 File Offset: 0x0000E4F7
		public UGCUpdateHandle_t(ulong value)
		{
			this.m_UGCUpdateHandle = value;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00010300 File Offset: 0x0000E500
		public override string ToString()
		{
			return this.m_UGCUpdateHandle.ToString();
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x0001030D File Offset: 0x0000E50D
		public override bool Equals(object other)
		{
			return other is UGCUpdateHandle_t && this == (UGCUpdateHandle_t)other;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001032A File Offset: 0x0000E52A
		public override int GetHashCode()
		{
			return this.m_UGCUpdateHandle.GetHashCode();
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x00010337 File Offset: 0x0000E537
		public static bool operator ==(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return x.m_UGCUpdateHandle == y.m_UGCUpdateHandle;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x00010347 File Offset: 0x0000E547
		public static bool operator !=(UGCUpdateHandle_t x, UGCUpdateHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x00010353 File Offset: 0x0000E553
		public static explicit operator UGCUpdateHandle_t(ulong value)
		{
			return new UGCUpdateHandle_t(value);
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x0001035B File Offset: 0x0000E55B
		public static explicit operator ulong(UGCUpdateHandle_t that)
		{
			return that.m_UGCUpdateHandle;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00010363 File Offset: 0x0000E563
		public bool Equals(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle == other.m_UGCUpdateHandle;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00010373 File Offset: 0x0000E573
		public int CompareTo(UGCUpdateHandle_t other)
		{
			return this.m_UGCUpdateHandle.CompareTo(other.m_UGCUpdateHandle);
		}

		// Token: 0x04000AB5 RID: 2741
		public static readonly UGCUpdateHandle_t Invalid = new UGCUpdateHandle_t(ulong.MaxValue);

		// Token: 0x04000AB6 RID: 2742
		public ulong m_UGCUpdateHandle;
	}
}
