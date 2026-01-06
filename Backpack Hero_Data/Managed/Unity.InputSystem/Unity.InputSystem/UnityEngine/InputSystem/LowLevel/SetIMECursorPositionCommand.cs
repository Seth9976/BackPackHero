using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000BD RID: 189
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	public struct SetIMECursorPositionCommand : IInputDeviceCommandInfo
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x00041C34 File Offset: 0x0003FE34
		public static FourCC Type
		{
			get
			{
				return new FourCC('I', 'M', 'E', 'P');
			}
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x00041C43 File Offset: 0x0003FE43
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
		}

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x00041C4B File Offset: 0x0003FE4B
		public FourCC typeStatic
		{
			get
			{
				return SetIMECursorPositionCommand.Type;
			}
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x00041C54 File Offset: 0x0003FE54
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
