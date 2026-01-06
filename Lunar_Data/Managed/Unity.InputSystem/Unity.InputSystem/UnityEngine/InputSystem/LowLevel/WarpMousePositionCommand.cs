using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct WarpMousePositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x00041CD4 File Offset: 0x0003FED4
		public static FourCC Type
		{
			get
			{
				return new FourCC('W', 'P', 'M', 'S');
			}
		}

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000CC4 RID: 3268 RVA: 0x00041CE3 File Offset: 0x0003FEE3
		public FourCC typeStatic
		{
			get
			{
				return WarpMousePositionCommand.Type;
			}
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00041CEC File Offset: 0x0003FEEC
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
