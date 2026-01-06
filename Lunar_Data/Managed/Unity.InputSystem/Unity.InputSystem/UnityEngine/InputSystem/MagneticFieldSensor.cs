using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004B RID: 75
	[InputControlLayout(displayName = "Magnetic Field")]
	public class MagneticFieldSensor : Sensor
	{
		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0002C6F2 File Offset: 0x0002A8F2
		// (set) Token: 0x060007D1 RID: 2001 RVA: 0x0002C6FA File Offset: 0x0002A8FA
		[InputControl(displayName = "Magnetic Field", noisy = true)]
		public Vector3Control magneticField { get; private set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002C703 File Offset: 0x0002A903
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0002C70A File Offset: 0x0002A90A
		public static MagneticFieldSensor current { get; private set; }

		// Token: 0x060007D4 RID: 2004 RVA: 0x0002C712 File Offset: 0x0002A912
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			MagneticFieldSensor.current = this;
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002C720 File Offset: 0x0002A920
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (MagneticFieldSensor.current == this)
			{
				MagneticFieldSensor.current = null;
			}
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002C736 File Offset: 0x0002A936
		protected override void FinishSetup()
		{
			this.magneticField = base.GetChildControl<Vector3Control>("magneticField");
			base.FinishSetup();
		}
	}
}
