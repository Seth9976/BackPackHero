using System;
using System.Collections;

namespace Unity.VisualScripting
{
	// Token: 0x02000132 RID: 306
	[UnitTitle("Wait For Next Frame")]
	[UnitOrder(4)]
	public class WaitForNextFrameUnit : WaitUnit
	{
		// Token: 0x06000830 RID: 2096 RVA: 0x0000F472 File Offset: 0x0000D672
		protected override IEnumerator Await(Flow flow)
		{
			yield return null;
			yield return base.exit;
			yield break;
		}
	}
}
