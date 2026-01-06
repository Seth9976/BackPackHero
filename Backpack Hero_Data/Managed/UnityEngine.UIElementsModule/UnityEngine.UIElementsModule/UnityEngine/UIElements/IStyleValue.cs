using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200028C RID: 652
	internal interface IStyleValue<T>
	{
		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001572 RID: 5490
		// (set) Token: 0x06001573 RID: 5491
		T value { get; set; }

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x06001574 RID: 5492
		// (set) Token: 0x06001575 RID: 5493
		StyleKeyword keyword { get; set; }
	}
}
