using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000AC RID: 172
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct EnableDeviceCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00041654 File Offset: 0x0003F854
		public static FourCC Type
		{
			get
			{
				return new FourCC('E', 'N', 'B', 'L');
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00041663 File Offset: 0x0003F863
		public FourCC typeStatic
		{
			get
			{
				return EnableDeviceCommand.Type;
			}
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0004166C File Offset: 0x0003F86C
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
