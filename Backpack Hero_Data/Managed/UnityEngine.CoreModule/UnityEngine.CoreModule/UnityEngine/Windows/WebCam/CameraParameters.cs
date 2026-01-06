using System;
using System.Linq;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.Windows.WebCam
{
	// Token: 0x020002B1 RID: 689
	[MovedFrom("UnityEngine.XR.WSA.WebCam")]
	[NativeHeader("PlatformDependent/Win/Webcam/CameraParameters.h")]
	[UsedByNativeCode]
	public struct CameraParameters
	{
		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06001CFD RID: 7421 RVA: 0x0002E508 File Offset: 0x0002C708
		// (set) Token: 0x06001CFE RID: 7422 RVA: 0x0002E520 File Offset: 0x0002C720
		public float hologramOpacity
		{
			get
			{
				return this.m_HologramOpacity;
			}
			set
			{
				this.m_HologramOpacity = value;
			}
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06001CFF RID: 7423 RVA: 0x0002E52C File Offset: 0x0002C72C
		// (set) Token: 0x06001D00 RID: 7424 RVA: 0x0002E544 File Offset: 0x0002C744
		public float frameRate
		{
			get
			{
				return this.m_FrameRate;
			}
			set
			{
				this.m_FrameRate = value;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06001D01 RID: 7425 RVA: 0x0002E550 File Offset: 0x0002C750
		// (set) Token: 0x06001D02 RID: 7426 RVA: 0x0002E568 File Offset: 0x0002C768
		public int cameraResolutionWidth
		{
			get
			{
				return this.m_CameraResolutionWidth;
			}
			set
			{
				this.m_CameraResolutionWidth = value;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06001D03 RID: 7427 RVA: 0x0002E574 File Offset: 0x0002C774
		// (set) Token: 0x06001D04 RID: 7428 RVA: 0x0002E58C File Offset: 0x0002C78C
		public int cameraResolutionHeight
		{
			get
			{
				return this.m_CameraResolutionHeight;
			}
			set
			{
				this.m_CameraResolutionHeight = value;
			}
		}

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x06001D05 RID: 7429 RVA: 0x0002E598 File Offset: 0x0002C798
		// (set) Token: 0x06001D06 RID: 7430 RVA: 0x0002E5B0 File Offset: 0x0002C7B0
		public CapturePixelFormat pixelFormat
		{
			get
			{
				return this.m_PixelFormat;
			}
			set
			{
				this.m_PixelFormat = value;
			}
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x0002E5BC File Offset: 0x0002C7BC
		public CameraParameters(WebCamMode webCamMode)
		{
			this.m_HologramOpacity = 1f;
			this.m_PixelFormat = CapturePixelFormat.BGRA32;
			this.m_FrameRate = 0f;
			this.m_CameraResolutionWidth = 0;
			this.m_CameraResolutionHeight = 0;
			bool flag = webCamMode == WebCamMode.PhotoMode;
			if (flag)
			{
				Resolution resolution = Enumerable.First<Resolution>(Enumerable.OrderByDescending<Resolution, int>(PhotoCapture.SupportedResolutions, (Resolution res) => res.width * res.height));
				this.m_CameraResolutionWidth = resolution.width;
				this.m_CameraResolutionHeight = resolution.height;
			}
			else
			{
				bool flag2 = webCamMode == WebCamMode.VideoMode;
				if (flag2)
				{
					Resolution resolution2 = Enumerable.First<Resolution>(Enumerable.OrderByDescending<Resolution, int>(VideoCapture.SupportedResolutions, (Resolution res) => res.width * res.height));
					float num = Enumerable.First<float>(Enumerable.OrderByDescending<float, float>(VideoCapture.GetSupportedFrameRatesForResolution(resolution2), (float fps) => fps));
					this.m_CameraResolutionWidth = resolution2.width;
					this.m_CameraResolutionHeight = resolution2.height;
					this.m_FrameRate = num;
				}
			}
		}

		// Token: 0x0400097A RID: 2426
		private float m_HologramOpacity;

		// Token: 0x0400097B RID: 2427
		private float m_FrameRate;

		// Token: 0x0400097C RID: 2428
		private int m_CameraResolutionWidth;

		// Token: 0x0400097D RID: 2429
		private int m_CameraResolutionHeight;

		// Token: 0x0400097E RID: 2430
		private CapturePixelFormat m_PixelFormat;
	}
}
