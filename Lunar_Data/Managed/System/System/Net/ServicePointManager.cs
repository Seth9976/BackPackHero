using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Net.Configuration;
using System.Net.Security;
using System.Threading;

namespace System.Net
{
	/// <summary>Manages the collection of <see cref="T:System.Net.ServicePoint" /> objects.</summary>
	// Token: 0x020004BF RID: 1215
	public class ServicePointManager
	{
		// Token: 0x06002751 RID: 10065 RVA: 0x000919F4 File Offset: 0x0008FBF4
		static ServicePointManager()
		{
			ConnectionManagementSection connectionManagementSection = ConfigurationManager.GetSection("system.net/connectionManagement") as ConnectionManagementSection;
			if (connectionManagementSection != null)
			{
				ServicePointManager.manager = new ConnectionManagementData(null);
				foreach (object obj in connectionManagementSection.ConnectionManagement)
				{
					ConnectionManagementElement connectionManagementElement = (ConnectionManagementElement)obj;
					ServicePointManager.manager.Add(connectionManagementElement.Address, connectionManagementElement.MaxConnection);
				}
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
				return;
			}
			ServicePointManager.manager = (ConnectionManagementData)ConfigurationSettings.GetConfig("system.net/connectionManagement");
			if (ServicePointManager.manager != null)
			{
				ServicePointManager.defaultConnectionLimit = (int)ServicePointManager.manager.GetMaxConnections("*");
			}
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x0000219B File Offset: 0x0000039B
		private ServicePointManager()
		{
		}

		/// <summary>Gets or sets policy for server certificates.</summary>
		/// <returns>An object that implements the <see cref="T:System.Net.ICertificatePolicy" /> interface.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000833 RID: 2099
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x00091AFC File Offset: 0x0008FCFC
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x00091B1B File Offset: 0x0008FD1B
		[Obsolete("Use ServerCertificateValidationCallback instead", false)]
		public static ICertificatePolicy CertificatePolicy
		{
			get
			{
				if (ServicePointManager.policy == null)
				{
					Interlocked.CompareExchange<ICertificatePolicy>(ref ServicePointManager.policy, new DefaultCertificatePolicy(), null);
				}
				return ServicePointManager.policy;
			}
			set
			{
				ServicePointManager.policy = value;
			}
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x00091B23 File Offset: 0x0008FD23
		internal static ICertificatePolicy GetLegacyCertificatePolicy()
		{
			return ServicePointManager.policy;
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether the certificate is checked against the certificate authority revocation list.</summary>
		/// <returns>true if the certificate revocation list is checked; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000834 RID: 2100
		// (get) Token: 0x06002756 RID: 10070 RVA: 0x00091B2A File Offset: 0x0008FD2A
		// (set) Token: 0x06002757 RID: 10071 RVA: 0x00091B31 File Offset: 0x0008FD31
		[MonoTODO("CRL checks not implemented")]
		public static bool CheckCertificateRevocationList
		{
			get
			{
				return ServicePointManager._checkCRL;
			}
			set
			{
				ServicePointManager._checkCRL = false;
			}
		}

		/// <summary>Gets or sets the maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum number of concurrent connections allowed by a <see cref="T:System.Net.ServicePoint" /> object. The default value is 2.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> is less than or equal to 0. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06002758 RID: 10072 RVA: 0x00091B39 File Offset: 0x0008FD39
		// (set) Token: 0x06002759 RID: 10073 RVA: 0x00091B40 File Offset: 0x0008FD40
		public static int DefaultConnectionLimit
		{
			get
			{
				return ServicePointManager.defaultConnectionLimit;
			}
			set
			{
				if (value <= 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.defaultConnectionLimit = value;
				if (ServicePointManager.manager != null)
				{
					ServicePointManager.manager.Add("*", ServicePointManager.defaultConnectionLimit);
				}
			}
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0001FC8B File Offset: 0x0001DE8B
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets a value that indicates how long a Domain Name Service (DNS) resolution is considered valid.</summary>
		/// <returns>The time-out value, in milliseconds. A value of -1 indicates an infinite time-out period. The default value is 120,000 milliseconds (two minutes).</returns>
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x0600275B RID: 10075 RVA: 0x00091B72 File Offset: 0x0008FD72
		// (set) Token: 0x0600275C RID: 10076 RVA: 0x00091B79 File Offset: 0x0008FD79
		public static int DnsRefreshTimeout
		{
			get
			{
				return ServicePointManager.dnsRefreshTimeout;
			}
			set
			{
				ServicePointManager.dnsRefreshTimeout = Math.Max(-1, value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether a Domain Name Service (DNS) resolution rotates among the applicable Internet Protocol (IP) addresses.</summary>
		/// <returns>false if a DNS resolution always returns the first IP address for a particular host; otherwise true. The default is false.</returns>
		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x0600275D RID: 10077 RVA: 0x00091B87 File Offset: 0x0008FD87
		// (set) Token: 0x0600275E RID: 10078 RVA: 0x00091B87 File Offset: 0x0008FD87
		[MonoTODO]
		public static bool EnableDnsRoundRobin
		{
			get
			{
				throw ServicePointManager.GetMustImplement();
			}
			set
			{
				throw ServicePointManager.GetMustImplement();
			}
		}

		/// <summary>Gets or sets the maximum idle time of a <see cref="T:System.Net.ServicePoint" /> object.</summary>
		/// <returns>The maximum idle time, in milliseconds, of a <see cref="T:System.Net.ServicePoint" /> object. The default value is 100,000 milliseconds (100 seconds).</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePointIdleTime" /> is less than <see cref="F:System.Threading.Timeout.Infinite" /> or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x0600275F RID: 10079 RVA: 0x00091B8E File Offset: 0x0008FD8E
		// (set) Token: 0x06002760 RID: 10080 RVA: 0x00091B95 File Offset: 0x0008FD95
		public static int MaxServicePointIdleTime
		{
			get
			{
				return ServicePointManager.maxServicePointIdleTime;
			}
			set
			{
				if (value < -2 || value > 2147483647)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				ServicePointManager.maxServicePointIdleTime = value;
			}
		}

		/// <summary>Gets or sets the maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain at any time.</summary>
		/// <returns>The maximum number of <see cref="T:System.Net.ServicePoint" /> objects to maintain. The default value is 0, which means there is no limit to the number of <see cref="T:System.Net.ServicePoint" /> objects.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> is less than 0 or greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000839 RID: 2105
		// (get) Token: 0x06002761 RID: 10081 RVA: 0x00091BB5 File Offset: 0x0008FDB5
		// (set) Token: 0x06002762 RID: 10082 RVA: 0x00091BBC File Offset: 0x0008FDBC
		public static int MaxServicePoints
		{
			get
			{
				return ServicePointManager.maxServicePoints;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("value");
				}
				ServicePointManager.maxServicePoints = value;
			}
		}

		// Token: 0x1700083A RID: 2106
		// (get) Token: 0x06002763 RID: 10083 RVA: 0x00003062 File Offset: 0x00001262
		// (set) Token: 0x06002764 RID: 10084 RVA: 0x0000822E File Offset: 0x0000642E
		[MonoTODO]
		public static bool ReusePort
		{
			get
			{
				return false;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the security protocol used by the <see cref="T:System.Net.ServicePoint" /> objects managed by the <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>One of the values defined in the <see cref="T:System.Net.SecurityProtocolType" /> enumeration.</returns>
		/// <exception cref="T:System.NotSupportedException">The value specified to set the property is not a valid <see cref="T:System.Net.SecurityProtocolType" /> enumeration value. </exception>
		// Token: 0x1700083B RID: 2107
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x00091BD3 File Offset: 0x0008FDD3
		// (set) Token: 0x06002766 RID: 10086 RVA: 0x00091BDA File Offset: 0x0008FDDA
		public static SecurityProtocolType SecurityProtocol
		{
			get
			{
				return ServicePointManager._securityProtocol;
			}
			set
			{
				ServicePointManager._securityProtocol = value;
			}
		}

		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x00091BE2 File Offset: 0x0008FDE2
		internal static ServerCertValidationCallback ServerCertValidationCallback
		{
			get
			{
				return ServicePointManager.server_cert_cb;
			}
		}

		/// <summary>Gets or sets the callback to validate a server certificate.</summary>
		/// <returns>A <see cref="T:System.Net.Security.RemoteCertificateValidationCallback" />. The default value is null.</returns>
		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x00091BE9 File Offset: 0x0008FDE9
		// (set) Token: 0x06002769 RID: 10089 RVA: 0x00091BFE File Offset: 0x0008FDFE
		public static RemoteCertificateValidationCallback ServerCertificateValidationCallback
		{
			get
			{
				if (ServicePointManager.server_cert_cb == null)
				{
					return null;
				}
				return ServicePointManager.server_cert_cb.ValidationCallback;
			}
			set
			{
				if (value == null)
				{
					ServicePointManager.server_cert_cb = null;
					return;
				}
				ServicePointManager.server_cert_cb = new ServerCertValidationCallback(value);
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.Security.EncryptionPolicy" /> for this <see cref="T:System.Net.ServicePointManager" /> instance.</summary>
		/// <returns>The encryption policy to use for this <see cref="T:System.Net.ServicePointManager" /> instance.</returns>
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x0600276A RID: 10090 RVA: 0x00003062 File Offset: 0x00001262
		[MonoTODO("Always returns EncryptionPolicy.RequireEncryption.")]
		public static EncryptionPolicy EncryptionPolicy
		{
			get
			{
				return EncryptionPolicy.RequireEncryption;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines whether 100-Continue behavior is used.</summary>
		/// <returns>true to enable 100-Continue behavior. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00091C15 File Offset: 0x0008FE15
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x00091C1C File Offset: 0x0008FE1C
		public static bool Expect100Continue
		{
			get
			{
				return ServicePointManager.expectContinue;
			}
			set
			{
				ServicePointManager.expectContinue = value;
			}
		}

		/// <summary>Determines whether the Nagle algorithm is used by the service points managed by this <see cref="T:System.Net.ServicePointManager" /> object.</summary>
		/// <returns>true to use the Nagle algorithm; otherwise, false. The default value is true.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00091C24 File Offset: 0x0008FE24
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x00091C2B File Offset: 0x0008FE2B
		public static bool UseNagleAlgorithm
		{
			get
			{
				return ServicePointManager.useNagle;
			}
			set
			{
				ServicePointManager.useNagle = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x0600276F RID: 10095 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool DisableStrongCrypto
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002770 RID: 10096 RVA: 0x00003062 File Offset: 0x00001262
		internal static bool DisableSendAuxRecord
		{
			get
			{
				return false;
			}
		}

		/// <summary>Enables or disables the keep-alive option on a TCP connection.</summary>
		/// <param name="enabled">If set to true, then the TCP keep-alive option on a TCP connection will be enabled using the specified <paramref name="keepAliveTime " />and <paramref name="keepAliveInterval" /> values. If set to false, then the TCP keep-alive option is disabled and the remaining parameters are ignored.The default value is false.</param>
		/// <param name="keepAliveTime">Specifies the timeout, in milliseconds, with no activity until the first keep-alive packet is sent.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <param name="keepAliveInterval">Specifies the interval, in milliseconds, between when successive keep-alive packets are sent if no acknowledgement is received.The value must be greater than 0.  If a value of less than or equal to zero is passed an <see cref="T:System.ArgumentOutOfRangeException" /> is thrown.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for <paramref name="keepAliveTime" /> or <paramref name="keepAliveInterval" /> parameter is less than or equal to 0.</exception>
		// Token: 0x06002771 RID: 10097 RVA: 0x00091C33 File Offset: 0x0008FE33
		public static void SetTcpKeepAlive(bool enabled, int keepAliveTime, int keepAliveInterval)
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
			ServicePointManager.tcp_keepalive = enabled;
			ServicePointManager.tcp_keepalive_time = keepAliveTime;
			ServicePointManager.tcp_keepalive_interval = keepAliveInterval;
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">The <see cref="T:System.Uri" /> object of the Internet resource to contact. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002772 RID: 10098 RVA: 0x00091C72 File Offset: 0x0008FE72
		public static ServicePoint FindServicePoint(Uri address)
		{
			return ServicePointManager.FindServicePoint(address, null);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified Uniform Resource Identifier (URI).</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="uriString">The URI of the Internet resource to be contacted. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.UriFormatException">The URI specified in <paramref name="uriString" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002773 RID: 10099 RVA: 0x00091C7B File Offset: 0x0008FE7B
		public static ServicePoint FindServicePoint(string uriString, IWebProxy proxy)
		{
			return ServicePointManager.FindServicePoint(new Uri(uriString), proxy);
		}

		/// <summary>Finds an existing <see cref="T:System.Net.ServicePoint" /> object or creates a new <see cref="T:System.Net.ServicePoint" /> object to manage communications with the specified <see cref="T:System.Uri" /> object.</summary>
		/// <returns>The <see cref="T:System.Net.ServicePoint" /> object that manages communications for the request.</returns>
		/// <param name="address">A <see cref="T:System.Uri" /> object that contains the address of the Internet resource to contact. </param>
		/// <param name="proxy">The proxy data for this request. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null. </exception>
		/// <exception cref="T:System.InvalidOperationException">The maximum number of <see cref="T:System.Net.ServicePoint" /> objects defined in <see cref="P:System.Net.ServicePointManager.MaxServicePoints" /> has been reached. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06002774 RID: 10100 RVA: 0x00091C8C File Offset: 0x0008FE8C
		public static ServicePoint FindServicePoint(Uri address, IWebProxy proxy)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			Uri uri = new Uri(address.Scheme + "://" + address.Authority);
			bool flag = false;
			bool flag2 = false;
			if (proxy != null && !proxy.IsBypassed(address))
			{
				flag = true;
				bool flag3 = address.Scheme == "https";
				address = proxy.GetProxy(address);
				if (address.Scheme != "http")
				{
					throw new NotSupportedException("Proxy scheme not supported.");
				}
				if (flag3 && address.Scheme == "http")
				{
					flag2 = true;
				}
			}
			address = new Uri(address.Scheme + "://" + address.Authority);
			ServicePointManager.SPKey spkey = new ServicePointManager.SPKey(uri, flag ? address : null, flag2);
			ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> concurrentDictionary = ServicePointManager.servicePoints;
			ServicePoint servicePoint2;
			lock (concurrentDictionary)
			{
				ServicePoint servicePoint;
				if (ServicePointManager.servicePoints.TryGetValue(spkey, out servicePoint))
				{
					servicePoint2 = servicePoint;
				}
				else
				{
					if (ServicePointManager.maxServicePoints > 0 && ServicePointManager.servicePoints.Count >= ServicePointManager.maxServicePoints)
					{
						throw new InvalidOperationException("maximum number of service points reached");
					}
					string text = address.ToString();
					int maxConnections = (int)ServicePointManager.manager.GetMaxConnections(text);
					servicePoint = new ServicePoint(spkey, address, maxConnections, ServicePointManager.maxServicePointIdleTime);
					servicePoint.Expect100Continue = ServicePointManager.expectContinue;
					servicePoint.UseNagleAlgorithm = ServicePointManager.useNagle;
					servicePoint.UsesProxy = flag;
					servicePoint.UseConnect = flag2;
					servicePoint.SetTcpKeepAlive(ServicePointManager.tcp_keepalive, ServicePointManager.tcp_keepalive_time, ServicePointManager.tcp_keepalive_interval);
					servicePoint2 = ServicePointManager.servicePoints.GetOrAdd(spkey, servicePoint);
				}
			}
			return servicePoint2;
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x00091E30 File Offset: 0x00090030
		internal static void CloseConnectionGroup(string connectionGroupName)
		{
			ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> concurrentDictionary = ServicePointManager.servicePoints;
			lock (concurrentDictionary)
			{
				foreach (ServicePoint servicePoint in ServicePointManager.servicePoints.Values)
				{
					servicePoint.CloseConnectionGroup(connectionGroupName);
				}
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x00091EA8 File Offset: 0x000900A8
		internal static void RemoveServicePoint(ServicePoint sp)
		{
			ServicePoint servicePoint;
			ServicePointManager.servicePoints.TryRemove(sp.Key, out servicePoint);
		}

		// Token: 0x040016C8 RID: 5832
		private static ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint> servicePoints = new ConcurrentDictionary<ServicePointManager.SPKey, ServicePoint>();

		// Token: 0x040016C9 RID: 5833
		private static ICertificatePolicy policy;

		// Token: 0x040016CA RID: 5834
		private static int defaultConnectionLimit = 2;

		// Token: 0x040016CB RID: 5835
		private static int maxServicePointIdleTime = 100000;

		// Token: 0x040016CC RID: 5836
		private static int maxServicePoints = 0;

		// Token: 0x040016CD RID: 5837
		private static int dnsRefreshTimeout = 120000;

		// Token: 0x040016CE RID: 5838
		private static bool _checkCRL = false;

		// Token: 0x040016CF RID: 5839
		private static SecurityProtocolType _securityProtocol = SecurityProtocolType.SystemDefault;

		// Token: 0x040016D0 RID: 5840
		private static bool expectContinue = true;

		// Token: 0x040016D1 RID: 5841
		private static bool useNagle;

		// Token: 0x040016D2 RID: 5842
		private static ServerCertValidationCallback server_cert_cb;

		// Token: 0x040016D3 RID: 5843
		private static bool tcp_keepalive;

		// Token: 0x040016D4 RID: 5844
		private static int tcp_keepalive_time;

		// Token: 0x040016D5 RID: 5845
		private static int tcp_keepalive_interval;

		/// <summary>The default number of non-persistent connections (4) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.0 or later server. This field is constant but is no longer used in the .NET Framework 2.0.</summary>
		// Token: 0x040016D6 RID: 5846
		public const int DefaultNonPersistentConnectionLimit = 4;

		/// <summary>The default number of persistent connections (2) allowed on a <see cref="T:System.Net.ServicePoint" /> object connected to an HTTP/1.1 or later server. This field is constant and is used to initialize the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property if the value of the <see cref="P:System.Net.ServicePointManager.DefaultConnectionLimit" /> property has not been set either directly or through configuration.</summary>
		// Token: 0x040016D7 RID: 5847
		public const int DefaultPersistentConnectionLimit = 2;

		// Token: 0x040016D8 RID: 5848
		private const string configKey = "system.net/connectionManagement";

		// Token: 0x040016D9 RID: 5849
		private static ConnectionManagementData manager;

		// Token: 0x020004C0 RID: 1216
		internal class SPKey
		{
			// Token: 0x06002777 RID: 10103 RVA: 0x00091EC8 File Offset: 0x000900C8
			public SPKey(Uri uri, Uri proxy, bool use_connect)
			{
				this.uri = uri;
				this.proxy = proxy;
				this.use_connect = use_connect;
			}

			// Token: 0x17000843 RID: 2115
			// (get) Token: 0x06002778 RID: 10104 RVA: 0x00091EE5 File Offset: 0x000900E5
			public Uri Uri
			{
				get
				{
					return this.uri;
				}
			}

			// Token: 0x17000844 RID: 2116
			// (get) Token: 0x06002779 RID: 10105 RVA: 0x00091EED File Offset: 0x000900ED
			public bool UseConnect
			{
				get
				{
					return this.use_connect;
				}
			}

			// Token: 0x17000845 RID: 2117
			// (get) Token: 0x0600277A RID: 10106 RVA: 0x00091EF5 File Offset: 0x000900F5
			public bool UsesProxy
			{
				get
				{
					return this.proxy != null;
				}
			}

			// Token: 0x0600277B RID: 10107 RVA: 0x00091F04 File Offset: 0x00090104
			public override int GetHashCode()
			{
				return ((23 * 31 + (this.use_connect ? 1 : 0)) * 31 + this.uri.GetHashCode()) * 31 + ((this.proxy != null) ? this.proxy.GetHashCode() : 0);
			}

			// Token: 0x0600277C RID: 10108 RVA: 0x00091F54 File Offset: 0x00090154
			public override bool Equals(object obj)
			{
				ServicePointManager.SPKey spkey = obj as ServicePointManager.SPKey;
				return obj != null && this.uri.Equals(spkey.uri) && this.use_connect == spkey.use_connect && this.UsesProxy == spkey.UsesProxy && (!this.UsesProxy || this.proxy.Equals(spkey.proxy));
			}

			// Token: 0x040016DA RID: 5850
			private Uri uri;

			// Token: 0x040016DB RID: 5851
			private Uri proxy;

			// Token: 0x040016DC RID: 5852
			private bool use_connect;
		}
	}
}
