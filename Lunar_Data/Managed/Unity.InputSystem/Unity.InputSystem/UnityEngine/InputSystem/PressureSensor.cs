using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004D RID: 77
	[InputControlLayout(displayName = "Pressure")]
	public class PressureSensor : Sensor
	{
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0002C7C4 File Offset: 0x0002A9C4
		[InputControl(displayName = "Atmospheric Pressure", noisy = true)]
		public AxisControl atmosphericPressure { get; private set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002C7CD File Offset: 0x0002A9CD
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0002C7D4 File Offset: 0x0002A9D4
		public static PressureSensor current { get; private set; }

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002C7DC File Offset: 0x0002A9DC
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			PressureSensor.current = this;
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002C7EA File Offset: 0x0002A9EA
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (PressureSensor.current == this)
			{
				PressureSensor.current = null;
			}
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002C800 File Offset: 0x0002AA00
		protected override void FinishSetup()
		{
			this.atmosphericPressure = base.GetChildControl<AxisControl>("atmosphericPressure");
			base.FinishSetup();
		}
	}
}
