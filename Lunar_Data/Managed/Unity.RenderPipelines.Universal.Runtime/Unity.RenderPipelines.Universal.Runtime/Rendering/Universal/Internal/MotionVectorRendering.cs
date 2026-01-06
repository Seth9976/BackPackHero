using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000106 RID: 262
	internal sealed class MotionVectorRendering
	{
		// Token: 0x06000816 RID: 2070 RVA: 0x00033065 File Offset: 0x00031265
		private MotionVectorRendering()
		{
			this.m_CameraFrameData = new Dictionary<Camera, PreviousFrameData>();
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x00033078 File Offset: 0x00031278
		public static MotionVectorRendering instance
		{
			get
			{
				if (MotionVectorRendering.s_Instance == null)
				{
					MotionVectorRendering.s_Instance = new MotionVectorRendering();
				}
				return MotionVectorRendering.s_Instance;
			}
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00033090 File Offset: 0x00031290
		public void Clear()
		{
			this.m_CameraFrameData.Clear();
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000330A0 File Offset: 0x000312A0
		public PreviousFrameData GetMotionDataForCamera(Camera camera, CameraData camData)
		{
			PreviousFrameData previousFrameData;
			if (!this.m_CameraFrameData.TryGetValue(camera, out previousFrameData))
			{
				previousFrameData = new PreviousFrameData();
				this.m_CameraFrameData.Add(camera, previousFrameData);
			}
			this.CalculateTime();
			this.UpdateMotionData(camera, camData, previousFrameData);
			return previousFrameData;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x000330E0 File Offset: 0x000312E0
		private void CalculateTime()
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			uint frameCount = (uint)Time.frameCount;
			bool flag = this.m_FrameCount != frameCount;
			this.m_FrameCount = frameCount;
			if (flag)
			{
				this.m_LastTime = ((this.m_Time > 0f) ? this.m_Time : realtimeSinceStartup);
				this.m_Time = realtimeSinceStartup;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00033134 File Offset: 0x00031334
		private void UpdateMotionData(Camera camera, CameraData cameraData, PreviousFrameData motionData)
		{
			if (cameraData.xr.enabled)
			{
				Matrix4x4 matrix4x = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrix(0), true) * cameraData.GetViewMatrix(0);
				Matrix4x4 matrix4x2 = GL.GetGPUProjectionMatrix(cameraData.GetProjectionMatrix(1), true) * cameraData.GetViewMatrix(1);
				if (motionData.lastFrameActive != Time.frameCount)
				{
					bool isFirstFrame = motionData.isFirstFrame;
					Matrix4x4[] previousViewProjectionMatrixStereo = motionData.previousViewProjectionMatrixStereo;
					previousViewProjectionMatrixStereo[0] = (isFirstFrame ? matrix4x : previousViewProjectionMatrixStereo[0]);
					previousViewProjectionMatrixStereo[1] = (isFirstFrame ? matrix4x2 : previousViewProjectionMatrixStereo[1]);
					motionData.isFirstFrame = false;
				}
				Matrix4x4[] viewProjectionMatrixStereo = motionData.viewProjectionMatrixStereo;
				viewProjectionMatrixStereo[0] = matrix4x;
				viewProjectionMatrixStereo[1] = matrix4x2;
			}
			else
			{
				Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(camera.projectionMatrix, true);
				Matrix4x4 worldToCameraMatrix = camera.worldToCameraMatrix;
				Matrix4x4 matrix4x3 = gpuprojectionMatrix * worldToCameraMatrix;
				if (motionData.lastFrameActive != Time.frameCount)
				{
					motionData.previousViewProjectionMatrix = (motionData.isFirstFrame ? matrix4x3 : motionData.viewProjectionMatrix);
					motionData.isFirstFrame = false;
				}
				motionData.viewProjectionMatrix = matrix4x3;
			}
			motionData.lastFrameActive = Time.frameCount;
		}

		// Token: 0x0400077B RID: 1915
		private static MotionVectorRendering s_Instance;

		// Token: 0x0400077C RID: 1916
		private Dictionary<Camera, PreviousFrameData> m_CameraFrameData;

		// Token: 0x0400077D RID: 1917
		private uint m_FrameCount;

		// Token: 0x0400077E RID: 1918
		private float m_LastTime;

		// Token: 0x0400077F RID: 1919
		private float m_Time;
	}
}
