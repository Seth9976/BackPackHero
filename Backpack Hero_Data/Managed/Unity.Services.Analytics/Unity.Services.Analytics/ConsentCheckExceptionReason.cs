using System;
using UnityEngine.Scripting;

namespace Unity.Services.Analytics
{
	// Token: 0x0200001D RID: 29
	[Preserve]
	public enum ConsentCheckExceptionReason
	{
		// Token: 0x04000099 RID: 153
		Unknown,
		// Token: 0x0400009A RID: 154
		DeserializationIssue,
		// Token: 0x0400009B RID: 155
		NoInternetConnection,
		// Token: 0x0400009C RID: 156
		InvalidConsentFlow,
		// Token: 0x0400009D RID: 157
		ConsentFlowNotKnown
	}
}
