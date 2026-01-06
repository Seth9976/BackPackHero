using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200006F RID: 111
	public interface IGraphElementWithData : IGraphElement, IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x0600038D RID: 909
		IGraphElementData CreateData();
	}
}
