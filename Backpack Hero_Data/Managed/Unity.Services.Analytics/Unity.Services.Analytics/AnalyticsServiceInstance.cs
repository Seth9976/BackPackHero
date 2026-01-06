using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Analytics.Data;
using Unity.Services.Analytics.Internal;
using Unity.Services.Analytics.Internal.Platform;
using Unity.Services.Analytics.Platform;
using Unity.Services.Authentication.Internal;
using Unity.Services.Core.Configuration.Internal;
using Unity.Services.Core.Device.Internal;
using UnityEngine;

namespace Unity.Services.Analytics
{
	// Token: 0x02000004 RID: 4
	internal class AnalyticsServiceInstance : IAnalyticsService, IUnstructuredEventRecorder
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000210C File Offset: 0x0000030C
		public async Task<List<string>> CheckForRequiredConsents()
		{
			GeoIPResponse geoIPResponse = await this.m_ConsentTracker.CheckGeoIP();
			List<string> list;
			if (geoIPResponse.identifier == Consent.None)
			{
				list = new List<string>();
			}
			else if (this.m_ConsentTracker.IsConsentDenied())
			{
				list = new List<string>();
			}
			else if (!this.m_ConsentTracker.IsConsentGiven())
			{
				list = new List<string> { geoIPResponse.identifier };
			}
			else
			{
				list = new List<string>();
			}
			return list;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002150 File Offset: 0x00000350
		public void ProvideOptInConsent(string identifier, bool consent)
		{
			this.m_CoreStatsHelper.SetCoreStatsConsent(consent);
			if (!this.m_ConsentTracker.IsGeoIpChecked())
			{
				throw new ConsentCheckException(ConsentCheckExceptionReason.ConsentFlowNotKnown, 0, "The required consent flow cannot be determined. Make sure CheckForRequiredConsents() method was successfully called.", null);
			}
			if (!consent)
			{
				if (this.m_ConsentTracker.IsConsentGiven(identifier))
				{
					this.m_ConsentTracker.BeginOptOutProcess(identifier);
					this.RevokeWithForgetEvent();
					return;
				}
				this.Revoke();
			}
			this.m_ConsentTracker.SetUserConsentStatus(identifier, consent);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000021BC File Offset: 0x000003BC
		public void OptOut()
		{
			Debug.Log(this.m_ConsentTracker.IsConsentDenied() ? "This user has opted out. Any cached events have been discarded and no more will be collected." : "This user has opted out and is in the process of being forgotten...");
			if (this.m_ConsentTracker.IsConsentGiven())
			{
				this.m_ConsentTracker.BeginOptOutProcess();
				this.RevokeWithForgetEvent();
				return;
			}
			if (this.m_ConsentTracker.IsOptingOutInProgress())
			{
				this.RevokeWithForgetEvent();
				return;
			}
			this.Revoke();
			this.m_ConsentTracker.SetDenyConsentToAll();
			this.m_CoreStatsHelper.SetCoreStatsConsent(false);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002237 File Offset: 0x00000437
		private void Revoke()
		{
			this.SwapToRevokedBuffer();
			AnalyticsContainer.DestroyContainer();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002244 File Offset: 0x00000444
		internal void RevokeWithForgetEvent()
		{
			this.SwapToRevokedBuffer();
			this.m_AnalyticsForgetter.AttemptToForget("com.unity.services.analytics.Events.OptOut", this.m_CollectURL, this.m_InstallId.GetOrCreateIdentifier(), BufferX.SerializeDateTime(DateTime.Now), new Action(this.ForgetMeEventUploaded));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002283 File Offset: 0x00000483
		internal void ForgetMeEventUploaded()
		{
			AnalyticsContainer.DestroyContainer();
			this.m_ConsentTracker.FinishOptOutProcess();
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002295 File Offset: 0x00000495
		public string PrivacyUrl
		{
			get
			{
				return "https://unity3d.com/legal/privacy-policy";
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x0000229C File Offset: 0x0000049C
		internal string CustomAnalyticsId
		{
			get
			{
				return this.m_CustomUserId.UserId;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022A9 File Offset: 0x000004A9
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022B1 File Offset: 0x000004B1
		internal bool ServiceEnabled { get; private set; } = true;

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022C2 File Offset: 0x000004C2
		public string SessionID { get; private set; }

		// Token: 0x06000011 RID: 17 RVA: 0x000022CC File Offset: 0x000004CC
		internal AnalyticsServiceInstance(IDataGenerator dataGenerator, IBuffer realBuffer, IBuffer revokedBuffer, ICoreStatsHelper coreStatsHelper, IConsentTracker consentTracker, IDispatcher dispatcher, IAnalyticsForgetter forgetter, ICloudProjectId cloudProjectId, IInstallationId installId, IPlayerId playerId, string environment, IExternalUserId customAnalyticsId, IAnalyticsServiceSystemCalls systemCalls)
		{
			this.m_CustomUserId = customAnalyticsId;
			this.m_DataGenerator = dataGenerator;
			this.m_SystemCalls = systemCalls;
			this.m_RealBuffer = realBuffer;
			this.m_RevokedBuffer = revokedBuffer;
			this.m_CoreStatsHelper = coreStatsHelper;
			this.m_ConsentTracker = consentTracker;
			this.m_DataDispatcher = dispatcher;
			this.SwapToRealBuffer();
			this.m_AnalyticsForgetter = forgetter;
			this.m_CommonParams = new StdCommonParams
			{
				ClientVersion = Application.version,
				ProjectID = Application.cloudProjectId,
				GameBundleID = Application.identifier,
				Platform = Runtime.Name(),
				BuildGuuid = Application.buildGUID,
				Idfv = SystemInfo.deviceUniqueIdentifier
			};
			this.m_InstallId = installId;
			this.m_PlayerId = playerId;
			string text = ((cloudProjectId != null) ? cloudProjectId.GetCloudProjectId() : null) ?? Application.cloudProjectId;
			this.m_CommonParams.ProjectID = text;
			this.m_CollectURL = string.Format("https://collect.analytics.unity3d.com/api/analytics/collect/v1/projects/{0}/environments/{1}", text, environment.ToLowerInvariant());
			this.m_DataBuffer.UserID = this.GetAnalyticsUserID();
			this.m_DataBuffer.InstallID = this.m_InstallId.GetOrCreateIdentifier();
			this.RefreshSessionID();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002418 File Offset: 0x00000618
		internal async Task Initialize()
		{
			if (this.ServiceEnabled)
			{
				AnalyticsContainer.Initialize();
				await this.InitializeUser();
				this.RecordStartupEvents();
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000245C File Offset: 0x0000065C
		private async Task InitializeUser()
		{
			this.SetVariableCommonParams();
			try
			{
				await this.m_ConsentTracker.CheckGeoIP();
				if (this.m_ConsentTracker.IsGeoIpChecked() && (this.m_ConsentTracker.IsConsentDenied() || this.m_ConsentTracker.IsOptingOutInProgress()))
				{
					this.OptOut();
				}
			}
			catch (ConsentCheckException)
			{
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000024A0 File Offset: 0x000006A0
		private void RecordStartupEvents()
		{
			this.m_DataGenerator.SdkStartup(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.Startup");
			this.m_DataGenerator.ClientDevice(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.Startup", SystemInfo.processorType, SystemInfo.graphicsDeviceName, (long)SystemInfo.processorCount, (long)SystemInfo.systemMemorySize, (long)Screen.width, (long)Screen.height, (long)((int)Screen.dpi));
			bool flag = false;
			this.m_DataGenerator.GameStarted(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.Startup", Application.buildGUID, SystemInfo.operatingSystem, flag, DebugDevice.IsDebugDevice(), Locale.AnalyticsRegionLanguageCode());
			if (this.m_InstallId != null && new InternalNewPlayerHelper(this.m_InstallId).IsNewPlayer())
			{
				this.m_DataGenerator.NewPlayer(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.Startup", SystemInfo.deviceModel);
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002578 File Offset: 0x00000778
		public string GetAnalyticsUserID()
		{
			if (string.IsNullOrEmpty(this.CustomAnalyticsId))
			{
				return this.m_InstallId.GetOrCreateIdentifier();
			}
			return this.CustomAnalyticsId;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002599 File Offset: 0x00000799
		internal void ApplicationPaused(bool paused)
		{
			if (paused)
			{
				this.m_ApplicationPauseTime = this.m_SystemCalls.UtcNow;
				return;
			}
			if (this.m_SystemCalls.UtcNow > this.m_ApplicationPauseTime + this.k_BackgroundSessionRefreshPeriod)
			{
				this.RefreshSessionID();
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000025DC File Offset: 0x000007DC
		internal void RefreshSessionID()
		{
			this.SessionID = Guid.NewGuid().ToString();
			this.m_DataBuffer.SessionID = this.SessionID;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002614 File Offset: 0x00000814
		public void Flush()
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			if (this.m_ConsentTracker.IsGeoIpChecked() && this.m_ConsentTracker.IsConsentGiven())
			{
				this.m_DataBuffer.InstallID = this.m_InstallId.GetOrCreateIdentifier();
				IBuffer dataBuffer = this.m_DataBuffer;
				IPlayerId playerId = this.m_PlayerId;
				dataBuffer.PlayerID = ((playerId != null) ? playerId.PlayerId : null);
				this.m_DataBuffer.UserID = this.GetAnalyticsUserID();
				this.m_DataBuffer.SessionID = this.SessionID;
				this.m_DataDispatcher.CollectUrl = this.m_CollectURL;
				this.m_DataDispatcher.Flush();
			}
			if (this.m_ConsentTracker.IsOptingOutInProgress())
			{
				this.m_AnalyticsForgetter.AttemptToForget("com.unity.services.analytics.Events.OptOut", this.m_CollectURL, this.m_InstallId.GetOrCreateIdentifier(), BufferX.SerializeDateTime(DateTime.Now), new Action(this.ForgetMeEventUploaded));
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000026F9 File Offset: 0x000008F9
		[Obsolete("This mechanism is no longer supported and will be removed in a future version. Use the new Core IAnalyticsStandardEventComponent API instead.")]
		public void RecordInternalEvent(Event eventToRecord)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			this.m_DataBuffer.PushEvent(eventToRecord);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002710 File Offset: 0x00000910
		internal void GameEnded()
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			this.m_DataGenerator.GameEnded(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.Shutdown", DataGenerator.SessionEndState.QUIT);
			if (this.m_ConsentTracker != null && this.m_ConsentTracker.IsGeoIpChecked())
			{
				this.Flush();
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002760 File Offset: 0x00000960
		internal void RecordGameRunningIfNecessary()
		{
			if (this.ServiceEnabled)
			{
				if (this.m_DataBuffer.Length == 0 || this.m_DataBuffer.Length == this.m_BufferLengthAtLastGameRunning)
				{
					this.SetVariableCommonParams();
					this.m_DataGenerator.GameRunning(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.AnalyticsServiceInstance.RecordGameRunningIfNecessary");
					this.m_BufferLengthAtLastGameRunning = this.m_DataBuffer.Length;
					return;
				}
				this.m_BufferLengthAtLastGameRunning = this.m_DataBuffer.Length;
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027D9 File Offset: 0x000009D9
		internal void InternalTick()
		{
			if (this.ServiceEnabled && this.m_ConsentTracker.IsGeoIpChecked())
			{
				this.Flush();
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000027F8 File Offset: 0x000009F8
		private void SetVariableCommonParams()
		{
			StdCommonParams commonParams = this.m_CommonParams;
			float? deviceVolume = DeviceVolumeProvider.GetDeviceVolume();
			commonParams.DeviceVolume = ((deviceVolume != null) ? new double?((double)deviceVolume.GetValueOrDefault()) : null);
			this.m_CommonParams.BatteryLoad = new double?((double)SystemInfo.batteryLevel);
			StdCommonParams commonParams2 = this.m_CommonParams;
			IPlayerId playerId = this.m_PlayerId;
			commonParams2.UasUserID = ((playerId != null) ? playerId.PlayerId : null);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000286A File Offset: 0x00000A6A
		private void GameEnded(DataGenerator.SessionEndState quitState)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			this.m_DataGenerator.GameEnded(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.Events.GameEnded", quitState);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002894 File Offset: 0x00000A94
		public async Task SetAnalyticsEnabled(bool enabled)
		{
			if (enabled && !this.ServiceEnabled)
			{
				this.SwapToRealBuffer();
				await this.InitializeUser();
				this.ServiceEnabled = true;
			}
			else if (!enabled && this.ServiceEnabled)
			{
				this.SwapToRevokedBuffer();
				this.ServiceEnabled = false;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000028E0 File Offset: 0x00000AE0
		private void SwapToRevokedBuffer()
		{
			this.m_RealBuffer.ClearBuffer();
			this.m_RealBuffer.ClearDiskCache();
			this.m_DataBuffer = this.m_RevokedBuffer;
			this.m_DataGenerator.SetBuffer(this.m_RevokedBuffer);
			this.m_DataDispatcher.SetBuffer(this.m_RevokedBuffer);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002931 File Offset: 0x00000B31
		private void SwapToRealBuffer()
		{
			this.m_DataBuffer = this.m_RealBuffer;
			this.m_DataGenerator.SetBuffer(this.m_RealBuffer);
			this.m_DataDispatcher.SetBuffer(this.m_RealBuffer);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002964 File Offset: 0x00000B64
		public void AcquisitionSource(AcquisitionSourceParameters acquisitionSourceParameters)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			if (string.IsNullOrEmpty(acquisitionSourceParameters.Channel))
			{
				Debug.LogError("Required to have a value for channel");
			}
			if (string.IsNullOrEmpty(acquisitionSourceParameters.CampaignId))
			{
				Debug.LogError("Required to have a value for campaignId");
			}
			if (string.IsNullOrEmpty(acquisitionSourceParameters.CreativeId))
			{
				Debug.LogError("Required to have a value for creativeId");
			}
			if (string.IsNullOrEmpty(acquisitionSourceParameters.CampaignName))
			{
				Debug.LogError("Required to have a value for campaignName");
			}
			if (string.IsNullOrEmpty(acquisitionSourceParameters.Provider))
			{
				Debug.LogError("Required to have a value for provider");
			}
			this.m_DataGenerator.AcquisitionSource(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.events.acquisitionSource", acquisitionSourceParameters);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002A0C File Offset: 0x00000C0C
		public void AdImpression(AdImpressionParameters adImpressionParameters)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			if (string.IsNullOrEmpty(adImpressionParameters.PlacementID))
			{
				Debug.LogError("Required to have a value for placementID.");
			}
			if (string.IsNullOrEmpty(adImpressionParameters.PlacementName))
			{
				Debug.LogError("Required to have a value for placementName.");
			}
			this.m_DataGenerator.AdImpression(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.events.adimpression", adImpressionParameters);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002A6C File Offset: 0x00000C6C
		public void CustomData(string eventName)
		{
			this.CustomData(eventName, null);
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002A78 File Offset: 0x00000C78
		public void CustomData(string eventName, IDictionary<string, object> eventParams)
		{
			this.CustomData(eventName, eventParams, null, false, false, "AnalyticsServiceInstance.CustomData");
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public void CustomData(string eventName, IDictionary<string, object> eventParams, int? eventVersion, bool includeCommonParams, bool includePlayerIds, string callingMethodIdentifier)
		{
			if (this.ServiceEnabled)
			{
				IBuffer dataBuffer = this.m_DataBuffer;
				DateTime now = DateTime.Now;
				int? num = eventVersion;
				dataBuffer.PushStartEvent(eventName, now, (num != null) ? new long?((long)num.GetValueOrDefault()) : null, includePlayerIds);
				if (includeCommonParams)
				{
					this.m_CommonParams.SerializeCommonEventParams(ref this.m_DataBuffer, callingMethodIdentifier);
				}
				if (eventParams != null)
				{
					foreach (KeyValuePair<string, object> keyValuePair in eventParams)
					{
						this.SerializeObject(eventName, keyValuePair.Key, keyValuePair.Value);
					}
				}
				this.m_DataBuffer.PushEndEvent();
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B5C File Offset: 0x00000D5C
		private void SerializeObject(string eventName, string key, object value)
		{
			if (value == null)
			{
				Debug.LogWarning(string.Concat(new string[] { "Value for key ", key, " was null, it will not be included in event ", eventName, "." }));
				return;
			}
			Type type = value.GetType();
			if (type == typeof(string))
			{
				this.m_DataBuffer.PushString((string)value, key);
				return;
			}
			if (type == typeof(int))
			{
				this.m_DataBuffer.PushInt((int)value, key);
				return;
			}
			if (type == typeof(long))
			{
				this.m_DataBuffer.PushInt64((long)value, key);
				return;
			}
			if (type == typeof(float))
			{
				this.m_DataBuffer.PushFloat((float)value, key);
				return;
			}
			if (type == typeof(double))
			{
				this.m_DataBuffer.PushDouble((double)value, key);
				return;
			}
			if (type == typeof(bool))
			{
				this.m_DataBuffer.PushBool((bool)value, key);
				return;
			}
			if (type == typeof(DateTime))
			{
				this.m_DataBuffer.PushTimestamp((DateTime)value, key);
				return;
			}
			Enum @enum = value as Enum;
			if (@enum != null)
			{
				this.m_DataBuffer.PushString(@enum.ToString(), key);
				return;
			}
			IDictionary<string, object> dictionary = value as IDictionary<string, object>;
			if (dictionary != null)
			{
				this.m_DataBuffer.PushObjectStart(key);
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					this.SerializeObject(eventName, keyValuePair.Key, keyValuePair.Value);
				}
				this.m_DataBuffer.PushObjectEnd();
				return;
			}
			IList<object> list = value as IList<object>;
			if (list != null)
			{
				if (list.Count > 0)
				{
					this.m_DataBuffer.PushArrayStart(key);
					for (int i = 0; i < list.Count; i++)
					{
						this.SerializeObject(eventName, null, list[i]);
					}
					this.m_DataBuffer.PushArrayEnd();
					return;
				}
			}
			else
			{
				Debug.LogError(string.Concat(new string[] { "Unknown type found for key ", key, ", this value will not be included in event ", eventName, "." }));
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002DB8 File Offset: 0x00000FB8
		public void Transaction(TransactionParameters transactionParameters)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			if (string.IsNullOrEmpty(transactionParameters.TransactionName))
			{
				Debug.LogError("Required to have a value for transactionName");
			}
			if (transactionParameters.TransactionType.Equals(TransactionType.INVALID))
			{
				Debug.LogError("Required to have a value for transactionType");
			}
			if (string.IsNullOrEmpty(transactionParameters.PaymentCountry))
			{
				transactionParameters.PaymentCountry = UserCountry.Name();
			}
			this.m_DataGenerator.Transaction(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.events.transaction", transactionParameters);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002E3E File Offset: 0x0000103E
		public long ConvertCurrencyToMinorUnits(string currencyCode, double value)
		{
			return this.converter.Convert(currencyCode, value);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002E50 File Offset: 0x00001050
		public void TransactionFailed(TransactionFailedParameters parameters)
		{
			if (!this.ServiceEnabled)
			{
				return;
			}
			if (string.IsNullOrEmpty(parameters.TransactionName))
			{
				Debug.LogError("Required to have a value for transactionName");
			}
			if (parameters.TransactionType.Equals(TransactionType.INVALID))
			{
				Debug.LogError("Required to have a value for transactionType");
			}
			if (string.IsNullOrEmpty(parameters.FailureReason))
			{
				Debug.LogError("Required to have a failure reason in transactionFailed event");
			}
			if (string.IsNullOrEmpty(parameters.PaymentCountry))
			{
				parameters.PaymentCountry = UserCountry.Name();
			}
			this.m_DataGenerator.TransactionFailed(DateTime.Now, this.m_CommonParams, "com.unity.services.analytics.events.TransactionFailed", parameters);
		}

		// Token: 0x04000002 RID: 2
		private const string k_CollectUrlPattern = "https://collect.analytics.unity3d.com/api/analytics/collect/v1/projects/{0}/environments/{1}";

		// Token: 0x04000003 RID: 3
		private const string k_ForgetCallingId = "com.unity.services.analytics.Events.OptOut";

		// Token: 0x04000004 RID: 4
		private const string m_StartUpCallingId = "com.unity.services.analytics.Events.Startup";

		// Token: 0x04000005 RID: 5
		private readonly TimeSpan k_BackgroundSessionRefreshPeriod = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000006 RID: 6
		private readonly string m_CollectURL;

		// Token: 0x04000007 RID: 7
		private readonly StdCommonParams m_CommonParams;

		// Token: 0x04000008 RID: 8
		private readonly IPlayerId m_PlayerId;

		// Token: 0x04000009 RID: 9
		private readonly IInstallationId m_InstallId;

		// Token: 0x0400000A RID: 10
		private readonly IDataGenerator m_DataGenerator;

		// Token: 0x0400000B RID: 11
		private readonly ICoreStatsHelper m_CoreStatsHelper;

		// Token: 0x0400000C RID: 12
		private readonly IConsentTracker m_ConsentTracker;

		// Token: 0x0400000D RID: 13
		private readonly IDispatcher m_DataDispatcher;

		// Token: 0x0400000E RID: 14
		private readonly IAnalyticsForgetter m_AnalyticsForgetter;

		// Token: 0x0400000F RID: 15
		private readonly IExternalUserId m_CustomUserId;

		// Token: 0x04000010 RID: 16
		private readonly IAnalyticsServiceSystemCalls m_SystemCalls;

		// Token: 0x04000011 RID: 17
		private readonly IBuffer m_RealBuffer;

		// Token: 0x04000012 RID: 18
		private readonly IBuffer m_RevokedBuffer;

		// Token: 0x04000013 RID: 19
		internal IBuffer m_DataBuffer;

		// Token: 0x04000016 RID: 22
		private int m_BufferLengthAtLastGameRunning;

		// Token: 0x04000017 RID: 23
		private DateTime m_ApplicationPauseTime;

		// Token: 0x04000018 RID: 24
		private readonly TransactionCurrencyConverter converter = new TransactionCurrencyConverter();
	}
}
