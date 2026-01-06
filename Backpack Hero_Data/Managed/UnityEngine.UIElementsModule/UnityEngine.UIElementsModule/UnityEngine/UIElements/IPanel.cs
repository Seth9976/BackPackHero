using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000057 RID: 87
	public interface IPanel : IDisposable
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001EF RID: 495
		VisualElement visualTree { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001F0 RID: 496
		EventDispatcher dispatcher { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001F1 RID: 497
		ContextType contextType { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001F2 RID: 498
		FocusController focusController { get; }

		// Token: 0x060001F3 RID: 499
		VisualElement Pick(Vector2 point);

		// Token: 0x060001F4 RID: 500
		VisualElement PickAll(Vector2 point, List<VisualElement> picked);

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001F5 RID: 501
		ContextualMenuManager contextualMenuManager { get; }
	}
}
