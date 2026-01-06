using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000400 RID: 1024
	public struct RasterState : IEquatable<RasterState>
	{
		// Token: 0x060022D6 RID: 8918 RVA: 0x0003A9AD File Offset: 0x00038BAD
		public RasterState(CullMode cullingMode = CullMode.Back, int offsetUnits = 0, float offsetFactor = 0f, bool depthClip = true)
		{
			this.m_CullingMode = cullingMode;
			this.m_OffsetUnits = offsetUnits;
			this.m_OffsetFactor = offsetFactor;
			this.m_DepthClip = Convert.ToByte(depthClip);
			this.m_Conservative = Convert.ToByte(false);
			this.m_Padding1 = 0;
			this.m_Padding2 = 0;
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x060022D7 RID: 8919 RVA: 0x0003A9EC File Offset: 0x00038BEC
		// (set) Token: 0x060022D8 RID: 8920 RVA: 0x0003AA04 File Offset: 0x00038C04
		public CullMode cullingMode
		{
			get
			{
				return this.m_CullingMode;
			}
			set
			{
				this.m_CullingMode = value;
			}
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x060022D9 RID: 8921 RVA: 0x0003AA10 File Offset: 0x00038C10
		// (set) Token: 0x060022DA RID: 8922 RVA: 0x0003AA2D File Offset: 0x00038C2D
		public bool depthClip
		{
			get
			{
				return Convert.ToBoolean(this.m_DepthClip);
			}
			set
			{
				this.m_DepthClip = Convert.ToByte(value);
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060022DB RID: 8923 RVA: 0x0003AA3C File Offset: 0x00038C3C
		// (set) Token: 0x060022DC RID: 8924 RVA: 0x0003AA59 File Offset: 0x00038C59
		public bool conservative
		{
			get
			{
				return Convert.ToBoolean(this.m_Conservative);
			}
			set
			{
				this.m_Conservative = Convert.ToByte(value);
			}
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x0003AA68 File Offset: 0x00038C68
		// (set) Token: 0x060022DE RID: 8926 RVA: 0x0003AA80 File Offset: 0x00038C80
		public int offsetUnits
		{
			get
			{
				return this.m_OffsetUnits;
			}
			set
			{
				this.m_OffsetUnits = value;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x0003AA8C File Offset: 0x00038C8C
		// (set) Token: 0x060022E0 RID: 8928 RVA: 0x0003AAA4 File Offset: 0x00038CA4
		public float offsetFactor
		{
			get
			{
				return this.m_OffsetFactor;
			}
			set
			{
				this.m_OffsetFactor = value;
			}
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x0003AAB0 File Offset: 0x00038CB0
		public bool Equals(RasterState other)
		{
			return this.m_CullingMode == other.m_CullingMode && this.m_OffsetUnits == other.m_OffsetUnits && this.m_OffsetFactor.Equals(other.m_OffsetFactor) && this.m_DepthClip == other.m_DepthClip && this.m_Conservative == other.m_Conservative;
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0003AB10 File Offset: 0x00038D10
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is RasterState && this.Equals((RasterState)obj);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0003AB48 File Offset: 0x00038D48
		public override int GetHashCode()
		{
			int num = (int)this.m_CullingMode;
			num = (num * 397) ^ this.m_OffsetUnits;
			num = (num * 397) ^ this.m_OffsetFactor.GetHashCode();
			num = (num * 397) ^ this.m_DepthClip.GetHashCode();
			return (num * 397) ^ this.m_Conservative.GetHashCode();
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0003ABB0 File Offset: 0x00038DB0
		public static bool operator ==(RasterState left, RasterState right)
		{
			return left.Equals(right);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0003ABCC File Offset: 0x00038DCC
		public static bool operator !=(RasterState left, RasterState right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000CF7 RID: 3319
		public static readonly RasterState defaultValue = new RasterState(CullMode.Back, 0, 0f, true);

		// Token: 0x04000CF8 RID: 3320
		private CullMode m_CullingMode;

		// Token: 0x04000CF9 RID: 3321
		private int m_OffsetUnits;

		// Token: 0x04000CFA RID: 3322
		private float m_OffsetFactor;

		// Token: 0x04000CFB RID: 3323
		private byte m_DepthClip;

		// Token: 0x04000CFC RID: 3324
		private byte m_Conservative;

		// Token: 0x04000CFD RID: 3325
		private byte m_Padding1;

		// Token: 0x04000CFE RID: 3326
		private byte m_Padding2;
	}
}
