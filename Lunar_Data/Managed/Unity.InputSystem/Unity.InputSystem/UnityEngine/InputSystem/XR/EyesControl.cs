using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006F RID: 111
	public class EyesControl : InputControl<Eyes>
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000360D2 File Offset: 0x000342D2
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000360DA File Offset: 0x000342DA
		[InputControl(offset = 0U, displayName = "LeftEyePosition")]
		public Vector3Control leftEyePosition { get; private set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x000360E3 File Offset: 0x000342E3
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x000360EB File Offset: 0x000342EB
		[InputControl(offset = 12U, displayName = "LeftEyeRotation")]
		public QuaternionControl leftEyeRotation { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000360F4 File Offset: 0x000342F4
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000360FC File Offset: 0x000342FC
		[InputControl(offset = 28U, displayName = "RightEyePosition")]
		public Vector3Control rightEyePosition { get; private set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00036105 File Offset: 0x00034305
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x0003610D File Offset: 0x0003430D
		[InputControl(offset = 40U, displayName = "RightEyeRotation")]
		public QuaternionControl rightEyeRotation { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00036116 File Offset: 0x00034316
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x0003611E File Offset: 0x0003431E
		[InputControl(offset = 56U, displayName = "FixationPoint")]
		public Vector3Control fixationPoint { get; private set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00036127 File Offset: 0x00034327
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0003612F File Offset: 0x0003432F
		[InputControl(offset = 68U, displayName = "LeftEyeOpenAmount")]
		public AxisControl leftEyeOpenAmount { get; private set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00036138 File Offset: 0x00034338
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x00036140 File Offset: 0x00034340
		[InputControl(offset = 72U, displayName = "RightEyeOpenAmount")]
		public AxisControl rightEyeOpenAmount { get; private set; }

		// Token: 0x060009FA RID: 2554 RVA: 0x0003614C File Offset: 0x0003434C
		protected override void FinishSetup()
		{
			this.leftEyePosition = base.GetChildControl<Vector3Control>("leftEyePosition");
			this.leftEyeRotation = base.GetChildControl<QuaternionControl>("leftEyeRotation");
			this.rightEyePosition = base.GetChildControl<Vector3Control>("rightEyePosition");
			this.rightEyeRotation = base.GetChildControl<QuaternionControl>("rightEyeRotation");
			this.fixationPoint = base.GetChildControl<Vector3Control>("fixationPoint");
			this.leftEyeOpenAmount = base.GetChildControl<AxisControl>("leftEyeOpenAmount");
			this.rightEyeOpenAmount = base.GetChildControl<AxisControl>("rightEyeOpenAmount");
			base.FinishSetup();
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000361D8 File Offset: 0x000343D8
		public unsafe override Eyes ReadUnprocessedValueFromState(void* statePtr)
		{
			return new Eyes
			{
				leftEyePosition = this.leftEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeRotation = this.leftEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyePosition = this.rightEyePosition.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeRotation = this.rightEyeRotation.ReadUnprocessedValueFromStateWithCaching(statePtr),
				fixationPoint = this.fixationPoint.ReadUnprocessedValueFromStateWithCaching(statePtr),
				leftEyeOpenAmount = this.leftEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr),
				rightEyeOpenAmount = this.rightEyeOpenAmount.ReadUnprocessedValueFromStateWithCaching(statePtr)
			};
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x00036274 File Offset: 0x00034474
		public unsafe override void WriteValueIntoState(Eyes value, void* statePtr)
		{
			this.leftEyePosition.WriteValueIntoState(value.leftEyePosition, statePtr);
			this.leftEyeRotation.WriteValueIntoState(value.leftEyeRotation, statePtr);
			this.rightEyePosition.WriteValueIntoState(value.rightEyePosition, statePtr);
			this.rightEyeRotation.WriteValueIntoState(value.rightEyeRotation, statePtr);
			this.fixationPoint.WriteValueIntoState(value.fixationPoint, statePtr);
			this.leftEyeOpenAmount.WriteValueIntoState(value.leftEyeOpenAmount, statePtr);
			this.rightEyeOpenAmount.WriteValueIntoState(value.rightEyeOpenAmount, statePtr);
		}
	}
}
