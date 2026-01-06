using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200007B RID: 123
	public interface IGameObjectEventUnit : IEventUnit, IUnit, IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IGraphEventListener
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000401 RID: 1025
		Type MessageListenerType { get; }
	}
}
