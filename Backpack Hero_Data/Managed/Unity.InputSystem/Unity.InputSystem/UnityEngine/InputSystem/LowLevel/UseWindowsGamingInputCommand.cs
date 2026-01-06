using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BF RID: 191
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	internal struct UseWindowsGamingInputCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00041CCD File Offset: 0x0003FECD
		public static FourCC Type
		{
			get
			{
				return new FourCC('U', 'W', 'G', 'I');
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00041CDC File Offset: 0x0003FEDC
		public FourCC typeStatic
		{
			get
			{
				return UseWindowsGamingInputCommand.Type;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00041CE4 File Offset: 0x0003FEE4
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
