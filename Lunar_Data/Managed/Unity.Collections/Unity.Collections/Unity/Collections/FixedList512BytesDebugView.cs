using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x02000067 RID: 103
	internal sealed class FixedList512BytesDebugView<[global::System.Runtime.CompilerServices.IsUnmanaged] T> where T : struct, ValueType
	{
		// Token: 0x060002EB RID: 747 RVA: 0x000081A3 File Offset: 0x000063A3
		public FixedList512BytesDebugView(FixedList512Bytes<T> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060002EC RID: 748 RVA: 0x000081B2 File Offset: 0x000063B2
		public T[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000C4 RID: 196
		private FixedList512Bytes<T> m_List;
	}
}
