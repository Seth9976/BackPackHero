using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.Scripting;
using UnityEngine.XR;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x02000061 RID: 97
	[Preserve]
	[InputControlLayout(stateType = typeof(PoseState))]
	public class PoseControl : InputControl<PoseState>
	{
		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600096F RID: 2415 RVA: 0x000349D2 File Offset: 0x00032BD2
		// (set) Token: 0x06000970 RID: 2416 RVA: 0x000349DA File Offset: 0x00032BDA
		public ButtonControl isTracked { get; private set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000971 RID: 2417 RVA: 0x000349E3 File Offset: 0x00032BE3
		// (set) Token: 0x06000972 RID: 2418 RVA: 0x000349EB File Offset: 0x00032BEB
		public IntegerControl trackingState { get; private set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000973 RID: 2419 RVA: 0x000349F4 File Offset: 0x00032BF4
		// (set) Token: 0x06000974 RID: 2420 RVA: 0x000349FC File Offset: 0x00032BFC
		public Vector3Control position { get; private set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x00034A05 File Offset: 0x00032C05
		// (set) Token: 0x06000976 RID: 2422 RVA: 0x00034A0D File Offset: 0x00032C0D
		public QuaternionControl rotation { get; private set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000977 RID: 2423 RVA: 0x00034A16 File Offset: 0x00032C16
		// (set) Token: 0x06000978 RID: 2424 RVA: 0x00034A1E File Offset: 0x00032C1E
		public Vector3Control velocity { get; private set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000979 RID: 2425 RVA: 0x00034A27 File Offset: 0x00032C27
		// (set) Token: 0x0600097A RID: 2426 RVA: 0x00034A2F File Offset: 0x00032C2F
		public Vector3Control angularVelocity { get; private set; }

		// Token: 0x0600097B RID: 2427 RVA: 0x00034A38 File Offset: 0x00032C38
		public PoseControl()
		{
			this.m_StateBlock.format = PoseState.s_Format;
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x00034A50 File Offset: 0x00032C50
		protected override void FinishSetup()
		{
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.position = base.GetChildControl<Vector3Control>("position");
			this.rotation = base.GetChildControl<QuaternionControl>("rotation");
			this.velocity = base.GetChildControl<Vector3Control>("velocity");
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x00034ACC File Offset: 0x00032CCC
		public unsafe override PoseState ReadUnprocessedValueFromState(void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				return *(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset);
			}
			return new PoseState
			{
				isTracked = (this.isTracked.ReadUnprocessedValueFromStateWithCaching(statePtr) > 0.5f),
				trackingState = (InputTrackingState)this.trackingState.ReadUnprocessedValueFromStateWithCaching(statePtr),
				position = this.position.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rotation = this.rotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				velocity = this.velocity.ReadUnprocessedValueFromStateWithCaching(statePtr),
				angularVelocity = this.angularVelocity.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x00034B80 File Offset: 0x00032D80
		public unsafe override void WriteValueIntoState(PoseState value, void* statePtr)
		{
			if (this.m_OptimizedControlDataType == 1349481317)
			{
				*(PoseState*)((byte*)statePtr + this.m_StateBlock.byteOffset) = value;
				return;
			}
			this.isTracked.WriteValueIntoState(value.isTracked, statePtr);
			this.trackingState.WriteValueIntoState((uint)value.trackingState, statePtr);
			this.position.WriteValueIntoState(value.position, statePtr);
			this.rotation.WriteValueIntoState(value.rotation, statePtr);
			this.velocity.WriteValueIntoState(value.velocity, statePtr);
			this.angularVelocity.WriteValueIntoState(value.angularVelocity, statePtr);
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x00034C20 File Offset: 0x00032E20
		protected override FourCC CalculateOptimizedControlDataType()
		{
			if (this.m_StateBlock.sizeInBits == 480U && this.m_StateBlock.bitOffset == 0U && this.isTracked.optimizedControlDataType == 1113150533 && this.trackingState.optimizedControlDataType == 1229870112 && this.position.optimizedControlDataType == 1447379763 && this.rotation.optimizedControlDataType == 1364541780 && this.velocity.optimizedControlDataType == 1447379763 && this.angularVelocity.optimizedControlDataType == 1447379763 && this.trackingState.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 4U && this.position.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 8U && this.rotation.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 20U && this.velocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 36U && this.angularVelocity.m_StateBlock.byteOffset == this.isTracked.m_StateBlock.byteOffset + 48U)
			{
				return 1349481317;
			}
			return 0;
		}
	}
}
