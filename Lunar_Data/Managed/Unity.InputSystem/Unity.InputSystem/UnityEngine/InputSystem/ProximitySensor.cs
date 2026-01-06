using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004E RID: 78
	[InputControlLayout(displayName = "Proximity")]
	public class ProximitySensor : Sensor
	{
		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x0002C821 File Offset: 0x0002AA21
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x0002C829 File Offset: 0x0002AA29
		[InputControl(displayName = "Distance", noisy = true)]
		public AxisControl distance { get; private set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002C832 File Offset: 0x0002AA32
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0002C839 File Offset: 0x0002AA39
		public static ProximitySensor current { get; private set; }

		// Token: 0x060007EC RID: 2028 RVA: 0x0002C841 File Offset: 0x0002AA41
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			ProximitySensor.current = this;
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x0002C84F File Offset: 0x0002AA4F
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (ProximitySensor.current == this)
			{
				ProximitySensor.current = null;
			}
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x0002C865 File Offset: 0x0002AA65
		protected override void FinishSetup()
		{
			this.distance = base.GetChildControl<AxisControl>("distance");
			base.FinishSetup();
		}
	}
}
