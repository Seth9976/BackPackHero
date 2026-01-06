using System;

namespace Steamworks
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public struct RTime32 : IEquatable<RTime32>, IComparable<RTime32>
	{
		// Token: 0x06000B05 RID: 2821 RVA: 0x0000FF4D File Offset: 0x0000E14D
		public RTime32(uint value)
		{
			this.m_RTime32 = value;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x0000FF56 File Offset: 0x0000E156
		public override string ToString()
		{
			return this.m_RTime32.ToString();
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x0000FF63 File Offset: 0x0000E163
		public override bool Equals(object other)
		{
			return other is RTime32 && this == (RTime32)other;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0000FF80 File Offset: 0x0000E180
		public override int GetHashCode()
		{
			return this.m_RTime32.GetHashCode();
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x0000FF8D File Offset: 0x0000E18D
		public static bool operator ==(RTime32 x, RTime32 y)
		{
			return x.m_RTime32 == y.m_RTime32;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x0000FF9D File Offset: 0x0000E19D
		public static bool operator !=(RTime32 x, RTime32 y)
		{
			return !(x == y);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0000FFA9 File Offset: 0x0000E1A9
		public static explicit operator RTime32(uint value)
		{
			return new RTime32(value);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x0000FFB1 File Offset: 0x0000E1B1
		public static explicit operator uint(RTime32 that)
		{
			return that.m_RTime32;
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0000FFB9 File Offset: 0x0000E1B9
		public bool Equals(RTime32 other)
		{
			return this.m_RTime32 == other.m_RTime32;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0000FFC9 File Offset: 0x0000E1C9
		public int CompareTo(RTime32 other)
		{
			return this.m_RTime32.CompareTo(other.m_RTime32);
		}

		// Token: 0x04000AAD RID: 2733
		public uint m_RTime32;
	}
}
