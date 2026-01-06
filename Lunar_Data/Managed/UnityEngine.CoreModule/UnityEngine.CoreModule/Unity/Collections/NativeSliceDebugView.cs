using System;

namespace Unity.Collections
{
	// Token: 0x0200009C RID: 156
	internal sealed class NativeSliceDebugView<T> where T : struct
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x000055F9 File Offset: 0x000037F9
		public NativeSliceDebugView(NativeSlice<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000560C File Offset: 0x0000380C
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000237 RID: 567
		private NativeSlice<T> m_Array;
	}
}
