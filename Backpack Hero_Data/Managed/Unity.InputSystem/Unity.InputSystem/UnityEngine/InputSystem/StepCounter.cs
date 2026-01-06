using System;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace UnityEngine.InputSystem
{
	// Token: 0x02000051 RID: 81
	[InputControlLayout(displayName = "Step Counter")]
	public class StepCounter : Sensor
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0002C98C File Offset: 0x0002AB8C
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x0002C994 File Offset: 0x0002AB94
		[InputControl(displayName = "Step Counter", noisy = true)]
		public IntegerControl stepCounter { get; private set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0002C99D File Offset: 0x0002AB9D
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x0002C9A4 File Offset: 0x0002ABA4
		public static StepCounter current { get; private set; }

		// Token: 0x06000806 RID: 2054 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		public override void MakeCurrent()
		{
			base.MakeCurrent();
			StepCounter.current = this;
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x0002C9BA File Offset: 0x0002ABBA
		protected override void OnRemoved()
		{
			base.OnRemoved();
			if (StepCounter.current == this)
			{
				StepCounter.current = null;
			}
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002C9D0 File Offset: 0x0002ABD0
		protected override void FinishSetup()
		{
			this.stepCounter = base.GetChildControl<IntegerControl>("stepCounter");
			base.FinishSetup();
		}
	}
}
