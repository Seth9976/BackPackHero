using System;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000EF RID: 239
	public interface IInputStateCallbackReceiver
	{
		// Token: 0x06000E1F RID: 3615
		void OnNextUpdate();

		// Token: 0x06000E20 RID: 3616
		void OnStateEvent(InputEventPtr eventPtr);

		// Token: 0x06000E21 RID: 3617
		bool GetStateOffsetForEvent(InputControl control, InputEventPtr eventPtr, ref uint offset);
	}
}
