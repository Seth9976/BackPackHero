using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000119 RID: 281
	[InputControlLayout(hideInUI = true)]
	public class TouchPhaseControl : InputControl<TouchPhase>
	{
		// Token: 0x06000FF6 RID: 4086 RVA: 0x0004C806 File Offset: 0x0004AA06
		public TouchPhaseControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0004C820 File Offset: 0x0004AA20
		public unsafe override TouchPhase ReadUnprocessedValueFromState(void* statePtr)
		{
			return (TouchPhase)base.stateBlock.ReadInt(statePtr);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0004C83C File Offset: 0x0004AA3C
		public unsafe override void WriteValueIntoState(TouchPhase value, void* statePtr)
		{
			*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = (int)value;
		}
	}
}
