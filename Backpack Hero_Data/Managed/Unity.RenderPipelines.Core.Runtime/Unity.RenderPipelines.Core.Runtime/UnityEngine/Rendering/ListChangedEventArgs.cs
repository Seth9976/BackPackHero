using System;

namespace UnityEngine.Rendering
{
	// Token: 0x0200005F RID: 95
	public sealed class ListChangedEventArgs<T> : EventArgs
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000EE6A File Offset: 0x0000D06A
		public ListChangedEventArgs(int index, T item)
		{
			this.index = index;
			this.item = item;
		}

		// Token: 0x04000200 RID: 512
		public readonly int index;

		// Token: 0x04000201 RID: 513
		public readonly T item;
	}
}
