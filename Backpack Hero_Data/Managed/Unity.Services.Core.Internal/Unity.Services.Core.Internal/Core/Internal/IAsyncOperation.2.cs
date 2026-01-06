using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002B RID: 43
	internal interface IAsyncOperation<out T> : IEnumerator
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000B3 RID: 179
		bool IsDone { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B4 RID: 180
		AsyncOperationStatus Status { get; }

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x060000B5 RID: 181
		// (remove) Token: 0x060000B6 RID: 182
		event Action<IAsyncOperation<T>> Completed;

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183
		Exception Exception { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B8 RID: 184
		T Result { get; }
	}
}
