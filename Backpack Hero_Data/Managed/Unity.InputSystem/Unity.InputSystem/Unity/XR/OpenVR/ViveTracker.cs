using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000007 RID: 7
	[InputControlLayout(displayName = "Vive Tracker")]
	public class ViveTracker : TrackedDevice
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000024A9 File Offset: 0x000006A9
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000024B1 File Offset: 0x000006B1
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024BA File Offset: 0x000006BA
		// (set) Token: 0x06000046 RID: 70 RVA: 0x000024C2 File Offset: 0x000006C2
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x06000047 RID: 71 RVA: 0x000024CB File Offset: 0x000006CB
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
		}
	}
}
