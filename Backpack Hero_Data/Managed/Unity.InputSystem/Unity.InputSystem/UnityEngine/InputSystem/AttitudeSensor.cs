using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000049 RID: 73
	[InputControlLayout(stateType = typeof(AttitudeState), displayName = "Attitude")]
	public class AttitudeSensor : Sensor
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002C664 File Offset: 0x0002A864
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0002C66C File Offset: 0x0002A86C
		public QuaternionControl attitude { get; private set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x0002C675 File Offset: 0x0002A875
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x0002C67C File Offset: 0x0002A87C
		public static AttitudeSensor current { get; private set; }

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002C684 File Offset: 0x0002A884
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AttitudeSensor.current = this;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0002C692 File Offset: 0x0002A892
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AttitudeSensor.current == this)
			{
				AttitudeSensor.current = null;
			}
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x0002C6A8 File Offset: 0x0002A8A8
		protected override void FinishSetup()
		{
			this.attitude = base.GetChildControl<QuaternionControl>("attitude");
			base.FinishSetup();
		}
	}
}
