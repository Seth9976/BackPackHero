using System;

namespace Unity.Collections
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public class NotBurstCompatibleAttribute : Attribute
	{
	}
}
