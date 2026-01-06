using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct TextEvent : IInputEventTypeInfo
	{
		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x000448EE File Offset: 0x00042AEE
		public FourCC typeStatic
		{
			get
			{
				return 1413830740;
			}
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000448FC File Offset: 0x00042AFC
		public unsafe static TextEvent* From(InputEventPtr eventPtr)
		{
			if (!eventPtr.valid)
			{
				throw new ArgumentNullException("eventPtr");
			}
			if (!eventPtr.IsA<TextEvent>())
			{
				throw new InvalidCastException(string.Format("Cannot cast event with type '{0}' into TextEvent", eventPtr.type));
			}
			return (TextEvent*)eventPtr.data;
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0004494C File Offset: 0x00042B4C
		public static TextEvent Create(int deviceId, char character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = (int)character
			};
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x00044984 File Offset: 0x00042B84
		public static TextEvent Create(int deviceId, int character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = character
			};
		}

		// Token: 0x04000591 RID: 1425
		public const int Type = 1413830740;

		// Token: 0x04000592 RID: 1426
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x04000593 RID: 1427
		[FieldOffset(20)]
		public int character;
	}
}
