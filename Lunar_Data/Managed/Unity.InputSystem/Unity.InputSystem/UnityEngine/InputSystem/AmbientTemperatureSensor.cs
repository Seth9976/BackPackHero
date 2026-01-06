using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000050 RID: 80
	[InputControlLayout(displayName = "Ambient Temperature")]
	public class AmbientTemperatureSensor : Sensor
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x0002C8EB File Offset: 0x0002AAEB
		// (set) Token: 0x060007F9 RID: 2041 RVA: 0x0002C8F3 File Offset: 0x0002AAF3
		[InputControl(displayName = "Ambient Temperature", noisy = true)]
		public AxisControl ambientTemperature { get; private set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002C8FC File Offset: 0x0002AAFC
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002C903 File Offset: 0x0002AB03
		public static AmbientTemperatureSensor current { get; private set; }

		// Token: 0x060007FC RID: 2044 RVA: 0x0002C90B File Offset: 0x0002AB0B
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AmbientTemperatureSensor.current = this;
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x0002C919 File Offset: 0x0002AB19
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AmbientTemperatureSensor.current == this)
			{
				AmbientTemperatureSensor.current = null;
			}
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002C92F File Offset: 0x0002AB2F
		protected override void FinishSetup()
		{
			this.ambientTemperature = base.GetChildControl<AxisControl>("ambientTemperature");
			base.FinishSetup();
		}
	}
}
