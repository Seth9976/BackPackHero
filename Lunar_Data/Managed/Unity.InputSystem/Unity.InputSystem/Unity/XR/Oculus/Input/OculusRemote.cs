using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000D RID: 13
	[InputControlLayout(displayName = "Oculus Remote", hideInUI = true)]
	public class OculusRemote : InputDevice
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002BE0 File Offset: 0x00000DE0
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002BE8 File Offset: 0x00000DE8
		[InputControl]
		public ButtonControl back { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002BF1 File Offset: 0x00000DF1
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002BF9 File Offset: 0x00000DF9
		[InputControl]
		public ButtonControl start { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002C02 File Offset: 0x00000E02
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002C0A File Offset: 0x00000E0A
		[InputControl]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x00002C13 File Offset: 0x00000E13
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.back = base.GetChildControl<ButtonControl>("back");
			this.start = base.GetChildControl<ButtonControl>("start");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
		}
	}
}
