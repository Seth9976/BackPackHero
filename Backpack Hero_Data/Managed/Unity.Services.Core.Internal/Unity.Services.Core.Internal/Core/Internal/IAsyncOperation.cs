using System;
using System.Collections;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002A RID: 42
	internal interface IAsyncOperation : IEnumerator
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000AE RID: 174
		bool IsDone { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AF RID: 175
		AsyncOperationStatus Status { get; }

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x060000B0 RID: 176
		// (remove) Token: 0x060000B1 RID: 177
		event Action<IAsyncOperation> Completed;

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B2 RID: 178
		Exception Exception { get; }
	}
}
