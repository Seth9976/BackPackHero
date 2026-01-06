using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.DualShock.LowLevel
{
	// Token: 0x020000A6 RID: 166
	[StructLayout(LayoutKind.Explicit, Size = 32)]
	internal struct DualShock3HIDInputReport : IInputStateTypeInfo
	{
		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x000413A0 File Offset: 0x0003F5A0
		public FourCC format
		{
			get
			{
				return new FourCC('H', 'I', 'D', ' ');
			}
		}

		// Token: 0x0400048E RID: 1166
		[FieldOffset(0)]
		private ushort padding1;

		// Token: 0x0400048F RID: 1167
		[InputControl(name = "select", displayName = "Share", bit = 0U)]
		[InputControl(name = "leftStickPress", bit = 1U)]
		[InputControl(name = "rightStickPress", bit = 2U)]
		[InputControl(name = "start", displayName = "Options", bit = 3U)]
		[InputControl(name = "dpad", format = "BIT", layout = "Dpad", bit = 4U, sizeInBits = 4U)]
		[InputControl(name = "dpad/up", bit = 4U)]
		[InputControl(name = "dpad/right", bit = 5U)]
		[InputControl(name = "dpad/down", bit = 6U)]
		[InputControl(name = "dpad/left", bit = 7U)]
		[FieldOffset(2)]
		public byte buttons1;

		// Token: 0x04000490 RID: 1168
		[InputControl(name = "leftTriggerButton", layout = "Button", bit = 0U, synthetic = true)]
		[InputControl(name = "rightTriggerButton", layout = "Button", bit = 1U, synthetic = true)]
		[InputControl(name = "leftShoulder", bit = 2U)]
		[InputControl(name = "rightShoulder", bit = 3U)]
		[InputControl(name = "buttonNorth", displayName = "Triangle", bit = 4U)]
		[InputControl(name = "buttonEast", displayName = "Circle", bit = 5U)]
		[InputControl(name = "buttonSouth", displayName = "Cross", bit = 6U)]
		[InputControl(name = "buttonWest", displayName = "Square", bit = 7U)]
		[FieldOffset(3)]
		public byte buttons2;

		// Token: 0x04000491 RID: 1169
		[InputControl(name = "systemButton", layout = "Button", displayName = "System", bit = 0U)]
		[InputControl(name = "touchpadButton", layout = "Button", displayName = "Touchpad Press", bit = 1U)]
		[FieldOffset(4)]
		public byte buttons3;

		// Token: 0x04000492 RID: 1170
		[FieldOffset(5)]
		private byte padding2;

		// Token: 0x04000493 RID: 1171
		[InputControl(name = "leftStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "leftStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "leftStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "leftStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "leftStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "leftStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(6)]
		public byte leftStickX;

		// Token: 0x04000494 RID: 1172
		[FieldOffset(7)]
		public byte leftStickY;

		// Token: 0x04000495 RID: 1173
		[InputControl(name = "rightStick", layout = "Stick", format = "VC2B")]
		[InputControl(name = "rightStick/x", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "rightStick/left", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/right", offset = 0U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1")]
		[InputControl(name = "rightStick/y", offset = 1U, format = "BYTE", parameters = "invert,normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5")]
		[InputControl(name = "rightStick/up", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0,clampMax=0.5,invert")]
		[InputControl(name = "rightStick/down", offset = 1U, format = "BYTE", parameters = "normalize,normalizeMin=0,normalizeMax=1,normalizeZero=0.5,clamp=1,clampMin=0.5,clampMax=1,invert=false")]
		[FieldOffset(8)]
		public byte rightStickX;

		// Token: 0x04000496 RID: 1174
		[FieldOffset(9)]
		public byte rightStickY;

		// Token: 0x04000497 RID: 1175
		[FixedBuffer(typeof(byte), 8)]
		[FieldOffset(10)]
		private DualShock3HIDInputReport.<padding3>e__FixedBuffer padding3;

		// Token: 0x04000498 RID: 1176
		[InputControl(name = "leftTrigger", format = "BYTE")]
		[FieldOffset(18)]
		public byte leftTrigger;

		// Token: 0x04000499 RID: 1177
		[InputControl(name = "rightTrigger", format = "BYTE")]
		[FieldOffset(19)]
		public byte rightTrigger;

		// Token: 0x020001F6 RID: 502
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 8)]
		public struct <padding3>e__FixedBuffer
		{
			// Token: 0x04000B02 RID: 2818
			public byte FixedElementField;
		}
	}
}
