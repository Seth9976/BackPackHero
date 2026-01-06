using System;
using System.Runtime.CompilerServices;

namespace Unity.Services.Core.Internal
{
	// Token: 0x0200002D RID: 45
	internal interface IAsyncOperationAwaiter<out T> : ICriticalNotifyCompletion, INotifyCompletion
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BB RID: 187
		bool IsCompleted { get; }

		// Token: 0x060000BC RID: 188
		T GetResult();
	}
}
