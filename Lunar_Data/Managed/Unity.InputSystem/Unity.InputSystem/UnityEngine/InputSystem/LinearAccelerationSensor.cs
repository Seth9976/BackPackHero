using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004A RID: 74
	[InputControlLayout(stateType = typeof(LinearAccelerationState), displayName = "Linear Acceleration")]
	public class LinearAccelerationSensor : Sensor
	{
		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x0002C68D File Offset: 0x0002A88D
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x0002C695 File Offset: 0x0002A895
		public Vector3Control acceleration { get; private set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002C69E File Offset: 0x0002A89E
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0002C6A5 File Offset: 0x0002A8A5
		public static LinearAccelerationSensor current { get; private set; }

		// Token: 0x060007CC RID: 1996 RVA: 0x0002C6AD File Offset: 0x0002A8AD
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LinearAccelerationSensor.current = this;
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x0002C6BB File Offset: 0x0002A8BB
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LinearAccelerationSensor.current == this)
			{
				LinearAccelerationSensor.current = null;
			}
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x0002C6D1 File Offset: 0x0002A8D1
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
