using System;
using UnityEngine.InputSystem.XR.Haptics;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000064 RID: 100
	public class XRControllerWithRumble : XRController
	{
		// Token: 0x06000994 RID: 2452 RVA: 0x00034F80 File Offset: 0x00033180
		public void SendImpulse(float amplitude, float duration)
		{
			SendHapticImpulseCommand sendHapticImpulseCommand = SendHapticImpulseCommand.Create(0, amplitude, duration);
			base.ExecuteCommand<SendHapticImpulseCommand>(ref sendHapticImpulseCommand);
		}
	}
}
