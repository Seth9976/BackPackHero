using System;

namespace System.Data.SqlClient
{
	// Token: 0x02000206 RID: 518
	internal struct SNIErrorDetails
	{
		// Token: 0x04001173 RID: 4467
		public string errorMessage;

		// Token: 0x04001174 RID: 4468
		public uint nativeError;

		// Token: 0x04001175 RID: 4469
		public uint sniErrorNumber;

		// Token: 0x04001176 RID: 4470
		public int provider;

		// Token: 0x04001177 RID: 4471
		public uint lineNumber;

		// Token: 0x04001178 RID: 4472
		public string function;

		// Token: 0x04001179 RID: 4473
		public Exception exception;
	}
}
