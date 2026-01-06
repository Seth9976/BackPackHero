using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.XR.WindowsMR.Input
{
	// Token: 0x02000014 RID: 20
	[InputControlLayout(displayName = "Windows MR Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class WMRSpatialController : XRControllerWithRumble
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00003067 File Offset: 0x00001267
		// (set) Token: 0x060000FD RID: 253 RVA: 0x0000306F File Offset: 0x0000126F
		[InputControl(aliases = new string[] { "Primary2DAxis", "thumbstickaxes" })]
		public Vector2Control joystick { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003078 File Offset: 0x00001278
		// (set) Token: 0x060000FF RID: 255 RVA: 0x00003080 File Offset: 0x00001280
		[InputControl(aliases = new string[] { "Secondary2DAxis", "touchpadaxes" })]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003089 File Offset: 0x00001289
		// (set) Token: 0x06000101 RID: 257 RVA: 0x00003091 File Offset: 0x00001291
		[InputControl(aliases = new string[] { "gripaxis" })]
		public AxisControl grip { get; private set; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000309A File Offset: 0x0000129A
		// (set) Token: 0x06000103 RID: 259 RVA: 0x000030A2 File Offset: 0x000012A2
		[InputControl(aliases = new string[] { "gripbutton" })]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000104 RID: 260 RVA: 0x000030AB File Offset: 0x000012AB
		// (set) Token: 0x06000105 RID: 261 RVA: 0x000030B3 File Offset: 0x000012B3
		[InputControl(aliases = new string[] { "Primary", "menubutton" })]
		public ButtonControl menu { get; private set; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000030BC File Offset: 0x000012BC
		// (set) Token: 0x06000107 RID: 263 RVA: 0x000030C4 File Offset: 0x000012C4
		[InputControl(aliases = new string[] { "triggeraxis" })]
		public AxisControl trigger { get; private set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000030CD File Offset: 0x000012CD
		// (set) Token: 0x06000109 RID: 265 RVA: 0x000030D5 File Offset: 0x000012D5
		[InputControl(aliases = new string[] { "triggerbutton" })]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600010A RID: 266 RVA: 0x000030DE File Offset: 0x000012DE
		// (set) Token: 0x0600010B RID: 267 RVA: 0x000030E6 File Offset: 0x000012E6
		[InputControl(aliases = new string[] { "thumbstickpressed" })]
		public ButtonControl joystickClicked { get; private set; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000030EF File Offset: 0x000012EF
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000030F7 File Offset: 0x000012F7
		[InputControl(aliases = new string[] { "joystickorpadpressed", "touchpadpressed" })]
		public ButtonControl touchpadClicked { get; private set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00003100 File Offset: 0x00001300
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00003108 File Offset: 0x00001308
		[InputControl(aliases = new string[] { "joystickorpadtouched", "touchpadtouched" })]
		public ButtonControl touchpadTouched { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00003111 File Offset: 0x00001311
		// (set) Token: 0x06000111 RID: 273 RVA: 0x00003119 File Offset: 0x00001319
		[InputControl(noisy = true, aliases = new string[] { "gripVelocity" })]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00003122 File Offset: 0x00001322
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000312A File Offset: 0x0000132A
		[InputControl(noisy = true, aliases = new string[] { "gripAngularVelocity" })]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00003133 File Offset: 0x00001333
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000313B File Offset: 0x0000133B
		[InputControl(noisy = true)]
		public AxisControl batteryLevel { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00003144 File Offset: 0x00001344
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000314C File Offset: 0x0000134C
		[InputControl(noisy = true)]
		public AxisControl sourceLossRisk { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00003155 File Offset: 0x00001355
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000315D File Offset: 0x0000135D
		[InputControl(noisy = true)]
		public Vector3Control sourceLossMitigationDirection { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00003166 File Offset: 0x00001366
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000316E File Offset: 0x0000136E
		[InputControl(noisy = true)]
		public Vector3Control pointerPosition { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00003177 File Offset: 0x00001377
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000317F File Offset: 0x0000137F
		[InputControl(noisy = true, aliases = new string[] { "PointerOrientation" })]
		public QuaternionControl pointerRotation { get; private set; }

		// Token: 0x0600011E RID: 286 RVA: 0x00003188 File Offset: 0x00001388
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.joystick = base.GetChildControl<Vector2Control>("joystick");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.menu = base.GetChildControl<ButtonControl>("menu");
			this.joystickClicked = base.GetChildControl<ButtonControl>("joystickClicked");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.touchpadClicked = base.GetChildControl<ButtonControl>("touchpadClicked");
			this.touchpadTouched = base.GetChildControl<ButtonControl>("touchPadTouched");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.batteryLevel = base.GetChildControl<AxisControl>("batteryLevel");
			this.sourceLossRisk = base.GetChildControl<AxisControl>("sourceLossRisk");
			this.sourceLossMitigationDirection = base.GetChildControl<Vector3Control>("sourceLossMitigationDirection");
			this.pointerPosition = base.GetChildControl<Vector3Control>("pointerPosition");
			this.pointerRotation = base.GetChildControl<QuaternionControl>("pointerRotation");
		}
	}
}
