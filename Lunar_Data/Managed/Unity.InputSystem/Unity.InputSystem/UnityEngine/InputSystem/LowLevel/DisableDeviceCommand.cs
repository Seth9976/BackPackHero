using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct DisableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x000415CE File Offset: 0x0003F7CE
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'S', 'B', 'L');
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000C79 RID: 3193 RVA: 0x000415DD File Offset: 0x0003F7DD
		public FourCC typeStatic
		{
			get
			{
				return DisableDeviceCommand.Type;
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000415E4 File Offset: 0x0003F7E4
		public static DisableDeviceCommand Create()
		{
			return new DisableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(DisableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x040004A8 RID: 1192
		internal const int kSize = 8;

		// Token: 0x040004A9 RID: 1193
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
