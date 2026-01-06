using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace ES3Internal
{
	// Token: 0x020000D9 RID: 217
	public class ES3WebClass
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00022210 File Offset: 0x00020410
		public float uploadProgress
		{
			get
			{
				if (this._webRequest == null)
				{
					return 0f;
				}
				return this._webRequest.uploadProgress;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0002222B File Offset: 0x0002042B
		public float downloadProgress
		{
			get
			{
				if (this._webRequest == null)
				{
					return 0f;
				}
				return this._webRequest.downloadProgress;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00022246 File Offset: 0x00020446
		public bool isError
		{
			get
			{
				return !string.IsNullOrEmpty(this.error) || this.errorCode > 0L;
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00022261 File Offset: 0x00020461
		public static bool IsNetworkError(UnityWebRequest www)
		{
			return www.result == UnityWebRequest.Result.ConnectionError;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0002226C File Offset: 0x0002046C
		protected ES3WebClass(string url, string apiKey)
		{
			this.url = url;
			this.apiKey = apiKey;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0002228D File Offset: 0x0002048D
		public void AddPOSTField(string fieldName, string value)
		{
			this.formData.Add(new KeyValuePair<string, string>(fieldName, value));
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000222A1 File Offset: 0x000204A1
		protected string GetUser(string user, string password)
		{
			if (string.IsNullOrEmpty(user))
			{
				return "";
			}
			if (!string.IsNullOrEmpty(password))
			{
				user += password;
			}
			user = ES3Hash.SHA1Hash(user);
			return user;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000222CC File Offset: 0x000204CC
		protected WWWForm CreateWWWForm()
		{
			WWWForm wwwform = new WWWForm();
			foreach (KeyValuePair<string, string> keyValuePair in this.formData)
			{
				wwwform.AddField(keyValuePair.Key, keyValuePair.Value);
			}
			return wwwform;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00022334 File Offset: 0x00020534
		protected bool HandleError(UnityWebRequest webRequest, bool errorIfDataIsDownloaded)
		{
			if (ES3WebClass.IsNetworkError(webRequest))
			{
				this.errorCode = 1L;
				this.error = "Error: " + webRequest.error;
			}
			else if (webRequest.responseCode >= 400L)
			{
				this.errorCode = webRequest.responseCode;
				if (string.IsNullOrEmpty(webRequest.downloadHandler.text))
				{
					this.error = string.Format("Server returned {0} error with no message", webRequest.responseCode);
				}
				else
				{
					this.error = webRequest.downloadHandler.text;
				}
			}
			else
			{
				if (!errorIfDataIsDownloaded || webRequest.downloadedBytes <= 0UL)
				{
					return false;
				}
				this.errorCode = 2L;
				this.error = "Server error: " + webRequest.downloadHandler.text;
			}
			return true;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x000223FD File Offset: 0x000205FD
		protected IEnumerator SendWebRequest(UnityWebRequest webRequest)
		{
			this._webRequest = webRequest;
			yield return webRequest.SendWebRequest();
			yield break;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00022413 File Offset: 0x00020613
		protected virtual void Reset()
		{
			this.error = null;
			this.errorCode = 0L;
			this.isDone = false;
		}

		// Token: 0x04000138 RID: 312
		protected string url;

		// Token: 0x04000139 RID: 313
		protected string apiKey;

		// Token: 0x0400013A RID: 314
		protected List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>();

		// Token: 0x0400013B RID: 315
		protected UnityWebRequest _webRequest;

		// Token: 0x0400013C RID: 316
		public bool isDone;

		// Token: 0x0400013D RID: 317
		public string error;

		// Token: 0x0400013E RID: 318
		public long errorCode;
	}
}
