using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B4 RID: 180
	public static class XRUtils
	{
		// Token: 0x06000607 RID: 1543 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		public static void DrawOcclusionMesh(CommandBuffer cmd, Camera camera, bool stereoEnabled = true)
		{
			if (!XRGraphics.enabled || !camera.stereoEnabled || !stereoEnabled)
			{
				return;
			}
			RectInt rectInt = new RectInt(0, 0, camera.pixelWidth, camera.pixelHeight);
			cmd.DrawOcclusionMesh(rectInt);
		}
	}
}
