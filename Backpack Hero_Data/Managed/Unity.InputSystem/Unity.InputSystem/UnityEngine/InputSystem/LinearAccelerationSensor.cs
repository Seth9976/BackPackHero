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
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x0002C6C9 File Offset: 0x0002A8C9
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x0002C6D1 File Offset: 0x0002A8D1
		public Vector3Control acceleration { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0002C6DA File Offset: 0x0002A8DA
		// (set) Token: 0x060007CD RID: 1997 RVA: 0x0002C6E1 File Offset: 0x0002A8E1
		public static LinearAccelerationSensor current { get; private set; }

		// Token: 0x060007CE RID: 1998 RVA: 0x0002C6E9 File Offset: 0x0002A8E9
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LinearAccelerationSensor.current = this;
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x0002C6F7 File Offset: 0x0002A8F7
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LinearAccelerationSensor.current == this)
			{
				LinearAccelerationSensor.current = null;
			}
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x0002C70D File Offset: 0x0002A90D
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
