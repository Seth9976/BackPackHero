using System;
using Unity.Services.Core;
using UnityEngine.Scripting;

namespace Unity.Services.Analytics
{
	// Token: 0x0200001E RID: 30
	public class ConsentCheckException : RequestFailedException
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003654 File Offset: 0x00001854
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000365C File Offset: 0x0000185C
		[Preserve]
		public ConsentCheckExceptionReason Reason { get; private set; }

		// Token: 0x06000066 RID: 102 RVA: 0x00003665 File Offset: 0x00001865
		public ConsentCheckException(ConsentCheckExceptionReason reason, int errorCode, string message, Exception innerException)
			: base(errorCode, message, innerException)
		{
			this.Reason = reason;
		}
	}
}
