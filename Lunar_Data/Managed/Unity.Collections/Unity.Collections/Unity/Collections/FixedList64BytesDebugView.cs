using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200005D RID: 93
	internal sealed class FixedList64BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000255 RID: 597 RVA: 0x00006A2B File Offset: 0x00004C2B
		public FixedList64BytesDebugView(FixedList64Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00006A3A File Offset: 0x00004C3A
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000BA RID: 186
		private FixedList64Bytes<T> m_List;
	}
}
