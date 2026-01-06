using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011B RID: 283
	public class Vector2Control : InputControl<Vector2>
	{
		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x0004C900 File Offset: 0x0004AB00
		// (set) Token: 0x06000FFE RID: 4094 RVA: 0x0004C908 File Offset: 0x0004AB08
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000FFF RID: 4095 RVA: 0x0004C911 File Offset: 0x0004AB11
		// (set) Token: 0x06001000 RID: 4096 RVA: 0x0004C919 File Offset: 0x0004AB19
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x06001001 RID: 4097 RVA: 0x0004C922 File Offset: 0x0004AB22
		public Vector2Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector2;
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0004C93A File Offset: 0x0004AB3A
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			base.FinishSetup();
		}

		// Token: 0x06001003 RID: 4099 RVA: 0x0004C964 File Offset: 0x0004AB64
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				return *(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector2(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06001004 RID: 4100 RVA: 0x0004C9B4 File Offset: 0x0004ABB4
		public unsafe override void WriteValueIntoState(Vector2 value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				*(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.x.WriteValueIntoState(value.x, statePtr);
			this.y.WriteValueIntoState(value.y, statePtr);
		}

		// Token: 0x06001005 RID: 4101 RVA: 0x0004CA0C File Offset: 0x0004AC0C
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x06001006 RID: 4102 RVA: 0x0004CA28 File Offset: 0x0004AC28
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 64U && this.m_StateBlock.bitOffset == 0U && this.x.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.optimizedControlDataType == InputStateBlock.FormatFloat && this.y.m_StateBlock.byteOffset == this.x.m_StateBlock.byteOffset + 4U)
			{
				return InputStateBlock.FormatVector2;
			}
			return InputStateBlock.FormatInvalid;
		}
	}
}
