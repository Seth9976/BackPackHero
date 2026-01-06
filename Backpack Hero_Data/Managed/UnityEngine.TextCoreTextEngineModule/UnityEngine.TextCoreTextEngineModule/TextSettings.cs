using System;
using System.Collections.Generic;
using UnityEngine.Serialization;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200003E RID: 62
	[ExcludeFromObjectFactory]
	[ExcludeFromPreset]
	[Serializable]
	public class TextSettings : ScriptableObject
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0001B185 File Offset: 0x00019385
		// (set) Token: 0x0600017F RID: 383 RVA: 0x0001B18D File Offset: 0x0001938D
		public string version
		{
			get
			{
				return this.m_Version;
			}
			internal set
			{
				this.m_Version = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0001B196 File Offset: 0x00019396
		// (set) Token: 0x06000181 RID: 385 RVA: 0x0001B19E File Offset: 0x0001939E
		public FontAsset defaultFontAsset
		{
			get
			{
				return this.m_DefaultFontAsset;
			}
			set
			{
				this.m_DefaultFontAsset = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0001B1A7 File Offset: 0x000193A7
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0001B1AF File Offset: 0x000193AF
		public string defaultFontAssetPath
		{
			get
			{
				return this.m_DefaultFontAssetPath;
			}
			set
			{
				this.m_DefaultFontAssetPath = value;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000184 RID: 388 RVA: 0x0001B1B8 File Offset: 0x000193B8
		// (set) Token: 0x06000185 RID: 389 RVA: 0x0001B1C0 File Offset: 0x000193C0
		public List<FontAsset> fallbackFontAssets
		{
			get
			{
				return this.m_FallbackFontAssets;
			}
			set
			{
				this.m_FallbackFontAssets = value;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0001B1C9 File Offset: 0x000193C9
		// (set) Token: 0x06000187 RID: 391 RVA: 0x0001B1D1 File Offset: 0x000193D1
		public bool matchMaterialPreset
		{
			get
			{
				return this.m_MatchMaterialPreset;
			}
			set
			{
				this.m_MatchMaterialPreset = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000188 RID: 392 RVA: 0x0001B1DA File Offset: 0x000193DA
		// (set) Token: 0x06000189 RID: 393 RVA: 0x0001B1E2 File Offset: 0x000193E2
		public int missingCharacterUnicode
		{
			get
			{
				return this.m_MissingCharacterUnicode;
			}
			set
			{
				this.m_MissingCharacterUnicode = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0001B1EB File Offset: 0x000193EB
		// (set) Token: 0x0600018B RID: 395 RVA: 0x0001B1F3 File Offset: 0x000193F3
		public bool clearDynamicDataOnBuild
		{
			get
			{
				return this.m_ClearDynamicDataOnBuild;
			}
			set
			{
				this.m_ClearDynamicDataOnBuild = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0001B1FC File Offset: 0x000193FC
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0001B204 File Offset: 0x00019404
		public SpriteAsset defaultSpriteAsset
		{
			get
			{
				return this.m_DefaultSpriteAsset;
			}
			set
			{
				this.m_DefaultSpriteAsset = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600018E RID: 398 RVA: 0x0001B20D File Offset: 0x0001940D
		// (set) Token: 0x0600018F RID: 399 RVA: 0x0001B215 File Offset: 0x00019415
		public string defaultSpriteAssetPath
		{
			get
			{
				return this.m_DefaultSpriteAssetPath;
			}
			set
			{
				this.m_DefaultSpriteAssetPath = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0001B21E File Offset: 0x0001941E
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0001B226 File Offset: 0x00019426
		public List<SpriteAsset> fallbackSpriteAssets
		{
			get
			{
				return this.m_FallbackSpriteAssets;
			}
			set
			{
				this.m_FallbackSpriteAssets = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0001B22F File Offset: 0x0001942F
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0001B237 File Offset: 0x00019437
		public uint missingSpriteCharacterUnicode
		{
			get
			{
				return this.m_MissingSpriteCharacterUnicode;
			}
			set
			{
				this.m_MissingSpriteCharacterUnicode = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0001B240 File Offset: 0x00019440
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0001B248 File Offset: 0x00019448
		public TextStyleSheet defaultStyleSheet
		{
			get
			{
				return this.m_DefaultStyleSheet;
			}
			set
			{
				this.m_DefaultStyleSheet = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0001B251 File Offset: 0x00019451
		// (set) Token: 0x06000197 RID: 407 RVA: 0x0001B259 File Offset: 0x00019459
		public string styleSheetsResourcePath
		{
			get
			{
				return this.m_StyleSheetsResourcePath;
			}
			set
			{
				this.m_StyleSheetsResourcePath = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000198 RID: 408 RVA: 0x0001B262 File Offset: 0x00019462
		// (set) Token: 0x06000199 RID: 409 RVA: 0x0001B26A File Offset: 0x0001946A
		public string defaultColorGradientPresetsPath
		{
			get
			{
				return this.m_DefaultColorGradientPresetsPath;
			}
			set
			{
				this.m_DefaultColorGradientPresetsPath = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0001B274 File Offset: 0x00019474
		// (set) Token: 0x0600019B RID: 411 RVA: 0x0001B2AC File Offset: 0x000194AC
		public UnicodeLineBreakingRules lineBreakingRules
		{
			get
			{
				bool flag = this.m_UnicodeLineBreakingRules == null;
				if (flag)
				{
					this.m_UnicodeLineBreakingRules = new UnicodeLineBreakingRules();
					UnicodeLineBreakingRules.LoadLineBreakingRules();
				}
				return this.m_UnicodeLineBreakingRules;
			}
			set
			{
				this.m_UnicodeLineBreakingRules = value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0001B2B5 File Offset: 0x000194B5
		// (set) Token: 0x0600019D RID: 413 RVA: 0x0001B2BD File Offset: 0x000194BD
		public bool displayWarnings
		{
			get
			{
				return this.m_DisplayWarnings;
			}
			set
			{
				this.m_DisplayWarnings = value;
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0001B2C8 File Offset: 0x000194C8
		protected void InitializeFontReferenceLookup()
		{
			for (int i = 0; i < this.m_FontReferences.Count; i++)
			{
				TextSettings.FontReferenceMap fontReferenceMap = this.m_FontReferences[i];
				bool flag = fontReferenceMap.font == null || fontReferenceMap.fontAsset == null;
				if (flag)
				{
					Debug.Log("Deleting invalid font reference.");
					this.m_FontReferences.RemoveAt(i);
					i--;
				}
				else
				{
					int instanceID = fontReferenceMap.font.GetInstanceID();
					bool flag2 = !this.m_FontLookup.ContainsKey(instanceID);
					if (flag2)
					{
						this.m_FontLookup.Add(instanceID, fontReferenceMap.fontAsset);
					}
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0001B37C File Offset: 0x0001957C
		protected FontAsset GetCachedFontAssetInternal(Font font)
		{
			bool flag = this.m_FontLookup == null;
			if (flag)
			{
				this.m_FontLookup = new Dictionary<int, FontAsset>();
				this.InitializeFontReferenceLookup();
			}
			int instanceID = font.GetInstanceID();
			bool flag2 = this.m_FontLookup.ContainsKey(instanceID);
			FontAsset fontAsset;
			if (flag2)
			{
				fontAsset = this.m_FontLookup[instanceID];
			}
			else
			{
				bool flag3 = font.name == "System Normal";
				FontAsset fontAsset2;
				if (flag3)
				{
					fontAsset2 = FontAsset.CreateFontAsset("Lucida Grande", "Regular", 90);
				}
				else
				{
					fontAsset2 = FontAsset.CreateFontAsset(font, 90, 9, GlyphRenderMode.SDFAA, 1024, 1024, AtlasPopulationMode.Dynamic, true);
				}
				bool flag4 = fontAsset2 != null;
				if (flag4)
				{
					fontAsset2.hideFlags = HideFlags.DontSave;
					fontAsset2.atlasTextures[0].hideFlags = HideFlags.DontSave;
					fontAsset2.material.hideFlags = HideFlags.DontSave;
					fontAsset2.isMultiAtlasTexturesEnabled = true;
					this.m_FontReferences.Add(new TextSettings.FontReferenceMap(font, fontAsset2));
					this.m_FontLookup.Add(instanceID, fontAsset2);
				}
				fontAsset = fontAsset2;
			}
			return fontAsset;
		}

		// Token: 0x04000329 RID: 809
		[SerializeField]
		protected string m_Version;

		// Token: 0x0400032A RID: 810
		[FormerlySerializedAs("m_defaultFontAsset")]
		[SerializeField]
		protected FontAsset m_DefaultFontAsset;

		// Token: 0x0400032B RID: 811
		[FormerlySerializedAs("m_defaultFontAssetPath")]
		[SerializeField]
		protected string m_DefaultFontAssetPath = "Fonts & Materials/";

		// Token: 0x0400032C RID: 812
		[FormerlySerializedAs("m_fallbackFontAssets")]
		[SerializeField]
		protected List<FontAsset> m_FallbackFontAssets;

		// Token: 0x0400032D RID: 813
		[FormerlySerializedAs("m_matchMaterialPreset")]
		[SerializeField]
		protected bool m_MatchMaterialPreset;

		// Token: 0x0400032E RID: 814
		[FormerlySerializedAs("m_missingGlyphCharacter")]
		[SerializeField]
		protected int m_MissingCharacterUnicode;

		// Token: 0x0400032F RID: 815
		[SerializeField]
		protected bool m_ClearDynamicDataOnBuild = true;

		// Token: 0x04000330 RID: 816
		[FormerlySerializedAs("m_defaultSpriteAsset")]
		[SerializeField]
		protected SpriteAsset m_DefaultSpriteAsset;

		// Token: 0x04000331 RID: 817
		[FormerlySerializedAs("m_defaultSpriteAssetPath")]
		[SerializeField]
		protected string m_DefaultSpriteAssetPath = "Sprite Assets/";

		// Token: 0x04000332 RID: 818
		[SerializeField]
		protected List<SpriteAsset> m_FallbackSpriteAssets;

		// Token: 0x04000333 RID: 819
		[SerializeField]
		protected uint m_MissingSpriteCharacterUnicode;

		// Token: 0x04000334 RID: 820
		[SerializeField]
		[FormerlySerializedAs("m_defaultStyleSheet")]
		protected TextStyleSheet m_DefaultStyleSheet;

		// Token: 0x04000335 RID: 821
		[SerializeField]
		protected string m_StyleSheetsResourcePath = "Text Style Sheets/";

		// Token: 0x04000336 RID: 822
		[FormerlySerializedAs("m_defaultColorGradientPresetsPath")]
		[SerializeField]
		protected string m_DefaultColorGradientPresetsPath = "Text Color Gradients/";

		// Token: 0x04000337 RID: 823
		[SerializeField]
		protected UnicodeLineBreakingRules m_UnicodeLineBreakingRules;

		// Token: 0x04000338 RID: 824
		[FormerlySerializedAs("m_warningsDisabled")]
		[SerializeField]
		protected bool m_DisplayWarnings = false;

		// Token: 0x04000339 RID: 825
		internal Dictionary<int, FontAsset> m_FontLookup;

		// Token: 0x0400033A RID: 826
		private List<TextSettings.FontReferenceMap> m_FontReferences = new List<TextSettings.FontReferenceMap>();

		// Token: 0x0200003F RID: 63
		[Serializable]
		private struct FontReferenceMap
		{
			// Token: 0x060001A1 RID: 417 RVA: 0x0001B4DD File Offset: 0x000196DD
			public FontReferenceMap(Font font, FontAsset fontAsset)
			{
				this.font = font;
				this.fontAsset = fontAsset;
			}

			// Token: 0x0400033B RID: 827
			public Font font;

			// Token: 0x0400033C RID: 828
			public FontAsset fontAsset;
		}
	}
}
