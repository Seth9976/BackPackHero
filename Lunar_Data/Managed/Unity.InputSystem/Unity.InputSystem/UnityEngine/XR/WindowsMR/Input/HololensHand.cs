using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace UnityEngine.XR.WindowsMR.Input
{
	// Token: 0x02000013 RID: 19
	[InputControlLayout(displayName = "HoloLens Hand", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class HololensHand : XRController
	{
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002FC4 File Offset: 0x000011C4
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002FCC File Offset: 0x000011CC
		[InputControl(noisy = true, aliases = new string[] { "gripVelocity" })]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00002FD5 File Offset: 0x000011D5
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00002FDD File Offset: 0x000011DD
		[InputControl(aliases = new string[] { "triggerbutton" })]
		public ButtonControl airTap { get; private set; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002FE6 File Offset: 0x000011E6
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x00002FEE File Offset: 0x000011EE
		[InputControl(noisy = true)]
		public AxisControl sourceLossRisk { get; private set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002FF7 File Offset: 0x000011F7
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00002FFF File Offset: 0x000011FF
		[InputControl(noisy = true)]
		public Vector3Control sourceLossMitigationDirection { get; private set; }

		// Token: 0x060000FA RID: 250 RVA: 0x00003008 File Offset: 0x00001208
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.airTap = base.GetChildControl<ButtonControl>("airTap");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.sourceLossRisk = base.GetChildControl<AxisControl>("sourceLossRisk");
			this.sourceLossMitigationDirection = base.GetChildControl<Vector3Control>("sourceLossMitigationDirection");
		}
	}
}
