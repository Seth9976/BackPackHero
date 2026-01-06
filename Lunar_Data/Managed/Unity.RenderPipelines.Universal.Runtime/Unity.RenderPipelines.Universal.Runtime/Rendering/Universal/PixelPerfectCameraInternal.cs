using System;
using UnityEngine.Experimental.Rendering.Universal;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000030 RID: 48
	[Serializable]
	internal class PixelPerfectCameraInternal : ISerializationCallbackReceiver
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x0000FC74 File Offset: 0x0000DE74
		internal PixelPerfectCameraInternal(IPixelPerfectCamera component)
		{
			this.m_Component = component;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000FCA7 File Offset: 0x0000DEA7
		public void OnBeforeSerialize()
		{
			this.m_SerializableComponent = this.m_Component as PixelPerfectCamera;
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000FCBA File Offset: 0x0000DEBA
		public void OnAfterDeserialize()
		{
			if (this.m_SerializableComponent != null)
			{
				this.m_Component = this.m_SerializableComponent;
			}
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000FCD8 File Offset: 0x0000DED8
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
				this.useOffscreenRT = true;
				if (!upscaleRT)
				{
					if (this.cropFrameXAndY)
					{
						this.offscreenRTWidth = this.zoom * refResolutionX;
						this.offscreenRTHeight = this.zoom * refResolutionY;
					}
					else if (cropFrameY)
					{
						this.offscreenRTWidth = screenWidth;
						this.offscreenRTHeight = this.zoom * refResolutionY;
					}
					else
					{
						this.offscreenRTWidth = this.zoom * refResolutionX;
						this.offscreenRTHeight = screenHeight;
					}
				}
				else if (this.cropFrameXAndY)
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
			else if (upscaleRT && this.zoom > 1)
			{
				this.useOffscreenRT = true;
				this.offscreenRTWidth = screenWidth / this.zoom / 2 * 2;
				this.offscreenRTHeight = screenHeight / this.zoom / 2 * 2;
			}
			if (this.useOffscreenRT)
			{
				this.pixelRect = new Rect(0f, 0f, (float)this.offscreenRTWidth, (float)this.offscreenRTHeight);
			}
			else
			{
				this.pixelRect = Rect.zero;
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

		// Token: 0x060001C7 RID: 455 RVA: 0x0000FFD0 File Offset: 0x0000E1D0
		internal Rect CalculateFinalBlitPixelRect(int screenWidth, int screenHeight)
		{
			Rect rect = default(Rect);
			if (this.useStretchFill)
			{
				float num = (float)screenWidth / (float)screenHeight;
				float num2 = (float)this.m_Component.refResolutionX / (float)this.m_Component.refResolutionY;
				if (num > num2)
				{
					rect.height = (float)screenHeight;
					rect.width = (float)screenHeight * num2;
					rect.x = (float)((screenWidth - (int)rect.width) / 2);
					rect.y = 0f;
				}
				else
				{
					rect.width = (float)screenWidth;
					rect.height = (float)screenWidth / num2;
					rect.y = (float)((screenHeight - (int)rect.height) / 2);
					rect.x = 0f;
				}
			}
			else
			{
				if (this.m_Component.upscaleRT)
				{
					rect.height = (float)(this.zoom * this.offscreenRTHeight);
					rect.width = (float)(this.zoom * this.offscreenRTWidth);
				}
				else
				{
					rect.height = (float)this.offscreenRTHeight;
					rect.width = (float)this.offscreenRTWidth;
				}
				rect.x = (float)((screenWidth - (int)rect.width) / 2);
				rect.y = (float)((screenHeight - (int)rect.height) / 2);
			}
			return rect;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00010100 File Offset: 0x0000E300
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

		// Token: 0x04000118 RID: 280
		[NonSerialized]
		private IPixelPerfectCamera m_Component;

		// Token: 0x04000119 RID: 281
		private PixelPerfectCamera m_SerializableComponent;

		// Token: 0x0400011A RID: 282
		internal float originalOrthoSize;

		// Token: 0x0400011B RID: 283
		internal bool hasPostProcessLayer;

		// Token: 0x0400011C RID: 284
		internal bool cropFrameXAndY;

		// Token: 0x0400011D RID: 285
		internal bool cropFrameXOrY;

		// Token: 0x0400011E RID: 286
		internal bool useStretchFill;

		// Token: 0x0400011F RID: 287
		internal int zoom = 1;

		// Token: 0x04000120 RID: 288
		internal bool useOffscreenRT;

		// Token: 0x04000121 RID: 289
		internal int offscreenRTWidth;

		// Token: 0x04000122 RID: 290
		internal int offscreenRTHeight;

		// Token: 0x04000123 RID: 291
		internal Rect pixelRect = Rect.zero;

		// Token: 0x04000124 RID: 292
		internal float orthoSize = 1f;

		// Token: 0x04000125 RID: 293
		internal float unitsPerPixel;

		// Token: 0x04000126 RID: 294
		internal int cinemachineVCamZoom = 1;
	}
}
