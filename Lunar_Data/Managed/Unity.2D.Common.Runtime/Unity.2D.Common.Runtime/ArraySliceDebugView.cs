using System;

namespace UnityEngine.U2D.Common.UTess
{
	// Token: 0x02000005 RID: 5
	internal sealed class ArraySliceDebugView<T> where T : struct
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023ED File Offset: 0x000005ED
		public ArraySliceDebugView(ArraySlice<T> slice)
		{
			this.m_Slice = slice;
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000023FC File Offset: 0x000005FC
		public T[] Items
		{
			get
			{
				return this.m_Slice.ToArray();
			}
		}

		// Token: 0x04000009 RID: 9
		private ArraySlice<T> m_Slice;
	}
}
