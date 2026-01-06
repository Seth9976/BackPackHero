using System;

namespace System.Net
{
	// Token: 0x02000373 RID: 883
	internal static class ExceptionCheck
	{
		// Token: 0x06001D10 RID: 7440 RVA: 0x0006978A File Offset: 0x0006798A
		internal static bool IsFatal(Exception exception)
		{
			return exception is OutOfMemoryException;
		}
	}
}
