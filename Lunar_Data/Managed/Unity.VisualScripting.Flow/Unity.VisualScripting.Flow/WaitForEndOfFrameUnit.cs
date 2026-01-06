using System;
using System.Collections;
using UnityEngine;

namespace Unity.VisualScripting
{
	// Token: 0x02000130 RID: 304
	[UnitTitle("Wait For End of Frame")]
	[UnitOrder(5)]
	public class WaitForEndOfFrameUnit : WaitUnit
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0000F25D File Offset: 0x0000D45D
		protected override IEnumerator Await(Flow flow)
		{
			yield return new WaitForEndOfFrame();
			yield return base.exit;
			yield break;
		}
	}
}
