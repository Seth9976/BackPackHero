using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x020000A0 RID: 160
	internal abstract class MobileTlsContext : IDisposable
	{
		// Token: 0x060002B7 RID: 695 RVA: 0x00008C38 File Offset: 0x00006E38
		protected MobileTlsContext(MobileAuthenticatedStream parent, MonoSslAuthenticationOptions options)
		{
			this.Parent = parent;
			this.Options = options;
			this.IsServer = options.ServerMode;
			this.EnabledProtocols = options.EnabledSslProtocols;
			if (options.ServerMode)
			{
				this.LocalServerCertificate = options.ServerCertificate;
				this.AskForClientCertificate = options.ClientCertificateRequired;
			}
			else
			{
				this.ClientCertificates = options.ClientCertificates;
				this.TargetHost = options.TargetHost;
				this.ServerName = options.TargetHost;
				if (!string.IsNullOrEmpty(this.ServerName))
				{
					int num = this.ServerName.IndexOf(':');
					if (num > 0)
					{
						this.ServerName = this.ServerName.Substring(0, num);
					}
				}
			}
			this.certificateValidator = ChainValidationHelper.GetInternalValidator(parent.SslStream, parent.Provider, parent.Settings);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00008D06 File Offset: 0x00006F06
		internal MonoSslAuthenticationOptions Options { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008D0E File Offset: 0x00006F0E
		internal MobileAuthenticatedStream Parent { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00008D16 File Offset: 0x00006F16
		public MonoTlsSettings Settings
		{
			get
			{
				return this.Parent.Settings;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00008D23 File Offset: 0x00006F23
		public MonoTlsProvider Provider
		{
			get
			{
				return this.Parent.Provider;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002BD RID: 701
		public abstract bool HasContext { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002BE RID: 702
		public abstract bool IsAuthenticated { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00008D30 File Offset: 0x00006F30
		public bool IsServer { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00008D38 File Offset: 0x00006F38
		internal string TargetHost { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00008D40 File Offset: 0x00006F40
		protected string ServerName { get; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00008D48 File Offset: 0x00006F48
		protected bool AskForClientCertificate { get; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x00008D50 File Offset: 0x00006F50
		protected SslProtocols EnabledProtocols { get; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00008D58 File Offset: 0x00006F58
		protected X509CertificateCollection ClientCertificates { get; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00003062 File Offset: 0x00001262
		internal bool AllowRenegotiation
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00008D60 File Offset: 0x00006F60
		protected void GetProtocolVersions(out TlsProtocolCode? min, out TlsProtocolCode? max)
		{
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls10);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls11);
			}
			else if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				min = new TlsProtocolCode?(TlsProtocolCode.Tls12);
			}
			else
			{
				min = null;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls12) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls12);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls11) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls11);
				return;
			}
			if ((this.EnabledProtocols & SslProtocols.Tls) != SslProtocols.None)
			{
				max = new TlsProtocolCode?(TlsProtocolCode.Tls10);
				return;
			}
			max = null;
		}

		// Token: 0x060002C7 RID: 711
		public abstract void StartHandshake();

		// Token: 0x060002C8 RID: 712
		public abstract bool ProcessHandshake();

		// Token: 0x060002C9 RID: 713
		public abstract void FinishHandshake();

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002CA RID: 714
		public abstract MonoTlsConnectionInfo ConnectionInfo { get; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00008E38 File Offset: 0x00007038
		// (set) Token: 0x060002CC RID: 716 RVA: 0x00008E40 File Offset: 0x00007040
		internal X509Certificate LocalServerCertificate { get; private set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002CD RID: 717
		internal abstract bool IsRemoteCertificateAvailable { get; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002CE RID: 718
		internal abstract X509Certificate LocalClientCertificate { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002CF RID: 719
		public abstract X509Certificate2 RemoteCertificate { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060002D0 RID: 720
		public abstract TlsProtocols NegotiatedProtocol { get; }

		// Token: 0x060002D1 RID: 721
		public abstract void Flush();

		// Token: 0x060002D2 RID: 722
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Read(byte[] buffer, int offset, int count);

		// Token: 0x060002D3 RID: 723
		[return: TupleElementNames(new string[] { "ret", "wantMore" })]
		public abstract ValueTuple<int, bool> Write(byte[] buffer, int offset, int count);

		// Token: 0x060002D4 RID: 724
		public abstract void Shutdown();

		// Token: 0x060002D5 RID: 725
		public abstract bool PendingRenegotiation();

		// Token: 0x060002D6 RID: 726 RVA: 0x00008E4C File Offset: 0x0000704C
		protected bool ValidateCertificate(X509Certificate2 leaf, X509Chain chain)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, leaf, chain);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00008E8C File Offset: 0x0000708C
		protected bool ValidateCertificate(X509Certificate2Collection certificates)
		{
			ValidationResult validationResult = this.certificateValidator.ValidateCertificate(this.TargetHost, this.IsServer, certificates);
			return validationResult != null && validationResult.Trusted && !validationResult.UserDenied;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00008EC8 File Offset: 0x000070C8
		protected X509Certificate SelectServerCertificate(string serverIdentity)
		{
			if (this.Options.ServerCertSelectionDelegate != null)
			{
				this.LocalServerCertificate = this.Options.ServerCertSelectionDelegate(serverIdentity);
				if (this.LocalServerCertificate == null)
				{
					throw new AuthenticationException("The server mode SSL must use a certificate with the associated private key.");
				}
			}
			else if (this.Settings.ClientCertificateSelectionCallback != null)
			{
				X509CertificateCollection x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.Options.ServerCertificate);
				this.LocalServerCertificate = this.Settings.ClientCertificateSelectionCallback(string.Empty, x509CertificateCollection, null, Array.Empty<string>());
			}
			else
			{
				this.LocalServerCertificate = this.Options.ServerCertificate;
			}
			if (this.LocalServerCertificate == null)
			{
				throw new NotSupportedException("The server mode SSL must use a certificate with the associated private key.");
			}
			return this.LocalServerCertificate;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008F80 File Offset: 0x00007180
		protected X509Certificate SelectClientCertificate(string[] acceptableIssuers)
		{
			if (this.Settings.DisallowUnauthenticatedCertificateRequest && !this.IsAuthenticated)
			{
				return null;
			}
			if (this.RemoteCertificate == null)
			{
				throw new TlsException(AlertDescription.InternalError, "Cannot request client certificate before receiving one from the server.");
			}
			X509Certificate x509Certificate;
			if (this.certificateValidator.SelectClientCertificate(this.TargetHost, this.ClientCertificates, this.IsAuthenticated ? this.RemoteCertificate : null, acceptableIssuers, out x509Certificate))
			{
				return x509Certificate;
			}
			if (this.ClientCertificates == null || this.ClientCertificates.Count == 0)
			{
				return null;
			}
			if (acceptableIssuers == null || acceptableIssuers.Length == 0)
			{
				return this.ClientCertificates[0];
			}
			for (int i = 0; i < this.ClientCertificates.Count; i++)
			{
				X509Certificate2 x509Certificate2 = this.ClientCertificates[i] as X509Certificate2;
				if (x509Certificate2 != null)
				{
					X509Chain x509Chain = null;
					try
					{
						x509Chain = new X509Chain();
						x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.NoCheck;
						x509Chain.ChainPolicy.VerificationFlags = X509VerificationFlags.IgnoreInvalidName;
						x509Chain.Build(x509Certificate2);
						if (x509Chain.ChainElements.Count != 0)
						{
							for (int j = 0; j < x509Chain.ChainElements.Count; j++)
							{
								string issuer = x509Chain.ChainElements[j].Certificate.Issuer;
								if (Array.IndexOf<string>(acceptableIssuers, issuer) != -1)
								{
									return x509Certificate2;
								}
							}
						}
					}
					catch
					{
					}
					finally
					{
						if (x509Chain != null)
						{
							x509Chain.Reset();
						}
					}
				}
			}
			return null;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002DA RID: 730
		public abstract bool CanRenegotiate { get; }

		// Token: 0x060002DB RID: 731
		public abstract void Renegotiate();

		// Token: 0x060002DC RID: 732 RVA: 0x000090F4 File Offset: 0x000072F4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00009104 File Offset: 0x00007304
		~MobileTlsContext()
		{
			this.Dispose(false);
		}

		// Token: 0x04000269 RID: 617
		private ChainValidationHelper certificateValidator;
	}
}
