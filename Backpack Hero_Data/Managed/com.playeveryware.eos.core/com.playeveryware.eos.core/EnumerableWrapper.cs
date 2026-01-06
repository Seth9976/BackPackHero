using System;
using System.Collections;
using System.Collections.Generic;

// Token: 0x02000003 RID: 3
public class EnumerableWrapper<T> : IEnumerable<T>, IEnumerable
{
	// Token: 0x06000003 RID: 3 RVA: 0x000020F0 File Offset: 0x000002F0
	public EnumerableWrapper(IEnumerator<T> aEnumerator)
	{
		this.enumerator = aEnumerator;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000020FF File Offset: 0x000002FF
	public IEnumerator<T> GetEnumerator()
	{
		return this.enumerator;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002107 File Offset: 0x00000307
	IEnumerator IEnumerable.GetEnumerator()
	{
		return this.GetEnumerator();
	}

	// Token: 0x04000001 RID: 1
	private IEnumerator<T> enumerator;
}
