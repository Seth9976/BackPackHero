using System;
using System.Collections.ObjectModel;

namespace Unity.VisualScripting
{
	// Token: 0x0200015B RID: 347
	public interface IMultiInputUnit : IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000902 RID: 2306
		// (set) Token: 0x06000903 RID: 2307
		int inputCount { get; set; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000904 RID: 2308
		ReadOnlyCollection<ValueInput> multiInputs { get; }
	}
}
