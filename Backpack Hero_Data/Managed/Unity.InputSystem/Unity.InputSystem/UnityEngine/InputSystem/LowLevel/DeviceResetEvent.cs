using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DB RID: 219
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceResetEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x000426C8 File Offset: 0x000408C8
		public FourCC typeStatic
		{
			get
			{
				return 1146245972;
			}
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x000426D4 File Offset: 0x000408D4
		public static DeviceResetEvent Create(int deviceId, bool hardReset = false, double time = -1.0)
		{
			DeviceResetEvent deviceResetEvent = new DeviceResetEvent
			{
				baseEvent = new InputEvent(1146245972, 20, deviceId, time)
			};
			deviceResetEvent.hardReset = hardReset;
			return deviceResetEvent;
		}

		// Token: 0x04000559 RID: 1369
		public const int Type = 1146245972;

		// Token: 0x0400055A RID: 1370
		[FieldOffset(0)]
		public InputEvent baseEvent;

		// Token: 0x0400055B RID: 1371
		[FieldOffset(8)]
		public bool hardReset;
	}
}
