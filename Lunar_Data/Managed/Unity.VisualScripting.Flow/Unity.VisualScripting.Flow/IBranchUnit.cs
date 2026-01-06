using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000038 RID: 56
	[TypeIconPriority]
	public interface IBranchUnit : IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000236 RID: 566
		ControlInput enter { get; }
	}
}
