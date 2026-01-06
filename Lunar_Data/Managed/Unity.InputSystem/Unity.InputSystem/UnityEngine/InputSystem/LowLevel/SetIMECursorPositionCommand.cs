using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct SetIMECursorPositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x00041BEC File Offset: 0x0003FDEC
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'P');
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x00041BFB File Offset: 0x0003FDFB
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x00041C03 File Offset: 0x0003FE03
		public FourCC typeStatic
		{
			get
			{
				return SetIMECursorPositionCommand.Type;
			}
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00041C0C File Offset: 0x0003FE0C
		public static SetIMECursorPositionCommand Create(Vector2 cursorPosition)
		{
			return new SetIMECursorPositionCommand
			{
				baseCommand = new InputDeviceCommand(SetIMECursorPositionCommand.Type, 16),
				m_Position = cursorPosition
			};
		}

		// Token: 0x040004DA RID: 1242
		internal const int kSize = 16;

		// Token: 0x040004DB RID: 1243
		[FieldOffset(0)]
		public InputDeviceCommand baseCommand;

		// Token: 0x040004DC RID: 1244
		[FieldOffset(8)]
		private Vector2 m_Position;
	}
}
