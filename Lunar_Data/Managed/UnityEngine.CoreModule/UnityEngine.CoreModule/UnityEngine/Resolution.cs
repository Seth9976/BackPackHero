using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000137 RID: 311
	[RequiredByNativeCode]
	public struct Resolution
	{
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0000ECB0 File Offset: 0x0000CEB0
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0000ECC8 File Offset: 0x0000CEC8
		public int width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0000ECD4 File Offset: 0x0000CED4
		// (set) Token: 0x060009E2 RID: 2530 RVA: 0x0000ECEC File Offset: 0x0000CEEC
		public int height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0000ECF8 File Offset: 0x0000CEF8
		// (set) Token: 0x060009E4 RID: 2532 RVA: 0x0000ED10 File Offset: 0x0000CF10
		public int refreshRate
		{
			get
			{
				return this.m_RefreshRate;
			}
			set
			{
				this.m_RefreshRate = value;
			}
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0000ED1C File Offset: 0x0000CF1C
		public override string ToString()
		{
			return UnityString.Format("{0} x {1} @ {2}Hz", new object[] { this.m_Width, this.m_Height, this.m_RefreshRate });
		}

		// Token: 0x040003E0 RID: 992
		private int m_Width;

		// Token: 0x040003E1 RID: 993
		private int m_Height;

		// Token: 0x040003E2 RID: 994
		private int m_RefreshRate;
	}
}
