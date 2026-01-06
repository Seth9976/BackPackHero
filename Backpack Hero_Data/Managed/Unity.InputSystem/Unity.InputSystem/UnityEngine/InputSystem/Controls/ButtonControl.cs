using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200010F RID: 271
	public class ButtonControl : AxisControl
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0004BA8A File Offset: 0x00049C8A
		public float pressPointOrDefault
		{
			get
			{
				if (this.pressPoint <= 0f)
				{
					return ButtonControl.s_GlobalDefaultButtonPressPoint;
				}
				return this.pressPoint;
			}
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0004BAA8 File Offset: 0x00049CA8
		public ButtonControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatBit;
			this.m_MinValue = 0f;
			this.m_MaxValue = 1f;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0004BAF6 File Offset: 0x00049CF6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new bool IsValueConsideredPressed(float value)
		{
			return value >= this.pressPointOrDefault;
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F95 RID: 3989 RVA: 0x0004BB04 File Offset: 0x00049D04
		public unsafe bool isPressed
		{
			get
			{
				return this.IsValueConsideredPressed(*base.value);
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0004BB13 File Offset: 0x00049D13
		public unsafe bool wasPressedThisFrame
		{
			get
			{
				return base.device.wasUpdatedThisFrame && this.IsValueConsideredPressed(*base.value) && !this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000F97 RID: 3991 RVA: 0x0004BB42 File Offset: 0x00049D42
		public unsafe bool wasReleasedThisFrame
		{
			get
			{
				return base.device.wasUpdatedThisFrame && !this.IsValueConsideredPressed(*base.value) && this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
			}
		}

		// Token: 0x04000664 RID: 1636
		public float pressPoint = -1f;

		// Token: 0x04000665 RID: 1637
		internal static float s_GlobalDefaultButtonPressPoint;

		// Token: 0x04000666 RID: 1638
		internal static float s_GlobalDefaultButtonReleaseThreshold;

		// Token: 0x04000667 RID: 1639
		internal const float kMinButtonPressPoint = 0.0001f;
	}
}
