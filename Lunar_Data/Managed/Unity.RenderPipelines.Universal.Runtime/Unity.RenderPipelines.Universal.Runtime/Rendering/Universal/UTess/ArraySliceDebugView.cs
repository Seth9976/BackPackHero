using System;

namespace UnityEngine.Rendering.Universal.UTess
{
	// Token: 0x0200011A RID: 282
	internal sealed class ArraySliceDebugView<T> where T : struct
	{
		// Token: 0x060008DE RID: 2270 RVA: 0x0003976D File Offset: 0x0003796D
		public ArraySliceDebugView(ArraySlice<T> slice)
		{
			this.m_Slice = slice;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x0003977C File Offset: 0x0003797C
		public T[] Items
		{
			get
			{
				return this.m_Slice.ToArray();
			}
		}

		// Token: 0x0400081A RID: 2074
		private ArraySlice<T> m_Slice;
	}
}
