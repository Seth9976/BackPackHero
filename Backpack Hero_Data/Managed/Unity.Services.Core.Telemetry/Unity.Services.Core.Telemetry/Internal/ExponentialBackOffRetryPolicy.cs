using System;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Unity.Services.Core.Telemetry.Internal
{
	// Token: 0x0200001A RID: 26
	internal class ExponentialBackOffRetryPolicy
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002F91 File Offset: 0x00001191
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002F99 File Offset: 0x00001199
		public int MaxTryCount
		{
			get
			{
				return this.m_MaxTryCount;
			}
			set
			{
				this.m_MaxTryCount = Math.Max(value, 0);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002FA8 File Offset: 0x000011A8
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002FB0 File Offset: 0x000011B0
		public float BaseDelaySeconds
		{
			get
			{
				return this.m_BaseDelaySeconds;
			}
			set
			{
				this.m_BaseDelaySeconds = Math.Max(value, 0f);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002FC3 File Offset: 0x000011C3
		public bool CanRetry(WebRequest webRequest, int sendCount)
		{
			return sendCount < this.MaxTryCount && ExponentialBackOffRetryPolicy.IsTransientError(webRequest);
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002FD6 File Offset: 0x000011D6
		public static bool IsTransientError(WebRequest webRequest)
		{
			return webRequest.Result == WebRequestResult.ConnectionError || (webRequest.Result == WebRequestResult.ProtocolError && ExponentialBackOffRetryPolicy.<IsTransientError>g__IsServerErrorCode|9_0(webRequest.ResponseCode));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002FF9 File Offset: 0x000011F9
		public float GetDelayBeforeSendingSeconds(int sendCount)
		{
			if (sendCount <= 0)
			{
				return this.BaseDelaySeconds;
			}
			return Mathf.Pow(this.BaseDelaySeconds, (float)sendCount);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000302E File Offset: 0x0000122E
		[CompilerGenerated]
		internal static bool <IsTransientError>g__IsServerErrorCode|9_0(long responseCode)
		{
			return responseCode >= 500L && responseCode < 600L;
		}

		// Token: 0x0400002F RID: 47
		private int m_MaxTryCount = 10;

		// Token: 0x04000030 RID: 48
		private float m_BaseDelaySeconds = 2f;
	}
}
