using System;
using UnityEngine.InputSystem.Haptics;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x020000A0 RID: 160
	public interface IDualShockHaptics : IDualMotorRumble, IHaptics
	{
		// Token: 0x06000C5A RID: 3162
		void SetLightBarColor(Color color);
	}
}
