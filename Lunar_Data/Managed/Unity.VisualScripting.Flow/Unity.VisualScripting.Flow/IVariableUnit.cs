using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200014B RID: 331
	public interface IVariableUnit : IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000896 RID: 2198
		ValueInput name { get; }
	}
}
