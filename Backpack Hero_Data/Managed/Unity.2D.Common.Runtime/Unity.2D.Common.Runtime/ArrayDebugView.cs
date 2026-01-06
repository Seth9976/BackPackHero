using System;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000003 RID: 3
	internal sealed class ArrayDebugView<T> where T : struct
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021A2 File Offset: 0x000003A2
		public ArrayDebugView(Array<T> array)
		{
			this.array = array;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000021B4 File Offset: 0x000003B4
		public T[] Items
		{
			get
			{
				T[] array = new T[this.array.Length];
				this.array.CopyTo(array);
				return array;
			}
		}

		// Token: 0x04000005 RID: 5
		private Array<T> array;
	}
}
