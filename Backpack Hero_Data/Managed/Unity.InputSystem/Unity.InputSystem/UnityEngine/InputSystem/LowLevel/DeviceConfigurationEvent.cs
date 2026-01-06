using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceConfigurationEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00042620 File Offset: 0x00040820
		public FourCC typeStatic
		{
			get
			{
				return 1145259591;
			}
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0004262C File Offset: 0x0004082C
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceConfigurationEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00042644 File Offset: 0x00040844
		public static DeviceConfigurationEvent Create(int deviceId, double time)
		{
			return new DeviceConfigurationEvent
			{
				baseEvent = new InputEvent(1145259591, 20, deviceId, time)
			};
		}

		// Token: 0x04000555 RID: 1365
		public const int Type = 1145259591;

		// Token: 0x04000556 RID: 1366
		[FieldOffset(0)]
		public InputEvent baseEvent;
	}
}
