using System;
using System.Reflection;

namespace Unity.VisualScripting
{
	// Token: 0x02000152 RID: 338
	public static class ExceptionUtility
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x000279AC File Offset: 0x00025BAC
		public static Exception Relevant(this Exception ex)
		{
			if (ex is TargetInvocationException)
			{
				return ex.InnerException;
			}
			return ex;
		}
	}
}
