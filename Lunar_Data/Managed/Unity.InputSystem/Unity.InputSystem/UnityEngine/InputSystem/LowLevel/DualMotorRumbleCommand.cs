using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C3 RID: 195
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct DualMotorRumbleCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000380 RID: 896
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00041DBC File Offset: 0x0003FFBC
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'M', 'B', 'L');
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x00041DCB File Offset: 0x0003FFCB
		public FourCC typeStatic
		{
			get
			{
				return DualMotorRumbleCommand.Type;
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00041DD4 File Offset: 0x0003FFD4
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
