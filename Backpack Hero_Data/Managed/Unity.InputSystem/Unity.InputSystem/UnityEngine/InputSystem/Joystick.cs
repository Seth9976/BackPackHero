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
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00013E6A File Offset: 0x0001206A
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x00013E72 File Offset: 0x00012072
		public ButtonControl trigger { get; protected set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00013E7B File Offset: 0x0001207B
		// (set) Token: 0x060004C9 RID: 1225 RVA: 0x00013E83 File Offset: 0x00012083
		public StickControl stick { get; protected set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00013E8C File Offset: 0x0001208C
		// (set) Token: 0x060004CB RID: 1227 RVA: 0x00013E94 File Offset: 0x00012094
		public AxisControl twist { get; protected set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00013E9D File Offset: 0x0001209D
		// (set) Token: 0x060004CD RID: 1229 RVA: 0x00013EA5 File Offset: 0x000120A5
		public Vector2Control hatswitch { get; protected set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00013EAE File Offset: 0x000120AE
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00013EB5 File Offset: 0x000120B5
		public static Joystick current { get; private set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00013EBD File Offset: 0x000120BD
		public new static ReadOnlyArray<Joystick> all
		{
			get
			{
				return new ReadOnlyArray<Joystick>(Joystick.s_Joysticks, 0, Joystick.s_JoystickCount);
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00013ED0 File Offset: 0x000120D0
		protected override void FinishSetup()
		{
			this.trigger = base.GetChildControl<ButtonControl>("{PrimaryTrigger}");
			this.stick = base.GetChildControl<StickControl>("{Primary2DMotion}");
			this.twist = base.TryGetChildControl<AxisControl>("{Twist}");
			this.hatswitch = base.TryGetChildControl<Vector2Control>("{Hatswitch}");
			base.FinishSetup();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00013F27 File Offset: 0x00012127
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Joystick.current = this;
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00013F35 File Offset: 0x00012135
		protected override void OnAdded()
		{
			ArrayHelpers.AppendWithCapacity<Joystick>(ref Joystick.s_Joysticks, ref Joystick.s_JoystickCount, this, 10);
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00013F4C File Offset: 0x0001214C
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
