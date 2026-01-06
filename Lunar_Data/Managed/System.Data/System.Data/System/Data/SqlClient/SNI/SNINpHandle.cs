using System;
using System.ComponentModel;
using System.IO;
using System.IO.Pipes;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x02000245 RID: 581
	internal class SNINpHandle : SNIHandle
	{
		// Token: 0x06001A81 RID: 6785 RVA: 0x000847F8 File Offset: 0x000829F8
		public SNINpHandle(string serverName, string pipeName, long timerExpire, object callbackObject)
		{
			this._targetServer = serverName;
			this._callbackObject = callbackObject;
			try
			{
				this._pipeStream = new NamedPipeClientStream(serverName, pipeName, PipeDirection.InOut, PipeOptions.WriteThrough | PipeOptions.Asynchronous);
				if (9223372036854775807L == timerExpire)
				{
					this._pipeStream.Connect(-1);
				}
				else
				{
					TimeSpan timeSpan = DateTime.FromFileTime(timerExpire) - DateTime.Now;
					timeSpan = ((timeSpan.Ticks < 0L) ? TimeSpan.FromTicks(0L) : timeSpan);
					this._pipeStream.Connect((int)timeSpan.TotalMilliseconds);
				}
			}
			catch (TimeoutException ex)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, ex);
				this._status = 1U;
				return;
			}
			catch (IOException ex2)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 40U, ex2);
				this._status = 1U;
				return;
			}
			if (!this._pipeStream.IsConnected || !this._pipeStream.CanWrite || !this._pipeStream.CanRead)
			{
				SNICommon.ReportSNIError(SNIProviders.NP_PROV, 0U, 40U, string.Empty);
				this._status = 1U;
				return;
			}
			this._sslOverTdsStream = new SslOverTdsStream(this._pipeStream);
			this._sslStream = new SslStream(this._sslOverTdsStream, true, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
			this._stream = this._pipeStream;
			this._status = 0U;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001A82 RID: 6786 RVA: 0x00084974 File Offset: 0x00082B74
		public override Guid ConnectionId
		{
			get
			{
				return this._connectionId;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x0008497C File Offset: 0x00082B7C
		public override uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x00084984 File Offset: 0x00082B84
		public override uint CheckConnection()
		{
			if (!this._stream.CanWrite || !this._stream.CanRead)
			{
				return 1U;
			}
			return 0U;
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000849A4 File Offset: 0x00082BA4
		public override void Dispose()
		{
			lock (this)
			{
				if (this._sslOverTdsStream != null)
				{
					this._sslOverTdsStream.Dispose();
					this._sslOverTdsStream = null;
				}
				if (this._sslStream != null)
				{
					this._sslStream.Dispose();
					this._sslStream = null;
				}
				if (this._pipeStream != null)
				{
					this._pipeStream.Dispose();
					this._pipeStream = null;
				}
				this._stream = null;
			}
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x00084A30 File Offset: 0x00082C30
		public override uint Receive(out SNIPacket packet, int timeout)
		{
			uint num;
			lock (this)
			{
				packet = null;
				try
				{
					packet = new SNIPacket(this._bufferSize);
					packet.ReadFromStream(this._stream);
					if (packet.Length == 0)
					{
						Win32Exception ex = new Win32Exception();
						return this.ReportErrorAndReleasePacket(packet, (uint)ex.NativeErrorCode, 0U, ex.Message);
					}
				}
				catch (ObjectDisposedException ex2)
				{
					return this.ReportErrorAndReleasePacket(packet, ex2);
				}
				catch (IOException ex3)
				{
					return this.ReportErrorAndReleasePacket(packet, ex3);
				}
				num = 0U;
			}
			return num;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x00084AE4 File Offset: 0x00082CE4
		public override uint ReceiveAsync(ref SNIPacket packet)
		{
			packet = new SNIPacket(this._bufferSize);
			uint num;
			try
			{
				packet.ReadFromStreamAsync(this._stream, this._receiveCallback);
				num = 997U;
			}
			catch (ObjectDisposedException ex)
			{
				num = this.ReportErrorAndReleasePacket(packet, ex);
			}
			catch (IOException ex2)
			{
				num = this.ReportErrorAndReleasePacket(packet, ex2);
			}
			return num;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x00084B50 File Offset: 0x00082D50
		public override uint Send(SNIPacket packet)
		{
			uint num;
			lock (this)
			{
				try
				{
					packet.WriteToStream(this._stream);
					num = 0U;
				}
				catch (ObjectDisposedException ex)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex);
				}
				catch (IOException ex2)
				{
					num = this.ReportErrorAndReleasePacket(packet, ex2);
				}
			}
			return num;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x00084BC8 File Offset: 0x00082DC8
		public override uint SendAsync(SNIPacket packet, bool disposePacketAfterSendAsync, SNIAsyncCallback callback = null)
		{
			SNIAsyncCallback sniasyncCallback = callback ?? this._sendCallback;
			packet.WriteToStreamAsync(this._stream, sniasyncCallback, SNIProviders.NP_PROV, disposePacketAfterSendAsync);
			return 997U;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00084BF5 File Offset: 0x00082DF5
		public override void SetAsyncCallbacks(SNIAsyncCallback receiveCallback, SNIAsyncCallback sendCallback)
		{
			this._receiveCallback = receiveCallback;
			this._sendCallback = sendCallback;
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00084C08 File Offset: 0x00082E08
		public override uint EnableSsl(uint options)
		{
			this._validateCert = (options & 1U) > 0U;
			try
			{
				this._sslStream.AuthenticateAsClientAsync(this._targetServer).GetAwaiter().GetResult();
				this._sslOverTdsStream.FinishHandshake();
			}
			catch (AuthenticationException ex)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, ex);
			}
			catch (InvalidOperationException ex2)
			{
				return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, ex2);
			}
			this._stream = this._sslStream;
			return 0U;
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00084C94 File Offset: 0x00082E94
		public override void DisableSsl()
		{
			this._sslStream.Dispose();
			this._sslStream = null;
			this._sslOverTdsStream.Dispose();
			this._sslOverTdsStream = null;
			this._stream = this._pipeStream;
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00084CC6 File Offset: 0x00082EC6
		private bool ValidateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			return !this._validateCert || SNICommon.ValidateSslServerCertificate(this._targetServer, sender, cert, chain, policyErrors);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00084CE2 File Offset: 0x00082EE2
		public override void SetBufferSize(int bufferSize)
		{
			this._bufferSize = bufferSize;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00084CEB File Offset: 0x00082EEB
		private uint ReportErrorAndReleasePacket(SNIPacket packet, Exception sniException)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, 35U, sniException);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00084CFF File Offset: 0x00082EFF
		private uint ReportErrorAndReleasePacket(SNIPacket packet, uint nativeError, uint sniError, string errorMessage)
		{
			if (packet != null)
			{
				packet.Release();
			}
			return SNICommon.ReportSNIError(SNIProviders.NP_PROV, nativeError, sniError, errorMessage);
		}

		// Token: 0x04001313 RID: 4883
		internal const string DefaultPipePath = "sql\\query";

		// Token: 0x04001314 RID: 4884
		private const int MAX_PIPE_INSTANCES = 255;

		// Token: 0x04001315 RID: 4885
		private readonly string _targetServer;

		// Token: 0x04001316 RID: 4886
		private readonly object _callbackObject;

		// Token: 0x04001317 RID: 4887
		private Stream _stream;

		// Token: 0x04001318 RID: 4888
		private NamedPipeClientStream _pipeStream;

		// Token: 0x04001319 RID: 4889
		private SslOverTdsStream _sslOverTdsStream;

		// Token: 0x0400131A RID: 4890
		private SslStream _sslStream;

		// Token: 0x0400131B RID: 4891
		private SNIAsyncCallback _receiveCallback;

		// Token: 0x0400131C RID: 4892
		private SNIAsyncCallback _sendCallback;

		// Token: 0x0400131D RID: 4893
		private bool _validateCert = true;

		// Token: 0x0400131E RID: 4894
		private readonly uint _status = uint.MaxValue;

		// Token: 0x0400131F RID: 4895
		private int _bufferSize = 4096;

		// Token: 0x04001320 RID: 4896
		private readonly Guid _connectionId = Guid.NewGuid();
	}
}
