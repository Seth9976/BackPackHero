using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Http
{
	// Token: 0x02000002 RID: 2
	internal static class CancellationHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static bool ShouldWrapInOperationCanceledException(Exception exception, CancellationToken cancellationToken)
		{
			return !(exception is OperationCanceledException) && cancellationToken.IsCancellationRequested;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000263
		internal static Exception CreateOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			return new TaskCanceledException(CancellationHelper.s_cancellationMessage, innerException, cancellationToken);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002071 File Offset: 0x00000271
		private static void ThrowOperationCanceledException(Exception innerException, CancellationToken cancellationToken)
		{
			throw CancellationHelper.CreateOperationCanceledException(innerException, cancellationToken);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000207A File Offset: 0x0000027A
		internal static void ThrowIfCancellationRequested(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				CancellationHelper.ThrowOperationCanceledException(null, cancellationToken);
			}
		}

		// Token: 0x04000001 RID: 1
		private static readonly string s_cancellationMessage = new OperationCanceledException().Message;
	}
}
