using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x02000096 RID: 150
	public class Texture2DAtlas
	{
		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x000167AB File Offset: 0x000149AB
		public static int maxMipLevelPadding
		{
			get
			{
				return Texture2DAtlas.s_MaxMipLevelPadding;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x000167B2 File Offset: 0x000149B2
		public RTHandle AtlasTexture
		{
			get
			{
				return this.m_AtlasTexture;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x000167BC File Offset: 0x000149BC
		public Texture2DAtlas(int width, int height, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, bool powerOfTwoPadding = false, string name = "", bool useMipMap = true)
		{
			this.m_Width = width;
			this.m_Height = height;
			this.m_Format = format;
			this.m_UseMipMaps = useMipMap;
			this.m_AtlasTexture = RTHandles.Alloc(this.m_Width, this.m_Height, 1, DepthBits.None, this.m_Format, filterMode, TextureWrapMode.Clamp, TextureDimension.Tex2D, false, useMipMap, false, false, 1, 0f, MSAASamples.None, false, false, RenderTextureMemoryless.None, name);
			this.m_IsAtlasTextureOwner = true;
			int num = (useMipMap ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int i = 0; i < num; i++)
			{
				Graphics.SetRenderTarget(this.m_AtlasTexture, i);
				GL.Clear(false, true, Color.clear);
			}
			this.m_AtlasAllocator = new AtlasAllocator(width, height, powerOfTwoPadding);
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001689C File Offset: 0x00014A9C
		public void Release()
		{
			this.ResetAllocator();
			if (this.m_IsAtlasTextureOwner)
			{
				RTHandles.Release(this.m_AtlasTexture);
			}
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x000168B7 File Offset: 0x00014AB7
		public void ResetAllocator()
		{
			this.m_AtlasAllocator.Reset();
			this.m_AllocationCache.Clear();
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x000168DC File Offset: 0x00014ADC
		public void ClearTarget(CommandBuffer cmd)
		{
			int num = (this.m_UseMipMaps ? this.GetTextureMipmapCount(this.m_Width, this.m_Height) : 1);
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				Blitter.BlitQuad(cmd, Texture2D.blackTexture, Texture2DAtlas.fullScaleOffset, Texture2DAtlas.fullScaleOffset, i, true);
			}
			this.m_IsGPUTextureUpToDate.Clear();
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00016947 File Offset: 0x00014B47
		private protected int GetTextureMipmapCount(int width, int height)
		{
			if (!this.m_UseMipMaps)
			{
				return 1;
			}
			return Mathf.FloorToInt(Mathf.Log((float)Mathf.Max(width, height), 2f)) + 1;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001696C File Offset: 0x00014B6C
		private protected bool Is2D(Texture texture)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			return texture is Texture2D || (renderTexture != null && renderTexture.dimension == TextureDimension.Tex2D);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00016998 File Offset: 0x00014B98
		private protected bool IsSingleChannelBlit(Texture source, Texture destination)
		{
			uint componentCount = GraphicsFormatUtility.GetComponentCount(source.graphicsFormat);
			uint componentCount2 = GraphicsFormatUtility.GetComponentCount(destination.graphicsFormat);
			if (componentCount == 1U || componentCount2 == 1U)
			{
				if (componentCount != componentCount2)
				{
					return true;
				}
				int num = (1 << (int)(GraphicsFormatUtility.GetSwizzleA(source.graphicsFormat) & (FormatSwizzle)7) << 24) | (1 << (int)(GraphicsFormatUtility.GetSwizzleB(source.graphicsFormat) & (FormatSwizzle)7) << 16) | (1 << (int)(GraphicsFormatUtility.GetSwizzleG(source.graphicsFormat) & (FormatSwizzle)7) << 8) | (1 << (int)(GraphicsFormatUtility.GetSwizzleR(source.graphicsFormat) & (FormatSwizzle)7));
				int num2 = (1 << (int)(GraphicsFormatUtility.GetSwizzleA(destination.graphicsFormat) & (FormatSwizzle)7) << 24) | (1 << (int)(GraphicsFormatUtility.GetSwizzleB(destination.graphicsFormat) & (FormatSwizzle)7) << 16) | (1 << (int)(GraphicsFormatUtility.GetSwizzleG(destination.graphicsFormat) & (FormatSwizzle)7) << 8) | (1 << (int)(GraphicsFormatUtility.GetSwizzleR(destination.graphicsFormat) & (FormatSwizzle)7));
				if (num != num2)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00016A7C File Offset: 0x00014C7C
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, Texture2DAtlas.BlitType blitType)
		{
			int num = this.GetTextureMipmapCount(texture.width, texture.height);
			if (!blitMips)
			{
				num = 1;
			}
			for (int i = 0; i < num; i++)
			{
				cmd.SetRenderTarget(this.m_AtlasTexture, i);
				switch (blitType)
				{
				case Texture2DAtlas.BlitType.Default:
					Blitter.BlitQuad(cmd, texture, sourceScaleOffset, scaleOffset, i, true);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedral:
					Blitter.BlitCubeToOctahedral2DQuad(cmd, texture, scaleOffset, i);
					break;
				case Texture2DAtlas.BlitType.SingleChannel:
					Blitter.BlitQuadSingleChannel(cmd, texture, sourceScaleOffset, scaleOffset, i);
					break;
				case Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel:
					Blitter.BlitCubeToOctahedral2DQuadSingleChannel(cmd, texture, scaleOffset, i);
					break;
				}
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00016B08 File Offset: 0x00014D08
		private protected void MarkGPUTextureValid(int instanceId, bool mipAreValid = false)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = (mipAreValid ? 2 : 1);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016B1D File Offset: 0x00014D1D
		private protected void MarkGPUTextureInvalid(int instanceId)
		{
			this.m_IsGPUTextureUpToDate[instanceId] = 0;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016B2C File Offset: 0x00014D2C
		public virtual void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (this.Is2D(texture))
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.Default;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.SingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, blitType);
				int num = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
				this.MarkGPUTextureValid(num, blitMips);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00016B94 File Offset: 0x00014D94
		public virtual void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			this.BlitTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, overrideInstanceID);
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00016BA8 File Offset: 0x00014DA8
		public virtual void BlitCubeTexture2D(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (texture.dimension == TextureDimension.Cube)
			{
				Texture2DAtlas.BlitType blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedral;
				if (this.IsSingleChannelBlit(texture, this.m_AtlasTexture.m_RT))
				{
					blitType = Texture2DAtlas.BlitType.CubeTo2DOctahedralSingleChannel;
				}
				this.Blit2DTexture(cmd, scaleOffset, texture, new Vector4(1f, 1f, 0f, 0f), blitMips, blitType);
				int num = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
				this.MarkGPUTextureValid(num, blitMips);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016C28 File Offset: 0x00014E28
		public virtual bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			int num = ((overrideInstanceID != -1) ? overrideInstanceID : this.GetTextureID(texture));
			bool flag = this.AllocateTextureWithoutBlit(num, width, height, ref scaleOffset);
			if (flag)
			{
				if (this.Is2D(texture))
				{
					this.BlitTexture(cmd, scaleOffset, texture, Texture2DAtlas.fullScaleOffset, true, -1);
				}
				else
				{
					this.BlitCubeTexture2D(cmd, scaleOffset, texture, true, -1);
				}
				this.MarkGPUTextureValid(num, true);
				this.m_TextureHashes[num] = CoreUtils.GetTextureHash(texture);
			}
			return flag;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00016C9F File Offset: 0x00014E9F
		public bool AllocateTextureWithoutBlit(Texture texture, int width, int height, ref Vector4 scaleOffset)
		{
			return this.AllocateTextureWithoutBlit(texture.GetInstanceID(), width, height, ref scaleOffset);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00016CB4 File Offset: 0x00014EB4
		public virtual bool AllocateTextureWithoutBlit(int instanceId, int width, int height, ref Vector4 scaleOffset)
		{
			scaleOffset = Vector4.zero;
			if (this.m_AtlasAllocator.Allocate(ref scaleOffset, width, height))
			{
				scaleOffset.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
				this.m_AllocationCache[instanceId] = new ValueTuple<Vector4, Vector2Int>(scaleOffset, new Vector2Int(width, height));
				this.MarkGPUTextureInvalid(instanceId);
				this.m_TextureHashes[instanceId] = -1;
				return true;
			}
			return false;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00016D54 File Offset: 0x00014F54
		private protected int GetTextureHash(Texture textureA, Texture textureB)
		{
			return CoreUtils.GetTextureHash(textureA) + 23 * CoreUtils.GetTextureHash(textureB);
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00016D66 File Offset: 0x00014F66
		public int GetTextureID(Texture texture)
		{
			return texture.GetInstanceID();
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00016D6E File Offset: 0x00014F6E
		public int GetTextureID(Texture textureA, Texture textureB)
		{
			return this.GetTextureID(textureA) + 23 * this.GetTextureID(textureB);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016D82 File Offset: 0x00014F82
		public bool IsCached(out Vector4 scaleOffset, Texture textureA, Texture textureB)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(textureA, textureB));
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016D93 File Offset: 0x00014F93
		public bool IsCached(out Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, this.GetTextureID(texture));
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016DA4 File Offset: 0x00014FA4
		public bool IsCached(out Vector4 scaleOffset, int id)
		{
			ValueTuple<Vector4, Vector2Int> valueTuple;
			bool flag = this.m_AllocationCache.TryGetValue(id, out valueTuple);
			scaleOffset = valueTuple.Item1;
			return flag;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016DCC File Offset: 0x00014FCC
		internal Vector2Int GetCachedTextureSize(int id)
		{
			ValueTuple<Vector4, Vector2Int> valueTuple;
			this.m_AllocationCache.TryGetValue(id, out valueTuple);
			return valueTuple.Item2;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public virtual bool NeedsUpdate(Texture texture, bool needMips = false)
		{
			RenderTexture renderTexture = texture as RenderTexture;
			int textureID = this.GetTextureID(texture);
			int textureHash = CoreUtils.GetTextureHash(texture);
			if (renderTexture != null)
			{
				int num;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num))
				{
					if ((ulong)renderTexture.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
				}
			}
			else
			{
				int num2;
				if (this.m_TextureHashes.TryGetValue(textureID, out num2) && num2 != textureHash)
				{
					this.m_TextureHashes[textureID] = textureHash;
					return true;
				}
				int num3;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num3))
				{
					return num3 == 0 || (needMips && num3 == 1);
				}
			}
			return false;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00016EA8 File Offset: 0x000150A8
		public virtual bool NeedsUpdate(Texture textureA, Texture textureB, bool needMips = false)
		{
			RenderTexture renderTexture = textureA as RenderTexture;
			RenderTexture renderTexture2 = textureB as RenderTexture;
			int textureID = this.GetTextureID(textureA, textureB);
			int textureHash = this.GetTextureHash(textureA, textureB);
			if (renderTexture != null || renderTexture2 != null)
			{
				int num;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num))
				{
					if (renderTexture != null && renderTexture2 != null && (ulong)Math.Min(renderTexture.updateCount, renderTexture2.updateCount) != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)Math.Min(renderTexture.updateCount, renderTexture2.updateCount);
						return true;
					}
					if (renderTexture != null && (ulong)renderTexture.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture.updateCount;
						return true;
					}
					if (renderTexture2 != null && (ulong)renderTexture2.updateCount != (ulong)((long)num))
					{
						this.m_IsGPUTextureUpToDate[textureID] = (int)renderTexture2.updateCount;
						return true;
					}
				}
				else
				{
					this.m_IsGPUTextureUpToDate[textureID] = textureHash;
				}
			}
			else
			{
				int num2;
				if (this.m_TextureHashes.TryGetValue(textureID, out num2) && num2 != textureHash)
				{
					this.m_TextureHashes[textureID] = textureID;
					return true;
				}
				int num3;
				if (this.m_IsGPUTextureUpToDate.TryGetValue(textureID, out num3))
				{
					return num3 == 0 || (needMips && num3 == 1);
				}
			}
			return false;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00016FEC File Offset: 0x000151EC
		public virtual bool AddTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture)
		{
			return this.IsCached(out scaleOffset, texture) || this.AllocateTexture(cmd, ref scaleOffset, texture, texture.width, texture.height, -1);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00017010 File Offset: 0x00015210
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture oldTexture, Texture newTexture, ref Vector4 scaleOffset, Vector4 sourceScaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			if (this.IsCached(out scaleOffset, oldTexture))
			{
				if (updateIfNeeded && this.NeedsUpdate(newTexture, false))
				{
					if (this.Is2D(newTexture))
					{
						this.BlitTexture(cmd, scaleOffset, newTexture, sourceScaleOffset, blitMips, -1);
					}
					else
					{
						this.BlitCubeTexture2D(cmd, scaleOffset, newTexture, blitMips, -1);
					}
					this.MarkGPUTextureValid(this.GetTextureID(newTexture), blitMips);
				}
				return true;
			}
			return this.AllocateTexture(cmd, ref scaleOffset, newTexture, newTexture.width, newTexture.height, -1);
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001708F File Offset: 0x0001528F
		public virtual bool UpdateTexture(CommandBuffer cmd, Texture texture, ref Vector4 scaleOffset, bool updateIfNeeded = true, bool blitMips = true)
		{
			return this.UpdateTexture(cmd, texture, texture, ref scaleOffset, Texture2DAtlas.fullScaleOffset, updateIfNeeded, blitMips);
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000170A4 File Offset: 0x000152A4
		internal bool EnsureTextureSlot(out bool isUploadNeeded, ref Vector4 scaleBias, int key, int width, int height)
		{
			isUploadNeeded = false;
			ValueTuple<Vector4, Vector2Int> valueTuple;
			if (this.m_AllocationCache.TryGetValue(key, out valueTuple))
			{
				scaleBias = valueTuple.Item1;
				return true;
			}
			if (!this.m_AtlasAllocator.Allocate(ref scaleBias, width, height))
			{
				return false;
			}
			isUploadNeeded = true;
			scaleBias.Scale(new Vector4(1f / (float)this.m_Width, 1f / (float)this.m_Height, 1f / (float)this.m_Width, 1f / (float)this.m_Height));
			this.m_AllocationCache.Add(key, new ValueTuple<Vector4, Vector2Int>(scaleBias, new Vector2Int(width, height)));
			return true;
		}

		// Token: 0x0400030E RID: 782
		private protected const int kGPUTexInvalid = 0;

		// Token: 0x0400030F RID: 783
		private protected const int kGPUTexValidMip0 = 1;

		// Token: 0x04000310 RID: 784
		private protected const int kGPUTexValidMipAll = 2;

		// Token: 0x04000311 RID: 785
		private protected RTHandle m_AtlasTexture;

		// Token: 0x04000312 RID: 786
		private protected int m_Width;

		// Token: 0x04000313 RID: 787
		private protected int m_Height;

		// Token: 0x04000314 RID: 788
		private protected GraphicsFormat m_Format;

		// Token: 0x04000315 RID: 789
		private protected bool m_UseMipMaps;

		// Token: 0x04000316 RID: 790
		private bool m_IsAtlasTextureOwner;

		// Token: 0x04000317 RID: 791
		private AtlasAllocator m_AtlasAllocator;

		// Token: 0x04000318 RID: 792
		[TupleElementNames(new string[] { "scaleOffset", "size" })]
		private Dictionary<int, ValueTuple<Vector4, Vector2Int>> m_AllocationCache = new Dictionary<int, ValueTuple<Vector4, Vector2Int>>();

		// Token: 0x04000319 RID: 793
		private Dictionary<int, int> m_IsGPUTextureUpToDate = new Dictionary<int, int>();

		// Token: 0x0400031A RID: 794
		private Dictionary<int, int> m_TextureHashes = new Dictionary<int, int>();

		// Token: 0x0400031B RID: 795
		private static readonly Vector4 fullScaleOffset = new Vector4(1f, 1f, 0f, 0f);

		// Token: 0x0400031C RID: 796
		private static readonly int s_MaxMipLevelPadding = 10;

		// Token: 0x0200016E RID: 366
		private enum BlitType
		{
			// Token: 0x04000579 RID: 1401
			Default,
			// Token: 0x0400057A RID: 1402
			CubeTo2DOctahedral,
			// Token: 0x0400057B RID: 1403
			SingleChannel,
			// Token: 0x0400057C RID: 1404
			CubeTo2DOctahedralSingleChannel
		}
	}
}
