using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011C RID: 284
	public class Vector3Control : InputControl<Vector3>
	{
		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0004CAAE File Offset: 0x0004ACAE
		// (set) Token: 0x06001008 RID: 4104 RVA: 0x0004CAB6 File Offset: 0x0004ACB6
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0004CABF File Offset: 0x0004ACBF
		// (set) Token: 0x0600100A RID: 4106 RVA: 0x0004CAC7 File Offset: 0x0004ACC7
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0004CAD0 File Offset: 0x0004ACD0
		// (set) Token: 0x0600100C RID: 4108 RVA: 0x0004CAD8 File Offset: 0x0004ACD8
		[InputControl(offset = 8U, displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x0600100D RID: 4109 RVA: 0x0004CAE1 File Offset: 0x0004ACE1
		public Vector3Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector3;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0004CAF9 File Offset: 0x0004ACF9
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			base.FinishSetup();
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0004CB34 File Offset: 0x0004AD34
		public unsafe override Vector3 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				return *(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector3(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr), this.z.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0004CB90 File Offset: 0x0004AD90
		public unsafe override void WriteValueIntoState(Vector3 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				*(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
			this.z.WriteValueIntoState(value.z, statePtr);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0004CBFC File Offset: 0x0004ADFC
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0004CC18 File Offset: 0x0004AE18
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 96U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.z.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U && this.z.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 8U)
			{
				return InputStateBlock.FormatVector3;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
