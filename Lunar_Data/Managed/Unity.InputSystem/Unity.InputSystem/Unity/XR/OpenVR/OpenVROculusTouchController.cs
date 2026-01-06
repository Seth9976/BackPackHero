using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000009 RID: 9
	[InputControlLayout(displayName = "Oculus Touch Controller (OpenVR)", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OpenVROculusTouchController : XRControllerWithRumble
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000025C4 File Offset: 0x000007C4
		// (set) Token: 0x06000056 RID: 86 RVA: 0x000025CC File Offset: 0x000007CC
		[InputControl]
		public Vector2Control thumbstick { get; private set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000025D5 File Offset: 0x000007D5
		// (set) Token: 0x06000058 RID: 88 RVA: 0x000025DD File Offset: 0x000007DD
		[InputControl]
		public AxisControl trigger { get; private set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000025E6 File Offset: 0x000007E6
		// (set) Token: 0x0600005A RID: 90 RVA: 0x000025EE File Offset: 0x000007EE
		[InputControl]
		public AxisControl grip { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025F7 File Offset: 0x000007F7
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000025FF File Offset: 0x000007FF
		[InputControl(aliases = new string[] { "Alternate" })]
		public ButtonControl primaryButton { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002608 File Offset: 0x00000808
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002610 File Offset: 0x00000810
		[InputControl(aliases = new string[] { "Primary" })]
		public ButtonControl secondaryButton { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002619 File Offset: 0x00000819
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002621 File Offset: 0x00000821
		[InputControl]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000262A File Offset: 0x0000082A
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00002632 File Offset: 0x00000832
		[InputControl]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000263B File Offset: 0x0000083B
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00002643 File Offset: 0x00000843
		[InputControl(aliases = new string[] { "primary2DAxisClicked" })]
		public ButtonControl thumbstickClicked { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000264C File Offset: 0x0000084C
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002654 File Offset: 0x00000854
		[InputControl(aliases = new string[] { "primary2DAxisTouch" })]
		public ButtonControl thumbstickTouched { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000265D File Offset: 0x0000085D
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00002665 File Offset: 0x00000865
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000266E File Offset: 0x0000086E
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00002676 File Offset: 0x00000876
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x0600006B RID: 107 RVA: 0x00002680 File Offset: 0x00000880
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.thumbstick = base.GetChildControl<Vector2Control>("thumbstick");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.primaryButton = base.GetChildControl<ButtonControl>("primaryButton");
			this.secondaryButton = base.GetChildControl<ButtonControl>("secondaryButton");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.thumbstickClicked = base.GetChildControl<ButtonControl>("thumbstickClicked");
			this.thumbstickTouched = base.GetChildControl<ButtonControl>("thumbstickTouched");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
		}
	}
}
