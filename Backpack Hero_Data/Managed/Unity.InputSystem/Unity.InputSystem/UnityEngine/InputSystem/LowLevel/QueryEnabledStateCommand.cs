using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryEnabledStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000C9C RID: 3228 RVA: 0x00041835 File Offset: 0x0003FA35
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'E', 'N', 'B');
			}
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x00041844 File Offset: 0x0003FA44
		public FourCC typeStatic
		{
			get
			{
				return QueryEnabledStateCommand.Type;
			}
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0004184C File Offset: 0x0003FA4C
		public static QueryEnabledStateCommand Create()
		{
			return new QueryEnabledStateCommand
			{
				baseCommand = new InputDeviceCommand(QueryEnabledStateCommand.Type, 9)
			};
		}

		// Token: 0x040004BD RID: 1213
		internal const int kSize = 9;

		// Token: 0x040004BE RID: 1214
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004BF RID: 1215
		[FieldOffset(8)]
		public bool isEnabled;
	}
}
