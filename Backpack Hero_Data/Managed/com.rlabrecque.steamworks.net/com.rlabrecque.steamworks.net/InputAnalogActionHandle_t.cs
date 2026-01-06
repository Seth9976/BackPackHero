using System;

namespace Steamworks
{
	// Token: 0x02000198 RID: 408
	[Serializable]
	public struct InputAnalogActionHandle_t : IEquatable<InputAnalogActionHandle_t>, IComparable<InputAnalogActionHandle_t>
	{
		// Token: 0x060009C1 RID: 2497 RVA: 0x0000ED7A File Offset: 0x0000CF7A
		public InputAnalogActionHandle_t(ulong value)
		{
			this.m_InputAnalogActionHandle = value;
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0000ED83 File Offset: 0x0000CF83
		public override string ToString()
		{
			return this.m_InputAnalogActionHandle.ToString();
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0000ED90 File Offset: 0x0000CF90
		public override bool Equals(object other)
		{
			return other is InputAnalogActionHandle_t && this == (InputAnalogActionHandle_t)other;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x0000EDAD File Offset: 0x0000CFAD
		public override int GetHashCode()
		{
			return this.m_InputAnalogActionHandle.GetHashCode();
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x0000EDBA File Offset: 0x0000CFBA
		public static bool operator ==(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return x.m_InputAnalogActionHandle == y.m_InputAnalogActionHandle;
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x0000EDCA File Offset: 0x0000CFCA
		public static bool operator !=(InputAnalogActionHandle_t x, InputAnalogActionHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x0000EDD6 File Offset: 0x0000CFD6
		public static explicit operator InputAnalogActionHandle_t(ulong value)
		{
			return new InputAnalogActionHandle_t(value);
		}

		// Token: 0x060009C8 RID: 2504 RVA: 0x0000EDDE File Offset: 0x0000CFDE
		public static explicit operator ulong(InputAnalogActionHandle_t that)
		{
			return that.m_InputAnalogActionHandle;
		}

		// Token: 0x060009C9 RID: 2505 RVA: 0x0000EDE6 File Offset: 0x0000CFE6
		public bool Equals(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle == other.m_InputAnalogActionHandle;
		}

		// Token: 0x060009CA RID: 2506 RVA: 0x0000EDF6 File Offset: 0x0000CFF6
		public int CompareTo(InputAnalogActionHandle_t other)
		{
			return this.m_InputAnalogActionHandle.CompareTo(other.m_InputAnalogActionHandle);
		}

		// Token: 0x04000A65 RID: 2661
		public ulong m_InputAnalogActionHandle;
	}
}
