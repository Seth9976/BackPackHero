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
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x00012C57 File Offset: 0x00010E57
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x00012C5F File Offset: 0x00010E5F
		public ButtonControl buttonWest { get; protected set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x00012C68 File Offset: 0x00010E68
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x00012C70 File Offset: 0x00010E70
		public ButtonControl buttonNorth { get; protected set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x00012C79 File Offset: 0x00010E79
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x00012C81 File Offset: 0x00010E81
		public ButtonControl buttonSouth { get; protected set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00012C8A File Offset: 0x00010E8A
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x00012C92 File Offset: 0x00010E92
		public ButtonControl buttonEast { get; protected set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00012C9B File Offset: 0x00010E9B
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00012CA3 File Offset: 0x00010EA3
		public ButtonControl leftStickButton { get; protected set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00012CAC File Offset: 0x00010EAC
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00012CB4 File Offset: 0x00010EB4
		public ButtonControl rightStickButton { get; protected set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x00012CBD File Offset: 0x00010EBD
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00012CC5 File Offset: 0x00010EC5
		public ButtonControl startButton { get; protected set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x00012CCE File Offset: 0x00010ECE
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00012CD6 File Offset: 0x00010ED6
		public ButtonControl selectButton { get; protected set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x00012CDF File Offset: 0x00010EDF
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00012CE7 File Offset: 0x00010EE7
		public DpadControl dpad { get; protected set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00012CF0 File Offset: 0x00010EF0
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x00012CF8 File Offset: 0x00010EF8
		public ButtonControl leftShoulder { get; protected set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x00012D01 File Offset: 0x00010F01
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x00012D09 File Offset: 0x00010F09
		public ButtonControl rightShoulder { get; protected set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x00012D12 File Offset: 0x00010F12
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x00012D1A File Offset: 0x00010F1A
		public StickControl leftStick { get; protected set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00012D23 File Offset: 0x00010F23
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00012D2B File Offset: 0x00010F2B
		public StickControl rightStick { get; protected set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00012D34 File Offset: 0x00010F34
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x00012D3C File Offset: 0x00010F3C
		public ButtonControl leftTrigger { get; protected set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x00012D45 File Offset: 0x00010F45
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x00012D4D File Offset: 0x00010F4D
		public ButtonControl rightTrigger { get; protected set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00012D56 File Offset: 0x00010F56
		public ButtonControl aButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00012D5E File Offset: 0x00010F5E
		public ButtonControl bButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x00012D66 File Offset: 0x00010F66
		public ButtonControl xButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00012D6E File Offset: 0x00010F6E
		public ButtonControl yButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x00012D76 File Offset: 0x00010F76
		public ButtonControl triangleButton
		{
			get
			{
				return this.buttonNorth;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00012D7E File Offset: 0x00010F7E
		public ButtonControl squareButton
		{
			get
			{
				return this.buttonWest;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x00012D86 File Offset: 0x00010F86
		public ButtonControl circleButton
		{
			get
			{
				return this.buttonEast;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x00012D8E File Offset: 0x00010F8E
		public ButtonControl crossButton
		{
			get
			{
				return this.buttonSouth;
			}
		}

		// Token: 0x17000132 RID: 306
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

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x00012E8B File Offset: 0x0001108B
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00012E92 File Offset: 0x00011092
		public static Gamepad current { get; private set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00012E9A File Offset: 0x0001109A
		public new static ReadOnlyArray<Gamepad> all
		{
			get
			{
				return new ReadOnlyArray<Gamepad>(Gamepad.s_Gamepads, 0, Gamepad.s_GamepadCount);
			}
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00012EAC File Offset: 0x000110AC
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

		// Token: 0x0600047E RID: 1150 RVA: 0x00012FBE File Offset: 0x000111BE
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gamepad.current = this;
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x00012FCC File Offset: 0x000111CC
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Gamepad>(ref Gamepad.s_Gamepads, ref Gamepad.s_GamepadCount, this, 10);
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x00012FE4 File Offset: 0x000111E4
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

		// Token: 0x06000481 RID: 1153 RVA: 0x00013024 File Offset: 0x00011224
		public virtual void PauseHaptics()
		{
			this.m_Rumble.PauseHaptics(this);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00013032 File Offset: 0x00011232
		public virtual void ResumeHaptics()
		{
			this.m_Rumble.ResumeHaptics(this);
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00013040 File Offset: 0x00011240
		public virtual void ResetHaptics()
		{
			this.m_Rumble.ResetHaptics(this);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001304E File Offset: 0x0001124E
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
