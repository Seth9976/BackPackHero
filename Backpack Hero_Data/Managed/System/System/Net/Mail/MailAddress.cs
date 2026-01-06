using System;
using System.Globalization;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents the address of an electronic mail sender or recipient.</summary>
	// Token: 0x0200062A RID: 1578
	public class MailAddress
	{
		// Token: 0x06003287 RID: 12935 RVA: 0x000B59D0 File Offset: 0x000B3BD0
		internal MailAddress(string displayName, string userName, string domain)
		{
			this._host = domain;
			this._userName = userName;
			this._displayName = displayName;
			this._displayNameEncoding = Encoding.GetEncoding("utf-8");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address. </summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.</exception>
		// Token: 0x06003288 RID: 12936 RVA: 0x000B59FD File Offset: 0x000B3BFD
		public MailAddress(string address)
			: this(address, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address and display name.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />. This parameter can be null.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x06003289 RID: 12937 RVA: 0x000B5A08 File Offset: 0x000B3C08
		public MailAddress(string address, string displayName)
			: this(address, displayName, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailAddress" /> class using the specified address, display name, and encoding.</summary>
		/// <param name="address">A <see cref="T:System.String" /> that contains an e-mail address.</param>
		/// <param name="displayName">A <see cref="T:System.String" /> that contains the display name associated with <paramref name="address" />.</param>
		/// <param name="displayNameEncoding">The <see cref="T:System.Text.Encoding" /> that defines the character set used for <paramref name="displayName" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="address" /> is null.-or-<paramref name="displayName" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="address" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="displayName" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="address" /> is not in a recognized format.-or-<paramref name="address" /> contains non-ASCII characters.</exception>
		// Token: 0x0600328A RID: 12938 RVA: 0x000B5A14 File Offset: 0x000B3C14
		public MailAddress(string address, string displayName, Encoding displayNameEncoding)
		{
			if (address == null)
			{
				throw new ArgumentNullException("address");
			}
			if (address == string.Empty)
			{
				throw new ArgumentException(SR.Format("The parameter '{0}' cannot be an empty string.", "address"), "address");
			}
			this._displayNameEncoding = displayNameEncoding ?? Encoding.GetEncoding("utf-8");
			this._displayName = displayName ?? string.Empty;
			if (!string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = MailAddressParser.NormalizeOrThrow(this._displayName);
				if (this._displayName.Length >= 2 && this._displayName[0] == '"' && this._displayName[this._displayName.Length - 1] == '"')
				{
					this._displayName = this._displayName.Substring(1, this._displayName.Length - 2);
				}
			}
			MailAddress mailAddress = MailAddressParser.ParseAddress(address);
			this._host = mailAddress._host;
			this._userName = mailAddress._userName;
			if (string.IsNullOrEmpty(this._displayName))
			{
				this._displayName = mailAddress._displayName;
			}
		}

		/// <summary>Gets the display name composed from the display name and address information specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the display name; otherwise, <see cref="F:System.String.Empty" /> ("") if no display name information was specified when this instance was created.</returns>
		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x0600328B RID: 12939 RVA: 0x000B5B30 File Offset: 0x000B3D30
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		/// <summary>Gets the user information from the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the user name portion of the <see cref="P:System.Net.Mail.MailAddress.Address" />.</returns>
		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x0600328C RID: 12940 RVA: 0x000B5B38 File Offset: 0x000B3D38
		public string User
		{
			get
			{
				return this._userName;
			}
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000B5B40 File Offset: 0x000B3D40
		private string GetUser(bool allowUnicode)
		{
			if (!allowUnicode && !MimeBasePart.IsAscii(this._userName, true))
			{
				throw new SmtpException(SR.Format("The client or server is only configured for E-mail addresses with ASCII local-parts: {0}.", this.Address));
			}
			return this._userName;
		}

		/// <summary>Gets the host portion of the address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the name of the host computer that accepts e-mail for the <see cref="P:System.Net.Mail.MailAddress.User" /> property.</returns>
		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x000B5B6F File Offset: 0x000B3D6F
		public string Host
		{
			get
			{
				return this._host;
			}
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000B5B78 File Offset: 0x000B3D78
		private string GetHost(bool allowUnicode)
		{
			string text = this._host;
			if (!allowUnicode && !MimeBasePart.IsAscii(text, true))
			{
				IdnMapping idnMapping = new IdnMapping();
				try
				{
					text = idnMapping.GetAscii(text);
				}
				catch (ArgumentException ex)
				{
					throw new SmtpException(SR.Format("The address has an invalid host name: {0}.", this.Address), ex);
				}
			}
			return text;
		}

		/// <summary>Gets the e-mail address specified when this instance was created.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the e-mail address.</returns>
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000B5BD4 File Offset: 0x000B3DD4
		public string Address
		{
			get
			{
				return this._userName + "@" + this._host;
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000B5BEC File Offset: 0x000B3DEC
		private string GetAddress(bool allowUnicode)
		{
			return this.GetUser(allowUnicode) + "@" + this.GetHost(allowUnicode);
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x000B5C06 File Offset: 0x000B3E06
		private string SmtpAddress
		{
			get
			{
				return "<" + this.Address + ">";
			}
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000B5C1D File Offset: 0x000B3E1D
		internal string GetSmtpAddress(bool allowUnicode)
		{
			return "<" + this.GetAddress(allowUnicode) + ">";
		}

		/// <summary>Returns a string representation of this instance.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the contents of this <see cref="T:System.Net.Mail.MailAddress" />.</returns>
		// Token: 0x06003294 RID: 12948 RVA: 0x000B5C35 File Offset: 0x000B3E35
		public override string ToString()
		{
			if (string.IsNullOrEmpty(this.DisplayName))
			{
				return this.Address;
			}
			return "\"" + this.DisplayName + "\" " + this.SmtpAddress;
		}

		/// <summary>Compares two mail addresses.</summary>
		/// <returns>true if the two mail addresses are equal; otherwise, false.</returns>
		/// <param name="value">A <see cref="T:System.Net.Mail.MailAddress" /> instance to compare to the current instance.</param>
		// Token: 0x06003295 RID: 12949 RVA: 0x000B5C66 File Offset: 0x000B3E66
		public override bool Equals(object value)
		{
			return value != null && this.ToString().Equals(value.ToString(), StringComparison.InvariantCultureIgnoreCase);
		}

		/// <summary>Returns a hash value for a mail address.</summary>
		/// <returns>An integer hash value.</returns>
		// Token: 0x06003296 RID: 12950 RVA: 0x000887D6 File Offset: 0x000869D6
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000B5C80 File Offset: 0x000B3E80
		internal string Encode(int charsConsumed, bool allowUnicode)
		{
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this._displayName))
			{
				if (MimeBasePart.IsAscii(this._displayName, false) || allowUnicode)
				{
					text = "\"" + this._displayName + "\"";
				}
				else
				{
					IEncodableStream encoderForHeader = MailAddress.s_encoderFactory.GetEncoderForHeader(this._displayNameEncoding, false, charsConsumed);
					byte[] bytes = this._displayNameEncoding.GetBytes(this._displayName);
					encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
					text = encoderForHeader.GetEncodedString();
				}
				text = text + " " + this.GetSmtpAddress(allowUnicode);
			}
			else
			{
				text = this.GetAddress(allowUnicode);
			}
			return text;
		}

		// Token: 0x04001ED3 RID: 7891
		private readonly Encoding _displayNameEncoding;

		// Token: 0x04001ED4 RID: 7892
		private readonly string _displayName;

		// Token: 0x04001ED5 RID: 7893
		private readonly string _userName;

		// Token: 0x04001ED6 RID: 7894
		private readonly string _host;

		// Token: 0x04001ED7 RID: 7895
		private static readonly EncodedStreamFactory s_encoderFactory = new EncodedStreamFactory();
	}
}
