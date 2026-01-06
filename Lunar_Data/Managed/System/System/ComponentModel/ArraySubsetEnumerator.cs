using System;
using System.Collections;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x0200071C RID: 1820
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	internal class ArraySubsetEnumerator : IEnumerator
	{
		// Token: 0x060039D6 RID: 14806 RVA: 0x000C8DD2 File Offset: 0x000C6FD2
		public ArraySubsetEnumerator(Array array, int count)
		{
			this.array = array;
			this.total = count;
			this.current = -1;
		}

		// Token: 0x060039D7 RID: 14807 RVA: 0x000C8DEF File Offset: 0x000C6FEF
		public bool MoveNext()
		{
			if (this.current < this.total - 1)
			{
				this.current++;
				return true;
			}
			return false;
		}

		// Token: 0x060039D8 RID: 14808 RVA: 0x000C8E12 File Offset: 0x000C7012
		public void Reset()
		{
			this.current = -1;
		}

		// Token: 0x17000D54 RID: 3412
		// (get) Token: 0x060039D9 RID: 14809 RVA: 0x000C8E1B File Offset: 0x000C701B
		public object Current
		{
			get
			{
				if (this.current == -1)
				{
					throw new InvalidOperationException();
				}
				return this.array.GetValue(this.current);
			}
		}

		// Token: 0x04002169 RID: 8553
		private Array array;

		// Token: 0x0400216A RID: 8554
		private int total;

		// Token: 0x0400216B RID: 8555
		private int current;
	}
}
