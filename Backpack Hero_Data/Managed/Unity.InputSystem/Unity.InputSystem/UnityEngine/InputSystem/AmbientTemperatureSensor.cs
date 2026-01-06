using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000050 RID: 80
	[InputControlLayout(displayName = "Ambient Temperature")]
	public class AmbientTemperatureSensor : Sensor
	{
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x0002C927 File Offset: 0x0002AB27
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x0002C92F File Offset: 0x0002AB2F
		[InputControl(displayName = "Ambient Temperature", noisy = true)]
		public AxisControl ambientTemperature { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x0002C938 File Offset: 0x0002AB38
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x0002C93F File Offset: 0x0002AB3F
		public static AmbientTemperatureSensor current { get; private set; }

		// Token: 0x060007FE RID: 2046 RVA: 0x0002C947 File Offset: 0x0002AB47
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AmbientTemperatureSensor.current = this;
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x0002C955 File Offset: 0x0002AB55
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AmbientTemperatureSensor.current == this)
			{
				AmbientTemperatureSensor.current = null;
			}
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x0002C96B File Offset: 0x0002AB6B
		protected override void FinishSetup()
		{
			this.ambientTemperature = base.GetChildControl<AxisControl>("ambientTemperature");
			base.FinishSetup();
		}
	}
}
