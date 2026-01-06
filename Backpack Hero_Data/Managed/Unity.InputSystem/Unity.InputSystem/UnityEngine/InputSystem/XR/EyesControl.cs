using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem.XR
{
	// Token: 0x0200006F RID: 111
	public class EyesControl : InputControl<Eyes>
	{
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x0003610E File Offset: 0x0003430E
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x00036116 File Offset: 0x00034316
		[InputControl(offset = 0U, displayName = "LeftEyePosition")]
		public Vector3Control leftEyePosition { get; private set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x0003611F File Offset: 0x0003431F
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x00036127 File Offset: 0x00034327
		[InputControl(offset = 12U, displayName = "LeftEyeRotation")]
		public QuaternionControl leftEyeRotation { get; private set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00036130 File Offset: 0x00034330
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x00036138 File Offset: 0x00034338
		[InputControl(offset = 28U, displayName = "RightEyePosition")]
		public Vector3Control rightEyePosition { get; private set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x00036141 File Offset: 0x00034341
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x00036149 File Offset: 0x00034349
		[InputControl(offset = 40U, displayName = "RightEyeRotation")]
		public QuaternionControl rightEyeRotation { get; private set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x00036152 File Offset: 0x00034352
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x0003615A File Offset: 0x0003435A
		[InputControl(offset = 56U, displayName = "FixationPoint")]
		public Vector3Control fixationPoint { get; private set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x00036163 File Offset: 0x00034363
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x0003616B File Offset: 0x0003436B
		[InputControl(offset = 68U, displayName = "LeftEyeOpenAmount")]
		public AxisControl leftEyeOpenAmount { get; private set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x00036174 File Offset: 0x00034374
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x0003617C File Offset: 0x0003437C
		[InputControl(offset = 72U, displayName = "RightEyeOpenAmount")]
		public AxisControl rightEyeOpenAmount { get; private set; }

		// Token: 0x060009FC RID: 2556 RVA: 0x00036188 File Offset: 0x00034388
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

		// Token: 0x060009FD RID: 2557 RVA: 0x00036214 File Offset: 0x00034414
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

		// Token: 0x060009FE RID: 2558 RVA: 0x000362B0 File Offset: 0x000344B0
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
