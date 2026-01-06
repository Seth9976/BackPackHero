using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A4 RID: 164
	[StructLayout(LayoutKind.Explicit, Size = 86)]
	internal struct DualSenseHIDBluetoothOutputReport : IInputDeviceCommandInfo
	{
		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06000C63 RID: 3171 RVA: 0x0004131D File Offset: 0x0003F51D
		public static FourCC Type
		{
			get
			{
				return new FourCC('H', 'I', 'D', 'O');
			}
		}

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0004132C File Offset: 0x0003F52C
		public FourCC typeStatic
		{
			get
			{
				return DualSenseHIDBluetoothOutputReport.Type;
			}
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x00041334 File Offset: 0x0003F534
		public static DualSenseHIDBluetoothOutputReport Create(DualSenseHIDOutputReportPayload payload, byte outputSequenceId)
		{
			return new DualSenseHIDBluetoothOutputReport
			{
				baseCommand = new InputDeviceCommand(DualSenseHIDBluetoothOutputReport.Type, 86),
				reportId = 49,
				tag1 = (byte)((outputSequenceId & 15) << 4),
				tag2 = 16,
				payload = payload
			};
		}

		// Token: 0x0400047C RID: 1148
		internal const int kSize = 86;

		// Token: 0x0400047D RID: 1149
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400047E RID: 1150
		[FieldOffset(8)]
		public byte reportId;

		// Token: 0x0400047F RID: 1151
		[FieldOffset(9)]
		public byte tag1;

		// Token: 0x04000480 RID: 1152
		[FieldOffset(10)]
		public byte tag2;

		// Token: 0x04000481 RID: 1153
		[FieldOffset(11)]
		public DualSenseHIDOutputReportPayload payload;

		// Token: 0x04000482 RID: 1154
		[FieldOffset(82)]
		public uint crc32;

		// Token: 0x04000483 RID: 1155
		[FixedBuffer(typeof(byte), 74)]
		[FieldOffset(8)]
		public DualSenseHIDBluetoothOutputReport.<rawData>e__FixedBuffer rawData;

		// Token: 0x020001F5 RID: 501
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 74)]
		public struct <rawData>e__FixedBuffer
		{
			// Token: 0x04000B01 RID: 2817
			public byte FixedElementField;
		}
	}
}
