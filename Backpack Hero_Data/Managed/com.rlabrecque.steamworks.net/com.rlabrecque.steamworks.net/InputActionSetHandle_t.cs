using System;

namespace Steamworks
{
	// Token: 0x02000197 RID: 407
	[Serializable]
	public struct InputActionSetHandle_t : IEquatable<InputActionSetHandle_t>, IComparable<InputActionSetHandle_t>
	{
		// Token: 0x060009B7 RID: 2487 RVA: 0x0000ECEB File Offset: 0x0000CEEB
		public InputActionSetHandle_t(ulong value)
		{
			this.m_InputActionSetHandle = value;
		}

		// Token: 0x060009B8 RID: 2488 RVA: 0x0000ECF4 File Offset: 0x0000CEF4
		public override string ToString()
		{
			return this.m_InputActionSetHandle.ToString();
		}

		// Token: 0x060009B9 RID: 2489 RVA: 0x0000ED01 File Offset: 0x0000CF01
		public override bool Equals(object other)
		{
			return other is InputActionSetHandle_t && this == (InputActionSetHandle_t)other;
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x0000ED1E File Offset: 0x0000CF1E
		public override int GetHashCode()
		{
			return this.m_InputActionSetHandle.GetHashCode();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0000ED2B File Offset: 0x0000CF2B
		public static bool operator ==(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return x.m_InputActionSetHandle == y.m_InputActionSetHandle;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0000ED3B File Offset: 0x0000CF3B
		public static bool operator !=(InputActionSetHandle_t x, InputActionSetHandle_t y)
		{
			return !(x == y);
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0000ED47 File Offset: 0x0000CF47
		public static explicit operator InputActionSetHandle_t(ulong value)
		{
			return new InputActionSetHandle_t(value);
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0000ED4F File Offset: 0x0000CF4F
		public static explicit operator ulong(InputActionSetHandle_t that)
		{
			return that.m_InputActionSetHandle;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0000ED57 File Offset: 0x0000CF57
		public bool Equals(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle == other.m_InputActionSetHandle;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0000ED67 File Offset: 0x0000CF67
		public int CompareTo(InputActionSetHandle_t other)
		{
			return this.m_InputActionSetHandle.CompareTo(other.m_InputActionSetHandle);
		}

		// Token: 0x04000A64 RID: 2660
		public ulong m_InputActionSetHandle;
	}
}
