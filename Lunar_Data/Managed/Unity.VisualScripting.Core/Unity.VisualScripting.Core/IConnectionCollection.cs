using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200002A RID: 42
	public interface IConnectionCollection<TConnection, TSource, TDestination> : ICollection<TConnection>, IEnumerable<TConnection>, IEnumerable where TConnection : IConnection<TSource, TDestination>
	{
		// Token: 0x1700004F RID: 79
		IEnumerable<TConnection> this[TSource source] { get; }

		// Token: 0x17000050 RID: 80
		IEnumerable<TConnection> this[TDestination destination] { get; }

		// Token: 0x060001A0 RID: 416
		IEnumerable<TConnection> WithSource(TSource source);

		// Token: 0x060001A1 RID: 417
		IEnumerable<TConnection> WithDestination(TDestination destination);
	}
}
