using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.DualShock
{
	// Token: 0x0200009B RID: 155
	[InputControlLayout(displayName = "PlayStation Controller")]
	public class DualShockGamepad : Gamepad, IDualShockHaptics, IDualMotorRumble, IHaptics
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x0004036C File Offset: 0x0003E56C
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x00040374 File Offset: 0x0003E574
		[InputControl(name = "buttonWest", displayName = "Square", shortDisplayName = "Square")]
		[InputControl(name = "buttonNorth", displayName = "Triangle", shortDisplayName = "Triangle")]
		[InputControl(name = "buttonEast", displayName = "Circle", shortDisplayName = "Circle")]
		[InputControl(name = "buttonSouth", displayName = "Cross", shortDisplayName = "Cross")]
		[InputControl]
		public ButtonControl touchpadButton { get; protected set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0004037D File Offset: 0x0003E57D
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x00040385 File Offset: 0x0003E585
		[InputControl(name = "start", displayName = "Options")]
		public ButtonControl optionsButton { get; protected set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0004038E File Offset: 0x0003E58E
		// (set) Token: 0x06000C15 RID: 3093 RVA: 0x00040396 File Offset: 0x0003E596
		[InputControl(name = "select", displayName = "Share")]
		public ButtonControl shareButton { get; protected set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C16 RID: 3094 RVA: 0x0004039F File Offset: 0x0003E59F
		// (set) Token: 0x06000C17 RID: 3095 RVA: 0x000403A7 File Offset: 0x0003E5A7
		[InputControl(name = "leftShoulder", displayName = "L1", shortDisplayName = "L1")]
		public ButtonControl L1 { get; protected set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C18 RID: 3096 RVA: 0x000403B0 File Offset: 0x0003E5B0
		// (set) Token: 0x06000C19 RID: 3097 RVA: 0x000403B8 File Offset: 0x0003E5B8
		[InputControl(name = "rightShoulder", displayName = "R1", shortDisplayName = "R1")]
		public ButtonControl R1 { get; protected set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000403C1 File Offset: 0x0003E5C1
		// (set) Token: 0x06000C1B RID: 3099 RVA: 0x000403C9 File Offset: 0x0003E5C9
		[InputControl(name = "leftTrigger", displayName = "L2", shortDisplayName = "L2")]
		public ButtonControl L2 { get; protected set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000403D2 File Offset: 0x0003E5D2
		// (set) Token: 0x06000C1D RID: 3101 RVA: 0x000403DA File Offset: 0x0003E5DA
		[InputControl(name = "rightTrigger", displayName = "R2", shortDisplayName = "R2")]
		public ButtonControl R2 { get; protected set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C1E RID: 3102 RVA: 0x000403E3 File Offset: 0x0003E5E3
		// (set) Token: 0x06000C1F RID: 3103 RVA: 0x000403EB File Offset: 0x0003E5EB
		[InputControl(name = "leftStickPress", displayName = "L3", shortDisplayName = "L3")]
		public ButtonControl L3 { get; protected set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x000403F4 File Offset: 0x0003E5F4
		// (set) Token: 0x06000C21 RID: 3105 RVA: 0x000403FC File Offset: 0x0003E5FC
		[InputControl(name = "rightStickPress", displayName = "R3", shortDisplayName = "R3")]
		public ButtonControl R3 { get; protected set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x00040405 File Offset: 0x0003E605
		// (set) Token: 0x06000C23 RID: 3107 RVA: 0x0004040C File Offset: 0x0003E60C
		public new static DualShockGamepad current { get; private set; }

		// Token: 0x06000C24 RID: 3108 RVA: 0x00040414 File Offset: 0x0003E614
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			DualShockGamepad.current = this;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00040422 File Offset: 0x0003E622
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (DualShockGamepad.current == this)
			{
				DualShockGamepad.current = null;
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00040438 File Offset: 0x0003E638
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.touchpadButton = base.GetChildControl<ButtonControl>("touchpadButton");
			this.optionsButton = base.startButton;
			this.shareButton = base.selectButton;
			this.L1 = base.leftShoulder;
			this.R1 = base.rightShoulder;
			this.L2 = base.leftTrigger;
			this.R2 = base.rightTrigger;
			this.L3 = base.leftStickButton;
			this.R3 = base.rightStickButton;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000404BC File Offset: 0x0003E6BC
		public virtual void SetLightBarColor(Color color)
		{
		}
	}
}
