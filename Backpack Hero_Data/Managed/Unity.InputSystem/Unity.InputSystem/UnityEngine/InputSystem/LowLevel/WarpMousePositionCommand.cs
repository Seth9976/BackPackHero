using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct WarpMousePositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000CC6 RID: 3270 RVA: 0x00041D1C File Offset: 0x0003FF1C
		public static FourCC Type
		{
			get
			{
				return new FourCC('W', 'P', 'M', 'S');
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00041D2B File Offset: 0x0003FF2B
		public FourCC typeStatic
		{
			get
			{
				return WarpMousePositionCommand.Type;
			}
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x00041D34 File Offset: 0x0003FF34
		public static WarpMousePositionCommand Create(Vector2 position)
		{
			return new WarpMousePositionCommand
			{
				baseCommand = new InputDeviceCommand(WarpMousePositionCommand.Type, 16),
				warpPositionInPlayerDisplaySpace = position
			};
		}

		// Token: 0x040004E3 RID: 1251
		internal const int kSize = 16;

		// Token: 0x040004E4 RID: 1252
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004E5 RID: 1253
		[FieldOffset(8)]
		public Vector2 warpPositionInPlayerDisplaySpace;
	}
}
