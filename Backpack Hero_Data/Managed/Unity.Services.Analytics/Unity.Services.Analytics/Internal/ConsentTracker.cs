using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x0200002D RID: 45
	internal class ConsentTracker : IConsentTracker
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000042E7 File Offset: 0x000024E7
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000042EF File Offset: 0x000024EF
		internal ConsentStatus optInPiplConsentStatus { get; set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000042F8 File Offset: 0x000024F8
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00004300 File Offset: 0x00002500
		internal ConsentStatus optOutConsentStatus { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004309 File Offset: 0x00002509
		private Dictionary<string, string> piplHeaders
		{
			get
			{
				return new Dictionary<string, string>
				{
					{ "PIPL_EXPORT", "true" },
					{ "PIPL_CONSENT", "true" }
				};
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004330 File Offset: 0x00002530
		public Dictionary<string, string> requiredHeaders
		{
			get
			{
				if (!(this.response.identifier == Consent.Pipl))
				{
					return new Dictionary<string, string>();
				}
				return this.piplHeaders;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00004355 File Offset: 0x00002555
		public ConsentTracker(ICoreStatsHelper coreStatsHelper)
			: this(new GeoAPI(), coreStatsHelper)
		{
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004364 File Offset: 0x00002564
		internal ConsentTracker(IGeoAPI geoApi, ICoreStatsHelper coreStatsHelper)
		{
			this.m_GeoAPI = geoApi ?? new GeoAPI();
			this.m_CoreStatsHelper = coreStatsHelper;
			this.optOutConsentStatus = ConsentStatus.Unknown;
			this.optInPiplConsentStatus = ConsentStatus.Unknown;
			this.ReadOptInPiplConsentStatus();
			this.ReadOptOutConsentStatus();
			this.m_CoreStatsHelper.SetCoreStatsConsent(false);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000043B4 File Offset: 0x000025B4
		public async Task<GeoIPResponse> CheckGeoIP()
		{
			GeoIPResponse geoIPResponse;
			try
			{
				if (this.IsGeoIpChecked())
				{
					geoIPResponse = this.response;
				}
				else
				{
					GeoIPResponse geoIPResponse2 = await this.GetGeoIPResponse();
					this.response = geoIPResponse2;
					if (this.optInPiplConsentStatus == ConsentStatus.Unknown || this.optInPiplConsentStatus == ConsentStatus.NotRequired || this.optInPiplConsentStatus == ConsentStatus.RequiredButUnchecked)
					{
						this.optInPiplConsentStatus = ((geoIPResponse2.identifier == Consent.Pipl) ? ConsentStatus.RequiredButUnchecked : ConsentStatus.NotRequired);
						PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", (int)this.optInPiplConsentStatus);
						PlayerPrefs.Save();
					}
					this.m_CoreStatsHelper.SetCoreStatsConsent(this.IsConsentGiven());
					geoIPResponse = geoIPResponse2;
				}
			}
			catch (ConsentCheckException ex)
			{
				throw ex;
			}
			return geoIPResponse;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000043F7 File Offset: 0x000025F7
		public void SetUserConsentStatus(string identifier, bool consentGiven)
		{
			this.ValidateConsentFlow(identifier);
			if (identifier == Consent.Pipl)
			{
				this.SetOptInPiplConsentStatus(consentGiven);
				return;
			}
			this.optOutConsentStatus = (consentGiven ? ConsentStatus.Unknown : ConsentStatus.OptedOut);
			PlayerPrefs.SetInt("unity.services.analytics.consent_status", (int)this.optOutConsentStatus);
			PlayerPrefs.Save();
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00004437 File Offset: 0x00002637
		public bool IsGeoIpChecked()
		{
			return this.response != null;
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004442 File Offset: 0x00002642
		public bool IsConsentGiven()
		{
			this.ValidateConsentWasChecked();
			return this.IsConsentGiven(this.response.identifier);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000445B File Offset: 0x0000265B
		public bool IsConsentGiven(string identifier)
		{
			if (identifier == Consent.Pipl)
			{
				return this.optInPiplConsentStatus == ConsentStatus.ConsentGiven;
			}
			return this.optOutConsentStatus == ConsentStatus.Unknown;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000447D File Offset: 0x0000267D
		public bool IsConsentDenied()
		{
			this.ValidateConsentWasChecked();
			if (this.response.identifier == Consent.Pipl)
			{
				return this.optInPiplConsentStatus == ConsentStatus.ConsentDenied || this.optInPiplConsentStatus == ConsentStatus.OptedOut;
			}
			return this.optOutConsentStatus == ConsentStatus.OptedOut;
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000044BA File Offset: 0x000026BA
		public bool IsOptingOutInProgress()
		{
			this.ValidateConsentWasChecked();
			if (!(this.response.identifier == Consent.Pipl))
			{
				return this.optOutConsentStatus == ConsentStatus.Forgetting;
			}
			return this.optInPiplConsentStatus == ConsentStatus.Forgetting;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000044EC File Offset: 0x000026EC
		public void BeginOptOutProcess(string identifier)
		{
			this.ValidateConsentWasChecked();
			this.ValidateConsentFlow(identifier);
			if (identifier == Consent.Pipl && this.optInPiplConsentStatus == ConsentStatus.ConsentGiven)
			{
				this.optInPiplConsentStatus = ConsentStatus.Forgetting;
				PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", (int)this.optInPiplConsentStatus);
				PlayerPrefs.Save();
				return;
			}
			if (identifier != Consent.Pipl && this.optOutConsentStatus == ConsentStatus.Unknown)
			{
				this.optOutConsentStatus = ConsentStatus.Forgetting;
				PlayerPrefs.SetInt("unity.services.analytics.consent_status", (int)this.optOutConsentStatus);
				PlayerPrefs.Save();
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000456C File Offset: 0x0000276C
		public void BeginOptOutProcess()
		{
			if (this.optInPiplConsentStatus == ConsentStatus.ConsentGiven)
			{
				this.optInPiplConsentStatus = ConsentStatus.Forgetting;
				PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", (int)this.optInPiplConsentStatus);
				PlayerPrefs.Save();
			}
			if (this.optOutConsentStatus == ConsentStatus.Unknown)
			{
				this.optOutConsentStatus = ConsentStatus.Forgetting;
				PlayerPrefs.SetInt("unity.services.analytics.consent_status", (int)this.optOutConsentStatus);
				PlayerPrefs.Save();
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000045C4 File Offset: 0x000027C4
		public void FinishOptOutProcess()
		{
			if (this.optInPiplConsentStatus == ConsentStatus.Forgetting)
			{
				this.optInPiplConsentStatus = ConsentStatus.OptedOut;
				PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", (int)this.optInPiplConsentStatus);
				PlayerPrefs.Save();
			}
			if (this.optOutConsentStatus == ConsentStatus.Forgetting)
			{
				this.optOutConsentStatus = ConsentStatus.OptedOut;
				PlayerPrefs.SetInt("unity.services.analytics.consent_status", (int)this.optOutConsentStatus);
				PlayerPrefs.Save();
			}
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000461C File Offset: 0x0000281C
		public void SetDenyConsentToAll()
		{
			this.optOutConsentStatus = ConsentStatus.OptedOut;
			this.optInPiplConsentStatus = ((this.optInPiplConsentStatus == ConsentStatus.Forgetting) ? ConsentStatus.OptedOut : ConsentStatus.ConsentDenied);
			PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", (int)this.optInPiplConsentStatus);
			PlayerPrefs.SetInt("unity.services.analytics.consent_status", (int)this.optOutConsentStatus);
			PlayerPrefs.Save();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004668 File Offset: 0x00002868
		internal void SetOptInPiplConsentStatus(bool consentGiven)
		{
			this.optInPiplConsentStatus = (consentGiven ? ConsentStatus.ConsentGiven : ConsentStatus.ConsentDenied);
			PlayerPrefs.SetInt("unity.services.analytics.pipl_consent_status", consentGiven ? 5 : 6);
			PlayerPrefs.Save();
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000468D File Offset: 0x0000288D
		internal void ReadOptInPiplConsentStatus()
		{
			if (PlayerPrefs.HasKey("unity.services.analytics.pipl_consent_status"))
			{
				this.optInPiplConsentStatus = (ConsentStatus)PlayerPrefs.GetInt("unity.services.analytics.pipl_consent_status");
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000046AB File Offset: 0x000028AB
		internal void ReadOptOutConsentStatus()
		{
			if (PlayerPrefs.HasKey("unity.services.analytics.consent_status"))
			{
				this.optOutConsentStatus = (ConsentStatus)PlayerPrefs.GetInt("unity.services.analytics.consent_status");
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000046CC File Offset: 0x000028CC
		internal async Task<GeoIPResponse> GetGeoIPResponse()
		{
			GeoIPResponse geoIPResponse;
			try
			{
				geoIPResponse = await this.m_GeoAPI.MakeRequest();
			}
			catch (ConsentCheckException ex)
			{
				throw ex;
			}
			return geoIPResponse;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x0000470F File Offset: 0x0000290F
		private void ValidateConsentWasChecked()
		{
			if (!this.IsGeoIpChecked())
			{
				throw new ConsentCheckException(ConsentCheckExceptionReason.ConsentFlowNotKnown, 0, "The required consent flow cannot be determined. Make sure GeoIP was successfully called.", null);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004728 File Offset: 0x00002928
		private void ValidateConsentFlow(string identifier)
		{
			if ((this.response.identifier == Consent.Pipl && identifier != this.response.identifier) || (this.response.identifier != Consent.Pipl && identifier == Consent.Pipl))
			{
				throw new ConsentCheckException(ConsentCheckExceptionReason.InvalidConsentFlow, 55, "The requested action is unavailable for this legislation. Please check documentation for more details.", null);
			}
		}

		// Token: 0x040000BF RID: 191
		private readonly IGeoAPI m_GeoAPI;

		// Token: 0x040000C0 RID: 192
		private readonly ICoreStatsHelper m_CoreStatsHelper;

		// Token: 0x040000C3 RID: 195
		internal GeoIPResponse response;

		// Token: 0x040000C4 RID: 196
		internal const string optInPiplConsentStatusPrefKey = "unity.services.analytics.pipl_consent_status";

		// Token: 0x040000C5 RID: 197
		internal const string optOutConsentStatusPrefKey = "unity.services.analytics.consent_status";
	}
}
