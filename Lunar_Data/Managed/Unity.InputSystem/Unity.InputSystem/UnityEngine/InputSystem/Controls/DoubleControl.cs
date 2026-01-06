using System;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000112 RID: 274
	public class DoubleControl : InputControl<double>
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0004BD36 File Offset: 0x00049F36
		public DoubleControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatDouble;
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0004BD4E File Offset: 0x00049F4E
		public unsafe override double ReadUnprocessedValueFromState(void* statePtr)
		{
			return this.m_StateBlock.ReadDouble(statePtr);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0004BD5C File Offset: 0x00049F5C
		public unsafe override void WriteValueIntoState(double value, void* statePtr)
		{
			this.m_StateBlock.WriteDouble(statePtr, value);
		}
	}
}
