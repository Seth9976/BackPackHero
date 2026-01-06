using System;
using System.Threading.Tasks;

namespace Unity.Services.Core.Internal
{
	// Token: 0x02000028 RID: 40
	internal static class AsyncOperationExtensions
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000279C File Offset: 0x0000099C
		public static AsyncOperationAwaiter GetAwaiter(this IAsyncOperation self)
		{
			return new AsyncOperationAwaiter(self);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000027A4 File Offset: 0x000009A4
		public static Task AsTask(this IAsyncOperation self)
		{
			AsyncOperationExtensions.<>c__DisplayClass1_0 CS$<>8__locals1 = new AsyncOperationExtensions.<>c__DisplayClass1_0();
			if (self.Status == AsyncOperationStatus.Succeeded)
			{
				return Task.CompletedTask;
			}
			CS$<>8__locals1.taskCompletionSource = new TaskCompletionSource<object>();
			if (self.IsDone)
			{
				CS$<>8__locals1.<AsTask>g__CompleteTask|0(self);
			}
			else
			{
				self.Completed += CS$<>8__locals1.<AsTask>g__CompleteTask|0;
			}
			return CS$<>8__locals1.taskCompletionSource.Task;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000027FF File Offset: 0x000009FF
		public static AsyncOperationAwaiter<T> GetAwaiter<T>(this IAsyncOperation<T> self)
		{
			return new AsyncOperationAwaiter<T>(self);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00002808 File Offset: 0x00000A08
		public static Task<T> AsTask<T>(this IAsyncOperation<T> self)
		{
			AsyncOperationExtensions.<>c__DisplayClass3_0<T> CS$<>8__locals1 = new AsyncOperationExtensions.<>c__DisplayClass3_0<T>();
			CS$<>8__locals1.taskCompletionSource = new TaskCompletionSource<T>();
			if (self.IsDone)
			{
				CS$<>8__locals1.<AsTask>g__CompleteTask|0(self);
			}
			else
			{
				self.Completed += CS$<>8__locals1.<AsTask>g__CompleteTask|0;
			}
			return CS$<>8__locals1.taskCompletionSource.Task;
		}
	}
}
