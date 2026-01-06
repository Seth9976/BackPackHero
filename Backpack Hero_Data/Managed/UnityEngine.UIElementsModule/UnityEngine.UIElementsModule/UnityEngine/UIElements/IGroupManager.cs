using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000037 RID: 55
	internal interface IGroupManager
	{
		// Token: 0x06000151 RID: 337
		IGroupBoxOption GetSelectedOption();

		// Token: 0x06000152 RID: 338
		void OnOptionSelectionChanged(IGroupBoxOption selectedOption);

		// Token: 0x06000153 RID: 339
		void RegisterOption(IGroupBoxOption option);

		// Token: 0x06000154 RID: 340
		void UnregisterOption(IGroupBoxOption option);
	}
}
