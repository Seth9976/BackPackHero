using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000050 RID: 80
	internal class DebugDisplaySettingsLighting : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x00013258 File Offset: 0x00011458
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x00013260 File Offset: 0x00011460
		internal DebugLightingMode DebugLightingMode { get; private set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x00013269 File Offset: 0x00011469
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x00013271 File Offset: 0x00011471
		internal DebugLightingFeatureFlags DebugLightingFeatureFlagsMask { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0001327A File Offset: 0x0001147A
		public bool AreAnySettingsActive
		{
			get
			{
				return this.DebugLightingMode != DebugLightingMode.None || this.DebugLightingFeatureFlagsMask > DebugLightingFeatureFlags.None;
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0001328F File Offset: 0x0001148F
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.DebugLightingMode != DebugLightingMode.Reflections && this.DebugLightingMode != DebugLightingMode.ReflectionsWithSmoothness;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000132A8 File Offset: 0x000114A8
		public bool IsLightingActive
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000132AB File Offset: 0x000114AB
		public bool TryGetScreenClearColor(ref Color color)
		{
			return false;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000132AE File Offset: 0x000114AE
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsLighting.SettingsPanel(this);
		}

		// Token: 0x02000156 RID: 342
		private static class Strings
		{
			// Token: 0x040008EF RID: 2287
			public static readonly DebugUI.Widget.NameAndTooltip LightingDebugMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Debug Mode",
				tooltip = "Use the drop-down to select which lighting and shadow debug information to overlay on the screen."
			};

			// Token: 0x040008F0 RID: 2288
			public static readonly DebugUI.Widget.NameAndTooltip LightingFeatures = new DebugUI.Widget.NameAndTooltip
			{
				name = "Lighting Features",
				tooltip = "Filter and debug selected lighting features in the system."
			};
		}

		// Token: 0x02000157 RID: 343
		internal static class WidgetFactory
		{
			// Token: 0x06000962 RID: 2402 RVA: 0x0003E874 File Offset: 0x0003CA74
			internal static DebugUI.Widget CreateLightingDebugMode(DebugDisplaySettingsLighting data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsLighting.Strings.LightingDebugMode;
				enumField.autoEnum = typeof(DebugLightingMode);
				enumField.getter = () => (int)data.DebugLightingMode;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.DebugLightingMode;
				enumField.setIndex = delegate(int value)
				{
					data.DebugLightingMode = (DebugLightingMode)value;
				};
				return enumField;
			}

			// Token: 0x06000963 RID: 2403 RVA: 0x0003E90C File Offset: 0x0003CB0C
			internal static DebugUI.Widget CreateLightingFeatures(DebugDisplaySettingsLighting data)
			{
				return new DebugUI.BitField
				{
					nameAndTooltip = DebugDisplaySettingsLighting.Strings.LightingFeatures,
					getter = () => data.DebugLightingFeatureFlagsMask,
					setter = delegate(Enum value)
					{
						data.DebugLightingFeatureFlagsMask = (DebugLightingFeatureFlags)value;
					},
					enumType = typeof(DebugLightingFeatureFlags)
				};
			}
		}

		// Token: 0x02000158 RID: 344
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x17000220 RID: 544
			// (get) Token: 0x06000964 RID: 2404 RVA: 0x0003E96A File Offset: 0x0003CB6A
			public override string PanelName
			{
				get
				{
					return "Lighting";
				}
			}

			// Token: 0x06000965 RID: 2405 RVA: 0x0003E974 File Offset: 0x0003CB74
			public SettingsPanel(DebugDisplaySettingsLighting data)
			{
				base.AddWidget(DebugDisplaySettingsCommon.WidgetFactory.CreateMissingDebugShadersWarning());
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Lighting Debug Modes",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsLighting.WidgetFactory.CreateLightingDebugMode(data),
						DebugDisplaySettingsLighting.WidgetFactory.CreateLightingFeatures(data)
					}
				});
			}
		}
	}
}
