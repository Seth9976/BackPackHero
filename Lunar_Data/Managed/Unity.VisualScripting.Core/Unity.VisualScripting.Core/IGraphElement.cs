using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200006B RID: 107
	public interface IGraphElement : IGraphItem, INotifiedCollectionItem, IDisposable, IPrewarmable, IAotStubbable, IIdentifiable, IAnalyticsIdentifiable
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000382 RID: 898
		// (set) Token: 0x06000383 RID: 899
		IGraph graph { get; set; }

		// Token: 0x06000384 RID: 900
		bool HandleDependencies();

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000385 RID: 901
		int dependencyOrder { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000386 RID: 902
		// (set) Token: 0x06000387 RID: 903
		Guid guid { get; set; }

		// Token: 0x06000388 RID: 904
		void Instantiate(GraphReference instance);

		// Token: 0x06000389 RID: 905
		void Uninstantiate(GraphReference instance);

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600038A RID: 906
		IEnumerable<ISerializationDependency> deserializationDependencies { get; }
	}
}
