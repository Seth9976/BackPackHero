using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004F RID: 79
	[InputControlLayout(displayName = "Humidity")]
	public class HumiditySensor : Sensor
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002C8C2 File Offset: 0x0002AAC2
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002C8CA File Offset: 0x0002AACA
		[InputControl(displayName = "Relative Humidity", noisy = true)]
		public AxisControl relativeHumidity { get; private set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x0002C8D3 File Offset: 0x0002AAD3
		// (set) Token: 0x060007F5 RID: 2037 RVA: 0x0002C8DA File Offset: 0x0002AADA
		public static HumiditySensor current { get; private set; }

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002C8E2 File Offset: 0x0002AAE2
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HumiditySensor.current = this;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HumiditySensor.current == this)
			{
				HumiditySensor.current = null;
			}
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002C906 File Offset: 0x0002AB06
		protected override void FinishSetup()
		{
			this.relativeHumidity = base.GetChildControl<AxisControl>("relativeHumidity");
			base.FinishSetup();
		}
	}
}
