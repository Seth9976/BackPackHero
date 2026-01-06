using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.LowLevel
{
	// Token: 0x020000C9 RID: 201
	internal struct JoystickState : IInputStateTypeInfo
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x00041E55 File Offset: 0x00040055
		public static FourCC kFormat
		{
			get
			{
				return new FourCC('J', 'O', 'Y', ' ');
			}
		}

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x00041E64 File Offset: 0x00040064
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
			// Token: 0x04000B17 RID: 2839
			HatSwitchUp,
			// Token: 0x04000B18 RID: 2840
			HatSwitchDown,
			// Token: 0x04000B19 RID: 2841
			HatSwitchLeft,
			// Token: 0x04000B1A RID: 2842
			HatSwitchRight,
			// Token: 0x04000B1B RID: 2843
			Trigger
		}
	}
}
