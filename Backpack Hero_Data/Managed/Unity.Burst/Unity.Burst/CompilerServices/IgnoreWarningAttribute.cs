using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class IgnoreWarningAttribute : Attribute
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00007934 File Offset: 0x00005B34
		public IgnoreWarningAttribute(int warning)
		{
		}
	}
}
