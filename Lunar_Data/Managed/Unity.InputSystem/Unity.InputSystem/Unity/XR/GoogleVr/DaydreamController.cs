using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.XR;

namespace Unity.XR.GoogleVr
{
	// Token: 0x02000011 RID: 17
	[InputControlLayout(displayName = "Daydream Controller", commonUsages = new string[] { "LeftHand", "RightHand" }, hideInUI = true)]
	public class DaydreamController : XRController
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00002E00 File Offset: 0x00001000
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00002E08 File Offset: 0x00001008
		[InputControl]
		public Vector2Control touchpad { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00002E11 File Offset: 0x00001011
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00002E19 File Offset: 0x00001019
		[InputControl]
		public ButtonControl volumeUp { get; private set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00002E22 File Offset: 0x00001022
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00002E2A File Offset: 0x0000102A
		[InputControl]
		public ButtonControl recentered { get; private set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00002E33 File Offset: 0x00001033
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00002E3B File Offset: 0x0000103B
		[InputControl]
		public ButtonControl volumeDown { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00002E44 File Offset: 0x00001044
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00002E4C File Offset: 0x0000104C
		[InputControl]
		public ButtonControl recentering { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002E55 File Offset: 0x00001055
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002E5D File Offset: 0x0000105D
		[InputControl]
		public ButtonControl app { get; private set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00002E66 File Offset: 0x00001066
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00002E6E File Offset: 0x0000106E
		[InputControl]
		public ButtonControl home { get; private set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00002E77 File Offset: 0x00001077
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00002E7F File Offset: 0x0000107F
		[InputControl]
		public ButtonControl touchpadClicked { get; private set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00002E88 File Offset: 0x00001088
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00002E90 File Offset: 0x00001090
		[InputControl]
		public ButtonControl touchpadTouched { get; private set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00002E99 File Offset: 0x00001099
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00002EA1 File Offset: 0x000010A1
		[InputControl(noisy = true)]
		public Vector3Control deviceVelocity { get; private set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00002EAA File Offset: 0x000010AA
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00002EB2 File Offset: 0x000010B2
		[InputControl(noisy = true)]
		public Vector3Control deviceAcceleration { get; private set; }

		// Token: 0x060000EC RID: 236 RVA: 0x00002EBC File Offset: 0x000010BC
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpad = base.GetChildControl<Vector2Control>("touchpad");
			this.volumeUp = base.GetChildControl<ButtonControl>("volumeUp");
			this.recentered = base.GetChildControl<ButtonControl>("recentered");
			this.volumeDown = base.GetChildControl<ButtonControl>("volumeDown");
			this.recentering = base.GetChildControl<ButtonControl>("recentering");
			this.app = base.GetChildControl<ButtonControl>("app");
			this.home = base.GetChildControl<ButtonControl>("home");
			this.touchpadClicked = base.GetChildControl<ButtonControl>("touchpadClicked");
			this.touchpadTouched = base.GetChildControl<ButtonControl>("touchpadTouched");
			this.deviceVelocity = base.GetChildControl<Vector3Control>("deviceVelocity");
			this.deviceAcceleration = base.GetChildControl<Vector3Control>("deviceAcceleration");
		}
	}
}
