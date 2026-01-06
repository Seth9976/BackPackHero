using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002C RID: 44
	internal interface IAsyncOperationAwaiter : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B9 RID: 185
		bool IsCompleted { get; }

		// Token: 0x060000BA RID: 186
		void GetResult();
	}
}
