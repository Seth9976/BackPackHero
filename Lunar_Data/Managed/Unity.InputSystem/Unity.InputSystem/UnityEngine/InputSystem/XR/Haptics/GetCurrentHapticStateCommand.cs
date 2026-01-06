using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XR.Haptics
{
	// Token: 0x02000073 RID: 115
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct GetCurrentHapticStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000A0A RID: 2570 RVA: 0x000367A4 File Offset: 0x000349A4
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'S', '0');
			}
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A0B RID: 2571 RVA: 0x000367B3 File Offset: 0x000349B3
		public FourCC typeStatic
		{
			get
			{
				return GetCurrentHapticStateCommand.Type;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000367BA File Offset: 0x000349BA
		public HapticState currentState
		{
			get
			{
				return new HapticState(this.samplesQueued, this.samplesAvailable);
			}
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000367D0 File Offset: 0x000349D0
		public static GetCurrentHapticStateCommand Create()
		{
			return new GetCurrentHapticStateCommand
			{
				baseCommand = new InputDeviceCommand(GetCurrentHapticStateCommand.Type, 16)
			};
		}

		// Token: 0x0400035C RID: 860
		private const int kSize = 16;

		// Token: 0x0400035D RID: 861
		[FieldOffset(0)]
		private InputDeviceCommand baseCommand;

		// Token: 0x0400035E RID: 862
		[FieldOffset(8)]
		public uint samplesQueued;

		// Token: 0x0400035F RID: 863
		[FieldOffset(12)]
		public uint samplesAvailable;
	}
}
