using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000027 RID: 39
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
	public class IgnoreWarningAttribute : Attribute
	{
		// Token: 0x0600013A RID: 314 RVA: 0x0000796C File Offset: 0x00005B6C
		public IgnoreWarningAttribute(int warning)
		{
		}
	}
}
