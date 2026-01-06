using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EE RID: 238
	internal interface IVisualElementPanelActivatable
	{
		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600076A RID: 1898
		VisualElement element { get; }

		// Token: 0x0600076B RID: 1899
		bool CanBeActivated();

		// Token: 0x0600076C RID: 1900
		void OnPanelActivate();

		// Token: 0x0600076D RID: 1901
		void OnPanelDeactivate();
	}
}
