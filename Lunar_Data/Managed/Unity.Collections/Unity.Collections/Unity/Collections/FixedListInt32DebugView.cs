using System;

namespace Unity.Collections
{
	// Token: 0x02000078 RID: 120
	[Obsolete("FixedListInt32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<int>", true)]
	internal sealed class FixedListInt32DebugView
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00008E07 File Offset: 0x00007007
		public FixedListInt32DebugView(FixedList32Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00008E16 File Offset: 0x00007016
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CF RID: 207
		private FixedList32Bytes<int> m_List;
	}
}
