using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004B RID: 75
	[InputControlLayout(displayName = "Magnetic Field")]
	public class MagneticFieldSensor : Sensor
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0002C72E File Offset: 0x0002A92E
		// (set) Token: 0x060007D3 RID: 2003 RVA: 0x0002C736 File Offset: 0x0002A936
		[InputControl(displayName = "Magnetic Field", noisy = true)]
		public Vector3Control magneticField { get; private set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0002C73F File Offset: 0x0002A93F
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0002C746 File Offset: 0x0002A946
		public static MagneticFieldSensor current { get; private set; }

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002C74E File Offset: 0x0002A94E
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			MagneticFieldSensor.current = this;
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x0002C75C File Offset: 0x0002A95C
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (MagneticFieldSensor.current == this)
			{
				MagneticFieldSensor.current = null;
			}
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x0002C772 File Offset: 0x0002A972
		protected override void FinishSetup()
		{
			this.magneticField = base.GetChildControl<Vector3Control>("magneticField");
			base.FinishSetup();
		}
	}
}
