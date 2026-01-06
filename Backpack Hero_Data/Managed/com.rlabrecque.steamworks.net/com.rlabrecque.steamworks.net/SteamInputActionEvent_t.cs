using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200019C RID: 412
	[Serializable]
	public struct SteamInputActionEvent_t
	{
		// Token: 0x04000A68 RID: 2664
		public InputHandle_t controllerHandle;

		// Token: 0x04000A69 RID: 2665
		public ESteamInputActionEventType eEventType;

		// Token: 0x04000A6A RID: 2666
		public SteamInputActionEvent_t.OptionValue m_val;

		// Token: 0x020001E7 RID: 487
		[Serializable]
		public struct AnalogAction_t
		{
			// Token: 0x04000ADA RID: 2778
			public InputAnalogActionHandle_t actionHandle;

			// Token: 0x04000ADB RID: 2779
			public InputAnalogActionData_t analogActionData;
		}

		// Token: 0x020001E8 RID: 488
		[Serializable]
		public struct DigitalAction_t
		{
			// Token: 0x04000ADC RID: 2780
			public InputDigitalActionHandle_t actionHandle;

			// Token: 0x04000ADD RID: 2781
			public InputDigitalActionData_t digitalActionData;
		}

		// Token: 0x020001E9 RID: 489
		[Serializable]
		[StructLayout(LayoutKind.Explicit)]
		public struct OptionValue
		{
			// Token: 0x04000ADE RID: 2782
			[FieldOffset(0)]
			public SteamInputActionEvent_t.AnalogAction_t analogAction;

			// Token: 0x04000ADF RID: 2783
			[FieldOffset(0)]
			public SteamInputActionEvent_t.DigitalAction_t digitalAction;
		}
	}
}
