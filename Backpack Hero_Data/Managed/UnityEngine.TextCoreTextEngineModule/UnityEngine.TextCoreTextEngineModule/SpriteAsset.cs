using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Serialization;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000019 RID: 25
	[ExcludeFromPreset]
	public class SpriteAsset : TextAsset
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006FAC File Offset: 0x000051AC
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00006FC4 File Offset: 0x000051C4
		public FaceInfo faceInfo
		{
			get
			{
				return this.m_FaceInfo;
			}
			internal set
			{
				this.m_FaceInfo = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00006FD0 File Offset: 0x000051D0
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006FE8 File Offset: 0x000051E8
		public Texture spriteSheet
		{
			get
			{
				return this.m_SpriteAtlasTexture;
			}
			internal set
			{
				this.m_SpriteAtlasTexture = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00006FF4 File Offset: 0x000051F4
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00007020 File Offset: 0x00005220
		public List<SpriteCharacter> spriteCharacterTable
		{
			get
			{
				bool flag = this.m_GlyphIndexLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterTable;
			}
			internal set
			{
				this.m_SpriteCharacterTable = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000702C File Offset: 0x0000522C
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00007058 File Offset: 0x00005258
		public Dictionary<uint, SpriteCharacter> spriteCharacterLookupTable
		{
			get
			{
				bool flag = this.m_SpriteCharacterLookup == null;
				if (flag)
				{
					this.UpdateLookupTables();
				}
				return this.m_SpriteCharacterLookup;
			}
			internal set
			{
				this.m_SpriteCharacterLookup = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00007064 File Offset: 0x00005264
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x0000707C File Offset: 0x0000527C
		public List<SpriteGlyph> spriteGlyphTable
		{
			get
			{
				return this.m_SpriteGlyphTable;
			}
			internal set
			{
				this.m_SpriteGlyphTable = value;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00002EF5 File Offset: 0x000010F5
		private void Awake()
		{
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00007088 File Offset: 0x00005288
		public void UpdateLookupTables()
		{
			bool flag = this.m_GlyphIndexLookup == null;
			if (flag)
			{
				this.m_GlyphIndexLookup = new Dictionary<uint, int>();
			}
			else
			{
				this.m_GlyphIndexLookup.Clear();
			}
			bool flag2 = this.m_SpriteGlyphLookup == null;
			if (flag2)
			{
				this.m_SpriteGlyphLookup = new Dictionary<uint, SpriteGlyph>();
			}
			else
			{
				this.m_SpriteGlyphLookup.Clear();
			}
			for (int i = 0; i < this.m_SpriteGlyphTable.Count; i++)
			{
				SpriteGlyph spriteGlyph = this.m_SpriteGlyphTable[i];
				uint index = spriteGlyph.index;
				bool flag3 = !this.m_GlyphIndexLookup.ContainsKey(index);
				if (flag3)
				{
					this.m_GlyphIndexLookup.Add(index, i);
				}
				bool flag4 = !this.m_SpriteGlyphLookup.ContainsKey(index);
				if (flag4)
				{
					this.m_SpriteGlyphLookup.Add(index, spriteGlyph);
				}
			}
			bool flag5 = this.m_NameLookup == null;
			if (flag5)
			{
				this.m_NameLookup = new Dictionary<int, int>();
			}
			else
			{
				this.m_NameLookup.Clear();
			}
			bool flag6 = this.m_SpriteCharacterLookup == null;
			if (flag6)
			{
				this.m_SpriteCharacterLookup = new Dictionary<uint, SpriteCharacter>();
			}
			else
			{
				this.m_SpriteCharacterLookup.Clear();
			}
			for (int j = 0; j < this.m_SpriteCharacterTable.Count; j++)
			{
				SpriteCharacter spriteCharacter = this.m_SpriteCharacterTable[j];
				bool flag7 = spriteCharacter == null;
				if (!flag7)
				{
					uint glyphIndex = spriteCharacter.glyphIndex;
					bool flag8 = !this.m_SpriteGlyphLookup.ContainsKey(glyphIndex);
					if (!flag8)
					{
						spriteCharacter.glyph = this.m_SpriteGlyphLookup[glyphIndex];
						spriteCharacter.textAsset = this;
						int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(this.m_SpriteCharacterTable[j].name);
						bool flag9 = !this.m_NameLookup.ContainsKey(hashCodeCaseInSensitive);
						if (flag9)
						{
							this.m_NameLookup.Add(hashCodeCaseInSensitive, j);
						}
						uint unicode = this.m_SpriteCharacterTable[j].unicode;
						bool flag10 = unicode != 65534U && !this.m_SpriteCharacterLookup.ContainsKey(unicode);
						if (flag10)
						{
							this.m_SpriteCharacterLookup.Add(unicode, spriteCharacter);
						}
					}
				}
			}
			this.m_IsSpriteAssetLookupTablesDirty = false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000072C4 File Offset: 0x000054C4
		public int GetSpriteIndexFromHashcode(int hashCode)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int num;
			bool flag2 = this.m_NameLookup.TryGetValue(hashCode, ref num);
			int num2;
			if (flag2)
			{
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			return num2;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00007304 File Offset: 0x00005504
		public int GetSpriteIndexFromUnicode(uint unicode)
		{
			bool flag = this.m_SpriteCharacterLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			SpriteCharacter spriteCharacter;
			bool flag2 = this.m_SpriteCharacterLookup.TryGetValue(unicode, ref spriteCharacter);
			int num;
			if (flag2)
			{
				num = (int)spriteCharacter.glyphIndex;
			}
			else
			{
				num = -1;
			}
			return num;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00007348 File Offset: 0x00005548
		public int GetSpriteIndexFromName(string name)
		{
			bool flag = this.m_NameLookup == null;
			if (flag)
			{
				this.UpdateLookupTables();
			}
			int hashCodeCaseInSensitive = TextUtilities.GetHashCodeCaseInSensitive(name);
			return this.GetSpriteIndexFromHashcode(hashCodeCaseInSensitive);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000737C File Offset: 0x0000557C
		public static SpriteAsset SearchForSpriteByUnicode(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int instanceID = spriteAsset.GetInstanceID();
					SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
					}
					else
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000742C File Offset: 0x0000562C
		private static SpriteAsset SearchForSpriteByUnicodeInternal(List<SpriteAsset> spriteAssets, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int instanceID = spriteAsset.GetInstanceID();
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset, unicode, includeFallbacks, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000074AC File Offset: 0x000056AC
		private static SpriteAsset SearchForSpriteByUnicodeInternal(SpriteAsset spriteAsset, uint unicode, bool includeFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(unicode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, unicode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000750C File Offset: 0x0000570C
		public static SpriteAsset SearchForSpriteByHashCode(SpriteAsset spriteAsset, int hashCode, bool includeFallbacks, out int spriteIndex, TextSettings textSettings = null)
		{
			bool flag = spriteAsset == null;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteIndex = -1;
				spriteAsset2 = null;
			}
			else
			{
				spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
				bool flag2 = spriteIndex != -1;
				if (flag2)
				{
					spriteAsset2 = spriteAsset;
				}
				else
				{
					bool flag3 = SpriteAsset.k_searchedSpriteAssets == null;
					if (flag3)
					{
						SpriteAsset.k_searchedSpriteAssets = new HashSet<int>();
					}
					else
					{
						SpriteAsset.k_searchedSpriteAssets.Clear();
					}
					int instanceID = spriteAsset.instanceID;
					SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					bool flag4 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
					if (flag4)
					{
						SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
						bool flag5 = spriteIndex != -1;
						if (flag5)
						{
							return spriteAsset3;
						}
					}
					bool flag6 = textSettings == null;
					if (flag6)
					{
						spriteIndex = -1;
						spriteAsset2 = null;
					}
					else
					{
						bool flag7 = includeFallbacks && textSettings.defaultSpriteAsset != null;
						if (flag7)
						{
							SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByHashCodeInternal(textSettings.defaultSpriteAsset, hashCode, true, out spriteIndex);
							bool flag8 = spriteIndex != -1;
							if (flag8)
							{
								return spriteAsset3;
							}
						}
						SpriteAsset.k_searchedSpriteAssets.Clear();
						uint missingSpriteCharacterUnicode = textSettings.missingSpriteCharacterUnicode;
						spriteIndex = spriteAsset.GetSpriteIndexFromUnicode(missingSpriteCharacterUnicode);
						bool flag9 = spriteIndex != -1;
						if (flag9)
						{
							spriteAsset2 = spriteAsset;
						}
						else
						{
							SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
							bool flag10 = includeFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
							if (flag10)
							{
								SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByUnicodeInternal(spriteAsset.fallbackSpriteAssets, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag11 = spriteIndex != -1;
								if (flag11)
								{
									return spriteAsset3;
								}
							}
							bool flag12 = includeFallbacks && textSettings.defaultSpriteAsset != null;
							if (flag12)
							{
								SpriteAsset spriteAsset3 = SpriteAsset.SearchForSpriteByUnicodeInternal(textSettings.defaultSpriteAsset, missingSpriteCharacterUnicode, true, out spriteIndex);
								bool flag13 = spriteIndex != -1;
								if (flag13)
								{
									return spriteAsset3;
								}
							}
							spriteIndex = -1;
							spriteAsset2 = null;
						}
					}
				}
			}
			return spriteAsset2;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000076F8 File Offset: 0x000058F8
		private static SpriteAsset SearchForSpriteByHashCodeInternal(List<SpriteAsset> spriteAssets, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			for (int i = 0; i < spriteAssets.Count; i++)
			{
				SpriteAsset spriteAsset = spriteAssets[i];
				bool flag = spriteAsset == null;
				if (!flag)
				{
					int instanceID = spriteAsset.instanceID;
					bool flag2 = !SpriteAsset.k_searchedSpriteAssets.Add(instanceID);
					if (!flag2)
					{
						spriteAsset = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset, hashCode, searchFallbacks, out spriteIndex);
						bool flag3 = spriteAsset != null;
						if (flag3)
						{
							return spriteAsset;
						}
					}
				}
			}
			spriteIndex = -1;
			return null;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007778 File Offset: 0x00005978
		private static SpriteAsset SearchForSpriteByHashCodeInternal(SpriteAsset spriteAsset, int hashCode, bool searchFallbacks, out int spriteIndex)
		{
			spriteIndex = spriteAsset.GetSpriteIndexFromHashcode(hashCode);
			bool flag = spriteIndex != -1;
			SpriteAsset spriteAsset2;
			if (flag)
			{
				spriteAsset2 = spriteAsset;
			}
			else
			{
				bool flag2 = searchFallbacks && spriteAsset.fallbackSpriteAssets != null && spriteAsset.fallbackSpriteAssets.Count > 0;
				if (flag2)
				{
					spriteAsset2 = SpriteAsset.SearchForSpriteByHashCodeInternal(spriteAsset.fallbackSpriteAssets, hashCode, true, out spriteIndex);
				}
				else
				{
					spriteIndex = -1;
					spriteAsset2 = null;
				}
			}
			return spriteAsset2;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000077D8 File Offset: 0x000059D8
		public void SortGlyphTable()
		{
			bool flag = this.m_SpriteGlyphTable == null || this.m_SpriteGlyphTable.Count == 0;
			if (!flag)
			{
				this.m_SpriteGlyphTable = Enumerable.ToList<SpriteGlyph>(Enumerable.OrderBy<SpriteGlyph, uint>(this.m_SpriteGlyphTable, (SpriteGlyph item) => item.index));
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000783C File Offset: 0x00005A3C
		internal void SortCharacterTable()
		{
			bool flag = this.m_SpriteCharacterTable != null && this.m_SpriteCharacterTable.Count > 0;
			if (flag)
			{
				this.m_SpriteCharacterTable = Enumerable.ToList<SpriteCharacter>(Enumerable.OrderBy<SpriteCharacter, uint>(this.m_SpriteCharacterTable, (SpriteCharacter c) => c.unicode));
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000789C File Offset: 0x00005A9C
		internal void SortGlyphAndCharacterTables()
		{
			this.SortGlyphTable();
			this.SortCharacterTable();
		}

		// Token: 0x040000A2 RID: 162
		internal Dictionary<int, int> m_NameLookup;

		// Token: 0x040000A3 RID: 163
		internal Dictionary<uint, int> m_GlyphIndexLookup;

		// Token: 0x040000A4 RID: 164
		[SerializeField]
		internal FaceInfo m_FaceInfo;

		// Token: 0x040000A5 RID: 165
		[FormerlySerializedAs("spriteSheet")]
		[SerializeField]
		internal Texture m_SpriteAtlasTexture;

		// Token: 0x040000A6 RID: 166
		[SerializeField]
		private List<SpriteCharacter> m_SpriteCharacterTable = new List<SpriteCharacter>();

		// Token: 0x040000A7 RID: 167
		internal Dictionary<uint, SpriteCharacter> m_SpriteCharacterLookup;

		// Token: 0x040000A8 RID: 168
		[SerializeField]
		private List<SpriteGlyph> m_SpriteGlyphTable = new List<SpriteGlyph>();

		// Token: 0x040000A9 RID: 169
		internal Dictionary<uint, SpriteGlyph> m_SpriteGlyphLookup;

		// Token: 0x040000AA RID: 170
		[SerializeField]
		public List<SpriteAsset> fallbackSpriteAssets;

		// Token: 0x040000AB RID: 171
		internal bool m_IsSpriteAssetLookupTablesDirty = false;

		// Token: 0x040000AC RID: 172
		private static HashSet<int> k_searchedSpriteAssets;
	}
}
