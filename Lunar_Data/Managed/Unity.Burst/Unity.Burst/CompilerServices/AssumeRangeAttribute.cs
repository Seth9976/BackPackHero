using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class AssumeRangeAttribute : Attribute
	{
		// Token: 0x06000133 RID: 307 RVA: 0x0000794E File Offset: 0x00005B4E
		public AssumeRangeAttribute(long min, long max)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00007956 File Offset: 0x00005B56
		public AssumeRangeAttribute(ulong min, ulong max)
		{
		}
	}
}
