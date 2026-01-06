using System;
using System.Collections;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000135 RID: 309
	[UnitTitle("Wait Until")]
	[UnitShortTitle("Wait Until")]
	[UnitOrder(2)]
	public class WaitUntilUnit : WaitUnit
	{
		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x0000F5A2 File Offset: 0x0000D7A2
		// (set) Token: 0x06000841 RID: 2113 RVA: 0x0000F5AA File Offset: 0x0000D7AA
		[DoNotSerialize]
		public ValueInput condition { get; private set; }

		// Token: 0x06000842 RID: 2114 RVA: 0x0000F5B3 File Offset: 0x0000D7B3
		protected override void Definition()
		{
			base.Definition();
			this.condition = base.ValueInput<bool>("condition");
			base.Requirement(this.condition, base.enter);
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0000F5DE File Offset: 0x0000D7DE
		protected override IEnumerator Await(Flow flow)
		{
			yield return new WaitUntil(() => flow.GetValue<bool>(this.condition));
			yield return base.exit;
			yield break;
		}
	}
}
