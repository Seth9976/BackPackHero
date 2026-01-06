using System;

namespace Steamworks
{
	// Token: 0x020001B8 RID: 440
	[Serializable]
	public struct AccountID_t : IEquatable<AccountID_t>, IComparable<AccountID_t>
	{
		// Token: 0x06000ADA RID: 2778 RVA: 0x0000FCE9 File Offset: 0x0000DEE9
		public AccountID_t(uint value)
		{
			this.m_AccountID = value;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x0000FCF2 File Offset: 0x0000DEF2
		public override string ToString()
		{
			return this.m_AccountID.ToString();
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x0000FCFF File Offset: 0x0000DEFF
		public override bool Equals(object other)
		{
			return other is AccountID_t && this == (AccountID_t)other;
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0000FD1C File Offset: 0x0000DF1C
		public override int GetHashCode()
		{
			return this.m_AccountID.GetHashCode();
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x0000FD29 File Offset: 0x0000DF29
		public static bool operator ==(AccountID_t x, AccountID_t y)
		{
			return x.m_AccountID == y.m_AccountID;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0000FD39 File Offset: 0x0000DF39
		public static bool operator !=(AccountID_t x, AccountID_t y)
		{
			return !(x == y);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0000FD45 File Offset: 0x0000DF45
		public static explicit operator AccountID_t(uint value)
		{
			return new AccountID_t(value);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x0000FD4D File Offset: 0x0000DF4D
		public static explicit operator uint(AccountID_t that)
		{
			return that.m_AccountID;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x0000FD55 File Offset: 0x0000DF55
		public bool Equals(AccountID_t other)
		{
			return this.m_AccountID == other.m_AccountID;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x0000FD65 File Offset: 0x0000DF65
		public int CompareTo(AccountID_t other)
		{
			return this.m_AccountID.CompareTo(other.m_AccountID);
		}

		// Token: 0x04000AA6 RID: 2726
		public uint m_AccountID;
	}
}
