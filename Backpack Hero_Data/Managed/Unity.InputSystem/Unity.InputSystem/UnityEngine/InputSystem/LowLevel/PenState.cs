using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CD RID: 205
	[StructLayout(LayoutKind.Explicit, Size = 36)]
	public struct PenState : IInputStateTypeInfo
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00041F63 File Offset: 0x00040163
		public static FourCC Format
		{
			get
			{
				return new FourCC('P', 'E', 'N', ' ');
			}
		}

		// Token: 0x06000CE2 RID: 3298 RVA: 0x00041F74 File Offset: 0x00040174
		public PenState WithButton(PenButton button, bool state = true)
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

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x00041FB4 File Offset: 0x000401B4
		public FourCC format
		{
			get
			{
				return PenState.Format;
			}
		}

		// Token: 0x0400051D RID: 1309
		[InputControl(usage = "Point", dontReset = true)]
		[FieldOffset(0)]
		public Vector2 position;

		// Token: 0x0400051E RID: 1310
		[InputControl(usage = "Secondary2DMotion", layout = "Delta")]
		[FieldOffset(8)]
		public Vector2 delta;

		// Token: 0x0400051F RID: 1311
		[InputControl(layout = "Vector2", displayName = "Tilt", usage = "Tilt")]
		[FieldOffset(16)]
		public Vector2 tilt;

		// Token: 0x04000520 RID: 1312
		[InputControl(layout = "Analog", usage = "Pressure", defaultState = 0f)]
		[FieldOffset(24)]
		public float pressure;

		// Token: 0x04000521 RID: 1313
		[InputControl(layout = "Axis", displayName = "Twist", usage = "Twist")]
		[FieldOffset(28)]
		public float twist;

		// Token: 0x04000522 RID: 1314
		[InputControl(name = "tip", displayName = "Tip", layout = "Button", bit = 0U, usage = "PrimaryAction")]
		[InputControl(name = "press", useStateFrom = "tip", synthetic = true, usages = new string[] { })]
		[InputControl(name = "eraser", displayName = "Eraser", layout = "Button", bit = 1U)]
		[InputControl(name = "inRange", displayName = "In Range?", layout = "Button", bit = 4U, synthetic = true)]
		[InputControl(name = "barrel1", displayName = "Barrel Button #1", layout = "Button", bit = 2U, alias = "barrelFirst", usage = "SecondaryAction")]
		[InputControl(name = "barrel2", displayName = "Barrel Button #2", layout = "Button", bit = 3U, alias = "barrelSecond")]
		[InputControl(name = "barrel3", displayName = "Barrel Button #3", layout = "Button", bit = 5U, alias = "barrelThird")]
		[InputControl(name = "barrel4", displayName = "Barrel Button #4", layout = "Button", bit = 6U, alias = "barrelFourth")]
		[InputControl(name = "radius", layout = "Vector2", format = "VEC2", sizeInBits = 64U, usage = "Radius", offset = 4294967294U)]
		[InputControl(name = "pointerId", layout = "Digital", format = "UINT", sizeInBits = 32U, offset = 4294967294U)]
		[FieldOffset(32)]
		public ushort buttons;

		// Token: 0x04000523 RID: 1315
		[FieldOffset(34)]
		private ushort displayIndex;
	}
}
