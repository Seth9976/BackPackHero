using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000054 RID: 84
	public class DebugDisplaySettingsUI : IDebugData
	{
		// Token: 0x06000323 RID: 803 RVA: 0x00013B9A File Offset: 0x00011D9A
		private void Reset()
		{
			if (this.m_Settings != null)
			{
				this.m_Settings.Reset();
				this.UnregisterDebug();
				this.RegisterDebug(this.m_Settings);
				DebugManager.instance.RefreshEditor();
			}
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00013BCC File Offset: 0x00011DCC
		public void RegisterDebug(DebugDisplaySettings settings)
		{
			DebugManager debugManager = DebugManager.instance;
			List<IDebugDisplaySettingsPanelDisposable> panels = new List<IDebugDisplaySettingsPanelDisposable>();
			debugManager.RegisterData(this);
			this.m_Settings = settings;
			this.m_DisposablePanels = panels;
			Action<IDebugDisplaySettingsData> action = delegate(IDebugDisplaySettingsData data)
			{
				IDebugDisplaySettingsPanelDisposable debugDisplaySettingsPanelDisposable = data.CreatePanel();
				DebugUI.Widget[] widgets = debugDisplaySettingsPanelDisposable.Widgets;
				string panelName = debugDisplaySettingsPanelDisposable.PanelName;
				ObservableList<DebugUI.Widget> children = debugManager.GetPanel(panelName, true, 0, false).children;
				panels.Add(debugDisplaySettingsPanelDisposable);
				children.Add(widgets);
			};
			this.m_Settings.ForEach(action);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00013C30 File Offset: 0x00011E30
		public void UnregisterDebug()
		{
			DebugManager instance = DebugManager.instance;
			foreach (IDebugDisplaySettingsPanelDisposable debugDisplaySettingsPanelDisposable in this.m_DisposablePanels)
			{
				DebugUI.Widget[] widgets = debugDisplaySettingsPanelDisposable.Widgets;
				string panelName = debugDisplaySettingsPanelDisposable.PanelName;
				ObservableList<DebugUI.Widget> children = instance.GetPanel(panelName, true, 0, false).children;
				debugDisplaySettingsPanelDisposable.Dispose();
				children.Remove(widgets);
			}
			this.m_DisposablePanels = null;
			instance.UnregisterData(this);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00013CB8 File Offset: 0x00011EB8
		public Action GetReset()
		{
			return new Action(this.Reset);
		}

		// Token: 0x04000243 RID: 579
		private IEnumerable<IDebugDisplaySettingsPanelDisposable> m_DisposablePanels;

		// Token: 0x04000244 RID: 580
		private DebugDisplaySettings m_Settings;
	}
}
