using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000E RID: 14
	[InputControlLayout(displayName = "Oculus Headset (w/ on-headset controls)", hideInUI = true)]
	public class OculusHMDExtended : OculusHMD
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002C56 File Offset: 0x00000E56
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00002C5E File Offset: 0x00000E5E
		[InputControl]
		public ButtonControl back { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002C67 File Offset: 0x00000E67
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00002C6F File Offset: 0x00000E6F
		[InputControl]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x060000BF RID: 191 RVA: 0x00002C78 File Offset: 0x00000E78
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.back = base.GetChildControl<ButtonControl>("back");
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
		}
	}
}
