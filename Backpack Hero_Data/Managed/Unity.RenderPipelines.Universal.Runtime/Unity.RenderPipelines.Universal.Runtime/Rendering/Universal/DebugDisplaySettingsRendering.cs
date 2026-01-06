using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000053 RID: 83
	internal class DebugDisplaySettingsRendering : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00013948 File Offset: 0x00011B48
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00013950 File Offset: 0x00011B50
		private DebugDisplaySettingsRendering.WireframeMode wireframeMode
		{
			get
			{
				return this.m_WireframeMode;
			}
			set
			{
				this.m_WireframeMode = value;
				this.UpdateDebugSceneOverrideMode();
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001395F File Offset: 0x00011B5F
		// (set) Token: 0x06000305 RID: 773 RVA: 0x00013967 File Offset: 0x00011B67
		private bool overdraw
		{
			get
			{
				return this.m_Overdraw;
			}
			set
			{
				this.m_Overdraw = value;
				this.UpdateDebugSceneOverrideMode();
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00013978 File Offset: 0x00011B78
		private void UpdateDebugSceneOverrideMode()
		{
			switch (this.wireframeMode)
			{
			case DebugDisplaySettingsRendering.WireframeMode.Wireframe:
				this.debugSceneOverrideMode = DebugSceneOverrideMode.Wireframe;
				return;
			case DebugDisplaySettingsRendering.WireframeMode.SolidWireframe:
				this.debugSceneOverrideMode = DebugSceneOverrideMode.SolidWireframe;
				return;
			case DebugDisplaySettingsRendering.WireframeMode.ShadedWireframe:
				this.debugSceneOverrideMode = DebugSceneOverrideMode.ShadedWireframe;
				return;
			default:
				this.debugSceneOverrideMode = (this.overdraw ? DebugSceneOverrideMode.Overdraw : DebugSceneOverrideMode.None);
				return;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000307 RID: 775 RVA: 0x000139CC File Offset: 0x00011BCC
		// (set) Token: 0x06000308 RID: 776 RVA: 0x000139D4 File Offset: 0x00011BD4
		internal DebugFullScreenMode debugFullScreenMode { get; private set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000309 RID: 777 RVA: 0x000139DD File Offset: 0x00011BDD
		// (set) Token: 0x0600030A RID: 778 RVA: 0x000139E5 File Offset: 0x00011BE5
		internal int debugFullScreenModeOutputSizeScreenPercent { get; private set; } = 50;

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600030B RID: 779 RVA: 0x000139EE File Offset: 0x00011BEE
		// (set) Token: 0x0600030C RID: 780 RVA: 0x000139F6 File Offset: 0x00011BF6
		internal DebugSceneOverrideMode debugSceneOverrideMode { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600030D RID: 781 RVA: 0x000139FF File Offset: 0x00011BFF
		// (set) Token: 0x0600030E RID: 782 RVA: 0x00013A07 File Offset: 0x00011C07
		internal DebugMipInfoMode debugMipInfoMode { get; private set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600030F RID: 783 RVA: 0x00013A10 File Offset: 0x00011C10
		// (set) Token: 0x06000310 RID: 784 RVA: 0x00013A18 File Offset: 0x00011C18
		internal DebugPostProcessingMode debugPostProcessingMode { get; private set; } = DebugPostProcessingMode.Auto;

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000311 RID: 785 RVA: 0x00013A21 File Offset: 0x00011C21
		// (set) Token: 0x06000312 RID: 786 RVA: 0x00013A29 File Offset: 0x00011C29
		internal bool enableMsaa { get; private set; } = true;

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000313 RID: 787 RVA: 0x00013A32 File Offset: 0x00011C32
		// (set) Token: 0x06000314 RID: 788 RVA: 0x00013A3A File Offset: 0x00011C3A
		internal bool enableHDR { get; private set; } = true;

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00013A43 File Offset: 0x00011C43
		// (set) Token: 0x06000316 RID: 790 RVA: 0x00013A4B File Offset: 0x00011C4B
		internal DebugValidationMode validationMode { get; private set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00013A54 File Offset: 0x00011C54
		// (set) Token: 0x06000318 RID: 792 RVA: 0x00013A5C File Offset: 0x00011C5C
		internal PixelValidationChannels validationChannels { get; private set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00013A65 File Offset: 0x00011C65
		// (set) Token: 0x0600031A RID: 794 RVA: 0x00013A6D File Offset: 0x00011C6D
		internal float ValidationRangeMin { get; private set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00013A76 File Offset: 0x00011C76
		// (set) Token: 0x0600031C RID: 796 RVA: 0x00013A7E File Offset: 0x00011C7E
		internal float ValidationRangeMax { get; private set; } = 1f;

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00013A87 File Offset: 0x00011C87
		public bool AreAnySettingsActive
		{
			get
			{
				return this.debugPostProcessingMode != DebugPostProcessingMode.Auto || this.debugFullScreenMode != DebugFullScreenMode.None || this.debugSceneOverrideMode != DebugSceneOverrideMode.None || this.debugMipInfoMode != DebugMipInfoMode.None || this.validationMode != DebugValidationMode.None || !this.enableMsaa || !this.enableHDR;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00013AC5 File Offset: 0x00011CC5
		public bool IsPostProcessingAllowed
		{
			get
			{
				return this.debugPostProcessingMode != DebugPostProcessingMode.Disabled && this.debugSceneOverrideMode == DebugSceneOverrideMode.None && this.debugMipInfoMode == DebugMipInfoMode.None;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00013AE2 File Offset: 0x00011CE2
		public bool IsLightingActive
		{
			get
			{
				return this.debugSceneOverrideMode == DebugSceneOverrideMode.None && this.debugMipInfoMode == DebugMipInfoMode.None;
			}
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public bool TryGetScreenClearColor(ref Color color)
		{
			switch (this.debugSceneOverrideMode)
			{
			case DebugSceneOverrideMode.None:
			case DebugSceneOverrideMode.ShadedWireframe:
				return false;
			case DebugSceneOverrideMode.Overdraw:
				color = Color.black;
				return true;
			case DebugSceneOverrideMode.Wireframe:
			case DebugSceneOverrideMode.SolidWireframe:
				color = new Color(0.1f, 0.1f, 0.1f, 1f);
				return true;
			default:
				throw new ArgumentOutOfRangeException("color");
			}
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00013B62 File Offset: 0x00011D62
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsRendering.SettingsPanel(this);
		}

		// Token: 0x04000236 RID: 566
		private DebugDisplaySettingsRendering.WireframeMode m_WireframeMode;

		// Token: 0x04000237 RID: 567
		private bool m_Overdraw;

		// Token: 0x0200015E RID: 350
		private enum WireframeMode
		{
			// Token: 0x04000911 RID: 2321
			None,
			// Token: 0x04000912 RID: 2322
			Wireframe,
			// Token: 0x04000913 RID: 2323
			SolidWireframe,
			// Token: 0x04000914 RID: 2324
			ShadedWireframe
		}

		// Token: 0x0200015F RID: 351
		private static class Strings
		{
			// Token: 0x04000915 RID: 2325
			public const string RangeValidationSettingsContainerName = "Pixel Range Settings";

			// Token: 0x04000916 RID: 2326
			public static readonly DebugUI.Widget.NameAndTooltip MapOverlays = new DebugUI.Widget.NameAndTooltip
			{
				name = "Map Overlays",
				tooltip = "Overlays render pipeline textures to validate the scene."
			};

			// Token: 0x04000917 RID: 2327
			public static readonly DebugUI.Widget.NameAndTooltip MapSize = new DebugUI.Widget.NameAndTooltip
			{
				name = "Map Size",
				tooltip = "Set the size of the render pipeline texture in the scene."
			};

			// Token: 0x04000918 RID: 2328
			public static readonly DebugUI.Widget.NameAndTooltip AdditionalWireframeModes = new DebugUI.Widget.NameAndTooltip
			{
				name = "Additional Wireframe Modes",
				tooltip = "Debug the scene with additional wireframe shader views that are different from those in the scene view."
			};

			// Token: 0x04000919 RID: 2329
			public static readonly DebugUI.Widget.NameAndTooltip WireframeNotSupportedWarning = new DebugUI.Widget.NameAndTooltip
			{
				name = "Warning: This platform might not support wireframe rendering.",
				tooltip = "Some platforms, for example, mobile platforms using OpenGL ES and Vulkan, might not support wireframe rendering."
			};

			// Token: 0x0400091A RID: 2330
			public static readonly DebugUI.Widget.NameAndTooltip Overdraw = new DebugUI.Widget.NameAndTooltip
			{
				name = "Overdraw",
				tooltip = "Debug anywhere pixels are overdrawn on top of each other."
			};

			// Token: 0x0400091B RID: 2331
			public static readonly DebugUI.Widget.NameAndTooltip PostProcessing = new DebugUI.Widget.NameAndTooltip
			{
				name = "Post-processing",
				tooltip = "Override the controls for Post Processing in the scene."
			};

			// Token: 0x0400091C RID: 2332
			public static readonly DebugUI.Widget.NameAndTooltip MSAA = new DebugUI.Widget.NameAndTooltip
			{
				name = "MSAA",
				tooltip = "Use the checkbox to disable MSAA in the scene."
			};

			// Token: 0x0400091D RID: 2333
			public static readonly DebugUI.Widget.NameAndTooltip HDR = new DebugUI.Widget.NameAndTooltip
			{
				name = "HDR",
				tooltip = "Use the checkbox to disable High Dynamic Range in the scene."
			};

			// Token: 0x0400091E RID: 2334
			public static readonly DebugUI.Widget.NameAndTooltip PixelValidationMode = new DebugUI.Widget.NameAndTooltip
			{
				name = "Pixel Validation Mode",
				tooltip = "Choose between modes that validate pixel on screen."
			};

			// Token: 0x0400091F RID: 2335
			public static readonly DebugUI.Widget.NameAndTooltip Channels = new DebugUI.Widget.NameAndTooltip
			{
				name = "Channels",
				tooltip = "Choose the texture channel used to validate the scene."
			};

			// Token: 0x04000920 RID: 2336
			public static readonly DebugUI.Widget.NameAndTooltip ValueRangeMin = new DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Min",
				tooltip = "Any values set below this field will be considered invalid and will appear red on screen."
			};

			// Token: 0x04000921 RID: 2337
			public static readonly DebugUI.Widget.NameAndTooltip ValueRangeMax = new DebugUI.Widget.NameAndTooltip
			{
				name = "Value Range Max",
				tooltip = "Any values set above this field will be considered invalid and will appear blue on screen."
			};
		}

		// Token: 0x02000160 RID: 352
		internal static class WidgetFactory
		{
			// Token: 0x06000974 RID: 2420 RVA: 0x0003F3DC File Offset: 0x0003D5DC
			internal static DebugUI.Widget CreateMapOverlays(DebugDisplaySettingsRendering data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MapOverlays;
				enumField.autoEnum = typeof(DebugFullScreenMode);
				enumField.getter = () => (int)data.debugFullScreenMode;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.debugFullScreenMode;
				enumField.setIndex = delegate(int value)
				{
					data.debugFullScreenMode = (DebugFullScreenMode)value;
				};
				return enumField;
			}

			// Token: 0x06000975 RID: 2421 RVA: 0x0003F474 File Offset: 0x0003D674
			internal static DebugUI.Widget CreateMapOverlaySize(DebugDisplaySettingsRendering data)
			{
				DebugUI.Container container = new DebugUI.Container();
				ObservableList<DebugUI.Widget> children = container.children;
				DebugUI.IntField intField = new DebugUI.IntField();
				intField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.MapSize;
				intField.getter = () => data.debugFullScreenModeOutputSizeScreenPercent;
				intField.setter = delegate(int value)
				{
					data.debugFullScreenModeOutputSizeScreenPercent = value;
				};
				intField.incStep = 10;
				intField.min = () => 0;
				intField.max = () => 100;
				children.Add(intField);
				return container;
			}

			// Token: 0x06000976 RID: 2422 RVA: 0x0003F524 File Offset: 0x0003D724
			internal static DebugUI.Widget CreateAdditionalWireframeShaderViews(DebugDisplaySettingsRendering data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.AdditionalWireframeModes;
				enumField.autoEnum = typeof(DebugDisplaySettingsRendering.WireframeMode);
				enumField.getter = () => (int)data.wireframeMode;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.wireframeMode;
				enumField.setIndex = delegate(int value)
				{
					data.wireframeMode = (DebugDisplaySettingsRendering.WireframeMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x06000977 RID: 2423 RVA: 0x0003F5E0 File Offset: 0x0003D7E0
			internal static DebugUI.Widget CreateWireframeNotSupportedWarning(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.MessageBox
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.WireframeNotSupportedWarning,
					style = DebugUI.MessageBox.Style.Warning,
					isHiddenCallback = delegate
					{
						GraphicsDeviceType graphicsDeviceType = SystemInfo.graphicsDeviceType;
						return (graphicsDeviceType != GraphicsDeviceType.OpenGLES2 && graphicsDeviceType != GraphicsDeviceType.OpenGLES3 && graphicsDeviceType != GraphicsDeviceType.Vulkan) || data.wireframeMode == DebugDisplaySettingsRendering.WireframeMode.None;
					}
				};
			}

			// Token: 0x06000978 RID: 2424 RVA: 0x0003F624 File Offset: 0x0003D824
			internal static DebugUI.Widget CreateOverdraw(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.Overdraw,
					getter = () => data.overdraw,
					setter = delegate(bool value)
					{
						data.overdraw = value;
					}
				};
			}

			// Token: 0x06000979 RID: 2425 RVA: 0x0003F674 File Offset: 0x0003D874
			internal static DebugUI.Widget CreatePostProcessing(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.EnumField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.PostProcessing,
					autoEnum = typeof(DebugPostProcessingMode),
					getter = () => (int)data.debugPostProcessingMode,
					setter = delegate(int value)
					{
						data.debugPostProcessingMode = (DebugPostProcessingMode)value;
					},
					getIndex = () => (int)data.debugPostProcessingMode,
					setIndex = delegate(int value)
					{
						data.debugPostProcessingMode = (DebugPostProcessingMode)value;
					}
				};
			}

			// Token: 0x0600097A RID: 2426 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
			internal static DebugUI.Widget CreateMSAA(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.MSAA,
					getter = () => data.enableMsaa,
					setter = delegate(bool value)
					{
						data.enableMsaa = value;
					}
				};
			}

			// Token: 0x0600097B RID: 2427 RVA: 0x0003F748 File Offset: 0x0003D948
			internal static DebugUI.Widget CreateHDR(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.BoolField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.HDR,
					getter = () => data.enableHDR,
					setter = delegate(bool value)
					{
						data.enableHDR = value;
					}
				};
			}

			// Token: 0x0600097C RID: 2428 RVA: 0x0003F798 File Offset: 0x0003D998
			internal static DebugUI.Widget CreatePixelValidationMode(DebugDisplaySettingsRendering data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.PixelValidationMode;
				enumField.autoEnum = typeof(DebugValidationMode);
				enumField.getter = () => (int)data.validationMode;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.validationMode;
				enumField.setIndex = delegate(int value)
				{
					data.validationMode = (DebugValidationMode)value;
				};
				enumField.onValueChanged = delegate(DebugUI.Field<int> _, int _)
				{
					DebugManager.instance.ReDrawOnScreenDebug();
				};
				return enumField;
			}

			// Token: 0x0600097D RID: 2429 RVA: 0x0003F854 File Offset: 0x0003DA54
			internal static DebugUI.Widget CreatePixelValidationChannels(DebugDisplaySettingsRendering data)
			{
				DebugUI.EnumField enumField = new DebugUI.EnumField();
				enumField.nameAndTooltip = DebugDisplaySettingsRendering.Strings.Channels;
				enumField.autoEnum = typeof(PixelValidationChannels);
				enumField.getter = () => (int)data.validationChannels;
				enumField.setter = delegate(int value)
				{
				};
				enumField.getIndex = () => (int)data.validationChannels;
				enumField.setIndex = delegate(int value)
				{
					data.validationChannels = (PixelValidationChannels)value;
				};
				return enumField;
			}

			// Token: 0x0600097E RID: 2430 RVA: 0x0003F8EC File Offset: 0x0003DAEC
			internal static DebugUI.Widget CreatePixelValueRangeMin(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.ValueRangeMin,
					getter = () => data.ValidationRangeMin,
					setter = delegate(float value)
					{
						data.ValidationRangeMin = value;
					},
					incStep = 0.01f
				};
			}

			// Token: 0x0600097F RID: 2431 RVA: 0x0003F948 File Offset: 0x0003DB48
			internal static DebugUI.Widget CreatePixelValueRangeMax(DebugDisplaySettingsRendering data)
			{
				return new DebugUI.FloatField
				{
					nameAndTooltip = DebugDisplaySettingsRendering.Strings.ValueRangeMax,
					getter = () => data.ValidationRangeMax,
					setter = delegate(float value)
					{
						data.ValidationRangeMax = value;
					},
					incStep = 0.01f
				};
			}
		}

		// Token: 0x02000161 RID: 353
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x17000222 RID: 546
			// (get) Token: 0x06000980 RID: 2432 RVA: 0x0003F9A1 File Offset: 0x0003DBA1
			public override string PanelName
			{
				get
				{
					return "Rendering";
				}
			}

			// Token: 0x06000981 RID: 2433 RVA: 0x0003F9A8 File Offset: 0x0003DBA8
			public SettingsPanel(DebugDisplaySettingsRendering data)
			{
				base.AddWidget(DebugDisplaySettingsCommon.WidgetFactory.CreateMissingDebugShadersWarning());
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Rendering Debug",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlays(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMapOverlaySize(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateHDR(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateMSAA(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreatePostProcessing(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateAdditionalWireframeShaderViews(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateWireframeNotSupportedWarning(data),
						DebugDisplaySettingsRendering.WidgetFactory.CreateOverdraw(data)
					}
				});
				base.AddWidget(new DebugUI.Foldout
				{
					displayName = "Pixel Validation",
					isHeader = true,
					opened = true,
					children = 
					{
						DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationMode(data),
						new DebugUI.Container
						{
							displayName = "Pixel Range Settings",
							isHiddenCallback = () => data.validationMode != DebugValidationMode.HighlightOutsideOfRange,
							children = 
							{
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValidationChannels(data),
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMin(data),
								DebugDisplaySettingsRendering.WidgetFactory.CreatePixelValueRangeMax(data)
							}
						}
					}
				});
			}
		}
	}
}
