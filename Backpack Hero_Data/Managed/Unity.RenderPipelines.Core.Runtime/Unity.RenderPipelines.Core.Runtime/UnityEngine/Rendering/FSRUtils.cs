using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000A9 RID: 169
	public static class FSRUtils
	{
		// Token: 0x060005CB RID: 1483 RVA: 0x0001B6CC File Offset: 0x000198CC
		public static void SetEasuConstants(CommandBuffer cmd, Vector2 inputViewportSizeInPixels, Vector2 inputImageSizeInPixels, Vector2 outputImageSizeInPixels)
		{
			Vector4 vector;
			vector.x = inputViewportSizeInPixels.x / outputImageSizeInPixels.x;
			vector.y = inputViewportSizeInPixels.y / outputImageSizeInPixels.y;
			vector.z = 0.5f * inputViewportSizeInPixels.x / outputImageSizeInPixels.x - 0.5f;
			vector.w = 0.5f * inputViewportSizeInPixels.y / outputImageSizeInPixels.y - 0.5f;
			Vector4 vector2;
			vector2.x = 1f / inputImageSizeInPixels.x;
			vector2.y = 1f / inputImageSizeInPixels.y;
			vector2.z = 1f / inputImageSizeInPixels.x;
			vector2.w = -1f / inputImageSizeInPixels.y;
			Vector4 vector3;
			vector3.x = -1f / inputImageSizeInPixels.x;
			vector3.y = 2f / inputImageSizeInPixels.y;
			vector3.z = 1f / inputImageSizeInPixels.x;
			vector3.w = 2f / inputImageSizeInPixels.y;
			Vector4 vector4;
			vector4.x = 0f / inputImageSizeInPixels.x;
			vector4.y = 4f / inputImageSizeInPixels.y;
			vector4.z = 0f;
			vector4.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants0, vector);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants1, vector2);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants2, vector3);
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrEasuConstants3, vector4);
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x0001B848 File Offset: 0x00019A48
		public static void SetRcasConstants(CommandBuffer cmd, float sharpnessStops = 0.2f)
		{
			float num = Mathf.Pow(2f, -sharpnessStops);
			ushort num2 = Mathf.FloatToHalf(num);
			float num3 = BitConverter.Int32BitsToSingle((int)num2 | ((int)num2 << 16));
			Vector4 vector;
			vector.x = num;
			vector.y = num3;
			vector.z = 0f;
			vector.w = 0f;
			cmd.SetGlobalVector(FSRUtils.ShaderConstants._FsrRcasConstants, vector);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x0001B8A8 File Offset: 0x00019AA8
		public static void SetRcasConstantsLinear(CommandBuffer cmd, float sharpnessLinear = 0.92f)
		{
			float num = (1f - sharpnessLinear) * 2.5f;
			FSRUtils.SetRcasConstants(cmd, num);
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x0001B8CA File Offset: 0x00019ACA
		public static bool IsSupported()
		{
			return SystemInfo.graphicsShaderLevel >= 45;
		}

		// Token: 0x04000366 RID: 870
		internal const float kMaxSharpnessStops = 2.5f;

		// Token: 0x04000367 RID: 871
		public const float kDefaultSharpnessStops = 0.2f;

		// Token: 0x04000368 RID: 872
		public const float kDefaultSharpnessLinear = 0.92f;

		// Token: 0x02000176 RID: 374
		private static class ShaderConstants
		{
			// Token: 0x040005A0 RID: 1440
			public static readonly int _FsrEasuConstants0 = Shader.PropertyToID("_FsrEasuConstants0");

			// Token: 0x040005A1 RID: 1441
			public static readonly int _FsrEasuConstants1 = Shader.PropertyToID("_FsrEasuConstants1");

			// Token: 0x040005A2 RID: 1442
			public static readonly int _FsrEasuConstants2 = Shader.PropertyToID("_FsrEasuConstants2");

			// Token: 0x040005A3 RID: 1443
			public static readonly int _FsrEasuConstants3 = Shader.PropertyToID("_FsrEasuConstants3");

			// Token: 0x040005A4 RID: 1444
			public static readonly int _FsrRcasConstants = Shader.PropertyToID("_FsrRcasConstants");
		}
	}
}
