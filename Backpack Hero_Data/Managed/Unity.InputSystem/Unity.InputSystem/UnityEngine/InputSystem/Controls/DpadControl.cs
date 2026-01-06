using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000113 RID: 275
	public class DpadControl : Vector2Control
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0004BDB7 File Offset: 0x00049FB7
		// (set) Token: 0x06000FAA RID: 4010 RVA: 0x0004BDBF File Offset: 0x00049FBF
		[InputControl(name = "x", layout = "DpadAxis", useStateFrom = "right", synthetic = true)]
		[InputControl(name = "y", layout = "DpadAxis", useStateFrom = "up", synthetic = true)]
		[InputControl(bit = 0U, displayName = "Up")]
		public ButtonControl up { get; set; }

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0004BDC8 File Offset: 0x00049FC8
		// (set) Token: 0x06000FAC RID: 4012 RVA: 0x0004BDD0 File Offset: 0x00049FD0
		[InputControl(bit = 1U, displayName = "Down")]
		public ButtonControl down { get; set; }

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0004BDD9 File Offset: 0x00049FD9
		// (set) Token: 0x06000FAE RID: 4014 RVA: 0x0004BDE1 File Offset: 0x00049FE1
		[InputControl(bit = 2U, displayName = "Left")]
		public ButtonControl left { get; set; }

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0004BDEA File Offset: 0x00049FEA
		// (set) Token: 0x06000FB0 RID: 4016 RVA: 0x0004BDF2 File Offset: 0x00049FF2
		[InputControl(bit = 3U, displayName = "Right")]
		public ButtonControl right { get; set; }

		// Token: 0x06000FB1 RID: 4017 RVA: 0x0004BDFB File Offset: 0x00049FFB
		public DpadControl()
		{
			this.m_StateBlock.sizeInBits = 4U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004BE20 File Offset: 0x0004A020
		protected override void FinishSetup()
		{
			this.up = base.GetChildControl<ButtonControl>("up");
			this.down = base.GetChildControl<ButtonControl>("down");
			this.left = base.GetChildControl<ButtonControl>("left");
			this.right = base.GetChildControl<ButtonControl>("right");
			base.FinishSetup();
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004BE78 File Offset: 0x0004A078
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			bool flag = this.up.ReadValueFromStateWithCaching(statePtr) >= this.up.pressPointOrDefault;
			bool flag2 = this.down.ReadValueFromStateWithCaching(statePtr) >= this.down.pressPointOrDefault;
			bool flag3 = this.left.ReadValueFromStateWithCaching(statePtr) >= this.left.pressPointOrDefault;
			bool flag4 = this.right.ReadValueFromStateWithCaching(statePtr) >= this.right.pressPointOrDefault;
			return DpadControl.MakeDpadVector(flag, flag2, flag3, flag4, true);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004BF04 File Offset: 0x0004A104
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			bool flag = this.up.IsValueConsideredPressed(value.y);
			bool flag2 = this.down.IsValueConsideredPressed(value.y * -1f);
			bool flag3 = this.left.IsValueConsideredPressed(value.x * -1f);
			bool flag4 = this.right.IsValueConsideredPressed(value.x);
			this.up.WriteValueIntoState((flag && !flag2) ? value.y : 0f, statePtr);
			this.down.WriteValueIntoState((flag2 && !flag) ? (value.y * -1f) : 0f, statePtr);
			this.left.WriteValueIntoState((flag3 && !flag4) ? (value.x * -1f) : 0f, statePtr);
			this.right.WriteValueIntoState((flag4 && !flag3) ? value.x : 0f, statePtr);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004BFF0 File Offset: 0x0004A1F0
		public static Vector2 MakeDpadVector(bool up, bool down, bool left, bool right, bool normalize = true)
		{
			float num = (up ? 1f : 0f);
			float num2 = (down ? (-1f) : 0f);
			float num3 = (left ? (-1f) : 0f);
			float num4 = (right ? 1f : 0f);
			Vector2 vector = new Vector2(num3 + num4, num + num2);
			if (normalize && vector.x != 0f && vector.y != 0f)
			{
				vector = new Vector2(vector.x * 0.707107f, vector.y * 0.707107f);
			}
			return vector;
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x0004C08D File Offset: 0x0004A28D
		public static Vector2 MakeDpadVector(float up, float down, float left, float right)
		{
			return new Vector2(-left + right, up - down);
		}

		// Token: 0x02000230 RID: 560
		[InputControlLayout(hideInUI = true)]
		public class DpadAxisControl : AxisControl
		{
			// Token: 0x170005CE RID: 1486
			// (get) Token: 0x06001575 RID: 5493 RVA: 0x0006282D File Offset: 0x00060A2D
			// (set) Token: 0x06001576 RID: 5494 RVA: 0x00062835 File Offset: 0x00060A35
			public int component { get; set; }

			// Token: 0x06001577 RID: 5495 RVA: 0x0006283E File Offset: 0x00060A3E
			protected override void FinishSetup()
			{
				base.FinishSetup();
				this.component = ((base.name == "x") ? 0 : 1);
				this.m_StateBlock = this.m_Parent.m_StateBlock;
			}

			// Token: 0x06001578 RID: 5496 RVA: 0x00062874 File Offset: 0x00060A74
			public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
			{
				return ((DpadControl)this.m_Parent).ReadUnprocessedValueFromState(statePtr)[this.component];
			}
		}

		// Token: 0x02000231 RID: 561
		internal enum ButtonBits
		{
			// Token: 0x04000BE3 RID: 3043
			Up,
			// Token: 0x04000BE4 RID: 3044
			Down,
			// Token: 0x04000BE5 RID: 3045
			Left,
			// Token: 0x04000BE6 RID: 3046
			Right
		}
	}
}
