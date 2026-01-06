using System;

namespace Unity.Collections
{
	// Token: 0x02000086 RID: 134
	[Obsolete("FixedListFloat128DebugView is deprecated. (UnityUpgradable) -> FixedList128BytesDebugView<float>", true)]
	internal sealed class FixedListFloat128DebugView
	{
		// Token: 0x06000350 RID: 848 RVA: 0x00008ECB File Offset: 0x000070CB
		public FixedListFloat128DebugView(FixedList128Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00008EDA File Offset: 0x000070DA
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D6 RID: 214
		private FixedList128Bytes<float> m_List;
	}
}
