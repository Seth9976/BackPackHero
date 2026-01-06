using System;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace UnityEngine.InputSystem.Controls
{
	// Token: 0x0200011B RID: 283
	public class Vector2Control : InputControl<Vector2>
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x0004C8B4 File Offset: 0x0004AAB4
		// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x0004C8BC File Offset: 0x0004AABC
		[InputControl(offset = 0U, displayName = "X")]
		public AxisControl x { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x0004C8C5 File Offset: 0x0004AAC5
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x0004C8CD File Offset: 0x0004AACD
		[InputControl(offset = 4U, displayName = "Y")]
		public AxisControl y { get; set; }

		// Token: 0x06000FFC RID: 4092 RVA: 0x0004C8D6 File Offset: 0x0004AAD6
		public Vector2Control()
		{
			this.m_StateBlock.format = InputStateBlock.FormatVector2;
		}

		// Token: 0x06000FFD RID: 4093 RVA: 0x0004C8EE File Offset: 0x0004AAEE
		protected override void FinishSetup()
		{
			this.x = base.GetChildControl<AxisControl>("x");
			this.y = base.GetChildControl<AxisControl>("y");
			base.FinishSetup();
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x0004C918 File Offset: 0x0004AB18
		public unsafe override Vector2 ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1447379762)
			{
				return *(Vector2*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new Vector2(this.x.ReadUnprocessedValueFromStateWithCaching(statePtr), this.y.ReadUnprocessedValueFromStateWithCaching(statePtr));
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x0004C968 File Offset: 0x0004AB68
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

		// Token: 0x06001000 RID: 4096 RVA: 0x0004C9C0 File Offset: 0x0004ABC0
		public unsafe override float EvaluateMagnitude(void* statePtr)
		{
			return base.ReadValueFromStateWithCaching(statePtr).magnitude;
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0004C9DC File Offset: 0x0004ABDC
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
