using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting
{
	// Token: 0x0200001D RID: 29
	public struct NoAllocEnumerator<T> : IEnumerator<T>, IEnumerator, IDisposable
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000038D2 File Offset: 0x00001AD2
		public NoAllocEnumerator(IList<T> list)
		{
			this = default(NoAllocEnumerator<T>);
			this.list = list;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000038E2 File Offset: 0x00001AE2
		public void Dispose()
		{
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000038E4 File Offset: 0x00001AE4
		public bool MoveNext()
		{
			if (this.index < this.list.Count)
			{
				this.current = this.list[this.index];
				this.index++;
				return true;
			}
			this.index = this.list.Count + 1;
			this.current = default(T);
			this.exceeded = true;
			return false;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003952 File Offset: 0x00001B52
		public T Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000395A File Offset: 0x00001B5A
		object IEnumerator.Current
		{
			get
			{
				if (this.exceeded)
				{
					throw new InvalidOperationException();
				}
				return this.Current;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003975 File Offset: 0x00001B75
		void IEnumerator.Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x04000019 RID: 25
		private readonly IList<T> list;

		// Token: 0x0400001A RID: 26
		private int index;

		// Token: 0x0400001B RID: 27
		private T current;

		// Token: 0x0400001C RID: 28
		private bool exceeded;
	}
}
