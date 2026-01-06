using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B5 RID: 181
	[StructLayout(LayoutKind.Explicit, Size = 9)]
	public struct QueryEnabledStateCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000417ED File Offset: 0x0003F9ED
		public static FourCC Type
		{
			get
			{
				return new FourCC('Q', 'E', 'N', 'B');
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x000417FC File Offset: 0x0003F9FC
		public FourCC typeStatic
		{
			get
			{
				return QueryEnabledStateCommand.Type;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00041804 File Offset: 0x0003FA04
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
