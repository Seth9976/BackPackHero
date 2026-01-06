using System;
using System.Collections;
using System.Collections.Generic;

namespace Pathfinding.RVO
{
	// Token: 0x0200028F RID: 655
	public struct IReadOnlySlice<T> : IReadOnlyList<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>
	{
		// Token: 0x17000214 RID: 532
		public T this[int index]
		{
			get
			{
				return this.data[index];
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000F76 RID: 3958 RVA: 0x0006150D File Offset: 0x0005F70D
		public int Count
		{
			get
			{
				return this.length;
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00022029 File Offset: 0x00020229
		public IEnumerator<T> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000F78 RID: 3960 RVA: 0x00022029 File Offset: 0x00020229
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000BA2 RID: 2978
		public T[] data;

		// Token: 0x04000BA3 RID: 2979
		public int length;
	}
}
