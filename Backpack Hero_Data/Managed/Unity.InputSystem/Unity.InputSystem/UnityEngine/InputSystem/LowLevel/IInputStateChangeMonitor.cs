using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F0 RID: 240
	public interface IInputStateChangeMonitor
	{
		// Token: 0x06000E27 RID: 3623
		void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex);

		// Token: 0x06000E28 RID: 3624
		void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex);
	}
}
