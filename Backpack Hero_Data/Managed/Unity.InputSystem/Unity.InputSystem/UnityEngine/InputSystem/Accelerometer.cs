using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000046 RID: 70
	[InputControlLayout(stateType = typeof(AccelerometerState))]
	public class Accelerometer : Sensor
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0002C535 File Offset: 0x0002A735
		// (set) Token: 0x060007AB RID: 1963 RVA: 0x0002C53D File Offset: 0x0002A73D
		public Vector3Control acceleration { get; private set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0002C546 File Offset: 0x0002A746
		// (set) Token: 0x060007AD RID: 1965 RVA: 0x0002C54D File Offset: 0x0002A74D
		public static Accelerometer current { get; private set; }

		// Token: 0x060007AE RID: 1966 RVA: 0x0002C555 File Offset: 0x0002A755
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			Accelerometer.current = this;
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0002C563 File Offset: 0x0002A763
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (Accelerometer.current == this)
			{
				Accelerometer.current = null;
			}
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0002C579 File Offset: 0x0002A779
		protected override void FinishSetup()
		{
			this.acceleration = base.GetChildControl<Vector3Control>("acceleration");
			base.FinishSetup();
		}
	}
}
