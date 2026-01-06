using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200004E RID: 78
	public class DebugDisplaySettings : IDebugDisplaySettingsQuery
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00012FD7 File Offset: 0x000111D7
		public static DebugDisplaySettings Instance
		{
			get
			{
				return DebugDisplaySettings.s_Instance.Value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00012FE3 File Offset: 0x000111E3
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00012FEB File Offset: 0x000111EB
		private DebugDisplaySettingsCommon CommonSettings { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00012FF4 File Offset: 0x000111F4
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00012FFC File Offset: 0x000111FC
		internal DebugDisplaySettingsMaterial MaterialSettings { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00013005 File Offset: 0x00011205
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0001300D File Offset: 0x0001120D
		internal DebugDisplaySettingsRendering RenderingSettings { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x00013016 File Offset: 0x00011216
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0001301E File Offset: 0x0001121E
		internal DebugDisplaySettingsLighting LightingSettings { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00013027 File Offset: 0x00011227
		public bool AreAnySettingsActive
		{
			get
			{
				return this.MaterialSettings.AreAnySettingsActive || this.LightingSettings.AreAnySettingsActive || this.RenderingSettings.AreAnySettingsActive;
			}
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00013050 File Offset: 0x00011250
		public bool TryGetScreenClearColor(ref Color color)
		{
			return this.MaterialSettings.TryGetScreenClearColor(ref color) || this.RenderingSettings.TryGetScreenClearColor(ref color) || this.LightingSettings.TryGetScreenClearColor(ref color);
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0001307C File Offset: 0x0001127C
		public bool IsLightingActive
		{
			get
			{
				return this.MaterialSettings.IsLightingActive && this.RenderingSettings.IsLightingActive && this.LightingSettings.IsLightingActive;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x000130A8 File Offset: 0x000112A8
		public bool IsPostProcessingAllowed
		{
			get
			{
				DebugPostProcessingMode debugPostProcessingMode = this.RenderingSettings.debugPostProcessingMode;
				switch (debugPostProcessingMode)
				{
				case DebugPostProcessingMode.Disabled:
					return false;
				case DebugPostProcessingMode.Auto:
					return this.MaterialSettings.IsPostProcessingAllowed && this.RenderingSettings.IsPostProcessingAllowed && this.LightingSettings.IsPostProcessingAllowed;
				case DebugPostProcessingMode.Enabled:
					return true;
				default:
					throw new ArgumentOutOfRangeException("debugPostProcessingMode", string.Format("Invalid post-processing state {0}", debugPostProcessingMode));
				}
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0001311B File Offset: 0x0001131B
		private TData Add<TData>(TData newData) where TData : IDebugDisplaySettingsData
		{
			this.m_Settings.Add(newData);
			return newData;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00013130 File Offset: 0x00011330
		private DebugDisplaySettings()
		{
			this.Reset();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0001314C File Offset: 0x0001134C
		internal void Reset()
		{
			this.m_Settings.Clear();
			this.CommonSettings = this.Add<DebugDisplaySettingsCommon>(new DebugDisplaySettingsCommon());
			this.MaterialSettings = this.Add<DebugDisplaySettingsMaterial>(new DebugDisplaySettingsMaterial());
			this.LightingSettings = this.Add<DebugDisplaySettingsLighting>(new DebugDisplaySettingsLighting());
			this.RenderingSettings = this.Add<DebugDisplaySettingsRendering>(new DebugDisplaySettingsRendering());
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000131A8 File Offset: 0x000113A8
		internal void ForEach(Action<IDebugDisplaySettingsData> onExecute)
		{
			foreach (IDebugDisplaySettingsData debugDisplaySettingsData in this.m_Settings)
			{
				onExecute(debugDisplaySettingsData);
			}
		}

		// Token: 0x04000221 RID: 545
		private readonly HashSet<IDebugDisplaySettingsData> m_Settings = new HashSet<IDebugDisplaySettingsData>();

		// Token: 0x04000222 RID: 546
		private static readonly Lazy<DebugDisplaySettings> s_Instance = new Lazy<DebugDisplaySettings>(() => new DebugDisplaySettings());
	}
}
