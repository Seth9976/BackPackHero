using System;
using System.IO;
using Unity;

namespace System.Net
{
	/// <summary>Encapsulates a File Transfer Protocol (FTP) server's response to a request.</summary>
	// Token: 0x020003A4 RID: 932
	public class FtpWebResponse : WebResponse, IDisposable
	{
		// Token: 0x06001ECF RID: 7887 RVA: 0x00071D48 File Offset: 0x0006FF48
		internal FtpWebResponse(Stream responseStream, long contentLength, Uri responseUri, FtpStatusCode statusCode, string statusLine, DateTime lastModified, string bannerMessage, string welcomeMessage, string exitMessage)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, contentLength, statusLine);
			}
			this._responseStream = responseStream;
			if (responseStream == null && contentLength < 0L)
			{
				contentLength = 0L;
			}
			this._contentLength = contentLength;
			this._responseUri = responseUri;
			this._statusCode = statusCode;
			this._statusLine = statusLine;
			this._lastModified = lastModified;
			this._bannerMessage = bannerMessage;
			this._welcomeMessage = welcomeMessage;
			this._exitMessage = exitMessage;
		}

		// Token: 0x06001ED0 RID: 7888 RVA: 0x00071DC1 File Offset: 0x0006FFC1
		internal void UpdateStatus(FtpStatusCode statusCode, string statusLine, string exitMessage)
		{
			this._statusCode = statusCode;
			this._statusLine = statusLine;
			this._exitMessage = exitMessage;
		}

		/// <summary>Retrieves the stream that contains response data sent from an FTP server.</summary>
		/// <returns>A readable <see cref="T:System.IO.Stream" /> instance that contains data returned with the response; otherwise, <see cref="F:System.IO.Stream.Null" /> if no response data was returned by the server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The response did not return a data stream. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ED1 RID: 7889 RVA: 0x00071DD8 File Offset: 0x0006FFD8
		public override Stream GetResponseStream()
		{
			Stream stream;
			if (this._responseStream != null)
			{
				stream = this._responseStream;
			}
			else
			{
				stream = (this._responseStream = new FtpWebResponse.EmptyStream());
			}
			return stream;
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00071E08 File Offset: 0x00070008
		internal void SetResponseStream(Stream stream)
		{
			if (stream == null || stream == Stream.Null || stream is FtpWebResponse.EmptyStream)
			{
				return;
			}
			this._responseStream = stream;
		}

		/// <summary>Frees the resources held by the response.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001ED3 RID: 7891 RVA: 0x00071E25 File Offset: 0x00070025
		public override void Close()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "Close");
			}
			Stream responseStream = this._responseStream;
			if (responseStream != null)
			{
				responseStream.Close();
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, null, "Close");
			}
		}

		/// <summary>Gets the length of the data received from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that contains the number of bytes of data received from the FTP server. </returns>
		// Token: 0x17000628 RID: 1576
		// (get) Token: 0x06001ED4 RID: 7892 RVA: 0x00071E5E File Offset: 0x0007005E
		public override long ContentLength
		{
			get
			{
				return this._contentLength;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x06001ED5 RID: 7893 RVA: 0x00071E68 File Offset: 0x00070068
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this._ftpRequestHeaders == null)
				{
					lock (this)
					{
						if (this._ftpRequestHeaders == null)
						{
							this._ftpRequestHeaders = new WebHeaderCollection();
						}
					}
				}
				return this._ftpRequestHeaders;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Boolean" />.true if the <see cref="P:System.Net.FtpWebResponse.Headers" /> property is supported by the <see cref="T:System.Net.FtpWebResponse" /> instance; otherwise, false.</returns>
		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x06001ED6 RID: 7894 RVA: 0x0000390E File Offset: 0x00001B0E
		public override bool SupportsHeaders
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the URI that sent the response to the request.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies the resource associated with this response.</returns>
		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x06001ED7 RID: 7895 RVA: 0x00071EC0 File Offset: 0x000700C0
		public override Uri ResponseUri
		{
			get
			{
				return this._responseUri;
			}
		}

		/// <summary>Gets the most recent status code sent from the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.FtpStatusCode" /> value that indicates the most recent status code returned with this response.</returns>
		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x06001ED8 RID: 7896 RVA: 0x00071EC8 File Offset: 0x000700C8
		public FtpStatusCode StatusCode
		{
			get
			{
				return this._statusCode;
			}
		}

		/// <summary>Gets text that describes a status code sent from the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> instance that contains the status code and message returned with this response.</returns>
		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001ED9 RID: 7897 RVA: 0x00071ED0 File Offset: 0x000700D0
		public string StatusDescription
		{
			get
			{
				return this._statusLine;
			}
		}

		/// <summary>Gets the date and time that a file on an FTP server was last modified.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the last modified date and time for a file.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x06001EDA RID: 7898 RVA: 0x00071ED8 File Offset: 0x000700D8
		public DateTime LastModified
		{
			get
			{
				return this._lastModified;
			}
		}

		/// <summary>Gets the message sent by the FTP server when a connection is established prior to logon.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the banner message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x06001EDB RID: 7899 RVA: 0x00071EE0 File Offset: 0x000700E0
		public string BannerMessage
		{
			get
			{
				return this._bannerMessage;
			}
		}

		/// <summary>Gets the message sent by the FTP server when authentication is complete.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the welcome message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06001EDC RID: 7900 RVA: 0x00071EE8 File Offset: 0x000700E8
		public string WelcomeMessage
		{
			get
			{
				return this._welcomeMessage;
			}
		}

		/// <summary>Gets the message sent by the server when the FTP session is ending.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the exit message sent by the server; otherwise, <see cref="F:System.String.Empty" /> if no message is sent.</returns>
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x06001EDD RID: 7901 RVA: 0x00071EF0 File Offset: 0x000700F0
		public string ExitMessage
		{
			get
			{
				return this._exitMessage;
			}
		}

		// Token: 0x06001EDE RID: 7902 RVA: 0x00013B26 File Offset: 0x00011D26
		internal FtpWebResponse()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001069 RID: 4201
		internal Stream _responseStream;

		// Token: 0x0400106A RID: 4202
		private long _contentLength;

		// Token: 0x0400106B RID: 4203
		private Uri _responseUri;

		// Token: 0x0400106C RID: 4204
		private FtpStatusCode _statusCode;

		// Token: 0x0400106D RID: 4205
		private string _statusLine;

		// Token: 0x0400106E RID: 4206
		private WebHeaderCollection _ftpRequestHeaders;

		// Token: 0x0400106F RID: 4207
		private DateTime _lastModified;

		// Token: 0x04001070 RID: 4208
		private string _bannerMessage;

		// Token: 0x04001071 RID: 4209
		private string _welcomeMessage;

		// Token: 0x04001072 RID: 4210
		private string _exitMessage;

		// Token: 0x020003A5 RID: 933
		internal sealed class EmptyStream : MemoryStream
		{
			// Token: 0x06001EDF RID: 7903 RVA: 0x00071EF8 File Offset: 0x000700F8
			internal EmptyStream()
				: base(Array.Empty<byte>(), false)
			{
			}
		}
	}
}
