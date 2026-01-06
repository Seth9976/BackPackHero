using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Serialization;

namespace System.Net
{
	/// <summary>Provides a set of properties and methods that are used to manage cookies. This class cannot be inherited.</summary>
	// Token: 0x0200045A RID: 1114
	[Serializable]
	public sealed class Cookie
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class.</summary>
		// Token: 0x060022FB RID: 8955 RVA: 0x000802F0 File Offset: 0x0007E4F0
		public Cookie()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Value" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character. </exception>
		// Token: 0x060022FC RID: 8956 RVA: 0x00080384 File Offset: 0x0007E584
		public Cookie(string name, string value)
		{
			this.Name = name;
			this.m_value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, and <see cref="P:System.Net.Cookie.Path" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x060022FD RID: 8957 RVA: 0x00080424 File Offset: 0x0007E624
		public Cookie(string name, string value, string path)
			: this(name, value)
		{
			this.Path = path;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Cookie" /> class with a specified <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, and <see cref="P:System.Net.Cookie.Domain" />.</summary>
		/// <param name="name">The name of a <see cref="T:System.Net.Cookie" />. The following characters must not be used inside <paramref name="name" />: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character. </param>
		/// <param name="value">The value of a <see cref="T:System.Net.Cookie" /> object. The following characters must not be used inside <paramref name="value" />: semicolon, comma. </param>
		/// <param name="path">The subset of URIs on the origin server to which this <see cref="T:System.Net.Cookie" /> applies. The default value is "/". </param>
		/// <param name="domain">The optional internet domain for which this <see cref="T:System.Net.Cookie" /> is valid. The default value is the host this <see cref="T:System.Net.Cookie" /> has been received from. </param>
		/// <exception cref="T:System.Net.CookieException">The <paramref name="name" /> parameter is null. -or- The <paramref name="name" /> parameter is of zero length. -or- The <paramref name="name" /> parameter contains an invalid character.-or- The <paramref name="value" /> parameter is null .-or - The <paramref name="value" /> parameter contains a string not enclosed in quotes that contains an invalid character.</exception>
		// Token: 0x060022FE RID: 8958 RVA: 0x00080435 File Offset: 0x0007E635
		public Cookie(string name, string value, string path, string domain)
			: this(name, value, path)
		{
			this.Domain = domain;
		}

		/// <summary>Gets or sets a comment that the server can add to a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment to document intended usage for this <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x00080448 File Offset: 0x0007E648
		// (set) Token: 0x06002300 RID: 8960 RVA: 0x00080450 File Offset: 0x0007E650
		public string Comment
		{
			get
			{
				return this.m_comment;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.m_comment = value;
			}
		}

		/// <summary>Gets or sets a URI comment that the server can provide with a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>An optional comment that represents the intended usage of the URI reference for this <see cref="T:System.Net.Cookie" />. The value must conform to URI format.</returns>
		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x06002301 RID: 8961 RVA: 0x00080463 File Offset: 0x0007E663
		// (set) Token: 0x06002302 RID: 8962 RVA: 0x0008046B File Offset: 0x0007E66B
		public Uri CommentUri
		{
			get
			{
				return this.m_commentUri;
			}
			set
			{
				this.m_commentUri = value;
			}
		}

		/// <summary>Determines whether a page script or other active content can access this cookie.</summary>
		/// <returns>Boolean value that determines whether a page script or other active content can access this cookie.</returns>
		// Token: 0x170006ED RID: 1773
		// (get) Token: 0x06002303 RID: 8963 RVA: 0x00080474 File Offset: 0x0007E674
		// (set) Token: 0x06002304 RID: 8964 RVA: 0x0008047C File Offset: 0x0007E67C
		public bool HttpOnly
		{
			get
			{
				return this.m_httpOnly;
			}
			set
			{
				this.m_httpOnly = value;
			}
		}

		/// <summary>Gets or sets the discard flag set by the server.</summary>
		/// <returns>true if the client is to discard the <see cref="T:System.Net.Cookie" /> at the end of the current session; otherwise, false. The default is false.</returns>
		// Token: 0x170006EE RID: 1774
		// (get) Token: 0x06002305 RID: 8965 RVA: 0x00080485 File Offset: 0x0007E685
		// (set) Token: 0x06002306 RID: 8966 RVA: 0x0008048D File Offset: 0x0007E68D
		public bool Discard
		{
			get
			{
				return this.m_discard;
			}
			set
			{
				this.m_discard = value;
			}
		}

		/// <summary>Gets or sets the URI for which the <see cref="T:System.Net.Cookie" /> is valid.</summary>
		/// <returns>The URI for which the <see cref="T:System.Net.Cookie" /> is valid.</returns>
		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x00080496 File Offset: 0x0007E696
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x0008049E File Offset: 0x0007E69E
		public string Domain
		{
			get
			{
				return this.m_domain;
			}
			set
			{
				this.m_domain = ((value == null) ? string.Empty : value);
				this.m_domain_implicit = false;
				this.m_domainKey = string.Empty;
			}
		}

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000804C4 File Offset: 0x0007E6C4
		private string _Domain
		{
			get
			{
				if (!this.Plain && !this.m_domain_implicit && this.m_domain.Length != 0)
				{
					return "$Domain=" + (this.IsQuotedDomain ? "\"" : string.Empty) + this.m_domain + (this.IsQuotedDomain ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		// Token: 0x170006F1 RID: 1777
		// (get) Token: 0x0600230A RID: 8970 RVA: 0x0008052C File Offset: 0x0007E72C
		// (set) Token: 0x0600230B RID: 8971 RVA: 0x00080534 File Offset: 0x0007E734
		internal bool DomainImplicit
		{
			get
			{
				return this.m_domain_implicit;
			}
			set
			{
				this.m_domain_implicit = value;
			}
		}

		/// <summary>Gets or sets the current state of the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the <see cref="T:System.Net.Cookie" /> has expired; otherwise, false. The default is false.</returns>
		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x0600230C RID: 8972 RVA: 0x0008053D File Offset: 0x0007E73D
		// (set) Token: 0x0600230D RID: 8973 RVA: 0x00080568 File Offset: 0x0007E768
		public bool Expired
		{
			get
			{
				return this.m_expires != DateTime.MinValue && this.m_expires.ToLocalTime() <= DateTime.Now;
			}
			set
			{
				if (value)
				{
					this.m_expires = DateTime.Now;
				}
			}
		}

		/// <summary>Gets or sets the expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The expiration date and time for the <see cref="T:System.Net.Cookie" /> as a <see cref="T:System.DateTime" /> instance.</returns>
		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x0600230E RID: 8974 RVA: 0x00080578 File Offset: 0x0007E778
		// (set) Token: 0x0600230F RID: 8975 RVA: 0x00080580 File Offset: 0x0007E780
		public DateTime Expires
		{
			get
			{
				return this.m_expires;
			}
			set
			{
				this.m_expires = value;
			}
		}

		/// <summary>Gets or sets the name for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The name for the <see cref="T:System.Net.Cookie" />.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation is null or the empty string- or -The value specified for a set operation contained an illegal character. The following characters must not be used inside the <see cref="P:System.Net.Cookie.Name" /> property: equal sign, semicolon, comma, newline (\n), return (\r), tab (\t), and space character. The dollar sign character ("$") cannot be the first character.</exception>
		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06002310 RID: 8976 RVA: 0x00080589 File Offset: 0x0007E789
		// (set) Token: 0x06002311 RID: 8977 RVA: 0x00080591 File Offset: 0x0007E791
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				if (ValidationHelper.IsBlankString(value) || !this.InternalSetName(value))
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(value == null) ? "<null>" : value
					}));
				}
			}
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x000805D0 File Offset: 0x0007E7D0
		internal bool InternalSetName(string value)
		{
			if (ValidationHelper.IsBlankString(value) || value[0] == '$' || value.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				this.m_name = string.Empty;
				return false;
			}
			this.m_name = value;
			return true;
		}

		/// <summary>Gets or sets the URIs to which the <see cref="T:System.Net.Cookie" /> applies.</summary>
		/// <returns>The URIs to which the <see cref="T:System.Net.Cookie" /> applies.</returns>
		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00080608 File Offset: 0x0007E808
		// (set) Token: 0x06002314 RID: 8980 RVA: 0x00080610 File Offset: 0x0007E810
		public string Path
		{
			get
			{
				return this.m_path;
			}
			set
			{
				this.m_path = ((value == null) ? string.Empty : value);
				this.m_path_implicit = false;
			}
		}

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0008062A File Offset: 0x0007E82A
		private string _Path
		{
			get
			{
				if (!this.Plain && !this.m_path_implicit && this.m_path.Length != 0)
				{
					return "$Path=" + this.m_path;
				}
				return string.Empty;
			}
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x0008065F File Offset: 0x0007E85F
		internal bool Plain
		{
			get
			{
				return this.Variant == CookieVariant.Plain;
			}
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x0008066C File Offset: 0x0007E86C
		internal Cookie Clone()
		{
			Cookie cookie = new Cookie(this.m_name, this.m_value);
			if (!this.m_port_implicit)
			{
				cookie.Port = this.m_port;
			}
			if (!this.m_path_implicit)
			{
				cookie.Path = this.m_path;
			}
			cookie.Domain = this.m_domain;
			cookie.DomainImplicit = this.m_domain_implicit;
			cookie.m_timeStamp = this.m_timeStamp;
			cookie.Comment = this.m_comment;
			cookie.CommentUri = this.m_commentUri;
			cookie.HttpOnly = this.m_httpOnly;
			cookie.Discard = this.m_discard;
			cookie.Expires = this.m_expires;
			cookie.Version = this.m_version;
			cookie.Secure = this.m_secure;
			cookie.m_cookieVariant = this.m_cookieVariant;
			return cookie;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00080738 File Offset: 0x0007E938
		private static bool IsDomainEqualToHost(string domain, string host)
		{
			return host.Length + 1 == domain.Length && string.Compare(host, 0, domain, 1, host.Length, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00080760 File Offset: 0x0007E960
		internal bool VerifySetDefaults(CookieVariant variant, Uri uri, bool isLocalDomain, string localDomain, bool set_default, bool isThrow)
		{
			string host = uri.Host;
			int port = uri.Port;
			string absolutePath = uri.AbsolutePath;
			bool flag = true;
			if (set_default)
			{
				if (this.Version == 0)
				{
					variant = CookieVariant.Plain;
				}
				else if (this.Version == 1 && variant == CookieVariant.Unknown)
				{
					variant = CookieVariant.Rfc2109;
				}
				this.m_cookieVariant = variant;
			}
			if (this.m_name == null || this.m_name.Length == 0 || this.m_name[0] == '$' || this.m_name.IndexOfAny(Cookie.Reserved2Name) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Name",
						(this.m_name == null) ? "<null>" : this.m_name
					}));
				}
				return false;
			}
			else if (this.m_value == null || ((this.m_value.Length <= 2 || this.m_value[0] != '"' || this.m_value[this.m_value.Length - 1] != '"') && this.m_value.IndexOfAny(Cookie.Reserved2Value) != -1))
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
					{
						"Value",
						(this.m_value == null) ? "<null>" : this.m_value
					}));
				}
				return false;
			}
			else if (this.Comment != null && (this.Comment.Length <= 2 || this.Comment[0] != '"' || this.Comment[this.Comment.Length - 1] != '"') && this.Comment.IndexOfAny(Cookie.Reserved2Value) != -1)
			{
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Comment", this.Comment }));
				}
				return false;
			}
			else
			{
				if (this.Path == null || (this.Path.Length > 2 && this.Path[0] == '"' && this.Path[this.Path.Length - 1] == '"') || this.Path.IndexOfAny(Cookie.Reserved2Value) == -1)
				{
					if (set_default && this.m_domain_implicit)
					{
						this.m_domain = host;
					}
					else
					{
						if (!this.m_domain_implicit)
						{
							string text = this.m_domain;
							if (!Cookie.DomainCharsTest(text))
							{
								if (isThrow)
								{
									throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[]
									{
										"Domain",
										(text == null) ? "<null>" : text
									}));
								}
								return false;
							}
							else
							{
								if (text[0] != '.')
								{
									if (variant != CookieVariant.Rfc2965 && variant != CookieVariant.Plain)
									{
										if (isThrow)
										{
											throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
										}
										return false;
									}
									else
									{
										text = "." + text;
									}
								}
								int num = host.IndexOf('.');
								if (isLocalDomain && string.Compare(localDomain, text, StringComparison.OrdinalIgnoreCase) == 0)
								{
									flag = true;
								}
								else if (text.IndexOf('.', 1, text.Length - 2) == -1)
								{
									if (!Cookie.IsDomainEqualToHost(text, host))
									{
										flag = false;
									}
								}
								else if (variant == CookieVariant.Plain)
								{
									if (!Cookie.IsDomainEqualToHost(text, host) && (host.Length <= text.Length || string.Compare(host, host.Length - text.Length, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0))
									{
										flag = false;
									}
								}
								else if ((num == -1 || text.Length != host.Length - num || string.Compare(host, num, text, 0, text.Length, StringComparison.OrdinalIgnoreCase) != 0) && !Cookie.IsDomainEqualToHost(text, host))
								{
									flag = false;
								}
								if (flag)
								{
									this.m_domainKey = text.ToLower(CultureInfo.InvariantCulture);
								}
							}
						}
						else if (string.Compare(host, this.m_domain, StringComparison.OrdinalIgnoreCase) != 0)
						{
							flag = false;
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Domain", this.m_domain }));
							}
							return false;
						}
					}
					if (set_default && this.m_path_implicit)
					{
						switch (this.m_cookieVariant)
						{
						case CookieVariant.Plain:
							this.m_path = absolutePath;
							goto IL_04B8;
						case CookieVariant.Rfc2109:
							this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/'));
							goto IL_04B8;
						}
						this.m_path = absolutePath.Substring(0, absolutePath.LastIndexOf('/') + 1);
					}
					else if (!absolutePath.StartsWith(CookieParser.CheckQuoted(this.m_path)))
					{
						if (isThrow)
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.m_path }));
						}
						return false;
					}
					IL_04B8:
					if (set_default && !this.m_port_implicit && this.m_port.Length == 0)
					{
						this.m_port_list = new int[] { port };
					}
					if (!this.m_port_implicit)
					{
						flag = false;
						int[] port_list = this.m_port_list;
						for (int i = 0; i < port_list.Length; i++)
						{
							if (port_list[i] == port)
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							if (isThrow)
							{
								throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", this.m_port }));
							}
							return false;
						}
					}
					return true;
				}
				if (isThrow)
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Path", this.Path }));
				}
				return false;
			}
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x00080CB0 File Offset: 0x0007EEB0
		private static bool DomainCharsTest(string name)
		{
			if (name == null || name.Length == 0)
			{
				return false;
			}
			foreach (char c in name)
			{
				if ((c < '0' || c > '9') && c != '.' && c != '-' && (c < 'a' || c > 'z') && (c < 'A' || c > 'Z') && c != '_')
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Gets or sets a list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</summary>
		/// <returns>The list of TCP ports that the <see cref="T:System.Net.Cookie" /> applies to.</returns>
		/// <exception cref="T:System.Net.CookieException">The value specified for a set operation could not be parsed or is not enclosed in double quotes. </exception>
		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600231B RID: 8987 RVA: 0x00080D13 File Offset: 0x0007EF13
		// (set) Token: 0x0600231C RID: 8988 RVA: 0x00080D1C File Offset: 0x0007EF1C
		public string Port
		{
			get
			{
				return this.m_port;
			}
			set
			{
				this.m_port_implicit = false;
				if (value == null || value.Length == 0)
				{
					this.m_port = string.Empty;
					return;
				}
				if (value[0] != '"' || value[value.Length - 1] != '"')
				{
					throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
				}
				string[] array = value.Split(Cookie.PortSplitDelimiters);
				List<int> list = new List<int>();
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] != string.Empty)
					{
						int num;
						if (!int.TryParse(array[i], out num))
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						if (num < 0 || num > 65535)
						{
							throw new CookieException(SR.GetString("The '{0}'='{1}' part of the cookie is invalid.", new object[] { "Port", value }));
						}
						list.Add(num);
					}
				}
				this.m_port_list = list.ToArray();
				this.m_port = value;
				this.m_version = 1;
				this.m_cookieVariant = CookieVariant.Rfc2965;
			}
		}

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00080E39 File Offset: 0x0007F039
		internal int[] PortList
		{
			get
			{
				return this.m_port_list;
			}
		}

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x0600231E RID: 8990 RVA: 0x00080E41 File Offset: 0x0007F041
		private string _Port
		{
			get
			{
				if (!this.m_port_implicit)
				{
					return "$Port" + ((this.m_port.Length == 0) ? string.Empty : ("=" + this.m_port));
				}
				return string.Empty;
			}
		}

		/// <summary>Gets or sets the security level of a <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>true if the client is only to return the cookie in subsequent requests if those requests use Secure Hypertext Transfer Protocol (HTTPS); otherwise, false. The default is false.</returns>
		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600231F RID: 8991 RVA: 0x00080E7F File Offset: 0x0007F07F
		// (set) Token: 0x06002320 RID: 8992 RVA: 0x00080E87 File Offset: 0x0007F087
		public bool Secure
		{
			get
			{
				return this.m_secure;
			}
			set
			{
				this.m_secure = value;
			}
		}

		/// <summary>Gets the time when the cookie was issued as a <see cref="T:System.DateTime" />.</summary>
		/// <returns>The time when the cookie was issued as a <see cref="T:System.DateTime" />.</returns>
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002321 RID: 8993 RVA: 0x00080E90 File Offset: 0x0007F090
		public DateTime TimeStamp
		{
			get
			{
				return this.m_timeStamp;
			}
		}

		/// <summary>Gets or sets the <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</summary>
		/// <returns>The <see cref="P:System.Net.Cookie.Value" /> for the <see cref="T:System.Net.Cookie" />.</returns>
		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002322 RID: 8994 RVA: 0x00080E98 File Offset: 0x0007F098
		// (set) Token: 0x06002323 RID: 8995 RVA: 0x00080EA0 File Offset: 0x0007F0A0
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = ((value == null) ? string.Empty : value);
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002324 RID: 8996 RVA: 0x00080EB3 File Offset: 0x0007F0B3
		// (set) Token: 0x06002325 RID: 8997 RVA: 0x00080EBB File Offset: 0x0007F0BB
		internal CookieVariant Variant
		{
			get
			{
				return this.m_cookieVariant;
			}
			set
			{
				this.m_cookieVariant = value;
			}
		}

		// Token: 0x170006FF RID: 1791
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x00080EC4 File Offset: 0x0007F0C4
		internal string DomainKey
		{
			get
			{
				if (!this.m_domain_implicit)
				{
					return this.m_domainKey;
				}
				return this.Domain;
			}
		}

		/// <summary>Gets or sets the version of HTTP state maintenance to which the cookie conforms.</summary>
		/// <returns>The version of HTTP state maintenance to which the cookie conforms.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a version is not allowed. </exception>
		// Token: 0x17000700 RID: 1792
		// (get) Token: 0x06002327 RID: 8999 RVA: 0x00080EDB File Offset: 0x0007F0DB
		// (set) Token: 0x06002328 RID: 9000 RVA: 0x00080EE3 File Offset: 0x0007F0E3
		public int Version
		{
			get
			{
				return this.m_version;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.m_version = value;
				if (value > 0 && this.m_cookieVariant < CookieVariant.Rfc2109)
				{
					this.m_cookieVariant = CookieVariant.Rfc2109;
				}
			}
		}

		// Token: 0x17000701 RID: 1793
		// (get) Token: 0x06002329 RID: 9001 RVA: 0x00080F10 File Offset: 0x0007F110
		private string _Version
		{
			get
			{
				if (this.Version != 0)
				{
					return "$Version=" + (this.IsQuotedVersion ? "\"" : string.Empty) + this.m_version.ToString(NumberFormatInfo.InvariantInfo) + (this.IsQuotedVersion ? "\"" : string.Empty);
				}
				return string.Empty;
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00080F6D File Offset: 0x0007F16D
		internal static IComparer GetComparer()
		{
			return Cookie.staticComparer;
		}

		/// <summary>Overrides the <see cref="M:System.Object.Equals(System.Object)" /> method.</summary>
		/// <returns>Returns true if the <see cref="T:System.Net.Cookie" /> is equal to <paramref name="comparand" />. Two <see cref="T:System.Net.Cookie" /> instances are equal if their <see cref="P:System.Net.Cookie.Name" />, <see cref="P:System.Net.Cookie.Value" />, <see cref="P:System.Net.Cookie.Path" />, <see cref="P:System.Net.Cookie.Domain" />, and <see cref="P:System.Net.Cookie.Version" /> properties are equal. <see cref="P:System.Net.Cookie.Name" /> and <see cref="P:System.Net.Cookie.Domain" /> string comparisons are case-insensitive.</returns>
		/// <param name="comparand">A reference to a <see cref="T:System.Net.Cookie" />. </param>
		// Token: 0x0600232B RID: 9003 RVA: 0x00080F74 File Offset: 0x0007F174
		public override bool Equals(object comparand)
		{
			if (!(comparand is Cookie))
			{
				return false;
			}
			Cookie cookie = (Cookie)comparand;
			return string.Compare(this.Name, cookie.Name, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Value, cookie.Value, StringComparison.Ordinal) == 0 && string.Compare(this.Path, cookie.Path, StringComparison.Ordinal) == 0 && string.Compare(this.Domain, cookie.Domain, StringComparison.OrdinalIgnoreCase) == 0 && this.Version == cookie.Version;
		}

		/// <summary>Overrides the <see cref="M:System.Object.GetHashCode" /> method.</summary>
		/// <returns>The 32-bit signed integer hash code for this instance.</returns>
		// Token: 0x0600232C RID: 9004 RVA: 0x00080FF4 File Offset: 0x0007F1F4
		public override int GetHashCode()
		{
			return string.Concat(new string[]
			{
				this.Name,
				"=",
				this.Value,
				";",
				this.Path,
				"; ",
				this.Domain,
				"; ",
				this.Version.ToString()
			}).GetHashCode();
		}

		/// <summary>Overrides the <see cref="M:System.Object.ToString" /> method.</summary>
		/// <returns>Returns a string representation of this <see cref="T:System.Net.Cookie" /> object that is suitable for including in a HTTP Cookie: request header.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600232D RID: 9005 RVA: 0x00081068 File Offset: 0x0007F268
		public override string ToString()
		{
			string domain = this._Domain;
			string path = this._Path;
			string port = this._Port;
			string version = this._Version;
			string text = string.Concat(new string[]
			{
				(version.Length == 0) ? string.Empty : (version + "; "),
				this.Name,
				"=",
				this.Value,
				(path.Length == 0) ? string.Empty : ("; " + path),
				(domain.Length == 0) ? string.Empty : ("; " + domain),
				(port.Length == 0) ? string.Empty : ("; " + port)
			});
			if (text == "=")
			{
				return string.Empty;
			}
			return text;
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00081144 File Offset: 0x0007F344
		internal string ToServerString()
		{
			string text = this.Name + "=" + this.Value;
			if (this.m_comment != null && this.m_comment.Length > 0)
			{
				text = text + "; Comment=" + this.m_comment;
			}
			if (this.m_commentUri != null)
			{
				text = text + "; CommentURL=\"" + this.m_commentUri.ToString() + "\"";
			}
			if (this.m_discard)
			{
				text += "; Discard";
			}
			if (!this.m_domain_implicit && this.m_domain != null && this.m_domain.Length > 0)
			{
				text = text + "; Domain=" + this.m_domain;
			}
			if (this.Expires != DateTime.MinValue)
			{
				int num = (int)(this.Expires.ToLocalTime() - DateTime.Now).TotalSeconds;
				if (num < 0)
				{
					num = 0;
				}
				text = text + "; Max-Age=" + num.ToString(NumberFormatInfo.InvariantInfo);
			}
			if (!this.m_path_implicit && this.m_path != null && this.m_path.Length > 0)
			{
				text = text + "; Path=" + this.m_path;
			}
			if (!this.Plain && !this.m_port_implicit && this.m_port != null && this.m_port.Length > 0)
			{
				text = text + "; Port=" + this.m_port;
			}
			if (this.m_version > 0)
			{
				text = text + "; Version=" + this.m_version.ToString(NumberFormatInfo.InvariantInfo);
			}
			if (!(text == "="))
			{
				return text;
			}
			return null;
		}

		// Token: 0x04001460 RID: 5216
		internal const int MaxSupportedVersion = 1;

		// Token: 0x04001461 RID: 5217
		internal const string CommentAttributeName = "Comment";

		// Token: 0x04001462 RID: 5218
		internal const string CommentUrlAttributeName = "CommentURL";

		// Token: 0x04001463 RID: 5219
		internal const string DiscardAttributeName = "Discard";

		// Token: 0x04001464 RID: 5220
		internal const string DomainAttributeName = "Domain";

		// Token: 0x04001465 RID: 5221
		internal const string ExpiresAttributeName = "Expires";

		// Token: 0x04001466 RID: 5222
		internal const string MaxAgeAttributeName = "Max-Age";

		// Token: 0x04001467 RID: 5223
		internal const string PathAttributeName = "Path";

		// Token: 0x04001468 RID: 5224
		internal const string PortAttributeName = "Port";

		// Token: 0x04001469 RID: 5225
		internal const string SecureAttributeName = "Secure";

		// Token: 0x0400146A RID: 5226
		internal const string VersionAttributeName = "Version";

		// Token: 0x0400146B RID: 5227
		internal const string HttpOnlyAttributeName = "HttpOnly";

		// Token: 0x0400146C RID: 5228
		internal const string SeparatorLiteral = "; ";

		// Token: 0x0400146D RID: 5229
		internal const string EqualsLiteral = "=";

		// Token: 0x0400146E RID: 5230
		internal const string QuotesLiteral = "\"";

		// Token: 0x0400146F RID: 5231
		internal const string SpecialAttributeLiteral = "$";

		// Token: 0x04001470 RID: 5232
		internal static readonly char[] PortSplitDelimiters = new char[] { ' ', ',', '"' };

		// Token: 0x04001471 RID: 5233
		internal static readonly char[] Reserved2Name = new char[] { ' ', '\t', '\r', '\n', '=', ';', ',' };

		// Token: 0x04001472 RID: 5234
		internal static readonly char[] Reserved2Value = new char[] { ';', ',' };

		// Token: 0x04001473 RID: 5235
		private static Comparer staticComparer = new Comparer();

		// Token: 0x04001474 RID: 5236
		private string m_comment = string.Empty;

		// Token: 0x04001475 RID: 5237
		private Uri m_commentUri;

		// Token: 0x04001476 RID: 5238
		private CookieVariant m_cookieVariant = CookieVariant.Plain;

		// Token: 0x04001477 RID: 5239
		private bool m_discard;

		// Token: 0x04001478 RID: 5240
		private string m_domain = string.Empty;

		// Token: 0x04001479 RID: 5241
		private bool m_domain_implicit = true;

		// Token: 0x0400147A RID: 5242
		private DateTime m_expires = DateTime.MinValue;

		// Token: 0x0400147B RID: 5243
		private string m_name = string.Empty;

		// Token: 0x0400147C RID: 5244
		private string m_path = string.Empty;

		// Token: 0x0400147D RID: 5245
		private bool m_path_implicit = true;

		// Token: 0x0400147E RID: 5246
		private string m_port = string.Empty;

		// Token: 0x0400147F RID: 5247
		private bool m_port_implicit = true;

		// Token: 0x04001480 RID: 5248
		private int[] m_port_list;

		// Token: 0x04001481 RID: 5249
		private bool m_secure;

		// Token: 0x04001482 RID: 5250
		[OptionalField]
		private bool m_httpOnly;

		// Token: 0x04001483 RID: 5251
		private DateTime m_timeStamp = DateTime.Now;

		// Token: 0x04001484 RID: 5252
		private string m_value = string.Empty;

		// Token: 0x04001485 RID: 5253
		private int m_version;

		// Token: 0x04001486 RID: 5254
		private string m_domainKey = string.Empty;

		// Token: 0x04001487 RID: 5255
		internal bool IsQuotedVersion;

		// Token: 0x04001488 RID: 5256
		internal bool IsQuotedDomain;
	}
}
