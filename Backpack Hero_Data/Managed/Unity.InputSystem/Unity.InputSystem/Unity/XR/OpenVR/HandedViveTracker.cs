using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000008 RID: 8
	[InputControlLayout(displayName = "Handed Vive Tracker", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class HandedViveTracker : ViveTracker
	{
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000049 RID: 73 RVA: 0x000024FD File Offset: 0x000006FD
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002505 File Offset: 0x00000705
		[InputControl]
		public AxisControl grip { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600004B RID: 75 RVA: 0x0000250E File Offset: 0x0000070E
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002516 File Offset: 0x00000716
		[InputControl]
		public ButtonControl gripPressed { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000251F File Offset: 0x0000071F
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002527 File Offset: 0x00000727
		[InputControl]
		public ButtonControl primary { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002530 File Offset: 0x00000730
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002538 File Offset: 0x00000738
		[InputControl(aliases = new string[] { "JoystickOrPadPressed" })]
		public ButtonControl trackpadPressed { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002541 File Offset: 0x00000741
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002549 File Offset: 0x00000749
		[InputControl]
		public ButtonControl triggerPressed { get; private set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00002554 File Offset: 0x00000754
		protected override void FinishSetup()
		{
			this.grip = base.GetChildControl<AxisControl>("grip");
			this.primary = base.GetChildControl<ButtonControl>("primary");
			this.gripPressed = base.GetChildControl<ButtonControl>("gripPressed");
			this.trackpadPressed = base.GetChildControl<ButtonControl>("trackpadPressed");
			this.triggerPressed = base.GetChildControl<ButtonControl>("triggerPressed");
			base.FinishSetup();
		}
	}
}
