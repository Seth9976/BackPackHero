using System;

namespace Unity.Services.Core
{
	// Token: 0x0200000A RID: 10
	public class RequestFailedException : Exception
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002230 File Offset: 0x00000430
		public int ErrorCode { get; }

		// Token: 0x06000021 RID: 33 RVA: 0x00002238 File Offset: 0x00000438
		public RequestFailedException(int errorCode, string message)
			: this(errorCode, message, null)
		{
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002243 File Offset: 0x00000443
		public RequestFailedException(int errorCode, string message, Exception innerException)
			: base(message, innerException)
		{
			this.ErrorCode = errorCode;
		}
	}
}
