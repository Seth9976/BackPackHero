using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;

namespace System.Net.Mime
{
	// Token: 0x02000618 RID: 1560
	internal class MimeWriter : BaseWriter
	{
		// Token: 0x06003203 RID: 12803 RVA: 0x000B36DA File Offset: 0x000B18DA
		internal MimeWriter(Stream stream, string boundary)
			: base(stream, false)
		{
			if (boundary == null)
			{
				throw new ArgumentNullException("boundary");
			}
			this._boundaryBytes = Encoding.ASCII.GetBytes(boundary);
		}

		// Token: 0x06003204 RID: 12804 RVA: 0x000B370C File Offset: 0x000B190C
		internal override void WriteHeaders(NameValueCollection headers, bool allowUnicode)
		{
			if (headers == null)
			{
				throw new ArgumentNullException("headers");
			}
			foreach (object obj in headers)
			{
				string text = (string)obj;
				base.WriteHeader(text, headers[text], allowUnicode);
			}
		}

		// Token: 0x06003205 RID: 12805 RVA: 0x000B3778 File Offset: 0x000B1978
		internal IAsyncResult BeginClose(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			this.Close(multiAsyncResult);
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000B379C File Offset: 0x000B199C
		internal void EndClose(IAsyncResult result)
		{
			MultiAsyncResult.End(result);
			this._stream.Close();
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000B37B0 File Offset: 0x000B19B0
		internal override void Close()
		{
			this.Close(null);
			this._stream.Close();
		}

		// Token: 0x06003208 RID: 12808 RVA: 0x000B37C4 File Offset: 0x000B19C4
		private void Close(MultiAsyncResult multiResult)
		{
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(this._boundaryBytes);
			this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			base.Flush(multiResult);
		}

		// Token: 0x06003209 RID: 12809 RVA: 0x000B3829 File Offset: 0x000B1A29
		protected override void OnClose(object sender, EventArgs args)
		{
			if (this._contentStream != sender)
			{
				return;
			}
			this._contentStream.Flush();
			this._contentStream = null;
			this._writeBoundary = true;
			this._isInContent = false;
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000B3858 File Offset: 0x000B1A58
		protected override void CheckBoundary()
		{
			if (this._writeBoundary)
			{
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._bufferBuilder.Append(MimeWriter.s_DASHDASH);
				this._bufferBuilder.Append(this._boundaryBytes);
				this._bufferBuilder.Append(BaseWriter.s_crlf);
				this._writeBoundary = false;
			}
		}

		// Token: 0x04001E90 RID: 7824
		private static byte[] s_DASHDASH = new byte[] { 45, 45 };

		// Token: 0x04001E91 RID: 7825
		private byte[] _boundaryBytes;

		// Token: 0x04001E92 RID: 7826
		private bool _writeBoundary = true;
	}
}
