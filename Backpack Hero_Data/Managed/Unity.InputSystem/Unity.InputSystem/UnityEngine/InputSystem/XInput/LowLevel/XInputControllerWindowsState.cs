using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.XInput.LowLevel
{
	// Token: 0x0200007C RID: 124
	[StructLayout(LayoutKind.Explicit, Size = 4)]
	internal struct XInputControllerWindowsState : IInputStateTypeInfo
	{
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00036AF7 File Offset: 0x00034CF7
		public FourCC format
		{
			get
			{
				return new FourCC('X', 'I', 'N', 'P');
			}
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00036B06 File Offset: 0x00034D06
		public XInputControllerWindowsState WithButton(XInputControllerWindowsState.Button button)
		{
			this.buttons |= (ushort)(1 << (int)button);
			return this;
		}

		// Token: 0x04000378 RID: 888
		[InputControl(name = "dpad", layout = "Dpad", sizeInBits = 4U, bit = 0U)]
		[InputControl(name = "dpad/up", bit = 0U)]
		[InputControl(name = "dpad/down", bit = 1U)]
		[InputControl(name = "dpad/left", bit = 2U)]
		[InputControl(name = "dpad/right", bit = 3U)]
		[InputControl(name = "start", bit = 4U, displayName = "Start")]
		[InputControl(name = "select", bit = 5U, displayName = "Select")]
		[InputControl(name = "leftStickPress", bit = 6U)]
		[InputControl(name = "rightStickPress", bit = 7U)]
		[InputControl(name = "leftShoulder", bit = 8U)]
		[InputControl(name = "rightShoulder", bit = 9U)]
		[InputControl(name = "buttonSouth", bit = 12U, displayName = "A")]
		[InputControl(name = "buttonEast", bit = 13U, displayName = "B")]
		[InputControl(name = "buttonWest", bit = 14U, displayName = "X")]
		[InputControl(name = "buttonNorth", bit = 15U, displayName = "Y")]
		[FieldOffset(0)]
		public ushort buttons;

		// Token: 0x04000379 RID: 889
		[InputControl(name = "leftTrigger", format = "BYTE")]
		[FieldOffset(2)]
		public byte leftTrigger;

		// Token: 0x0400037A RID: 890
		[InputControl(name = "rightTrigger", format = "BYTE")]
		[FieldOffset(3)]
		public byte rightTrigger;

		// Token: 0x0400037B RID: 891
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "leftStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "leftStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "leftStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(4)]
		public short leftStickX;

		// Token: 0x0400037C RID: 892
		[FieldOffset(6)]
		public short leftStickY;

		// Token: 0x0400037D RID: 893
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2S")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "SHRT")]
		[InputControl(name = "rightStick/y", offset = 2U, format = "SHRT", parameters = "clamp=false,invert=false,normalize=false")]
		[InputControl(name = "rightStick/up", offset = 2U, format = "SHRT")]
		[InputControl(name = "rightStick/down", offset = 2U, format = "SHRT")]
		[FieldOffset(8)]
		public short rightStickX;

		// Token: 0x0400037E RID: 894
		[FieldOffset(10)]
		public short rightStickY;

		// Token: 0x020001C1 RID: 449
		public enum Button
		{
			// Token: 0x0400090C RID: 2316
			DPadUp,
			// Token: 0x0400090D RID: 2317
			DPadDown,
			// Token: 0x0400090E RID: 2318
			DPadLeft,
			// Token: 0x0400090F RID: 2319
			DPadRight,
			// Token: 0x04000910 RID: 2320
			Start,
			// Token: 0x04000911 RID: 2321
			Select,
			// Token: 0x04000912 RID: 2322
			LeftThumbstickPress,
			// Token: 0x04000913 RID: 2323
			RightThumbstickPress,
			// Token: 0x04000914 RID: 2324
			LeftShoulder,
			// Token: 0x04000915 RID: 2325
			RightShoulder,
			// Token: 0x04000916 RID: 2326
			A = 12,
			// Token: 0x04000917 RID: 2327
			B,
			// Token: 0x04000918 RID: 2328
			X,
			// Token: 0x04000919 RID: 2329
			Y
		}
	}
}
