using System;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023E RID: 574
	internal class SNIError
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x000837A4 File Offset: 0x000819A4
		public SNIError(SNIProviders provider, uint nativeError, uint sniErrorCode, string errorMessage)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = nativeError;
			this.sniError = sniErrorCode;
			this.errorMessage = errorMessage;
			this.exception = null;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000837E4 File Offset: 0x000819E4
		public SNIError(SNIProviders provider, uint sniErrorCode, Exception sniException)
		{
			this.lineNumber = 0U;
			this.function = string.Empty;
			this.provider = provider;
			this.nativeError = 0U;
			this.sniError = sniErrorCode;
			this.errorMessage = string.Empty;
			this.exception = sniException;
		}

		// Token: 0x040012EA RID: 4842
		public readonly SNIProviders provider;

		// Token: 0x040012EB RID: 4843
		public readonly string errorMessage;

		// Token: 0x040012EC RID: 4844
		public readonly uint nativeError;

		// Token: 0x040012ED RID: 4845
		public readonly uint sniError;

		// Token: 0x040012EE RID: 4846
		public readonly string function;

		// Token: 0x040012EF RID: 4847
		public readonly uint lineNumber;

		// Token: 0x040012F0 RID: 4848
		public readonly Exception exception;
	}
}
