using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AB RID: 171
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct DisableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00041616 File Offset: 0x0003F816
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'S', 'B', 'L');
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00041625 File Offset: 0x0003F825
		public FourCC typeStatic
		{
			get
			{
				return DisableDeviceCommand.Type;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x0004162C File Offset: 0x0003F82C
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
