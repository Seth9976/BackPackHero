using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200010F RID: 271
	public class ButtonControl : AxisControl
	{
		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x0004BA3E File Offset: 0x00049C3E
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

		// Token: 0x06000F8E RID: 3982 RVA: 0x0004BA5C File Offset: 0x00049C5C
		public ButtonControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatBit;
			this.m_MinValue = 0f;
			this.m_MaxValue = 1f;
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0004BAAA File Offset: 0x00049CAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public new bool IsValueConsideredPressed(float value)
		{
			return value >= this.pressPointOrDefault;
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000F90 RID: 3984 RVA: 0x0004BAB8 File Offset: 0x00049CB8
		public unsafe bool isPressed
		{
			get
			{
				return this.IsValueConsideredPressed(*base.value);
			}
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000F91 RID: 3985 RVA: 0x0004BAC7 File Offset: 0x00049CC7
		public unsafe bool wasPressedThisFrame
		{
			get
			{
				return base.device.wasUpdatedThisFrame && this.IsValueConsideredPressed(*base.value) && !this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000F92 RID: 3986 RVA: 0x0004BAF6 File Offset: 0x00049CF6
		public unsafe bool wasReleasedThisFrame
		{
			get
			{
				return base.device.wasUpdatedThisFrame && !this.IsValueConsideredPressed(*base.value) && this.IsValueConsideredPressed(base.ReadValueFromPreviousFrame());
			}
		}

		// Token: 0x04000663 RID: 1635
		public float pressPoint = -1f;

		// Token: 0x04000664 RID: 1636
		internal static float s_GlobalDefaultButtonPressPoint;

		// Token: 0x04000665 RID: 1637
		internal static float s_GlobalDefaultButtonReleaseThreshold;

		// Token: 0x04000666 RID: 1638
		internal const float kMinButtonPressPoint = 0.0001f;
	}
}
