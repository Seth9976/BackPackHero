using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000261 RID: 609
	internal class TextureRegistry
	{
		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00047D31 File Offset: 0x00045F31
		public static TextureRegistry instance { get; } = new TextureRegistry();

		// Token: 0x06001250 RID: 4688 RVA: 0x00047D38 File Offset: 0x00045F38
		public Texture GetTexture(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			Texture texture;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to get an invalid texture (index={0}).", id.index));
				texture = null;
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to get a texture (index={0}) that is not allocated.", id.index));
					texture = null;
				}
				else
				{
					texture = textureInfo.texture;
				}
			}
			return texture;
		}

		// Token: 0x06001251 RID: 4689 RVA: 0x00047DDC File Offset: 0x00045FDC
		public TextureId AllocAndAcquireDynamic()
		{
			return this.AllocAndAcquire(null, true);
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00047DF8 File Offset: 0x00045FF8
		public void UpdateDynamic(TextureId id, Texture texture)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to update an invalid dynamic texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = !textureInfo.dynamic;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to update a texture (index={0}) that is not dynamic.", id.index));
				}
				else
				{
					bool flag3 = textureInfo.refCount < 1;
					if (flag3)
					{
						Debug.LogError(string.Format("Attempted to update a dynamic texture (index={0}) that is not allocated.", id.index));
					}
					else
					{
						textureInfo.texture = texture;
						this.m_Textures[id.index] = textureInfo;
					}
				}
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00047ED8 File Offset: 0x000460D8
		private TextureId AllocAndAcquire(Texture texture, bool dynamic)
		{
			TextureRegistry.TextureInfo textureInfo = new TextureRegistry.TextureInfo
			{
				texture = texture,
				dynamic = dynamic,
				refCount = 1
			};
			bool flag = this.m_FreeIds.Count > 0;
			TextureId textureId;
			if (flag)
			{
				textureId = this.m_FreeIds.Pop();
				this.m_Textures[textureId.index] = textureInfo;
			}
			else
			{
				bool flag2 = this.m_Textures.Count == 2048;
				if (flag2)
				{
					Debug.LogError(string.Format("Failed to allocate a {0} because the limit of {1} textures is reached.", "TextureId", 2048));
					return TextureId.invalid;
				}
				textureId = new TextureId(this.m_Textures.Count);
				this.m_Textures.Add(textureInfo);
			}
			bool flag3 = !dynamic;
			if (flag3)
			{
				this.m_TextureToId[texture] = textureId;
			}
			return textureId;
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00047FC0 File Offset: 0x000461C0
		public TextureId Acquire(Texture tex)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(tex, ref textureId);
			TextureId textureId2;
			if (flag)
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[textureId.index];
				Debug.Assert(textureInfo.refCount > 0);
				Debug.Assert(!textureInfo.dynamic);
				textureInfo.refCount++;
				this.m_Textures[textureId.index] = textureInfo;
				textureId2 = textureId;
			}
			else
			{
				textureId2 = this.AllocAndAcquire(tex, false);
			}
			return textureId2;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00048044 File Offset: 0x00046244
		public void Acquire(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to acquire an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to acquire a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount++;
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000480F8 File Offset: 0x000462F8
		public void Release(TextureId id)
		{
			bool flag = id.index < 0 || id.index >= this.m_Textures.Count;
			if (flag)
			{
				Debug.LogError(string.Format("Attempted to release an invalid texture (index={0}).", id.index));
			}
			else
			{
				TextureRegistry.TextureInfo textureInfo = this.m_Textures[id.index];
				bool flag2 = textureInfo.refCount < 1;
				if (flag2)
				{
					Debug.LogError(string.Format("Attempted to release a texture (index={0}) that is not allocated.", id.index));
				}
				else
				{
					textureInfo.refCount--;
					bool flag3 = textureInfo.refCount == 0;
					if (flag3)
					{
						bool flag4 = !textureInfo.dynamic;
						if (flag4)
						{
							this.m_TextureToId.Remove(textureInfo.texture);
						}
						textureInfo.texture = null;
						textureInfo.dynamic = false;
						this.m_FreeIds.Push(id);
					}
					this.m_Textures[id.index] = textureInfo;
				}
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000481FC File Offset: 0x000463FC
		public TextureId TextureToId(Texture texture)
		{
			TextureId textureId;
			bool flag = this.m_TextureToId.TryGetValue(texture, ref textureId);
			TextureId textureId2;
			if (flag)
			{
				textureId2 = textureId;
			}
			else
			{
				textureId2 = TextureId.invalid;
			}
			return textureId2;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0004822C File Offset: 0x0004642C
		public TextureRegistry.Statistics GatherStatistics()
		{
			TextureRegistry.Statistics statistics = default(TextureRegistry.Statistics);
			statistics.freeIdsCount = this.m_FreeIds.Count;
			statistics.createdIdsCount = this.m_Textures.Count;
			statistics.allocatedIdsTotalCount = this.m_Textures.Count - this.m_FreeIds.Count;
			statistics.allocatedIdsDynamicCount = statistics.allocatedIdsTotalCount - this.m_TextureToId.Count;
			statistics.allocatedIdsStaticCount = statistics.allocatedIdsTotalCount - statistics.allocatedIdsDynamicCount;
			statistics.availableIdsCount = 2048 - statistics.allocatedIdsTotalCount;
			return statistics;
		}

		// Token: 0x04000869 RID: 2153
		private List<TextureRegistry.TextureInfo> m_Textures = new List<TextureRegistry.TextureInfo>(128);

		// Token: 0x0400086A RID: 2154
		private Dictionary<Texture, TextureId> m_TextureToId = new Dictionary<Texture, TextureId>(128);

		// Token: 0x0400086B RID: 2155
		private Stack<TextureId> m_FreeIds = new Stack<TextureId>();

		// Token: 0x0400086C RID: 2156
		internal const int maxTextures = 2048;

		// Token: 0x02000262 RID: 610
		private struct TextureInfo
		{
			// Token: 0x0400086E RID: 2158
			public Texture texture;

			// Token: 0x0400086F RID: 2159
			public bool dynamic;

			// Token: 0x04000870 RID: 2160
			public int refCount;
		}

		// Token: 0x02000263 RID: 611
		public struct Statistics
		{
			// Token: 0x04000871 RID: 2161
			public int freeIdsCount;

			// Token: 0x04000872 RID: 2162
			public int createdIdsCount;

			// Token: 0x04000873 RID: 2163
			public int allocatedIdsTotalCount;

			// Token: 0x04000874 RID: 2164
			public int allocatedIdsDynamicCount;

			// Token: 0x04000875 RID: 2165
			public int allocatedIdsStaticCount;

			// Token: 0x04000876 RID: 2166
			public int availableIdsCount;
		}
	}
}
