using System;

namespace Steamworks
{
	// Token: 0x02000199 RID: 409
	[Serializable]
	public struct InputDigitalActionHandle_t : IEquatable<InputDigitalActionHandle_t>, IComparable<InputDigitalActionHandle_t>
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x0000EE09 File Offset: 0x0000D009
		public InputDigitalActionHandle_t(ulong value)
		{
			this.m_InputDigitalActionHandle = value;
		}

		// Token: 0x060009CC RID: 2508 RVA: 0x0000EE12 File Offset: 0x0000D012
		public override string ToString()
		{
			return this.m_InputDigitalActionHandle.ToString();
		}

		// Token: 0x060009CD RID: 2509 RVA: 0x0000EE1F File Offset: 0x0000D01F
		public override bool Equals(object other)
		{
			return other is InputDigitalActionHandle_t && this == (InputDigitalActionHandle_t)other;
		}

		// Token: 0x060009CE RID: 2510 RVA: 0x0000EE3C File Offset: 0x0000D03C
		public override int GetHashCode()
		{
			return this.m_InputDigitalActionHandle.GetHashCode();
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x0000EE49 File Offset: 0x0000D049
		public static bool operator ==(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return x.m_InputDigitalActionHandle == y.m_InputDigitalActionHandle;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0000EE59 File Offset: 0x0000D059
		public static bool operator !=(InputDigitalActionHandle_t x, InputDigitalActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x0000EE65 File Offset: 0x0000D065
		public static explicit operator InputDigitalActionHandle_t(ulong value)
		{
			return new InputDigitalActionHandle_t(value);
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x0000EE6D File Offset: 0x0000D06D
		public static explicit operator ulong(InputDigitalActionHandle_t that)
		{
			return that.m_InputDigitalActionHandle;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x0000EE75 File Offset: 0x0000D075
		public bool Equals(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle == other.m_InputDigitalActionHandle;
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x0000EE85 File Offset: 0x0000D085
		public int CompareTo(InputDigitalActionHandle_t other)
		{
			return this.m_InputDigitalActionHandle.CompareTo(other.m_InputDigitalActionHandle);
		}

		// Token: 0x04000A66 RID: 2662
		public ulong m_InputDigitalActionHandle;
	}
}
