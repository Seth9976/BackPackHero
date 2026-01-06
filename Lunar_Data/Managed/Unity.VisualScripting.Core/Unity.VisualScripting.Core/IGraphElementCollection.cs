using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200006C RID: 108
	public interface IGraphElementCollection<T> : IKeyedCollection<Guid, T>, ICollection<T>, IEnumerable<T>, IEnumerable, INotifyCollectionChanged<T> where T : IGraphElement
	{
	}
}
