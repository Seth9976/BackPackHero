using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000F RID: 15
	[InputControlLayout(displayName = "GearVR Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class GearVRTrackedController : XRController
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002CAA File Offset: 0x00000EAA
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002CB2 File Offset: 0x00000EB2
		[InputControl]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002CBB File Offset: 0x00000EBB
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002CC3 File Offset: 0x00000EC3
		[InputControl]
		public AxisControl trigger { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002CCC File Offset: 0x00000ECC
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002CD4 File Offset: 0x00000ED4
		[InputControl]
		public ButtonControl back { get; private set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002CDD File Offset: 0x00000EDD
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002CE5 File Offset: 0x00000EE5
		[InputControl]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002CEE File Offset: 0x00000EEE
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002CF6 File Offset: 0x00000EF6
		[InputControl]
		public ButtonControl touchpadClicked { get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002CFF File Offset: 0x00000EFF
		// (set) Token: 0x060000CC RID: 204 RVA: 0x00002D07 File Offset: 0x00000F07
		[InputControl]
		public ButtonControl touchpadTouched { get; private set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002D10 File Offset: 0x00000F10
		// (set) Token: 0x060000CE RID: 206 RVA: 0x00002D18 File Offset: 0x00000F18
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002D21 File Offset: 0x00000F21
		// (set) Token: 0x060000D0 RID: 208 RVA: 0x00002D29 File Offset: 0x00000F29
		[InputControl(noisy = true)]
		public Vector3Control deviceAcceleration { get; private set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002D32 File Offset: 0x00000F32
		// (set) Token: 0x060000D2 RID: 210 RVA: 0x00002D3A File Offset: 0x00000F3A
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularAcceleration { get; private set; }

		// Token: 0x060000D3 RID: 211 RVA: 0x00002D44 File Offset: 0x00000F44
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
			this.trigger = base.GetChildControl<AxisControl>("trigger");
			this.back = base.GetChildControl<ButtonControl>("back");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			this.touchpadClicked = base.GetChildControl<ButtonControl>("touchpadClicked");
			this.touchpadTouched = base.GetChildControl<ButtonControl>("touchpadTouched");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
			this.deviceAngularAcceleration = base.GetChildControl<Vector3Control>("deviceAngularAcceleration");
		}
	}
}
