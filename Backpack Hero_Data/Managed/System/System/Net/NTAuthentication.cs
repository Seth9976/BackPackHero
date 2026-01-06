using System;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace System.Net
{
	// Token: 0x02000379 RID: 889
	internal class NTAuthentication
	{
		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001D4E RID: 7502 RVA: 0x0006ABEE File Offset: 0x00068DEE
		internal bool IsCompleted
		{
			get
			{
				return this._isCompleted;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x06001D4F RID: 7503 RVA: 0x0006ABF6 File Offset: 0x00068DF6
		internal bool IsValidContext
		{
			get
			{
				return this._securityContext != null && !this._securityContext.IsInvalid;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001D50 RID: 7504 RVA: 0x0006AC10 File Offset: 0x00068E10
		internal string Package
		{
			get
			{
				return this._package;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0006AC18 File Offset: 0x00068E18
		internal bool IsServer
		{
			get
			{
				return this._isServer;
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001D52 RID: 7506 RVA: 0x0006AC20 File Offset: 0x00068E20
		internal string ClientSpecifiedSpn
		{
			get
			{
				if (this._clientSpecifiedSpn == null)
				{
					this._clientSpecifiedSpn = this.GetClientSpecifiedSpn();
				}
				return this._clientSpecifiedSpn;
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0006AC3C File Offset: 0x00068E3C
		internal string ProtocolName
		{
			get
			{
				if (this._protocolName == null)
				{
					string text = null;
					if (this.IsValidContext)
					{
						text = NegotiateStreamPal.QueryContextAuthenticationPackage(this._securityContext);
						if (this.IsCompleted)
						{
							this._protocolName = text;
						}
					}
					return text ?? string.Empty;
				}
				return this._protocolName;
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001D54 RID: 7508 RVA: 0x0006AC87 File Offset: 0x00068E87
		internal bool IsKerberos
		{
			get
			{
				if (this._lastProtocolName == null)
				{
					this._lastProtocolName = this.ProtocolName;
				}
				return this._lastProtocolName == "Kerberos";
			}
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x0006ACAA File Offset: 0x00068EAA
		internal NTAuthentication(bool isServer, string package, NetworkCredential credential, string spn, ContextFlagsPal requestedContextFlags, ChannelBinding channelBinding)
		{
			this.Initialize(isServer, package, credential, spn, requestedContextFlags, channelBinding);
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x0006ACC4 File Offset: 0x00068EC4
		private void Initialize(bool isServer, string package, NetworkCredential credential, string spn, ContextFlagsPal requestedContextFlags, ChannelBinding channelBinding)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, package, spn, requestedContextFlags, "Initialize");
			}
			this._tokenSize = NegotiateStreamPal.QueryMaxTokenSize(package);
			this._isServer = isServer;
			this._spn = spn;
			this._securityContext = null;
			this._requestedContextFlags = requestedContextFlags;
			this._package = package;
			this._channelBinding = channelBinding;
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Peer SPN-> '{0}'", new object[] { this._spn }), "Initialize");
			}
			if (credential == CredentialCache.DefaultCredentials)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "using DefaultCredentials", "Initialize");
				}
				this._credentialsHandle = NegotiateStreamPal.AcquireDefaultCredential(package, this._isServer);
				return;
			}
			this._credentialsHandle = NegotiateStreamPal.AcquireCredentialsHandle(package, this._isServer, credential);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x0006AD98 File Offset: 0x00068F98
		internal SafeDeleteContext GetContext(out SecurityStatusPal status)
		{
			status = new SecurityStatusPal(SecurityStatusPalErrorCode.OK, null);
			if (!this.IsCompleted || !this.IsValidContext)
			{
				NetEventSource.Fail(this, "Should be called only when completed with success, currently is not!", "GetContext");
			}
			if (!this.IsServer)
			{
				NetEventSource.Fail(this, "The method must not be called by the client side!", "GetContext");
			}
			if (!this.IsValidContext)
			{
				status = new SecurityStatusPal(SecurityStatusPalErrorCode.InvalidHandle, null);
				return null;
			}
			return this._securityContext;
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x0006AE08 File Offset: 0x00069008
		internal void CloseContext()
		{
			if (this._securityContext != null && !this._securityContext.IsClosed)
			{
				this._securityContext.Dispose();
			}
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x0006AE2A File Offset: 0x0006902A
		internal int VerifySignature(byte[] buffer, int offset, int count)
		{
			return NegotiateStreamPal.VerifySignature(this._securityContext, buffer, offset, count);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x0006AE3A File Offset: 0x0006903A
		internal int MakeSignature(byte[] buffer, int offset, int count, ref byte[] output)
		{
			return NegotiateStreamPal.MakeSignature(this._securityContext, buffer, offset, count, ref output);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0006AE4C File Offset: 0x0006904C
		internal string GetOutgoingBlob(string incomingBlob)
		{
			byte[] array = null;
			if (incomingBlob != null && incomingBlob.Length > 0)
			{
				array = Convert.FromBase64String(incomingBlob);
			}
			byte[] array2 = null;
			if ((this.IsValidContext || this.IsCompleted) && array == null)
			{
				this._isCompleted = true;
			}
			else
			{
				SecurityStatusPal securityStatusPal;
				array2 = this.GetOutgoingBlob(array, true, out securityStatusPal);
			}
			string text = null;
			if (array2 != null && array2.Length != 0)
			{
				text = Convert.ToBase64String(array2);
			}
			if (this.IsCompleted)
			{
				this.CloseContext();
			}
			return text;
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0006AEB8 File Offset: 0x000690B8
		internal byte[] GetOutgoingBlob(byte[] incomingBlob, bool thrownOnError)
		{
			SecurityStatusPal securityStatusPal;
			return this.GetOutgoingBlob(incomingBlob, thrownOnError, out securityStatusPal);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0006AED0 File Offset: 0x000690D0
		internal byte[] GetOutgoingBlob(byte[] incomingBlob, bool throwOnError, out SecurityStatusPal statusCode)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, incomingBlob, "GetOutgoingBlob");
			}
			SecurityBuffer[] array = null;
			if (incomingBlob != null && this._channelBinding != null)
			{
				array = new SecurityBuffer[]
				{
					new SecurityBuffer(incomingBlob, SecurityBufferType.SECBUFFER_TOKEN),
					new SecurityBuffer(this._channelBinding)
				};
			}
			else if (incomingBlob != null)
			{
				array = new SecurityBuffer[]
				{
					new SecurityBuffer(incomingBlob, SecurityBufferType.SECBUFFER_TOKEN)
				};
			}
			else if (this._channelBinding != null)
			{
				array = new SecurityBuffer[]
				{
					new SecurityBuffer(this._channelBinding)
				};
			}
			SecurityBuffer securityBuffer = new SecurityBuffer(this._tokenSize, SecurityBufferType.SECBUFFER_TOKEN);
			bool flag = this._securityContext == null;
			try
			{
				if (!this._isServer)
				{
					statusCode = NegotiateStreamPal.InitializeSecurityContext(this._credentialsHandle, ref this._securityContext, this._spn, this._requestedContextFlags, array, securityBuffer, ref this._contextFlags);
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.InitializeSecurityContext() returns statusCode:0x{0:x8} ({1})", new object[]
						{
							(int)statusCode.ErrorCode,
							statusCode
						}), "GetOutgoingBlob");
					}
					if (statusCode.ErrorCode == SecurityStatusPalErrorCode.CompleteNeeded)
					{
						statusCode = NegotiateStreamPal.CompleteAuthToken(ref this._securityContext, new SecurityBuffer[] { securityBuffer });
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.CompleteAuthToken() returns statusCode:0x{0:x8} ({1})", new object[]
							{
								(int)statusCode.ErrorCode,
								statusCode
							}), "GetOutgoingBlob");
						}
						securityBuffer.token = null;
					}
				}
				else
				{
					statusCode = NegotiateStreamPal.AcceptSecurityContext(this._credentialsHandle, ref this._securityContext, this._requestedContextFlags, array, securityBuffer, ref this._contextFlags);
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, FormattableStringFactory.Create("SSPIWrapper.AcceptSecurityContext() returns statusCode:0x{0:x8} ({1})", new object[]
						{
							(int)statusCode.ErrorCode,
							statusCode
						}), "GetOutgoingBlob");
					}
				}
			}
			finally
			{
				if (flag && this._credentialsHandle != null)
				{
					this._credentialsHandle.Dispose();
				}
			}
			if (statusCode.ErrorCode < SecurityStatusPalErrorCode.OutOfMemory)
			{
				if (flag && this._credentialsHandle != null)
				{
					SSPIHandleCache.CacheCredential(this._credentialsHandle);
				}
				if (statusCode.ErrorCode == SecurityStatusPalErrorCode.OK)
				{
					this._isCompleted = true;
				}
				else if (NetEventSource.IsEnabled && NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("need continue statusCode:0x{0:x8} ({1}) _securityContext:{2}", new object[]
					{
						(int)statusCode.ErrorCode,
						statusCode,
						this._securityContext
					}), "GetOutgoingBlob");
				}
				if (NetEventSource.IsEnabled && NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, FormattableStringFactory.Create("IsCompleted: {0}", new object[] { this.IsCompleted }), "GetOutgoingBlob");
				}
				return securityBuffer.token;
			}
			this.CloseContext();
			this._isCompleted = true;
			if (throwOnError)
			{
				Exception ex = NegotiateStreamPal.CreateExceptionFromError(statusCode);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, ex, "GetOutgoingBlob");
				}
				throw ex;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Exit(this, FormattableStringFactory.Create("null statusCode:0x{0:x8} ({1})", new object[]
				{
					(int)statusCode.ErrorCode,
					statusCode
				}), "GetOutgoingBlob");
			}
			return null;
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0006B214 File Offset: 0x00069414
		private string GetClientSpecifiedSpn()
		{
			if (!this.IsValidContext || !this.IsCompleted)
			{
				NetEventSource.Fail(this, "Trying to get the client SPN before handshaking is done!", "GetClientSpecifiedSpn");
			}
			string text = NegotiateStreamPal.QueryContextClientSpecifiedSpn(this._securityContext);
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("The client specified SPN is [{0}]", new object[] { text }), "GetClientSpecifiedSpn");
			}
			return text;
		}

		// Token: 0x04000F00 RID: 3840
		private bool _isServer;

		// Token: 0x04000F01 RID: 3841
		private SafeFreeCredentials _credentialsHandle;

		// Token: 0x04000F02 RID: 3842
		private SafeDeleteContext _securityContext;

		// Token: 0x04000F03 RID: 3843
		private string _spn;

		// Token: 0x04000F04 RID: 3844
		private int _tokenSize;

		// Token: 0x04000F05 RID: 3845
		private ContextFlagsPal _requestedContextFlags;

		// Token: 0x04000F06 RID: 3846
		private ContextFlagsPal _contextFlags;

		// Token: 0x04000F07 RID: 3847
		private bool _isCompleted;

		// Token: 0x04000F08 RID: 3848
		private string _package;

		// Token: 0x04000F09 RID: 3849
		private string _lastProtocolName;

		// Token: 0x04000F0A RID: 3850
		private string _protocolName;

		// Token: 0x04000F0B RID: 3851
		private string _clientSpecifiedSpn;

		// Token: 0x04000F0C RID: 3852
		private ChannelBinding _channelBinding;
	}
}
