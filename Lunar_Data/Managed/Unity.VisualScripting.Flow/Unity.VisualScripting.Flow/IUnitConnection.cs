using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000004 RID: 4
	public interface IUnitConnection : IConnection<IUnitOutputPort, IUnitInputPort>, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001F RID: 31
		FlowGraph graph { get; }
	}
}
