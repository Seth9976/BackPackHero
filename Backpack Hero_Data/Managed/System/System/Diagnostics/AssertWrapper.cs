using System;

namespace System.Diagnostics
{
	// Token: 0x02000248 RID: 584
	internal class AssertWrapper
	{
		// Token: 0x0600120E RID: 4622 RVA: 0x0004E1D6 File Offset: 0x0004C3D6
		public static void ShowAssert(string stackTrace, StackFrame frame, string message, string detailMessage)
		{
			new DefaultTraceListener().Fail(message, detailMessage);
		}
	}
}
