using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200016D RID: 365
	public interface IUnitPortCollection<TPort> : IKeyedCollection<string, TPort>, ICollection<TPort>, IEnumerable<TPort>, IEnumerable where TPort : IUnitPort
	{
		// Token: 0x0600096D RID: 2413
		TPort Single();
	}
}
