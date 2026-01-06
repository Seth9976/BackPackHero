using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Experimental.Rendering;

namespace UnityEngine.Rendering
{
	// Token: 0x0200008F RID: 143
	public class PowerOfTwoTextureAtlas : Texture2DAtlas
	{
		// Token: 0x0600041E RID: 1054 RVA: 0x00014E7E File Offset: 0x0001307E
		public PowerOfTwoTextureAtlas(int size, int mipPadding, GraphicsFormat format, FilterMode filterMode = FilterMode.Point, string name = "", bool useMipMap = true)
			: base(size, size, format, filterMode, true, name, useMipMap)
		{
			this.m_MipPadding = mipPadding;
			int num = size & (size - 1);
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00014EA8 File Offset: 0x000130A8
		public int mipPadding
		{
			get
			{
				return this.m_MipPadding;
			}
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x00014EB0 File Offset: 0x000130B0
		private int GetTexturePadding()
		{
			return (int)Mathf.Pow(2f, (float)this.m_MipPadding) * 2;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00014EC8 File Offset: 0x000130C8
		public Vector4 GetPayloadScaleOffset(Texture texture, in Vector4 scaleOffset)
		{
			int texturePadding = this.GetTexturePadding();
			Vector2 vector = Vector2.one * (float)texturePadding;
			Vector2 powerOfTwoTextureSize = this.GetPowerOfTwoTextureSize(texture);
			return PowerOfTwoTextureAtlas.GetPayloadScaleOffset(in powerOfTwoTextureSize, in vector, in scaleOffset);
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00014EFC File Offset: 0x000130FC
		public static Vector4 GetPayloadScaleOffset(in Vector2 textureSize, in Vector2 paddingSize, in Vector4 scaleOffset)
		{
			Vector2 vector = new Vector2(scaleOffset.x, scaleOffset.y);
			Vector2 vector2 = new Vector2(scaleOffset.z, scaleOffset.w);
			Vector2 vector3 = (textureSize + paddingSize) / textureSize;
			Vector2 vector4 = paddingSize / 2f / (textureSize + paddingSize);
			Vector2 vector5 = vector / vector3;
			Vector2 vector6 = vector2 + vector * vector4;
			return new Vector4(vector5.x, vector5.y, vector6.x, vector6.y);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x00014FA8 File Offset: 0x000131A8
		private void Blit2DTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips, PowerOfTwoTextureAtlas.BlitType blitType)
		{
			int num = base.GetTextureMipmapCount(texture.width, texture.height);
			int texturePadding = this.GetTexturePadding();
			Vector2 powerOfTwoTextureSize = this.GetPowerOfTwoTextureSize(texture);
			bool flag = texture.filterMode > FilterMode.Point;
			if (!blitMips)
			{
				num = 1;
			}
			using (new ProfilingScope(cmd, ProfilingSampler.Get<CoreProfileId>(CoreProfileId.BlitTextureInPotAtlas)))
			{
				for (int i = 0; i < num; i++)
				{
					cmd.SetRenderTarget(this.m_AtlasTexture, i);
					switch (blitType)
					{
					case PowerOfTwoTextureAtlas.BlitType.Padding:
						Blitter.BlitQuadWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, flag, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.PaddingMultiply:
						Blitter.BlitQuadWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, flag, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPadding:
						Blitter.BlitOctahedralWithPadding(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, flag, texturePadding);
						break;
					case PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply:
						Blitter.BlitOctahedralWithPaddingMultiply(cmd, texture, powerOfTwoTextureSize, sourceScaleOffset, scaleOffset, i, flag, texturePadding);
						break;
					}
				}
			}
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x00015094 File Offset: 0x00013294
		public override void BlitTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.Padding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000150C4 File Offset: 0x000132C4
		public void BlitTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.PaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000150F4 File Offset: 0x000132F4
		public override void BlitOctahedralTexture(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPadding);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x00015124 File Offset: 0x00013324
		public void BlitOctahedralTextureMultiply(CommandBuffer cmd, Vector4 scaleOffset, Texture texture, Vector4 sourceScaleOffset, bool blitMips = true, int overrideInstanceID = -1)
		{
			if (base.Is2D(texture))
			{
				this.Blit2DTexture(cmd, scaleOffset, texture, sourceScaleOffset, blitMips, PowerOfTwoTextureAtlas.BlitType.OctahedralPaddingMultiply);
				base.MarkGPUTextureValid((overrideInstanceID != -1) ? overrideInstanceID : texture.GetInstanceID(), blitMips);
			}
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00015154 File Offset: 0x00013354
		private void TextureSizeToPowerOfTwo(Texture texture, ref int width, ref int height)
		{
			width = Mathf.NextPowerOfTwo(width);
			height = Mathf.NextPowerOfTwo(height);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00015168 File Offset: 0x00013368
		private Vector2 GetPowerOfTwoTextureSize(Texture texture)
		{
			int width = texture.width;
			int height = texture.height;
			this.TextureSizeToPowerOfTwo(texture, ref width, ref height);
			return new Vector2((float)width, (float)height);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00015198 File Offset: 0x00013398
		public override bool AllocateTexture(CommandBuffer cmd, ref Vector4 scaleOffset, Texture texture, int width, int height, int overrideInstanceID = -1)
		{
			if (height != width)
			{
				Debug.LogError(string.Concat(new string[]
				{
					"Can't place ",
					(texture != null) ? texture.ToString() : null,
					" in the atlas ",
					this.m_AtlasTexture.name,
					": Only squared texture are allowed in this atlas."
				}));
				return false;
			}
			this.TextureSizeToPowerOfTwo(texture, ref height, ref width);
			return base.AllocateTexture(cmd, ref scaleOffset, texture, width, height, -1);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x0001520C File Offset: 0x0001340C
		public void ResetRequestedTexture()
		{
			this.m_RequestedTextures.Clear();
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00015219 File Offset: 0x00013419
		public bool ReserveSpace(Texture texture)
		{
			return this.ReserveSpace(texture, texture.width, texture.height);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0001522E File Offset: 0x0001342E
		public bool ReserveSpace(Texture texture, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(texture), width, height);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x0001523F File Offset: 0x0001343F
		public bool ReserveSpace(Texture textureA, Texture textureB, int width, int height)
		{
			return this.ReserveSpace(base.GetTextureID(textureA, textureB), width, height);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00015254 File Offset: 0x00013454
		private bool ReserveSpace(int id, int width, int height)
		{
			this.m_RequestedTextures[id] = new Vector2Int(width, height);
			Vector2Int cachedTextureSize = base.GetCachedTextureSize(id);
			Vector4 vector;
			if (!base.IsCached(out vector, id) || cachedTextureSize.x != width || cachedTextureSize.y != height)
			{
				Vector4 zero = Vector4.zero;
				if (!this.AllocateTextureWithoutBlit(id, width, height, ref zero))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000152B4 File Offset: 0x000134B4
		public bool RelayoutEntries()
		{
			List<ValueTuple<int, Vector2Int>> list = new List<ValueTuple<int, Vector2Int>>();
			foreach (KeyValuePair<int, Vector2Int> keyValuePair in this.m_RequestedTextures)
			{
				list.Add(new ValueTuple<int, Vector2Int>(keyValuePair.Key, keyValuePair.Value));
			}
			base.ResetAllocator();
			list.Sort(([TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c1, [TupleElementNames(new string[] { "instanceId", "size" })] ValueTuple<int, Vector2Int> c2) => c2.Item2.magnitude.CompareTo(c1.Item2.magnitude));
			bool flag = true;
			Vector4 zero = Vector4.zero;
			foreach (ValueTuple<int, Vector2Int> valueTuple in list)
			{
				bool flag2 = flag;
				int item = valueTuple.Item1;
				Vector2Int vector2Int = valueTuple.Item2;
				int x = vector2Int.x;
				vector2Int = valueTuple.Item2;
				flag = flag2 & this.AllocateTextureWithoutBlit(item, x, vector2Int.y, ref zero);
			}
			return flag;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000153C0 File Offset: 0x000135C0
		public static long GetApproxCacheSizeInByte(int nbElement, int resolution, bool hasMipmap, GraphicsFormat format)
		{
			return (long)((double)(nbElement * resolution * resolution) * (double)((hasMipmap ? 1.33f : 1f) * GraphicsFormatUtility.GetBlockSize(format)));
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000153E4 File Offset: 0x000135E4
		public static int GetMaxCacheSizeForWeightInByte(int weight, bool hasMipmap, GraphicsFormat format)
		{
			float num = GraphicsFormatUtility.GetBlockSize(format) * (hasMipmap ? 1.33f : 1f);
			return CoreUtils.PreviousPowerOfTwo((int)Mathf.Sqrt((float)weight / num));
		}

		// Token: 0x040002EB RID: 747
		private readonly int m_MipPadding;

		// Token: 0x040002EC RID: 748
		private const float k_MipmapFactorApprox = 1.33f;

		// Token: 0x040002ED RID: 749
		private Dictionary<int, Vector2Int> m_RequestedTextures = new Dictionary<int, Vector2Int>();

		// Token: 0x02000169 RID: 361
		private enum BlitType
		{
			// Token: 0x04000569 RID: 1385
			Padding,
			// Token: 0x0400056A RID: 1386
			PaddingMultiply,
			// Token: 0x0400056B RID: 1387
			OctahedralPadding,
			// Token: 0x0400056C RID: 1388
			OctahedralPaddingMultiply
		}
	}
}
