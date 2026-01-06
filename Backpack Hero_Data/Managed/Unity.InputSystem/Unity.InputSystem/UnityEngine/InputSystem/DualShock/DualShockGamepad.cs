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
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x000403B4 File Offset: 0x0003E5B4
		// (set) Token: 0x06000C14 RID: 3092 RVA: 0x000403BC File Offset: 0x0003E5BC
		[InputControl(name = "buttonWest", displayName = "Square", shortDisplayName = "Square")]
		[InputControl(name = "buttonNorth", displayName = "Triangle", shortDisplayName = "Triangle")]
		[InputControl(name = "buttonEast", displayName = "Circle", shortDisplayName = "Circle")]
		[InputControl(name = "buttonSouth", displayName = "Cross", shortDisplayName = "Cross")]
		[InputControl]
		public ButtonControl touchpadButton { get; protected set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000C15 RID: 3093 RVA: 0x000403C5 File Offset: 0x0003E5C5
		// (set) Token: 0x06000C16 RID: 3094 RVA: 0x000403CD File Offset: 0x0003E5CD
		[InputControl(name = "start", displayName = "Options")]
		public ButtonControl optionsButton { get; protected set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x000403D6 File Offset: 0x0003E5D6
		// (set) Token: 0x06000C18 RID: 3096 RVA: 0x000403DE File Offset: 0x0003E5DE
		[InputControl(name = "select", displayName = "Share")]
		public ButtonControl shareButton { get; protected set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x000403E7 File Offset: 0x0003E5E7
		// (set) Token: 0x06000C1A RID: 3098 RVA: 0x000403EF File Offset: 0x0003E5EF
		[InputControl(name = "leftShoulder", displayName = "L1", shortDisplayName = "L1")]
		public ButtonControl L1 { get; protected set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000403F8 File Offset: 0x0003E5F8
		// (set) Token: 0x06000C1C RID: 3100 RVA: 0x00040400 File Offset: 0x0003E600
		[InputControl(name = "rightShoulder", displayName = "R1", shortDisplayName = "R1")]
		public ButtonControl R1 { get; protected set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000C1D RID: 3101 RVA: 0x00040409 File Offset: 0x0003E609
		// (set) Token: 0x06000C1E RID: 3102 RVA: 0x00040411 File Offset: 0x0003E611
		[InputControl(name = "leftTrigger", displayName = "L2", shortDisplayName = "L2")]
		public ButtonControl L2 { get; protected set; }

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000C1F RID: 3103 RVA: 0x0004041A File Offset: 0x0003E61A
		// (set) Token: 0x06000C20 RID: 3104 RVA: 0x00040422 File Offset: 0x0003E622
		[InputControl(name = "rightTrigger", displayName = "R2", shortDisplayName = "R2")]
		public ButtonControl R2 { get; protected set; }

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0004042B File Offset: 0x0003E62B
		// (set) Token: 0x06000C22 RID: 3106 RVA: 0x00040433 File Offset: 0x0003E633
		[InputControl(name = "leftStickPress", displayName = "L3", shortDisplayName = "L3")]
		public ButtonControl L3 { get; protected set; }

		// Token: 0x1700033D RID: 829
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0004043C File Offset: 0x0003E63C
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00040444 File Offset: 0x0003E644
		[InputControl(name = "rightStickPress", displayName = "R3", shortDisplayName = "R3")]
		public ButtonControl R3 { get; protected set; }

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0004044D File Offset: 0x0003E64D
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00040454 File Offset: 0x0003E654
		public new static DualShockGamepad current { get; private set; }

		// Token: 0x06000C27 RID: 3111 RVA: 0x0004045C File Offset: 0x0003E65C
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			DualShockGamepad.current = this;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x0004046A File Offset: 0x0003E66A
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (DualShockGamepad.current == this)
			{
				DualShockGamepad.current = null;
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00040480 File Offset: 0x0003E680
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

		// Token: 0x06000C2A RID: 3114 RVA: 0x00040504 File Offset: 0x0003E704
		public virtual void SetLightBarColor(Color color)
		{
		}
	}
}
