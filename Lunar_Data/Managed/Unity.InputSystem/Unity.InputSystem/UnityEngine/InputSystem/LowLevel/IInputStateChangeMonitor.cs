using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000F0 RID: 240
	public interface IInputStateChangeMonitor
	{
		// Token: 0x06000E22 RID: 3618
		void NotifyControlStateChanged(InputControl control, double time, InputEventPtr eventPtr, long monitorIndex);

		// Token: 0x06000E23 RID: 3619
		void NotifyTimerExpired(InputControl control, double time, long monitorIndex, int timerIndex);
	}
}
