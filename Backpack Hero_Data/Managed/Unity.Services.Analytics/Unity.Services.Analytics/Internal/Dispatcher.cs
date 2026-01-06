using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000036 RID: 54
	internal class Dispatcher : IDispatcher
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00004B42 File Offset: 0x00002D42
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00004B4A File Offset: 0x00002D4A
		internal bool FlushInProgress { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00004B53 File Offset: 0x00002D53
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00004B5B File Offset: 0x00002D5B
		public string CollectUrl { get; set; }

		// Token: 0x0600011E RID: 286 RVA: 0x00004B64 File Offset: 0x00002D64
		public Dispatcher(IWebRequestHelper webRequestHelper, IConsentTracker consentTracker)
		{
			this.m_WebRequestHelper = webRequestHelper;
			this.m_ConsentTracker = consentTracker;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B7A File Offset: 0x00002D7A
		public void SetBuffer(IBuffer buffer)
		{
			this.m_DataBuffer = buffer;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B83 File Offset: 0x00002D83
		public void Flush()
		{
			if (this.FlushInProgress)
			{
				Debug.LogWarning("Analytics Dispatcher is already flushing.");
				return;
			}
			if (!this.m_ConsentTracker.IsGeoIpChecked() || !this.m_ConsentTracker.IsConsentGiven())
			{
				Debug.LogWarning("Required consent wasn't checked and given when trying to dispatch events, the events cannot be sent.");
				return;
			}
			this.FlushBufferToService();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004BC4 File Offset: 0x00002DC4
		private void FlushBufferToService()
		{
			this.FlushInProgress = true;
			byte[] array = this.m_DataBuffer.Serialize();
			this.m_FlushBufferIndex = this.m_DataBuffer.Length;
			if (array == null || array.Length == 0)
			{
				this.FlushInProgress = false;
				this.m_FlushBufferIndex = 0;
				return;
			}
			this.m_FlushRequest = this.m_WebRequestHelper.CreateWebRequest(this.CollectUrl, "POST", array);
			if (this.m_ConsentTracker.IsGeoIpChecked() && this.m_ConsentTracker.IsConsentGiven())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.m_ConsentTracker.requiredHeaders)
				{
					this.m_FlushRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.m_WebRequestHelper.SendWebRequest(this.m_FlushRequest, new Action<long>(this.UploadCompleted));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004CBC File Offset: 0x00002EBC
		private void UploadCompleted(long responseCode)
		{
			if (!this.m_FlushRequest.isNetworkError && (responseCode == 204L || responseCode == 400L))
			{
				this.m_DataBuffer.ClearBuffer((long)this.m_FlushBufferIndex);
				this.m_DataBuffer.ClearDiskCache();
			}
			else
			{
				this.m_DataBuffer.FlushToDisk();
			}
			this.FlushInProgress = false;
			this.m_FlushBufferIndex = 0;
			this.m_FlushRequest.Dispose();
			this.m_FlushRequest = null;
		}

		// Token: 0x040000D0 RID: 208
		private readonly IWebRequestHelper m_WebRequestHelper;

		// Token: 0x040000D1 RID: 209
		private readonly IConsentTracker m_ConsentTracker;

		// Token: 0x040000D2 RID: 210
		private IBuffer m_DataBuffer;

		// Token: 0x040000D3 RID: 211
		private IWebRequest m_FlushRequest;

		// Token: 0x040000D5 RID: 213
		private int m_FlushBufferIndex;
	}
}
