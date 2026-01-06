using System;

namespace Unity.Collections
{
	// Token: 0x02000082 RID: 130
	[Obsolete("FixedListFloat32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<float>", true)]
	internal sealed class FixedListFloat32DebugView
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00008E93 File Offset: 0x00007093
		public FixedListFloat32DebugView(FixedList32Bytes<float> list)
		{
			this.m_List = list;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00008EA2 File Offset: 0x000070A2
		public float[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000D4 RID: 212
		private FixedList32Bytes<float> m_List;
	}
}
