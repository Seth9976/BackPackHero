using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x0200004C RID: 76
	[InputControlLayout(displayName = "Light")]
	public class LightSensor : Sensor
	{
		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0002C757 File Offset: 0x0002A957
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0002C75F File Offset: 0x0002A95F
		[InputControl(displayName = "Light Level", noisy = true)]
		public AxisControl lightLevel { get; private set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0002C768 File Offset: 0x0002A968
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0002C76F File Offset: 0x0002A96F
		public static LightSensor current { get; private set; }

		// Token: 0x060007DC RID: 2012 RVA: 0x0002C777 File Offset: 0x0002A977
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			LightSensor.current = this;
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0002C785 File Offset: 0x0002A985
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (LightSensor.current == this)
			{
				LightSensor.current = null;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0002C79B File Offset: 0x0002A99B
		protected override void FinishSetup()
		{
			this.lightLevel = base.GetChildControl<AxisControl>("lightLevel");
			base.FinishSetup();
		}
	}
}
