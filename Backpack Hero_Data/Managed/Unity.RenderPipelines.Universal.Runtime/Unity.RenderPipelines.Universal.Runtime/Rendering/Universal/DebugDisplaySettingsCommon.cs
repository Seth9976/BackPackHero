using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004F RID: 79
	internal class DebugDisplaySettingsCommon : IDebugDisplaySettingsData, IDebugDisplaySettingsQuery
	{
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00013218 File Offset: 0x00011418
		public bool AreAnySettingsActive
		{
			get
			{
				return DebugDisplaySettings.Instance.AreAnySettingsActive;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00013224 File Offset: 0x00011424
		public bool IsPostProcessingAllowed
		{
			get
			{
				return DebugDisplaySettings.Instance.IsPostProcessingAllowed;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002DF RID: 735 RVA: 0x00013230 File Offset: 0x00011430
		public bool IsLightingActive
		{
			get
			{
				return DebugDisplaySettings.Instance.IsLightingActive;
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0001323C File Offset: 0x0001143C
		public bool TryGetScreenClearColor(ref Color color)
		{
			return DebugDisplaySettings.Instance.TryGetScreenClearColor(ref color);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00013249 File Offset: 0x00011449
		public IDebugDisplaySettingsPanelDisposable CreatePanel()
		{
			return new DebugDisplaySettingsCommon.SettingsPanel();
		}

		// Token: 0x02000154 RID: 340
		internal static class WidgetFactory
		{
			// Token: 0x0600095E RID: 2398 RVA: 0x0003E5A9 File Offset: 0x0003C7A9
			internal static DebugUI.Widget CreateMissingDebugShadersWarning()
			{
				DebugUI.MessageBox messageBox = new DebugUI.MessageBox();
				messageBox.displayName = "Warning: the debug shader variants are missing. Ensure that the \"Strip Debug Variants\" option is disabled in URP Global Settings.";
				messageBox.style = DebugUI.MessageBox.Style.Warning;
				messageBox.isHiddenCallback = () => !(UniversalRenderPipelineGlobalSettings.instance != null) || !UniversalRenderPipelineGlobalSettings.instance.stripDebugVariants;
				return messageBox;
			}
		}

		// Token: 0x02000155 RID: 341
		private class SettingsPanel : DebugDisplaySettingsPanel
		{
			// Token: 0x1700021F RID: 543
			// (get) Token: 0x0600095F RID: 2399 RVA: 0x0003E5E7 File Offset: 0x0003C7E7
			public override string PanelName
			{
				get
				{
					return "Frequently Used";
				}
			}

			// Token: 0x06000960 RID: 2400 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
			public SettingsPanel()
			{
				base.AddWidget(DebugDisplaySettingsCommon.WidgetFactory.CreateMissingDebugShadersWarning());
				DebugDisplaySettingsMaterial materialSettings = DebugDisplaySettings.Instance.MaterialSettings;
				DebugUI.Foldout foldout = new DebugUI.Foldout();
				foldout.displayName = "Material Filters";
				foldout.isHeader = true;
				foldout.opened = true;
				foldout.children.Add(DebugDisplaySettingsMaterial.WidgetFactory.CreateMaterialOverride(materialSettings));
				List<DebugUI.Foldout.ContextMenuItem> list = new List<DebugUI.Foldout.ContextMenuItem>();
				DebugUI.Foldout.ContextMenuItem contextMenuItem = default(DebugUI.Foldout.ContextMenuItem);
				contextMenuItem.displayName = "Go to Section...";
				contextMenuItem.action = delegate
				{
					DebugManager.instance.RequestEditorWindowPanelIndex(1);
				};
				list.Add(contextMenuItem);
				foldout.contextMenuItems = list;
				base.AddWidget(foldout);
				DebugDisplaySettingsLighting lightingSettings = DebugDisplaySettings.Instance.LightingSettings;
				DebugUI.Foldout foldout2 = new DebugUI.Foldout();
				foldout2.displayName = "Lighting Debug Modes";
				foldout2.isHeader = true;
				foldout2.opened = true;
				foldout2.children.Add(DebugDisplaySettingsLighting.WidgetFactory.CreateLightingDebugMode(lightingSettings));
				foldout2.children.Add(DebugDisplaySettingsLighting.WidgetFactory.CreateLightingFeatures(lightingSettings));
				List<DebugUI.Foldout.ContextMenuItem> list2 = new List<DebugUI.Foldout.ContextMenuItem>();
				contextMenuItem = default(DebugUI.Foldout.ContextMenuItem);
				contextMenuItem.displayName = "Go to Section...";
				contextMenuItem.action = delegate
				{
					DebugManager.instance.RequestEditorWindowPanelIndex(2);
				};
				list2.Add(contextMenuItem);
				foldout2.contextMenuItems = list2;
				base.AddWidget(foldout2);
				DebugDisplaySettingsRendering renderingSettings = DebugDisplaySettings.Instance.RenderingSettings;
				DebugUI.Foldout foldout3 = new DebugUI.Foldout();
				foldout3.displayName = "Rendering Debug";
				foldout3.isHeader = true;
				foldout3.opened = true;
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateHDR(renderingSettings));
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateMSAA(renderingSettings));
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreatePostProcessing(renderingSettings));
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateAdditionalWireframeShaderViews(renderingSettings));
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateWireframeNotSupportedWarning(renderingSettings));
				foldout3.children.Add(DebugDisplaySettingsRendering.WidgetFactory.CreateOverdraw(renderingSettings));
				List<DebugUI.Foldout.ContextMenuItem> list3 = new List<DebugUI.Foldout.ContextMenuItem>();
				contextMenuItem = default(DebugUI.Foldout.ContextMenuItem);
				contextMenuItem.displayName = "Go to Section...";
				contextMenuItem.action = delegate
				{
					DebugManager.instance.RequestEditorWindowPanelIndex(3);
				};
				list3.Add(contextMenuItem);
				foldout3.contextMenuItems = list3;
				base.AddWidget(foldout3);
			}

			// Token: 0x040008EE RID: 2286
			private const string k_GoToSectionString = "Go to Section...";
		}
	}
}
