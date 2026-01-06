using System;
using UnityEngine.Analytics;

namespace Unity.Services.Analytics
{
	// Token: 0x02000020 RID: 32
	internal class CoreStatsHelper : ICoreStatsHelper
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003678 File Offset: 0x00001878
		public void SetCoreStatsConsent(bool userProvidedConsent)
		{
			UGSAnalyticsInternalTools.SetPrivacyStatus(userProvidedConsent);
		}
	}
}
