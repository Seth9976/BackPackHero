using System;

namespace Unity.Services.Core
{
	// Token: 0x02000003 RID: 3
	public static class CommonErrorCodes
	{
		// Token: 0x04000002 RID: 2
		public const int Unknown = 0;

		// Token: 0x04000003 RID: 3
		public const int TransportError = 1;

		// Token: 0x04000004 RID: 4
		public const int Timeout = 2;

		// Token: 0x04000005 RID: 5
		public const int ServiceUnavailable = 3;

		// Token: 0x04000006 RID: 6
		public const int ApiMissing = 4;

		// Token: 0x04000007 RID: 7
		public const int RequestRejected = 5;

		// Token: 0x04000008 RID: 8
		public const int TooManyRequests = 50;

		// Token: 0x04000009 RID: 9
		public const int InvalidToken = 51;

		// Token: 0x0400000A RID: 10
		public const int TokenExpired = 52;

		// Token: 0x0400000B RID: 11
		public const int Forbidden = 53;

		// Token: 0x0400000C RID: 12
		public const int NotFound = 54;

		// Token: 0x0400000D RID: 13
		public const int InvalidRequest = 55;
	}
}
