using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	/// <summary>Represents a MIME protocol Content-Disposition header.</summary>
	// Token: 0x02000605 RID: 1541
	public class ContentDisposition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with a <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> of <see cref="F:System.Net.Mime.DispositionTypeNames.Attachment" />. </summary>
		// Token: 0x06003179 RID: 12665 RVA: 0x000B137C File Offset: 0x000AF57C
		public ContentDisposition()
		{
			this._isChanged = true;
			this._disposition = (this._dispositionType = "attachment");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mime.ContentDisposition" /> class with the specified disposition information.</summary>
		/// <param name="disposition">A <see cref="T:System.Net.Mime.DispositionTypeNames" /> value that contains the disposition.</param>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="disposition" /> is null or equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x0600317A RID: 12666 RVA: 0x000B13AA File Offset: 0x000AF5AA
		public ContentDisposition(string disposition)
		{
			if (disposition == null)
			{
				throw new ArgumentNullException("disposition");
			}
			this._isChanged = true;
			this._disposition = disposition;
			this.ParseValue();
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000B13D4 File Offset: 0x000AF5D4
		internal DateTime GetDateParameter(string parameterName)
		{
			SmtpDateTime smtpDateTime = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet(parameterName) as SmtpDateTime;
			if (smtpDateTime != null)
			{
				return smtpDateTime.Date;
			}
			return DateTime.MinValue;
		}

		/// <summary>Gets or sets the disposition type for an e-mail attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the disposition type. The value is not restricted but is typically one of the <see cref="P:System.Net.Mime.ContentDisposition.DispositionType" /> values.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is equal to <see cref="F:System.String.Empty" /> ("").</exception>
		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000B1407 File Offset: 0x000AF607
		// (set) Token: 0x0600317D RID: 12669 RVA: 0x000B140F File Offset: 0x000AF60F
		public string DispositionType
		{
			get
			{
				return this._dispositionType;
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
				this._isChanged = true;
				this._dispositionType = value;
			}
		}

		/// <summary>Gets the parameters included in the Content-Disposition header represented by this instance.</summary>
		/// <returns>A writable <see cref="T:System.Collections.Specialized.StringDictionary" /> that contains parameter name/value pairs.</returns>
		// Token: 0x17000B86 RID: 2950
		// (get) Token: 0x0600317E RID: 12670 RVA: 0x000B144C File Offset: 0x000AF64C
		public StringDictionary Parameters
		{
			get
			{
				TrackingValidationObjectDictionary trackingValidationObjectDictionary;
				if ((trackingValidationObjectDictionary = this._parameters) == null)
				{
					trackingValidationObjectDictionary = (this._parameters = new TrackingValidationObjectDictionary(ContentDisposition.s_validators));
				}
				return trackingValidationObjectDictionary;
			}
		}

		/// <summary>Gets or sets the suggested file name for an e-mail attachment.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the file name. </returns>
		// Token: 0x17000B87 RID: 2951
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000B1476 File Offset: 0x000AF676
		// (set) Token: 0x06003180 RID: 12672 RVA: 0x000B1488 File Offset: 0x000AF688
		public string FileName
		{
			get
			{
				return this.Parameters["filename"];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Parameters.Remove("filename");
					return;
				}
				this.Parameters["filename"] = value;
			}
		}

		/// <summary>Gets or sets the creation date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file creation date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000B88 RID: 2952
		// (get) Token: 0x06003181 RID: 12673 RVA: 0x000B14B4 File Offset: 0x000AF6B4
		// (set) Token: 0x06003182 RID: 12674 RVA: 0x000B14C4 File Offset: 0x000AF6C4
		public DateTime CreationDate
		{
			get
			{
				return this.GetDateParameter("creation-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("creation-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the modification date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file modification date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000B89 RID: 2953
		// (get) Token: 0x06003183 RID: 12675 RVA: 0x000B14EE File Offset: 0x000AF6EE
		// (set) Token: 0x06003184 RID: 12676 RVA: 0x000B14FC File Offset: 0x000AF6FC
		public DateTime ModificationDate
		{
			get
			{
				return this.GetDateParameter("modification-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("modification-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that determines the disposition type (Inline or Attachment) for an e-mail attachment.</summary>
		/// <returns>true if content in the attachment is presented inline as part of the e-mail body; otherwise, false. </returns>
		// Token: 0x17000B8A RID: 2954
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000B1526 File Offset: 0x000AF726
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000B1538 File Offset: 0x000AF738
		public bool Inline
		{
			get
			{
				return this._dispositionType == "inline";
			}
			set
			{
				this._isChanged = true;
				this._dispositionType = (value ? "inline" : "attachment");
			}
		}

		/// <summary>Gets or sets the read date for a file attachment.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> value that indicates the file read date; otherwise, <see cref="F:System.DateTime.MinValue" /> if no date was specified.</returns>
		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x06003187 RID: 12679 RVA: 0x000B1556 File Offset: 0x000AF756
		// (set) Token: 0x06003188 RID: 12680 RVA: 0x000B1564 File Offset: 0x000AF764
		public DateTime ReadDate
		{
			get
			{
				return this.GetDateParameter("read-date");
			}
			set
			{
				SmtpDateTime smtpDateTime = new SmtpDateTime(value);
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("read-date", smtpDateTime);
			}
		}

		/// <summary>Gets or sets the size of a file attachment.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that specifies the number of bytes in the file attachment. The default value is -1, which indicates that the file size is unknown.</returns>
		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000B1590 File Offset: 0x000AF790
		// (set) Token: 0x0600318A RID: 12682 RVA: 0x000B15BF File Offset: 0x000AF7BF
		public long Size
		{
			get
			{
				object obj = ((TrackingValidationObjectDictionary)this.Parameters).InternalGet("size");
				if (obj != null)
				{
					return (long)obj;
				}
				return -1L;
			}
			set
			{
				((TrackingValidationObjectDictionary)this.Parameters).InternalSet("size", value);
			}
		}

		// Token: 0x0600318B RID: 12683 RVA: 0x000B15DC File Offset: 0x000AF7DC
		internal void Set(string contentDisposition, HeaderCollection headers)
		{
			this._disposition = contentDisposition;
			this.ParseValue();
			headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
			this._isPersisted = true;
		}

		// Token: 0x0600318C RID: 12684 RVA: 0x000B1604 File Offset: 0x000AF804
		internal void PersistIfNeeded(HeaderCollection headers, bool forcePersist)
		{
			if (this.IsChanged || !this._isPersisted || forcePersist)
			{
				headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this.ToString());
				this._isPersisted = true;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600318D RID: 12685 RVA: 0x000B1637 File Offset: 0x000AF837
		internal bool IsChanged
		{
			get
			{
				return this._isChanged || (this._parameters != null && this._parameters.IsChanged);
			}
		}

		/// <summary>Returns a <see cref="T:System.String" /> representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the property values for this instance.</returns>
		// Token: 0x0600318E RID: 12686 RVA: 0x000B1658 File Offset: 0x000AF858
		public override string ToString()
		{
			if (this._disposition == null || this._isChanged || (this._parameters != null && this._parameters.IsChanged))
			{
				this._disposition = this.Encode(false);
				this._isChanged = false;
				this._parameters.IsChanged = false;
				this._isPersisted = false;
			}
			return this._disposition;
		}

		// Token: 0x0600318F RID: 12687 RVA: 0x000B16B8 File Offset: 0x000AF8B8
		internal string Encode(bool allowUnicode)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this._dispositionType);
			foreach (object obj in this.Parameters.Keys)
			{
				string text = (string)obj;
				stringBuilder.Append("; ");
				ContentDisposition.EncodeToBuffer(text, stringBuilder, allowUnicode);
				stringBuilder.Append('=');
				ContentDisposition.EncodeToBuffer(this._parameters[text], stringBuilder, allowUnicode);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003190 RID: 12688 RVA: 0x000B1758 File Offset: 0x000AF958
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

		/// <summary>Determines whether the content-disposition header of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object is equal to the content-disposition header of this object.</summary>
		/// <returns>true if the content-disposition headers are the same; otherwise false.</returns>
		/// <param name="rparam">The <see cref="T:System.Net.Mime.ContentDisposition" /> object to compare with this object.</param>
		// Token: 0x06003191 RID: 12689 RVA: 0x000B17D0 File Offset: 0x000AF9D0
		public override bool Equals(object rparam)
		{
			return rparam != null && string.Equals(this.ToString(), rparam.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		/// <summary>Determines the hash code of the specified <see cref="T:System.Net.Mime.ContentDisposition" /> object</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06003192 RID: 12690 RVA: 0x000B17E9 File Offset: 0x000AF9E9
		public override int GetHashCode()
		{
			return this.ToString().ToLowerInvariant().GetHashCode();
		}

		// Token: 0x06003193 RID: 12691 RVA: 0x000B17FC File Offset: 0x000AF9FC
		private void ParseValue()
		{
			int num = 0;
			try
			{
				this._dispositionType = MailBnfHelper.ReadToken(this._disposition, ref num, null);
				if (string.IsNullOrEmpty(this._dispositionType))
				{
					throw new FormatException("The mail header is malformed.");
				}
				if (this._parameters == null)
				{
					this._parameters = new TrackingValidationObjectDictionary(ContentDisposition.s_validators);
				}
				else
				{
					this._parameters.Clear();
				}
				while (MailBnfHelper.SkipCFWS(this._disposition, ref num))
				{
					if (this._disposition[num++] != ';')
					{
						throw new FormatException(SR.Format("An invalid character was found in the mail header: '{0}'.", this._disposition[num - 1]));
					}
					if (!MailBnfHelper.SkipCFWS(this._disposition, ref num))
					{
						break;
					}
					string text = MailBnfHelper.ReadParameterAttribute(this._disposition, ref num, null);
					if (this._disposition[num++] != '=')
					{
						throw new FormatException("The mail header is malformed.");
					}
					if (!MailBnfHelper.SkipCFWS(this._disposition, ref num))
					{
						throw new FormatException("The specified content disposition is invalid.");
					}
					string text2 = ((this._disposition[num] == '"') ? MailBnfHelper.ReadQuotedString(this._disposition, ref num, null) : MailBnfHelper.ReadToken(this._disposition, ref num, null));
					if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(text2))
					{
						throw new FormatException("The specified content disposition is invalid.");
					}
					this.Parameters.Add(text, text2);
				}
			}
			catch (FormatException ex)
			{
				throw new FormatException("The specified content disposition is invalid.", ex);
			}
			this._parameters.IsChanged = false;
		}

		// Token: 0x04001E42 RID: 7746
		private const string CreationDateKey = "creation-date";

		// Token: 0x04001E43 RID: 7747
		private const string ModificationDateKey = "modification-date";

		// Token: 0x04001E44 RID: 7748
		private const string ReadDateKey = "read-date";

		// Token: 0x04001E45 RID: 7749
		private const string FileNameKey = "filename";

		// Token: 0x04001E46 RID: 7750
		private const string SizeKey = "size";

		// Token: 0x04001E47 RID: 7751
		private TrackingValidationObjectDictionary _parameters;

		// Token: 0x04001E48 RID: 7752
		private string _disposition;

		// Token: 0x04001E49 RID: 7753
		private string _dispositionType;

		// Token: 0x04001E4A RID: 7754
		private bool _isChanged;

		// Token: 0x04001E4B RID: 7755
		private bool _isPersisted;

		// Token: 0x04001E4C RID: 7756
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue s_dateParser = (object v) => new SmtpDateTime(v.ToString());

		// Token: 0x04001E4D RID: 7757
		private static readonly TrackingValidationObjectDictionary.ValidateAndParseValue s_longParser = delegate(object value)
		{
			long num;
			if (!long.TryParse(value.ToString(), NumberStyles.None, CultureInfo.InvariantCulture, out num))
			{
				throw new FormatException("The specified content disposition is invalid.");
			}
			return num;
		};

		// Token: 0x04001E4E RID: 7758
		private static readonly Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue> s_validators = new Dictionary<string, TrackingValidationObjectDictionary.ValidateAndParseValue>
		{
			{
				"creation-date",
				ContentDisposition.s_dateParser
			},
			{
				"modification-date",
				ContentDisposition.s_dateParser
			},
			{
				"read-date",
				ContentDisposition.s_dateParser
			},
			{
				"size",
				ContentDisposition.s_longParser
			}
		};
	}
}
