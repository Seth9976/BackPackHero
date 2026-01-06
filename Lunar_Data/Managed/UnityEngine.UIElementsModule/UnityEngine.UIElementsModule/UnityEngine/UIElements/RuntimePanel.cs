using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000249 RID: 585
	internal class RuntimePanel : BaseRuntimePanel, IRuntimePanel
	{
		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000437FC File Offset: 0x000419FC
		public PanelSettings panelSettings
		{
			get
			{
				return this.m_PanelSettings;
			}
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00043804 File Offset: 0x00041A04
		public static RuntimePanel Create(ScriptableObject ownerObject)
		{
			return new RuntimePanel(ownerObject);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x0004381C File Offset: 0x00041A1C
		private RuntimePanel(ScriptableObject ownerObject)
			: base(ownerObject, RuntimePanel.s_EventDispatcher)
		{
			this.focusController = new FocusController(new NavigateFocusRing(this.visualTree));
			this.m_PanelSettings = ownerObject as PanelSettings;
			base.name = ((this.m_PanelSettings != null) ? this.m_PanelSettings.name : "RuntimePanel");
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00043884 File Offset: 0x00041A84
		public override void Update()
		{
			bool flag = this.m_PanelSettings != null;
			if (flag)
			{
				this.m_PanelSettings.ApplyPanelSettings();
			}
			base.Update();
		}

		// Token: 0x040007C5 RID: 1989
		internal static readonly EventDispatcher s_EventDispatcher = RuntimeEventDispatcher.Create();

		// Token: 0x040007C6 RID: 1990
		private readonly PanelSettings m_PanelSettings;
	}
}
