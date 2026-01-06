using System;

namespace Unity.Collections
{
	// Token: 0x02000084 RID: 132
	[Obsolete("FixedListFloat64DebugView is deprecated. (UnityUpgradable) -> FixedList64BytesDebugView<float>", true)]
	internal sealed class FixedListFloat64DebugView
	{
		// Token: 0x0600034E RID: 846 RVA: 0x00008EAF File Offset: 0x000070AF
		public FixedListFloat64DebugView(FixedList64Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00008EBE File Offset: 0x000070BE
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D5 RID: 213
		private FixedList64Bytes<float> m_List;
	}
}
