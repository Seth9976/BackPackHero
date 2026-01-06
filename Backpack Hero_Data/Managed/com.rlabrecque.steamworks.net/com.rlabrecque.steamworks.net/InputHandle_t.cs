using System;

namespace Steamworks
{
	// Token: 0x0200019A RID: 410
	[Serializable]
	public struct InputHandle_t : IEquatable<InputHandle_t>, IComparable<InputHandle_t>
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x0000EE98 File Offset: 0x0000D098
		public InputHandle_t(ulong value)
		{
			this.m_InputHandle = value;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x0000EEA1 File Offset: 0x0000D0A1
		public override string ToString()
		{
			return this.m_InputHandle.ToString();
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x0000EEAE File Offset: 0x0000D0AE
		public override bool Equals(object other)
		{
			return other is InputHandle_t && this == (InputHandle_t)other;
		}

		// Token: 0x060009D8 RID: 2520 RVA: 0x0000EECB File Offset: 0x0000D0CB
		public override int GetHashCode()
		{
			return this.m_InputHandle.GetHashCode();
		}

		// Token: 0x060009D9 RID: 2521 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public static bool operator ==(InputHandle_t x, InputHandle_t y)
		{
			return x.m_InputHandle == y.m_InputHandle;
		}

		// Token: 0x060009DA RID: 2522 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public static bool operator !=(InputHandle_t x, InputHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public static explicit operator InputHandle_t(ulong value)
		{
			return new InputHandle_t(value);
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x0000EEFC File Offset: 0x0000D0FC
		public static explicit operator ulong(InputHandle_t that)
		{
			return that.m_InputHandle;
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0000EF04 File Offset: 0x0000D104
		public bool Equals(InputHandle_t other)
		{
			return this.m_InputHandle == other.m_InputHandle;
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0000EF14 File Offset: 0x0000D114
		public int CompareTo(InputHandle_t other)
		{
			return this.m_InputHandle.CompareTo(other.m_InputHandle);
		}

		// Token: 0x04000A67 RID: 2663
		public ulong m_InputHandle;
	}
}
