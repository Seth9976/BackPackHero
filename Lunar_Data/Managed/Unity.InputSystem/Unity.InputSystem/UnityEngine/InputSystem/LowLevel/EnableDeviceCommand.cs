using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct EnableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x0004160C File Offset: 0x0003F80C
		public static FourCC Type
		{
			get
			{
				return new FourCC('E', 'N', 'B', 'L');
			}
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0004161B File Offset: 0x0003F81B
		public FourCC typeStatic
		{
			get
			{
				return EnableDeviceCommand.Type;
			}
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00041624 File Offset: 0x0003F824
		public static EnableDeviceCommand Create()
		{
			return new EnableDeviceCommand
			{
				baseCommand = new InputDeviceCommand(EnableDeviceCommand.Type, 8)
			};
		}

		// Token: 0x040004AA RID: 1194
		internal const int kSize = 8;

		// Token: 0x040004AB RID: 1195
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
