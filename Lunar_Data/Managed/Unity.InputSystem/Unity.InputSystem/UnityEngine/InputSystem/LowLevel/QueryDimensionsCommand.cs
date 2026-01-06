using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000B4 RID: 180
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct QueryDimensionsCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000417AD File Offset: 0x0003F9AD
		public static FourCC Type
		{
			get
			{
				return new FourCC('D', 'I', 'M', 'S');
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x000417BC File Offset: 0x0003F9BC
		public FourCC typeStatic
		{
			get
			{
				return QueryDimensionsCommand.Type;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000417C4 File Offset: 0x0003F9C4
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
