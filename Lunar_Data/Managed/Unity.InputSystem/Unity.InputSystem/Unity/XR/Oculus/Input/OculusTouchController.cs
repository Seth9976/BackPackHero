using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000B RID: 11
	[InputControlLayout(displayName = "Oculus Touch Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class OculusTouchController : XRControllerWithRumble
	{
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000292C File Offset: 0x00000B2C
		// (set) Token: 0x0600008A RID: 138 RVA: 0x00002934 File Offset: 0x00000B34
		[InputControl(aliases = new string[] { "Primary2DAxis", "Joystick" })]
		public Vector2Control thumbstick { get; private set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000293D File Offset: 0x00000B3D
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00002945 File Offset: 0x00000B45
		[InputControl]
		public AxisControl trigger { get; private set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000294E File Offset: 0x00000B4E
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00002956 File Offset: 0x00000B56
		[InputControl]
		public AxisControl grip { get; private set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000295F File Offset: 0x00000B5F
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002967 File Offset: 0x00000B67
		[InputControl(aliases = new string[] { "A", "X", "Alternate" })]
		public ButtonControl primaryButton { get; private set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002970 File Offset: 0x00000B70
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002978 File Offset: 0x00000B78
		[InputControl(aliases = new string[] { "B", "Y", "Primary" })]
		public ButtonControl secondaryButton { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002981 File Offset: 0x00000B81
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00002989 File Offset: 0x00000B89
		[InputControl(aliases = new string[] { "GripButton" })]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00002992 File Offset: 0x00000B92
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000299A File Offset: 0x00000B9A
		[InputControl]
		public ButtonControl start { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000029A3 File Offset: 0x00000BA3
		// (set) Token: 0x06000098 RID: 152 RVA: 0x000029AB File Offset: 0x00000BAB
		[InputControl(aliases = new string[] { "JoystickOrPadPressed", "thumbstickClick" })]
		public ButtonControl thumbstickClicked { get; private set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000029B4 File Offset: 0x00000BB4
		// (set) Token: 0x0600009A RID: 154 RVA: 0x000029BC File Offset: 0x00000BBC
		[InputControl(aliases = new string[] { "ATouched", "XTouched", "ATouch", "XTouch" })]
		public ButtonControl primaryTouched { get; private set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000029CD File Offset: 0x00000BCD
		[InputControl(aliases = new string[] { "BTouched", "YTouched", "BTouch", "YTouch" })]
		public ButtonControl secondaryTouched { get; private set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000029D6 File Offset: 0x00000BD6
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000029DE File Offset: 0x00000BDE
		[InputControl(aliases = new string[] { "indexTouch", "indexNearTouched" })]
		public AxisControl triggerTouched { get; private set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000029E7 File Offset: 0x00000BE7
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000029EF File Offset: 0x00000BEF
		[InputControl(aliases = new string[] { "indexButton", "indexTouched" })]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000029F8 File Offset: 0x00000BF8
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x00002A00 File Offset: 0x00000C00
		[InputControl(aliases = new string[] { "JoystickOrPadTouched", "thumbstickTouch" })]
		[InputControl(name = "trackingState", layout = "Integer", aliases = new string[] { "controllerTrackingState" })]
		[InputControl(name = "isTracked", layout = "Button", aliases = new string[] { "ControllerIsTracked" })]
		[InputControl(name = "devicePosition", layout = "Vector3", aliases = new string[] { "controllerPosition" })]
		[InputControl(name = "deviceRotation", layout = "Quaternion", aliases = new string[] { "controllerRotation" })]
		public ButtonControl thumbstickTouched { get; private set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002A09 File Offset: 0x00000C09
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00002A11 File Offset: 0x00000C11
		[InputControl(noisy = true, aliases = new string[] { "controllerVelocity" })]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002A1A File Offset: 0x00000C1A
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00002A22 File Offset: 0x00000C22
		[InputControl(noisy = true, aliases = new string[] { "controllerAngularVelocity" })]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002A2B File Offset: 0x00000C2B
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002A33 File Offset: 0x00000C33
		[InputControl(noisy = true, aliases = new string[] { "controllerAcceleration" })]
		public Vector3Control deviceAcceleration { get; private set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002A3C File Offset: 0x00000C3C
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002A44 File Offset: 0x00000C44
		[InputControl(noisy = true, aliases = new string[] { "controllerAngularAcceleration" })]
		public Vector3Control deviceAngularAcceleration { get; private set; }

		// Token: 0x060000AB RID: 171 RVA: 0x00002A50 File Offset: 0x00000C50
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.thumbstick = base.GetChildControl<Vector2Control>("thumbstick");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.triggerTouched = base.GetChildControl<AxisControl>("triggerTouched");
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.primaryButton = base.GetChildControl<ButtonControl>("primaryButton");
			this.secondaryButton = base.GetChildControl<ButtonControl>("secondaryButton");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.start = base.GetChildControl<ButtonControl>("start");
			this.thumbstickClicked = base.GetChildControl<ButtonControl>("thumbstickClicked");
			this.primaryTouched = base.GetChildControl<ButtonControl>("primaryTouched");
			this.secondaryTouched = base.GetChildControl<ButtonControl>("secondaryTouched");
			this.thumbstickTouched = base.GetChildControl<ButtonControl>("thumbstickTouched");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
			this.deviceAngularAcceleration = base.GetChildControl<Vector3Control>("deviceAngularAcceleration");
		}
	}
}
