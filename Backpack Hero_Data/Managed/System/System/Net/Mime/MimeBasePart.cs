using System;
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000611 RID: 1553
	internal class MimeBasePart
	{
		// Token: 0x060031CA RID: 12746 RVA: 0x0000219B File Offset: 0x0000039B
		internal MimeBasePart()
		{
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000B2741 File Offset: 0x000B0941
		internal static bool ShouldUseBase64Encoding(Encoding encoding)
		{
			return encoding == Encoding.Unicode || encoding == Encoding.UTF8 || encoding == Encoding.UTF32 || encoding == Encoding.BigEndianUnicode;
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000B2765 File Offset: 0x000B0965
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding)
		{
			return MimeBasePart.EncodeHeaderValue(value, encoding, base64Encoding, 0);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000B2770 File Offset: 0x000B0970
		internal static string EncodeHeaderValue(string value, Encoding encoding, bool base64Encoding, int headerLength)
		{
			if (MimeBasePart.IsAscii(value, false))
			{
				return value;
			}
			if (encoding == null)
			{
				encoding = Encoding.GetEncoding("utf-8");
			}
			IEncodableStream encoderForHeader = new EncodedStreamFactory().GetEncoderForHeader(encoding, base64Encoding, headerLength);
			byte[] bytes = encoding.GetBytes(value);
			encoderForHeader.EncodeBytes(bytes, 0, bytes.Length);
			return encoderForHeader.GetEncodedString();
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000B27C0 File Offset: 0x000B09C0
		internal static string DecodeHeaderValue(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return string.Empty;
			}
			string text = string.Empty;
			string[] array = value.Split(MimeBasePart.s_headerValueSplitChars, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(MimeBasePart.s_questionMarkSplitChars);
				if (array2.Length != 5 || array2[0] != "=" || array2[4] != "=")
				{
					return value;
				}
				string text2 = array2[1];
				bool flag = array2[2] == "B";
				byte[] bytes = Encoding.ASCII.GetBytes(array2[3]);
				int num = new EncodedStreamFactory().GetEncoderForHeader(Encoding.GetEncoding(text2), flag, 0).DecodeBytes(bytes, 0, bytes.Length);
				Encoding encoding = Encoding.GetEncoding(text2);
				text += encoding.GetString(bytes, 0, num);
			}
			return text;
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000B2898 File Offset: 0x000B0A98
		internal static Encoding DecodeEncoding(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return null;
			}
			string[] array = value.Split(MimeBasePart.s_decodeEncodingSplitChars);
			if (array.Length < 5 || array[0] != "=" || array[4] != "=")
			{
				return null;
			}
			return Encoding.GetEncoding(array[1]);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000B28EC File Offset: 0x000B0AEC
		internal static bool IsAscii(string value, bool permitCROrLF)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			foreach (char c in value)
			{
				if (c > '\u007f')
				{
					return false;
				}
				if (!permitCROrLF && (c == '\r' || c == '\n'))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060031D1 RID: 12753 RVA: 0x000B2939 File Offset: 0x000B0B39
		// (set) Token: 0x060031D2 RID: 12754 RVA: 0x000B294C File Offset: 0x000B0B4C
		internal string ContentID
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentID));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentID)] = value;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060031D3 RID: 12755 RVA: 0x000B297A File Offset: 0x000B0B7A
		// (set) Token: 0x060031D4 RID: 12756 RVA: 0x000B298D File Offset: 0x000B0B8D
		internal string ContentLocation
		{
			get
			{
				return this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)];
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					this.Headers.Remove(MailHeaderInfo.GetString(MailHeaderID.ContentLocation));
					return;
				}
				this.Headers[MailHeaderInfo.GetString(MailHeaderID.ContentLocation)] = value;
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060031D5 RID: 12757 RVA: 0x000B29BC File Offset: 0x000B0BBC
		internal NameValueCollection Headers
		{
			get
			{
				if (this._headers == null)
				{
					this._headers = new HeaderCollection();
				}
				if (this._contentType == null)
				{
					this._contentType = new ContentType();
				}
				this._contentType.PersistIfNeeded(this._headers, false);
				if (this._contentDisposition != null)
				{
					this._contentDisposition.PersistIfNeeded(this._headers, false);
				}
				return this._headers;
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060031D6 RID: 12758 RVA: 0x000B2A24 File Offset: 0x000B0C24
		// (set) Token: 0x060031D7 RID: 12759 RVA: 0x000B2A49 File Offset: 0x000B0C49
		internal ContentType ContentType
		{
			get
			{
				ContentType contentType;
				if ((contentType = this._contentType) == null)
				{
					contentType = (this._contentType = new ContentType());
				}
				return contentType;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._contentType = value;
				this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, true);
			}
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x000B2A78 File Offset: 0x000B0C78
		internal void PrepareHeaders(bool allowUnicode)
		{
			this._contentType.PersistIfNeeded((HeaderCollection)this.Headers, false);
			this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentType), this._contentType.Encode(allowUnicode));
			if (this._contentDisposition != null)
			{
				this._contentDisposition.PersistIfNeeded((HeaderCollection)this.Headers, false);
				this._headers.InternalSet(MailHeaderInfo.GetString(MailHeaderID.ContentDisposition), this._contentDisposition.Encode(allowUnicode));
			}
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x0000822E File Offset: 0x0000642E
		internal virtual void Send(BaseWriter writer, bool allowUnicode)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x0000822E File Offset: 0x0000642E
		internal virtual IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000B2AF8 File Offset: 0x000B0CF8
		internal void EndSend(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			LazyAsyncResult lazyAsyncResult = asyncResult as MimeBasePart.MimePartAsyncResult;
			if (lazyAsyncResult == null || lazyAsyncResult.AsyncObject != this)
			{
				throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
			}
			if (lazyAsyncResult.EndCalled)
			{
				throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndSend"));
			}
			lazyAsyncResult.InternalWaitForCompletion();
			lazyAsyncResult.EndCalled = true;
			if (lazyAsyncResult.Result is Exception)
			{
				throw (Exception)lazyAsyncResult.Result;
			}
		}

		// Token: 0x04001E6C RID: 7788
		internal const string DefaultCharSet = "utf-8";

		// Token: 0x04001E6D RID: 7789
		private static readonly char[] s_decodeEncodingSplitChars = new char[] { '?', '\r', '\n' };

		// Token: 0x04001E6E RID: 7790
		protected ContentType _contentType;

		// Token: 0x04001E6F RID: 7791
		protected ContentDisposition _contentDisposition;

		// Token: 0x04001E70 RID: 7792
		private HeaderCollection _headers;

		// Token: 0x04001E71 RID: 7793
		private static readonly char[] s_headerValueSplitChars = new char[] { '\r', '\n', ' ' };

		// Token: 0x04001E72 RID: 7794
		private static readonly char[] s_questionMarkSplitChars = new char[] { '?' };

		// Token: 0x02000612 RID: 1554
		internal class MimePartAsyncResult : LazyAsyncResult
		{
			// Token: 0x060031DD RID: 12765 RVA: 0x000B2BB8 File Offset: 0x000B0DB8
			internal MimePartAsyncResult(MimeBasePart part, object state, AsyncCallback callback)
				: base(part, state, callback)
			{
			}
		}
	}
}
