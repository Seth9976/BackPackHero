using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004C RID: 76
	[InputControlLayout(displayName = "Light")]
	public class LightSensor : Sensor
	{
		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002C793 File Offset: 0x0002A993
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0002C79B File Offset: 0x0002A99B
		[InputControl(displayName = "Light Level", noisy = true)]
		public AxisControl lightLevel { get; private set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0002C7A4 File Offset: 0x0002A9A4
		// (set) Token: 0x060007DD RID: 2013 RVA: 0x0002C7AB File Offset: 0x0002A9AB
		public static LightSensor current { get; private set; }

		// Token: 0x060007DE RID: 2014 RVA: 0x0002C7B3 File Offset: 0x0002A9B3
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LightSensor.current = this;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0002C7C1 File Offset: 0x0002A9C1
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LightSensor.current == this)
			{
				LightSensor.current = null;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0002C7D7 File Offset: 0x0002A9D7
		protected override void FinishSetup()
		{
			this.lightLevel = base.GetChildControl<AxisControl>("lightLevel");
			base.FinishSetup();
		}
	}
}
