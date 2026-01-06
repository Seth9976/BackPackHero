using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000039 RID: 57
	[InputControlLayout(stateType = typeof(JoystickState), isGenericTypeOfDevice = true)]
	public class Joystick : InputDevice
	{
		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00013E2E File Offset: 0x0001202E
		// (set) Token: 0x060004C5 RID: 1221 RVA: 0x00013E36 File Offset: 0x00012036
		public ButtonControl trigger { get; protected set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00013E3F File Offset: 0x0001203F
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00013E47 File Offset: 0x00012047
		public StickControl stick { get; protected set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00013E50 File Offset: 0x00012050
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00013E58 File Offset: 0x00012058
		public AxisControl twist { get; protected set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00013E61 File Offset: 0x00012061
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00013E69 File Offset: 0x00012069
		public Vector2Control hatswitch { get; protected set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00013E72 File Offset: 0x00012072
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00013E79 File Offset: 0x00012079
		public static Joystick current { get; private set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00013E81 File Offset: 0x00012081
		public new static ReadOnlyArray<Joystick> all
		{
			get
			{
				return new ReadOnlyArray<Joystick>(Joystick.s_Joysticks, 0, Joystick.s_JoystickCount);
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00013E94 File Offset: 0x00012094
		protected override void FinishSetup()
		{
			this.trigger = base.GetChildControl<ButtonControl>("{PrimaryTrigger}");
			this.stick = base.GetChildControl<StickControl>("{Primary2DMotion}");
			this.twist = base.TryGetChildControl<AxisControl>("{Twist}");
			this.hatswitch = base.TryGetChildControl<Vector2Control>("{Hatswitch}");
			base.FinishSetup();
		}

		// Token: 0x060004D0 RID: 1232 RVA: 0x00013EEB File Offset: 0x000120EB
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Joystick.current = this;
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013EF9 File Offset: 0x000120F9
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Joystick>(ref Joystick.s_Joysticks, ref Joystick.s_JoystickCount, this, 10);
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013F10 File Offset: 0x00012110
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Joystick.current == this)
			{
				Joystick.current = null;
			}
			int num = Joystick.s_Joysticks.IndexOfReference(this, Joystick.s_JoystickCount);
			if (num != -1)
			{
				Joystick.s_Joysticks.EraseAtWithCapacity(ref Joystick.s_JoystickCount, num);
			}
		}

		// Token: 0x04000179 RID: 377
		private static int s_JoystickCount;

		// Token: 0x0400017A RID: 378
		private static Joystick[] s_Joysticks;
	}
}
