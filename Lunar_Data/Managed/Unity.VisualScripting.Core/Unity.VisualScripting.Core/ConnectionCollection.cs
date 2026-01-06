using System;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000026 RID: 38
	public class ConnectionCollection<TConnection, TSource, TDestination> : ConnectionCollectionBase<TConnection, TSource, TDestination, List<TConnection>> where TConnection : IConnection<TSource, TDestination>
	{
		// Token: 0x06000173 RID: 371 RVA: 0x0000462F File Offset: 0x0000282F
		public ConnectionCollection()
			: base(new List<TConnection>())
		{
		}
	}
}
