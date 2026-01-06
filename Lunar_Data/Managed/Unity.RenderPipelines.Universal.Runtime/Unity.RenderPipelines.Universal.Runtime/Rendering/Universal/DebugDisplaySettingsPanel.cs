using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000052 RID: 82
	public abstract class DebugDisplaySettingsPanel : IDebugDisplaySettingsPanelDisposable, IDebugDisplaySettingsPanel, IDisposable
	{
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060002FD RID: 765
		public abstract string PanelName { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0001390D File Offset: 0x00011B0D
		public DebugUI.Widget[] Widgets
		{
			get
			{
				return this.m_Widgets.ToArray();
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0001391A File Offset: 0x00011B1A
		protected void AddWidget(DebugUI.Widget widget)
		{
			this.m_Widgets.Add(widget);
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00013928 File Offset: 0x00011B28
		public void Dispose()
		{
			this.m_Widgets.Clear();
		}

		// Token: 0x04000235 RID: 565
		private readonly List<DebugUI.Widget> m_Widgets = new List<DebugUI.Widget>();
	}
}
