using System;

namespace Unity.Collections
{
	// Token: 0x0200006E RID: 110
	[Obsolete("FixedListByte32DebugView is deprecated. (UnityUpgradable) -> FixedList32BytesDebugView<byte>", true)]
	internal sealed class FixedListByte32DebugView
	{
		// Token: 0x06000338 RID: 824 RVA: 0x00008D7B File Offset: 0x00006F7B
		public FixedListByte32DebugView(FixedList32Bytes<byte> list)
		{
			this.m_List = list;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00008D8A File Offset: 0x00006F8A
		public byte[] Items
		{
			get
			{
				return this.m_List.ToArray();
			}
		}

		// Token: 0x040000CA RID: 202
		private FixedList32Bytes<byte> m_List;
	}
}
