using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BC RID: 188
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestSyncCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x00041BAC File Offset: 0x0003FDAC
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'Y', 'N', 'C');
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x00041BBB File Offset: 0x0003FDBB
		public FourCC typeStatic
		{
			get
			{
				return RequestSyncCommand.Type;
			}
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x00041BC4 File Offset: 0x0003FDC4
		public static RequestSyncCommand Create()
		{
			return new RequestSyncCommand
			{
				baseCommand = new InputDeviceCommand(RequestSyncCommand.Type, 8)
			};
		}

		// Token: 0x040004D8 RID: 1240
		internal const int kSize = 8;

		// Token: 0x040004D9 RID: 1241
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;
	}
}
