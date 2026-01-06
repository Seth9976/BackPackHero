using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BB RID: 187
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct RequestResetCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CB3 RID: 3251 RVA: 0x00041B6C File Offset: 0x0003FD6C
		public static FourCC Type
		{
			get
			{
				return new FourCC('R', 'S', 'E', 'T');
			}
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x00041B7B File Offset: 0x0003FD7B
		public FourCC typeStatic
		{
			get
			{
				return RequestResetCommand.Type;
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00041B84 File Offset: 0x0003FD84
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
