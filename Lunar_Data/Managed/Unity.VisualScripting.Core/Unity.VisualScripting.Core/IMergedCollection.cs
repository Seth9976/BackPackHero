using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x02000015 RID: 21
	public interface IMergedCollection<T> : ICollection<T>, IEnumerable<T>, IEnumerable
	{
		// Token: 0x0600008D RID: 141
		bool Includes<TI>() where TI : T;

		// Token: 0x0600008E RID: 142
		bool Includes(Type elementType);
	}
}
