using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DD RID: 221
	[StructLayout(LayoutKind.Explicit, Size = 152)]
	public struct IMECompositionEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000426C6 File Offset: 0x000408C6
		public FourCC typeStatic
		{
			get
			{
				return 1229800787;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000426D4 File Offset: 0x000408D4
		public static IMECompositionEvent Create(int deviceId, string compositionString, double time)
		{
			return new IMECompositionEvent
			{
				baseEvent = new InputEvent(1229800787, 152, deviceId, time),
				compositionString = new IMECompositionString(compositionString)
			};
		}

		// Token: 0x0400055C RID: 1372
		internal const int kIMECharBufferSize = 64;

		// Token: 0x0400055D RID: 1373
		public const int Type = 1229800787;

		// Token: 0x0400055E RID: 1374
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x0400055F RID: 1375
		[FieldOffset(20)]
		public IMECompositionString compositionString;
	}
}
