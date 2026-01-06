using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000E8 RID: 232
	internal interface IInputRuntime
	{
		// Token: 0x06000DC2 RID: 3522
		int AllocateDeviceId();

		// Token: 0x06000DC3 RID: 3523
		void Update(InputUpdateType type);

		// Token: 0x06000DC4 RID: 3524
		unsafe void QueueEvent(InputEvent* ptr);

		// Token: 0x06000DC5 RID: 3525
		unsafe long DeviceCommand(int deviceId, InputDeviceCommand* commandPtr);

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000DC6 RID: 3526
		// (set) Token: 0x06000DC7 RID: 3527
		InputUpdateDelegate onUpdate { get; set; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000DC8 RID: 3528
		// (set) Token: 0x06000DC9 RID: 3529
		Action<InputUpdateType> onBeforeUpdate { get; set; }

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000DCA RID: 3530
		// (set) Token: 0x06000DCB RID: 3531
		Func<InputUpdateType, bool> onShouldRunUpdate { get; set; }

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06000DCC RID: 3532
		// (set) Token: 0x06000DCD RID: 3533
		Action<int, string> onDeviceDiscovered { get; set; }

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06000DCE RID: 3534
		// (set) Token: 0x06000DCF RID: 3535
		Action<bool> onPlayerFocusChanged { get; set; }

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06000DD0 RID: 3536
		bool isPlayerFocused { get; }

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06000DD1 RID: 3537
		// (set) Token: 0x06000DD2 RID: 3538
		Action onShutdown { get; set; }

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06000DD3 RID: 3539
		// (set) Token: 0x06000DD4 RID: 3540
		float pollingFrequency { get; set; }

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000DD5 RID: 3541
		double currentTime { get; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000DD6 RID: 3542
		double currentTimeForFixedUpdate { get; }

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000DD7 RID: 3543
		float unscaledGameTime { get; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000DD8 RID: 3544
		double currentTimeOffsetToRealtimeSinceStartup { get; }

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06000DD9 RID: 3545
		bool runInBackground { get; }

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000DDA RID: 3546
		Vector2 screenSize { get; }

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000DDB RID: 3547
		ScreenOrientation screenOrientation { get; }

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000DDC RID: 3548
		bool isInBatchMode { get; }
	}
}
