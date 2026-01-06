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
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000A0C RID: 2572 RVA: 0x000367E0 File Offset: 0x000349E0
		private static FourCC Type
		{
			get
			{
				return new FourCC('X', 'H', 'S', '0');
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x000367EF File Offset: 0x000349EF
		public FourCC typeStatic
		{
			get
			{
				return GetCurrentHapticStateCommand.Type;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000367F6 File Offset: 0x000349F6
		public HapticState currentState
		{
			get
			{
				return new HapticState(this.samplesQueued, this.samplesAvailable);
			}
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0003680C File Offset: 0x00034A0C
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
