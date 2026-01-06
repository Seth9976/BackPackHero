using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000116 RID: 278
	internal sealed class PreviousFrameData
	{
		// Token: 0x060008B5 RID: 2229 RVA: 0x000391C8 File Offset: 0x000373C8
		internal PreviousFrameData()
		{
			this.m_IsFirstFrame = true;
			this.m_LastFrameActive = -1;
			this.m_viewProjectionMatrix = Matrix4x4.identity;
			this.m_PreviousViewProjectionMatrix = Matrix4x4.identity;
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x00039217 File Offset: 0x00037417
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x0003921F File Offset: 0x0003741F
		internal bool isFirstFrame
		{
			get
			{
				return this.m_IsFirstFrame;
			}
			set
			{
				this.m_IsFirstFrame = value;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x00039228 File Offset: 0x00037428
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x00039230 File Offset: 0x00037430
		internal int lastFrameActive
		{
			get
			{
				return this.m_LastFrameActive;
			}
			set
			{
				this.m_LastFrameActive = value;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x00039239 File Offset: 0x00037439
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x00039241 File Offset: 0x00037441
		internal Matrix4x4 viewProjectionMatrix
		{
			get
			{
				return this.m_viewProjectionMatrix;
			}
			set
			{
				this.m_viewProjectionMatrix = value;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0003924A File Offset: 0x0003744A
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x00039252 File Offset: 0x00037452
		internal Matrix4x4 previousViewProjectionMatrix
		{
			get
			{
				return this.m_PreviousViewProjectionMatrix;
			}
			set
			{
				this.m_PreviousViewProjectionMatrix = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x0003925B File Offset: 0x0003745B
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00039263 File Offset: 0x00037463
		internal Matrix4x4[] previousViewProjectionMatrixStereo
		{
			get
			{
				return this.m_PreviousViewProjectionMatrixStereo;
			}
			set
			{
				this.m_PreviousViewProjectionMatrixStereo = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x0003926C File Offset: 0x0003746C
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00039274 File Offset: 0x00037474
		internal Matrix4x4[] viewProjectionMatrixStereo
		{
			get
			{
				return this.m_ViewProjectionMatrixStereo;
			}
			set
			{
				this.m_ViewProjectionMatrixStereo = value;
			}
		}

		// Token: 0x04000803 RID: 2051
		private bool m_IsFirstFrame;

		// Token: 0x04000804 RID: 2052
		private int m_LastFrameActive;

		// Token: 0x04000805 RID: 2053
		private Matrix4x4 m_viewProjectionMatrix;

		// Token: 0x04000806 RID: 2054
		private Matrix4x4 m_PreviousViewProjectionMatrix;

		// Token: 0x04000807 RID: 2055
		private Matrix4x4[] m_ViewProjectionMatrixStereo = new Matrix4x4[2];

		// Token: 0x04000808 RID: 2056
		private Matrix4x4[] m_PreviousViewProjectionMatrixStereo = new Matrix4x4[2];
	}
}
