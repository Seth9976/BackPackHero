using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Switch.LowLevel
{
	// Token: 0x0200008F RID: 143
	[StructLayout(LayoutKind.Explicit, Size = 7)]
	internal struct SwitchProControllerHIDInputState : IInputStateTypeInfo
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x0003D2D0 File Offset: 0x0003B4D0
		public FourCC format
		{
			get
			{
				return SwitchProControllerHIDInputState.Format;
			}
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0003D2D7 File Offset: 0x0003B4D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public SwitchProControllerHIDInputState WithButton(SwitchProControllerHIDInputState.Button button, bool value = true)
		{
			this.Set(button, value);
			return this;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0003D2E8 File Offset: 0x0003B4E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(SwitchProControllerHIDInputState.Button button, bool state)
		{
			if (button >= SwitchProControllerHIDInputState.Button.Capture)
			{
				if (button < (SwitchProControllerHIDInputState.Button)18)
				{
					byte b = (byte)(1 << button - SwitchProControllerHIDInputState.Button.Capture);
					if (state)
					{
						this.buttons2 |= b;
						return;
					}
					this.buttons2 &= ~b;
				}
				return;
			}
			ushort num = (ushort)(1 << (int)button);
			if (state)
			{
				this.buttons1 |= num;
				return;
			}
			this.buttons1 &= ~num;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0003D35B File Offset: 0x0003B55B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Press(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, true);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0003D365 File Offset: 0x0003B565
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Release(SwitchProControllerHIDInputState.Button button)
		{
			this.Set(button, false);
		}

		// Token: 0x04000414 RID: 1044
		public static FourCC Format = new FourCC('S', 'P', 'V', 'S');

		// Token: 0x04000415 RID: 1045
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85")]
		[InputControl(name = "leftStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "leftStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(0)]
		public byte leftStickX;

		// Token: 0x04000416 RID: 1046
		[FieldOffset(1)]
		public byte leftStickY;

		// Token: 0x04000417 RID: 1047
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "rightStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5")]
		[InputControl(name = "rightStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.15,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0.15,normalizeMax=0.85,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=0.85,invert=false")]
		[FieldOffset(2)]
		public byte rightStickX;

		// Token: 0x04000418 RID: 1048
		[FieldOffset(3)]
		public byte rightStickY;

		// Token: 0x04000419 RID: 1049
		[InputControl(name = "dpad", format = "BIT", bit = 0U, sizeInBits = 4U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/right", bit = 1U)]
		[InputControl(name = "dpad/down", bit = 2U)]
		[InputControl(name = "dpad/left", bit = 3U)]
		[InputControl(name = "buttonWest", displayName = "Y", shortDisplayName = "Y", bit = 4U, usage = "SecondaryAction")]
		[InputControl(name = "buttonNorth", displayName = "X", shortDisplayName = "X", bit = 5U)]
		[InputControl(name = "buttonSouth", displayName = "B", shortDisplayName = "B", bit = 6U, usage = "Back")]
		[InputControl(name = "buttonEast", displayName = "A", shortDisplayName = "A", bit = 7U, usage = "PrimaryAction")]
		[InputControl(name = "leftShoulder", displayName = "L", shortDisplayName = "L", bit = 8U)]
		[InputControl(name = "rightShoulder", displayName = "R", shortDisplayName = "R", bit = 9U)]
		[InputControl(name = "leftStickPress", displayName = "Left Stick", bit = 10U)]
		[InputControl(name = "rightStickPress", displayName = "Right Stick", bit = 11U)]
		[InputControl(name = "leftTrigger", displayName = "ZL", shortDisplayName = "ZL", format = "BIT", bit = 12U)]
		[InputControl(name = "rightTrigger", displayName = "ZR", shortDisplayName = "ZR", format = "BIT", bit = 13U)]
		[InputControl(name = "start", displayName = "Plus", bit = 14U, usage = "Menu")]
		[InputControl(name = "select", displayName = "Minus", bit = 15U)]
		[FieldOffset(4)]
		public ushort buttons1;

		// Token: 0x0400041A RID: 1050
		[InputControl(name = "capture", layout = "Button", displayName = "Capture", bit = 0U)]
		[InputControl(name = "home", layout = "Button", displayName = "Home", bit = 1U)]
		[FieldOffset(6)]
		public byte buttons2;

		// Token: 0x020001D6 RID: 470
		public enum Button
		{
			// Token: 0x04000988 RID: 2440
			Up,
			// Token: 0x04000989 RID: 2441
			Right,
			// Token: 0x0400098A RID: 2442
			Down,
			// Token: 0x0400098B RID: 2443
			Left,
			// Token: 0x0400098C RID: 2444
			West,
			// Token: 0x0400098D RID: 2445
			North,
			// Token: 0x0400098E RID: 2446
			South,
			// Token: 0x0400098F RID: 2447
			East,
			// Token: 0x04000990 RID: 2448
			L,
			// Token: 0x04000991 RID: 2449
			R,
			// Token: 0x04000992 RID: 2450
			StickL,
			// Token: 0x04000993 RID: 2451
			StickR,
			// Token: 0x04000994 RID: 2452
			ZL,
			// Token: 0x04000995 RID: 2453
			ZR,
			// Token: 0x04000996 RID: 2454
			Plus,
			// Token: 0x04000997 RID: 2455
			Minus,
			// Token: 0x04000998 RID: 2456
			Capture,
			// Token: 0x04000999 RID: 2457
			Home,
			// Token: 0x0400099A RID: 2458
			X = 5,
			// Token: 0x0400099B RID: 2459
			B,
			// Token: 0x0400099C RID: 2460
			Y = 4,
			// Token: 0x0400099D RID: 2461
			A = 7
		}
	}
}
