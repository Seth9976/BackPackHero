using System;
using System.Collections;

namespace System.Net
{
	/// <summary>Provides storage for multiple credentials.</summary>
	// Token: 0x020003D6 RID: 982
	public class CredentialCache : ICredentials, ICredentialsByHost, IEnumerable
	{
		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06002055 RID: 8277 RVA: 0x000766EE File Offset: 0x000748EE
		internal bool IsDefaultInCache
		{
			get
			{
				return this.m_NumbDefaultCredInCache != 0;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance to the credential cache for use with protocols other than SMTP and associates it with a Uniform Resource Identifier (URI) prefix and authentication protocol. </summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <param name="cred">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">The same credentials are added more than once. </exception>
		// Token: 0x06002057 RID: 8279 RVA: 0x00076718 File Offset: 0x00074918
		public void Add(Uri uriPrefix, string authType, NetworkCredential cred)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			if (cred is SystemNetworkCredential)
			{
				throw new ArgumentException(SR.GetString("Default credentials cannot be supplied for the {0} authentication scheme.", new object[] { authType }), "authType");
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			this.cache.Add(credentialKey, cred);
			if (cred is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Adds a <see cref="T:System.Net.NetworkCredential" /> instance for use with SMTP to the credential cache and associates it with a host computer, port, and authentication protocol. Credentials added using this method are valid for SMTP only. This method does not work for HTTP or FTP requests.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" /> using <paramref name="cred" />. See Remarks.</param>
		/// <param name="credential">The <see cref="T:System.Net.NetworkCredential" /> to add to the credential cache. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or-<paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x06002058 RID: 8280 RVA: 0x000767A8 File Offset: 0x000749A8
		public void Add(string host, int port, string authenticationType, NetworkCredential credential)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			if (credential is SystemNetworkCredential)
			{
				throw new ArgumentException(SR.GetString("Default credentials cannot be supplied for the {0} authentication scheme.", new object[] { authenticationType }), "authenticationType");
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			this.cacheForHosts.Add(credentialHostKey, credential);
			if (credential is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache++;
			}
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified Uniform Resource Identifier (URI) prefix and authentication protocol.</summary>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential is used for. </param>
		/// <param name="authType">The authentication scheme used by the host named in <paramref name="uriPrefix" />. </param>
		// Token: 0x06002059 RID: 8281 RVA: 0x0007686C File Offset: 0x00074A6C
		public void Remove(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null || authType == null)
			{
				return;
			}
			this.m_version++;
			CredentialKey credentialKey = new CredentialKey(uriPrefix, authType);
			if (this.cache[credentialKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cache.Remove(credentialKey);
		}

		/// <summary>Deletes a <see cref="T:System.Net.NetworkCredential" /> instance from the cache if it is associated with the specified host, port, and authentication protocol.</summary>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		// Token: 0x0600205A RID: 8282 RVA: 0x000768CC File Offset: 0x00074ACC
		public void Remove(string host, int port, string authenticationType)
		{
			if (host == null || authenticationType == null)
			{
				return;
			}
			if (port < 0)
			{
				return;
			}
			this.m_version++;
			CredentialHostKey credentialHostKey = new CredentialHostKey(host, port, authenticationType);
			if (this.cacheForHosts[credentialHostKey] is SystemNetworkCredential)
			{
				this.m_NumbDefaultCredInCache--;
			}
			this.cacheForHosts.Remove(credentialHostKey);
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified Uniform Resource Identifier (URI) and authentication type.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="uriPrefix">A <see cref="T:System.Uri" /> that specifies the URI prefix of the resources that the credential grants access to. </param>
		/// <param name="authType">The authentication scheme used by the resource named in <paramref name="uriPrefix" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="uriPrefix" /> or <paramref name="authType" /> is null. </exception>
		// Token: 0x0600205B RID: 8283 RVA: 0x0007692C File Offset: 0x00074B2C
		public NetworkCredential GetCredential(Uri uriPrefix, string authType)
		{
			if (uriPrefix == null)
			{
				throw new ArgumentNullException("uriPrefix");
			}
			if (authType == null)
			{
				throw new ArgumentNullException("authType");
			}
			int num = -1;
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cache.GetEnumerator();
			while (enumerator.MoveNext())
			{
				CredentialKey credentialKey = (CredentialKey)enumerator.Key;
				if (credentialKey.Match(uriPrefix, authType))
				{
					int uriPrefixLength = credentialKey.UriPrefixLength;
					if (uriPrefixLength > num)
					{
						num = uriPrefixLength;
						networkCredential = (NetworkCredential)enumerator.Value;
					}
				}
			}
			return networkCredential;
		}

		/// <summary>Returns the <see cref="T:System.Net.NetworkCredential" /> instance associated with the specified host, port, and authentication protocol.</summary>
		/// <returns>A <see cref="T:System.Net.NetworkCredential" /> or, if there is no matching credential in the cache, null.</returns>
		/// <param name="host">A <see cref="T:System.String" /> that identifies the host computer.</param>
		/// <param name="port">A <see cref="T:System.Int32" /> that specifies the port to connect to on <paramref name="host" />.</param>
		/// <param name="authenticationType">A <see cref="T:System.String" /> that identifies the authentication scheme used when connecting to <paramref name="host" />. See Remarks.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="host" /> is null. -or- <paramref name="authType" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="authType" /> not an accepted value. See Remarks. -or-<paramref name="host" /> is equal to the empty string ("").</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="port" /> is less than zero.</exception>
		// Token: 0x0600205C RID: 8284 RVA: 0x000769A8 File Offset: 0x00074BA8
		public NetworkCredential GetCredential(string host, int port, string authenticationType)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (authenticationType == null)
			{
				throw new ArgumentNullException("authenticationType");
			}
			if (host.Length == 0)
			{
				throw new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "host" }));
			}
			if (port < 0)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			NetworkCredential networkCredential = null;
			IDictionaryEnumerator enumerator = this.cacheForHosts.GetEnumerator();
			while (enumerator.MoveNext())
			{
				if (((CredentialHostKey)enumerator.Key).Match(host, port, authenticationType))
				{
					networkCredential = (NetworkCredential)enumerator.Value;
				}
			}
			return networkCredential;
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.CredentialCache" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.CredentialCache" />.</returns>
		// Token: 0x0600205D RID: 8285 RVA: 0x00076A40 File Offset: 0x00074C40
		public IEnumerator GetEnumerator()
		{
			return new CredentialCache.CredentialEnumerator(this, this.cache, this.cacheForHosts, this.m_version);
		}

		/// <summary>Gets the system credentials of the application.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> that represents the system credentials of the application.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="USERNAME" />
		/// </PermissionSet>
		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600205E RID: 8286 RVA: 0x00076A5A File Offset: 0x00074C5A
		public static ICredentials DefaultCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}

		/// <summary>Gets the network credentials of the current security context.</summary>
		/// <returns>An <see cref="T:System.Net.NetworkCredential" /> that represents the network credentials of the current user or application.</returns>
		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x0600205F RID: 8287 RVA: 0x00076A5A File Offset: 0x00074C5A
		public static NetworkCredential DefaultNetworkCredentials
		{
			get
			{
				return SystemNetworkCredential.defaultCredential;
			}
		}

		// Token: 0x04001130 RID: 4400
		private Hashtable cache = new Hashtable();

		// Token: 0x04001131 RID: 4401
		private Hashtable cacheForHosts = new Hashtable();

		// Token: 0x04001132 RID: 4402
		internal int m_version;

		// Token: 0x04001133 RID: 4403
		private int m_NumbDefaultCredInCache;

		// Token: 0x020003D7 RID: 983
		private class CredentialEnumerator : IEnumerator
		{
			// Token: 0x06002060 RID: 8288 RVA: 0x00076A64 File Offset: 0x00074C64
			internal CredentialEnumerator(CredentialCache cache, Hashtable table, Hashtable hostTable, int version)
			{
				this.m_cache = cache;
				this.m_array = new ICredentials[table.Count + hostTable.Count];
				table.Values.CopyTo(this.m_array, 0);
				hostTable.Values.CopyTo(this.m_array, table.Count);
				this.m_version = version;
			}

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x06002061 RID: 8289 RVA: 0x00076AD0 File Offset: 0x00074CD0
			object IEnumerator.Current
			{
				get
				{
					if (this.m_index < 0 || this.m_index >= this.m_array.Length)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					if (this.m_version != this.m_cache.m_version)
					{
						throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
					}
					return this.m_array[this.m_index];
				}
			}

			// Token: 0x06002062 RID: 8290 RVA: 0x00076B38 File Offset: 0x00074D38
			bool IEnumerator.MoveNext()
			{
				if (this.m_version != this.m_cache.m_version)
				{
					throw new InvalidOperationException(SR.GetString("Collection was modified; enumeration operation may not execute."));
				}
				int num = this.m_index + 1;
				this.m_index = num;
				if (num < this.m_array.Length)
				{
					return true;
				}
				this.m_index = this.m_array.Length;
				return false;
			}

			// Token: 0x06002063 RID: 8291 RVA: 0x00076B94 File Offset: 0x00074D94
			void IEnumerator.Reset()
			{
				this.m_index = -1;
			}

			// Token: 0x04001134 RID: 4404
			private CredentialCache m_cache;

			// Token: 0x04001135 RID: 4405
			private ICredentials[] m_array;

			// Token: 0x04001136 RID: 4406
			private int m_index = -1;

			// Token: 0x04001137 RID: 4407
			private int m_version;
		}
	}
}
