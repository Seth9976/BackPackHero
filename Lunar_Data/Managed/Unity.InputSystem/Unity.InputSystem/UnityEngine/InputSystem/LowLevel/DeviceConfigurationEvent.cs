using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000D9 RID: 217
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceConfigurationEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x000425D8 File Offset: 0x000407D8
		public FourCC typeStatic
		{
			get
			{
				return 1145259591;
			}
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x000425E4 File Offset: 0x000407E4
		public unsafe InputEventPtr ToEventPtr()
		{
			fixed (DeviceConfigurationEvent* ptr = &this)
			{
				return new InputEventPtr((InputEvent*)ptr);
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000425FC File Offset: 0x000407FC
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
