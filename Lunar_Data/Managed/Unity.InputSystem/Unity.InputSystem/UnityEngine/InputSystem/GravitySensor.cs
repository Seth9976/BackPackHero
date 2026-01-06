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
		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0002C5C3 File Offset: 0x0002A7C3
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x0002C5CB File Offset: 0x0002A7CB
		public Vector3Control gravity { get; private set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x0002C5DB File Offset: 0x0002A7DB
		public static GravitySensor current { get; private set; }

		// Token: 0x060007BC RID: 1980 RVA: 0x0002C5E3 File Offset: 0x0002A7E3
		protected override void FinishSetup()
		{
			this.gravity = base.GetChildControl<Vector3Control>("gravity");
			base.FinishSetup();
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0002C5FC File Offset: 0x0002A7FC
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			GravitySensor.current = this;
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0002C60A File Offset: 0x0002A80A
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
