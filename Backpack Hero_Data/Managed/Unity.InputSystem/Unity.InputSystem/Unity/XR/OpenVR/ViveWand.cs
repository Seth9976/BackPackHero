using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000005 RID: 5
	[InputControlLayout(displayName = "Vive Wand", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class ViveWand : XRControllerWithRumble
	{
		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002332 File Offset: 0x00000532
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000233A File Offset: 0x0000053A
		[InputControl]
		public AxisControl grip { get; private set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002343 File Offset: 0x00000543
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000234B File Offset: 0x0000054B
		[InputControl]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002354 File Offset: 0x00000554
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000235C File Offset: 0x0000055C
		[InputControl]
		public ButtonControl primary { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002365 File Offset: 0x00000565
		// (set) Token: 0x06000033 RID: 51 RVA: 0x0000236D File Offset: 0x0000056D
		[InputControl(aliases = new string[] { "primary2DAxisClick", "joystickOrPadPressed" })]
		public ButtonControl trackpadPressed { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002376 File Offset: 0x00000576
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000237E File Offset: 0x0000057E
		[InputControl(aliases = new string[] { "primary2DAxisTouch", "joystickOrPadTouched" })]
		public ButtonControl trackpadTouched { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002387 File Offset: 0x00000587
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000238F File Offset: 0x0000058F
		[InputControl(aliases = new string[] { "Primary2DAxis" })]
		public Vector2Control trackpad { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002398 File Offset: 0x00000598
		// (set) Token: 0x06000039 RID: 57 RVA: 0x000023A0 File Offset: 0x000005A0
		[InputControl]
		public AxisControl trigger { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000023A9 File Offset: 0x000005A9
		// (set) Token: 0x0600003B RID: 59 RVA: 0x000023B1 File Offset: 0x000005B1
		[InputControl]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003C RID: 60 RVA: 0x000023BA File Offset: 0x000005BA
		// (set) Token: 0x0600003D RID: 61 RVA: 0x000023C2 File Offset: 0x000005C2
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600003E RID: 62 RVA: 0x000023CB File Offset: 0x000005CB
		// (set) Token: 0x0600003F RID: 63 RVA: 0x000023D3 File Offset: 0x000005D3
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x06000040 RID: 64 RVA: 0x000023DC File Offset: 0x000005DC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.primary = base.GetChildControl<ButtonControl>("primary");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.trackpadPressed = base.GetChildControl<ButtonControl>("trackpadPressed");
			this.trackpadTouched = base.GetChildControl<ButtonControl>("trackpadTouched");
			this.trackpad = base.GetChildControl<Vector2Control>("trackpad");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
		}
	}
}
