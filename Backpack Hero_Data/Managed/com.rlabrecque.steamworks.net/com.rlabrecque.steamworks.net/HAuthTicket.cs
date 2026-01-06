using System;

namespace Steamworks
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public struct HAuthTicket : IEquatable<HAuthTicket>, IComparable<HAuthTicket>
	{
		// Token: 0x0600097E RID: 2430 RVA: 0x0000E9C4 File Offset: 0x0000CBC4
		public HAuthTicket(uint value)
		{
			this.m_HAuthTicket = value;
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x0000E9CD File Offset: 0x0000CBCD
		public override string ToString()
		{
			return this.m_HAuthTicket.ToString();
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0000E9DA File Offset: 0x0000CBDA
		public override bool Equals(object other)
		{
			return other is HAuthTicket && this == (HAuthTicket)other;
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0000E9F7 File Offset: 0x0000CBF7
		public override int GetHashCode()
		{
			return this.m_HAuthTicket.GetHashCode();
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0000EA04 File Offset: 0x0000CC04
		public static bool operator ==(HAuthTicket x, HAuthTicket y)
		{
			return x.m_HAuthTicket == y.m_HAuthTicket;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0000EA14 File Offset: 0x0000CC14
		public static bool operator !=(HAuthTicket x, HAuthTicket y)
		{
			return !(x == y);
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public static explicit operator HAuthTicket(uint value)
		{
			return new HAuthTicket(value);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0000EA28 File Offset: 0x0000CC28
		public static explicit operator uint(HAuthTicket that)
		{
			return that.m_HAuthTicket;
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0000EA30 File Offset: 0x0000CC30
		public bool Equals(HAuthTicket other)
		{
			return this.m_HAuthTicket == other.m_HAuthTicket;
		}

		// Token: 0x06000987 RID: 2439 RVA: 0x0000EA40 File Offset: 0x0000CC40
		public int CompareTo(HAuthTicket other)
		{
			return this.m_HAuthTicket.CompareTo(other.m_HAuthTicket);
		}

		// Token: 0x04000A4E RID: 2638
		public static readonly HAuthTicket Invalid = new HAuthTicket(0U);

		// Token: 0x04000A4F RID: 2639
		public uint m_HAuthTicket;
	}
}
