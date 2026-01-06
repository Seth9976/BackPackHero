using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PlayEveryWare.EpicOnlineServices
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class EOSConfig : ICloneableGeneric<EOSConfig>, IEmpty
	{
		// Token: 0x0600001E RID: 30 RVA: 0x00002201 File Offset: 0x00000401
		public static bool IsEncryptionKeyValid(string key)
		{
			return key != null && key.Length == 64 && !EOSConfig.InvalidEncryptionKeyRegex.Match(key).Success;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002228 File Offset: 0x00000428
		public static bool StringIsEqualToAny(string flagAsCString, params string[] parameters)
		{
			foreach (string text in parameters)
			{
				if (flagAsCString == text)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002255 File Offset: 0x00000455
		public static T EnumCast<T, V>(V value)
		{
			return (T)((object)Enum.ToObject(typeof(T), value));
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002271 File Offset: 0x00000471
		public EOSConfig Clone()
		{
			return (EOSConfig)base.MemberwiseClone();
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002280 File Offset: 0x00000480
		public bool IsEmpty()
		{
			return EmptyPredicates.IsEmptyOrNull(this.productName) && EmptyPredicates.IsEmptyOrNull(this.productVersion) && EmptyPredicates.IsEmptyOrNull(this.productID) && EmptyPredicates.IsEmptyOrNull(this.sandboxID) && EmptyPredicates.IsEmptyOrNull(this.deploymentID) && (this.sandboxDeploymentOverrides == null || this.sandboxDeploymentOverrides.Count == 0) && EmptyPredicates.IsEmptyOrNull(this.clientSecret) && EmptyPredicates.IsEmptyOrNull(this.clientID) && EmptyPredicates.IsEmptyOrNull(this.encryptionKey) && EmptyPredicates.IsEmptyOrNull<string>(this.platformOptionsFlags) && EmptyPredicates.IsEmptyOrNull<string>(this.authScopeOptionsFlags) && EmptyPredicates.IsEmptyOrNull(this.repeatButtonDelayForOverlay) && EmptyPredicates.IsEmptyOrNull(this.initialButtonDelayForOverlay);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002347 File Offset: 0x00000547
		public float GetInitialButtonDelayForOverlayAsFloat()
		{
			return float.Parse(this.initialButtonDelayForOverlay);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002354 File Offset: 0x00000554
		public void SetInitialButtonDelayForOverlayFromFloat(float f)
		{
			this.initialButtonDelayForOverlay = f.ToString();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002363 File Offset: 0x00000563
		public float GetRepeatButtonDelayForOverlayAsFloat()
		{
			return float.Parse(this.repeatButtonDelayForOverlay);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002370 File Offset: 0x00000570
		public void SetRepeatButtonDelayForOverlayFromFloat(float f)
		{
			this.repeatButtonDelayForOverlay = f.ToString();
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002380 File Offset: 0x00000580
		public ulong GetThreadAffinityNetworkWork(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_networkWork))
			{
				num = ulong.Parse(this.ThreadAffinity_networkWork);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000023AC File Offset: 0x000005AC
		public ulong GetThreadAffinityStorageIO(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_storageIO))
			{
				num = ulong.Parse(this.ThreadAffinity_storageIO);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023D8 File Offset: 0x000005D8
		public ulong GetThreadAffinityWebSocketIO(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_webSocketIO))
			{
				num = ulong.Parse(this.ThreadAffinity_webSocketIO);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002404 File Offset: 0x00000604
		public ulong GetThreadAffinityP2PIO(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_P2PIO))
			{
				num = ulong.Parse(this.ThreadAffinity_P2PIO);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002430 File Offset: 0x00000630
		public ulong GetThreadAffinityHTTPRequestIO(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_HTTPRequestIO))
			{
				num = ulong.Parse(this.ThreadAffinity_HTTPRequestIO);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000245C File Offset: 0x0000065C
		public ulong GetThreadAffinityRTCIO(ulong defaultValue = 0UL)
		{
			ulong num;
			if (!string.IsNullOrEmpty(this.ThreadAffinity_RTCIO))
			{
				num = ulong.Parse(this.ThreadAffinity_RTCIO);
			}
			else
			{
				num = defaultValue;
			}
			return num;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002487 File Offset: 0x00000687
		public bool IsEncryptionKeyValid()
		{
			return EOSConfig.IsEncryptionKeyValid(this.encryptionKey);
		}

		// Token: 0x0400000D RID: 13
		public string productName;

		// Token: 0x0400000E RID: 14
		public string productVersion;

		// Token: 0x0400000F RID: 15
		public string productID;

		// Token: 0x04000010 RID: 16
		public string sandboxID;

		// Token: 0x04000011 RID: 17
		public string deploymentID;

		// Token: 0x04000012 RID: 18
		public List<SandboxDeploymentOverride> sandboxDeploymentOverrides;

		// Token: 0x04000013 RID: 19
		public string clientSecret;

		// Token: 0x04000014 RID: 20
		public string clientID;

		// Token: 0x04000015 RID: 21
		public string encryptionKey;

		// Token: 0x04000016 RID: 22
		public List<string> platformOptionsFlags;

		// Token: 0x04000017 RID: 23
		public List<string> authScopeOptionsFlags;

		// Token: 0x04000018 RID: 24
		public uint tickBudgetInMilliseconds;

		// Token: 0x04000019 RID: 25
		public string ThreadAffinity_networkWork;

		// Token: 0x0400001A RID: 26
		public string ThreadAffinity_storageIO;

		// Token: 0x0400001B RID: 27
		public string ThreadAffinity_webSocketIO;

		// Token: 0x0400001C RID: 28
		public string ThreadAffinity_P2PIO;

		// Token: 0x0400001D RID: 29
		public string ThreadAffinity_HTTPRequestIO;

		// Token: 0x0400001E RID: 30
		public string ThreadAffinity_RTCIO;

		// Token: 0x0400001F RID: 31
		public bool alwaysSendInputToOverlay;

		// Token: 0x04000020 RID: 32
		public string initialButtonDelayForOverlay;

		// Token: 0x04000021 RID: 33
		public string repeatButtonDelayForOverlay;

		// Token: 0x04000022 RID: 34
		public bool hackForceSendInputDirectlyToSDK;

		// Token: 0x04000023 RID: 35
		public bool isServer;

		// Token: 0x04000024 RID: 36
		public static Regex InvalidEncryptionKeyRegex = new Regex("[^0-9a-fA-F]");
	}
}
