using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200010D RID: 269
	[InputControlLayout(hideInUI = true)]
	public class AnyKeyControl : ButtonControl
	{
		// Token: 0x06000F86 RID: 3974 RVA: 0x0004B680 File Offset: 0x00049880
		public AnyKeyControl()
		{
			this.m_StateBlock.sizeInBits = 1U;
			this.m_StateBlock.format = InputStateBlock.FormatBit;
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004B6A4 File Offset: 0x000498A4
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			if (!this.CheckStateIsAtDefault(statePtr, null))
			{
				return 1f;
			}
			return 0f;
		}
	}
}
