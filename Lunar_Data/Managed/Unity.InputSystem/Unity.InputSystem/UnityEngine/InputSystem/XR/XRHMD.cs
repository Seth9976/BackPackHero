using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000062 RID: 98
	[InputControlLayout(isGenericTypeOfDevice = true, displayName = "XR HMD", canRunInBackground = true)]
	public class XRHMD : TrackedDevice
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000980 RID: 2432 RVA: 0x00034DD7 File Offset: 0x00032FD7
		// (set) Token: 0x06000981 RID: 2433 RVA: 0x00034DDF File Offset: 0x00032FDF
		[InputControl(noisy = true)]
		public Vector3Control leftEyePosition { get; private set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x00034DE8 File Offset: 0x00032FE8
		// (set) Token: 0x06000983 RID: 2435 RVA: 0x00034DF0 File Offset: 0x00032FF0
		[InputControl(noisy = true)]
		public QuaternionControl leftEyeRotation { get; private set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000984 RID: 2436 RVA: 0x00034DF9 File Offset: 0x00032FF9
		// (set) Token: 0x06000985 RID: 2437 RVA: 0x00034E01 File Offset: 0x00033001
		[InputControl(noisy = true)]
		public Vector3Control rightEyePosition { get; private set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x00034E0A File Offset: 0x0003300A
		// (set) Token: 0x06000987 RID: 2439 RVA: 0x00034E12 File Offset: 0x00033012
		[InputControl(noisy = true)]
		public QuaternionControl rightEyeRotation { get; private set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00034E1B File Offset: 0x0003301B
		// (set) Token: 0x06000989 RID: 2441 RVA: 0x00034E23 File Offset: 0x00033023
		[InputControl(noisy = true)]
		public Vector3Control centerEyePosition { get; private set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x00034E2C File Offset: 0x0003302C
		// (set) Token: 0x0600098B RID: 2443 RVA: 0x00034E34 File Offset: 0x00033034
		[InputControl(noisy = true)]
		public QuaternionControl centerEyeRotation { get; private set; }

		// Token: 0x0600098C RID: 2444 RVA: 0x00034E40 File Offset: 0x00033040
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
