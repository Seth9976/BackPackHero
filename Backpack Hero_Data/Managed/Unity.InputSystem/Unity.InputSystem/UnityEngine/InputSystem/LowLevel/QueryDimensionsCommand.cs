using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct QueryDimensionsCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000C99 RID: 3225 RVA: 0x000417F5 File Offset: 0x0003F9F5
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'I', 'M', 'S');
			}
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000C9A RID: 3226 RVA: 0x00041804 File Offset: 0x0003FA04
		public FourCC typeStatic
		{
			get
			{
				return QueryDimensionsCommand.Type;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0004180C File Offset: 0x0003FA0C
		public static QueryDimensionsCommand Create()
		{
			return new QueryDimensionsCommand
			{
				baseCommand = new InputDeviceCommand(QueryDimensionsCommand.Type, 16)
			};
		}

		// Token: 0x040004BA RID: 1210
		internal const int kSize = 16;

		// Token: 0x040004BB RID: 1211
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004BC RID: 1212
		[FieldOffset(8)]
		public Vector2 outDimensions;
	}
}
