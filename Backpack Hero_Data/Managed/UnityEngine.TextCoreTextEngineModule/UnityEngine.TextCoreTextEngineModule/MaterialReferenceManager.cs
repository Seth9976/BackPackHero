using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000016 RID: 22
	internal class MaterialReferenceManager
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000AA RID: 170 RVA: 0x00006594 File Offset: 0x00004794
		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000065C1 File Offset: 0x000047C1
		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000065D0 File Offset: 0x000047D0
		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006620 File Offset: 0x00004820
		public static void AddSpriteAsset(SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(spriteAsset);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006630 File Offset: 0x00004830
		private void AddSpriteAssetInternal(SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(spriteAsset.hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(spriteAsset.hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(spriteAsset.hashCode, spriteAsset.material);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00006680 File Offset: 0x00004880
		public static void AddSpriteAsset(int hashCode, SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00006690 File Offset: 0x00004890
		private void AddSpriteAssetInternal(int hashCode, SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000066D1 File Offset: 0x000048D1
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000066E1 File Offset: 0x000048E1
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000066F2 File Offset: 0x000048F2
		public static void AddColorGradientPreset(int hashCode, TextColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006704 File Offset: 0x00004904
		private void AddColorGradientPreset_Internal(int hashCode, TextColorGradient spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006734 File Offset: 0x00004934
		public bool Contains(FontAsset font)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(font.hashCode);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00006758 File Offset: 0x00004958
		public bool Contains(SpriteAsset sprite)
		{
			return this.m_FontAssetReferenceLookup.ContainsKey(sprite.hashCode);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000677C File Offset: 0x0000497C
		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x0000679C File Offset: 0x0000499C
		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, ref fontAsset);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000067C0 File Offset: 0x000049C0
		public static bool TryGetSpriteAsset(int hashCode, out SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000067E0 File Offset: 0x000049E0
		private bool TryGetSpriteAssetInternal(int hashCode, out SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, ref spriteAsset);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00006804 File Offset: 0x00004A04
		public static bool TryGetColorGradientPreset(int hashCode, out TextColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00006824 File Offset: 0x00004A24
		private bool TryGetColorGradientPresetInternal(int hashCode, out TextColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, ref gradientPreset);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00006848 File Offset: 0x00004A48
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00006868 File Offset: 0x00004A68
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, ref material);
		}

		// Token: 0x04000092 RID: 146
		private static MaterialReferenceManager s_Instance;

		// Token: 0x04000093 RID: 147
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x04000094 RID: 148
		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x04000095 RID: 149
		private Dictionary<int, SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, SpriteAsset>();

		// Token: 0x04000096 RID: 150
		private Dictionary<int, TextColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TextColorGradient>();
	}
}
