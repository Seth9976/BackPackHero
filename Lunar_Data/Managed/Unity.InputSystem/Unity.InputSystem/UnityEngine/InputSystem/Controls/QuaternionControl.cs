using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x02000116 RID: 278
	public class QuaternionControl : InputControl<Quaternion>
	{
		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x0004C1BC File Offset: 0x0004A3BC
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x0004C1C4 File Offset: 0x0004A3C4
		[InputControl(displayName = "X")]
		public AxisControl x { get; private set; }

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0004C1CD File Offset: 0x0004A3CD
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x0004C1D5 File Offset: 0x0004A3D5
		[InputControl(displayName = "Y")]
		public AxisControl y { get; private set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x0004C1DE File Offset: 0x0004A3DE
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0004C1E6 File Offset: 0x0004A3E6
		[InputControl(displayName = "Z")]
		public AxisControl z { get; private set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x0004C1EF File Offset: 0x0004A3EF
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x0004C1F7 File Offset: 0x0004A3F7
		[InputControl(displayName = "W")]
		public AxisControl w { get; private set; }

		// Token: 0x06000FC3 RID: 4035 RVA: 0x0004C200 File Offset: 0x0004A400
		public QuaternionControl()
		{
			this.m_StateBlock.sizeInBits = 128U;
			this.m_StateBlock.format = InputStateBlock.FormatQuaternion;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0004C228 File Offset: 0x0004A428
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			this.w = base.GetChildControl<AxisControl>("w");
			base.FinishSetup();
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x0004C280 File Offset: 0x0004A480
		public unsafe override Quaternion ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1364541780)
			{
				return *(Quaternion*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Quaternion(this.x.ReadValueFromStateWithCaching(statePtr), this.y.ReadValueFromStateWithCaching(statePtr), this.z.ReadValueFromStateWithCaching(statePtr), this.w.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x0004C2E8 File Offset: 0x0004A4E8
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

		// Token: 0x06000FC7 RID: 4039 RVA: 0x0004C364 File Offset: 0x0004A564
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
