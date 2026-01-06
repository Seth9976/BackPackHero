using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000054 RID: 84
	[InputControlLayout(displayName = "Tracked Device", isGenericTypeOfDevice = true)]
	public class TrackedDevice : InputDevice
	{
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002D532 File Offset: 0x0002B732
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0002D53A File Offset: 0x0002B73A
		[InputControl(synthetic = true)]
		public IntegerControl trackingState { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002D543 File Offset: 0x0002B743
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0002D54B File Offset: 0x0002B74B
		[InputControl(synthetic = true)]
		public ButtonControl isTracked { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0002D554 File Offset: 0x0002B754
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0002D55C File Offset: 0x0002B75C
		[InputControl(noisy = true, dontReset = true)]
		public Vector3Control devicePosition { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0002D565 File Offset: 0x0002B765
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0002D56D File Offset: 0x0002B76D
		[InputControl(noisy = true, dontReset = true)]
		public QuaternionControl deviceRotation { get; private set; }

		// Token: 0x06000827 RID: 2087 RVA: 0x0002D578 File Offset: 0x0002B778
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.devicePosition = base.GetChildControl<Vector3Control>("devicePosition");
			this.deviceRotation = base.GetChildControl<QuaternionControl>("deviceRotation");
		}
	}
}
