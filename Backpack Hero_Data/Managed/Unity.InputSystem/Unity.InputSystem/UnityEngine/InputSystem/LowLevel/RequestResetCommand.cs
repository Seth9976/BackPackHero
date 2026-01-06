using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestResetCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00041BB4 File Offset: 0x0003FDB4
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'S', 'E', 'T');
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00041BC3 File Offset: 0x0003FDC3
		public FourCC typeStatic
		{
			get
			{
				return RequestResetCommand.Type;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00041BCC File Offset: 0x0003FDCC
		public static RequestResetCommand Create()
		{
			return new RequestResetCommand
			{
				baseCommand = new InputDeviceCommand(RequestResetCommand.Type, 8)
			};
		}

		// Token: 0x040004D6 RID: 1238
		internal const int kSize = 8;

		// Token: 0x040004D7 RID: 1239
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
