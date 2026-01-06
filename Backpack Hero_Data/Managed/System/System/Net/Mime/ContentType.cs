using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Type header.</summary>
	// Token: 0x02000607 RID: 1543
	public class ContentType
	{
		/// <summary>Initializes a new default instance of the <see cref="T:System.Net.Mime.ContentType" /> class. </summary>
		// Token: 0x06003199 RID: 12697 RVA: 0x000B1A67 File Offset: 0x000AFC67
		public ContentType()
			: this("application/octet-stream")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentType" /> class using the specified string. </summary>
		/// <param name="contentType">A <see cref="T:System.String" />, for example, "text/plain; charset=us-ascii", that contains the MIME media type, subtype, and optional parameters.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="contentType" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="contentType" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="contentType" /> is in a form that cannot be parsed.</exception>
		// Token: 0x0600319A RID: 12698 RVA: 0x000B1A74 File Offset: 0x000AFC74
		public ContentType(string contentType)
		{
			if (contentType == null)
			{
				throw new ArgumentNullException("contentType");
			}
			if (contentType == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "contentType"), "contentType");
			}
			this._isChanged = true;
			this._type = contentType;
			this.ParseValue();
		}

		/// <summary>Gets or sets the value of the boundary parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the boundary parameter.</returns>
		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x0600319B RID: 12699 RVA: 0x000B1ADB File Offset: 0x000AFCDB
		// (set) Token: 0x0600319C RID: 12700 RVA: 0x000B1AED File Offset: 0x000AFCED
		public string Boundary
		{
			get
			{
				return this.Parameters["boundary"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("boundary");
					return;
				}
				this.Parameters["boundary"] = value;
			}
		}

		/// <summary>Gets or sets the value of the charset parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the charset parameter.</returns>
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600319D RID: 12701 RVA: 0x000B1B21 File Offset: 0x000AFD21
		// (set) Token: 0x0600319E RID: 12702 RVA: 0x000B1B33 File Offset: 0x000AFD33
		public string CharSet
		{
			get
			{
				return this.Parameters["charset"];
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("charset");
					return;
				}
				this.Parameters["charset"] = value;
			}
		}

		/// <summary>Gets or sets the media type value included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the media type and subtype value. This value does not include the semicolon (;) separator that follows the subtype.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">The value specified for a set operation is in a form that cannot be parsed.</exception>
		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600319F RID: 12703 RVA: 0x000B1B67 File Offset: 0x000AFD67
		// (set) Token: 0x060031A0 RID: 12704 RVA: 0x000B1B80 File Offset: 0x000AFD80
		public string MediaType
		{
			get
			{
				return this._mediaType + "/" + this._subType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("This property cannot be set to an empty string.", "value");
				}
				int num = 0;
				this._mediaType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this._mediaType.Length == 0 || num >= value.Length || value[num++] != '/')
				{
					throw new FormatException("The specified media type is invalid.");
				}
				this._subType = MailBnfHelper.ReadToken(value, ref num, null);
				if (this._subType.Length == 0 || num < value.Length)
				{
					throw new FormatException("The specified media type is invalid.");
				}
				this._isChanged = true;
				this._isPersisted = false;
			}
		}

		/// <summary>Gets or sets the value of the name parameter included in the Content-Type header represented by this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the value associated with the name parameter. </returns>
		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000B1C38 File Offset: 0x000AFE38
		// (set) Token: 0x060031A2 RID: 12706 RVA: 0x000B1C66 File Offset: 0x000AFE66
		public string Name
		{
			get
			{
				string text = this.Parameters["name"];
				if (MimeBasePart.DecodeEncoding(text) != null)
				{
					text = MimeBasePart.DecodeHeaderValue(text);
				}
				return text;
			}
			set
			{
				if (value == null || value == string.Empty)
				{
					this.Parameters.Remove("name");
					return;
				}
				this.Parameters["name"] = value;
			}
		}

		/// <summary>Gets the dictionary that contains the parameters included in the Content-Type header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains name and value pairs.</returns>
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060031A3 RID: 12707 RVA: 0x000B1C9A File Offset: 0x000AFE9A
		public StringDictionary Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060031A4 RID: 12708 RVA: 0x000B1CA2 File Offset: 0x000AFEA2
		internal void Set(string contentType, HeaderCollection headers)
		{
			this._type = contentType;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
			this._isPersisted = true;
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000B1CCA File Offset: 0x000AFECA
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this._isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this.ToString());
				this._isPersisted = true;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000B1CFD File Offset: 0x000AFEFD
		internal bool IsChanged
		{
			get
			{
				return this._isChanged || (this._parameters != null && this._parameters.IsChanged);
			}
		}

		/// <summary>Returns a string representation of this <see cref="T:System.Net.Mime.ContentType" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the current settings for this <see cref="T:System.Net.Mime.ContentType" />.</returns>
		// Token: 0x060031A7 RID: 12711 RVA: 0x000B1D1E File Offset: 0x000AFF1E
		public override string ToString()
		{
			if (this._type == null || this.IsChanged)
			{
				this._type = this.Encode(false);
				this._isChanged = false;
				this._parameters.IsChanged = false;
				this._isPersisted = false;
			}
			return this._type;
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000B1D60 File Offset: 0x000AFF60
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._mediaType);
			stringBuilder.Append('/');
			stringBuilder.Append(this._subType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentType.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentType.EncodeToBuffer(this._parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000B1E18 File Offset: 0x000B0018
		private static void EncodeToBuffer(string value, StringBuilder builder, bool allowUnicode)
		{
			Encoding encoding = MimeBasePart.DecodeEncoding(value);
			if (encoding != null)
			{
				builder.Append('"').Append(value).Append('"');
				return;
			}
			if ((allowUnicode && !MailBnfHelper.HasCROrLF(value)) || MimeBasePart.IsAscii(value, false))
			{
				MailBnfHelper.GetTokenOrQuotedString(value, builder, allowUnicode);
				return;
			}
			encoding = Encoding.GetEncoding("utf-8");
			builder.Append('"').Append(MimeBasePart.EncodeHeaderValue(value, encoding, MimeBasePart.ShouldUseBase64Encoding(encoding))).Append('"');
		}

		/// <summary>Determines whether the content-type header of the specified <see cref="T:System.Net.Mime.ContentType" /> object is equal to the content-type header of this object.</summary>
		/// <returns>true if the content-type headers are the same; otherwise false.</returns>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentType" /> object to compare with this object.</param>
		// Token: 0x060031AA RID: 12714 RVA: 0x000B17D0 File Offset: 0x000AF9D0
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Equals(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentType" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x060031AB RID: 12715 RVA: 0x000B17E9 File Offset: 0x000AF9E9
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000B1E90 File Offset: 0x000B0090
		private void ParseValue()
		{
			int num = 0;
			Exception ex = null;
			try
			{
				this._mediaType = MailBnfHelper.ReadToken(this._type, ref num, null);
				if (this._mediaType == null || this._mediaType.Length == 0 || num >= this._type.Length || this._type[num++] != '/')
				{
					ex = new FormatException("The specified content type is invalid.");
				}
				if (ex == null)
				{
					this._subType = MailBnfHelper.ReadToken(this._type, ref num, null);
					if (this._subType == null || this._subType.Length == 0)
					{
						ex = new FormatException("The specified content type is invalid.");
					}
				}
				if (ex == null)
				{
					while (MailBnfHelper.SkipCFWS(this._type, ref num))
					{
						if (this._type[num++] != ';')
						{
							ex = new FormatException("The specified content type is invalid.");
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this._type, ref num))
						{
							break;
						}
						string text = MailBnfHelper.ReadParameterAttribute(this._type, ref num, null);
						if (text == null || text.Length == 0)
						{
							ex = new FormatException("The specified content type is invalid.");
							break;
						}
						if (num >= this._type.Length || this._type[num++] != '=')
						{
							ex = new FormatException("The specified content type is invalid.");
							break;
						}
						if (!MailBnfHelper.SkipCFWS(this._type, ref num))
						{
							ex = new FormatException("The specified content type is invalid.");
							break;
						}
						string text2 = ((this._type[num] == '"') ? MailBnfHelper.ReadQuotedString(this._type, ref num, null) : MailBnfHelper.ReadToken(this._type, ref num, null));
						if (text2 == null)
						{
							ex = new FormatException("The specified content type is invalid.");
							break;
						}
						this._parameters.Add(text, text2);
					}
				}
				this._parameters.IsChanged = false;
			}
			catch (FormatException)
			{
				throw new FormatException("The specified content type is invalid.");
			}
			if (ex != null)
			{
				throw new FormatException("The specified content type is invalid.");
			}
		}

		// Token: 0x04001E50 RID: 7760
		private readonly TrackingStringDictionary _parameters = new TrackingStringDictionary();

		// Token: 0x04001E51 RID: 7761
		private string _mediaType;

		// Token: 0x04001E52 RID: 7762
		private string _subType;

		// Token: 0x04001E53 RID: 7763
		private bool _isChanged;

		// Token: 0x04001E54 RID: 7764
		private string _type;

		// Token: 0x04001E55 RID: 7765
		private bool _isPersisted;

		// Token: 0x04001E56 RID: 7766
		internal const string Default = "application/octet-stream";
	}
}
