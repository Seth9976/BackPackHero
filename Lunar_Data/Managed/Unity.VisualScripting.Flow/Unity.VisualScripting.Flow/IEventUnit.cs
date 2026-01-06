using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200007A RID: 122
	public interface IEventUnit : IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener
	{
		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000400 RID: 1024
		bool coroutine { get; }
	}
}
