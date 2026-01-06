using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics.Internal;

namespace Unity.Services.Analytics
{
	// Token: 0x02000017 RID: 23
	public interface IAnalyticsService
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000033 RID: 51
		string PrivacyUrl { get; }

		// Token: 0x06000034 RID: 52
		void Flush();

		// Token: 0x06000035 RID: 53
		void AdImpression(AdImpressionParameters parameters);

		// Token: 0x06000036 RID: 54
		void Transaction(TransactionParameters transactionParameters);

		// Token: 0x06000037 RID: 55
		void TransactionFailed(TransactionFailedParameters parameters);

		// Token: 0x06000038 RID: 56
		void CustomData(string eventName, IDictionary<string, object> eventParams);

		// Token: 0x06000039 RID: 57
		void CustomData(string eventName);

		// Token: 0x0600003A RID: 58
		Task<List<string>> CheckForRequiredConsents();

		// Token: 0x0600003B RID: 59
		void ProvideOptInConsent(string identifier, bool consent);

		// Token: 0x0600003C RID: 60
		void OptOut();

		// Token: 0x0600003D RID: 61
		[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
		void RecordInternalEvent(Event eventToRecord);

		// Token: 0x0600003E RID: 62
		void AcquisitionSource(AcquisitionSourceParameters acquisitionSourceParameters);

		// Token: 0x0600003F RID: 63
		Task SetAnalyticsEnabled(bool enabled);

		// Token: 0x06000040 RID: 64
		long ConvertCurrencyToMinorUnits(string currencyCode, double value);

		// Token: 0x06000041 RID: 65
		string GetAnalyticsUserID();

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000042 RID: 66
		string SessionID { get; }
	}
}
