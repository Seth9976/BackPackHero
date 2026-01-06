using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x020000B6 RID: 182
	internal sealed class NativeListDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000705 RID: 1797 RVA: 0x00016073 File Offset: 0x00014273
		public NativeListDebugView(NativeList<T> array)
		{
			this.m_Array = array;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000706 RID: 1798 RVA: 0x00016084 File Offset: 0x00014284
		public T[] Items
		{
			get
			{
				return this.m_Array.AsArray().ToArray();
			}
		}

		// Token: 0x04000284 RID: 644
		private NativeList<T> m_Array;
	}
}
