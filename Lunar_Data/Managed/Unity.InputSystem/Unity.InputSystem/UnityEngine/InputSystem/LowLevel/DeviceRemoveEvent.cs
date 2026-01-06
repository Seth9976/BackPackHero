using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DA RID: 218
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceRemoveEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x0004262C File Offset: 0x0004082C
		public FourCC typeStatic
		{
			get
			{
				return 1146242381;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00042638 File Offset: 0x00040838
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceRemoveEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00042650 File Offset: 0x00040850
		public static DeviceRemoveEvent Create(int deviceId, double time = -1.0)
		{
			return new DeviceRemoveEvent
			{
				baseEvent = new InputEvent(1146242381, 20, deviceId, time)
			};
		}

		// Token: 0x04000557 RID: 1367
		public const int Type = 1146242381;

		// Token: 0x04000558 RID: 1368
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
