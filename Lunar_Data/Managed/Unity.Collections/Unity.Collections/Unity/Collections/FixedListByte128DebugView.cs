using System;

namespace Unity.Collections
{
	// Token: 0x02000072 RID: 114
	[Obsolete("FixedListByte128DebugView is deprecated. (UnityUpgradable) -> FixedList128BytesDebugView<byte>", true)]
	internal sealed class FixedListByte128DebugView
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00008DB3 File Offset: 0x00006FB3
		public FixedListByte128DebugView(FixedList128Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600033D RID: 829 RVA: 0x00008DC2 File Offset: 0x00006FC2
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CC RID: 204
		private FixedList128Bytes<byte> m_List;
	}
}
