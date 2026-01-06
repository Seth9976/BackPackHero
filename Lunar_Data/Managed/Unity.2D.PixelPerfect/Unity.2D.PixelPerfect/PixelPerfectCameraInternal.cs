using System;

namespace UnityEngine.U2D
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	internal class PixelPerfectCameraInternal : ISerializationCallbackReceiver
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000024E1 File Offset: 0x000006E1
		internal PixelPerfectCameraInternal(IPixelPerfectCamera component)
		{
			this.m_Component = component;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002514 File Offset: 0x00000714
		public void OnBeforeSerialize()
		{
			this.m_SerializableComponent = this.m_Component as PixelPerfectCamera;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002527 File Offset: 0x00000727
		public void OnAfterDeserialize()
		{
			if (this.m_SerializableComponent != null)
			{
				this.m_Component = this.m_SerializableComponent;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002544 File Offset: 0x00000744
		internal void CalculateCameraProperties(int screenWidth, int screenHeight)
		{
			int assetsPPU = this.m_Component.assetsPPU;
			int refResolutionX = this.m_Component.refResolutionX;
			int refResolutionY = this.m_Component.refResolutionY;
			bool upscaleRT = this.m_Component.upscaleRT;
			bool pixelSnapping = this.m_Component.pixelSnapping;
			bool cropFrameX = this.m_Component.cropFrameX;
			bool cropFrameY = this.m_Component.cropFrameY;
			bool stretchFill = this.m_Component.stretchFill;
			this.cropFrameXAndY = cropFrameY && cropFrameX;
			this.cropFrameXOrY = cropFrameY || cropFrameX;
			this.useStretchFill = this.cropFrameXAndY && stretchFill;
			int num = screenHeight / refResolutionY;
			int num2 = screenWidth / refResolutionX;
			this.zoom = Math.Max(1, Math.Min(num, num2));
			this.useOffscreenRT = false;
			this.offscreenRTWidth = 0;
			this.offscreenRTHeight = 0;
			if (this.cropFrameXOrY)
			{
				if (!upscaleRT)
				{
					if (this.useStretchFill)
					{
						this.useOffscreenRT = true;
						this.offscreenRTWidth = this.zoom * refResolutionX;
						this.offscreenRTHeight = this.zoom * refResolutionY;
					}
				}
				else
				{
					this.useOffscreenRT = true;
					if (this.cropFrameXAndY)
					{
						this.offscreenRTWidth = refResolutionX;
						this.offscreenRTHeight = refResolutionY;
					}
					else if (cropFrameY)
					{
						this.offscreenRTWidth = screenWidth / this.zoom / 2 * 2;
						this.offscreenRTHeight = refResolutionY;
					}
					else
					{
						this.offscreenRTWidth = refResolutionX;
						this.offscreenRTHeight = screenHeight / this.zoom / 2 * 2;
					}
				}
			}
			else if (upscaleRT && this.zoom > 1)
			{
				this.useOffscreenRT = true;
				this.offscreenRTWidth = screenWidth / this.zoom / 2 * 2;
				this.offscreenRTHeight = screenHeight / this.zoom / 2 * 2;
			}
			this.pixelRect = Rect.zero;
			if (this.cropFrameXOrY && !upscaleRT && !this.useStretchFill)
			{
				if (this.cropFrameXAndY)
				{
					this.pixelRect.width = (float)(this.zoom * refResolutionX);
					this.pixelRect.height = (float)(this.zoom * refResolutionY);
				}
				else if (cropFrameY)
				{
					this.pixelRect.width = (float)screenWidth;
					this.pixelRect.height = (float)(this.zoom * refResolutionY);
				}
				else
				{
					this.pixelRect.width = (float)(this.zoom * refResolutionX);
					this.pixelRect.height = (float)screenHeight;
				}
				this.pixelRect.x = (float)((screenWidth - (int)this.pixelRect.width) / 2);
				this.pixelRect.y = (float)((screenHeight - (int)this.pixelRect.height) / 2);
			}
			else if (this.useOffscreenRT)
			{
				this.pixelRect = new Rect(0f, 0f, (float)this.offscreenRTWidth, (float)this.offscreenRTHeight);
			}
			if (cropFrameY)
			{
				this.orthoSize = (float)refResolutionY * 0.5f / (float)assetsPPU;
			}
			else if (cropFrameX)
			{
				float num3 = ((this.pixelRect == Rect.zero) ? ((float)screenWidth / (float)screenHeight) : (this.pixelRect.width / this.pixelRect.height));
				this.orthoSize = (float)refResolutionX / num3 * 0.5f / (float)assetsPPU;
			}
			else if (upscaleRT && this.zoom > 1)
			{
				this.orthoSize = (float)this.offscreenRTHeight * 0.5f / (float)assetsPPU;
			}
			else
			{
				float num4 = ((this.pixelRect == Rect.zero) ? ((float)screenHeight) : this.pixelRect.height);
				this.orthoSize = num4 * 0.5f / (float)(this.zoom * assetsPPU);
			}
			if (upscaleRT || (!upscaleRT && pixelSnapping))
			{
				this.unitsPerPixel = 1f / (float)assetsPPU;
				return;
			}
			this.unitsPerPixel = 1f / (float)(this.zoom * assetsPPU);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000028DC File Offset: 0x00000ADC
		internal Rect CalculatePostRenderPixelRect(float cameraAspect, int screenWidth, int screenHeight)
		{
			Rect rect = default(Rect);
			if (this.useStretchFill)
			{
				if ((float)screenWidth / (float)screenHeight > cameraAspect)
				{
					rect.height = (float)screenHeight;
					rect.width = (float)screenHeight * cameraAspect;
					rect.x = (float)((screenWidth - (int)rect.width) / 2);
					rect.y = 0f;
				}
				else
				{
					rect.width = (float)screenWidth;
					rect.height = (float)screenWidth / cameraAspect;
					rect.y = (float)((screenHeight - (int)rect.height) / 2);
					rect.x = 0f;
				}
			}
			else
			{
				rect.height = (float)(this.zoom * this.offscreenRTHeight);
				rect.width = (float)(this.zoom * this.offscreenRTWidth);
				rect.x = (float)((screenWidth - (int)rect.width) / 2);
				rect.y = (float)((screenHeight - (int)rect.height) / 2);
			}
			return rect;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000029C4 File Offset: 0x00000BC4
		internal float CorrectCinemachineOrthoSize(float targetOrthoSize)
		{
			float num;
			if (this.m_Component.upscaleRT)
			{
				this.cinemachineVCamZoom = Math.Max(1, Mathf.RoundToInt(this.orthoSize / targetOrthoSize));
				num = this.orthoSize / (float)this.cinemachineVCamZoom;
			}
			else
			{
				this.cinemachineVCamZoom = Math.Max(1, Mathf.RoundToInt((float)this.zoom * this.orthoSize / targetOrthoSize));
				num = (float)this.zoom * this.orthoSize / (float)this.cinemachineVCamZoom;
			}
			if (!this.m_Component.upscaleRT && !this.m_Component.pixelSnapping)
			{
				this.unitsPerPixel = 1f / (float)(this.cinemachineVCamZoom * this.m_Component.assetsPPU);
			}
			return num;
		}

		// Token: 0x0400000C RID: 12
		[NonSerialized]
		private IPixelPerfectCamera m_Component;

		// Token: 0x0400000D RID: 13
		private PixelPerfectCamera m_SerializableComponent;

		// Token: 0x0400000E RID: 14
		internal float originalOrthoSize;

		// Token: 0x0400000F RID: 15
		internal bool hasPostProcessLayer;

		// Token: 0x04000010 RID: 16
		internal bool cropFrameXAndY;

		// Token: 0x04000011 RID: 17
		internal bool cropFrameXOrY;

		// Token: 0x04000012 RID: 18
		internal bool useStretchFill;

		// Token: 0x04000013 RID: 19
		internal int zoom = 1;

		// Token: 0x04000014 RID: 20
		internal bool useOffscreenRT;

		// Token: 0x04000015 RID: 21
		internal int offscreenRTWidth;

		// Token: 0x04000016 RID: 22
		internal int offscreenRTHeight;

		// Token: 0x04000017 RID: 23
		internal Rect pixelRect = Rect.zero;

		// Token: 0x04000018 RID: 24
		internal float orthoSize = 1f;

		// Token: 0x04000019 RID: 25
		internal float unitsPerPixel;

		// Token: 0x0400001A RID: 26
		internal int cinemachineVCamZoom = 1;
	}
}
