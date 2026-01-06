using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004D RID: 77
	[InputControlLayout(displayName = "Pressure")]
	public class PressureSensor : Sensor
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		// (set) Token: 0x060007E3 RID: 2019 RVA: 0x0002C800 File Offset: 0x0002AA00
		[InputControl(displayName = "Atmospheric Pressure", noisy = true)]
		public AxisControl atmosphericPressure { get; private set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0002C809 File Offset: 0x0002AA09
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x0002C810 File Offset: 0x0002AA10
		public static PressureSensor current { get; private set; }

		// Token: 0x060007E6 RID: 2022 RVA: 0x0002C818 File Offset: 0x0002AA18
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			PressureSensor.current = this;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0002C826 File Offset: 0x0002AA26
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (PressureSensor.current == this)
			{
				PressureSensor.current = null;
			}
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0002C83C File Offset: 0x0002AA3C
		protected override void FinishSetup()
		{
			this.atmosphericPressure = base.GetChildControl<AxisControl>("atmosphericPressure");
			base.FinishSetup();
		}
	}
}
