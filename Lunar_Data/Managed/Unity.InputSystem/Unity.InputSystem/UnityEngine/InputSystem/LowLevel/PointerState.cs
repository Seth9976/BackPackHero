using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000CE RID: 206
	internal struct PointerState : IInputStateTypeInfo
	{
		// Token: 0x1700038A RID: 906
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x00041F73 File Offset: 0x00040173
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('P', 'T', 'R', ' ');
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x00041F82 File Offset: 0x00040182
		public FourCC format
		{
			get
			{
				return PointerState.kFormat;
			}
		}

		// Token: 0x04000524 RID: 1316
		private uint pointerId;

		// Token: 0x04000525 RID: 1317
		[InputControl(layout = "Vector2", displayName = "Position", usage = "Point", dontReset = true)]
		public Vector2 position;

		// Token: 0x04000526 RID: 1318
		[InputControl(layout = "Delta", displayName = "Delta", usage = "Secondary2DMotion")]
		public Vector2 delta;

		// Token: 0x04000527 RID: 1319
		[InputControl(layout = "Analog", displayName = "Pressure", usage = "Pressure", defaultState = 1f)]
		public float pressure;

		// Token: 0x04000528 RID: 1320
		[InputControl(layout = "Vector2", displayName = "Radius", usage = "Radius")]
		public Vector2 radius;

		// Token: 0x04000529 RID: 1321
		[InputControl(name = "press", displayName = "Press", layout = "Button", format = "BIT", bit = 0U)]
		public ushort buttons;

		// Token: 0x0400052A RID: 1322
		[InputControl(name = "displayIndex", layout = "Integer", displayName = "Display Index")]
		public ushort displayIndex;
	}
}
