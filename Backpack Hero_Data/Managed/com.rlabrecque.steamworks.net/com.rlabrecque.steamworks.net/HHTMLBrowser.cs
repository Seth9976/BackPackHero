using System;

namespace Steamworks
{
	// Token: 0x02000194 RID: 404
	[Serializable]
	public struct HHTMLBrowser : IEquatable<HHTMLBrowser>, IComparable<HHTMLBrowser>
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x0000EB17 File Offset: 0x0000CD17
		public HHTMLBrowser(uint value)
		{
			this.m_HHTMLBrowser = value;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0000EB20 File Offset: 0x0000CD20
		public override string ToString()
		{
			return this.m_HHTMLBrowser.ToString();
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0000EB2D File Offset: 0x0000CD2D
		public override bool Equals(object other)
		{
			return other is HHTMLBrowser && this == (HHTMLBrowser)other;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0000EB4A File Offset: 0x0000CD4A
		public override int GetHashCode()
		{
			return this.m_HHTMLBrowser.GetHashCode();
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0000EB57 File Offset: 0x0000CD57
		public static bool operator ==(HHTMLBrowser x, HHTMLBrowser y)
		{
			return x.m_HHTMLBrowser == y.m_HHTMLBrowser;
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0000EB67 File Offset: 0x0000CD67
		public static bool operator !=(HHTMLBrowser x, HHTMLBrowser y)
		{
			return !(x == y);
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0000EB73 File Offset: 0x0000CD73
		public static explicit operator HHTMLBrowser(uint value)
		{
			return new HHTMLBrowser(value);
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0000EB7B File Offset: 0x0000CD7B
		public static explicit operator uint(HHTMLBrowser that)
		{
			return that.m_HHTMLBrowser;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0000EB83 File Offset: 0x0000CD83
		public bool Equals(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser == other.m_HHTMLBrowser;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0000EB93 File Offset: 0x0000CD93
		public int CompareTo(HHTMLBrowser other)
		{
			return this.m_HHTMLBrowser.CompareTo(other.m_HHTMLBrowser);
		}

		// Token: 0x04000A5E RID: 2654
		public static readonly HHTMLBrowser Invalid = new HHTMLBrowser(0U);

		// Token: 0x04000A5F RID: 2655
		public uint m_HHTMLBrowser;
	}
}
