using System;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000DB RID: 219
	[StructLayout(LayoutKind.Explicit, Size = 20)]
	public struct DeviceResetEvent : IInputEventTypeInfo
	{
		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00042680 File Offset: 0x00040880
		public FourCC typeStatic
		{
			get
			{
				return 1146245972;
			}
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0004268C File Offset: 0x0004088C
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
