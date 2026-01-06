using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.OpenVR
{
	// Token: 0x02000003 RID: 3
	[InputControlLayout(displayName = "OpenVR Headset", hideInUI = true)]
	public class OpenVRHMD : XRHMD
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002071 File Offset: 0x00000271
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002079 File Offset: 0x00000279
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002082 File Offset: 0x00000282
		// (set) Token: 0x06000005 RID: 5 RVA: 0x0000208A File Offset: 0x0000028A
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002093 File Offset: 0x00000293
		// (set) Token: 0x06000007 RID: 7 RVA: 0x0000209B File Offset: 0x0000029B
		[InputControl(noisy = true)]
		public Vector3Control leftEyeVelocity { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020A4 File Offset: 0x000002A4
		// (set) Token: 0x06000009 RID: 9 RVA: 0x000020AC File Offset: 0x000002AC
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAngularVelocity { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020B5 File Offset: 0x000002B5
		// (set) Token: 0x0600000B RID: 11 RVA: 0x000020BD File Offset: 0x000002BD
		[InputControl(noisy = true)]
		public Vector3Control rightEyeVelocity { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020C6 File Offset: 0x000002C6
		// (set) Token: 0x0600000D RID: 13 RVA: 0x000020CE File Offset: 0x000002CE
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAngularVelocity { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020D7 File Offset: 0x000002D7
		// (set) Token: 0x0600000F RID: 15 RVA: 0x000020DF File Offset: 0x000002DF
		[InputControl(noisy = true)]
		public Vector3Control centerEyeVelocity { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020E8 File Offset: 0x000002E8
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000020F0 File Offset: 0x000002F0
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAngularVelocity { get; private set; }

		// Token: 0x06000012 RID: 18 RVA: 0x000020FC File Offset: 0x000002FC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.leftEyeVelocity = base.GetChildControl<Vector3Control>("leftEyeVelocity");
			this.leftEyeAngularVelocity = base.GetChildControl<Vector3Control>("leftEyeAngularVelocity");
			this.rightEyeVelocity = base.GetChildControl<Vector3Control>("rightEyeVelocity");
			this.rightEyeAngularVelocity = base.GetChildControl<Vector3Control>("rightEyeAngularVelocity");
			this.centerEyeVelocity = base.GetChildControl<Vector3Control>("centerEyeVelocity");
			this.centerEyeAngularVelocity = base.GetChildControl<Vector3Control>("centerEyeAngularVelocity");
		}
	}
}
