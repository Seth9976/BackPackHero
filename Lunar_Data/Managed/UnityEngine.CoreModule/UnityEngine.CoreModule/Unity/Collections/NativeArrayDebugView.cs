using System;

namespace Unity.Collections
{
	// Token: 0x02000097 RID: 151
	internal sealed class NativeArrayDebugView<T> where T : struct
	{
		// Token: 0x060002A9 RID: 681 RVA: 0x00005075 File Offset: 0x00003275
		public NativeArrayDebugView(NativeArray<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00005086 File Offset: 0x00003286
		public T[] Items
		{
			get
			{
				return this.m_Array.ToArray();
			}
		}

		// Token: 0x04000230 RID: 560
		private NativeArray<T> m_Array;
	}
}
