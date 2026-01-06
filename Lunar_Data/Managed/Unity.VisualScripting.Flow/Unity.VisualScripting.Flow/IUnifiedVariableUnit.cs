using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000139 RID: 313
	public interface IUnifiedVariableUnit : IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000859 RID: 2137
		VariableKind kind { get; }

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x0600085A RID: 2138
		ValueInput name { get; }
	}
}
