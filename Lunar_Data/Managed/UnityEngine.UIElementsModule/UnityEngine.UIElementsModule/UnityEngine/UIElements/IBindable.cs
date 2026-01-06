using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000123 RID: 291
	public interface IBindable
	{
		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060009D1 RID: 2513
		// (set) Token: 0x060009D2 RID: 2514
		IBinding binding { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060009D3 RID: 2515
		// (set) Token: 0x060009D4 RID: 2516
		string bindingPath { get; set; }
	}
}
