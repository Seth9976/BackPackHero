using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using UnityEngine.Bindings;
using UnityEngineInternal;

namespace UnityEngine.Networking
{
	// Token: 0x02000009 RID: 9
	[NativeHeader("Modules/UnityWebRequest/Public/UnityWebRequest.h")]
	[StructLayout(0)]
	public class UnityWebRequest : IDisposable
	{
		// Token: 0x0600004B RID: 75
		[NativeMethod(IsThreadSafe = true)]
		[NativeConditional("ENABLE_UNITYWEBREQUEST")]
		[MethodImpl(4096)]
		private static extern string GetWebErrorString(UnityWebRequest.UnityWebRequestError err);

		// Token: 0x0600004C RID: 76
		[VisibleToOtherModules]
		[MethodImpl(4096)]
		internal static extern string GetHTTPStatusString(long responseCode);

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000035DE File Offset: 0x000017DE
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000035E6 File Offset: 0x000017E6
		public bool disposeCertificateHandlerOnDispose { get; set; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000035EF File Offset: 0x000017EF
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000035F7 File Offset: 0x000017F7
		public bool disposeDownloadHandlerOnDispose { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003600 File Offset: 0x00001800
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003608 File Offset: 0x00001808
		public bool disposeUploadHandlerOnDispose { get; set; }

		// Token: 0x06000053 RID: 83 RVA: 0x00003611 File Offset: 0x00001811
		public static void ClearCookieCache()
		{
			UnityWebRequest.ClearCookieCache(null, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000361C File Offset: 0x0000181C
		public static void ClearCookieCache(Uri uri)
		{
			bool flag = uri == null;
			if (flag)
			{
				UnityWebRequest.ClearCookieCache(null, null);
			}
			else
			{
				string host = uri.Host;
				string text = uri.AbsolutePath;
				bool flag2 = text == "/";
				if (flag2)
				{
					text = null;
				}
				UnityWebRequest.ClearCookieCache(host, text);
			}
		}

		// Token: 0x06000055 RID: 85
		[MethodImpl(4096)]
		private static extern void ClearCookieCache(string domain, string path);

		// Token: 0x06000056 RID: 86
		[MethodImpl(4096)]
		internal static extern IntPtr Create();

		// Token: 0x06000057 RID: 87
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		private extern void Release();

		// Token: 0x06000058 RID: 88 RVA: 0x00003668 File Offset: 0x00001868
		internal void InternalDestroy()
		{
			bool flag = this.m_Ptr != IntPtr.Zero;
			if (flag)
			{
				this.Abort();
				this.Release();
				this.m_Ptr = IntPtr.Zero;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000036A5 File Offset: 0x000018A5
		private void InternalSetDefaults()
		{
			this.disposeDownloadHandlerOnDispose = true;
			this.disposeUploadHandlerOnDispose = true;
			this.disposeCertificateHandlerOnDispose = true;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000036C0 File Offset: 0x000018C0
		public UnityWebRequest()
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000036DC File Offset: 0x000018DC
		public UnityWebRequest(string url)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003700 File Offset: 0x00001900
		public UnityWebRequest(Uri uri)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00003724 File Offset: 0x00001924
		public UnityWebRequest(string url, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003750 File Offset: 0x00001950
		public UnityWebRequest(Uri uri, string method)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000377C File Offset: 0x0000197C
		public UnityWebRequest(string url, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.url = url;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x000037B9 File Offset: 0x000019B9
		public UnityWebRequest(Uri uri, string method, DownloadHandler downloadHandler, UploadHandler uploadHandler)
		{
			this.m_Ptr = UnityWebRequest.Create();
			this.InternalSetDefaults();
			this.uri = uri;
			this.method = method;
			this.downloadHandler = downloadHandler;
			this.uploadHandler = uploadHandler;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x000037F8 File Offset: 0x000019F8
		~UnityWebRequest()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00003830 File Offset: 0x00001A30
		public void Dispose()
		{
			this.DisposeHandlers();
			this.InternalDestroy();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003848 File Offset: 0x00001A48
		private void DisposeHandlers()
		{
			bool disposeDownloadHandlerOnDispose = this.disposeDownloadHandlerOnDispose;
			if (disposeDownloadHandlerOnDispose)
			{
				DownloadHandler downloadHandler = this.downloadHandler;
				bool flag = downloadHandler != null;
				if (flag)
				{
					downloadHandler.Dispose();
				}
			}
			bool disposeUploadHandlerOnDispose = this.disposeUploadHandlerOnDispose;
			if (disposeUploadHandlerOnDispose)
			{
				UploadHandler uploadHandler = this.uploadHandler;
				bool flag2 = uploadHandler != null;
				if (flag2)
				{
					uploadHandler.Dispose();
				}
			}
			bool disposeCertificateHandlerOnDispose = this.disposeCertificateHandlerOnDispose;
			if (disposeCertificateHandlerOnDispose)
			{
				CertificateHandler certificateHandler = this.certificateHandler;
				bool flag3 = certificateHandler != null;
				if (flag3)
				{
					certificateHandler.Dispose();
				}
			}
		}

		// Token: 0x06000064 RID: 100
		[NativeThrows]
		[MethodImpl(4096)]
		internal extern UnityWebRequestAsyncOperation BeginWebRequest();

		// Token: 0x06000065 RID: 101 RVA: 0x000038D0 File Offset: 0x00001AD0
		[Obsolete("Use SendWebRequest.  It returns a UnityWebRequestAsyncOperation which contains a reference to the WebRequest object.", false)]
		public AsyncOperation Send()
		{
			return this.SendWebRequest();
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000038E8 File Offset: 0x00001AE8
		public UnityWebRequestAsyncOperation SendWebRequest()
		{
			UnityWebRequestAsyncOperation unityWebRequestAsyncOperation = this.BeginWebRequest();
			bool flag = unityWebRequestAsyncOperation != null;
			if (flag)
			{
				unityWebRequestAsyncOperation.webRequest = this;
			}
			return unityWebRequestAsyncOperation;
		}

		// Token: 0x06000067 RID: 103
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(4096)]
		public extern void Abort();

		// Token: 0x06000068 RID: 104
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetMethod(UnityWebRequest.UnityWebRequestMethod methodType);

		// Token: 0x06000069 RID: 105 RVA: 0x00003914 File Offset: 0x00001B14
		internal void InternalSetMethod(UnityWebRequest.UnityWebRequestMethod methodType)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetMethod(methodType);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x0600006A RID: 106
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetCustomMethod(string customMethodName);

		// Token: 0x0600006B RID: 107 RVA: 0x00003958 File Offset: 0x00001B58
		internal void InternalSetCustomMethod(string customMethodName)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request method can no longer be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCustomMethod(customMethodName);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x0600006C RID: 108
		[MethodImpl(4096)]
		internal extern UnityWebRequest.UnityWebRequestMethod GetMethod();

		// Token: 0x0600006D RID: 109
		[MethodImpl(4096)]
		internal extern string GetCustomMethod();

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x0600006F RID: 111 RVA: 0x000039F8 File Offset: 0x00001BF8
		public string method
		{
			get
			{
				string text;
				switch (this.GetMethod())
				{
				case UnityWebRequest.UnityWebRequestMethod.Get:
					text = "GET";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Post:
					text = "POST";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Put:
					text = "PUT";
					break;
				case UnityWebRequest.UnityWebRequestMethod.Head:
					text = "HEAD";
					break;
				default:
					text = this.GetCustomMethod();
					break;
				}
				return text;
			}
			set
			{
				bool flag = string.IsNullOrEmpty(value);
				if (flag)
				{
					throw new ArgumentException("Cannot set a UnityWebRequest's method to an empty or null string");
				}
				string text = value.ToUpper();
				string text2 = text;
				if (!(text2 == "GET"))
				{
					if (!(text2 == "POST"))
					{
						if (!(text2 == "PUT"))
						{
							if (!(text2 == "HEAD"))
							{
								this.InternalSetCustomMethod(value.ToUpper());
							}
							else
							{
								this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Head);
							}
						}
						else
						{
							this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Put);
						}
					}
					else
					{
						this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Post);
					}
				}
				else
				{
					this.InternalSetMethod(UnityWebRequest.UnityWebRequestMethod.Get);
				}
			}
		}

		// Token: 0x06000070 RID: 112
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError GetError();

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003A94 File Offset: 0x00001C94
		public string error
		{
			get
			{
				UnityWebRequest.Result result = this.result;
				UnityWebRequest.Result result2 = result;
				string text;
				if (result2 > UnityWebRequest.Result.Success)
				{
					if (result2 != UnityWebRequest.Result.ProtocolError)
					{
						text = UnityWebRequest.GetWebErrorString(this.GetError());
					}
					else
					{
						text = string.Format("HTTP/1.1 {0} {1}", this.responseCode, UnityWebRequest.GetHTTPStatusString(this.responseCode));
					}
				}
				else
				{
					text = null;
				}
				return text;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000072 RID: 114
		// (set) Token: 0x06000073 RID: 115
		private extern bool use100Continue
		{
			[MethodImpl(4096)]
			get;
			[MethodImpl(4096)]
			set;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003AF0 File Offset: 0x00001CF0
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003B08 File Offset: 0x00001D08
		public bool useHttpContinue
		{
			get
			{
				return this.use100Continue;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its 100-Continue setting cannot be altered");
				}
				this.use100Continue = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003B38 File Offset: 0x00001D38
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00003B50 File Offset: 0x00001D50
		public string url
		{
			get
			{
				return this.GetUrl();
			}
			set
			{
				string text = "http://localhost/";
				this.InternalSetUrl(WebRequestUtils.MakeInitialUrl(value, text));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003B74 File Offset: 0x00001D74
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00003B94 File Offset: 0x00001D94
		public Uri uri
		{
			get
			{
				return new Uri(this.GetUrl());
			}
			set
			{
				bool flag = !value.IsAbsoluteUri;
				if (flag)
				{
					throw new ArgumentException("URI must be absolute");
				}
				this.InternalSetUrl(WebRequestUtils.MakeUriString(value, value.OriginalString, false));
				this.m_Uri = value;
			}
		}

		// Token: 0x0600007A RID: 122
		[MethodImpl(4096)]
		private extern string GetUrl();

		// Token: 0x0600007B RID: 123
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetUrl(string url);

		// Token: 0x0600007C RID: 124 RVA: 0x00003BD8 File Offset: 0x00001DD8
		private void InternalSetUrl(string url)
		{
			bool flag = !this.isModifiable;
			if (flag)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its URL cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUrl(url);
			bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag2)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007D RID: 125
		public extern long responseCode
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600007E RID: 126
		[MethodImpl(4096)]
		private extern float GetUploadProgress();

		// Token: 0x0600007F RID: 127
		[MethodImpl(4096)]
		private extern bool IsExecuting();

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00003C1C File Offset: 0x00001E1C
		public float uploadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetUploadProgress();
				}
				return num;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000081 RID: 129
		public extern bool isModifiable
		{
			[NativeMethod("IsModifiable")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00003C54 File Offset: 0x00001E54
		public bool isDone
		{
			get
			{
				return this.result > UnityWebRequest.Result.InProgress;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003C70 File Offset: 0x00001E70
		[Obsolete("UnityWebRequest.isNetworkError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ConnectionError) instead.", false)]
		public bool isNetworkError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ConnectionError;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00003C8C File Offset: 0x00001E8C
		[Obsolete("UnityWebRequest.isHttpError is deprecated. Use (UnityWebRequest.result == UnityWebRequest.Result.ProtocolError) instead.", false)]
		public bool isHttpError
		{
			get
			{
				return this.result == UnityWebRequest.Result.ProtocolError;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000085 RID: 133
		public extern UnityWebRequest.Result result
		{
			[NativeMethod("GetResult")]
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x06000086 RID: 134
		[MethodImpl(4096)]
		private extern float GetDownloadProgress();

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public float downloadProgress
		{
			get
			{
				bool flag = !this.IsExecuting() && !this.isDone;
				float num;
				if (flag)
				{
					num = -1f;
				}
				else
				{
					num = this.GetDownloadProgress();
				}
				return num;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000088 RID: 136
		public extern ulong uploadedBytes
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000089 RID: 137
		public extern ulong downloadedBytes
		{
			[MethodImpl(4096)]
			get;
		}

		// Token: 0x0600008A RID: 138
		[MethodImpl(4096)]
		private extern int GetRedirectLimit();

		// Token: 0x0600008B RID: 139
		[NativeThrows]
		[MethodImpl(4096)]
		private extern void SetRedirectLimitFromScripting(int limit);

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003CE0 File Offset: 0x00001EE0
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00003CF8 File Offset: 0x00001EF8
		public int redirectLimit
		{
			get
			{
				return this.GetRedirectLimit();
			}
			set
			{
				this.SetRedirectLimitFromScripting(value);
			}
		}

		// Token: 0x0600008E RID: 142
		[MethodImpl(4096)]
		private extern bool GetChunked();

		// Token: 0x0600008F RID: 143
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetChunked(bool chunked);

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00003D04 File Offset: 0x00001F04
		// (set) Token: 0x06000091 RID: 145 RVA: 0x00003D1C File Offset: 0x00001F1C
		[Obsolete("HTTP/2 and many HTTP/1.1 servers don't support this; we recommend leaving it set to false (default).", false)]
		public bool chunkedTransfer
		{
			get
			{
				return this.GetChunked();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent and its chunked transfer encoding setting cannot be altered");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetChunked(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x06000092 RID: 146
		[MethodImpl(4096)]
		public extern string GetRequestHeader(string name);

		// Token: 0x06000093 RID: 147
		[NativeMethod("SetRequestHeader")]
		[MethodImpl(4096)]
		internal extern UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(string name, string value);

		// Token: 0x06000094 RID: 148 RVA: 0x00003D60 File Offset: 0x00001F60
		public void SetRequestHeader(string name, string value)
		{
			bool flag = string.IsNullOrEmpty(name);
			if (flag)
			{
				throw new ArgumentException("Cannot set a Request Header with a null or empty name");
			}
			bool flag2 = value == null;
			if (flag2)
			{
				throw new ArgumentException("Cannot set a Request header with a null");
			}
			bool flag3 = !this.isModifiable;
			if (flag3)
			{
				throw new InvalidOperationException("UnityWebRequest has already been sent and its request headers cannot be altered");
			}
			UnityWebRequest.UnityWebRequestError unityWebRequestError = this.InternalSetRequestHeader(name, value);
			bool flag4 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
			if (flag4)
			{
				throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
			}
		}

		// Token: 0x06000095 RID: 149
		[MethodImpl(4096)]
		public extern string GetResponseHeader(string name);

		// Token: 0x06000096 RID: 150
		[MethodImpl(4096)]
		internal extern string[] GetResponseHeaderKeys();

		// Token: 0x06000097 RID: 151 RVA: 0x00003DD0 File Offset: 0x00001FD0
		public Dictionary<string, string> GetResponseHeaders()
		{
			string[] responseHeaderKeys = this.GetResponseHeaderKeys();
			bool flag = responseHeaderKeys == null || responseHeaderKeys.Length == 0;
			Dictionary<string, string> dictionary;
			if (flag)
			{
				dictionary = null;
			}
			else
			{
				Dictionary<string, string> dictionary2 = new Dictionary<string, string>(responseHeaderKeys.Length, StringComparer.OrdinalIgnoreCase);
				for (int i = 0; i < responseHeaderKeys.Length; i++)
				{
					string responseHeader = this.GetResponseHeader(responseHeaderKeys[i]);
					dictionary2.Add(responseHeaderKeys[i], responseHeader);
				}
				dictionary = dictionary2;
			}
			return dictionary;
		}

		// Token: 0x06000098 RID: 152
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetUploadHandler(UploadHandler uh);

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003E40 File Offset: 0x00002040
		// (set) Token: 0x0600009A RID: 154 RVA: 0x00003E58 File Offset: 0x00002058
		public UploadHandler uploadHandler
		{
			get
			{
				return this.m_UploadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the upload handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetUploadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_UploadHandler = value;
			}
		}

		// Token: 0x0600009B RID: 155
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetDownloadHandler(DownloadHandler dh);

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003EA4 File Offset: 0x000020A4
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003EBC File Offset: 0x000020BC
		public DownloadHandler downloadHandler
		{
			get
			{
				return this.m_DownloadHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the download handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetDownloadHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_DownloadHandler = value;
			}
		}

		// Token: 0x0600009E RID: 158
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetCertificateHandler(CertificateHandler ch);

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003F08 File Offset: 0x00002108
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003F20 File Offset: 0x00002120
		public CertificateHandler certificateHandler
		{
			get
			{
				return this.m_CertificateHandler;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the certificate handler");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetCertificateHandler(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
				this.m_CertificateHandler = value;
			}
		}

		// Token: 0x060000A1 RID: 161
		[MethodImpl(4096)]
		private extern int GetTimeoutMsec();

		// Token: 0x060000A2 RID: 162
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetTimeoutMsec(int timeout);

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00003F6C File Offset: 0x0000216C
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00003F8C File Offset: 0x0000218C
		public int timeout
		{
			get
			{
				return this.GetTimeoutMsec() / 1000;
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				value = Math.Max(value, 0);
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetTimeoutMsec(value * 1000);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A5 RID: 165
		[MethodImpl(4096)]
		private extern bool GetSuppressErrorsToConsole();

		// Token: 0x060000A6 RID: 166
		[MethodImpl(4096)]
		private extern UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(bool suppress);

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003FE0 File Offset: 0x000021E0
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00003FF8 File Offset: 0x000021F8
		internal bool suppressErrorsToConsole
		{
			get
			{
				return this.GetSuppressErrorsToConsole();
			}
			set
			{
				bool flag = !this.isModifiable;
				if (flag)
				{
					throw new InvalidOperationException("UnityWebRequest has already been sent; cannot modify the timeout");
				}
				UnityWebRequest.UnityWebRequestError unityWebRequestError = this.SetSuppressErrorsToConsole(value);
				bool flag2 = unityWebRequestError > UnityWebRequest.UnityWebRequestError.OK;
				if (flag2)
				{
					throw new InvalidOperationException(UnityWebRequest.GetWebErrorString(unityWebRequestError));
				}
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000403C File Offset: 0x0000223C
		public static UnityWebRequest Get(string uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004064 File Offset: 0x00002264
		public static UnityWebRequest Get(Uri uri)
		{
			return new UnityWebRequest(uri, "GET", new DownloadHandlerBuffer(), null);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000408C File Offset: 0x0000228C
		public static UnityWebRequest Delete(string uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000040AC File Offset: 0x000022AC
		public static UnityWebRequest Delete(Uri uri)
		{
			return new UnityWebRequest(uri, "DELETE");
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000040CC File Offset: 0x000022CC
		public static UnityWebRequest Head(string uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000040EC File Offset: 0x000022EC
		public static UnityWebRequest Head(Uri uri)
		{
			return new UnityWebRequest(uri, "HEAD");
		}

		// Token: 0x060000AF RID: 175 RVA: 0x0000410B File Offset: 0x0000230B
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		[EditorBrowsable(1)]
		public static UnityWebRequest GetTexture(string uri)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000410B File Offset: 0x0000230B
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestTexture.GetTexture(*)", true)]
		public static UnityWebRequest GetTexture(string uri, bool nonReadable)
		{
			throw new NotSupportedException("UnityWebRequest.GetTexture is obsolete. Use UnityWebRequestTexture.GetTexture instead.");
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004118 File Offset: 0x00002318
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAudioClip is obsolete. Use UnityWebRequestMultimedia.GetAudioClip instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestMultimedia.GetAudioClip(*)", true)]
		public static UnityWebRequest GetAudioClip(string uri, AudioType audioType)
		{
			return null;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000412C File Offset: 0x0000232C
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri)
		{
			return null;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004140 File Offset: 0x00002340
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint crc)
		{
			return null;
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004154 File Offset: 0x00002354
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, uint version, uint crc)
		{
			return null;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004168 File Offset: 0x00002368
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, Hash128 hash, uint crc)
		{
			return null;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x0000417C File Offset: 0x0000237C
		[EditorBrowsable(1)]
		[Obsolete("UnityWebRequest.GetAssetBundle is obsolete. Use UnityWebRequestAssetBundle.GetAssetBundle instead (UnityUpgradable) -> [UnityEngine] UnityWebRequestAssetBundle.GetAssetBundle(*)", true)]
		public static UnityWebRequest GetAssetBundle(string uri, CachedAssetBundle cachedAssetBundle, uint crc)
		{
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00004190 File Offset: 0x00002390
		public static UnityWebRequest Put(string uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000041BC File Offset: 0x000023BC
		public static UnityWebRequest Put(Uri uri, byte[] bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(bodyData));
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000041E8 File Offset: 0x000023E8
		public static UnityWebRequest Put(string uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000421C File Offset: 0x0000241C
		public static UnityWebRequest Put(Uri uri, string bodyData)
		{
			return new UnityWebRequest(uri, "PUT", new DownloadHandlerBuffer(), new UploadHandlerRaw(Encoding.UTF8.GetBytes(bodyData)));
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004250 File Offset: 0x00002450
		public static UnityWebRequest Post(string uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004278 File Offset: 0x00002478
		public static UnityWebRequest Post(Uri uri, string postData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, postData);
			return unityWebRequest;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000042A0 File Offset: 0x000024A0
		private static void SetupPost(UnityWebRequest request, string postData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = string.IsNullOrEmpty(postData);
			if (!flag)
			{
				string text = WWWTranscoder.DataEncode(postData, Encoding.UTF8);
				byte[] bytes = Encoding.UTF8.GetBytes(text);
				request.uploadHandler = new UploadHandlerRaw(bytes);
				request.uploadHandler.contentType = "application/x-www-form-urlencoded";
			}
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00004300 File Offset: 0x00002500
		public static UnityWebRequest Post(string uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004328 File Offset: 0x00002528
		public static UnityWebRequest Post(Uri uri, WWWForm formData)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formData);
			return unityWebRequest;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00004350 File Offset: 0x00002550
		private static void SetupPost(UnityWebRequest request, WWWForm formData)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			bool flag = formData == null;
			if (!flag)
			{
				byte[] array = formData.data;
				bool flag2 = array.Length == 0;
				if (flag2)
				{
					array = null;
				}
				bool flag3 = array != null;
				if (flag3)
				{
					request.uploadHandler = new UploadHandlerRaw(array);
				}
				Dictionary<string, string> headers = formData.headers;
				foreach (KeyValuePair<string, string> keyValuePair in headers)
				{
					request.SetRequestHeader(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000043FC File Offset: 0x000025FC
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000441C File Offset: 0x0000261C
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections)
		{
			byte[] array = UnityWebRequest.GenerateBoundary();
			return UnityWebRequest.Post(uri, multipartFormSections, array);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000443C File Offset: 0x0000263C
		public static UnityWebRequest Post(string uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004464 File Offset: 0x00002664
		public static UnityWebRequest Post(Uri uri, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, multipartFormSections, boundary);
			return unityWebRequest;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000448C File Offset: 0x0000268C
		private static void SetupPost(UnityWebRequest request, List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = multipartFormSections != null && multipartFormSections.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeFormSections(multipartFormSections, boundary);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "multipart/form-data; boundary=" + Encoding.UTF8.GetString(boundary, 0, boundary.Length)
				};
			}
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000044FC File Offset: 0x000026FC
		public static UnityWebRequest Post(string uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004524 File Offset: 0x00002724
		public static UnityWebRequest Post(Uri uri, Dictionary<string, string> formFields)
		{
			UnityWebRequest unityWebRequest = new UnityWebRequest(uri, "POST");
			UnityWebRequest.SetupPost(unityWebRequest, formFields);
			return unityWebRequest;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000454C File Offset: 0x0000274C
		private static void SetupPost(UnityWebRequest request, Dictionary<string, string> formFields)
		{
			request.downloadHandler = new DownloadHandlerBuffer();
			byte[] array = null;
			bool flag = formFields != null && formFields.Count != 0;
			if (flag)
			{
				array = UnityWebRequest.SerializeSimpleForm(formFields);
			}
			bool flag2 = array == null;
			if (!flag2)
			{
				request.uploadHandler = new UploadHandlerRaw(array)
				{
					contentType = "application/x-www-form-urlencoded"
				};
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000045A8 File Offset: 0x000027A8
		public static string EscapeURL(string s)
		{
			return UnityWebRequest.EscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000045C8 File Offset: 0x000027C8
		public static string EscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s == "";
				if (flag2)
				{
					text = "";
				}
				else
				{
					bool flag3 = e == null;
					if (flag3)
					{
						text = null;
					}
					else
					{
						byte[] bytes = e.GetBytes(s);
						byte[] array = WWWTranscoder.URLEncode(bytes);
						text = e.GetString(array);
					}
				}
			}
			return text;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004624 File Offset: 0x00002824
		public static string UnEscapeURL(string s)
		{
			return UnityWebRequest.UnEscapeURL(s, Encoding.UTF8);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00004644 File Offset: 0x00002844
		public static string UnEscapeURL(string s, Encoding e)
		{
			bool flag = s == null;
			string text;
			if (flag)
			{
				text = null;
			}
			else
			{
				bool flag2 = s.IndexOf('%') == -1 && s.IndexOf('+') == -1;
				if (flag2)
				{
					text = s;
				}
				else
				{
					byte[] bytes = e.GetBytes(s);
					byte[] array = WWWTranscoder.URLDecode(bytes);
					text = e.GetString(array);
				}
			}
			return text;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x0000469C File Offset: 0x0000289C
		public static byte[] SerializeFormSections(List<IMultipartFormSection> multipartFormSections, byte[] boundary)
		{
			bool flag = multipartFormSections == null || multipartFormSections.Count == 0;
			byte[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				byte[] bytes = Encoding.UTF8.GetBytes("\r\n");
				byte[] bytes2 = WWWForm.DefaultEncoding.GetBytes("--");
				int num = 0;
				foreach (IMultipartFormSection multipartFormSection in multipartFormSections)
				{
					num += 64 + multipartFormSection.sectionData.Length;
				}
				List<byte> list = new List<byte>(num);
				foreach (IMultipartFormSection multipartFormSection2 in multipartFormSections)
				{
					string text = "form-data";
					string sectionName = multipartFormSection2.sectionName;
					string fileName = multipartFormSection2.fileName;
					string text2 = "Content-Disposition: " + text;
					bool flag2 = !string.IsNullOrEmpty(sectionName);
					if (flag2)
					{
						text2 = text2 + "; name=\"" + sectionName + "\"";
					}
					bool flag3 = !string.IsNullOrEmpty(fileName);
					if (flag3)
					{
						text2 = text2 + "; filename=\"" + fileName + "\"";
					}
					text2 += "\r\n";
					string contentType = multipartFormSection2.contentType;
					bool flag4 = !string.IsNullOrEmpty(contentType);
					if (flag4)
					{
						text2 = text2 + "Content-Type: " + contentType + "\r\n";
					}
					list.AddRange(bytes);
					list.AddRange(bytes2);
					list.AddRange(boundary);
					list.AddRange(bytes);
					list.AddRange(Encoding.UTF8.GetBytes(text2));
					list.AddRange(bytes);
					list.AddRange(multipartFormSection2.sectionData);
				}
				list.AddRange(bytes);
				list.AddRange(bytes2);
				list.AddRange(boundary);
				list.AddRange(bytes2);
				list.AddRange(bytes);
				array = list.ToArray();
			}
			return array;
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000048CC File Offset: 0x00002ACC
		public static byte[] GenerateBoundary()
		{
			byte[] array = new byte[40];
			for (int i = 0; i < 40; i++)
			{
				int num = Random.Range(48, 110);
				bool flag = num > 57;
				if (flag)
				{
					num += 7;
				}
				bool flag2 = num > 90;
				if (flag2)
				{
					num += 6;
				}
				array[i] = (byte)num;
			}
			return array;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x0000492C File Offset: 0x00002B2C
		public static byte[] SerializeSimpleForm(Dictionary<string, string> formFields)
		{
			string text = "";
			foreach (KeyValuePair<string, string> keyValuePair in formFields)
			{
				bool flag = text.Length > 0;
				if (flag)
				{
					text += "&";
				}
				text = text + WWWTranscoder.DataEncode(keyValuePair.Key) + "=" + WWWTranscoder.DataEncode(keyValuePair.Value);
			}
			return Encoding.UTF8.GetBytes(text);
		}

		// Token: 0x04000021 RID: 33
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000022 RID: 34
		[NonSerialized]
		internal DownloadHandler m_DownloadHandler;

		// Token: 0x04000023 RID: 35
		[NonSerialized]
		internal UploadHandler m_UploadHandler;

		// Token: 0x04000024 RID: 36
		[NonSerialized]
		internal CertificateHandler m_CertificateHandler;

		// Token: 0x04000025 RID: 37
		[NonSerialized]
		internal Uri m_Uri;

		// Token: 0x04000026 RID: 38
		public const string kHttpVerbGET = "GET";

		// Token: 0x04000027 RID: 39
		public const string kHttpVerbHEAD = "HEAD";

		// Token: 0x04000028 RID: 40
		public const string kHttpVerbPOST = "POST";

		// Token: 0x04000029 RID: 41
		public const string kHttpVerbPUT = "PUT";

		// Token: 0x0400002A RID: 42
		public const string kHttpVerbCREATE = "CREATE";

		// Token: 0x0400002B RID: 43
		public const string kHttpVerbDELETE = "DELETE";

		// Token: 0x0200000A RID: 10
		internal enum UnityWebRequestMethod
		{
			// Token: 0x04000030 RID: 48
			Get,
			// Token: 0x04000031 RID: 49
			Post,
			// Token: 0x04000032 RID: 50
			Put,
			// Token: 0x04000033 RID: 51
			Head,
			// Token: 0x04000034 RID: 52
			Custom
		}

		// Token: 0x0200000B RID: 11
		internal enum UnityWebRequestError
		{
			// Token: 0x04000036 RID: 54
			OK,
			// Token: 0x04000037 RID: 55
			Unknown,
			// Token: 0x04000038 RID: 56
			SDKError,
			// Token: 0x04000039 RID: 57
			UnsupportedProtocol,
			// Token: 0x0400003A RID: 58
			MalformattedUrl,
			// Token: 0x0400003B RID: 59
			CannotResolveProxy,
			// Token: 0x0400003C RID: 60
			CannotResolveHost,
			// Token: 0x0400003D RID: 61
			CannotConnectToHost,
			// Token: 0x0400003E RID: 62
			AccessDenied,
			// Token: 0x0400003F RID: 63
			GenericHttpError,
			// Token: 0x04000040 RID: 64
			WriteError,
			// Token: 0x04000041 RID: 65
			ReadError,
			// Token: 0x04000042 RID: 66
			OutOfMemory,
			// Token: 0x04000043 RID: 67
			Timeout,
			// Token: 0x04000044 RID: 68
			HTTPPostError,
			// Token: 0x04000045 RID: 69
			SSLCannotConnect,
			// Token: 0x04000046 RID: 70
			Aborted,
			// Token: 0x04000047 RID: 71
			TooManyRedirects,
			// Token: 0x04000048 RID: 72
			ReceivedNoData,
			// Token: 0x04000049 RID: 73
			SSLNotSupported,
			// Token: 0x0400004A RID: 74
			FailedToSendData,
			// Token: 0x0400004B RID: 75
			FailedToReceiveData,
			// Token: 0x0400004C RID: 76
			SSLCertificateError,
			// Token: 0x0400004D RID: 77
			SSLCipherNotAvailable,
			// Token: 0x0400004E RID: 78
			SSLCACertError,
			// Token: 0x0400004F RID: 79
			UnrecognizedContentEncoding,
			// Token: 0x04000050 RID: 80
			LoginFailed,
			// Token: 0x04000051 RID: 81
			SSLShutdownFailed,
			// Token: 0x04000052 RID: 82
			NoInternetConnection
		}

		// Token: 0x0200000C RID: 12
		public enum Result
		{
			// Token: 0x04000054 RID: 84
			InProgress,
			// Token: 0x04000055 RID: 85
			Success,
			// Token: 0x04000056 RID: 86
			ConnectionError,
			// Token: 0x04000057 RID: 87
			ProtocolError,
			// Token: 0x04000058 RID: 88
			DataProcessingError
		}
	}
}
