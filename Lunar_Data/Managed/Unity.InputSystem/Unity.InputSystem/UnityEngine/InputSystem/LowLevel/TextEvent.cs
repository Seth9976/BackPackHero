using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Explicit, Size = 24)]
	public struct TextEvent : IInputEventTypeInfo
	{
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x000448A6 File Offset: 0x00042AA6
		public FourCC typeStatic
		{
			get
			{
				return 1413830740;
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x000448B4 File Offset: 0x00042AB4
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

		// Token: 0x06000DBC RID: 3516 RVA: 0x00044904 File Offset: 0x00042B04
		public static TextEvent Create(int deviceId, char character, double time = -1.0)
		{
			return new TextEvent
			{
				baseEvent = new InputEvent(1413830740, 24, deviceId, time),
				character = (int)character
			};
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0004493C File Offset: 0x00042B3C
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
