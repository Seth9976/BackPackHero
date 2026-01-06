using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000004 RID: 4
	[InputControlLayout(displayName = "Windows MR Controller (OpenVR)", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OpenVRControllerWMR : XRController
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000219F File Offset: 0x0000039F
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000021A7 File Offset: 0x000003A7
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000021B0 File Offset: 0x000003B0
		// (set) Token: 0x06000017 RID: 23 RVA: 0x000021B8 File Offset: 0x000003B8
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000021C1 File Offset: 0x000003C1
		// (set) Token: 0x06000019 RID: 25 RVA: 0x000021C9 File Offset: 0x000003C9
		[InputControl(aliases = new string[] { "primary2DAxisClick", "joystickOrPadPressed" })]
		public ButtonControl touchpadClick { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000021D2 File Offset: 0x000003D2
		// (set) Token: 0x0600001B RID: 27 RVA: 0x000021DA File Offset: 0x000003DA
		[InputControl(aliases = new string[] { "primary2DAxisTouch", "joystickOrPadTouched" })]
		public ButtonControl touchpadTouch { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000021E3 File Offset: 0x000003E3
		// (set) Token: 0x0600001D RID: 29 RVA: 0x000021EB File Offset: 0x000003EB
		[InputControl]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001E RID: 30 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x0600001F RID: 31 RVA: 0x000021FC File Offset: 0x000003FC
		[InputControl]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002205 File Offset: 0x00000405
		// (set) Token: 0x06000021 RID: 33 RVA: 0x0000220D File Offset: 0x0000040D
		[InputControl(aliases = new string[] { "primary" })]
		public ButtonControl menu { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002216 File Offset: 0x00000416
		// (set) Token: 0x06000023 RID: 35 RVA: 0x0000221E File Offset: 0x0000041E
		[InputControl]
		public AxisControl trigger { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002227 File Offset: 0x00000427
		// (set) Token: 0x06000025 RID: 37 RVA: 0x0000222F File Offset: 0x0000042F
		[InputControl]
		public AxisControl grip { get; private set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002238 File Offset: 0x00000438
		// (set) Token: 0x06000027 RID: 39 RVA: 0x00002240 File Offset: 0x00000440
		[InputControl(aliases = new string[] { "secondary2DAxis" })]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002249 File Offset: 0x00000449
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002251 File Offset: 0x00000451
		[InputControl(aliases = new string[] { "primary2DAxis" })]
		public Vector2Control joystick { get; private set; }

		// Token: 0x0600002A RID: 42 RVA: 0x0000225C File Offset: 0x0000045C
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.touchpadClick = base.GetChildControl<ButtonControl>("touchpadClick");
			this.touchpadTouch = base.GetChildControl<ButtonControl>("touchpadTouch");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.menu = base.GetChildControl<ButtonControl>("menu");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
			this.joystick = base.GetChildControl<Vector2Control>("joystick");
		}
	}
}
