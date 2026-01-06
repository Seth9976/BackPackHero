using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000062 RID: 98
	[InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR HMD", canRunInBackground = true)]
	public class XRHMD : TrackedDevice
	{
		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00034E13 File Offset: 0x00033013
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00034E1B File Offset: 0x0003301B
		[InputControl(noisy = true)]
		public Vector3Control leftEyePosition { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00034E24 File Offset: 0x00033024
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00034E2C File Offset: 0x0003302C
		[InputControl(noisy = true)]
		public QuaternionControl leftEyeRotation { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00034E35 File Offset: 0x00033035
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00034E3D File Offset: 0x0003303D
		[InputControl(noisy = true)]
		public Vector3Control rightEyePosition { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00034E46 File Offset: 0x00033046
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00034E4E File Offset: 0x0003304E
		[InputControl(noisy = true)]
		public QuaternionControl rightEyeRotation { get; private set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00034E57 File Offset: 0x00033057
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00034E5F File Offset: 0x0003305F
		[InputControl(noisy = true)]
		public Vector3Control centerEyePosition { get; private set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x0600098C RID: 2444 RVA: 0x00034E68 File Offset: 0x00033068
		// (set) Token: 0x0600098D RID: 2445 RVA: 0x00034E70 File Offset: 0x00033070
		[InputControl(noisy = true)]
		public QuaternionControl centerEyeRotation { get; private set; }

		// Token: 0x0600098E RID: 2446 RVA: 0x00034E7C File Offset: 0x0003307C
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.centerEyePosition = base.GetChildControl<Vector3Control>("centerEyePosition");
			this.centerEyeRotation = base.GetChildControl<QuaternionControl>("centerEyeRotation");
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
		}
	}
}
