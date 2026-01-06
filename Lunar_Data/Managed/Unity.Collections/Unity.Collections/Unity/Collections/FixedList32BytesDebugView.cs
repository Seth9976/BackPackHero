using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000058 RID: 88
	internal sealed class FixedList32BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00005E6F File Offset: 0x0000406F
		public FixedList32BytesDebugView(FixedList32Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00005E7E File Offset: 0x0000407E
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000B5 RID: 181
		private FixedList32Bytes<T> m_List;
	}
}
