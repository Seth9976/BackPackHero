using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BC RID: 188
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestSyncCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00041BF4 File Offset: 0x0003FDF4
		public static FourCC Type
		{
			get
			{
				return new FourCC('S', 'Y', 'N', 'C');
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00041C03 File Offset: 0x0003FE03
		public FourCC typeStatic
		{
			get
			{
				return RequestSyncCommand.Type;
			}
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x00041C0C File Offset: 0x0003FE0C
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
