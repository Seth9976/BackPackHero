using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A3 RID: 163
	[StructLayout(LayoutKind.Explicit, Size = 56)]
	internal struct DualSenseHIDUSBOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x000412CB File Offset: 0x0003F4CB
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06000C61 RID: 3169 RVA: 0x000412DA File Offset: 0x0003F4DA
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDUSBOutputReport.Type;
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000412E4 File Offset: 0x0003F4E4
		public static DualSenseHIDUSBOutputReport Create(DualSenseHIDOutputReportPayload payload)
		{
			return new DualSenseHIDUSBOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDUSBOutputReport.Type, 56),
				reportId = 2,
				payload = payload
			};
		}

		// Token: 0x04000478 RID: 1144
		internal const int kSize = 56;

		// Token: 0x04000479 RID: 1145
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400047A RID: 1146
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x0400047B RID: 1147
		[FieldOffset(9)]
		public DualSenseHIDOutputReportPayload payload;
	}
}
