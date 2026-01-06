using System;

namespace Unity.Collections
{
	// Token: 0x02000074 RID: 116
	[Obsolete("FixedListByte512DebugView is deprecated. (UnityUpgradable) -> FixedList512BytesDebugView<byte>", true)]
	internal sealed class FixedListByte512DebugView
	{
		// Token: 0x0600033E RID: 830 RVA: 0x00008DCF File Offset: 0x00006FCF
		public FixedListByte512DebugView(FixedList512Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00008DDE File Offset: 0x00006FDE
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CD RID: 205
		private FixedList512Bytes<byte> m_List;
	}
}
