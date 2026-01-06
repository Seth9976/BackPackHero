using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using Unity;

namespace System.Net
{
	/// <summary>Provides connection management for HTTP connections.</summary>
	// Token: 0x020004BE RID: 1214
	public class ServicePoint
	{
		// Token: 0x06002724 RID: 10020 RVA: 0x00091374 File Offset: 0x0008F574
		internal ServicePoint(ServicePointManager.SPKey key, Uri uri, int connectionLimit, int maxIdleTime)
		{
			this.sendContinue = true;
			this.hostE = new object();
			this.connectionLeaseTimeout = -1;
			this.receiveBufferSize = -1;
			base..ctor();
			this.Key = key;
			this.uri = uri;
			this.connectionLimit = connectionLimit;
			this.maxIdleTime = maxIdleTime;
			this.Scheduler = new ServicePointScheduler(this, connectionLimit, maxIdleTime);
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06002725 RID: 10021 RVA: 0x000913D3 File Offset: 0x0008F5D3
		internal ServicePointManager.SPKey Key { get; }

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06002726 RID: 10022 RVA: 0x000913DB File Offset: 0x0008F5DB
		// (set) Token: 0x06002727 RID: 10023 RVA: 0x000913E3 File Offset: 0x0008F5E3
		private ServicePointScheduler Scheduler { get; set; }

		/// <summary>Gets the Uniform Resource Identifier (URI) of the server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</summary>
		/// <returns>An instance of the <see cref="T:System.Uri" /> class that contains the URI of the Internet server that this <see cref="T:System.Net.ServicePoint" /> object connects to.</returns>
		/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Net.ServicePoint" /> is in host mode.</exception>
		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06002728 RID: 10024 RVA: 0x000913EC File Offset: 0x0008F5EC
		public Uri Address
		{
			get
			{
				return this.uri;
			}
		}

		/// <summary>Specifies the delegate to associate a local <see cref="T:System.Net.IPEndPoint" /> with a <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A delegate that forces a <see cref="T:System.Net.ServicePoint" /> to use a particular local Internet Protocol (IP) address and port number. The default value is null.</returns>
		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06002729 RID: 10025 RVA: 0x000913F4 File Offset: 0x0008F5F4
		// (set) Token: 0x0600272A RID: 10026 RVA: 0x000913FC File Offset: 0x0008F5FC
		public BindIPEndPoint BindIPEndPointDelegate
		{
			get
			{
				return this.endPointCallback;
			}
			set
			{
				this.endPointCallback = value;
			}
		}

		/// <summary>Gets or sets the number of milliseconds after which an active <see cref="T:System.Net.ServicePoint" /> connection is closed.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of milliseconds that an active <see cref="T:System.Net.ServicePoint" /> connection remains open. The default is -1, which allows an active <see cref="T:System.Net.ServicePoint" /> connection to stay connected indefinitely. Set this property to 0 to force <see cref="T:System.Net.ServicePoint" /> connections to close after servicing a request.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is a negative number less than -1.</exception>
		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x0600272B RID: 10027 RVA: 0x00091405 File Offset: 0x0008F605
		// (set) Token: 0x0600272C RID: 10028 RVA: 0x0009140D File Offset: 0x0008F60D
		public int ConnectionLeaseTimeout
		{
			get
			{
				return this.connectionLeaseTimeout;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.connectionLeaseTimeout = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of connections allowed on this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The connection limit is equal to or less than 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x00091425 File Offset: 0x0008F625
		// (set) Token: 0x0600272E RID: 10030 RVA: 0x0009142D File Offset: 0x0008F62D
		public int ConnectionLimit
		{
			get
			{
				return this.connectionLimit;
			}
			set
			{
				this.connectionLimit = value;
				if (!this.disposed)
				{
					this.Scheduler.ConnectionLimit = value;
				}
			}
		}

		/// <summary>Gets the connection name. </summary>
		/// <returns>A <see cref="T:System.String" /> that represents the connection name. </returns>
		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x0009144A File Offset: 0x0008F64A
		public string ConnectionName
		{
			get
			{
				return this.uri.Scheme;
			}
		}

		/// <summary>Gets the number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The number of open connections associated with this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x00091457 File Offset: 0x0008F657
		public int CurrentConnections
		{
			get
			{
				if (!this.disposed)
				{
					return this.Scheduler.CurrentConnections;
				}
				return 0;
			}
		}

		/// <summary>Gets the date and time that the <see cref="T:System.Net.ServicePoint" /> object was last connected to a host.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object that contains the date and time at which the <see cref="T:System.Net.ServicePoint" /> object was last connected.</returns>
		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x00091470 File Offset: 0x0008F670
		public DateTime IdleSince
		{
			get
			{
				if (this.disposed)
				{
					return DateTime.MinValue;
				}
				return this.Scheduler.IdleSince.ToLocalTime();
			}
		}

		/// <summary>Gets or sets the amount of time a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before the connection is closed.</summary>
		/// <returns>The length of time, in milliseconds, that a connection associated with the <see cref="T:System.Net.ServicePoint" /> object can remain idle before it is closed and reused for another connection.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePoint.MaxIdleTime" /> is set to less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x0009149E File Offset: 0x0008F69E
		// (set) Token: 0x06002733 RID: 10035 RVA: 0x000914A6 File Offset: 0x0008F6A6
		public int MaxIdleTime
		{
			get
			{
				return this.maxIdleTime;
			}
			set
			{
				this.maxIdleTime = value;
				if (!this.disposed)
				{
					this.Scheduler.MaxIdleTime = value;
				}
			}
		}

		/// <summary>Gets the version of the HTTP protocol that the <see cref="T:System.Net.ServicePoint" /> object uses.</summary>
		/// <returns>A <see cref="T:System.Version" /> object that contains the HTTP protocol version that the <see cref="T:System.Net.ServicePoint" /> object uses.</returns>
		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x000914C3 File Offset: 0x0008F6C3
		public virtual Version ProtocolVersion
		{
			get
			{
				return this.protocolVersion;
			}
		}

		/// <summary>Gets or sets the size of the receiving buffer for the socket used by this <see cref="T:System.Net.ServicePoint" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the size, in bytes, of the receive buffer. The default is 8192.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x000914CB File Offset: 0x0008F6CB
		// (set) Token: 0x06002736 RID: 10038 RVA: 0x000914D3 File Offset: 0x0008F6D3
		public int ReceiveBufferSize
		{
			get
			{
				return this.receiveBufferSize;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.receiveBufferSize = value;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections.</summary>
		/// <returns>true if the <see cref="T:System.Net.ServicePoint" /> object supports pipelined connections; otherwise, false.</returns>
		// Token: 0x17000829 RID: 2089
		// (get) Token: 0x06002737 RID: 10039 RVA: 0x000914EB File Offset: 0x0008F6EB
		public bool SupportsPipelining
		{
			get
			{
				return HttpVersion.Version11.Equals(this.protocolVersion);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to expect 100-Continue responses for POST requests; otherwise, false. The default value is true.</returns>
		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002738 RID: 10040 RVA: 0x000914FD File Offset: 0x0008F6FD
		// (set) Token: 0x06002739 RID: 10041 RVA: 0x00091505 File Offset: 0x0008F705
		public bool Expect100Continue
		{
			get
			{
				return this.SendContinue;
			}
			set
			{
				this.SendContinue = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether the Nagle algorithm is used on connections managed by this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x0600273A RID: 10042 RVA: 0x0009150E File Offset: 0x0008F70E
		// (set) Token: 0x0600273B RID: 10043 RVA: 0x00091516 File Offset: 0x0008F716
		public bool UseNagleAlgorithm
		{
			get
			{
				return this.useNagle;
			}
			set
			{
				this.useNagle = value;
			}
		}

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x0600273C RID: 10044 RVA: 0x0009151F File Offset: 0x0008F71F
		// (set) Token: 0x0600273D RID: 10045 RVA: 0x0009154B File Offset: 0x0008F74B
		internal bool SendContinue
		{
			get
			{
				return this.sendContinue && (this.protocolVersion == null || this.protocolVersion == HttpVersion.Version11);
			}
			set
			{
				this.sendContinue = value;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime " />and <paramref name="keepAliveInterval" /> values. If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent. The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x0600273E RID: 10046 RVA: 0x00091554 File Offset: 0x0008F754
		public void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
		{
			if (enabled)
			{
				if (keepAliveTime <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveTime", "Must be greater than 0");
				}
				if (keepAliveInterval <= 0)
				{
					throw new ArgumentOutOfRangeException("keepAliveInterval", "Must be greater than 0");
				}
			}
			this.tcp_keepalive = enabled;
			this.tcp_keepalive_time = keepAliveTime;
			this.tcp_keepalive_interval = keepAliveInterval;
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x000915A4 File Offset: 0x0008F7A4
		internal void KeepAliveSetup(Socket socket)
		{
			if (!this.tcp_keepalive)
			{
				return;
			}
			byte[] array = new byte[12];
			ServicePoint.PutBytes(array, this.tcp_keepalive ? 1U : 0U, 0);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_time, 4);
			ServicePoint.PutBytes(array, (uint)this.tcp_keepalive_interval, 8);
			socket.IOControl((IOControlCode)((ulong)(-1744830460)), array, null);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x00091600 File Offset: 0x0008F800
		private static void PutBytes(byte[] bytes, uint v, int offset)
		{
			if (BitConverter.IsLittleEndian)
			{
				bytes[offset] = (byte)(v & 255U);
				bytes[offset + 1] = (byte)((v & 65280U) >> 8);
				bytes[offset + 2] = (byte)((v & 16711680U) >> 16);
				bytes[offset + 3] = (byte)((v & 4278190080U) >> 24);
				return;
			}
			bytes[offset + 3] = (byte)(v & 255U);
			bytes[offset + 2] = (byte)((v & 65280U) >> 8);
			bytes[offset + 1] = (byte)((v & 16711680U) >> 16);
			bytes[offset] = (byte)((v & 4278190080U) >> 24);
		}

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002741 RID: 10049 RVA: 0x00091689 File Offset: 0x0008F889
		// (set) Token: 0x06002742 RID: 10050 RVA: 0x00091691 File Offset: 0x0008F891
		internal bool UsesProxy
		{
			get
			{
				return this.usesProxy;
			}
			set
			{
				this.usesProxy = value;
			}
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002743 RID: 10051 RVA: 0x0009169A File Offset: 0x0008F89A
		// (set) Token: 0x06002744 RID: 10052 RVA: 0x000916A2 File Offset: 0x0008F8A2
		internal bool UseConnect
		{
			get
			{
				return this.useConnect;
			}
			set
			{
				this.useConnect = value;
			}
		}

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002745 RID: 10053 RVA: 0x000916AC File Offset: 0x0008F8AC
		private bool HasTimedOut
		{
			get
			{
				int dnsRefreshTimeout = ServicePointManager.DnsRefreshTimeout;
				return dnsRefreshTimeout != -1 && this.lastDnsResolve + TimeSpan.FromMilliseconds((double)dnsRefreshTimeout) < DateTime.UtcNow;
			}
		}

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002746 RID: 10054 RVA: 0x000916E4 File Offset: 0x0008F8E4
		internal IPHostEntry HostEntry
		{
			get
			{
				object obj = this.hostE;
				lock (obj)
				{
					string text = this.uri.Host;
					if (this.uri.HostNameType == UriHostNameType.IPv6 || this.uri.HostNameType == UriHostNameType.IPv4)
					{
						if (this.host != null)
						{
							return this.host;
						}
						if (this.uri.HostNameType == UriHostNameType.IPv6)
						{
							text = text.Substring(1, text.Length - 2);
						}
						this.host = new IPHostEntry();
						this.host.AddressList = new IPAddress[] { IPAddress.Parse(text) };
						return this.host;
					}
					else
					{
						if (!this.HasTimedOut && this.host != null)
						{
							return this.host;
						}
						this.lastDnsResolve = DateTime.UtcNow;
						try
						{
							this.host = Dns.GetHostEntry(text);
						}
						catch
						{
							return null;
						}
					}
				}
				return this.host;
			}
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000917F0 File Offset: 0x0008F9F0
		internal void SetVersion(Version version)
		{
			this.protocolVersion = version;
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000917FC File Offset: 0x0008F9FC
		internal void SendRequest(WebOperation operation, string groupName)
		{
			lock (this)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(typeof(ServicePoint).FullName);
				}
				this.Scheduler.SendRequest(operation, groupName);
			}
		}

		/// <summary>Removes the specified connection group from this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> value that indicates whether the connection group was closed.</returns>
		/// <param name="connectionGroupName">The name of the connection group that contains the connections to close and remove from this service point. </param>
		// Token: 0x06002749 RID: 10057 RVA: 0x0009185C File Offset: 0x0008FA5C
		public bool CloseConnectionGroup(string connectionGroupName)
		{
			bool flag2;
			lock (this)
			{
				if (this.disposed)
				{
					flag2 = true;
				}
				else
				{
					flag2 = this.Scheduler.CloseConnectionGroup(connectionGroupName);
				}
			}
			return flag2;
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000918AC File Offset: 0x0008FAAC
		internal void FreeServicePoint()
		{
			this.disposed = true;
			this.Scheduler = null;
		}

		/// <summary>Gets the certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>An instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> class that contains the security certificate received for this <see cref="T:System.Net.ServicePoint" /> object.</returns>
		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600274B RID: 10059 RVA: 0x000918BC File Offset: 0x0008FABC
		public X509Certificate Certificate
		{
			get
			{
				object serverCertificateOrBytes = this.m_ServerCertificateOrBytes;
				if (serverCertificateOrBytes != null && serverCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ServerCertificateOrBytes = new X509Certificate((byte[])serverCertificateOrBytes));
				}
				return serverCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x0009190A File Offset: 0x0008FB0A
		internal void UpdateServerCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ServerCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ServerCertificateOrBytes = null;
		}

		/// <summary>Gets the last client certificate sent to the server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> object that contains the public values of the last client certificate sent to the server.</returns>
		// Token: 0x17000832 RID: 2098
		// (get) Token: 0x0600274D RID: 10061 RVA: 0x00091924 File Offset: 0x0008FB24
		public X509Certificate ClientCertificate
		{
			get
			{
				object clientCertificateOrBytes = this.m_ClientCertificateOrBytes;
				if (clientCertificateOrBytes != null && clientCertificateOrBytes.GetType() == typeof(byte[]))
				{
					return (X509Certificate)(this.m_ClientCertificateOrBytes = new X509Certificate((byte[])clientCertificateOrBytes));
				}
				return clientCertificateOrBytes as X509Certificate;
			}
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00091972 File Offset: 0x0008FB72
		internal void UpdateClientCertificate(X509Certificate certificate)
		{
			if (certificate != null)
			{
				this.m_ClientCertificateOrBytes = certificate.GetRawCertData();
				return;
			}
			this.m_ClientCertificateOrBytes = null;
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x0009198C File Offset: 0x0008FB8C
		internal bool CallEndPointDelegate(Socket sock, IPEndPoint remote)
		{
			if (this.endPointCallback == null)
			{
				return true;
			}
			int num = 0;
			checked
			{
				for (;;)
				{
					IPEndPoint ipendPoint = null;
					try
					{
						ipendPoint = this.endPointCallback(this, remote, num);
					}
					catch
					{
						return false;
					}
					if (ipendPoint == null)
					{
						break;
					}
					try
					{
						sock.Bind(ipendPoint);
					}
					catch (SocketException)
					{
						num++;
						continue;
					}
					return true;
				}
				return true;
			}
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x00013B26 File Offset: 0x00011D26
		internal ServicePoint()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x040016B2 RID: 5810
		private readonly Uri uri;

		// Token: 0x040016B3 RID: 5811
		private DateTime lastDnsResolve;

		// Token: 0x040016B4 RID: 5812
		private Version protocolVersion;

		// Token: 0x040016B5 RID: 5813
		private IPHostEntry host;

		// Token: 0x040016B6 RID: 5814
		private bool usesProxy;

		// Token: 0x040016B7 RID: 5815
		private bool sendContinue;

		// Token: 0x040016B8 RID: 5816
		private bool useConnect;

		// Token: 0x040016B9 RID: 5817
		private object hostE;

		// Token: 0x040016BA RID: 5818
		private bool useNagle;

		// Token: 0x040016BB RID: 5819
		private BindIPEndPoint endPointCallback;

		// Token: 0x040016BC RID: 5820
		private bool tcp_keepalive;

		// Token: 0x040016BD RID: 5821
		private int tcp_keepalive_time;

		// Token: 0x040016BE RID: 5822
		private int tcp_keepalive_interval;

		// Token: 0x040016BF RID: 5823
		private bool disposed;

		// Token: 0x040016C0 RID: 5824
		private int connectionLeaseTimeout;

		// Token: 0x040016C1 RID: 5825
		private int receiveBufferSize;

		// Token: 0x040016C4 RID: 5828
		private int connectionLimit;

		// Token: 0x040016C5 RID: 5829
		private int maxIdleTime;

		// Token: 0x040016C6 RID: 5830
		private object m_ServerCertificateOrBytes;

		// Token: 0x040016C7 RID: 5831
		private object m_ClientCertificateOrBytes;
	}
}
