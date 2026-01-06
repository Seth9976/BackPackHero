using System;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace Unity.XR.Oculus.Input
{
	// Token: 0x0200000C RID: 12
	public class OculusTrackingReference : TrackedDevice
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00002B8C File Offset: 0x00000D8C
		// (set) Token: 0x060000AE RID: 174 RVA: 0x00002B94 File Offset: 0x00000D94
		[InputControl(aliases = new string[] { "trackingReferenceTrackingState" })]
		public new IntegerControl trackingState { get; private set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002B9D File Offset: 0x00000D9D
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x00002BA5 File Offset: 0x00000DA5
		[InputControl(aliases = new string[] { "trackingReferenceIsTracked" })]
		public new ButtonControl isTracked { get; private set; }

		// Token: 0x060000B1 RID: 177 RVA: 0x00002BAE File Offset: 0x00000DAE
		protected override void FinishSetup()
		{
			base.FinishSetup();
			this.trackingState = base.GetChildControl<IntegerControl>("trackingState");
			this.isTracked = base.GetChildControl<ButtonControl>("isTracked");
		}
	}
}
