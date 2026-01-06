using System;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000111 RID: 273
	public class DiscreteButtonControl : ButtonControl
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0004BBC8 File Offset: 0x00049DC8
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for DiscreteButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0004BC14 File Offset: 0x00049E14
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int num = MemoryHelpers.ReadTwosComplementMultipleBitsAsInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float num2 = 0f;
			if (this.minValue > this.maxValue)
			{
				if (this.wrapAtValue == this.nullValue)
				{
					this.wrapAtValue = this.minValue;
				}
				if ((num >= this.minValue && num <= this.wrapAtValue) || (num != this.nullValue && num <= this.maxValue))
				{
					num2 = 1f;
				}
			}
			else
			{
				num2 = ((num >= this.minValue && num <= this.maxValue) ? 1f : 0f);
			}
			return base.Preprocess(num2);
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0004BCCC File Offset: 0x00049ECC
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			if (this.writeMode == DiscreteButtonControl.WriteMode.WriteNullAndMaxValue)
			{
				void* ptr = (void*)((byte*)statePtr + this.m_StateBlock.byteOffset);
				int num = ((value >= base.pressPointOrDefault) ? this.maxValue : this.nullValue);
				MemoryHelpers.WriteIntAsTwosComplementMultipleBits(ptr, this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits, num);
				return;
			}
			throw new NotSupportedException("Writing value states for DiscreteButtonControl is not supported as a single value may correspond to multiple states");
		}

		// Token: 0x0400066B RID: 1643
		public int minValue;

		// Token: 0x0400066C RID: 1644
		public int maxValue;

		// Token: 0x0400066D RID: 1645
		public int wrapAtValue;

		// Token: 0x0400066E RID: 1646
		public int nullValue;

		// Token: 0x0400066F RID: 1647
		public DiscreteButtonControl.WriteMode writeMode;

		// Token: 0x0200022F RID: 559
		public enum WriteMode
		{
			// Token: 0x04000BDE RID: 3038
			WriteDisabled,
			// Token: 0x04000BDF RID: 3039
			WriteNullAndMaxValue
		}
	}
}
