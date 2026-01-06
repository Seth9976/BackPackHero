using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000051 RID: 81
	internal class DebugDisplaySettingsMaterial : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002ED RID: 749 RVA: 0x000132BE File Offset: 0x000114BE
		// (set) Token: 0x060002EE RID: 750 RVA: 0x000132C8 File Offset: 0x000114C8
		internal DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset albedoDebugValidationPreset
		{
			get
			{
				return this.m_AlbedoDebugValidationPreset;
			}
			set
			{
				this.m_AlbedoDebugValidationPreset = value;
				DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData albedoDebugValidationPresetData = this.m_AlbedoDebugValidationPresetData[(int)value];
				this.AlbedoMinLuminance = albedoDebugValidationPresetData.minLuminance;
				this.AlbedoMaxLuminance = albedoDebugValidationPresetData.maxLuminance;
				this.AlbedoCompareColor = albedoDebugValidationPresetData.color;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0001330D File Offset: 0x0001150D
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x00013323 File Offset: 0x00011523
		internal float AlbedoHueTolerance
		{
			get
			{
				if (this.m_AlbedoDebugValidationPreset != DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return this.m_AlbedoHueTolerance;
				}
				return 1f;
			}
			private set
			{
				this.m_AlbedoHueTolerance = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0001332C File Offset: 0x0001152C
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x00013342 File Offset: 0x00011542
		internal float AlbedoSaturationTolerance
		{
			get
			{
				if (this.m_AlbedoDebugValidationPreset != DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance)
				{
					return this.m_AlbedoSaturationTolerance;
				}
				return 1f;
			}
			private set
			{
				this.m_AlbedoSaturationTolerance = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0001334B File Offset: 0x0001154B
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x00013353 File Offset: 0x00011553
		internal DebugMaterialMode DebugMaterialModeData { get; private set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0001335C File Offset: 0x0001155C
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x00013364 File Offset: 0x00011564
		internal DebugVertexAttributeMode DebugVertexAttributeIndexData { get; private set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0001336D File Offset: 0x0001156D
		public bool AreAnySettingsActive
		{
			get
			{
				return this.DebugMaterialModeData != DebugMaterialMode.None || this.DebugVertexAttributeIndexData != DebugVertexAttributeMode.None || this.MaterialValidationMode > DebugMaterialValidationMode.None;
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0001338A File Offset: 0x0001158A
		public bool IsPostProcessingAllowed
		{
			get
			{
				return !this.AreAnySettingsActive;
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00013395 File Offset: 0x00011595
		public bool IsLightingActive
		{
			get
			{
				return !this.AreAnySettingsActive;
			}
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000133A0 File Offset: 0x000115A0
		public bool TryGetScreenClearColor(ref Color color)
		{
			return false;
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000133A3 File Offset: 0x000115A3
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsMaterial.SettingsPanel(this);
		}

		// Token: 0x04000229 RID: 553
		private DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[] m_AlbedoDebugValidationPresetData = new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData[]
		{
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Default Luminance",
				color = new Color(0.49803922f, 0.49803922f, 0.49803922f),
				minLuminance = 0.01f,
				maxLuminance = 0.9f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Black Acrylic Paint",
				color = new Color(0.21960784f, 0.21960784f, 0.21960784f),
				minLuminance = 0.03f,
				maxLuminance = 0.07f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dark Soil",
				color = new Color(0.33333334f, 0.23921569f, 0.19215687f),
				minLuminance = 0.05f,
				maxLuminance = 0.14f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Worn Asphalt",
				color = new Color(0.35686275f, 0.35686275f, 0.35686275f),
				minLuminance = 0.1f,
				maxLuminance = 0.15f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Clay Soil",
				color = new Color(0.5372549f, 0.47058824f, 0.4f),
				minLuminance = 0.15f,
				maxLuminance = 0.35f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Green Grass",
				color = new Color(0.48235294f, 0.5137255f, 0.2901961f),
				minLuminance = 0.16f,
				maxLuminance = 0.26f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Old Concrete",
				color = new Color(0.5294118f, 0.53333336f, 0.5137255f),
				minLuminance = 0.17f,
				maxLuminance = 0.3f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Red Clay Tile",
				color = new Color(0.77254903f, 0.49019608f, 0.39215687f),
				minLuminance = 0.23f,
				maxLuminance = 0.33f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Dry Sand",
				color = new Color(0.69411767f, 0.654902f, 0.5176471f),
				minLuminance = 0.2f,
				maxLuminance = 0.45f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "New Concrete",
				color = new Color(0.7254902f, 0.7137255f, 0.6862745f),
				minLuminance = 0.32f,
				maxLuminance = 0.55f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "White Acrylic Paint",
				color = new Color(0.8901961f, 0.8901961f, 0.8901961f),
				minLuminance = 0.75f,
				maxLuminance = 0.85f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Fresh Snow",
				color = new Color(0.9529412f, 0.9529412f, 0.9529412f),
				minLuminance = 0.85f,
				maxLuminance = 0.95f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Blue Sky",
				color = new Color(0.3647059f, 0.48235294f, 0.6156863f),
				minLuminance = new Color(0.3647059f, 0.48235294f, 0.6156863f).linear.maxColorComponent - 0.05f,
				maxLuminance = new Color(0.3647059f, 0.48235294f, 0.6156863f).linear.maxColorComponent + 0.05f
			},
			new DebugDisplaySettingsMaterial.AlbedoDebugValidationPresetData
			{
				name = "Foliage",
				color = new Color(0.35686275f, 0.42352942f, 0.25490198f),
				minLuminance = new Color(0.35686275f, 0.42352942f, 0.25490198f).linear.maxColorComponent - 0.05f,
				maxLuminance = new Color(0.35686275f, 0.42352942f, 0.25490198f).linear.maxColorComponent + 0.05f
			}
		};

		// Token: 0x0400022A RID: 554
		private DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset m_AlbedoDebugValidationPreset;

		// Token: 0x0400022B RID: 555
		internal float AlbedoMinLuminance = 0.01f;

		// Token: 0x0400022C RID: 556
		internal float AlbedoMaxLuminance = 0.9f;

		// Token: 0x0400022D RID: 557
		private float m_AlbedoHueTolerance = 0.104f;

		// Token: 0x0400022E RID: 558
		private float m_AlbedoSaturationTolerance = 0.214f;

		// Token: 0x0400022F RID: 559
		internal Color AlbedoCompareColor = new Color(0.49803922f, 0.49803922f, 0.49803922f, 1f);

		// Token: 0x04000230 RID: 560
		internal float MetallicMinValue;

		// Token: 0x04000231 RID: 561
		internal float MetallicMaxValue = 0.9f;

		// Token: 0x04000232 RID: 562
		internal DebugMaterialValidationMode MaterialValidationMode;

		// Token: 0x02000159 RID: 345
		internal enum AlbedoDebugValidationPreset
		{
			// Token: 0x040008F2 RID: 2290
			DefaultLuminance,
			// Token: 0x040008F3 RID: 2291
			BlackAcrylicPaint,
			// Token: 0x040008F4 RID: 2292
			DarkSoil,
			// Token: 0x040008F5 RID: 2293
			WornAsphalt,
			// Token: 0x040008F6 RID: 2294
			DryClaySoil,
			// Token: 0x040008F7 RID: 2295
			GreenGrass,
			// Token: 0x040008F8 RID: 2296
			OldConcrete,
			// Token: 0x040008F9 RID: 2297
			RedClayTile,
			// Token: 0x040008FA RID: 2298
			DrySand,
			// Token: 0x040008FB RID: 2299
			NewConcrete,
			// Token: 0x040008FC RID: 2300
			WhiteAcrylicPaint,
			// Token: 0x040008FD RID: 2301
			FreshSnow,
			// Token: 0x040008FE RID: 2302
			BlueSky,
			// Token: 0x040008FF RID: 2303
			Foliage
		}

		// Token: 0x0200015A RID: 346
		private struct AlbedoDebugValidationPresetData
		{
			// Token: 0x04000900 RID: 2304
			public string name;

			// Token: 0x04000901 RID: 2305
			public Color color;

			// Token: 0x04000902 RID: 2306
			public float minLuminance;

			// Token: 0x04000903 RID: 2307
			public float maxLuminance;
		}

		// Token: 0x0200015B RID: 347
		private static class Strings
		{
			// Token: 0x04000904 RID: 2308
			public const string AlbedoSettingsContainerName = "Albedo Settings";

			// Token: 0x04000905 RID: 2309
			public const string MetallicSettingsContainerName = "Metallic Settings";

			// Token: 0x04000906 RID: 2310
			public static readonly DebugUI.Widget.NameAndTooltip MaterialOverride = new DebugUI.Widget.NameAndTooltip
			{
				name = "Material Override",
				tooltip = "Use the drop-down to select a Material property to visualize on every GameObject on screen."
			};

			// Token: 0x04000907 RID: 2311
			public static readonly DebugUI.Widget.NameAndTooltip VertexAttribute = new DebugUI.Widget.NameAndTooltip
			{
				name = "Vertex Attribute",
				tooltip = "Use the drop-down to select a 3D GameObject attribute, like Texture Coordinates or Vertex Color, to visualize on screen."
			};

			// Token: 0x04000908 RID: 2312
			public static readonly DebugUI.Widget.NameAndTooltip MaterialValidationMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Material Validation Mode",
				tooltip = "Debug and validate material properties."
			};

			// Token: 0x04000909 RID: 2313
			public static readonly DebugUI.Widget.NameAndTooltip ValidationPreset = new DebugUI.Widget.NameAndTooltip
			{
				name = "Validation Preset",
				tooltip = "Validate using a list of preset surfaces and inputs based on real-world surfaces."
			};

			// Token: 0x0400090A RID: 2314
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoMinLuminance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Min Luminance",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			// Token: 0x0400090B RID: 2315
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoMaxLuminance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Max Luminance",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};

			// Token: 0x0400090C RID: 2316
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoHueTolerance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Hue Tolerance",
				tooltip = "Validate a material based on a specific hue."
			};

			// Token: 0x0400090D RID: 2317
			public static readonly DebugUI.Widget.NameAndTooltip AlbedoSaturationTolerance = new DebugUI.Widget.NameAndTooltip
			{
				name = "Saturation Tolerance",
				tooltip = "Validate a material based on a specific Saturation."
			};

			// Token: 0x0400090E RID: 2318
			public static readonly DebugUI.Widget.NameAndTooltip MetallicMinValue = new DebugUI.Widget.NameAndTooltip
			{
				name = "Min Value",
				tooltip = "Any values set below this field are invalid and appear red on screen."
			};

			// Token: 0x0400090F RID: 2319
			public static readonly DebugUI.Widget.NameAndTooltip MetallicMaxValue = new DebugUI.Widget.NameAndTooltip
			{
				name = "Max Value",
				tooltip = "Any values set above this field are invalid and appear blue on screen."
			};
		}

		// Token: 0x0200015C RID: 348
		internal static class WidgetFactory
		{
			// Token: 0x06000967 RID: 2407 RVA: 0x0003EB64 File Offset: 0x0003CD64
			internal static DebugUI.Widget CreateMaterialOverride(DebugDisplaySettingsMaterial data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MaterialOverride;
				enumField.autoEnum = typeof(DebugMaterialMode);
				enumField.getter = () => (int)data.DebugMaterialModeData;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.DebugMaterialModeData;
				enumField.setIndex = delegate(int value)
				{
					data.DebugMaterialModeData = (DebugMaterialMode)value;
				};
				return enumField;
			}

			// Token: 0x06000968 RID: 2408 RVA: 0x0003EBFC File Offset: 0x0003CDFC
			internal static DebugUI.Widget CreateVertexAttribute(DebugDisplaySettingsMaterial data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.VertexAttribute;
				enumField.autoEnum = typeof(DebugVertexAttributeMode);
				enumField.getter = () => (int)data.DebugVertexAttributeIndexData;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.DebugVertexAttributeIndexData;
				enumField.setIndex = delegate(int value)
				{
					data.DebugVertexAttributeIndexData = (DebugVertexAttributeMode)value;
				};
				return enumField;
			}

			// Token: 0x06000969 RID: 2409 RVA: 0x0003EC94 File Offset: 0x0003CE94
			internal static DebugUI.Widget CreateMaterialValidationMode(DebugDisplaySettingsMaterial data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MaterialValidationMode;
				enumField.autoEnum = typeof(DebugMaterialValidationMode);
				enumField.getter = () => (int)data.MaterialValidationMode;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.MaterialValidationMode;
				enumField.setIndex = delegate(int value)
				{
					data.MaterialValidationMode = (DebugMaterialValidationMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x0600096A RID: 2410 RVA: 0x0003ED50 File Offset: 0x0003CF50
			internal static DebugUI.Widget CreateAlbedoPreset(DebugDisplaySettingsMaterial data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsMaterial.Strings.ValidationPreset;
				enumField.autoEnum = typeof(DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset);
				enumField.getter = () => (int)data.albedoDebugValidationPreset;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.albedoDebugValidationPreset;
				enumField.setIndex = delegate(int value)
				{
					data.albedoDebugValidationPreset = (DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x0600096B RID: 2411 RVA: 0x0003EE0C File Offset: 0x0003D00C
			internal static DebugUI.Widget CreateAlbedoMinLuminance(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoMinLuminance,
					getter = () => data.AlbedoMinLuminance,
					setter = delegate(float value)
					{
						data.AlbedoMinLuminance = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x0600096C RID: 2412 RVA: 0x0003EE68 File Offset: 0x0003D068
			internal static DebugUI.Widget CreateAlbedoMaxLuminance(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoMaxLuminance,
					getter = () => data.AlbedoMaxLuminance,
					setter = delegate(float value)
					{
						data.AlbedoMaxLuminance = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x0600096D RID: 2413 RVA: 0x0003EEC4 File Offset: 0x0003D0C4
			internal static DebugUI.Widget CreateAlbedoHueTolerance(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoHueTolerance,
					getter = () => data.AlbedoHueTolerance,
					setter = delegate(float value)
					{
						data.AlbedoHueTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => data.albedoDebugValidationPreset == DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			// Token: 0x0600096E RID: 2414 RVA: 0x0003EF30 File Offset: 0x0003D130
			internal static DebugUI.Widget CreateAlbedoSaturationTolerance(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.AlbedoSaturationTolerance,
					getter = () => data.AlbedoSaturationTolerance,
					setter = delegate(float value)
					{
						data.AlbedoSaturationTolerance = value;
					},
					incStep = 0.01f,
					isHiddenCallback = () => data.albedoDebugValidationPreset == DebugDisplaySettingsMaterial.AlbedoDebugValidationPreset.DefaultLuminance
				};
			}

			// Token: 0x0600096F RID: 2415 RVA: 0x0003EF9C File Offset: 0x0003D19C
			internal static DebugUI.Widget CreateMetallicMinValue(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MetallicMinValue,
					getter = () => data.MetallicMinValue,
					setter = delegate(float value)
					{
						data.MetallicMinValue = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x06000970 RID: 2416 RVA: 0x0003EFF8 File Offset: 0x0003D1F8
			internal static DebugUI.Widget CreateMetallicMaxValue(DebugDisplaySettingsMaterial data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsMaterial.Strings.MetallicMaxValue,
					getter = () => data.MetallicMaxValue,
					setter = delegate(float value)
					{
						data.MetallicMaxValue = value;
					},
					incStep = 0.01f
				};
			}
		}

		// Token: 0x0200015D RID: 349
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x17000221 RID: 545
			// (get) Token: 0x06000971 RID: 2417 RVA: 0x0003F051 File Offset: 0x0003D251
			public override string PanelName
			{
				get
				{
					return "Material";
				}
			}

			// Token: 0x06000972 RID: 2418 RVA: 0x0003F058 File Offset: 0x0003D258
			public SettingsPanel(DebugDisplaySettingsMaterial data)
			{
				base.AddWidget(DebugDisplaySettingsCommon.WidgetFactory.CreateMissingDebugShadersWarning());
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Material Filters",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialOverride(data),
						DebugDisplaySettingsMaterial.WidgetFactory.CreateVertexAttribute(data)
					}
				});
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Material Validation",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialValidationMode(data),
						new DebugUI.Container
						{
							displayName = "Albedo Settings",
							isHiddenCallback = () => data.MaterialValidationMode != DebugMaterialValidationMode.Albedo,
							children = 
							{
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoPreset(data),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMinLuminance(data),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoMaxLuminance(data),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoHueTolerance(data),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateAlbedoSaturationTolerance(data)
							}
						},
						new DebugUI.Container
						{
							displayName = "Metallic Settings",
							isHiddenCallback = () => data.MaterialValidationMode != DebugMaterialValidationMode.Metallic,
							children = 
							{
								DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMinValue(data),
								DebugDisplaySettingsMaterial.WidgetFactory.CreateMetallicMaxValue(data)
							}
						}
					}
				});
			}
		}
	}
}
