using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000295 RID: 661
	internal interface IStyleDataGroup<T>
	{
		// Token: 0x0600168D RID: 5773
		T Copy();

		// Token: 0x0600168E RID: 5774
		void CopyFrom(ref T other);
	}
}
