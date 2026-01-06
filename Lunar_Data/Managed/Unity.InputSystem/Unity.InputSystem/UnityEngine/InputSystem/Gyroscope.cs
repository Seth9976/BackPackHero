using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000047 RID: 71
	[InputControlLayout(stateType = typeof(GyroscopeState))]
	public class Gyroscope : Sensor
	{
		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007B0 RID: 1968 RVA: 0x0002C55E File Offset: 0x0002A75E
		// (set) Token: 0x060007B1 RID: 1969 RVA: 0x0002C566 File Offset: 0x0002A766
		public Vector3Control angularVelocity { get; private set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0002C56F File Offset: 0x0002A76F
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0002C576 File Offset: 0x0002A776
		public static Gyroscope current { get; private set; }

		// Token: 0x060007B4 RID: 1972 RVA: 0x0002C57E File Offset: 0x0002A77E
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gyroscope.current = this;
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0002C58C File Offset: 0x0002A78C
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Gyroscope.current == this)
			{
				Gyroscope.current = null;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002C5A2 File Offset: 0x0002A7A2
		protected override void FinishSetup()
		{
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}
	}
}
