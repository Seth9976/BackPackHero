using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000A RID: 10
	[InputControlLayout(displayName = "Oculus Headset", hideInUI = true)]
	public class OculusHMD : XRHMD
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002756 File Offset: 0x00000956
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000275E File Offset: 0x0000095E
		[InputControl]
		[InputControl(name = "trackingState", layout = "Integer", aliases = new string[] { "devicetrackingstate" })]
		[InputControl(name = "isTracked", layout = "Button", aliases = new string[] { "deviceistracked" })]
		public ButtonControl userPresence { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002767 File Offset: 0x00000967
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000276F File Offset: 0x0000096F
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularVelocity { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00002780 File Offset: 0x00000980
		[InputControl(noisy = true)]
		public Vector3Control deviceAcceleration { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002789 File Offset: 0x00000989
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00002791 File Offset: 0x00000991
		[InputControl(noisy = true)]
		public Vector3Control deviceAngularAcceleration { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000279A File Offset: 0x0000099A
		// (set) Token: 0x06000076 RID: 118 RVA: 0x000027A2 File Offset: 0x000009A2
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAngularVelocity { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000027AB File Offset: 0x000009AB
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000027B3 File Offset: 0x000009B3
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAcceleration { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000027BC File Offset: 0x000009BC
		// (set) Token: 0x0600007A RID: 122 RVA: 0x000027C4 File Offset: 0x000009C4
		[InputControl(noisy = true)]
		public Vector3Control leftEyeAngularAcceleration { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000027CD File Offset: 0x000009CD
		// (set) Token: 0x0600007C RID: 124 RVA: 0x000027D5 File Offset: 0x000009D5
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAngularVelocity { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000027DE File Offset: 0x000009DE
		// (set) Token: 0x0600007E RID: 126 RVA: 0x000027E6 File Offset: 0x000009E6
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAcceleration { get; private set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000027EF File Offset: 0x000009EF
		// (set) Token: 0x06000080 RID: 128 RVA: 0x000027F7 File Offset: 0x000009F7
		[InputControl(noisy = true)]
		public Vector3Control rightEyeAngularAcceleration { get; private set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00002800 File Offset: 0x00000A00
		// (set) Token: 0x06000082 RID: 130 RVA: 0x00002808 File Offset: 0x00000A08
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAngularVelocity { get; private set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00002811 File Offset: 0x00000A11
		// (set) Token: 0x06000084 RID: 132 RVA: 0x00002819 File Offset: 0x00000A19
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAcceleration { get; private set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00002822 File Offset: 0x00000A22
		// (set) Token: 0x06000086 RID: 134 RVA: 0x0000282A File Offset: 0x00000A2A
		[InputControl(noisy = true)]
		public Vector3Control centerEyeAngularAcceleration { get; private set; }

		// Token: 0x06000087 RID: 135 RVA: 0x00002834 File Offset: 0x00000A34
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.userPresence = base.GetChildControl<ButtonControl>("userPresence");
			this.deviceAngularVelocity = base.GetChildControl<Vector3Control>("deviceAngularVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
			this.deviceAngularAcceleration = base.GetChildControl<Vector3Control>("deviceAngularAcceleration");
			this.leftEyeAngularVelocity = base.GetChildControl<Vector3Control>("leftEyeAngularVelocity");
			this.leftEyeAcceleration = base.GetChildControl<Vector3Control>("leftEyeAcceleration");
			this.leftEyeAngularAcceleration = base.GetChildControl<Vector3Control>("leftEyeAngularAcceleration");
			this.rightEyeAngularVelocity = base.GetChildControl<Vector3Control>("rightEyeAngularVelocity");
			this.rightEyeAcceleration = base.GetChildControl<Vector3Control>("rightEyeAcceleration");
			this.rightEyeAngularAcceleration = base.GetChildControl<Vector3Control>("rightEyeAngularAcceleration");
			this.centerEyeAngularVelocity = base.GetChildControl<Vector3Control>("centerEyeAngularVelocity");
			this.centerEyeAcceleration = base.GetChildControl<Vector3Control>("centerEyeAcceleration");
			this.centerEyeAngularAcceleration = base.GetChildControl<Vector3Control>("centerEyeAngularAcceleration");
		}
	}
}
