using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XInput
{
	// Token: 0x0200007B RID: 123
	internal static class XInputSupport
	{
		// Token: 0x06000A2C RID: 2604 RVA: 0x00036ABC File Offset: 0x00034CBC
		public static void Initialize()
		{
			InputSystem.RegisterLayout<XInputController>(null, null);
			InputSystem.RegisterLayout<XInputControllerWindows>(null, new InputDeviceMatcher?(default(InputDeviceMatcher).WithInterface("XInput", true)));
		}
	}
}
