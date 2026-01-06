using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000114 RID: 276
	public class IntegerControl : InputControl<int>
	{
		// Token: 0x06000FB2 RID: 4018 RVA: 0x0004C04F File Offset: 0x0004A24F
		public IntegerControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatInt;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0004C067 File Offset: 0x0004A267
		public unsafe override int ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				return *(int*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return this.m_StateBlock.ReadInt(statePtr);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x0004C096 File Offset: 0x0004A296
		public unsafe override void WriteValueIntoState(int value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1229870112)
			{
				*(int*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.m_StateBlock.WriteInt(statePtr, value);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x0004C0C7 File Offset: 0x0004A2C7
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.format == InputStateBlock.FormatInt && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatInt;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
