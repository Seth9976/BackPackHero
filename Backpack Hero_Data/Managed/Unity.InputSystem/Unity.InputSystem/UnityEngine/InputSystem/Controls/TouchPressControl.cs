using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011A RID: 282
	[InputControlLayout(hideInUI = true)]
	public class TouchPressControl : ButtonControl
	{
		// Token: 0x06000FF9 RID: 4089 RVA: 0x0004C850 File Offset: 0x0004AA50
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.stateBlock.format.IsIntegerFormat())
			{
				throw new NotSupportedException(string.Format("Non-integer format '{0}' is not supported for TouchButtonControl '{1}'", base.stateBlock.format, this));
			}
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004C89C File Offset: 0x0004AA9C
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			TouchPhase touchPhase = (TouchPhase)MemoryHelpers.ReadMultipleBitsAsUInt((void*)((byte*)statePtr + this.m_StateBlock.byteOffset), this.m_StateBlock.bitOffset, this.m_StateBlock.sizeInBits);
			float num = 0f;
			if (touchPhase == TouchPhase.Began || touchPhase == TouchPhase.Stationary || touchPhase == TouchPhase.Moved)
			{
				num = 1f;
			}
			return base.Preprocess(num);
		}

		// Token: 0x06000FFB RID: 4091 RVA: 0x0004C8F1 File Offset: 0x0004AAF1
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			throw new NotSupportedException();
		}
	}
}
