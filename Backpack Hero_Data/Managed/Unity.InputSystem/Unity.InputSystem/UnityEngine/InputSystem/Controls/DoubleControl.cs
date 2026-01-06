using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000112 RID: 274
	public class DoubleControl : InputControl<double>
	{
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0004BD82 File Offset: 0x00049F82
		public DoubleControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatDouble;
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0004BD9A File Offset: 0x00049F9A
		public unsafe override double ReadUnprocessedValueFromState(void* statePtr)
		{
			return this.m_StateBlock.ReadDouble(statePtr);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0004BDA8 File Offset: 0x00049FA8
		public unsafe override void WriteValueIntoState(double value, void* statePtr)
		{
			this.m_StateBlock.WriteDouble(statePtr, value);
		}
	}
}
