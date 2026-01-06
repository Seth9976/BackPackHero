using System;

namespace System.Net
{
	// Token: 0x0200037D RID: 893
	internal readonly struct SecurityStatusPal
	{
		// Token: 0x06001D65 RID: 7525 RVA: 0x0006B56E File Offset: 0x0006976E
		public SecurityStatusPal(SecurityStatusPalErrorCode errorCode, Exception exception = null)
		{
			this.ErrorCode = errorCode;
			this.Exception = exception;
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0006B580 File Offset: 0x00069780
		public override string ToString()
		{
			if (this.Exception != null)
			{
				return string.Format("{0}={1}, {2}={3}", new object[] { "ErrorCode", this.ErrorCode, "Exception", this.Exception });
			}
			return string.Format("{0}={1}", "ErrorCode", this.ErrorCode);
		}

		// Token: 0x04000F11 RID: 3857
		public readonly SecurityStatusPalErrorCode ErrorCode;

		// Token: 0x04000F12 RID: 3858
		public readonly Exception Exception;
	}
}
