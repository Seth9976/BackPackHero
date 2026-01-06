using System;

namespace Steamworks
{
	// Token: 0x020001B7 RID: 439
	[Serializable]
	public struct ScreenshotHandle : IEquatable<ScreenshotHandle>, IComparable<ScreenshotHandle>
	{
		// Token: 0x06000ACF RID: 2767 RVA: 0x0000FC4D File Offset: 0x0000DE4D
		public ScreenshotHandle(uint value)
		{
			this.m_ScreenshotHandle = value;
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x0000FC56 File Offset: 0x0000DE56
		public override string ToString()
		{
			return this.m_ScreenshotHandle.ToString();
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0000FC63 File Offset: 0x0000DE63
		public override bool Equals(object other)
		{
			return other is ScreenshotHandle && this == (ScreenshotHandle)other;
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x0000FC80 File Offset: 0x0000DE80
		public override int GetHashCode()
		{
			return this.m_ScreenshotHandle.GetHashCode();
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0000FC8D File Offset: 0x0000DE8D
		public static bool operator ==(ScreenshotHandle x, ScreenshotHandle y)
		{
			return x.m_ScreenshotHandle == y.m_ScreenshotHandle;
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0000FC9D File Offset: 0x0000DE9D
		public static bool operator !=(ScreenshotHandle x, ScreenshotHandle y)
		{
			return !(x == y);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0000FCA9 File Offset: 0x0000DEA9
		public static explicit operator ScreenshotHandle(uint value)
		{
			return new ScreenshotHandle(value);
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0000FCB1 File Offset: 0x0000DEB1
		public static explicit operator uint(ScreenshotHandle that)
		{
			return that.m_ScreenshotHandle;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x0000FCB9 File Offset: 0x0000DEB9
		public bool Equals(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle == other.m_ScreenshotHandle;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0000FCC9 File Offset: 0x0000DEC9
		public int CompareTo(ScreenshotHandle other)
		{
			return this.m_ScreenshotHandle.CompareTo(other.m_ScreenshotHandle);
		}

		// Token: 0x04000AA4 RID: 2724
		public static readonly ScreenshotHandle Invalid = new ScreenshotHandle(0U);

		// Token: 0x04000AA5 RID: 2725
		public uint m_ScreenshotHandle;
	}
}
