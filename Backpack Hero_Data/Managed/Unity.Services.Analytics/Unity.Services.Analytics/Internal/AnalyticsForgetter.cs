using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Unity.Services.Analytics.Internal
{
	// Token: 0x02000024 RID: 36
	internal class AnalyticsForgetter : IAnalyticsForgetter
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000036BF File Offset: 0x000018BF
		public AnalyticsForgetter(IConsentTracker consentTracker)
		{
			this.ConsentTracker = consentTracker;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000036D0 File Offset: 0x000018D0
		public void AttemptToForget(string collectUrl, string userId, string timestamp, string callingMethod, Action successfulUploadCallback)
		{
			if (this.m_Request != null || this.m_SuccessfullyUploaded)
			{
				return;
			}
			this.m_CollectUrl = collectUrl;
			this.m_Callback = successfulUploadCallback;
			string text = string.Concat(new string[]
			{
				"{\"eventList\":[{\"eventName\":\"ddnaForgetMe\",\"userID\":\"",
				userId,
				"\",\"eventUUID\":\"",
				Guid.NewGuid().ToString(),
				"\",\"eventTimestamp\":\"",
				timestamp,
				"\",\"eventVersion\":1,\"eventParams\":{\"clientVersion\":\"",
				Application.version,
				"\",\"sdkMethod\":\"",
				callingMethod,
				"\"}}]}"
			});
			this.m_Event = Encoding.UTF8.GetBytes(text);
			UnityWebRequest unityWebRequest = new UnityWebRequest(this.m_CollectUrl, "POST");
			UploadHandlerRaw uploadHandlerRaw = new UploadHandlerRaw(this.m_Event)
			{
				contentType = "application/json"
			};
			unityWebRequest.uploadHandler = uploadHandlerRaw;
			if (this.ConsentTracker.IsGeoIpChecked() && this.ConsentTracker.IsOptingOutInProgress())
			{
				foreach (KeyValuePair<string, string> keyValuePair in this.ConsentTracker.requiredHeaders)
				{
					unityWebRequest.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
			this.m_Request = unityWebRequest.SendWebRequest();
			this.m_Request.completed += this.UploadComplete;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003840 File Offset: 0x00001A40
		private void UploadComplete(AsyncOperation _)
		{
			long responseCode = this.m_Request.webRequest.responseCode;
			if (this.m_Request.webRequest.result == UnityWebRequest.Result.Success && responseCode == 204L)
			{
				this.m_SuccessfullyUploaded = true;
				this.m_Callback();
			}
			this.m_Request.webRequest.Dispose();
			this.m_Request = null;
		}

		// Token: 0x040000A0 RID: 160
		private string m_CollectUrl;

		// Token: 0x040000A1 RID: 161
		private byte[] m_Event;

		// Token: 0x040000A2 RID: 162
		private Action m_Callback;

		// Token: 0x040000A3 RID: 163
		private bool m_SuccessfullyUploaded;

		// Token: 0x040000A4 RID: 164
		private UnityWebRequestAsyncOperation m_Request;

		// Token: 0x040000A5 RID: 165
		private readonly IConsentTracker ConsentTracker;
	}
}
