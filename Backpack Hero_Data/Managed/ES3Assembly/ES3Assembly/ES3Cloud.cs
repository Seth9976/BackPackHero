using System;
using System.Collections;
using System.Text;
using ES3Internal;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000012 RID: 18
public class ES3Cloud : ES3WebClass
{
	// Token: 0x0600013E RID: 318 RVA: 0x00005626 File Offset: 0x00003826
	public ES3Cloud(string url, string apiKey)
		: base(url, apiKey)
	{
	}

	// Token: 0x0600013F RID: 319 RVA: 0x00005643 File Offset: 0x00003843
	public ES3Cloud(string url, string apiKey, int timeout)
		: base(url, apiKey)
	{
		this.timeout = timeout;
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000140 RID: 320 RVA: 0x00005667 File Offset: 0x00003867
	public byte[] data
	{
		get
		{
			return this._data;
		}
	}

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x06000141 RID: 321 RVA: 0x0000566F File Offset: 0x0000386F
	public string text
	{
		get
		{
			if (this.data == null)
			{
				return null;
			}
			return this.encoding.GetString(this.data);
		}
	}

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x06000142 RID: 322 RVA: 0x0000568C File Offset: 0x0000388C
	public string[] filenames
	{
		get
		{
			if (this.data == null || this.data.Length == 0)
			{
				return new string[0];
			}
			return this.text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
		}
	}

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x06000143 RID: 323 RVA: 0x000056C0 File Offset: 0x000038C0
	public DateTime timestamp
	{
		get
		{
			if (this.data == null || this.data.Length == 0)
			{
				return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			}
			double num;
			if (!double.TryParse(this.text, out num))
			{
				throw new FormatException("Could not convert downloaded data to a timestamp. Data downloaded was: " + this.text);
			}
			return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddSeconds(num);
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x0000572F File Offset: 0x0000392F
	public IEnumerator Sync()
	{
		return this.Sync(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00005748 File Offset: 0x00003948
	public IEnumerator Sync(string filePath)
	{
		return this.Sync(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00005761 File Offset: 0x00003961
	public IEnumerator Sync(string filePath, string user)
	{
		return this.Sync(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00005776 File Offset: 0x00003976
	public IEnumerator Sync(string filePath, string user, string password)
	{
		return this.Sync(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00005787 File Offset: 0x00003987
	public IEnumerator Sync(string filePath, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x06000149 RID: 329 RVA: 0x000057A0 File Offset: 0x000039A0
	public IEnumerator Sync(string filePath, string user, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x0600014A RID: 330 RVA: 0x000057B5 File Offset: 0x000039B5
	public IEnumerator Sync(string filePath, string user, string password, ES3Settings settings)
	{
		return this.Sync(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x000057C7 File Offset: 0x000039C7
	private IEnumerator Sync(ES3Settings settings, string user, string password)
	{
		this.Reset();
		yield return this.DownloadFile(settings, user, password, this.GetFileTimestamp(settings));
		if (this.errorCode == 3L)
		{
			this.Reset();
			if (ES3.FileExists(settings))
			{
				yield return this.UploadFile(settings, user, password);
			}
		}
		this.isDone = true;
		yield break;
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000057EB File Offset: 0x000039EB
	public IEnumerator UploadFile()
	{
		return this.UploadFile(new ES3Settings(null, null), "", "");
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00005804 File Offset: 0x00003A04
	public IEnumerator UploadFile(string filePath)
	{
		return this.UploadFile(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x0600014E RID: 334 RVA: 0x0000581D File Offset: 0x00003A1D
	public IEnumerator UploadFile(string filePath, string user)
	{
		return this.UploadFile(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00005832 File Offset: 0x00003A32
	public IEnumerator UploadFile(string filePath, string user, string password)
	{
		return this.UploadFile(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00005843 File Offset: 0x00003A43
	public IEnumerator UploadFile(string filePath, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x06000151 RID: 337 RVA: 0x0000585C File Offset: 0x00003A5C
	public IEnumerator UploadFile(string filePath, string user, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00005871 File Offset: 0x00003A71
	public IEnumerator UploadFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.UploadFile(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00005883 File Offset: 0x00003A83
	public IEnumerator UploadFile(ES3File es3File)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, "", "", this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000154 RID: 340 RVA: 0x000058AD File Offset: 0x00003AAD
	public IEnumerator UploadFile(ES3File es3File, string user)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, user, "", this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000155 RID: 341 RVA: 0x000058D3 File Offset: 0x00003AD3
	public IEnumerator UploadFile(ES3File es3File, string user, string password)
	{
		return this.UploadFile(es3File.GetBytes(null), es3File.settings, user, password, this.DateTimeToUnixTimestamp(DateTime.Now));
	}

	// Token: 0x06000156 RID: 342 RVA: 0x000058F5 File Offset: 0x00003AF5
	public IEnumerator UploadFile(ES3Settings settings, string user, string password)
	{
		return this.UploadFile(ES3.LoadRawBytes(settings), settings, user, password);
	}

	// Token: 0x06000157 RID: 343 RVA: 0x00005906 File Offset: 0x00003B06
	public IEnumerator UploadFile(byte[] bytes, ES3Settings settings, string user, string password)
	{
		return this.UploadFile(bytes, settings, user, password, this.DateTimeToUnixTimestamp(ES3.GetTimestamp(settings)));
	}

	// Token: 0x06000158 RID: 344 RVA: 0x0000591F File Offset: 0x00003B1F
	private IEnumerator UploadFile(byte[] bytes, ES3Settings settings, string user, string password, long fileTimestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("putFile", settings.path);
		wwwform.AddField("timestamp", fileTimestamp.ToString());
		wwwform.AddField("user", base.GetUser(user, password));
		wwwform.AddBinaryData("data", bytes, "data.dat", "multipart/form-data");
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x00005953 File Offset: 0x00003B53
	public IEnumerator DownloadFile()
	{
		return this.DownloadFile(new ES3Settings(null, null), "", "", 0L);
	}

	// Token: 0x0600015A RID: 346 RVA: 0x0000596E File Offset: 0x00003B6E
	public IEnumerator DownloadFile(string filePath)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), "", "", 0L);
	}

	// Token: 0x0600015B RID: 347 RVA: 0x00005989 File Offset: 0x00003B89
	public IEnumerator DownloadFile(string filePath, string user)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), user, "", 0L);
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000059A0 File Offset: 0x00003BA0
	public IEnumerator DownloadFile(string filePath, string user, string password)
	{
		return this.DownloadFile(new ES3Settings(filePath, null), user, password, 0L);
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000059B3 File Offset: 0x00003BB3
	public IEnumerator DownloadFile(string filePath, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), "", "", 0L);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x000059CE File Offset: 0x00003BCE
	public IEnumerator DownloadFile(string filePath, string user, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), user, "", 0L);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x000059E5 File Offset: 0x00003BE5
	public IEnumerator DownloadFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DownloadFile(new ES3Settings(filePath, settings), user, password, 0L);
	}

	// Token: 0x06000160 RID: 352 RVA: 0x000059F9 File Offset: 0x00003BF9
	public IEnumerator DownloadFile(ES3File es3File)
	{
		return this.DownloadFile(es3File, "", "", 0L);
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00005A0E File Offset: 0x00003C0E
	public IEnumerator DownloadFile(ES3File es3File, string user)
	{
		return this.DownloadFile(es3File, user, "", 0L);
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00005A1F File Offset: 0x00003C1F
	public IEnumerator DownloadFile(ES3File es3File, string user, string password)
	{
		return this.DownloadFile(es3File, user, password, 0L);
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00005A2C File Offset: 0x00003C2C
	private IEnumerator DownloadFile(ES3File es3File, string user, string password, long timestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFile", es3File.settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		if (timestamp > 0L)
		{
			wwwform.AddField("timestamp", timestamp.ToString());
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				if (webRequest.downloadedBytes > 0UL)
				{
					es3File.Clear();
					es3File.SaveRaw(webRequest.downloadHandler.data, null);
				}
				else
				{
					this.error = string.Format("File {0} was not found on the server.", es3File.settings.path);
					this.errorCode = 3L;
				}
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00005A58 File Offset: 0x00003C58
	private IEnumerator DownloadFile(ES3Settings settings, string user, string password, long timestamp)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFile", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		if (timestamp > 0L)
		{
			wwwform.AddField("timestamp", timestamp.ToString());
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				if (webRequest.downloadedBytes > 0UL)
				{
					ES3.SaveRaw(webRequest.downloadHandler.data, settings);
				}
				else
				{
					this.error = string.Format("File {0} was not found on the server.", settings.path);
					this.errorCode = 3L;
				}
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00005A84 File Offset: 0x00003C84
	public IEnumerator DeleteFile()
	{
		return this.DeleteFile(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00005A9D File Offset: 0x00003C9D
	public IEnumerator DeleteFile(string filePath)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00005AB6 File Offset: 0x00003CB6
	public IEnumerator DeleteFile(string filePath, string user)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000168 RID: 360 RVA: 0x00005ACB File Offset: 0x00003CCB
	public IEnumerator DeleteFile(string filePath, string user, string password)
	{
		return this.DeleteFile(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x06000169 RID: 361 RVA: 0x00005ADC File Offset: 0x00003CDC
	public IEnumerator DeleteFile(string filePath, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x0600016A RID: 362 RVA: 0x00005AF5 File Offset: 0x00003CF5
	public IEnumerator DeleteFile(string filePath, string user, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x0600016B RID: 363 RVA: 0x00005B0A File Offset: 0x00003D0A
	public IEnumerator DeleteFile(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DeleteFile(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x0600016C RID: 364 RVA: 0x00005B1C File Offset: 0x00003D1C
	private IEnumerator DeleteFile(ES3Settings settings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("deleteFile", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00005B40 File Offset: 0x00003D40
	public IEnumerator RenameFile(string filePath, string newFilePath)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), "", "");
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00005B60 File Offset: 0x00003D60
	public IEnumerator RenameFile(string filePath, string newFilePath, string user)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), user, "");
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00005B7C File Offset: 0x00003D7C
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, string password)
	{
		return this.RenameFile(new ES3Settings(filePath, null), new ES3Settings(newFilePath, null), user, password);
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00005B95 File Offset: 0x00003D95
	public IEnumerator RenameFile(string filePath, string newFilePath, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), "", "");
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00005BB5 File Offset: 0x00003DB5
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), user, "");
	}

	// Token: 0x06000172 RID: 370 RVA: 0x00005BD3 File Offset: 0x00003DD3
	public IEnumerator RenameFile(string filePath, string newFilePath, string user, string password, ES3Settings settings)
	{
		return this.RenameFile(new ES3Settings(filePath, settings), new ES3Settings(newFilePath, settings), user, password);
	}

	// Token: 0x06000173 RID: 371 RVA: 0x00005BEE File Offset: 0x00003DEE
	private IEnumerator RenameFile(ES3Settings settings, ES3Settings newSettings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("renameFile", settings.path);
		wwwform.AddField("newFilename", newSettings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			base.HandleError(webRequest, true);
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x00005C1A File Offset: 0x00003E1A
	public IEnumerator DownloadFilenames(string user = "", string password = "")
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFilenames", "");
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00005C37 File Offset: 0x00003E37
	public IEnumerator SearchFilenames(string searchPattern, string user = "", string password = "")
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getFilenames", "");
		wwwform.AddField("user", base.GetUser(user, password));
		if (!string.IsNullOrEmpty(searchPattern))
		{
			wwwform.AddField("pattern", searchPattern);
		}
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00005C5B File Offset: 0x00003E5B
	public IEnumerator DownloadTimestamp()
	{
		return this.DownloadTimestamp(new ES3Settings(null, null), "", "");
	}

	// Token: 0x06000177 RID: 375 RVA: 0x00005C74 File Offset: 0x00003E74
	public IEnumerator DownloadTimestamp(string filePath)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), "", "");
	}

	// Token: 0x06000178 RID: 376 RVA: 0x00005C8D File Offset: 0x00003E8D
	public IEnumerator DownloadTimestamp(string filePath, string user)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), user, "");
	}

	// Token: 0x06000179 RID: 377 RVA: 0x00005CA2 File Offset: 0x00003EA2
	public IEnumerator DownloadTimestamp(string filePath, string user, string password)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, null), user, password);
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00005CB3 File Offset: 0x00003EB3
	public IEnumerator DownloadTimestamp(string filePath, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), "", "");
	}

	// Token: 0x0600017B RID: 379 RVA: 0x00005CCC File Offset: 0x00003ECC
	public IEnumerator DownloadTimestamp(string filePath, string user, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), user, "");
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00005CE1 File Offset: 0x00003EE1
	public IEnumerator DownloadTimestamp(string filePath, string user, string password, ES3Settings settings)
	{
		return this.DownloadTimestamp(new ES3Settings(filePath, settings), user, password);
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00005CF3 File Offset: 0x00003EF3
	private IEnumerator DownloadTimestamp(ES3Settings settings, string user, string password)
	{
		this.Reset();
		WWWForm wwwform = base.CreateWWWForm();
		wwwform.AddField("apiKey", this.apiKey);
		wwwform.AddField("getTimestamp", settings.path);
		wwwform.AddField("user", base.GetUser(user, password));
		using (UnityWebRequest webRequest = UnityWebRequest.Post(this.url, wwwform))
		{
			webRequest.timeout = this.timeout;
			yield return base.SendWebRequest(webRequest);
			if (!base.HandleError(webRequest, false))
			{
				this._data = webRequest.downloadHandler.data;
			}
		}
		UnityWebRequest webRequest = null;
		this.isDone = true;
		yield break;
		yield break;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00005D18 File Offset: 0x00003F18
	private long DateTimeToUnixTimestamp(DateTime dt)
	{
		return Convert.ToInt64((dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00005D4F File Offset: 0x00003F4F
	private long GetFileTimestamp(ES3Settings settings)
	{
		return this.DateTimeToUnixTimestamp(ES3.GetTimestamp(settings));
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00005D5D File Offset: 0x00003F5D
	protected override void Reset()
	{
		this._data = null;
		base.Reset();
	}

	// Token: 0x04000040 RID: 64
	private int timeout = 20;

	// Token: 0x04000041 RID: 65
	public Encoding encoding = Encoding.UTF8;

	// Token: 0x04000042 RID: 66
	private byte[] _data;
}
