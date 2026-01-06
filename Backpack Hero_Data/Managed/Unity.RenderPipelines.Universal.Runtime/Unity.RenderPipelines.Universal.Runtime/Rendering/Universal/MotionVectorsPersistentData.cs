using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000081 RID: 129
	internal sealed class MotionVectorsPersistentData
	{
		// Token: 0x060004CF RID: 1231 RVA: 0x0001C7D4 File Offset: 0x0001A9D4
		internal MotionVectorsPersistentData()
		{
			for (int i = 0; i < this.m_ViewProjection.Length; i++)
			{
				this.m_ViewProjection[i] = Matrix4x4.identity;
				this.m_PreviousViewProjection[i] = Matrix4x4.identity;
				this.m_LastFrameIndex[i] = -1;
				this.m_PrevAspectRatio[i] = -1f;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001C862 File Offset: 0x0001AA62
		internal int lastFrameIndex
		{
			get
			{
				return this.m_LastFrameIndex[0];
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001C86C File Offset: 0x0001AA6C
		internal Matrix4x4 viewProjection
		{
			get
			{
				return this.m_ViewProjection[0];
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001C87A File Offset: 0x0001AA7A
		internal Matrix4x4 previousViewProjection
		{
			get
			{
				return this.m_PreviousViewProjection[0];
			}
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001C888 File Offset: 0x0001AA88
		internal Matrix4x4[] viewProjectionStereo
		{
			get
			{
				return this.m_ViewProjection;
			}
		}

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060004D4 RID: 1236 RVA: 0x0001C890 File Offset: 0x0001AA90
		internal Matrix4x4[] previousViewProjectionStereo
		{
			get
			{
				return this.m_PreviousViewProjection;
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001C898 File Offset: 0x0001AA98
		internal int GetXRMultiPassId(ref CameraData cameraData)
		{
			if (!cameraData.xr.enabled)
			{
				return 0;
			}
			return cameraData.xr.multipassId;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001C8B4 File Offset: 0x0001AAB4
		public void Update(ref CameraData cameraData)
		{
			int xrmultiPassId = this.GetXRMultiPassId(ref cameraData);
			bool flag = this.m_PrevAspectRatio[xrmultiPassId] != cameraData.aspectRatio;
			if (this.m_LastFrameIndex[xrmultiPassId] != Time.frameCount || flag)
			{
				if (cameraData.xr.enabled && cameraData.xr.singlePassEnabled)
				{
					Matrix4x4 matrix4x = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrix(0), true) * cameraData.GetViewMatrix(0);
					Matrix4x4 matrix4x2 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrix(1), true) * cameraData.GetViewMatrix(1);
					this.m_PreviousViewProjection[0] = (flag ? matrix4x : this.m_ViewProjection[0]);
					this.m_PreviousViewProjection[1] = (flag ? matrix4x2 : this.m_ViewProjection[1]);
					this.m_ViewProjection[0] = matrix4x;
					this.m_ViewProjection[1] = matrix4x2;
				}
				else
				{
					Matrix4x4 matrix4x3 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrix(0), true) * cameraData.GetViewMatrix(0);
					this.m_PreviousViewProjection[xrmultiPassId] = (flag ? matrix4x3 : this.m_ViewProjection[xrmultiPassId]);
					this.m_ViewProjection[xrmultiPassId] = matrix4x3;
				}
				this.m_LastFrameIndex[xrmultiPassId] = Time.frameCount;
				this.m_PrevAspectRatio[xrmultiPassId] = cameraData.aspectRatio;
			}
		}

		// Token: 0x04000364 RID: 868
		private readonly Matrix4x4[] m_ViewProjection = new Matrix4x4[2];

		// Token: 0x04000365 RID: 869
		private readonly Matrix4x4[] m_PreviousViewProjection = new Matrix4x4[2];

		// Token: 0x04000366 RID: 870
		private readonly int[] m_LastFrameIndex = new int[2];

		// Token: 0x04000367 RID: 871
		private readonly float[] m_PrevAspectRatio = new float[2];
	}
}
