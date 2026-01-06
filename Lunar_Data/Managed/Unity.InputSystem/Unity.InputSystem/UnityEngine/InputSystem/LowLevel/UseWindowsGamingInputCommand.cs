using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BF RID: 191
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	internal struct UseWindowsGamingInputCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x00041C85 File Offset: 0x0003FE85
		public static FourCC Type
		{
			get
			{
				return new FourCC('U', 'W', 'G', 'I');
			}
		}

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00041C94 File Offset: 0x0003FE94
		public FourCC typeStatic
		{
			get
			{
				return UseWindowsGamingInputCommand.Type;
			}
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x00041C9C File Offset: 0x0003FE9C
		public static UseWindowsGamingInputCommand Create(bool enable)
		{
			return new UseWindowsGamingInputCommand
			{
				baseCommand = new InputDeviceCommand(UseWindowsGamingInputCommand.Type, 9),
				enable = (enable ? 1 : 0)
			};
		}

		// Token: 0x040004E0 RID: 1248
		internal const int kSize = 9;

		// Token: 0x040004E1 RID: 1249
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004E2 RID: 1250
		[FieldOffset(8)]
		public byte enable;
	}
}
