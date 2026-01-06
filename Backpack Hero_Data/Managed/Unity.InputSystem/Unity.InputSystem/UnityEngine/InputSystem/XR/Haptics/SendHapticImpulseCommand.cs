using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000077 RID: 119
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct SendHapticImpulseCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0003698B File Offset: 0x00034B8B
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'I', '0');
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0003699A File Offset: 0x00034B9A
		public FourCC typeStatic
		{
			get
			{
				return SendHapticImpulseCommand.Type;
			}
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x000369A4 File Offset: 0x00034BA4
		public static SendHapticImpulseCommand Create(int motorChannel, float motorAmplitude, float motorDuration)
		{
			return new SendHapticImpulseCommand
			{
				baseCommand = new InputDeviceCommand(SendHapticImpulseCommand.Type, 20),
				channel = motorChannel,
				amplitude = motorAmplitude,
				duration = motorDuration
			};
		}

		// Token: 0x0400036E RID: 878
		private const int kSize = 20;

		// Token: 0x0400036F RID: 879
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x04000370 RID: 880
		[FieldOffset(8)]
		private int channel;

		// Token: 0x04000371 RID: 881
		[FieldOffset(12)]
		private float amplitude;

		// Token: 0x04000372 RID: 882
		[FieldOffset(16)]
		private float duration;
	}
}
