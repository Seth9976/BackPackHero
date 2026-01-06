using System;
using System.Collections.Generic;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000098 RID: 152
	internal class Texture2DAtlasDynamic
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000174FE File Offset: 0x000156FE
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00017508 File Offset: 0x00015708
		public Texture2DAtlasDynamic(int width, int height, int capacity, GraphicsFormat format)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, 1, DepthBits.None, this.m_Format, FilterMode.Point, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, true, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, "");
			this.isAtlasTextureOwner = true;
			this.m_AtlasAllocator = new AtlasAllocatorDynamic(width, height, capacity);
			this.m_AllocationCache = new Dictionary<int, Vector4>(capacity);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00017588 File Offset: 0x00015788
		public Texture2DAtlasDynamic(int width, int height, int capacity, RTHandle atlasTexture)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = atlasTexture.rt.graphicsFormat;
			this.m_AtlasTexture = atlasTexture;
			this.isAtlasTextureOwner = false;
			this.m_AtlasAllocator = new AtlasAllocatorDynamic(width, height, capacity);
			this.m_AllocationCache = new Dictionary<int, Vector4>(capacity);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000175E4 File Offset: 0x000157E4
		public void Release()
		{
			this.ResetAllocator();
			if (this.isAtlasTextureOwner)
			{
				RTHandles.Release(this.m_AtlasTexture);
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000175FF File Offset: 0x000157FF
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Release();
			this.m_AllocationCache.Clear();
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00017618 File Offset: 0x00015818
		public bool AddTexture(CommandBuffer cmd, out Vector4 scaleOffset, Texture texture)
		{
			int instanceID = texture.GetInstanceID();
			if (this.m_AllocationCache.TryGetValue(instanceID, out scaleOffset))
			{
				return true;
			}
			int width = texture.width;
			int height = texture.height;
			if (this.m_AtlasAllocator.Allocate(out scaleOffset, instanceID, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				for (int i = 0; i < (texture as Texture2D).mipmapCount; i++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, i);
					Blitter.BlitQuad(cmd, texture, new Vector4(1f, 1f, 0f, 0f), scaleOffset, i, false);
				}
				this.m_AllocationCache.Add(instanceID, scaleOffset);
				return true;
			}
			return false;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00017706 File Offset: 0x00015906
		public bool IsCached(out Vector4 scaleOffset, int key)
		{
			return this.m_AllocationCache.TryGetValue(key, out scaleOffset);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00017718 File Offset: 0x00015918
		public bool EnsureTextureSlot(out bool isUploadNeeded, out Vector4 scaleOffset, int key, int width, int height)
		{
			isUploadNeeded = false;
			if (this.m_AllocationCache.TryGetValue(key, out scaleOffset))
			{
				return true;
			}
			if (!this.m_AtlasAllocator.Allocate(out scaleOffset, key, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
			this.m_AllocationCache.Add(key, scaleOffset);
			return true;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x000177A3 File Offset: 0x000159A3
		public void ReleaseTextureSlot(int key)
		{
			this.m_AtlasAllocator.Release(key);
			this.m_AllocationCache.Remove(key);
		}

		// Token: 0x04000322 RID: 802
		private RTHandle m_AtlasTexture;

		// Token: 0x04000323 RID: 803
		private bool isAtlasTextureOwner;

		// Token: 0x04000324 RID: 804
		private int m_Width;

		// Token: 0x04000325 RID: 805
		private int m_Height;

		// Token: 0x04000326 RID: 806
		private GraphicsFormat m_Format;

		// Token: 0x04000327 RID: 807
		private AtlasAllocatorDynamic m_AtlasAllocator;

		// Token: 0x04000328 RID: 808
		private Dictionary<int, Vector4> m_AllocationCache;
	}
}
