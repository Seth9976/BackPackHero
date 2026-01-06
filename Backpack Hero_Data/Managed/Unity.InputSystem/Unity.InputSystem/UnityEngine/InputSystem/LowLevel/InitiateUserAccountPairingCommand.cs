using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AF RID: 175
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct InitiateUserAccountPairingCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000C86 RID: 3206 RVA: 0x000416F3 File Offset: 0x0003F8F3
		public static FourCC Type
		{
			get
			{
				return new FourCC('P', 'A', 'I', 'R');
			}
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00041702 File Offset: 0x0003F902
		public FourCC typeStatic
		{
			get
			{
				return InitiateUserAccountPairingCommand.Type;
			}
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0004170C File Offset: 0x0003F90C
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
			// Token: 0x04000B09 RID: 2825
			SuccessfullyInitiated = 1,
			// Token: 0x04000B0A RID: 2826
			ErrorNotSupported = -1,
			// Token: 0x04000B0B RID: 2827
			ErrorAlreadyInProgress = -2
		}
	}
}
