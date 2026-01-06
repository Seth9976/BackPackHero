using System;

namespace Unity.VisualScripting.FullSerializer
{
	// Token: 0x0200017A RID: 378
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
	public sealed class fsForwardAttribute : Attribute
	{
		// Token: 0x06000A1A RID: 2586 RVA: 0x0002A586 File Offset: 0x00028786
		public fsForwardAttribute(string memberName)
		{
			this.MemberName = memberName;
		}

		// Token: 0x0400025C RID: 604
		public string MemberName;
	}
}
