using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000116 RID: 278
	public class QuaternionControl : InputControl<Quaternion>
	{
		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0004C208 File Offset: 0x0004A408
		// (set) Token: 0x06000FC1 RID: 4033 RVA: 0x0004C210 File Offset: 0x0004A410
		[InputControl(displayName = "X")]
		public AxisControl x { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FC2 RID: 4034 RVA: 0x0004C219 File Offset: 0x0004A419
		// (set) Token: 0x06000FC3 RID: 4035 RVA: 0x0004C221 File Offset: 0x0004A421
		[InputControl(displayName = "Y")]
		public AxisControl y { get; private set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x0004C22A File Offset: 0x0004A42A
		// (set) Token: 0x06000FC5 RID: 4037 RVA: 0x0004C232 File Offset: 0x0004A432
		[InputControl(displayName = "Z")]
		public AxisControl z { get; private set; }

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0004C23B File Offset: 0x0004A43B
		// (set) Token: 0x06000FC7 RID: 4039 RVA: 0x0004C243 File Offset: 0x0004A443
		[InputControl(displayName = "W")]
		public AxisControl w { get; private set; }

		// Token: 0x06000FC8 RID: 4040 RVA: 0x0004C24C File Offset: 0x0004A44C
		public QuaternionControl()
		{
			this.m_StateBlock.sizeInBits = 128U;
			this.m_StateBlock.format = InputStateBlock.FormatQuaternion;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x0004C274 File Offset: 0x0004A474
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			this.w = base.GetChildControl<AxisControl>("w");
			base.FinishSetup();
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x0004C2CC File Offset: 0x0004A4CC
		public unsafe override Quaternion ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				return *(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Quaternion(this.x.ReadValueFromStateWithCaching(statePtr), this.y.ReadValueFromStateWithCaching(statePtr), this.z.ReadValueFromStateWithCaching(statePtr), this.w.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004C334 File Offset: 0x0004A534
		public unsafe override void WriteValueIntoState(Quaternion value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				*(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
			this.w.WriteValueIntoState(value.w, statePtr);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0004C3B0 File Offset: 0x0004A5B0
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 128U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.w.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U && this.w.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 12U && this.x.m_ProcessorStack.length == 0 && this.y.m_ProcessorStack.length == 0 && this.z.m_ProcessorStack.length == 0)
			{
				return InputStateBlock.FormatQuaternion;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
