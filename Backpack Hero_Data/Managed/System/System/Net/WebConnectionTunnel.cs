using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004D5 RID: 1237
	internal class WebConnectionTunnel
	{
		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06002827 RID: 10279 RVA: 0x000950B6 File Offset: 0x000932B6
		public HttpWebRequest Request { get; }

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002828 RID: 10280 RVA: 0x000950BE File Offset: 0x000932BE
		public Uri ConnectUri { get; }

		// Token: 0x06002829 RID: 10281 RVA: 0x000950C6 File Offset: 0x000932C6
		public WebConnectionTunnel(HttpWebRequest request, Uri connectUri)
		{
			this.Request = request;
			this.ConnectUri = connectUri;
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x0600282A RID: 10282 RVA: 0x000950DC File Offset: 0x000932DC
		// (set) Token: 0x0600282B RID: 10283 RVA: 0x000950E4 File Offset: 0x000932E4
		public bool Success { get; private set; }

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x0600282C RID: 10284 RVA: 0x000950ED File Offset: 0x000932ED
		// (set) Token: 0x0600282D RID: 10285 RVA: 0x000950F5 File Offset: 0x000932F5
		public bool CloseConnection { get; private set; }

		// Token: 0x17000871 RID: 2161
		// (get) Token: 0x0600282E RID: 10286 RVA: 0x000950FE File Offset: 0x000932FE
		// (set) Token: 0x0600282F RID: 10287 RVA: 0x00095106 File Offset: 0x00093306
		public int StatusCode { get; private set; }

		// Token: 0x17000872 RID: 2162
		// (get) Token: 0x06002830 RID: 10288 RVA: 0x0009510F File Offset: 0x0009330F
		// (set) Token: 0x06002831 RID: 10289 RVA: 0x00095117 File Offset: 0x00093317
		public string StatusDescription { get; private set; }

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002832 RID: 10290 RVA: 0x00095120 File Offset: 0x00093320
		// (set) Token: 0x06002833 RID: 10291 RVA: 0x00095128 File Offset: 0x00093328
		public string[] Challenge { get; private set; }

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002834 RID: 10292 RVA: 0x00095131 File Offset: 0x00093331
		// (set) Token: 0x06002835 RID: 10293 RVA: 0x00095139 File Offset: 0x00093339
		public WebHeaderCollection Headers { get; private set; }

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06002836 RID: 10294 RVA: 0x00095142 File Offset: 0x00093342
		// (set) Token: 0x06002837 RID: 10295 RVA: 0x0009514A File Offset: 0x0009334A
		public Version ProxyVersion { get; private set; }

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x00095153 File Offset: 0x00093353
		// (set) Token: 0x06002839 RID: 10297 RVA: 0x0009515B File Offset: 0x0009335B
		public byte[] Data { get; private set; }

		// Token: 0x0600283A RID: 10298 RVA: 0x00095164 File Offset: 0x00093364
		internal async Task Initialize(Stream stream, CancellationToken cancellationToken)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("CONNECT ");
			stringBuilder.Append(this.Request.Address.Host);
			stringBuilder.Append(':');
			stringBuilder.Append(this.Request.Address.Port);
			stringBuilder.Append(" HTTP/");
			if (this.Request.ProtocolVersion == HttpVersion.Version11)
			{
				stringBuilder.Append("1.1");
			}
			else
			{
				stringBuilder.Append("1.0");
			}
			stringBuilder.Append("\r\nHost: ");
			stringBuilder.Append(this.Request.Address.Authority);
			bool flag = false;
			string[] challenge = this.Challenge;
			this.Challenge = null;
			string text = this.Request.Headers["Proxy-Authorization"];
			bool have_auth = text != null;
			if (have_auth)
			{
				stringBuilder.Append("\r\nProxy-Authorization: ");
				stringBuilder.Append(text);
				flag = text.ToUpper().Contains("NTLM");
			}
			else if (challenge != null && this.StatusCode == 407)
			{
				ICredentials credentials = this.Request.Proxy.Credentials;
				have_auth = true;
				if (this.connectRequest == null)
				{
					this.connectRequest = (HttpWebRequest)WebRequest.Create(string.Concat(new string[]
					{
						this.ConnectUri.Scheme,
						"://",
						this.ConnectUri.Host,
						":",
						this.ConnectUri.Port.ToString(),
						"/"
					}));
					this.connectRequest.Method = "CONNECT";
					this.connectRequest.Credentials = credentials;
				}
				if (credentials != null)
				{
					for (int i = 0; i < challenge.Length; i++)
					{
						Authorization authorization = AuthenticationManager.Authenticate(challenge[i], this.connectRequest, credentials);
						if (authorization != null)
						{
							flag = authorization.ModuleAuthenticationType == "NTLM";
							stringBuilder.Append("\r\nProxy-Authorization: ");
							stringBuilder.Append(authorization.Message);
							break;
						}
					}
				}
			}
			if (flag)
			{
				stringBuilder.Append("\r\nProxy-Connection: keep-alive");
				this.ntlmAuthState++;
			}
			stringBuilder.Append("\r\n\r\n");
			this.StatusCode = 0;
			byte[] bytes = Encoding.Default.GetBytes(stringBuilder.ToString());
			await stream.WriteAsync(bytes, 0, bytes.Length, cancellationToken).ConfigureAwait(false);
			ValueTuple<WebHeaderCollection, byte[], int> valueTuple = await this.ReadHeaders(stream, cancellationToken).ConfigureAwait(false);
			this.Headers = valueTuple.Item1;
			this.Data = valueTuple.Item2;
			this.StatusCode = valueTuple.Item3;
			if ((!have_auth || this.ntlmAuthState == WebConnectionTunnel.NtlmAuthState.Challenge) && this.Headers != null && this.StatusCode == 407)
			{
				string text2 = this.Headers["Connection"];
				if (!string.IsNullOrEmpty(text2) && text2.ToLower() == "close")
				{
					this.CloseConnection = true;
				}
				this.Challenge = this.Headers.GetValues("Proxy-Authenticate");
				this.Success = false;
			}
			else
			{
				this.Success = this.StatusCode == 200 && this.Headers != null;
			}
			if (this.Challenge == null && (this.StatusCode == 401 || this.StatusCode == 407))
			{
				HttpWebResponse httpWebResponse = new HttpWebResponse(this.ConnectUri, "CONNECT", (HttpStatusCode)this.StatusCode, this.Headers);
				throw new WebException((this.StatusCode == 407) ? "(407) Proxy Authentication Required" : "(401) Unauthorized", null, WebExceptionStatus.ProtocolError, httpWebResponse);
			}
		}

		// Token: 0x0600283B RID: 10299 RVA: 0x000951B8 File Offset: 0x000933B8
		private async Task<ValueTuple<WebHeaderCollection, byte[], int>> ReadHeaders(Stream stream, CancellationToken cancellationToken)
		{
			byte[] retBuffer = null;
			int status = 200;
			byte[] buffer = new byte[1024];
			MemoryStream ms = new MemoryStream();
			int num2;
			WebHeaderCollection webHeaderCollection;
			for (;;)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int num = await stream.ReadAsync(buffer, 0, 1024, cancellationToken).ConfigureAwait(false);
				if (num == 0)
				{
					break;
				}
				ms.Write(buffer, 0, num);
				num2 = 0;
				string text = null;
				bool flag = false;
				webHeaderCollection = new WebHeaderCollection();
				while (WebConnection.ReadLine(ms.GetBuffer(), ref num2, (int)ms.Length, ref text))
				{
					if (text == null)
					{
						goto Block_2;
					}
					if (flag)
					{
						webHeaderCollection.Add(text);
					}
					else
					{
						string[] array = text.Split(' ', StringSplitOptions.None);
						if (array.Length < 2)
						{
							goto Block_6;
						}
						if (string.Compare(array[0], "HTTP/1.1", true) == 0)
						{
							this.ProxyVersion = HttpVersion.Version11;
						}
						else
						{
							if (string.Compare(array[0], "HTTP/1.0", true) != 0)
							{
								goto IL_022A;
							}
							this.ProxyVersion = HttpVersion.Version10;
						}
						status = (int)uint.Parse(array[1]);
						if (array.Length >= 3)
						{
							this.StatusDescription = string.Join(" ", array, 2, array.Length - 2);
						}
						flag = true;
					}
				}
			}
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			Block_2:
			string text2 = webHeaderCollection["Content-Length"];
			int num3;
			if (string.IsNullOrEmpty(text2) || !int.TryParse(text2, out num3))
			{
				num3 = 0;
			}
			if (ms.Length - (long)num2 - (long)num3 > 0L)
			{
				retBuffer = new byte[ms.Length - (long)num2 - (long)num3];
				Buffer.BlockCopy(ms.GetBuffer(), num2 + num3, retBuffer, 0, retBuffer.Length);
			}
			else
			{
				this.FlushContents(stream, num3 - (int)(ms.Length - (long)num2));
			}
			return new ValueTuple<WebHeaderCollection, byte[], int>(webHeaderCollection, retBuffer, status);
			Block_6:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
			IL_022A:
			throw WebConnection.GetException(WebExceptionStatus.ServerProtocolViolation, null);
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x0009520C File Offset: 0x0009340C
		private void FlushContents(Stream stream, int contentLength)
		{
			while (contentLength > 0)
			{
				byte[] array = new byte[contentLength];
				int num = stream.Read(array, 0, contentLength);
				if (num <= 0)
				{
					break;
				}
				contentLength -= num;
			}
		}

		// Token: 0x04001754 RID: 5972
		private HttpWebRequest connectRequest;

		// Token: 0x04001755 RID: 5973
		private WebConnectionTunnel.NtlmAuthState ntlmAuthState;

		// Token: 0x020004D6 RID: 1238
		private enum NtlmAuthState
		{
			// Token: 0x0400175F RID: 5983
			None,
			// Token: 0x04001760 RID: 5984
			Challenge,
			// Token: 0x04001761 RID: 5985
			Response
		}
	}
}
