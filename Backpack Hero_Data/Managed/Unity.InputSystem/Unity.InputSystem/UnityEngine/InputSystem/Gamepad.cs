using System;
using System.ComponentModel;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Haptics;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000036 RID: 54
	[InputControlLayout(stateType = typeof(GamepadState), isGenericTypeOfDevice = true)]
	public class Gamepad : InputDevice, IDualMotorRumble, IHaptics
	{
		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00012C93 File Offset: 0x00010E93
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00012C9B File Offset: 0x00010E9B
		public ButtonControl buttonWest { get; protected set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00012CA4 File Offset: 0x00010EA4
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00012CAC File Offset: 0x00010EAC
		public ButtonControl buttonNorth { get; protected set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00012CB5 File Offset: 0x00010EB5
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00012CBD File Offset: 0x00010EBD
		public ButtonControl buttonSouth { get; protected set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00012CC6 File Offset: 0x00010EC6
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00012CCE File Offset: 0x00010ECE
		public ButtonControl buttonEast { get; protected set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012CD7 File Offset: 0x00010ED7
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00012CDF File Offset: 0x00010EDF
		public ButtonControl leftStickButton { get; protected set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00012CE8 File Offset: 0x00010EE8
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00012CF0 File Offset: 0x00010EF0
		public ButtonControl rightStickButton { get; protected set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00012CF9 File Offset: 0x00010EF9
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00012D01 File Offset: 0x00010F01
		public ButtonControl startButton { get; protected set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00012D0A File Offset: 0x00010F0A
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00012D12 File Offset: 0x00010F12
		public ButtonControl selectButton { get; protected set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00012D1B File Offset: 0x00010F1B
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00012D23 File Offset: 0x00010F23
		public DpadControl dpad { get; protected set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00012D2C File Offset: 0x00010F2C
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00012D34 File Offset: 0x00010F34
		public ButtonControl leftShoulder { get; protected set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00012D3D File Offset: 0x00010F3D
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00012D45 File Offset: 0x00010F45
		public ButtonControl rightShoulder { get; protected set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00012D4E File Offset: 0x00010F4E
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00012D56 File Offset: 0x00010F56
		public StickControl leftStick { get; protected set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00012D5F File Offset: 0x00010F5F
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00012D67 File Offset: 0x00010F67
		public StickControl rightStick { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00012D70 File Offset: 0x00010F70
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00012D78 File Offset: 0x00010F78
		public ButtonControl leftTrigger { get; protected set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00012D81 File Offset: 0x00010F81
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x00012D89 File Offset: 0x00010F89
		public ButtonControl rightTrigger { get; protected set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00012D92 File Offset: 0x00010F92
		public ButtonControl aButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00012D9A File Offset: 0x00010F9A
		public ButtonControl bButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00012DA2 File Offset: 0x00010FA2
		public ButtonControl xButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00012DAA File Offset: 0x00010FAA
		public ButtonControl yButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00012DB2 File Offset: 0x00010FB2
		public ButtonControl triangleButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00012DBA File Offset: 0x00010FBA
		public ButtonControl squareButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x00012DC2 File Offset: 0x00010FC2
		public ButtonControl circleButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00012DCA File Offset: 0x00010FCA
		public ButtonControl crossButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x17000133 RID: 307
		public ButtonControl this[GamepadButton button]
		{
			get
			{
				switch (button)
				{
				case GamepadButton.DpadUp:
					return this.dpad.up;
				case GamepadButton.DpadDown:
					return this.dpad.down;
				case GamepadButton.DpadLeft:
					return this.dpad.left;
				case GamepadButton.DpadRight:
					return this.dpad.right;
				case GamepadButton.North:
					return this.buttonNorth;
				case GamepadButton.East:
					return this.buttonEast;
				case GamepadButton.South:
					return this.buttonSouth;
				case GamepadButton.West:
					return this.buttonWest;
				case GamepadButton.LeftStick:
					return this.leftStickButton;
				case GamepadButton.RightStick:
					return this.rightStickButton;
				case GamepadButton.LeftShoulder:
					return this.leftShoulder;
				case GamepadButton.RightShoulder:
					return this.rightShoulder;
				case GamepadButton.Start:
					return this.startButton;
				case GamepadButton.Select:
					return this.selectButton;
				default:
					if (button == GamepadButton.LeftTrigger)
					{
						return this.leftTrigger;
					}
					if (button != GamepadButton.RightTrigger)
					{
						throw new InvalidEnumArgumentException("button", (int)button, typeof(GamepadButton));
					}
					return this.rightTrigger;
				}
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00012EC7 File Offset: 0x000110C7
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x00012ECE File Offset: 0x000110CE
		public static Gamepad current { get; private set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00012ED6 File Offset: 0x000110D6
		public new static ReadOnlyArray<Gamepad> all
		{
			get
			{
				return new ReadOnlyArray<Gamepad>(Gamepad.s_Gamepads, 0, Gamepad.s_GamepadCount);
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012EE8 File Offset: 0x000110E8
		protected override void FinishSetup()
		{
			this.buttonWest = base.GetChildControl<ButtonControl>("buttonWest");
			this.buttonNorth = base.GetChildControl<ButtonControl>("buttonNorth");
			this.buttonSouth = base.GetChildControl<ButtonControl>("buttonSouth");
			this.buttonEast = base.GetChildControl<ButtonControl>("buttonEast");
			this.startButton = base.GetChildControl<ButtonControl>("start");
			this.selectButton = base.GetChildControl<ButtonControl>("select");
			this.leftStickButton = base.GetChildControl<ButtonControl>("leftStickPress");
			this.rightStickButton = base.GetChildControl<ButtonControl>("rightStickPress");
			this.dpad = base.GetChildControl<DpadControl>("dpad");
			this.leftShoulder = base.GetChildControl<ButtonControl>("leftShoulder");
			this.rightShoulder = base.GetChildControl<ButtonControl>("rightShoulder");
			this.leftStick = base.GetChildControl<StickControl>("leftStick");
			this.rightStick = base.GetChildControl<StickControl>("rightStick");
			this.leftTrigger = base.GetChildControl<ButtonControl>("leftTrigger");
			this.rightTrigger = base.GetChildControl<ButtonControl>("rightTrigger");
			base.FinishSetup();
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012FFA File Offset: 0x000111FA
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gamepad.current = this;
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00013008 File Offset: 0x00011208
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Gamepad>(ref Gamepad.s_Gamepads, ref Gamepad.s_GamepadCount, this, 10);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013020 File Offset: 0x00011220
		protected override void OnRemoved()
		{
			if (Gamepad.current == this)
			{
				Gamepad.current = null;
			}
			int num = Gamepad.s_Gamepads.IndexOfReference(this, Gamepad.s_GamepadCount);
			if (num != -1)
			{
				Gamepad.s_Gamepads.EraseAtWithCapacity(ref Gamepad.s_GamepadCount, num);
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013060 File Offset: 0x00011260
		public virtual void PauseHaptics()
		{
			this.m_Rumble.PauseHaptics(this);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001306E File Offset: 0x0001126E
		public virtual void ResumeHaptics()
		{
			this.m_Rumble.ResumeHaptics(this);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001307C File Offset: 0x0001127C
		public virtual void ResetHaptics()
		{
			this.m_Rumble.ResetHaptics(this);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001308A File Offset: 0x0001128A
		public virtual void SetMotorSpeeds(float lowFrequency, float highFrequency)
		{
			this.m_Rumble.SetMotorSpeeds(this, lowFrequency, highFrequency);
		}

		// Token: 0x04000151 RID: 337
		private DualMotorRumble m_Rumble;

		// Token: 0x04000152 RID: 338
		private static int s_GamepadCount;

		// Token: 0x04000153 RID: 339
		private static Gamepad[] s_Gamepads;
	}
}
