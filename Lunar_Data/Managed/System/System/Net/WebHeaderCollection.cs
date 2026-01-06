using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Net
{
	/// <summary>Contains protocol headers associated with a request or response.</summary>
	// Token: 0x0200041B RID: 1051
	[ComVisible(true)]
	[Serializable]
	public class WebHeaderCollection : NameValueCollection, ISerializable
	{
		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x00078CD7 File Offset: 0x00076ED7
		internal string ContentLength
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[1]);
				}
				return this.m_CommonHeaders[1];
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x00078CF7 File Offset: 0x00076EF7
		internal string CacheControl
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[2]);
				}
				return this.m_CommonHeaders[2];
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06002132 RID: 8498 RVA: 0x00078D17 File Offset: 0x00076F17
		internal string ContentType
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[3]);
				}
				return this.m_CommonHeaders[3];
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06002133 RID: 8499 RVA: 0x00078D37 File Offset: 0x00076F37
		internal string Date
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[4]);
				}
				return this.m_CommonHeaders[4];
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06002134 RID: 8500 RVA: 0x00078D57 File Offset: 0x00076F57
		internal string Expires
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[5]);
				}
				return this.m_CommonHeaders[5];
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06002135 RID: 8501 RVA: 0x00078D77 File Offset: 0x00076F77
		internal string ETag
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[6]);
				}
				return this.m_CommonHeaders[6];
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06002136 RID: 8502 RVA: 0x00078D97 File Offset: 0x00076F97
		internal string LastModified
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[7]);
				}
				return this.m_CommonHeaders[7];
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06002137 RID: 8503 RVA: 0x00078DB7 File Offset: 0x00076FB7
		internal string Location
		{
			get
			{
				return WebHeaderCollection.HeaderEncoding.DecodeUtf8FromString((this.m_CommonHeaders != null) ? this.m_CommonHeaders[8] : this.Get(WebHeaderCollection.s_CommonHeaderNames[8]));
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06002138 RID: 8504 RVA: 0x00078DDD File Offset: 0x00076FDD
		internal string ProxyAuthenticate
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[9]);
				}
				return this.m_CommonHeaders[9];
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06002139 RID: 8505 RVA: 0x00078DFF File Offset: 0x00076FFF
		internal string SetCookie2
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[11]);
				}
				return this.m_CommonHeaders[11];
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x00078E21 File Offset: 0x00077021
		internal string SetCookie
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[12]);
				}
				return this.m_CommonHeaders[12];
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x00078E43 File Offset: 0x00077043
		internal string Server
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[13]);
				}
				return this.m_CommonHeaders[13];
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x00078E65 File Offset: 0x00077065
		internal string Via
		{
			get
			{
				if (this.m_CommonHeaders == null)
				{
					return this.Get(WebHeaderCollection.s_CommonHeaderNames[14]);
				}
				return this.m_CommonHeaders[14];
			}
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00078E88 File Offset: 0x00077088
		private void NormalizeCommonHeaders()
		{
			if (this.m_CommonHeaders == null)
			{
				return;
			}
			for (int i = 0; i < this.m_CommonHeaders.Length; i++)
			{
				if (this.m_CommonHeaders[i] != null)
				{
					this.InnerCollection.Add(WebHeaderCollection.s_CommonHeaderNames[i], this.m_CommonHeaders[i]);
				}
			}
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x00078EE3 File Offset: 0x000770E3
		private NameValueCollection InnerCollection
		{
			get
			{
				if (this.m_InnerCollection == null)
				{
					this.m_InnerCollection = new NameValueCollection(16, CaseInsensitiveAscii.StaticInstance);
				}
				return this.m_InnerCollection;
			}
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00078F08 File Offset: 0x00077108
		internal static bool AllowMultiValues(string name)
		{
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[name];
			return headerInfo.AllowMultiValues || headerInfo.HeaderName == "";
		}

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x06002140 RID: 8512 RVA: 0x00078F3B File Offset: 0x0007713B
		private bool AllowHttpRequestHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebRequest;
				}
				return this.m_Type == WebHeaderCollectionType.WebRequest || this.m_Type == WebHeaderCollectionType.HttpWebRequest || this.m_Type == WebHeaderCollectionType.HttpListenerRequest;
			}
		}

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x06002141 RID: 8513 RVA: 0x00078F69 File Offset: 0x00077169
		internal bool AllowHttpResponseHeader
		{
			get
			{
				if (this.m_Type == WebHeaderCollectionType.Unknown)
				{
					this.m_Type = WebHeaderCollectionType.WebResponse;
				}
				return this.m_Type == WebHeaderCollectionType.WebResponse || this.m_Type == WebHeaderCollectionType.HttpWebResponse || this.m_Type == WebHeaderCollectionType.HttpListenerResponse;
			}
		}

		/// <summary>Gets or sets the specified request header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header value.</returns>
		/// <param name="header">The request header value.</param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000697 RID: 1687
		public string this[HttpRequestHeader header]
		{
			get
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpRequestHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Gets or sets the specified response header.</summary>
		/// <returns>A <see cref="T:System.String" /> instance containing the specified header.</returns>
		/// <param name="header">The response header value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000698 RID: 1688
		public string this[HttpResponseHeader header]
		{
			get
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_CommonHeaders != null)
				{
					if (header == HttpResponseHeader.ProxyAuthenticate)
					{
						return this.m_CommonHeaders[9];
					}
					if (header == HttpResponseHeader.WwwAuthenticate)
					{
						return this.m_CommonHeaders[15];
					}
				}
				return base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)];
			}
			set
			{
				if (!this.AllowHttpResponseHeader)
				{
					throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
				}
				if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
				}
				base[UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header)] = value;
			}
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x06002146 RID: 8518 RVA: 0x000790B4 File Offset: 0x000772B4
		public void Add(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Inserts the specified header with the specified value into the collection.</summary>
		/// <param name="header">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x06002147 RID: 8519 RVA: 0x000790DC File Offset: 0x000772DC
		public void Add(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.Add(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		// Token: 0x06002148 RID: 8520 RVA: 0x00079150 File Offset: 0x00077350
		public void Set(HttpRequestHeader header, string value)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> value to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		// Token: 0x06002149 RID: 8521 RVA: 0x00079178 File Offset: 0x00077378
		public void Set(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.Set(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000791EC File Offset: 0x000773EC
		internal void SetInternal(HttpResponseHeader header, string value)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.SetInternal(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header), value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpRequestHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpRequestHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600214B RID: 8523 RVA: 0x00079260 File Offset: 0x00077460
		public void Remove(HttpRequestHeader header)
		{
			if (!this.AllowHttpRequestHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds response headers and cannot contain the specified request header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.ToString((int)header));
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="header">The <see cref="T:System.Net.HttpResponseHeader" /> instance to remove from the collection. </param>
		/// <exception cref="T:System.InvalidOperationException">This <see cref="T:System.Net.WebHeaderCollection" /> instance does not allow instances of <see cref="T:System.Net.HttpResponseHeader" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600214C RID: 8524 RVA: 0x00079286 File Offset: 0x00077486
		public void Remove(HttpResponseHeader header)
		{
			if (!this.AllowHttpResponseHeader)
			{
				throw new InvalidOperationException(SR.GetString("This collection holds request headers and cannot contain the specified response header."));
			}
			this.Remove(UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.ToString((int)header));
		}

		/// <summary>Inserts a header into the collection without checking whether the header is on the restricted header list.</summary>
		/// <param name="headerName">The header to add to the collection. </param>
		/// <param name="headerValue">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="headerValue" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="headerName" /> is not null and the length of <paramref name="headerValue" /> is too long (greater than 65,535 characters).</exception>
		// Token: 0x0600214D RID: 8525 RVA: 0x000792AC File Offset: 0x000774AC
		protected void AddWithoutValidate(string headerName, string headerValue)
		{
			headerName = WebHeaderCollection.CheckBadChars(headerName, false);
			headerValue = WebHeaderCollection.CheckBadChars(headerValue, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && headerValue != null && headerValue.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("headerValue", headerValue, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(headerName, headerValue);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00079328 File Offset: 0x00077528
		internal void SetAddVerified(string name, string value)
		{
			if (WebHeaderCollection.HInfo[name].AllowMultiValues)
			{
				this.NormalizeCommonHeaders();
				base.InvalidateCachedArrays();
				this.InnerCollection.Add(name, value);
				return;
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0007937A File Offset: 0x0007757A
		internal void AddInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x00079395 File Offset: 0x00077595
		internal void ChangeInternal(string name, string value)
		{
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x000793B0 File Offset: 0x000775B0
		internal void RemoveInternal(string name)
		{
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x000793D2 File Offset: 0x000775D2
		internal void CheckUpdate(string name, string value)
		{
			value = WebHeaderCollection.CheckBadChars(value, true);
			this.ChangeInternal(name, value);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000793E5 File Offset: 0x000775E5
		private void AddInternalNotCommon(string name, string value)
		{
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x000793FC File Offset: 0x000775FC
		internal static string CheckBadChars(string name, bool isHeaderValue)
		{
			if (name != null && name.Length != 0)
			{
				if (isHeaderValue)
				{
					name = name.Trim(WebHeaderCollection.HttpTrimCharacters);
					int num = 0;
					for (int i = 0; i < name.Length; i++)
					{
						char c = 'ÿ' & name[i];
						switch (num)
						{
						case 0:
							if (c == '\r')
							{
								num = 1;
							}
							else if (c == '\n')
							{
								num = 2;
							}
							else if (c == '\u007f' || (c < ' ' && c != '\t'))
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid Control characters."), "value");
							}
							break;
						case 1:
							if (c != '\n')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 2;
							break;
						case 2:
							if (c != ' ' && c != '\t')
							{
								throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
							}
							num = 0;
							break;
						}
					}
					if (num != 0)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid CRLF characters."), "value");
					}
				}
				else
				{
					if (name.IndexOfAny(ValidationHelper.InvalidParamChars) != -1)
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid HTTP Header characters."), "name");
					}
					if (WebHeaderCollection.ContainsNonAsciiChars(name))
					{
						throw new ArgumentException(SR.GetString("Specified value has invalid non-ASCII characters."), "name");
					}
				}
				return name;
			}
			if (!isHeaderValue)
			{
				throw (name == null) ? new ArgumentNullException("name") : new ArgumentException(SR.GetString("The parameter '{0}' cannot be an empty string.", new object[] { "name" }), "name");
			}
			return string.Empty;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0007956E File Offset: 0x0007776E
		internal static bool IsValidToken(string token)
		{
			return token.Length > 0 && token.IndexOfAny(ValidationHelper.InvalidParamChars) == -1 && !WebHeaderCollection.ContainsNonAsciiChars(token);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00079594 File Offset: 0x00077794
		internal static bool ContainsNonAsciiChars(string token)
		{
			for (int i = 0; i < token.Length; i++)
			{
				if (token[i] < ' ' || token[i] > '~')
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000795CC File Offset: 0x000777CC
		internal void ThrowOnRestrictedHeader(string headerName)
		{
			if (this.m_Type == WebHeaderCollectionType.HttpWebRequest)
			{
				if (WebHeaderCollection.HInfo[headerName].IsRequestRestricted)
				{
					throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
				}
			}
			else if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && WebHeaderCollection.HInfo[headerName].IsResponseRestricted)
			{
				throw new ArgumentException(SR.GetString("The '{0}' header must be modified using the appropriate property or method.", new object[] { headerName }), "name");
			}
		}

		/// <summary>Inserts a header with the specified name and value into the collection.</summary>
		/// <param name="name">The header to add to the collection. </param>
		/// <param name="value">The content of the header. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is null, <see cref="F:System.String.Empty" />, or contains invalid characters.-or- <paramref name="name" /> is a restricted header that must be set with a property setting.-or- <paramref name="value" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		// Token: 0x06002158 RID: 8536 RVA: 0x00079650 File Offset: 0x00077850
		public override void Add(string name, string value)
		{
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(name, value);
		}

		/// <summary>Inserts the specified header into the collection.</summary>
		/// <param name="header">The header to add, with the name and value separated by a colon. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="header" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="header" /> does not contain a colon (:) character.The length of <paramref name="value" /> is greater than 65535.-or- The name part of <paramref name="header" /> is <see cref="F:System.String.Empty" /> or contains invalid characters.-or- <paramref name="header" /> is a restricted header that should be set with a property.-or- The value part of <paramref name="header" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length the string after the colon (:) is greater than 65535. </exception>
		// Token: 0x06002159 RID: 8537 RVA: 0x000796D4 File Offset: 0x000778D4
		public void Add(string header)
		{
			if (ValidationHelper.IsBlankString(header))
			{
				throw new ArgumentNullException("header");
			}
			int num = header.IndexOf(':');
			if (num < 0)
			{
				throw new ArgumentException(SR.GetString("Specified value does not have a ':' separator."), "header");
			}
			string text = header.Substring(0, num);
			string text2 = header.Substring(num + 1);
			text = WebHeaderCollection.CheckBadChars(text, false);
			this.ThrowOnRestrictedHeader(text);
			text2 = WebHeaderCollection.CheckBadChars(text2, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && text2 != null && text2.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", text2, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Add(text, text2);
		}

		/// <summary>Sets the specified header to the specified value.</summary>
		/// <param name="name">The header to set. </param>
		/// <param name="value">The content of the header to set. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length of <paramref name="value" /> is greater than 65535. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> or <paramref name="value" /> contain invalid characters. </exception>
		// Token: 0x0600215A RID: 8538 RVA: 0x0007979C File Offset: 0x0007799C
		public override void Set(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.ThrowOnRestrictedHeader(name);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x00079830 File Offset: 0x00077A30
		internal void SetInternal(string name, string value)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			name = WebHeaderCollection.CheckBadChars(name, false);
			value = WebHeaderCollection.CheckBadChars(value, true);
			if (this.m_Type == WebHeaderCollectionType.HttpListenerResponse && value != null && value.Length > 65535)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.GetString("Header values cannot be longer than {0} characters.", new object[] { ushort.MaxValue }));
			}
			this.NormalizeCommonHeaders();
			base.InvalidateCachedArrays();
			this.InnerCollection.Set(name, value);
		}

		/// <summary>Removes the specified header from the collection.</summary>
		/// <param name="name">The name of the header to remove from the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="name" /> is null<see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="name" /> is a restricted header.-or- <paramref name="name" /> contains invalid characters. </exception>
		// Token: 0x0600215C RID: 8540 RVA: 0x000798C0 File Offset: 0x00077AC0
		public override void Remove(string name)
		{
			if (ValidationHelper.IsBlankString(name))
			{
				throw new ArgumentNullException("name");
			}
			this.ThrowOnRestrictedHeader(name);
			name = WebHeaderCollection.CheckBadChars(name, false);
			this.NormalizeCommonHeaders();
			if (this.m_InnerCollection != null)
			{
				base.InvalidateCachedArrays();
				this.m_InnerCollection.Remove(name);
			}
		}

		/// <summary>Gets an array of header values stored in a header.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="header">The header to return. </param>
		// Token: 0x0600215D RID: 8541 RVA: 0x00079910 File Offset: 0x00077B10
		public override string[] GetValues(string header)
		{
			this.NormalizeCommonHeaders();
			HeaderInfo headerInfo = WebHeaderCollection.HInfo[header];
			string[] values = this.InnerCollection.GetValues(header);
			if (headerInfo == null || values == null || !headerInfo.AllowMultiValues)
			{
				return values;
			}
			ArrayList arrayList = null;
			for (int i = 0; i < values.Length; i++)
			{
				string[] array = headerInfo.Parser(values[i]);
				if (arrayList == null)
				{
					if (array.Length > 1)
					{
						arrayList = new ArrayList(values);
						arrayList.RemoveRange(i, values.Length - i);
						arrayList.AddRange(array);
					}
				}
				else
				{
					arrayList.AddRange(array);
				}
			}
			if (arrayList != null)
			{
				string[] array2 = new string[arrayList.Count];
				arrayList.CopyTo(array2);
				return array2;
			}
			return values;
		}

		/// <summary>This method is obsolete.</summary>
		/// <returns>The <see cref="T:System.String" /> representation of the collection.</returns>
		// Token: 0x0600215E RID: 8542 RVA: 0x000799BA File Offset: 0x00077BBA
		public override string ToString()
		{
			return WebHeaderCollection.GetAsString(this, false, false);
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x000799C4 File Offset: 0x00077BC4
		internal string ToString(bool forTrace)
		{
			return WebHeaderCollection.GetAsString(this, false, true);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x000799D0 File Offset: 0x00077BD0
		internal static string GetAsString(NameValueCollection cc, bool winInetCompat, bool forTrace)
		{
			if (winInetCompat)
			{
				throw new InvalidOperationException();
			}
			if (cc == null || cc.Count == 0)
			{
				return "\r\n";
			}
			StringBuilder stringBuilder = new StringBuilder(30 * cc.Count);
			string text = cc[string.Empty];
			if (text != null)
			{
				stringBuilder.Append(text).Append("\r\n");
			}
			for (int i = 0; i < cc.Count; i++)
			{
				string key = cc.GetKey(i);
				string text2 = cc.Get(i);
				if (!ValidationHelper.IsBlankString(key))
				{
					stringBuilder.Append(key);
					if (winInetCompat)
					{
						stringBuilder.Append(':');
					}
					else
					{
						stringBuilder.Append(": ");
					}
					stringBuilder.Append(text2).Append("\r\n");
				}
			}
			if (!forTrace)
			{
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

		/// <summary>Converts the <see cref="T:System.Net.WebHeaderCollection" /> to a byte array..</summary>
		/// <returns>A <see cref="T:System.Byte" /> array holding the header collection.</returns>
		// Token: 0x06002161 RID: 8545 RVA: 0x00079A9B File Offset: 0x00077C9B
		public byte[] ToByteArray()
		{
			return WebHeaderCollection.HeaderEncoding.GetBytes(this.ToString());
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request.</summary>
		/// <returns>true if the header is restricted; otherwise false.</returns>
		/// <param name="headerName">The header to test. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x06002162 RID: 8546 RVA: 0x00079AA8 File Offset: 0x00077CA8
		public static bool IsRestricted(string headerName)
		{
			return WebHeaderCollection.IsRestricted(headerName, false);
		}

		/// <summary>Tests whether the specified HTTP header can be set for the request or the response.</summary>
		/// <returns>true if the header is restricted; otherwise, false.</returns>
		/// <param name="headerName">The header to test.</param>
		/// <param name="response">Does the Framework test the response or the request?</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is null or <see cref="F:System.String.Empty" />. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		// Token: 0x06002163 RID: 8547 RVA: 0x00079AB1 File Offset: 0x00077CB1
		public static bool IsRestricted(string headerName, bool response)
		{
			if (!response)
			{
				return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsRequestRestricted;
			}
			return WebHeaderCollection.HInfo[WebHeaderCollection.CheckBadChars(headerName, false)].IsResponseRestricted;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class.</summary>
		// Token: 0x06002164 RID: 8548 RVA: 0x00079AE3 File Offset: 0x00077CE3
		public WebHeaderCollection()
			: base(DBNull.Value)
		{
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00079AF0 File Offset: 0x00077CF0
		internal WebHeaderCollection(WebHeaderCollectionType type)
			: base(DBNull.Value)
		{
			this.m_Type = type;
			if (type == WebHeaderCollectionType.HttpWebResponse)
			{
				this.m_CommonHeaders = new string[WebHeaderCollection.s_CommonHeaderNames.Length - 1];
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00079B1C File Offset: 0x00077D1C
		internal WebHeaderCollection(NameValueCollection cc)
			: base(DBNull.Value)
		{
			this.m_InnerCollection = new NameValueCollection(cc.Count + 2, CaseInsensitiveAscii.StaticInstance);
			int count = cc.Count;
			for (int i = 0; i < count; i++)
			{
				string key = cc.GetKey(i);
				string[] values = cc.GetValues(i);
				if (values != null)
				{
					for (int j = 0; j < values.Length; j++)
					{
						this.InnerCollection.Add(key, values[j]);
					}
				}
				else
				{
					this.InnerCollection.Add(key, null);
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.WebHeaderCollection" /> class from the specified instances of the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> and <see cref="T:System.Runtime.Serialization.StreamingContext" /> classes.</summary>
		/// <param name="serializationInfo">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> containing the information required to serialize the <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> containing the source of the serialized stream associated with the new <see cref="T:System.Net.WebHeaderCollection" />. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="headerName" /> contains invalid characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="headerName" /> is a null reference or <see cref="F:System.String.Empty" />. </exception>
		// Token: 0x06002167 RID: 8551 RVA: 0x00079BA4 File Offset: 0x00077DA4
		protected WebHeaderCollection(SerializationInfo serializationInfo, StreamingContext streamingContext)
			: base(DBNull.Value)
		{
			int @int = serializationInfo.GetInt32("Count");
			this.m_InnerCollection = new NameValueCollection(@int + 2, CaseInsensitiveAscii.StaticInstance);
			for (int i = 0; i < @int; i++)
			{
				string @string = serializationInfo.GetString(i.ToString(NumberFormatInfo.InvariantInfo));
				string string2 = serializationInfo.GetString((i + @int).ToString(NumberFormatInfo.InvariantInfo));
				this.InnerCollection.Add(@string, string2);
			}
		}

		/// <summary>Implements the <see cref="T:System.Runtime.Serialization.ISerializable" /> interface and raises the deserialization event when the deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event.</param>
		// Token: 0x06002168 RID: 8552 RVA: 0x00003917 File Offset: 0x00001B17
		public override void OnDeserialization(object sender)
		{
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data needed to serialize the target object.</summary>
		/// <param name="serializationInfo">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="streamingContext">A <see cref="T:System.Runtime.Serialization.StreamingContext" /> that specifies the destination for this serialization.</param>
		// Token: 0x06002169 RID: 8553 RVA: 0x00079C20 File Offset: 0x00077E20
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.NormalizeCommonHeaders();
			serializationInfo.AddValue("Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				serializationInfo.AddValue(i.ToString(NumberFormatInfo.InvariantInfo), this.GetKey(i));
				serializationInfo.AddValue((i + this.Count).ToString(NumberFormatInfo.InvariantInfo), this.Get(i));
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x00079C90 File Offset: 0x00077E90
		internal unsafe DataParseStatus ParseHeaders(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			byte* ptr;
			if (buffer == null || buffer.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &buffer[0];
			}
			if (buffer.Length < size)
			{
				return DataParseStatus.NeedMoreData;
			}
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int i = unparsed;
			int num4 = totalResponseHeadersLength;
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			for (;;)
			{
				string text = string.Empty;
				string text2 = string.Empty;
				bool flag = false;
				string text3 = null;
				if (this.Count == 0)
				{
					while (i < size)
					{
						char c = (char)ptr[i];
						if (c != ' ' && c != '\t')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_6;
						}
					}
					if (i == size)
					{
						goto Block_7;
					}
				}
				int num5 = i;
				while (i < size)
				{
					char c = (char)ptr[i];
					if (c != ':' && c != '\n')
					{
						if (c > ' ')
						{
							num = i;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_12;
						}
					}
					else
					{
						if (c != ':')
						{
							break;
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_15;
						}
						break;
					}
				}
				if (i == size)
				{
					goto Block_16;
				}
				int num6;
				for (;;)
				{
					num6 = ((this.Count == 0 && num < 0) ? 1 : 0);
					char c;
					while (i < size && num6 < 2)
					{
						c = (char)ptr[i];
						if (c > ' ')
						{
							break;
						}
						if (c == '\n')
						{
							num6++;
							if (num6 == 1)
							{
								if (i + 1 == size)
								{
									goto Block_21;
								}
								flag = ptr[i + 1] == 32 || ptr[i + 1] == 9;
							}
						}
						i++;
						if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
						{
							goto Block_24;
						}
					}
					if (num6 != 2 && (num6 != 1 || flag))
					{
						if (i == size)
						{
							goto Block_28;
						}
						num2 = i;
						while (i < size)
						{
							c = (char)ptr[i];
							if (c == '\n')
							{
								break;
							}
							if (c > ' ')
							{
								num3 = i;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_32;
							}
						}
						if (i == size)
						{
							goto Block_33;
						}
						num6 = 0;
						while (i < size && num6 < 2)
						{
							c = (char)ptr[i];
							if (c != '\r' && c != '\n')
							{
								break;
							}
							if (c == '\n')
							{
								num6++;
							}
							i++;
							if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
							{
								goto Block_37;
							}
						}
						if (i == size && num6 < 2)
						{
							goto Block_40;
						}
					}
					if (num2 >= 0 && num2 > num && num3 >= num2)
					{
						text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num2, num3 - num2 + 1);
					}
					text3 = ((text3 == null) ? text2 : (text3 + " " + text2));
					if (i >= size || num6 != 1)
					{
						break;
					}
					c = (char)ptr[i];
					if (c != ' ' && c != '\t')
					{
						break;
					}
					i++;
					if (maximumResponseHeadersLength >= 0 && ++num4 >= maximumResponseHeadersLength)
					{
						goto Block_49;
					}
				}
				if (num5 >= 0 && num >= num5)
				{
					text = WebHeaderCollection.HeaderEncoding.GetString(ptr + num5, num - num5 + 1);
				}
				if (text.Length > 0)
				{
					this.AddInternal(text, text3);
				}
				totalResponseHeadersLength = num4;
				unparsed = i;
				if (num6 == 2)
				{
					goto Block_53;
				}
			}
			Block_6:
			DataParseStatus dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_7:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_12:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_15:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_16:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_21:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_24:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_28:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_32:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_33:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_37:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_40:
			dataParseStatus = DataParseStatus.NeedMoreData;
			goto IL_030A;
			Block_49:
			dataParseStatus = DataParseStatus.DataTooBig;
			goto IL_030A;
			Block_53:
			dataParseStatus = DataParseStatus.Done;
			IL_030A:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x00079FC0 File Offset: 0x000781C0
		internal unsafe DataParseStatus ParseHeadersStrict(byte[] buffer, int size, ref int unparsed, ref int totalResponseHeadersLength, int maximumResponseHeadersLength, ref WebParseError parseError)
		{
			WebParseErrorCode webParseErrorCode = WebParseErrorCode.Generic;
			DataParseStatus dataParseStatus = DataParseStatus.Invalid;
			int num = unparsed;
			int num2 = ((maximumResponseHeadersLength <= 0) ? int.MaxValue : (maximumResponseHeadersLength - totalResponseHeadersLength + num));
			DataParseStatus dataParseStatus2 = DataParseStatus.DataTooBig;
			if (size < num2)
			{
				num2 = size;
				dataParseStatus2 = DataParseStatus.NeedMoreData;
			}
			if (num >= num2)
			{
				dataParseStatus = dataParseStatus2;
			}
			else
			{
				try
				{
					fixed (byte[] array = buffer)
					{
						byte* ptr;
						if (buffer == null || array.Length == 0)
						{
							ptr = null;
						}
						else
						{
							ptr = &array[0];
						}
						while (ptr[num] != 13)
						{
							int num3 = num;
							while (num < num2 && ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]]) == WebHeaderCollection.RfcChar.Reg)
							{
								num++;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							if (num == num3)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.InvalidHeaderName;
								goto IL_0416;
							}
							int num4 = num - 1;
							int num5 = 0;
							WebHeaderCollection.RfcChar rfcChar;
							while (num < num2 && (rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) != WebHeaderCollection.RfcChar.Colon)
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_011D;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_011D;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_011D;
									}
									num5 = 0;
									break;
								default:
									goto IL_011D;
								}
								num++;
								continue;
								IL_011D:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_0416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							if (num5 != 0)
							{
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.IncompleteHeaderLine;
								goto IL_0416;
							}
							if (++num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							int num6 = -1;
							int num7 = -1;
							StringBuilder stringBuilder = null;
							while (num < num2 && ((rfcChar = ((ptr[num] > 127) ? WebHeaderCollection.RfcChar.High : WebHeaderCollection.RfcCharMap[(int)ptr[num]])) == WebHeaderCollection.RfcChar.WS || num5 != 2))
							{
								switch (rfcChar)
								{
								case WebHeaderCollection.RfcChar.High:
								case WebHeaderCollection.RfcChar.Reg:
								case WebHeaderCollection.RfcChar.Colon:
								case WebHeaderCollection.RfcChar.Delim:
									if (num5 == 1)
									{
										goto IL_023E;
									}
									if (num5 == 3)
									{
										num5 = 0;
										if (num6 != -1)
										{
											string @string = WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1);
											if (stringBuilder == null)
											{
												stringBuilder = new StringBuilder(@string, @string.Length * 5);
											}
											else
											{
												stringBuilder.Append(" ");
												stringBuilder.Append(@string);
											}
										}
										num6 = -1;
									}
									if (num6 == -1)
									{
										num6 = num;
									}
									num7 = num;
									break;
								case WebHeaderCollection.RfcChar.Ctl:
									goto IL_023E;
								case WebHeaderCollection.RfcChar.CR:
									if (num5 != 0)
									{
										goto IL_023E;
									}
									num5 = 1;
									break;
								case WebHeaderCollection.RfcChar.LF:
									if (num5 != 1)
									{
										goto IL_023E;
									}
									num5 = 2;
									break;
								case WebHeaderCollection.RfcChar.WS:
									if (num5 == 1)
									{
										goto IL_023E;
									}
									if (num5 == 2)
									{
										num5 = 3;
									}
									break;
								default:
									goto IL_023E;
								}
								num++;
								continue;
								IL_023E:
								dataParseStatus = DataParseStatus.Invalid;
								webParseErrorCode = WebParseErrorCode.CrLfError;
								goto IL_0416;
							}
							if (num == num2)
							{
								dataParseStatus = dataParseStatus2;
								goto IL_0416;
							}
							string text = ((num6 == -1) ? "" : WebHeaderCollection.HeaderEncoding.GetString(ptr + num6, num7 - num6 + 1));
							if (stringBuilder != null)
							{
								if (text.Length != 0)
								{
									stringBuilder.Append(" ");
									stringBuilder.Append(text);
								}
								text = stringBuilder.ToString();
							}
							string text2 = null;
							int num8 = num4 - num3 + 1;
							if (this.m_CommonHeaders != null)
							{
								int num9 = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(ptr[num3] & 31)];
								if (num9 >= 0)
								{
									string text3;
									for (;;)
									{
										text3 = WebHeaderCollection.s_CommonHeaderNames[num9++];
										if (text3.Length < num8 || CaseInsensitiveAscii.AsciiToLower[(int)ptr[num3]] != CaseInsensitiveAscii.AsciiToLower[(int)text3[0]])
										{
											goto IL_03E3;
										}
										if (text3.Length <= num8)
										{
											byte* ptr2 = ptr + num3 + 1;
											int num10 = 1;
											while (num10 < text3.Length && ((char)(*(ptr2++)) == text3[num10] || CaseInsensitiveAscii.AsciiToLower[(int)(*(ptr2 - 1))] == CaseInsensitiveAscii.AsciiToLower[(int)text3[num10]]))
											{
												num10++;
											}
											if (num10 == text3.Length)
											{
												break;
											}
										}
									}
									this.m_NumCommonHeaders++;
									num9--;
									if (this.m_CommonHeaders[num9] == null)
									{
										this.m_CommonHeaders[num9] = text;
									}
									else
									{
										this.NormalizeCommonHeaders();
										this.AddInternalNotCommon(text3, text);
									}
									text2 = text3;
								}
							}
							IL_03E3:
							if (text2 == null)
							{
								text2 = WebHeaderCollection.HeaderEncoding.GetString(ptr + num3, num8);
								this.AddInternalNotCommon(text2, text);
							}
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
						}
						if (++num == num2)
						{
							dataParseStatus = dataParseStatus2;
						}
						else if (ptr[num++] == 10)
						{
							totalResponseHeadersLength += num - unparsed;
							unparsed = num;
							dataParseStatus = DataParseStatus.Done;
						}
						else
						{
							dataParseStatus = DataParseStatus.Invalid;
							webParseErrorCode = WebParseErrorCode.CrLfError;
						}
					}
				}
				finally
				{
					byte[] array = null;
				}
			}
			IL_0416:
			if (dataParseStatus == DataParseStatus.Invalid)
			{
				parseError.Section = WebParseErrorSection.ResponseHeader;
				parseError.Code = webParseErrorCode;
			}
			return dataParseStatus;
		}

		/// <summary>Serializes this instance into the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object.</summary>
		/// <param name="serializationInfo">The object into which this <see cref="T:System.Net.WebHeaderCollection" /> will be serialized. </param>
		/// <param name="streamingContext">The destination of the serialization. </param>
		// Token: 0x0600216C RID: 8556 RVA: 0x0007A414 File Offset: 0x00078614
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter, SerializationFormatter = true)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		/// <summary>Get the value of a particular header in the collection, specified by the name of the header.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the value of the specified header.</returns>
		/// <param name="name">The name of the Web header.</param>
		// Token: 0x0600216D RID: 8557 RVA: 0x0007A420 File Offset: 0x00078620
		public override string Get(string name)
		{
			if (this.m_CommonHeaders != null && name != null && name.Length > 0 && name[0] < 'Ā')
			{
				int num = (int)WebHeaderCollection.s_CommonHeaderHints[(int)(name[0] & '\u001f')];
				if (num >= 0)
				{
					for (;;)
					{
						string text = WebHeaderCollection.s_CommonHeaderNames[num++];
						if (text.Length < name.Length || CaseInsensitiveAscii.AsciiToLower[(int)name[0]] != CaseInsensitiveAscii.AsciiToLower[(int)text[0]])
						{
							goto IL_00EF;
						}
						if (text.Length <= name.Length)
						{
							int num2 = 1;
							while (num2 < text.Length && (name[num2] == text[num2] || (name[num2] <= 'ÿ' && CaseInsensitiveAscii.AsciiToLower[(int)name[num2]] == CaseInsensitiveAscii.AsciiToLower[(int)text[num2]])))
							{
								num2++;
							}
							if (num2 == text.Length)
							{
								break;
							}
						}
					}
					return this.m_CommonHeaders[num - 1];
				}
			}
			IL_00EF:
			if (this.m_InnerCollection == null)
			{
				return null;
			}
			return this.m_InnerCollection.Get(name);
		}

		/// <summary>Returns an enumerator that can iterate through the <see cref="T:System.Net.WebHeaderCollection" /> instance.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Net.WebHeaderCollection" />.</returns>
		// Token: 0x0600216E RID: 8558 RVA: 0x0007A532 File Offset: 0x00078732
		public override IEnumerator GetEnumerator()
		{
			this.NormalizeCommonHeaders();
			return new NameObjectCollectionBase.NameObjectKeysEnumerator(this.InnerCollection);
		}

		/// <summary>Gets the number of headers in the collection.</summary>
		/// <returns>An <see cref="T:System.Int32" /> indicating the number of headers in a request.</returns>
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x0007A545 File Offset: 0x00078745
		public override int Count
		{
			get
			{
				return ((this.m_InnerCollection == null) ? 0 : this.m_InnerCollection.Count) + this.m_NumCommonHeaders;
			}
		}

		/// <summary>Gets the collection of header names (keys) in the collection.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> containing all header names in a Web request.</returns>
		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x0007A564 File Offset: 0x00078764
		public override NameObjectCollectionBase.KeysCollection Keys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.Keys;
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0007A577 File Offset: 0x00078777
		internal override bool InternalHasKeys()
		{
			this.NormalizeCommonHeaders();
			return this.m_InnerCollection != null && this.m_InnerCollection.HasKeys();
		}

		/// <summary>Get the value of a particular header in the collection, specified by an index into the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of the specified header.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06002172 RID: 8562 RVA: 0x0007A594 File Offset: 0x00078794
		public override string Get(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.Get(index);
		}

		/// <summary>Gets an array of header values stored in the <paramref name="index" /> position of the header collection.</summary>
		/// <returns>An array of header strings.</returns>
		/// <param name="index">The header index to return.</param>
		// Token: 0x06002173 RID: 8563 RVA: 0x0007A5A8 File Offset: 0x000787A8
		public override string[] GetValues(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetValues(index);
		}

		/// <summary>Get the header name at the specified position in the collection.</summary>
		/// <returns>A <see cref="T:System.String" /> holding the header name.</returns>
		/// <param name="index">The zero-based index of the key to get from the collection.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is negative. -or-<paramref name="index" /> exceeds the size of the collection.</exception>
		// Token: 0x06002174 RID: 8564 RVA: 0x0007A5BC File Offset: 0x000787BC
		public override string GetKey(int index)
		{
			this.NormalizeCommonHeaders();
			return this.InnerCollection.GetKey(index);
		}

		/// <summary>Gets all header names (keys) in the collection.</summary>
		/// <returns>An array of type <see cref="T:System.String" /> containing all header names in a Web request.</returns>
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x06002175 RID: 8565 RVA: 0x0007A5D0 File Offset: 0x000787D0
		public override string[] AllKeys
		{
			get
			{
				this.NormalizeCommonHeaders();
				return this.InnerCollection.AllKeys;
			}
		}

		/// <summary>Removes all headers from the collection.</summary>
		// Token: 0x06002176 RID: 8566 RVA: 0x0007A5E3 File Offset: 0x000787E3
		public override void Clear()
		{
			this.m_CommonHeaders = null;
			this.m_NumCommonHeaders = 0;
			base.InvalidateCachedArrays();
			if (this.m_InnerCollection != null)
			{
				this.m_InnerCollection.Clear();
			}
		}

		// Token: 0x0400133A RID: 4922
		private const int ApproxAveHeaderLineSize = 30;

		// Token: 0x0400133B RID: 4923
		private const int ApproxHighAvgNumHeaders = 16;

		// Token: 0x0400133C RID: 4924
		private static readonly HeaderInfoTable HInfo = new HeaderInfoTable();

		// Token: 0x0400133D RID: 4925
		private string[] m_CommonHeaders;

		// Token: 0x0400133E RID: 4926
		private int m_NumCommonHeaders;

		// Token: 0x0400133F RID: 4927
		private static readonly string[] s_CommonHeaderNames = new string[]
		{
			"Accept-Ranges", "Content-Length", "Cache-Control", "Content-Type", "Date", "Expires", "ETag", "Last-Modified", "Location", "Proxy-Authenticate",
			"P3P", "Set-Cookie2", "Set-Cookie", "Server", "Via", "WWW-Authenticate", "X-AspNet-Version", "X-Powered-By", "["
		};

		// Token: 0x04001340 RID: 4928
		private static readonly sbyte[] s_CommonHeaderHints = new sbyte[]
		{
			-1, 0, -1, 1, 4, 5, -1, -1, -1, -1,
			-1, -1, 7, -1, -1, -1, 9, -1, -1, 11,
			-1, -1, 14, 15, 16, -1, -1, -1, -1, -1,
			-1, -1
		};

		// Token: 0x04001341 RID: 4929
		private const int c_AcceptRanges = 0;

		// Token: 0x04001342 RID: 4930
		private const int c_ContentLength = 1;

		// Token: 0x04001343 RID: 4931
		private const int c_CacheControl = 2;

		// Token: 0x04001344 RID: 4932
		private const int c_ContentType = 3;

		// Token: 0x04001345 RID: 4933
		private const int c_Date = 4;

		// Token: 0x04001346 RID: 4934
		private const int c_Expires = 5;

		// Token: 0x04001347 RID: 4935
		private const int c_ETag = 6;

		// Token: 0x04001348 RID: 4936
		private const int c_LastModified = 7;

		// Token: 0x04001349 RID: 4937
		private const int c_Location = 8;

		// Token: 0x0400134A RID: 4938
		private const int c_ProxyAuthenticate = 9;

		// Token: 0x0400134B RID: 4939
		private const int c_P3P = 10;

		// Token: 0x0400134C RID: 4940
		private const int c_SetCookie2 = 11;

		// Token: 0x0400134D RID: 4941
		private const int c_SetCookie = 12;

		// Token: 0x0400134E RID: 4942
		private const int c_Server = 13;

		// Token: 0x0400134F RID: 4943
		private const int c_Via = 14;

		// Token: 0x04001350 RID: 4944
		private const int c_WwwAuthenticate = 15;

		// Token: 0x04001351 RID: 4945
		private const int c_XAspNetVersion = 16;

		// Token: 0x04001352 RID: 4946
		private const int c_XPoweredBy = 17;

		// Token: 0x04001353 RID: 4947
		private NameValueCollection m_InnerCollection;

		// Token: 0x04001354 RID: 4948
		private WebHeaderCollectionType m_Type;

		// Token: 0x04001355 RID: 4949
		private static readonly char[] HttpTrimCharacters = new char[] { '\t', '\n', '\v', '\f', '\r', ' ' };

		// Token: 0x04001356 RID: 4950
		private static WebHeaderCollection.RfcChar[] RfcCharMap = new WebHeaderCollection.RfcChar[]
		{
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.LF,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.CR,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.Ctl,
			WebHeaderCollection.RfcChar.WS,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Colon,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Delim,
			WebHeaderCollection.RfcChar.Reg,
			WebHeaderCollection.RfcChar.Ctl
		};

		// Token: 0x0200041C RID: 1052
		internal static class HeaderEncoding
		{
			// Token: 0x06002178 RID: 8568 RVA: 0x0007A718 File Offset: 0x00078918
			internal unsafe static string GetString(byte[] bytes, int byteIndex, int byteCount)
			{
				byte* ptr;
				if (bytes == null || bytes.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &bytes[0];
				}
				return WebHeaderCollection.HeaderEncoding.GetString(ptr + byteIndex, byteCount);
			}

			// Token: 0x06002179 RID: 8569 RVA: 0x0007A748 File Offset: 0x00078948
			internal unsafe static string GetString(byte* pBytes, int byteCount)
			{
				if (byteCount < 1)
				{
					return "";
				}
				string text = new string('\0', byteCount);
				fixed (string text2 = text)
				{
					char* ptr = text2;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					char* ptr2 = ptr;
					while (byteCount >= 8)
					{
						*ptr2 = (char)(*pBytes);
						ptr2[1] = (char)pBytes[1];
						ptr2[2] = (char)pBytes[2];
						ptr2[3] = (char)pBytes[3];
						ptr2[4] = (char)pBytes[4];
						ptr2[5] = (char)pBytes[5];
						ptr2[6] = (char)pBytes[6];
						ptr2[7] = (char)pBytes[7];
						ptr2 += 8;
						pBytes += 8;
						byteCount -= 8;
					}
					for (int i = 0; i < byteCount; i++)
					{
						ptr2[i] = (char)pBytes[i];
					}
				}
				return text;
			}

			// Token: 0x0600217A RID: 8570 RVA: 0x0007A7FE File Offset: 0x000789FE
			internal static int GetByteCount(string myString)
			{
				return myString.Length;
			}

			// Token: 0x0600217B RID: 8571 RVA: 0x0007A808 File Offset: 0x00078A08
			internal unsafe static void GetBytes(string myString, int charIndex, int charCount, byte[] bytes, int byteIndex)
			{
				if (myString.Length == 0)
				{
					return;
				}
				fixed (byte[] array = bytes)
				{
					byte* ptr;
					if (bytes == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					byte* ptr2 = ptr + byteIndex;
					int num = charIndex + charCount;
					while (charIndex < num)
					{
						*(ptr2++) = (byte)myString[charIndex++];
					}
				}
			}

			// Token: 0x0600217C RID: 8572 RVA: 0x0007A85C File Offset: 0x00078A5C
			internal static byte[] GetBytes(string myString)
			{
				byte[] array = new byte[myString.Length];
				if (myString.Length != 0)
				{
					WebHeaderCollection.HeaderEncoding.GetBytes(myString, 0, myString.Length, array, 0);
				}
				return array;
			}

			// Token: 0x0600217D RID: 8573 RVA: 0x0007A890 File Offset: 0x00078A90
			[FriendAccessAllowed]
			internal static string DecodeUtf8FromString(string input)
			{
				if (string.IsNullOrWhiteSpace(input))
				{
					return input;
				}
				bool flag = false;
				for (int i = 0; i < input.Length; i++)
				{
					if (input[i] > 'ÿ')
					{
						return input;
					}
					if (input[i] > '\u007f')
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					byte[] array = new byte[input.Length];
					for (int j = 0; j < input.Length; j++)
					{
						if (input[j] > 'ÿ')
						{
							return input;
						}
						array[j] = (byte)input[j];
					}
					try
					{
						return Encoding.GetEncoding("utf-8", EncoderFallback.ExceptionFallback, DecoderFallback.ExceptionFallback).GetString(array);
					}
					catch (ArgumentException)
					{
					}
					return input;
				}
				return input;
			}
		}

		// Token: 0x0200041D RID: 1053
		private enum RfcChar : byte
		{
			// Token: 0x04001358 RID: 4952
			High,
			// Token: 0x04001359 RID: 4953
			Reg,
			// Token: 0x0400135A RID: 4954
			Ctl,
			// Token: 0x0400135B RID: 4955
			CR,
			// Token: 0x0400135C RID: 4956
			LF,
			// Token: 0x0400135D RID: 4957
			WS,
			// Token: 0x0400135E RID: 4958
			Colon,
			// Token: 0x0400135F RID: 4959
			Delim
		}
	}
}
