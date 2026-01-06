using System;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x0200009F RID: 159
	internal static class DualShockSupport
	{
		// Token: 0x06000C5C RID: 3164 RVA: 0x00041124 File Offset: 0x0003F324
		public static void Initialize()
		{
			InputSystem.RegisterLayout<DualShockGamepad>(null, null);
			string text = null;
			InputDeviceMatcher inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("vendorId", 1356);
			InputSystem.RegisterLayout<DualSenseGamepadHID>(text, new InputDeviceMatcher?(inputDeviceMatcher.WithCapability<int>("productId", 3302)));
			string text2 = null;
			inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("vendorId", 1356);
			InputSystem.RegisterLayout<DualShock4GamepadHID>(text2, new InputDeviceMatcher?(inputDeviceMatcher.WithCapability<int>("productId", 2508)));
			inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("vendorId", 1356);
			InputSystem.RegisterLayoutMatcher<DualShock4GamepadHID>(inputDeviceMatcher.WithCapability<int>("productId", 1476));
			inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithManufacturer("Sony.+Entertainment", true);
			InputSystem.RegisterLayoutMatcher<DualShock4GamepadHID>(inputDeviceMatcher.WithProduct("Wireless Controller", true));
			string text3 = null;
			inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithCapability<int>("vendorId", 1356);
			InputSystem.RegisterLayout<DualShock3GamepadHID>(text3, new InputDeviceMatcher?(inputDeviceMatcher.WithCapability<int>("productId", 616)));
			inputDeviceMatcher = default(InputDeviceMatcher);
			inputDeviceMatcher = inputDeviceMatcher.WithInterface("HID", true);
			inputDeviceMatcher = inputDeviceMatcher.WithManufacturer("Sony.+Entertainment", true);
			InputSystem.RegisterLayoutMatcher<DualShock3GamepadHID>(inputDeviceMatcher.WithProduct("PLAYSTATION(R)3 Controller", true));
		}
	}
}
