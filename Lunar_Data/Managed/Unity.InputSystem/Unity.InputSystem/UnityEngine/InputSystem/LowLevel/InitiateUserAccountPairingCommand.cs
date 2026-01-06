using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InitiateUserAccountPairingCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x000416AB File Offset: 0x0003F8AB
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'I', 'R');
			}
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000C84 RID: 3204 RVA: 0x000416BA File Offset: 0x0003F8BA
		public FourCC typeStatic
		{
			get
			{
				return InitiateUserAccountPairingCommand.Type;
			}
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000416C4 File Offset: 0x0003F8C4
		public static InitiateUserAccountPairingCommand Create()
		{
			return new InitiateUserAccountPairingCommand
			{
				baseCommand = new InputDeviceCommand(InitiateUserAccountPairingCommand.Type, 8)
			};
		}

		// Token: 0x040004AF RID: 1199
		internal const int kSize = 8;

		// Token: 0x040004B0 RID: 1200
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x020001FA RID: 506
		public enum Result
		{
			// Token: 0x04000B08 RID: 2824
			SuccessfullyInitiated = 1,
			// Token: 0x04000B09 RID: 2825
			ErrorNotSupported = -1,
			// Token: 0x04000B0A RID: 2826
			ErrorAlreadyInProgress = -2
		}
	}
}
