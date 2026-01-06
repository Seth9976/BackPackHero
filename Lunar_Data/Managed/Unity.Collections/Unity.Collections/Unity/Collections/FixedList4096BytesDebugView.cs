using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200006C RID: 108
	internal sealed class FixedList4096BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00008D5F File Offset: 0x00006F5F
		public FixedList4096BytesDebugView(FixedList4096Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00008D6E File Offset: 0x00006F6E
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C9 RID: 201
		private FixedList4096Bytes<T> m_List;
	}
}
