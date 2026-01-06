using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D6 RID: 214
	[StructLayout(LayoutKind.Explicit, Size = 560)]
	internal struct TouchscreenState : IInputStateTypeInfo
	{
		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00042237 File Offset: 0x00040437
		public static FourCC Format
		{
			get
			{
				return new FourCC('T', 'S', 'C', 'R');
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x00042248 File Offset: 0x00040448
		public unsafe TouchState* primaryTouch
		{
			get
			{
				fixed (byte* ptr = &this.primaryTouchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x00042264 File Offset: 0x00040464
		public unsafe TouchState* touches
		{
			get
			{
				fixed (byte* ptr = &this.touchData.FixedElementField)
				{
					return (TouchState*)ptr;
				}
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0004227F File Offset: 0x0004047F
		public FourCC format
		{
			get
			{
				return TouchscreenState.Format;
			}
		}

		// Token: 0x04000544 RID: 1348
		public const int MaxTouches = 10;

		// Token: 0x04000545 RID: 1349
		[FixedBuffer(typeof(byte), 56)]
		[InputControl(name = "primaryTouch", displayName = "Primary Touch", layout = "Touch", synthetic = true)]
		[InputControl(name = "primaryTouch/tap", usage = "PrimaryAction")]
		[InputControl(name = "position", useStateFrom = "primaryTouch/position")]
		[InputControl(name = "delta", useStateFrom = "primaryTouch/delta", layout = "Delta")]
		[InputControl(name = "pressure", useStateFrom = "primaryTouch/pressure")]
		[InputControl(name = "radius", useStateFrom = "primaryTouch/radius")]
		[InputControl(name = "press", useStateFrom = "primaryTouch/phase", layout = "TouchPress", synthetic = true, usages = new string[] { })]
		[FieldOffset(0)]
		public TouchscreenState.<primaryTouchData>e__FixedBuffer primaryTouchData;

		// Token: 0x04000546 RID: 1350
		internal const int kTouchDataOffset = 56;

		// Token: 0x04000547 RID: 1351
		[FixedBuffer(typeof(byte), 560)]
		[InputControl(layout = "Touch", name = "touch", displayName = "Touch", arraySize = 10)]
		[FieldOffset(56)]
		public TouchscreenState.<touchData>e__FixedBuffer touchData;

		// Token: 0x02000203 RID: 515
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 56)]
		public struct <primaryTouchData>e__FixedBuffer
		{
			// Token: 0x04000B1C RID: 2844
			public byte FixedElementField;
		}

		// Token: 0x02000204 RID: 516
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 560)]
		public struct <touchData>e__FixedBuffer
		{
			// Token: 0x04000B1D RID: 2845
			public byte FixedElementField;
		}
	}
}
