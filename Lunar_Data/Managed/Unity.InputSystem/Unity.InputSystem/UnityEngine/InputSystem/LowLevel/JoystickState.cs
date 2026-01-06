using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C9 RID: 201
	internal struct JoystickState : IInputStateTypeInfo
	{
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000CD3 RID: 3283 RVA: 0x00041E0D File Offset: 0x0004000D
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('J', 'O', 'Y', ' ');
			}
		}

		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06000CD4 RID: 3284 RVA: 0x00041E1C File Offset: 0x0004001C
		public FourCC format
		{
			get
			{
				return JoystickState.kFormat;
			}
		}

		// Token: 0x0400050C RID: 1292
		[InputControl(name = "trigger", displayName = "Trigger", layout = "Button", usages = new string[] { "PrimaryTrigger", "PrimaryAction", "Submit" }, bit = 4U)]
		public int buttons;

		// Token: 0x0400050D RID: 1293
		[InputControl(displayName = "Stick", layout = "Stick", usage = "Primary2DMotion", processors = "stickDeadzone")]
		public Vector2 stick;

		// Token: 0x02000201 RID: 513
		public enum Button
		{
			// Token: 0x04000B16 RID: 2838
			HatSwitchUp,
			// Token: 0x04000B17 RID: 2839
			HatSwitchDown,
			// Token: 0x04000B18 RID: 2840
			HatSwitchLeft,
			// Token: 0x04000B19 RID: 2841
			HatSwitchRight,
			// Token: 0x04000B1A RID: 2842
			Trigger
		}
	}
}
