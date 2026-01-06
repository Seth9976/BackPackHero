using System;
using System.Runtime.InteropServices;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A2 RID: 162
	[StructLayout(LayoutKind.Explicit, Size = 47)]
	internal struct DualSenseHIDOutputReportPayload
	{
		// Token: 0x04000471 RID: 1137
		[FieldOffset(0)]
		public byte enableFlags1;

		// Token: 0x04000472 RID: 1138
		[FieldOffset(1)]
		public byte enableFlags2;

		// Token: 0x04000473 RID: 1139
		[FieldOffset(2)]
		public byte highFrequencyMotorSpeed;

		// Token: 0x04000474 RID: 1140
		[FieldOffset(3)]
		public byte lowFrequencyMotorSpeed;

		// Token: 0x04000475 RID: 1141
		[FieldOffset(44)]
		public byte redColor;

		// Token: 0x04000476 RID: 1142
		[FieldOffset(45)]
		public byte greenColor;

		// Token: 0x04000477 RID: 1143
		[FieldOffset(46)]
		public byte blueColor;
	}
}
