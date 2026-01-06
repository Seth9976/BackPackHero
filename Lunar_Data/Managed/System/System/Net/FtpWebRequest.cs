using System;
using System.IO;
using System.Net.Cache;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Unity;

namespace System.Net
{
	/// <summary>Implements a File Transfer Protocol (FTP) client.</summary>
	// Token: 0x0200039F RID: 927
	public sealed class FtpWebRequest : WebRequest
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001E80 RID: 7808 RVA: 0x000700B8 File Offset: 0x0006E2B8
		internal FtpMethodInfo MethodInfo
		{
			get
			{
				return this._methodInfo;
			}
		}

		/// <summary>Defines the default cache policy for all FTP requests.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> that defines the cache policy for FTP requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The caller tried to set this property to null.</exception>
		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001E81 RID: 7809 RVA: 0x000700C0 File Offset: 0x0006E2C0
		// (set) Token: 0x06001E82 RID: 7810 RVA: 0x00003917 File Offset: 0x00001B17
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return WebRequest.DefaultCachePolicy;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the command to send to the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the FTP command to send to the server. The default value is <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001E83 RID: 7811 RVA: 0x000700C7 File Offset: 0x0006E2C7
		// (set) Token: 0x06001E84 RID: 7812 RVA: 0x000700D4 File Offset: 0x0006E2D4
		public override string Method
		{
			get
			{
				return this._methodInfo.Method;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("FTP Method names cannot be null or empty.", "value");
				}
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				try
				{
					this._methodInfo = FtpMethodInfo.GetMethodInfo(value);
				}
				catch (ArgumentException)
				{
					throw new ArgumentException("This method is not supported.", "value");
				}
			}
		}

		/// <summary>Gets or sets the new name of a file being renamed.</summary>
		/// <returns>The new name of the file being renamed.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is null or an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001E85 RID: 7813 RVA: 0x0007013C File Offset: 0x0006E33C
		// (set) Token: 0x06001E86 RID: 7814 RVA: 0x00070144 File Offset: 0x0006E344
		public string RenameTo
		{
			get
			{
				return this._renameTo;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("The RenameTo filename cannot be null or empty.", "value");
				}
				this._renameTo = value;
			}
		}

		/// <summary>Gets or sets the credentials used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance; otherwise, null if the property has not been set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Net.ICredentials" /> of a type other than <see cref="T:System.Net.NetworkCredential" /> was specified for a set operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001E87 RID: 7815 RVA: 0x00070178 File Offset: 0x0006E378
		// (set) Token: 0x06001E88 RID: 7816 RVA: 0x00070180 File Offset: 0x0006E380
		public override ICredentials Credentials
		{
			get
			{
				return this._authInfo;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == CredentialCache.DefaultNetworkCredentials)
				{
					throw new ArgumentException("Default credentials are not supported on an FTP request.", "value");
				}
				this._authInfo = value;
			}
		}

		/// <summary>Gets the URI requested by this instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies a resource that is accessed using the File Transfer Protocol.</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001E89 RID: 7817 RVA: 0x000701CD File Offset: 0x0006E3CD
		public override Uri RequestUri
		{
			get
			{
				return this._uri;
			}
		}

		/// <summary>Gets or sets the number of milliseconds to wait for a request.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of milliseconds to wait before a request times out. The default value is <see cref="F:System.Threading.Timeout.Infinite" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001E8A RID: 7818 RVA: 0x000701D5 File Offset: 0x0006E3D5
		// (set) Token: 0x06001E8B RID: 7819 RVA: 0x000701E0 File Offset: 0x0006E3E0
		public override int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.");
				}
				if (this._timeout != value)
				{
					this._timeout = value;
					this._timerQueue = null;
				}
			}
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001E8C RID: 7820 RVA: 0x0007022F File Offset: 0x0006E42F
		internal int RemainingTimeout
		{
			get
			{
				return this._remainingTimeout;
			}
		}

		/// <summary>Gets or sets a time-out when reading from or writing to a stream.</summary>
		/// <returns>The number of milliseconds before the reading or writing times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001E8D RID: 7821 RVA: 0x00070237 File Offset: 0x0006E437
		// (set) Token: 0x06001E8E RID: 7822 RVA: 0x0007023F File Offset: 0x0006E43F
		public int ReadWriteTimeout
		{
			get
			{
				return this._readWriteTimeout;
			}
			set
			{
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this._readWriteTimeout = value;
			}
		}

		/// <summary>Gets or sets a byte offset into the file being downloaded by this request.</summary>
		/// <returns>An <see cref="T:System.Int64" /> instance that specifies the file offset, in bytes. The default value is zero.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than zero. </exception>
		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001E8F RID: 7823 RVA: 0x00070273 File Offset: 0x0006E473
		// (set) Token: 0x06001E90 RID: 7824 RVA: 0x0007027B File Offset: 0x0006E47B
		public long ContentOffset
		{
			get
			{
				return this._contentOffset;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._contentOffset = value;
			}
		}

		/// <summary>Gets or sets a value that is ignored by the <see cref="T:System.Net.FtpWebRequest" /> class.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that should be ignored.</returns>
		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001E91 RID: 7825 RVA: 0x000702A7 File Offset: 0x0006E4A7
		// (set) Token: 0x06001E92 RID: 7826 RVA: 0x000702AF File Offset: 0x0006E4AF
		public override long ContentLength
		{
			get
			{
				return this._contentLength;
			}
			set
			{
				this._contentLength = value;
			}
		}

		/// <summary>Gets or sets the proxy used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance responsible for communicating with the FTP server.</returns>
		/// <exception cref="T:System.ArgumentNullException">This property cannot be set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x00002F6A File Offset: 0x0000116A
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000702B8 File Offset: 0x0006E4B8
		public override IWebProxy Proxy
		{
			get
			{
				return null;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		/// <summary>Gets or sets the name of the connection group that contains the service point used to send the current request.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains a connection group name.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000702CD File Offset: 0x0006E4CD
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x000702D5 File Offset: 0x0006E4D5
		public override string ConnectionGroupName
		{
			get
			{
				return this._connectionGroupName;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._connectionGroupName = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.ServicePoint" /> object used to connect to the FTP server.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> object that can be used to customize connection behavior.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000702F1 File Offset: 0x0006E4F1
		public ServicePoint ServicePoint
		{
			get
			{
				if (this._servicePoint == null)
				{
					this._servicePoint = ServicePointManager.FindServicePoint(this._uri);
				}
				return this._servicePoint;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001E98 RID: 7832 RVA: 0x00070312 File Offset: 0x0006E512
		internal bool Aborted
		{
			get
			{
				return this._aborted;
			}
		}

		// Token: 0x06001E99 RID: 7833 RVA: 0x0007031C File Offset: 0x0006E51C
		internal FtpWebRequest(Uri uri)
		{
			this._timeout = 100000;
			this._passive = true;
			this._binary = true;
			this._timerQueue = FtpWebRequest.s_DefaultTimerQueue;
			this._readWriteTimeout = 300000;
			base..ctor();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, uri, ".ctor");
			}
			if (uri.Scheme != Uri.UriSchemeFtp)
			{
				throw new ArgumentOutOfRangeException("uri");
			}
			this._timerCallback = new TimerThread.Callback(this.TimerCallback);
			this._syncObject = new object();
			NetworkCredential networkCredential = null;
			this._uri = uri;
			this._methodInfo = FtpMethodInfo.GetMethodInfo("RETR");
			if (this._uri.UserInfo != null && this._uri.UserInfo.Length != 0)
			{
				string userInfo = this._uri.UserInfo;
				string text = userInfo;
				string text2 = "";
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					text = Uri.UnescapeDataString(userInfo.Substring(0, num));
					num++;
					text2 = Uri.UnescapeDataString(userInfo.Substring(num, userInfo.Length - num));
				}
				networkCredential = new NetworkCredential(text, text2);
			}
			if (networkCredential == null)
			{
				networkCredential = FtpWebRequest.s_defaultFtpNetworkCredential;
			}
			this._authInfo = networkCredential;
		}

		/// <summary>Returns the FTP server response.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance.- or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="P:System.Net.FtpWebRequest.EnableSsl" /> is set to true, but the server does not support this feature.- or -A <see cref="P:System.Net.FtpWebRequest.Timeout" /> was specified and the timeout has expired.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9A RID: 7834 RVA: 0x00070448 File Offset: 0x0006E648
		public override WebResponse GetResponse()
		{
			if (NetEventSource.IsEnabled)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Enter(this, null, "GetResponse");
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "GetResponse");
				}
			}
			try
			{
				this.CheckError();
				if (this._ftpWebResponse != null)
				{
					return this._ftpWebResponse;
				}
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getResponseStarted = true;
				this._startTime = DateTime.UtcNow;
				this._remainingTimeout = this.Timeout;
				if (this.Timeout != -1)
				{
					this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
					if (this._remainingTimeout <= 0)
					{
						throw ExceptionHelper.TimeoutException;
					}
				}
				FtpWebRequest.RequestStage requestStage = this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				if (requestStage >= FtpWebRequest.RequestStage.RequestStarted)
				{
					if (requestStage < FtpWebRequest.RequestStage.ReadReady)
					{
						object syncObject = this._syncObject;
						lock (syncObject)
						{
							if (this._requestStage < FtpWebRequest.RequestStage.ReadReady)
							{
								this._readAsyncResult = new LazyAsyncResult(null, null, null);
							}
						}
						if (this._readAsyncResult != null)
						{
							this._readAsyncResult.InternalWaitForCompletion();
						}
						this.CheckError();
					}
				}
				else
				{
					this.SubmitRequest(false);
					if (this._methodInfo.IsUpload)
					{
						this.FinishRequestStage(FtpWebRequest.RequestStage.WriteReady);
					}
					else
					{
						this.FinishRequestStage(FtpWebRequest.RequestStage.ReadReady);
					}
					this.CheckError();
					this.EnsureFtpWebResponse(null);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "GetResponse");
				}
				if (this._exception == null)
				{
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Error(this, ex, "GetResponse");
					}
					this.SetException(ex);
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, this._ftpWebResponse, "GetResponse");
				}
			}
			return this._ftpWebResponse;
		}

		/// <summary>Begins sending a request and receiving a response from an FTP server asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9B RID: 7835 RVA: 0x00070674 File Offset: 0x0006E874
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "BeginGetResponse");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "BeginGetResponse");
			}
			ContextAwareResult contextAwareResult;
			try
			{
				if (this._ftpWebResponse != null)
				{
					contextAwareResult = new ContextAwareResult(this, state, callback);
					contextAwareResult.InvokeCallback(this._ftpWebResponse);
					return contextAwareResult;
				}
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getResponseStarted = true;
				this.CheckError();
				FtpWebRequest.RequestStage requestStage = this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				contextAwareResult = new ContextAwareResult(true, true, this, state, callback);
				this._readAsyncResult = contextAwareResult;
				if (requestStage >= FtpWebRequest.RequestStage.RequestStarted)
				{
					contextAwareResult.StartPostingAsyncOp();
					contextAwareResult.FinishPostingAsyncOp();
					if (requestStage >= FtpWebRequest.RequestStage.ReadReady)
					{
						contextAwareResult = null;
					}
					else
					{
						object obj = this._syncObject;
						lock (obj)
						{
							if (this._requestStage >= FtpWebRequest.RequestStage.ReadReady)
							{
								contextAwareResult = null;
							}
						}
					}
					if (contextAwareResult == null)
					{
						contextAwareResult = (ContextAwareResult)this._readAsyncResult;
						if (!contextAwareResult.InternalPeekCompleted)
						{
							contextAwareResult.InvokeCallback();
						}
					}
				}
				else
				{
					object obj = contextAwareResult.StartPostingAsyncOp();
					lock (obj)
					{
						this.SubmitRequest(true);
						contextAwareResult.FinishPostingAsyncOp();
					}
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "BeginGetResponse");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "BeginGetResponse");
				}
			}
			return contextAwareResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.Net.WebException">An error occurred using an HTTP proxy. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9C RID: 7836 RVA: 0x0007084C File Offset: 0x0006EA4C
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "EndGetResponse");
			}
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (lazyAsyncResult == null)
				{
					throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
				}
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndGetResponse"));
				}
				lazyAsyncResult.InternalWaitForCompletion();
				lazyAsyncResult.EndCalled = true;
				this.CheckError();
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "EndGetResponse");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "EndGetResponse");
				}
			}
			return this._ftpWebResponse;
		}

		/// <summary>Retrieves the stream used to upload data to an FTP server.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance used to store data to be sent to the server by the current request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> has been called and has not completed. - or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" /> or <see cref="F:System.Net.WebRequestMethods.Ftp.AppendFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9D RID: 7837 RVA: 0x00070914 File Offset: 0x0006EB14
		public override Stream GetRequestStream()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "GetRequestStream");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "GetRequestStream");
			}
			try
			{
				if (this._getRequestStreamStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getRequestStreamStarted = true;
				if (!this._methodInfo.IsUpload)
				{
					throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
				}
				this.CheckError();
				this._startTime = DateTime.UtcNow;
				this._remainingTimeout = this.Timeout;
				if (this.Timeout != -1)
				{
					this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
					if (this._remainingTimeout <= 0)
					{
						throw ExceptionHelper.TimeoutException;
					}
				}
				this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				this.SubmitRequest(false);
				this.FinishRequestStage(FtpWebRequest.RequestStage.WriteReady);
				this.CheckError();
				if (this._stream.CanTimeout)
				{
					this._stream.WriteTimeout = this.ReadWriteTimeout;
					this._stream.ReadTimeout = this.ReadWriteTimeout;
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "GetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "GetRequestStream");
				}
			}
			return this._stream;
		}

		/// <summary>Begins asynchronously opening a request's content stream for writing.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">A previous call to this method or <see cref="M:System.Net.FtpWebRequest.GetRequestStream" /> has not yet completed. </exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9E RID: 7838 RVA: 0x00070A88 File Offset: 0x0006EC88
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "BeginGetRequestStream");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "BeginGetRequestStream");
			}
			ContextAwareResult contextAwareResult = null;
			try
			{
				if (this._getRequestStreamStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getRequestStreamStarted = true;
				if (!this._methodInfo.IsUpload)
				{
					throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
				}
				this.CheckError();
				this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				contextAwareResult = new ContextAwareResult(true, true, this, state, callback);
				object obj = contextAwareResult.StartPostingAsyncOp();
				lock (obj)
				{
					this._writeAsyncResult = contextAwareResult;
					this.SubmitRequest(true);
					contextAwareResult.FinishPostingAsyncOp();
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "BeginGetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "BeginGetRequestStream");
				}
			}
			return contextAwareResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance associated with this instance.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001E9F RID: 7839 RVA: 0x00070BB0 File Offset: 0x0006EDB0
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "EndGetRequestStream");
			}
			Stream stream = null;
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (lazyAsyncResult == null)
				{
					throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
				}
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndGetResponse"));
				}
				lazyAsyncResult.InternalWaitForCompletion();
				lazyAsyncResult.EndCalled = true;
				this.CheckError();
				stream = this._stream;
				lazyAsyncResult.EndCalled = true;
				if (stream.CanTimeout)
				{
					stream.WriteTimeout = this.ReadWriteTimeout;
					stream.ReadTimeout = this.ReadWriteTimeout;
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "EndGetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "EndGetRequestStream");
				}
			}
			return stream;
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x00070CA0 File Offset: 0x0006EEA0
		private void SubmitRequest(bool isAsync)
		{
			try
			{
				this._async = isAsync;
				for (;;)
				{
					FtpControlStream ftpControlStream = this._connection;
					if (ftpControlStream == null)
					{
						if (isAsync)
						{
							break;
						}
						ftpControlStream = this.CreateConnection();
						this._connection = ftpControlStream;
					}
					if (!isAsync && this.Timeout != -1)
					{
						this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
						if (this._remainingTimeout <= 0)
						{
							goto Block_6;
						}
					}
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, "Request being submitted", "SubmitRequest");
					}
					ftpControlStream.SetSocketTimeoutOption(this.RemainingTimeout);
					try
					{
						this.TimedSubmitRequestHelper(isAsync);
					}
					catch (Exception ex)
					{
						if (this.AttemptedRecovery(ex))
						{
							if (!isAsync && this.Timeout != -1)
							{
								this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
								if (this._remainingTimeout <= 0)
								{
									throw;
								}
							}
							continue;
						}
						throw;
					}
					goto IL_00E9;
				}
				this.CreateConnectionAsync();
				return;
				Block_6:
				throw ExceptionHelper.TimeoutException;
				IL_00E9:;
			}
			catch (WebException ex2)
			{
				IOException ex3 = ex2.InnerException as IOException;
				if (ex3 != null)
				{
					SocketException ex4 = ex3.InnerException as SocketException;
					if (ex4 != null && ex4.SocketErrorCode == SocketError.TimedOut)
					{
						this.SetException(new WebException("The operation has timed out.", WebExceptionStatus.Timeout));
					}
				}
				this.SetException(ex2);
			}
			catch (Exception ex5)
			{
				this.SetException(ex5);
			}
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x00070E1C File Offset: 0x0006F01C
		private Exception TranslateConnectException(Exception e)
		{
			SocketException ex = e as SocketException;
			if (ex == null)
			{
				return e;
			}
			if (ex.SocketErrorCode == SocketError.HostNotFound)
			{
				return new WebException("The remote name could not be resolved", WebExceptionStatus.NameResolutionFailure);
			}
			return new WebException("Unable to connect to the remote server", WebExceptionStatus.ConnectFailure);
		}

		// Token: 0x06001EA2 RID: 7842 RVA: 0x00070E5C File Offset: 0x0006F05C
		private async void CreateConnectionAsync()
		{
			string host = this._uri.Host;
			int port = this._uri.Port;
			TcpClient client = new TcpClient();
			object obj;
			try
			{
				await client.ConnectAsync(host, port).ConfigureAwait(false);
				obj = new FtpControlStream(client);
			}
			catch (Exception ex)
			{
				obj = this.TranslateConnectException(ex);
			}
			this.AsyncRequestCallback(obj);
		}

		// Token: 0x06001EA3 RID: 7843 RVA: 0x00070E94 File Offset: 0x0006F094
		private FtpControlStream CreateConnection()
		{
			string host = this._uri.Host;
			int port = this._uri.Port;
			TcpClient tcpClient = new TcpClient();
			try
			{
				tcpClient.Connect(host, port);
			}
			catch (Exception ex)
			{
				throw this.TranslateConnectException(ex);
			}
			return new FtpControlStream(tcpClient);
		}

		// Token: 0x06001EA4 RID: 7844 RVA: 0x00070EE8 File Offset: 0x0006F0E8
		private Stream TimedSubmitRequestHelper(bool isAsync)
		{
			if (isAsync)
			{
				if (this._requestCompleteAsyncResult == null)
				{
					this._requestCompleteAsyncResult = new LazyAsyncResult(null, null, null);
				}
				return this._connection.SubmitRequest(this, true, true);
			}
			Stream stream = null;
			bool flag = false;
			TimerThread.Timer timer = this.TimerQueue.CreateTimer(this._timerCallback, null);
			try
			{
				stream = this._connection.SubmitRequest(this, false, true);
			}
			catch (Exception ex)
			{
				if ((!(ex is SocketException) && !(ex is ObjectDisposedException)) || !timer.HasExpired)
				{
					timer.Cancel();
					throw;
				}
				flag = true;
			}
			if (flag || !timer.Cancel())
			{
				this._timedOut = true;
				throw ExceptionHelper.TimeoutException;
			}
			if (stream != null)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._aborted)
					{
						((ICloseEx)stream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						this.CheckError();
						throw new InternalException();
					}
					this._stream = stream;
				}
			}
			return stream;
		}

		// Token: 0x06001EA5 RID: 7845 RVA: 0x00070FEC File Offset: 0x0006F1EC
		private void TimerCallback(TimerThread.Timer timer, int timeNoticed, object context)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "TimerCallback");
			}
			FtpControlStream connection = this._connection;
			if (connection != null)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "aborting connection", "TimerCallback");
				}
				connection.AbortConnect();
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001EA6 RID: 7846 RVA: 0x00071033 File Offset: 0x0006F233
		private TimerThread.Queue TimerQueue
		{
			get
			{
				if (this._timerQueue == null)
				{
					this._timerQueue = TimerThread.GetOrCreateQueue(this.RemainingTimeout);
				}
				return this._timerQueue;
			}
		}

		// Token: 0x06001EA7 RID: 7847 RVA: 0x00071054 File Offset: 0x0006F254
		private bool AttemptedRecovery(Exception e)
		{
			if (e is OutOfMemoryException || this._onceFailed || this._aborted || this._timedOut || this._connection == null || !this._connection.RecoverableFailure)
			{
				return false;
			}
			this._onceFailed = true;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				if (this._connection == null)
				{
					return false;
				}
				this._connection.CloseSocket();
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connection: {0}", new object[] { this._connection }), "AttemptedRecovery");
				}
				this._connection = null;
			}
			return true;
		}

		// Token: 0x06001EA8 RID: 7848 RVA: 0x00071118 File Offset: 0x0006F318
		private void SetException(Exception exception)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "SetException");
			}
			if (exception is OutOfMemoryException)
			{
				this._exception = exception;
				throw exception;
			}
			FtpControlStream connection = this._connection;
			if (this._exception == null)
			{
				if (exception is WebException)
				{
					this.EnsureFtpWebResponse(exception);
					this._exception = new WebException(exception.Message, null, ((WebException)exception).Status, this._ftpWebResponse);
				}
				else if (exception is AuthenticationException || exception is SecurityException)
				{
					this._exception = exception;
				}
				else if (connection != null && connection.StatusCode != FtpStatusCode.Undefined)
				{
					this.EnsureFtpWebResponse(exception);
					this._exception = new WebException(SR.Format("The remote server returned an error: {0}.", connection.StatusLine), exception, WebExceptionStatus.ProtocolError, this._ftpWebResponse);
				}
				else
				{
					this._exception = new WebException(exception.Message, exception);
				}
				if (connection != null && this._ftpWebResponse != null)
				{
					this._ftpWebResponse.UpdateStatus(connection.StatusCode, connection.StatusLine, connection.ExitMessage);
				}
			}
		}

		// Token: 0x06001EA9 RID: 7849 RVA: 0x00071219 File Offset: 0x0006F419
		private void CheckError()
		{
			if (this._exception != null)
			{
				ExceptionDispatchInfo.Throw(this._exception);
			}
		}

		// Token: 0x06001EAA RID: 7850 RVA: 0x0007122E File Offset: 0x0006F42E
		internal void RequestCallback(object obj)
		{
			if (this._async)
			{
				this.AsyncRequestCallback(obj);
				return;
			}
			this.SyncRequestCallback(obj);
		}

		// Token: 0x06001EAB RID: 7851 RVA: 0x00071248 File Offset: 0x0006F448
		private void SyncRequestCallback(object obj)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, obj, "SyncRequestCallback");
			}
			FtpWebRequest.RequestStage requestStage = FtpWebRequest.RequestStage.CheckForError;
			try
			{
				bool flag = obj == null;
				Exception ex = obj as Exception;
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("exp:{0} completedRequest:{1}", new object[] { ex, flag }), "SyncRequestCallback");
				}
				if (ex != null)
				{
					this.SetException(ex);
				}
				else
				{
					if (!flag)
					{
						throw new InternalException();
					}
					FtpControlStream connection = this._connection;
					if (connection != null)
					{
						this.EnsureFtpWebResponse(null);
						this._ftpWebResponse.UpdateStatus(connection.StatusCode, connection.StatusLine, connection.ExitMessage);
					}
					requestStage = FtpWebRequest.RequestStage.ReleaseConnection;
				}
			}
			catch (Exception ex2)
			{
				this.SetException(ex2);
			}
			finally
			{
				this.FinishRequestStage(requestStage);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "SyncRequestCallback");
				}
				this.CheckError();
			}
		}

		// Token: 0x06001EAC RID: 7852 RVA: 0x00071338 File Offset: 0x0006F538
		private void AsyncRequestCallback(object obj)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, obj, "AsyncRequestCallback");
			}
			FtpWebRequest.RequestStage requestStage = FtpWebRequest.RequestStage.CheckForError;
			try
			{
				FtpControlStream ftpControlStream = obj as FtpControlStream;
				FtpDataStream ftpDataStream = obj as FtpDataStream;
				Exception ex = obj as Exception;
				bool flag = obj == null;
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("stream:{0} conn:{1} exp:{2} completedRequest:{3}", new object[] { ftpDataStream, ftpControlStream, ex, flag }), "AsyncRequestCallback");
				}
				for (;;)
				{
					if (ex != null)
					{
						if (this.AttemptedRecovery(ex))
						{
							ftpControlStream = this.CreateConnection();
							if (ftpControlStream == null)
							{
								break;
							}
							ex = null;
						}
						if (ex != null)
						{
							goto Block_9;
						}
					}
					if (ftpControlStream != null)
					{
						object obj2 = this._syncObject;
						lock (obj2)
						{
							if (this._aborted)
							{
								if (NetEventSource.IsEnabled)
								{
									NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connect:{0}", new object[] { ftpControlStream }), "AsyncRequestCallback");
								}
								ftpControlStream.CloseSocket();
								break;
							}
							this._connection = ftpControlStream;
							if (NetEventSource.IsEnabled)
							{
								NetEventSource.Associate(this, this._connection, "AsyncRequestCallback");
							}
						}
						try
						{
							ftpDataStream = (FtpDataStream)this.TimedSubmitRequestHelper(true);
						}
						catch (Exception ex)
						{
							continue;
						}
						break;
					}
					goto IL_012F;
				}
				return;
				Block_9:
				this.SetException(ex);
				return;
				IL_012F:
				if (ftpDataStream != null)
				{
					object obj2 = this._syncObject;
					lock (obj2)
					{
						if (this._aborted)
						{
							((ICloseEx)ftpDataStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
							goto IL_01CA;
						}
						this._stream = ftpDataStream;
					}
					ftpDataStream.SetSocketTimeoutOption(this.Timeout);
					this.EnsureFtpWebResponse(null);
					requestStage = (ftpDataStream.CanRead ? FtpWebRequest.RequestStage.ReadReady : FtpWebRequest.RequestStage.WriteReady);
				}
				else
				{
					if (!flag)
					{
						throw new InternalException();
					}
					ftpControlStream = this._connection;
					if (ftpControlStream != null)
					{
						this.EnsureFtpWebResponse(null);
						this._ftpWebResponse.UpdateStatus(ftpControlStream.StatusCode, ftpControlStream.StatusLine, ftpControlStream.ExitMessage);
					}
					requestStage = FtpWebRequest.RequestStage.ReleaseConnection;
				}
				IL_01CA:;
			}
			catch (Exception ex2)
			{
				this.SetException(ex2);
			}
			finally
			{
				this.FinishRequestStage(requestStage);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "AsyncRequestCallback");
				}
			}
		}

		// Token: 0x06001EAD RID: 7853 RVA: 0x000715B8 File Offset: 0x0006F7B8
		private FtpWebRequest.RequestStage FinishRequestStage(FtpWebRequest.RequestStage stage)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state:{0}", new object[] { stage }), "FinishRequestStage");
			}
			if (this._exception != null)
			{
				stage = FtpWebRequest.RequestStage.ReleaseConnection;
			}
			object syncObject = this._syncObject;
			FtpWebRequest.RequestStage requestStage;
			LazyAsyncResult writeAsyncResult;
			LazyAsyncResult readAsyncResult;
			FtpControlStream connection;
			lock (syncObject)
			{
				requestStage = this._requestStage;
				if (stage == FtpWebRequest.RequestStage.CheckForError)
				{
					return requestStage;
				}
				if (requestStage == FtpWebRequest.RequestStage.ReleaseConnection && stage == FtpWebRequest.RequestStage.ReleaseConnection)
				{
					return FtpWebRequest.RequestStage.ReleaseConnection;
				}
				if (stage > requestStage)
				{
					this._requestStage = stage;
				}
				if (stage <= FtpWebRequest.RequestStage.RequestStarted)
				{
					return requestStage;
				}
				writeAsyncResult = this._writeAsyncResult;
				readAsyncResult = this._readAsyncResult;
				connection = this._connection;
				if (stage == FtpWebRequest.RequestStage.ReleaseConnection)
				{
					if (this._exception == null && !this._aborted && requestStage != FtpWebRequest.RequestStage.ReadReady && this._methodInfo.IsDownload && !this._ftpWebResponse.IsFromCache)
					{
						return requestStage;
					}
					this._connection = null;
				}
			}
			FtpWebRequest.RequestStage requestStage2;
			try
			{
				if ((stage == FtpWebRequest.RequestStage.ReleaseConnection || requestStage == FtpWebRequest.RequestStage.ReleaseConnection) && connection != null)
				{
					try
					{
						if (this._exception != null)
						{
							connection.Abort(this._exception);
						}
					}
					finally
					{
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connection: {0}", new object[] { connection }), "FinishRequestStage");
						}
						connection.CloseSocket();
						if (this._async && this._requestCompleteAsyncResult != null)
						{
							this._requestCompleteAsyncResult.InvokeCallback();
						}
					}
				}
				requestStage2 = requestStage;
			}
			finally
			{
				try
				{
					if (stage >= FtpWebRequest.RequestStage.WriteReady)
					{
						if (this._methodInfo.IsUpload && !this._getRequestStreamStarted)
						{
							if (this._stream != null)
							{
								this._stream.Close();
							}
						}
						else if (writeAsyncResult != null && !writeAsyncResult.InternalPeekCompleted)
						{
							writeAsyncResult.InvokeCallback();
						}
					}
				}
				finally
				{
					if (stage >= FtpWebRequest.RequestStage.ReadReady && readAsyncResult != null && !readAsyncResult.InternalPeekCompleted)
					{
						readAsyncResult.InvokeCallback();
					}
				}
			}
			return requestStage2;
		}

		/// <summary>Terminates an asynchronous FTP operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001EAE RID: 7854 RVA: 0x000717AC File Offset: 0x0006F9AC
		public override void Abort()
		{
			if (this._aborted)
			{
				return;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "Abort");
			}
			try
			{
				object syncObject = this._syncObject;
				Stream stream;
				FtpControlStream connection;
				lock (syncObject)
				{
					if (this._requestStage >= FtpWebRequest.RequestStage.ReleaseConnection)
					{
						return;
					}
					this._aborted = true;
					stream = this._stream;
					connection = this._connection;
					this._exception = ExceptionHelper.RequestAbortedException;
				}
				if (stream != null)
				{
					if (!(stream is ICloseEx))
					{
						NetEventSource.Fail(this, "The _stream member is not CloseEx hence the risk of connection been orphaned.", "Abort");
					}
					((ICloseEx)stream).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
				if (connection != null)
				{
					connection.Abort(ExceptionHelper.RequestAbortedException);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "Abort");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "Abort");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the control connection to the FTP server is closed after the request completes.</summary>
		/// <returns>true if the connection to the server should not be destroyed; otherwise, false. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001EAF RID: 7855 RVA: 0x0000390E File Offset: 0x00001B0E
		// (set) Token: 0x06001EB0 RID: 7856 RVA: 0x000702B8 File Offset: 0x0006E4B8
		public bool KeepAlive
		{
			get
			{
				return true;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06001EB1 RID: 7857 RVA: 0x000718AC File Offset: 0x0006FAAC
		// (set) Token: 0x06001EB2 RID: 7858 RVA: 0x000702B8 File Offset: 0x0006E4B8
		public override RequestCachePolicy CachePolicy
		{
			get
			{
				return FtpWebRequest.DefaultCachePolicy;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies the data type for file transfers.</summary>
		/// <returns>true to indicate to the server that the data to be transferred is binary; false to indicate that the data is text. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress.</exception>
		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x06001EB3 RID: 7859 RVA: 0x000718B3 File Offset: 0x0006FAB3
		// (set) Token: 0x06001EB4 RID: 7860 RVA: 0x000718BB File Offset: 0x0006FABB
		public bool UseBinary
		{
			get
			{
				return this._binary;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._binary = value;
			}
		}

		/// <summary>Gets or sets the behavior of a client application's data transfer process.</summary>
		/// <returns>false if the client application's data transfer process listens for a connection on the data port; otherwise, true if the client should initiate a connection on the data port. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001EB5 RID: 7861 RVA: 0x000718D7 File Offset: 0x0006FAD7
		// (set) Token: 0x06001EB6 RID: 7862 RVA: 0x000718DF File Offset: 0x0006FADF
		public bool UsePassive
		{
			get
			{
				return this._passive;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._passive = value;
			}
		}

		/// <summary>Gets or sets the certificates used for establishing an encrypted connection to the FTP server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object that contains the client certificates.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x000718FB File Offset: 0x0006FAFB
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x0007192D File Offset: 0x0006FB2D
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return LazyInitializer.EnsureInitialized<X509CertificateCollection>(ref this._clientCertificates, ref this._syncObject, () => new X509CertificateCollection());
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> that specifies that an SSL connection should be used.</summary>
		/// <returns>true if control and data transmissions are encrypted; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection to the FTP server has already been established.</exception>
		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00071944 File Offset: 0x0006FB44
		// (set) Token: 0x06001EBA RID: 7866 RVA: 0x0007194C File Offset: 0x0006FB4C
		public bool EnableSsl
		{
			get
			{
				return this._enableSsl;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._enableSsl = value;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00071968 File Offset: 0x0006FB68
		// (set) Token: 0x06001EBC RID: 7868 RVA: 0x00071983 File Offset: 0x0006FB83
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this._ftpRequestHeaders == null)
				{
					this._ftpRequestHeaders = new WebHeaderCollection();
				}
				return this._ftpRequestHeaders;
			}
			set
			{
				this._ftpRequestHeaders = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Content type information is not supported for FTP.</exception>
		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x0007198C File Offset: 0x0006FB8C
		// (set) Token: 0x06001EBE RID: 7870 RVA: 0x0007198C File Offset: 0x0006FB8C
		public override string ContentType
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for FTP.</exception>
		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x0007198C File Offset: 0x0006FB8C
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x0007198C File Offset: 0x0006FB8C
		public override bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Preauthentication is not supported for FTP.</exception>
		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x0007198C File Offset: 0x0006FB8C
		// (set) Token: 0x06001EC2 RID: 7874 RVA: 0x0007198C File Offset: 0x0006FB8C
		public override bool PreAuthenticate
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x06001EC3 RID: 7875 RVA: 0x00071993 File Offset: 0x0006FB93
		private bool InUse
		{
			get
			{
				return this._getRequestStreamStarted || this._getResponseStarted;
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x000719A8 File Offset: 0x0006FBA8
		private void EnsureFtpWebResponse(Exception exception)
		{
			if (this._ftpWebResponse == null || (this._ftpWebResponse.GetResponseStream() is FtpWebResponse.EmptyStream && this._stream != null))
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._ftpWebResponse == null || (this._ftpWebResponse.GetResponseStream() is FtpWebResponse.EmptyStream && this._stream != null))
					{
						Stream stream = this._stream;
						if (this._methodInfo.IsUpload)
						{
							stream = null;
						}
						if (this._stream != null && this._stream.CanRead && this._stream.CanTimeout)
						{
							this._stream.ReadTimeout = this.ReadWriteTimeout;
							this._stream.WriteTimeout = this.ReadWriteTimeout;
						}
						FtpControlStream connection = this._connection;
						long num = ((connection != null) ? connection.ContentLength : (-1L));
						if (stream == null && num < 0L)
						{
							num = 0L;
						}
						if (this._ftpWebResponse != null)
						{
							this._ftpWebResponse.SetResponseStream(stream);
						}
						else if (connection != null)
						{
							this._ftpWebResponse = new FtpWebResponse(stream, num, connection.ResponseUri, connection.StatusCode, connection.StatusLine, connection.LastModified, connection.BannerMessage, connection.WelcomeMessage, connection.ExitMessage);
						}
						else
						{
							this._ftpWebResponse = new FtpWebResponse(stream, -1L, this._uri, FtpStatusCode.Undefined, null, DateTime.Now, null, null, null);
						}
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Returns {0} with stream {1}", new object[]
				{
					this._ftpWebResponse,
					this._ftpWebResponse._responseStream
				}), "EnsureFtpWebResponse");
			}
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00071B68 File Offset: 0x0006FD68
		internal void DataStreamClosed(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) == CloseExState.Normal)
			{
				if (this._async)
				{
					this._requestCompleteAsyncResult.InternalWaitForCompletion();
					this.CheckError();
					return;
				}
				if (this._connection != null)
				{
					this._connection.CheckContinuePipeline();
					return;
				}
			}
			else
			{
				FtpControlStream connection = this._connection;
				if (connection != null)
				{
					connection.Abort(ExceptionHelper.RequestAbortedException);
				}
			}
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00013B26 File Offset: 0x00011D26
		internal FtpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001037 RID: 4151
		private object _syncObject;

		// Token: 0x04001038 RID: 4152
		private ICredentials _authInfo;

		// Token: 0x04001039 RID: 4153
		private readonly Uri _uri;

		// Token: 0x0400103A RID: 4154
		private FtpMethodInfo _methodInfo;

		// Token: 0x0400103B RID: 4155
		private string _renameTo;

		// Token: 0x0400103C RID: 4156
		private bool _getRequestStreamStarted;

		// Token: 0x0400103D RID: 4157
		private bool _getResponseStarted;

		// Token: 0x0400103E RID: 4158
		private DateTime _startTime;

		// Token: 0x0400103F RID: 4159
		private int _timeout;

		// Token: 0x04001040 RID: 4160
		private int _remainingTimeout;

		// Token: 0x04001041 RID: 4161
		private long _contentLength;

		// Token: 0x04001042 RID: 4162
		private long _contentOffset;

		// Token: 0x04001043 RID: 4163
		private X509CertificateCollection _clientCertificates;

		// Token: 0x04001044 RID: 4164
		private bool _passive;

		// Token: 0x04001045 RID: 4165
		private bool _binary;

		// Token: 0x04001046 RID: 4166
		private string _connectionGroupName;

		// Token: 0x04001047 RID: 4167
		private ServicePoint _servicePoint;

		// Token: 0x04001048 RID: 4168
		private bool _async;

		// Token: 0x04001049 RID: 4169
		private bool _aborted;

		// Token: 0x0400104A RID: 4170
		private bool _timedOut;

		// Token: 0x0400104B RID: 4171
		private Exception _exception;

		// Token: 0x0400104C RID: 4172
		private TimerThread.Queue _timerQueue;

		// Token: 0x0400104D RID: 4173
		private TimerThread.Callback _timerCallback;

		// Token: 0x0400104E RID: 4174
		private bool _enableSsl;

		// Token: 0x0400104F RID: 4175
		private FtpControlStream _connection;

		// Token: 0x04001050 RID: 4176
		private Stream _stream;

		// Token: 0x04001051 RID: 4177
		private FtpWebRequest.RequestStage _requestStage;

		// Token: 0x04001052 RID: 4178
		private bool _onceFailed;

		// Token: 0x04001053 RID: 4179
		private WebHeaderCollection _ftpRequestHeaders;

		// Token: 0x04001054 RID: 4180
		private FtpWebResponse _ftpWebResponse;

		// Token: 0x04001055 RID: 4181
		private int _readWriteTimeout;

		// Token: 0x04001056 RID: 4182
		private ContextAwareResult _writeAsyncResult;

		// Token: 0x04001057 RID: 4183
		private LazyAsyncResult _readAsyncResult;

		// Token: 0x04001058 RID: 4184
		private LazyAsyncResult _requestCompleteAsyncResult;

		// Token: 0x04001059 RID: 4185
		private static readonly NetworkCredential s_defaultFtpNetworkCredential = new NetworkCredential("anonymous", "anonymous@", string.Empty);

		// Token: 0x0400105A RID: 4186
		private const int s_DefaultTimeout = 100000;

		// Token: 0x0400105B RID: 4187
		private static readonly TimerThread.Queue s_DefaultTimerQueue = TimerThread.GetOrCreateQueue(100000);

		// Token: 0x020003A0 RID: 928
		private enum RequestStage
		{
			// Token: 0x0400105D RID: 4189
			CheckForError,
			// Token: 0x0400105E RID: 4190
			RequestStarted,
			// Token: 0x0400105F RID: 4191
			WriteReady,
			// Token: 0x04001060 RID: 4192
			ReadReady,
			// Token: 0x04001061 RID: 4193
			ReleaseConnection
		}
	}
}
