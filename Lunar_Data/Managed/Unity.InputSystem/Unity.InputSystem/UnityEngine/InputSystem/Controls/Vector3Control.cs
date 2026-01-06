using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011C RID: 284
	public class Vector3Control : InputControl<Vector3>
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0004CA62 File Offset: 0x0004AC62
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x0004CA6A File Offset: 0x0004AC6A
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0004CA73 File Offset: 0x0004AC73
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0004CA7B File Offset: 0x0004AC7B
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0004CA84 File Offset: 0x0004AC84
		// (set) Token: 0x06001007 RID: 4103 RVA: 0x0004CA8C File Offset: 0x0004AC8C
		[InputControl(offset = 8U, displayName = "Z")]
		public AxisControl z { get; set; }

		// Token: 0x06001008 RID: 4104 RVA: 0x0004CA95 File Offset: 0x0004AC95
		public Vector3Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector3;
		}

		// Token: 0x06001009 RID: 4105 RVA: 0x0004CAAD File Offset: 0x0004ACAD
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			this.z = base.GetChildControl<AxisControl>("z");
			base.FinishSetup();
		}

		// Token: 0x0600100A RID: 4106 RVA: 0x0004CAE8 File Offset: 0x0004ACE8
		public unsafe override Vector3 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379763)
			{
				return *(Vector3*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector3(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr), this.z.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0004CB44 File Offset: 0x0004AD44
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

		// Token: 0x0600100C RID: 4108 RVA: 0x0004CBB0 File Offset: 0x0004ADB0
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0004CBCC File Offset: 0x0004ADCC
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
