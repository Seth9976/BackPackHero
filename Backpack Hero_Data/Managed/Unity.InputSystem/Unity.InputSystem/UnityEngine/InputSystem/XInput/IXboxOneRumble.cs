using System;
using UnityEngine.InputSystem.Haptics;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x02000078 RID: 120
	public interface IXboxOneRumble : IDualMotorRumble, IHaptics
	{
		// Token: 0x06000A21 RID: 2593
		void SetMotorSpeeds(float lowFrequency, float highFrequency, float leftTrigger, float rightTrigger);
	}
}
