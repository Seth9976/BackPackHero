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
		// Token: 0x17000345 RID: 837
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000410AE File Offset: 0x0003F2AE
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x000410B6 File Offset: 0x0003F2B6
		public ButtonControl leftTriggerButton { get; private set; }

		// Token: 0x17000346 RID: 838
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000410BF File Offset: 0x0003F2BF
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x000410C7 File Offset: 0x0003F2C7
		public ButtonControl rightTriggerButton { get; private set; }

		// Token: 0x17000347 RID: 839
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x000410D0 File Offset: 0x0003F2D0
		// (set) Token: 0x06000C59 RID: 3161 RVA: 0x000410D8 File Offset: 0x0003F2D8
		public ButtonControl playStationButton { get; private set; }

		// Token: 0x06000C5A RID: 3162 RVA: 0x000410E1 File Offset: 0x0003F2E1
		protected override void FinishSetup()
		{
			this.leftTriggerButton = base.GetChildControl<ButtonControl>("leftTriggerButton");
			this.rightTriggerButton = base.GetChildControl<ButtonControl>("rightTriggerButton");
			this.playStationButton = base.GetChildControl<ButtonControl>("systemButton");
			base.FinishSetup();
		}
	}
}
