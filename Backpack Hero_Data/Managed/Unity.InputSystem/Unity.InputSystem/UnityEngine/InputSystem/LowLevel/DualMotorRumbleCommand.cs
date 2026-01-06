using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct DualMotorRumbleCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x00041E04 File Offset: 0x00040004
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'M', 'B', 'L');
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CCE RID: 3278 RVA: 0x00041E13 File Offset: 0x00040013
		public FourCC typeStatic
		{
			get
			{
				return DualMotorRumbleCommand.Type;
			}
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00041E1C File Offset: 0x0004001C
		public static DualMotorRumbleCommand Create(float lowFrequency, float highFrequency)
		{
			return new DualMotorRumbleCommand
			{
				baseCommand = new InputDeviceCommand(DualMotorRumbleCommand.Type, 16),
				lowFrequencyMotorSpeed = lowFrequency,
				highFrequencyMotorSpeed = highFrequency
			};
		}

		// Token: 0x04000508 RID: 1288
		internal const int kSize = 16;

		// Token: 0x04000509 RID: 1289
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x0400050A RID: 1290
		[FieldOffset(8)]
		public float lowFrequencyMotorSpeed;

		// Token: 0x0400050B RID: 1291
		[FieldOffset(12)]
		public float highFrequencyMotorSpeed;
	}
}
