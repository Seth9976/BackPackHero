using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.DualShock.LowLevel;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x0200009E RID: 158
	[InputControlLayout(stateType = typeof(DualShock3HIDInputReport), hideInUI = true, displayName = "PS3 Controller")]
	public class DualShock3GamepadHID : DualShockGamepad
	{
		// Token: 0x17000343 RID: 835
		// (get) Token: 0x06000C51 RID: 3153 RVA: 0x00041066 File Offset: 0x0003F266
		// (set) Token: 0x06000C52 RID: 3154 RVA: 0x0004106E File Offset: 0x0003F26E
		public ButtonControl leftTriggerButton { get; private set; }

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00041077 File Offset: 0x0003F277
		// (set) Token: 0x06000C54 RID: 3156 RVA: 0x0004107F File Offset: 0x0003F27F
		public ButtonControl rightTriggerButton { get; private set; }

		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00041088 File Offset: 0x0003F288
		// (set) Token: 0x06000C56 RID: 3158 RVA: 0x00041090 File Offset: 0x0003F290
		public ButtonControl playStationButton { get; private set; }

		// Token: 0x06000C57 RID: 3159 RVA: 0x00041099 File Offset: 0x0003F299
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}
	}
}
