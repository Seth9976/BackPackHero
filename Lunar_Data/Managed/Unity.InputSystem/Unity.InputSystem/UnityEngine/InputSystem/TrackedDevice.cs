using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000054 RID: 84
	[InputControlLayout(displayName = "Tracked Device", isGenericTypeOfDevice = true)]
	public class TrackedDevice : InputDevice
	{
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0002D4F6 File Offset: 0x0002B6F6
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0002D4FE File Offset: 0x0002B6FE
		[InputControl(synthetic = true)]
		public IntegerControl trackingState { get; private set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0002D507 File Offset: 0x0002B707
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0002D50F File Offset: 0x0002B70F
		[InputControl(synthetic = true)]
		public ButtonControl isTracked { get; private set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0002D518 File Offset: 0x0002B718
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0002D520 File Offset: 0x0002B720
		[InputControl(noisy = true, dontReset = true)]
		public Vector3Control devicePosition { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x0002D529 File Offset: 0x0002B729
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x0002D531 File Offset: 0x0002B731
		[InputControl(noisy = true, dontReset = true)]
		public QuaternionControl deviceRotation { get; private set; }

		// Token: 0x06000825 RID: 2085 RVA: 0x0002D53C File Offset: 0x0002B73C
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
