using System;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200001F RID: 31
	internal struct WebRequest
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000321F File Offset: 0x0000141F
		public bool IsSuccess
		{
			get
			{
				return this.Result == WebRequestResult.Success;
			}
		}

		// Token: 0x0400003A RID: 58
		public WebRequestResult Result;

		// Token: 0x0400003B RID: 59
		public string ErrorMessage;

		// Token: 0x0400003C RID: 60
		public string ErrorBody;

		// Token: 0x0400003D RID: 61
		public long ResponseCode;
	}
}
