using System;

namespace Unity.Collections
{
	// Token: 0x0200007E RID: 126
	[Obsolete("FixedListInt512DebugView is deprecated. (UnityUpgradable) -> FixedList512BytesDebugView<int>", true)]
	internal sealed class FixedListInt512DebugView
	{
		// Token: 0x06000348 RID: 840 RVA: 0x00008E5B File Offset: 0x0000705B
		public FixedListInt512DebugView(FixedList512Bytes<int> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000349 RID: 841 RVA: 0x00008E6A File Offset: 0x0000706A
		public int[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D2 RID: 210
		private FixedList512Bytes<int> m_List;
	}
}
