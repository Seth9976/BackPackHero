using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000048 RID: 72
	[InputControlLayout(stateType = typeof(GravityState), displayName = "Gravity")]
	public class GravitySensor : Sensor
	{
		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002C5FF File Offset: 0x0002A7FF
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0002C607 File Offset: 0x0002A807
		public Vector3Control gravity { get; private set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0002C610 File Offset: 0x0002A810
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x0002C617 File Offset: 0x0002A817
		public static GravitySensor current { get; private set; }

		// Token: 0x060007BE RID: 1982 RVA: 0x0002C61F File Offset: 0x0002A81F
		protected override void FinishSetup()
		{
			this.gravity = base.GetChildControl<Vector3Control>("gravity");
			base.FinishSetup();
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0002C638 File Offset: 0x0002A838
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			GravitySensor.current = this;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0002C646 File Offset: 0x0002A846
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (GravitySensor.current == this)
			{
				GravitySensor.current = null;
			}
		}
	}
}
