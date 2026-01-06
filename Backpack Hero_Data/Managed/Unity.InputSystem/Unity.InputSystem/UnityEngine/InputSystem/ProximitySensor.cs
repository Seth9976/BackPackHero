using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004E RID: 78
	[InputControlLayout(displayName = "Proximity")]
	public class ProximitySensor : Sensor
	{
		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x0002C85D File Offset: 0x0002AA5D
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x0002C865 File Offset: 0x0002AA65
		[InputControl(displayName = "Distance", noisy = true)]
		public AxisControl distance { get; private set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x0002C86E File Offset: 0x0002AA6E
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x0002C875 File Offset: 0x0002AA75
		public static ProximitySensor current { get; private set; }

		// Token: 0x060007EE RID: 2030 RVA: 0x0002C87D File Offset: 0x0002AA7D
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			ProximitySensor.current = this;
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x0002C88B File Offset: 0x0002AA8B
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (ProximitySensor.current == this)
			{
				ProximitySensor.current = null;
			}
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x0002C8A1 File Offset: 0x0002AAA1
		protected override void FinishSetup()
		{
			this.distance = base.GetChildControl<AxisControl>("distance");
			base.FinishSetup();
		}
	}
}
