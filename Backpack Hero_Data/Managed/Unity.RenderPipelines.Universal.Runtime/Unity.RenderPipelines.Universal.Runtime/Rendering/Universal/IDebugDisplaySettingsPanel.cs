using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000058 RID: 88
	public interface IDebugDisplaySettingsPanel
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x0600034A RID: 842
		string PanelName { get; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600034B RID: 843
		DebugUI.Widget[] Widgets { get; }
	}
}
