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
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0002C59A File Offset: 0x0002A79A
		// (set) Token: 0x060007B3 RID: 1971 RVA: 0x0002C5A2 File Offset: 0x0002A7A2
		public Vector3Control angularVelocity { get; private set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0002C5AB File Offset: 0x0002A7AB
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x0002C5B2 File Offset: 0x0002A7B2
		public static Gyroscope current { get; private set; }

		// Token: 0x060007B6 RID: 1974 RVA: 0x0002C5BA File Offset: 0x0002A7BA
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Gyroscope.current = this;
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0002C5C8 File Offset: 0x0002A7C8
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Gyroscope.current == this)
			{
				Gyroscope.current = null;
			}
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0002C5DE File Offset: 0x0002A7DE
		protected override void FinishSetup()
		{
			this.angularVelocity = base.GetChildControl<Vector3Control>("angularVelocity");
			base.FinishSetup();
		}
	}
}
