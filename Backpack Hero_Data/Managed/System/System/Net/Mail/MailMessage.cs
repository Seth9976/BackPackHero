using System;
using System.Collections.Specialized;
using System.Net.Mime;
using System.Text;

namespace System.Net.Mail
{
	/// <summary>Represents an e-mail message that can be sent using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x02000638 RID: 1592
	public class MailMessage : IDisposable
	{
		/// <summary>Initializes an empty instance of the <see cref="T:System.Net.Mail.MailMessage" /> class.</summary>
		// Token: 0x060032F4 RID: 13044 RVA: 0x000B8C7C File Offset: 0x000B6E7C
		public MailMessage()
		{
			this.to = new MailAddressCollection();
			this.alternateViews = new AlternateViewCollection();
			this.attachments = new AttachmentCollection();
			this.bcc = new MailAddressCollection();
			this.cc = new MailAddressCollection();
			this.replyTo = new MailAddressCollection();
			this.headers = new NameValueCollection();
			this.headers.Add("MIME-Version", "1.0");
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.Net.Mail.MailAddress" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.Net.Mail.MailAddress" /> that contains the address of the recipient of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x060032F5 RID: 13045 RVA: 0x000B8CFC File Offset: 0x000B6EFC
		public MailMessage(MailAddress from, MailAddress to)
			: this()
		{
			if (from == null || to == null)
			{
				throw new ArgumentNullException();
			}
			this.From = from;
			this.to.Add(to);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class by using the specified <see cref="T:System.String" /> class objects. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the addresses of the recipients of the e-mail message.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x060032F6 RID: 13046 RVA: 0x000B8D24 File Offset: 0x000B6F24
		public MailMessage(string from, string to)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Mail.MailMessage" /> class. </summary>
		/// <param name="from">A <see cref="T:System.String" /> that contains the address of the sender of the e-mail message.</param>
		/// <param name="to">A <see cref="T:System.String" /> that contains the address of the recipient of the e-mail message.</param>
		/// <param name="subject">A <see cref="T:System.String" /> that contains the subject text.</param>
		/// <param name="body">A <see cref="T:System.String" /> that contains the message body.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="from" /> is null.-or-<paramref name="to" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="from" /> is <see cref="F:System.String.Empty" /> ("").-or-<paramref name="to" /> is <see cref="F:System.String.Empty" /> ("").</exception>
		/// <exception cref="T:System.FormatException">
		///   <paramref name="from" /> or <paramref name="to" /> is malformed.</exception>
		// Token: 0x060032F7 RID: 13047 RVA: 0x000B8DB4 File Offset: 0x000B6FB4
		public MailMessage(string from, string to, string subject, string body)
			: this()
		{
			if (from == null || from == string.Empty)
			{
				throw new ArgumentNullException("from");
			}
			if (to == null || to == string.Empty)
			{
				throw new ArgumentNullException("to");
			}
			this.from = new MailAddress(from);
			foreach (string text in to.Split(new char[] { ',' }))
			{
				this.to.Add(new MailAddress(text.Trim()));
			}
			this.Body = body;
			this.Subject = subject;
		}

		/// <summary>Gets the attachment collection used to store alternate forms of the message body.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AlternateViewCollection" />.</returns>
		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000B8E52 File Offset: 0x000B7052
		public AlternateViewCollection AlternateViews
		{
			get
			{
				return this.alternateViews;
			}
		}

		/// <summary>Gets the attachment collection used to store data attached to this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.AttachmentCollection" />.</returns>
		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000B8E5A File Offset: 0x000B705A
		public AttachmentCollection Attachments
		{
			get
			{
				return this.attachments;
			}
		}

		/// <summary>Gets the address collection that contains the blind carbon copy (BCC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000B8E62 File Offset: 0x000B7062
		public MailAddressCollection Bcc
		{
			get
			{
				return this.bcc;
			}
		}

		/// <summary>Gets or sets the message body.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the body text.</returns>
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x060032FB RID: 13051 RVA: 0x000B8E6A File Offset: 0x000B706A
		// (set) Token: 0x060032FC RID: 13052 RVA: 0x000B8E72 File Offset: 0x000B7072
		public string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				if (value != null && this.bodyEncoding == null)
				{
					this.bodyEncoding = this.GuessEncoding(value) ?? Encoding.ASCII;
				}
				this.body = value;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x060032FD RID: 13053 RVA: 0x000B8E9C File Offset: 0x000B709C
		internal ContentType BodyContentType
		{
			get
			{
				return new ContentType(this.isHtml ? "text/html" : "text/plain")
				{
					CharSet = (this.BodyEncoding ?? Encoding.ASCII).HeaderName
				};
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x000B8ED1 File Offset: 0x000B70D1
		internal TransferEncoding ContentTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
		}

		/// <summary>Gets or sets the encoding used to encode the message body.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000BC5 RID: 3013
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000B8EDE File Offset: 0x000B70DE
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000B8EE6 File Offset: 0x000B70E6
		public Encoding BodyEncoding
		{
			get
			{
				return this.bodyEncoding;
			}
			set
			{
				this.bodyEncoding = value;
			}
		}

		/// <summary>Gets or sets the transfer encoding used to encode the message body.</summary>
		/// <returns>Returns <see cref="T:System.Net.Mime.TransferEncoding" />.A <see cref="T:System.Net.Mime.TransferEncoding" /> applied to the contents of the <see cref="P:System.Net.Mail.MailMessage.Body" />.</returns>
		// Token: 0x17000BC6 RID: 3014
		// (get) Token: 0x06003301 RID: 13057 RVA: 0x000B8ED1 File Offset: 0x000B70D1
		// (set) Token: 0x06003302 RID: 13058 RVA: 0x0000822E File Offset: 0x0000642E
		public TransferEncoding BodyTransferEncoding
		{
			get
			{
				return MailMessage.GuessTransferEncoding(this.BodyEncoding);
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the address collection that contains the carbon copy (CC) recipients for this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000BC7 RID: 3015
		// (get) Token: 0x06003303 RID: 13059 RVA: 0x000B8EEF File Offset: 0x000B70EF
		public MailAddressCollection CC
		{
			get
			{
				return this.cc;
			}
		}

		/// <summary>Gets or sets the delivery notifications for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.DeliveryNotificationOptions" /> value that contains the delivery notifications for this message.</returns>
		// Token: 0x17000BC8 RID: 3016
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x000B8EF7 File Offset: 0x000B70F7
		// (set) Token: 0x06003305 RID: 13061 RVA: 0x000B8EFF File Offset: 0x000B70FF
		public DeliveryNotificationOptions DeliveryNotificationOptions
		{
			get
			{
				return this.deliveryNotificationOptions;
			}
			set
			{
				this.deliveryNotificationOptions = value;
			}
		}

		/// <summary>Gets or sets the from address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the from address information.</returns>
		// Token: 0x17000BC9 RID: 3017
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x000B8F08 File Offset: 0x000B7108
		// (set) Token: 0x06003307 RID: 13063 RVA: 0x000B8F10 File Offset: 0x000B7110
		public MailAddress From
		{
			get
			{
				return this.from;
			}
			set
			{
				this.from = value;
			}
		}

		/// <summary>Gets the e-mail headers that are transmitted with this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the e-mail headers.</returns>
		// Token: 0x17000BCA RID: 3018
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x000B8F19 File Offset: 0x000B7119
		public NameValueCollection Headers
		{
			get
			{
				return this.headers;
			}
		}

		/// <summary>Gets or sets a value indicating whether the mail message body is in Html.</summary>
		/// <returns>true if the message body is in Html; else false. The default is false.</returns>
		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x06003309 RID: 13065 RVA: 0x000B8F21 File Offset: 0x000B7121
		// (set) Token: 0x0600330A RID: 13066 RVA: 0x000B8F29 File Offset: 0x000B7129
		public bool IsBodyHtml
		{
			get
			{
				return this.isHtml;
			}
			set
			{
				this.isHtml = value;
			}
		}

		/// <summary>Gets or sets the priority of this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailPriority" /> that contains the priority of this message.</returns>
		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600330B RID: 13067 RVA: 0x000B8F32 File Offset: 0x000B7132
		// (set) Token: 0x0600330C RID: 13068 RVA: 0x000B8F3A File Offset: 0x000B713A
		public MailPriority Priority
		{
			get
			{
				return this.priority;
			}
			set
			{
				this.priority = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the user-defined custom headers for this e-mail message.</summary>
		/// <returns>The encoding used for user-defined custom headers for this e-mail message.</returns>
		// Token: 0x17000BCD RID: 3021
		// (get) Token: 0x0600330D RID: 13069 RVA: 0x000B8F43 File Offset: 0x000B7143
		// (set) Token: 0x0600330E RID: 13070 RVA: 0x000B8F4B File Offset: 0x000B714B
		public Encoding HeadersEncoding
		{
			get
			{
				return this.headersEncoding;
			}
			set
			{
				this.headersEncoding = value;
			}
		}

		/// <summary>Gets or sets the list of addresses to reply to for the mail message.</summary>
		/// <returns>The list of the addresses to reply to for the mail message.</returns>
		// Token: 0x17000BCE RID: 3022
		// (get) Token: 0x0600330F RID: 13071 RVA: 0x000B8F54 File Offset: 0x000B7154
		public MailAddressCollection ReplyToList
		{
			get
			{
				return this.replyTo;
			}
		}

		/// <summary>Gets or sets the ReplyTo address for the mail message.</summary>
		/// <returns>A MailAddress that indicates the value of the <see cref="P:System.Net.Mail.MailMessage.ReplyTo" /> field.</returns>
		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x000B8F5C File Offset: 0x000B715C
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x000B8F79 File Offset: 0x000B7179
		[Obsolete("Use ReplyToList instead")]
		public MailAddress ReplyTo
		{
			get
			{
				if (this.replyTo.Count == 0)
				{
					return null;
				}
				return this.replyTo[0];
			}
			set
			{
				this.replyTo.Clear();
				this.replyTo.Add(value);
			}
		}

		/// <summary>Gets or sets the sender's address for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.Net.Mail.MailAddress" /> that contains the sender's address information.</returns>
		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x000B8F92 File Offset: 0x000B7192
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x000B8F9A File Offset: 0x000B719A
		public MailAddress Sender
		{
			get
			{
				return this.sender;
			}
			set
			{
				this.sender = value;
			}
		}

		/// <summary>Gets or sets the subject line for this e-mail message.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the subject content.</returns>
		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x000B8FA3 File Offset: 0x000B71A3
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x000B8FAB File Offset: 0x000B71AB
		public string Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				if (value != null && this.subjectEncoding == null)
				{
					this.subjectEncoding = this.GuessEncoding(value);
				}
				this.subject = value;
			}
		}

		/// <summary>Gets or sets the encoding used for the subject content for this e-mail message.</summary>
		/// <returns>An <see cref="T:System.Text.Encoding" /> that was used to encode the <see cref="P:System.Net.Mail.MailMessage.Subject" /> property.</returns>
		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x000B8FCC File Offset: 0x000B71CC
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x000B8FD4 File Offset: 0x000B71D4
		public Encoding SubjectEncoding
		{
			get
			{
				return this.subjectEncoding;
			}
			set
			{
				this.subjectEncoding = value;
			}
		}

		/// <summary>Gets the address collection that contains the recipients of this e-mail message.</summary>
		/// <returns>A writable <see cref="T:System.Net.Mail.MailAddressCollection" /> object.</returns>
		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x000B8FDD File Offset: 0x000B71DD
		public MailAddressCollection To
		{
			get
			{
				return this.to;
			}
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Net.Mail.MailMessage" />. </summary>
		// Token: 0x06003319 RID: 13081 RVA: 0x000B8FE5 File Offset: 0x000B71E5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Net.Mail.MailMessage" /> and optionally releases the managed resources. </summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x0600331A RID: 13082 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000B8FF4 File Offset: 0x000B71F4
		private Encoding GuessEncoding(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					return MailMessage.UTF8Unmarked;
				}
			}
			return null;
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000B9028 File Offset: 0x000B7228
		internal static TransferEncoding GuessTransferEncoding(Encoding enc)
		{
			if (Encoding.ASCII.Equals(enc))
			{
				return TransferEncoding.SevenBit;
			}
			if (Encoding.UTF8.CodePage == enc.CodePage || Encoding.Unicode.CodePage == enc.CodePage || Encoding.UTF32.CodePage == enc.CodePage)
			{
				return TransferEncoding.Base64;
			}
			return TransferEncoding.QuotedPrintable;
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000B9080 File Offset: 0x000B7280
		internal static string To2047(byte[] bytes)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in bytes)
			{
				if (b < 33 || b > 126 || b == 63 || b == 61 || b == 95)
				{
					stringBuilder.Append('=');
					stringBuilder.Append(MailMessage.hex[(b >> 4) & 15]);
					stringBuilder.Append(MailMessage.hex[(int)(b & 15)]);
				}
				else
				{
					stringBuilder.Append((char)b);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600331E RID: 13086 RVA: 0x000B9100 File Offset: 0x000B7300
		internal static string EncodeSubjectRFC2047(string s, Encoding enc)
		{
			if (s == null || Encoding.ASCII.Equals(enc))
			{
				return s;
			}
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					string text = MailMessage.To2047(enc.GetBytes(s));
					return string.Concat(new string[] { "=?", enc.HeaderName, "?Q?", text, "?=" });
				}
			}
			return s;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x0600331F RID: 13087 RVA: 0x000B917D File Offset: 0x000B737D
		private static Encoding UTF8Unmarked
		{
			get
			{
				if (MailMessage.utf8unmarked == null)
				{
					MailMessage.utf8unmarked = new UTF8Encoding(false);
				}
				return MailMessage.utf8unmarked;
			}
		}

		// Token: 0x04001F13 RID: 7955
		private AlternateViewCollection alternateViews;

		// Token: 0x04001F14 RID: 7956
		private AttachmentCollection attachments;

		// Token: 0x04001F15 RID: 7957
		private MailAddressCollection bcc;

		// Token: 0x04001F16 RID: 7958
		private MailAddressCollection replyTo;

		// Token: 0x04001F17 RID: 7959
		private string body;

		// Token: 0x04001F18 RID: 7960
		private MailPriority priority;

		// Token: 0x04001F19 RID: 7961
		private MailAddress sender;

		// Token: 0x04001F1A RID: 7962
		private DeliveryNotificationOptions deliveryNotificationOptions;

		// Token: 0x04001F1B RID: 7963
		private MailAddressCollection cc;

		// Token: 0x04001F1C RID: 7964
		private MailAddress from;

		// Token: 0x04001F1D RID: 7965
		private NameValueCollection headers;

		// Token: 0x04001F1E RID: 7966
		private MailAddressCollection to;

		// Token: 0x04001F1F RID: 7967
		private string subject;

		// Token: 0x04001F20 RID: 7968
		private Encoding subjectEncoding;

		// Token: 0x04001F21 RID: 7969
		private Encoding bodyEncoding;

		// Token: 0x04001F22 RID: 7970
		private Encoding headersEncoding = Encoding.UTF8;

		// Token: 0x04001F23 RID: 7971
		private bool isHtml;

		// Token: 0x04001F24 RID: 7972
		private static char[] hex = new char[]
		{
			'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
			'A', 'B', 'C', 'D', 'E', 'F'
		};

		// Token: 0x04001F25 RID: 7973
		private static Encoding utf8unmarked;
	}
}
