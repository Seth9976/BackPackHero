using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EC RID: 236
	public interface IVisualElementScheduledItem
	{
		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600075F RID: 1887
		VisualElement element { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000760 RID: 1888
		bool isActive { get; }

		// Token: 0x06000761 RID: 1889
		void Resume();

		// Token: 0x06000762 RID: 1890
		void Pause();

		// Token: 0x06000763 RID: 1891
		void ExecuteLater(long delayMs);

		// Token: 0x06000764 RID: 1892
		IVisualElementScheduledItem StartingIn(long delayMs);

		// Token: 0x06000765 RID: 1893
		IVisualElementScheduledItem Every(long intervalMs);

		// Token: 0x06000766 RID: 1894
		IVisualElementScheduledItem Until(Func<bool> stopCondition);

		// Token: 0x06000767 RID: 1895
		IVisualElementScheduledItem ForDuration(long durationMs);
	}
}
