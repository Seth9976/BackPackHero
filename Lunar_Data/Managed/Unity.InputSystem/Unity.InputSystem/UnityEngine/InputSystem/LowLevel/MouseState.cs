using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CB RID: 203
	[StructLayout(LayoutKind.Explicit, Size = 30)]
	public struct MouseState : IInputStateTypeInfo
	{
		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x00041EC4 File Offset: 0x000400C4
		public static FourCC Format
		{
			get
			{
				return new FourCC('M', 'O', 'U', 'S');
			}
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00041ED4 File Offset: 0x000400D4
		public MouseState WithButton(MouseButton button, bool state = true)
		{
			uint num = 1U << (int)button;
			if (state)
			{
				this.buttons |= (ushort)num;
			}
			else
			{
				this.buttons &= (ushort)(~(ushort)num);
			}
			return this;
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x00041F14 File Offset: 0x00040114
		public FourCC format
		{
			get
			{
				return MouseState.Format;
			}
		}

		// Token: 0x04000511 RID: 1297
		[InputControl(usage = "Point", dontReset = true)]
		[FieldOffset(0)]
		public Vector2 position;

		// Token: 0x04000512 RID: 1298
		[InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		[FieldOffset(8)]
		public Vector2 delta;

		// Token: 0x04000513 RID: 1299
		[InputControl(displayName = "Scroll", layout = "Delta")]
		[InputControl(name = "scroll/x", aliases = new string[] { "horizontal" }, usage = "ScrollHorizontal", displayName = "Left/Right")]
		[InputControl(name = "scroll/y", aliases = new string[] { "vertical" }, usage = "ScrollVertical", displayName = "Up/Down", shortDisplayName = "Wheel")]
		[FieldOffset(16)]
		public Vector2 scroll;

		// Token: 0x04000514 RID: 1300
		[InputControl(name = "press", useStateFrom = "leftButton", synthetic = true, usages = new string[] { })]
		[InputControl(name = "leftButton", layout = "Button", bit = 0U, usage = "PrimaryAction", displayName = "Left Button", shortDisplayName = "LMB")]
		[InputControl(name = "rightButton", layout = "Button", bit = 1U, usage = "SecondaryAction", displayName = "Right Button", shortDisplayName = "RMB")]
		[InputControl(name = "middleButton", layout = "Button", bit = 2U, displayName = "Middle Button", shortDisplayName = "MMB")]
		[InputControl(name = "forwardButton", layout = "Button", bit = 3U, usage = "Forward", displayName = "Forward")]
		[InputControl(name = "backButton", layout = "Button", bit = 4U, usage = "Back", displayName = "Back")]
		[InputControl(name = "pressure", layout = "Axis", usage = "Pressure", offset = 4294967294U, format = "FLT", sizeInBits = 32U)]
		[InputControl(name = "radius", layout = "Vector2", usage = "Radius", offset = 4294967294U, format = "VEC2", sizeInBits = 64U)]
		[InputControl(name = "pointerId", layout = "Digital", format = "BIT", sizeInBits = 1U, offset = 4294967294U)]
		[FieldOffset(24)]
		public ushort buttons;

		// Token: 0x04000515 RID: 1301
		[InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		[FieldOffset(26)]
		public ushort displayIndex;

		// Token: 0x04000516 RID: 1302
		[InputControl(name = "clickCount", layout = "Integer", displayName = "Click Count", synthetic = true)]
		[FieldOffset(28)]
		public ushort clickCount;
	}
}
