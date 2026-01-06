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
		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C5D RID: 3165 RVA: 0x00041283 File Offset: 0x0003F483
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x17000348 RID: 840
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x00041292 File Offset: 0x0003F492
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDUSBOutputReport.Type;
			}
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0004129C File Offset: 0x0003F49C
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
