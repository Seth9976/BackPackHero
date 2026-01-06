using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000D2 RID: 210
	[AttributeUsage(64)]
	[Obsolete("Use [ContractAnnotation('=> halt')] instead")]
	public sealed class TerminatesProgramAttribute : Attribute
	{
	}
}
