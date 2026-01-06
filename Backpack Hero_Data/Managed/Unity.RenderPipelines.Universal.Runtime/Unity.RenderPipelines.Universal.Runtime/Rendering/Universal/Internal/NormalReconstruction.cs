using System;

namespace UnityEngine.Rendering.Universal.Internal
{
	// Token: 0x02000107 RID: 263
	public static class NormalReconstruction
	{
		// Token: 0x0600081C RID: 2076 RVA: 0x00033248 File Offset: 0x00031448
		public static void SetupProperties(CommandBuffer cmd, in CameraData cameraData)
		{
			int num = ((cameraData.xr.enabled && cameraData.xr.singlePassEnabled) ? 2 : 1);
			for (int i = 0; i < num; i++)
			{
				CameraData cameraData2 = cameraData;
				Matrix4x4 viewMatrix = cameraData2.GetViewMatrix(i);
				cameraData2 = cameraData;
				Matrix4x4 projectionMatrix = cameraData2.GetProjectionMatrix(i);
				NormalReconstruction.s_NormalReconstructionMatrix[i] = projectionMatrix * viewMatrix;
				Matrix4x4 matrix4x = viewMatrix;
				matrix4x.SetColumn(3, new Vector4(0f, 0f, 0f, 1f));
				Matrix4x4 inverse = (projectionMatrix * matrix4x).inverse;
				NormalReconstruction.s_NormalReconstructionMatrix[i] = inverse;
			}
			cmd.SetGlobalMatrixArray(NormalReconstruction.s_NormalReconstructionMatrixID, NormalReconstruction.s_NormalReconstructionMatrix);
		}

		// Token: 0x04000780 RID: 1920
		private static readonly int s_NormalReconstructionMatrixID = Shader.PropertyToID("_NormalReconstructionMatrix");

		// Token: 0x04000781 RID: 1921
		private static Matrix4x4[] s_NormalReconstructionMatrix = new Matrix4x4[2];
	}
}
