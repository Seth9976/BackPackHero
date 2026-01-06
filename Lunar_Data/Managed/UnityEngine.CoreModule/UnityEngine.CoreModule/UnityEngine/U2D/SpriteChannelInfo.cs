using System;
using UnityEngine.Bindings;

namespace UnityEngine.U2D
{
	// Token: 0x02000271 RID: 625
	[VisibleToOtherModules]
	internal struct SpriteChannelInfo
	{
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0002B718 File Offset: 0x00029918
		// (set) Token: 0x06001B22 RID: 6946 RVA: 0x0002B735 File Offset: 0x00029935
		public unsafe void* buffer
		{
			get
			{
				return (void*)this.m_Buffer;
			}
			set
			{
				this.m_Buffer = (IntPtr)value;
			}
		}

		// Token: 0x1700057B RID: 1403
		// (get) Token: 0x06001B23 RID: 6947 RVA: 0x0002B744 File Offset: 0x00029944
		// (set) Token: 0x06001B24 RID: 6948 RVA: 0x0002B75C File Offset: 0x0002995C
		public int count
		{
			get
			{
				return this.m_Count;
			}
			set
			{
				this.m_Count = value;
			}
		}

		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0002B768 File Offset: 0x00029968
		// (set) Token: 0x06001B26 RID: 6950 RVA: 0x0002B780 File Offset: 0x00029980
		public int offset
		{
			get
			{
				return this.m_Offset;
			}
			set
			{
				this.m_Offset = value;
			}
		}

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x06001B27 RID: 6951 RVA: 0x0002B78C File Offset: 0x0002998C
		// (set) Token: 0x06001B28 RID: 6952 RVA: 0x0002B7A4 File Offset: 0x000299A4
		public int stride
		{
			get
			{
				return this.m_Stride;
			}
			set
			{
				this.m_Stride = value;
			}
		}

		// Token: 0x040008F0 RID: 2288
		[NativeName("buffer")]
		private IntPtr m_Buffer;

		// Token: 0x040008F1 RID: 2289
		[NativeName("count")]
		private int m_Count;

		// Token: 0x040008F2 RID: 2290
		[NativeName("offset")]
		private int m_Offset;

		// Token: 0x040008F3 RID: 2291
		[NativeName("stride")]
		private int m_Stride;
	}
}
