using System;
using System.Collections;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000133 RID: 307
	[UnitTitle("Wait For Seconds")]
	[UnitOrder(1)]
	public class WaitForSecondsUnit : WaitUnit
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000832 RID: 2098 RVA: 0x0000F489 File Offset: 0x0000D689
		// (set) Token: 0x06000833 RID: 2099 RVA: 0x0000F491 File Offset: 0x0000D691
		[DoNotSerialize]
		[PortLabel("Delay")]
		public ValueInput seconds { get; private set; }

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0000F49A File Offset: 0x0000D69A
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0000F4A2 File Offset: 0x0000D6A2
		[DoNotSerialize]
		[PortLabel("Unscaled")]
		public ValueInput unscaledTime { get; private set; }

		// Token: 0x06000836 RID: 2102 RVA: 0x0000F4AC File Offset: 0x0000D6AC
		protected override void Definition()
		{
			base.Definition();
			this.seconds = base.ValueInput<float>("seconds", 0f);
			this.unscaledTime = base.ValueInput<bool>("unscaledTime", false);
			base.Requirement(this.seconds, base.enter);
			base.Requirement(this.unscaledTime, base.enter);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0000F50B File Offset: 0x0000D70B
		protected override IEnumerator Await(Flow flow)
		{
			float value = flow.GetValue<float>(this.seconds);
			if (flow.GetValue<bool>(this.unscaledTime))
			{
				yield return new WaitForSecondsRealtime(value);
			}
			else
			{
				yield return new WaitForSeconds(value);
			}
			yield return base.exit;
			yield break;
		}
	}
}
