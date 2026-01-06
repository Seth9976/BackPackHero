using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200002B RID: 43
	internal interface IConsentTracker
	{
		// Token: 0x060000D7 RID: 215
		Task<GeoIPResponse> CheckGeoIP();

		// Token: 0x060000D8 RID: 216
		void SetUserConsentStatus(string key, bool consentGiven);

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000D9 RID: 217
		Dictionary<string, string> requiredHeaders { get; }

		// Token: 0x060000DA RID: 218
		void BeginOptOutProcess(string identifier);

		// Token: 0x060000DB RID: 219
		void BeginOptOutProcess();

		// Token: 0x060000DC RID: 220
		void FinishOptOutProcess();

		// Token: 0x060000DD RID: 221
		bool IsGeoIpChecked();

		// Token: 0x060000DE RID: 222
		bool IsConsentGiven(string identifier);

		// Token: 0x060000DF RID: 223
		bool IsConsentGiven();

		// Token: 0x060000E0 RID: 224
		bool IsConsentDenied();

		// Token: 0x060000E1 RID: 225
		bool IsOptingOutInProgress();

		// Token: 0x060000E2 RID: 226
		void SetDenyConsentToAll();
	}
}
