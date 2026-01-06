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
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0002C628 File Offset: 0x0002A828
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0002C630 File Offset: 0x0002A830
		public QuaternionControl attitude { get; private set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0002C639 File Offset: 0x0002A839
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0002C640 File Offset: 0x0002A840
		public static AttitudeSensor current { get; private set; }

		// Token: 0x060007C4 RID: 1988 RVA: 0x0002C648 File Offset: 0x0002A848
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			AttitudeSensor.current = this;
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x0002C656 File Offset: 0x0002A856
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (AttitudeSensor.current == this)
			{
				AttitudeSensor.current = null;
			}
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x0002C66C File Offset: 0x0002A86C
		protected override void FinishSetup()
		{
			this.attitude = base.GetChildControl<QuaternionControl>("attitude");
			base.FinishSetup();
		}
	}
}
