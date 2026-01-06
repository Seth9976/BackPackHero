using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020003F8 RID: 1016
	public struct DepthState : IEquatable<DepthState>
	{
		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x00039F28 File Offset: 0x00038128
		public static DepthState defaultValue
		{
			get
			{
				return new DepthState(true, CompareFunction.Less);
			}
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00039F41 File Offset: 0x00038141
		public DepthState(bool writeEnabled = true, CompareFunction compareFunction = CompareFunction.Less)
		{
			this.m_WriteEnabled = Convert.ToByte(writeEnabled);
			this.m_CompareFunction = (sbyte)compareFunction;
		}

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06002294 RID: 8852 RVA: 0x00039F58 File Offset: 0x00038158
		// (set) Token: 0x06002295 RID: 8853 RVA: 0x00039F75 File Offset: 0x00038175
		public bool writeEnabled
		{
			get
			{
				return Convert.ToBoolean(this.m_WriteEnabled);
			}
			set
			{
				this.m_WriteEnabled = Convert.ToByte(value);
			}
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06002296 RID: 8854 RVA: 0x00039F84 File Offset: 0x00038184
		// (set) Token: 0x06002297 RID: 8855 RVA: 0x00039F9C File Offset: 0x0003819C
		public CompareFunction compareFunction
		{
			get
			{
				return (CompareFunction)this.m_CompareFunction;
			}
			set
			{
				this.m_CompareFunction = (sbyte)value;
			}
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00039FA8 File Offset: 0x000381A8
		public bool Equals(DepthState other)
		{
			return this.m_WriteEnabled == other.m_WriteEnabled && this.m_CompareFunction == other.m_CompareFunction;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x00039FDC File Offset: 0x000381DC
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is DepthState && this.Equals((DepthState)obj);
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x0003A014 File Offset: 0x00038214
		public override int GetHashCode()
		{
			return (this.m_WriteEnabled.GetHashCode() * 397) ^ this.m_CompareFunction.GetHashCode();
		}

		// Token: 0x0600229B RID: 8859 RVA: 0x0003A044 File Offset: 0x00038244
		public static bool operator ==(DepthState left, DepthState right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x0003A060 File Offset: 0x00038260
		public static bool operator !=(DepthState left, DepthState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CCB RID: 3275
		private byte m_WriteEnabled;

		// Token: 0x04000CCC RID: 3276
		private sbyte m_CompareFunction;
	}
}
