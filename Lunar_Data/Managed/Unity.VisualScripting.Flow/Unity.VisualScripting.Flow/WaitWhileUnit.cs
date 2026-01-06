using System;
using System.Collections;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000136 RID: 310
	[UnitTitle("Wait While")]
	[UnitShortTitle("Wait While")]
	[UnitOrder(3)]
	public class WaitWhileUnit : WaitUnit
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000F5FC File Offset: 0x0000D7FC
		// (set) Token: 0x06000846 RID: 2118 RVA: 0x0000F604 File Offset: 0x0000D804
		[DoNotSerialize]
		public ValueInput condition { get; private set; }

		// Token: 0x06000847 RID: 2119 RVA: 0x0000F60D File Offset: 0x0000D80D
		protected override void Definition()
		{
			base.Definition();
			this.condition = base.ValueInput<bool>("condition");
			base.Requirement(this.condition, base.enter);
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0000F638 File Offset: 0x0000D838
		protected override IEnumerator Await(Flow flow)
		{
			yield return new WaitWhile(() => flow.GetValue<bool>(this.condition));
			yield return base.exit;
			yield break;
		}
	}
}
