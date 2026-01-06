using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000A RID: 10
	public interface IStateTransition : IGraphElementWithDebugData, IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable, IConnection<IState, IState>
	{
		// Token: 0x0600002E RID: 46
		void Branch(Flow flow);

		// Token: 0x0600002F RID: 47
		void OnEnter(Flow flow);

		// Token: 0x06000030 RID: 48
		void OnExit(Flow flow);
	}
}
