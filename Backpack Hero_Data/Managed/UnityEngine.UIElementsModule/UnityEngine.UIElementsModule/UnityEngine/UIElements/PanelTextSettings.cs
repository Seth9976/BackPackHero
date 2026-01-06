using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B4 RID: 692
	public class PanelTextSettings : TextSettings
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0005EBEC File Offset: 0x0005CDEC
		internal static PanelTextSettings defaultPanelTextSettings
		{
			get
			{
				bool flag = PanelTextSettings.s_DefaultPanelTextSettings == null;
				if (flag)
				{
					bool flag2 = PanelTextSettings.s_DefaultPanelTextSettings == null;
					if (flag2)
					{
						PanelTextSettings.s_DefaultPanelTextSettings = ScriptableObject.CreateInstance<PanelTextSettings>();
					}
				}
				return PanelTextSettings.s_DefaultPanelTextSettings;
			}
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0005EC30 File Offset: 0x0005CE30
		internal static void UpdateLocalizationFontAsset()
		{
			string text = " - Linux";
			Dictionary<SystemLanguage, string> dictionary = new Dictionary<SystemLanguage, string>();
			dictionary.Add(SystemLanguage.English, Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/English" + text + ".asset"));
			dictionary.Add(SystemLanguage.Japanese, Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/Japanese" + text + ".asset"));
			dictionary.Add(SystemLanguage.ChineseSimplified, Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/ChineseSimplified" + text + ".asset"));
			dictionary.Add(SystemLanguage.ChineseTraditional, Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/ChineseTraditional" + text + ".asset"));
			dictionary.Add(SystemLanguage.Korean, Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/Localization/Korean" + text + ".asset"));
			Dictionary<SystemLanguage, string> dictionary2 = dictionary;
			string text2 = Path.Combine(UIElementsPackageUtility.EditorResourcesBasePath, "UIPackageResources/FontAssets/DynamicOSFontAssets/GlobalFallback/GlobalFallback" + text + ".asset");
			FontAsset fontAsset = PanelTextSettings.EditorGUIUtilityLoad.Invoke(dictionary2[PanelTextSettings.GetCurrentLanguage.Invoke()]) as FontAsset;
			FontAsset fontAsset2 = PanelTextSettings.EditorGUIUtilityLoad.Invoke(text2) as FontAsset;
			PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets[0] = fontAsset;
			PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets[PanelTextSettings.defaultPanelTextSettings.fallbackFontAssets.Count - 1] = fontAsset2;
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005ED7C File Offset: 0x0005CF7C
		internal FontAsset GetCachedFontAsset(Font font)
		{
			return base.GetCachedFontAssetInternal(font);
		}

		// Token: 0x040009F1 RID: 2545
		private static PanelTextSettings s_DefaultPanelTextSettings;

		// Token: 0x040009F2 RID: 2546
		internal static Func<string, Object> EditorGUIUtilityLoad;

		// Token: 0x040009F3 RID: 2547
		internal static Func<SystemLanguage> GetCurrentLanguage;

		// Token: 0x040009F4 RID: 2548
		internal static readonly string s_DefaultEditorPanelTextSettingPath = "UIPackageResources/Default Editor Text Settings.asset";
	}
}
