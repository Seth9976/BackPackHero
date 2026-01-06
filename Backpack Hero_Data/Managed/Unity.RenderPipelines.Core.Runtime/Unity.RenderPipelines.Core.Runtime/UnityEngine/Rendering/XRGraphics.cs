using System;
using UnityEngine.XR;

namespace UnityEngine.Rendering
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public class XRGraphics
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0000F2F9 File Offset: 0x0000D4F9
		// (set) Token: 0x0600032D RID: 813 RVA: 0x0000F30D File Offset: 0x0000D50D
		public static float eyeTextureResolutionScale
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureResolutionScale;
				}
				return 1f;
			}
			set
			{
				XRSettings.eyeTextureResolutionScale = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000F315 File Offset: 0x0000D515
		public static float renderViewportScale
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.renderViewportScale;
				}
				return 1f;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000F329 File Offset: 0x0000D529
		public static bool enabled
		{
			get
			{
				return XRSettings.enabled;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000330 RID: 816 RVA: 0x0000F330 File Offset: 0x0000D530
		public static bool isDeviceActive
		{
			get
			{
				return XRGraphics.enabled && XRSettings.isDeviceActive;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000F340 File Offset: 0x0000D540
		public static string loadedDeviceName
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.loadedDeviceName;
				}
				return "No XR device loaded";
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0000F354 File Offset: 0x0000D554
		public static string[] supportedDevices
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.supportedDevices;
				}
				return new string[1];
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000F369 File Offset: 0x0000D569
		public static XRGraphics.StereoRenderingMode stereoRenderingMode
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return (XRGraphics.StereoRenderingMode)XRSettings.stereoRenderingMode;
				}
				return XRGraphics.StereoRenderingMode.SinglePass;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0000F379 File Offset: 0x0000D579
		public static RenderTextureDescriptor eyeTextureDesc
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureDesc;
				}
				return new RenderTextureDescriptor(0, 0);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000F38F File Offset: 0x0000D58F
		public static int eyeTextureWidth
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureWidth;
				}
				return 0;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0000F39F File Offset: 0x0000D59F
		public static int eyeTextureHeight
		{
			get
			{
				if (XRGraphics.enabled)
				{
					return XRSettings.eyeTextureHeight;
				}
				return 0;
			}
		}

		// Token: 0x02000143 RID: 323
		public enum StereoRenderingMode
		{
			// Token: 0x04000506 RID: 1286
			MultiPass,
			// Token: 0x04000507 RID: 1287
			SinglePass,
			// Token: 0x04000508 RID: 1288
			SinglePassInstanced,
			// Token: 0x04000509 RID: 1289
			SinglePassMultiView
		}
	}
}
