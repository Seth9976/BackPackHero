using System;
using UnityEngine.InputSystem.Haptics;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x020000A0 RID: 160
	public interface IDualShockHaptics : IDualMotorRumble, IHaptics
	{
		// Token: 0x06000C5D RID: 3165
		void SetLightBarColor(Color color);
	}
}
