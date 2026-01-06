using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000051 RID: 81
	[InputControlLayout(displayName = "Step Counter")]
	public class StepCounter : Sensor
	{
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0002C950 File Offset: 0x0002AB50
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x0002C958 File Offset: 0x0002AB58
		[InputControl(displayName = "Step Counter", noisy = true)]
		public IntegerControl stepCounter { get; private set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002C961 File Offset: 0x0002AB61
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002C968 File Offset: 0x0002AB68
		public static StepCounter current { get; private set; }

		// Token: 0x06000804 RID: 2052 RVA: 0x0002C970 File Offset: 0x0002AB70
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			StepCounter.current = this;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x0002C97E File Offset: 0x0002AB7E
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (StepCounter.current == this)
			{
				StepCounter.current = null;
			}
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x0002C994 File Offset: 0x0002AB94
		protected override void FinishSetup()
		{
			this.stepCounter = base.GetChildControl<IntegerControl>("stepCounter");
			base.FinishSetup();
		}
	}
}
