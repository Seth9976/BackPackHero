using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DA RID: 218
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceRemoveEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00042674 File Offset: 0x00040874
		public FourCC typeStatic
		{
			get
			{
				return 1146242381;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x00042680 File Offset: 0x00040880
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceRemoveEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x00042698 File Offset: 0x00040898
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
