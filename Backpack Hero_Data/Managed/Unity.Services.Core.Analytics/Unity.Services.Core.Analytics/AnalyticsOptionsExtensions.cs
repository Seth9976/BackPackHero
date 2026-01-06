using System;

namespace Unity.Services.Core.Analytics
{
	// Token: 0x02000002 RID: 2
	public static class AnalyticsOptionsExtensions
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("SetAnalyticsUserId is deprecated. Please use UnityServices.ExternalUserId instead.", false)]
		public static InitializationOptions SetAnalyticsUserId(this InitializationOptions self, string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new ArgumentException("Analytics user id cannot be null or empty.", "id");
			}
			return self.SetOption("com.unity.services.core.analytics-user-id", id);
		}

		// Token: 0x04000001 RID: 1
		internal const string AnalyticsUserIdKey = "com.unity.services.core.analytics-user-id";
	}
}
