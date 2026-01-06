using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EF RID: 239
	public interface IInputStateCallbackReceiver
	{
		// Token: 0x06000E24 RID: 3620
		void OnNextUpdate();

		// Token: 0x06000E25 RID: 3621
		void OnStateEvent(InputEventPtr eventPtr);

		// Token: 0x06000E26 RID: 3622
		bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset);
	}
}
