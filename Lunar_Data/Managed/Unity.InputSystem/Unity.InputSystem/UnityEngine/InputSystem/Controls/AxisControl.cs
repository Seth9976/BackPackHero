using System;
using System.Runtime.CompilerServices;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Processors;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200010E RID: 270
	public class AxisControl : InputControl<float>
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x0004B670 File Offset: 0x00049870
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected float Preprocess(float value)
		{
			if (this.scale)
			{
				value *= this.scaleFactor;
			}
			if (this.clamp == AxisControl.Clamp.ToConstantBeforeNormalize)
			{
				if (value < this.clampMin || value > this.clampMax)
				{
					value = this.clampConstant;
				}
			}
			else if (this.clamp == AxisControl.Clamp.BeforeNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Normalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.clamp == AxisControl.Clamp.AfterNormalize)
			{
				value = Mathf.Clamp(value, this.clampMin, this.clampMax);
			}
			if (this.invert)
			{
				value *= -1f;
			}
			return value;
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0004B724 File Offset: 0x00049924
		private float Unpreprocess(float value)
		{
			if (this.invert)
			{
				value *= -1f;
			}
			if (this.normalize)
			{
				value = NormalizeProcessor.Denormalize(value, this.normalizeMin, this.normalizeMax, this.normalizeZero);
			}
			if (this.scale)
			{
				value /= this.scaleFactor;
			}
			return value;
		}

		// Token: 0x06000F85 RID: 3973 RVA: 0x0004B777 File Offset: 0x00049977
		public AxisControl()
		{
			this.m_StateBlock.format = InputStateBlock.FormatFloat;
		}

		// Token: 0x06000F86 RID: 3974 RVA: 0x0004B790 File Offset: 0x00049990
		protected override void FinishSetup()
		{
			base.FinishSetup();
			if (!base.hasDefaultState && this.normalize && Mathf.Abs(this.normalizeZero) > Mathf.Epsilon)
			{
				this.m_DefaultState = base.stateBlock.FloatToPrimitiveValue(this.normalizeZero);
			}
		}

		// Token: 0x06000F87 RID: 3975 RVA: 0x0004B7E0 File Offset: 0x000499E0
		public unsafe override float ReadUnprocessedValueFromState(void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num != 1113150533)
			{
				if (num == 1179407392)
				{
					return *(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset);
				}
				float num2 = base.stateBlock.ReadFloat(statePtr);
				return this.Preprocess(num2);
			}
			else
			{
				if (((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] == 0)
				{
					return 0f;
				}
				return 1f;
			}
		}

		// Token: 0x06000F88 RID: 3976 RVA: 0x0004B850 File Offset: 0x00049A50
		public unsafe override void WriteValueIntoState(float value, void* statePtr)
		{
			int num = this.m_OptimizedControlDataType;
			if (num == 1113150533)
			{
				((byte*)statePtr)[this.m_StateBlock.m_ByteOffset] = ((value >= 0.5f) ? 1 : 0);
				return;
			}
			if (num == 1179407392)
			{
				*(float*)((byte*)statePtr + this.m_StateBlock.m_ByteOffset) = value;
				return;
			}
			value = this.Unpreprocess(value);
			base.stateBlock.WriteFloat(statePtr, value);
		}

		// Token: 0x06000F89 RID: 3977 RVA: 0x0004B8C0 File Offset: 0x00049AC0
		public unsafe override bool CompareValue(void* firstStatePtr, void* secondStatePtr)
		{
			float num = base.ReadValueFromState(firstStatePtr);
			float num2 = base.ReadValueFromState(secondStatePtr);
			return !Mathf.Approximately(num, num2);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x0004B8E5 File Offset: 0x00049AE5
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return this.EvaluateMagnitude(base.ReadValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x0004B8F4 File Offset: 0x00049AF4
		private float EvaluateMagnitude(float value)
		{
			if (this.m_MinValue.isEmpty || this.m_MaxValue.isEmpty)
			{
				return Mathf.Abs(value);
			}
			float num = this.m_MinValue.ToSingle(null);
			float num2 = this.m_MaxValue.ToSingle(null);
			float num3 = Mathf.Clamp(value, num, num2);
			if (num >= 0f)
			{
				return NormalizeProcessor.Normalize(num3, num, num2, 0f);
			}
			if (num3 < 0f)
			{
				return NormalizeProcessor.Normalize(Mathf.Abs(num3), 0f, Mathf.Abs(num), 0f);
			}
			return NormalizeProcessor.Normalize(num3, 0f, num2, 0f);
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x0004B990 File Offset: 0x00049B90
		protected override FourCC CalculateOptimizedControlDataType()
		{
			bool flag = this.clamp == AxisControl.Clamp.None && !this.invert && !this.normalize && !this.scale;
			if (flag && this.m_StateBlock.format == InputStateBlock.FormatFloat && this.m_StateBlock.sizeInBits == 32U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatFloat;
			}
			if (flag && this.m_StateBlock.format == InputStateBlock.FormatBit && this.m_StateBlock.sizeInBits == 8U && this.m_StateBlock.bitOffset == 0U)
			{
				return InputStateBlock.FormatByte;
			}
			return InputStateBlock.FormatInvalid;
		}

		// Token: 0x04000658 RID: 1624
		public AxisControl.Clamp clamp;

		// Token: 0x04000659 RID: 1625
		public float clampMin;

		// Token: 0x0400065A RID: 1626
		public float clampMax;

		// Token: 0x0400065B RID: 1627
		public float clampConstant;

		// Token: 0x0400065C RID: 1628
		public bool invert;

		// Token: 0x0400065D RID: 1629
		public bool normalize;

		// Token: 0x0400065E RID: 1630
		public float normalizeMin;

		// Token: 0x0400065F RID: 1631
		public float normalizeMax;

		// Token: 0x04000660 RID: 1632
		public float normalizeZero;

		// Token: 0x04000661 RID: 1633
		public bool scale;

		// Token: 0x04000662 RID: 1634
		public float scaleFactor;

		// Token: 0x0200022E RID: 558
		public enum Clamp
		{
			// Token: 0x04000BD9 RID: 3033
			None,
			// Token: 0x04000BDA RID: 3034
			BeforeNormalize,
			// Token: 0x04000BDB RID: 3035
			AfterNormalize,
			// Token: 0x04000BDC RID: 3036
			ToConstantBeforeNormalize
		}
	}
}
