using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000070 RID: 112
	public interface IGraphElementWithDebugData : IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600038E RID: 910
		IGraphElementDebugData CreateDebugData();
	}
}
