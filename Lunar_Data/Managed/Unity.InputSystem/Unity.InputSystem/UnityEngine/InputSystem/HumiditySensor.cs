using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004F RID: 79
	[InputControlLayout(displayName = "Humidity")]
	public class HumiditySensor : Sensor
	{
		// Token: 0x170001FE RID: 510
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0002C886 File Offset: 0x0002AA86
		// (set) Token: 0x060007F1 RID: 2033 RVA: 0x0002C88E File Offset: 0x0002AA8E
		[InputControl(displayName = "Relative Humidity", noisy = true)]
		public AxisControl relativeHumidity { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x0002C897 File Offset: 0x0002AA97
		// (set) Token: 0x060007F3 RID: 2035 RVA: 0x0002C89E File Offset: 0x0002AA9E
		public static HumiditySensor current { get; private set; }

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002C8A6 File Offset: 0x0002AAA6
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			HumiditySensor.current = this;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (HumiditySensor.current == this)
			{
				HumiditySensor.current = null;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002C8CA File Offset: 0x0002AACA
		protected override void FinishSetup()
		{
			this.relativeHumidity = base.GetChildControl<AxisControl>("relativeHumidity");
			base.FinishSetup();
		}
	}
}
