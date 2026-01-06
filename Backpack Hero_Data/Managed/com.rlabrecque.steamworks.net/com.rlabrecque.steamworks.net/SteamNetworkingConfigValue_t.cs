using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public struct SteamNetworkingConfigValue_t
	{
		// Token: 0x04000A7E RID: 2686
		public ESteamNetworkingConfigValue m_eValue;

		// Token: 0x04000A7F RID: 2687
		public ESteamNetworkingConfigDataType m_eDataType;

		// Token: 0x04000A80 RID: 2688
		public SteamNetworkingConfigValue_t.OptionValue m_val;

		// Token: 0x020001EA RID: 490
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000AE0 RID: 2784
			[FieldOffset(0)]
			public int m_int32;

			// Token: 0x04000AE1 RID: 2785
			[FieldOffset(0)]
			public long m_int64;

			// Token: 0x04000AE2 RID: 2786
			[FieldOffset(0)]
			public float m_float;

			// Token: 0x04000AE3 RID: 2787
			[FieldOffset(0)]
			public IntPtr m_string;

			// Token: 0x04000AE4 RID: 2788
			[FieldOffset(0)]
			public IntPtr m_functionPtr;
		}
	}
}
