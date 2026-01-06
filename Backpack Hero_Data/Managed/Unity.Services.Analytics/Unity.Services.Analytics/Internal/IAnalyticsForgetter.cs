using System;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000023 RID: 35
	internal interface IAnalyticsForgetter
	{
		// Token: 0x0600006F RID: 111
		void AttemptToForget(string collectUrl, string userId, string timestamp, string callingMethod, Action successfulUploadCallback);
	}
}
