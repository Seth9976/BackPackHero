using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000248 RID: 584
	internal interface IRuntimePanel
	{
		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001182 RID: 4482
		PanelSettings panelSettings { get; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001183 RID: 4483
		// (set) Token: 0x06001184 RID: 4484
		GameObject selectableGameObject { get; set; }
	}
}
