using System;

namespace Unity.VisualScripting
{
	// Token: 0x02000165 RID: 357
	public interface IUnitControlPort : IUnitPort, IGraphItem
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x0600095A RID: 2394
		bool isPredictable { get; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x0600095B RID: 2395
		bool couldBeEntered { get; }
	}
}
