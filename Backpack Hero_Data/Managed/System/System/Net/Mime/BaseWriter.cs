using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mail;
using System.Runtime.ExceptionServices;

namespace System.Net.Mime
{
	// Token: 0x02000604 RID: 1540
	internal abstract class BaseWriter
	{
		// Token: 0x0600316B RID: 12651 RVA: 0x000B0FE8 File Offset: 0x000AF1E8
		protected BaseWriter(Stream stream, bool shouldEncodeLeadingDots)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this._stream = stream;
			this._shouldEncodeLeadingDots = shouldEncodeLeadingDots;
			this._onCloseHandler = new EventHandler(this.OnClose);
			this._bufferBuilder = new BufferBuilder();
			this._lineLength = 76;
		}

		// Token: 0x0600316C RID: 12652
		internal abstract void WriteHeaders(NameValueCollection headers, bool allowUnicode);

		// Token: 0x0600316D RID: 12653 RVA: 0x000B1040 File Offset: 0x000AF240
		internal void WriteHeader(string name, string value, bool allowUnicode)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this.CheckBoundary();
			this._bufferBuilder.Append(name);
			this._bufferBuilder.Append(": ");
			this.WriteAndFold(value, name.Length + 2, allowUnicode);
			this._bufferBuilder.Append(BaseWriter.s_crlf);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000B10C0 File Offset: 0x000AF2C0
		private void WriteAndFold(string value, int charsAlreadyOnLine, bool allowUnicode)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < value.Length; i++)
			{
				if (MailBnfHelper.IsFWSAt(value, i))
				{
					i += 2;
					this._bufferBuilder.Append(value, num2, i - num2, allowUnicode);
					num2 = i;
					num = i;
					charsAlreadyOnLine = 0;
				}
				else if (i - num2 > this._lineLength - charsAlreadyOnLine && num != num2)
				{
					this._bufferBuilder.Append(value, num2, num - num2, allowUnicode);
					this._bufferBuilder.Append(BaseWriter.s_crlf);
					num2 = num;
					charsAlreadyOnLine = 0;
				}
				else if (value[i] == MailBnfHelper.Space || value[i] == MailBnfHelper.Tab)
				{
					num = i;
				}
			}
			if (value.Length - num2 > 0)
			{
				this._bufferBuilder.Append(value, num2, value.Length - num2, allowUnicode);
			}
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000B1187 File Offset: 0x000AF387
		internal Stream GetContentStream()
		{
			return this.GetContentStream(null);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x000B1190 File Offset: 0x000AF390
		private Stream GetContentStream(MultiAsyncResult multiResult)
		{
			if (this._isInContent)
			{
				throw new InvalidOperationException("This operation cannot be performed while in content.");
			}
			this._isInContent = true;
			this.CheckBoundary();
			this._bufferBuilder.Append(BaseWriter.s_crlf);
			this.Flush(multiResult);
			ClosableStream closableStream = new ClosableStream(new EightBitStream(this._stream, this._shouldEncodeLeadingDots), this._onCloseHandler);
			this._contentStream = closableStream;
			return closableStream;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x000B11FC File Offset: 0x000AF3FC
		internal IAsyncResult BeginGetContentStream(AsyncCallback callback, object state)
		{
			MultiAsyncResult multiAsyncResult = new MultiAsyncResult(this, callback, state);
			Stream contentStream = this.GetContentStream(multiAsyncResult);
			if (!(multiAsyncResult.Result is Exception))
			{
				multiAsyncResult.Result = contentStream;
			}
			multiAsyncResult.CompleteSequence();
			return multiAsyncResult;
		}

		// Token: 0x06003172 RID: 12658 RVA: 0x000B1238 File Offset: 0x000AF438
		internal Stream EndGetContentStream(IAsyncResult result)
		{
			object obj = MultiAsyncResult.End(result);
			Exception ex = obj as Exception;
			if (ex != null)
			{
				ExceptionDispatchInfo.Throw(ex);
			}
			return (Stream)obj;
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x000B1260 File Offset: 0x000AF460
		protected void Flush(MultiAsyncResult multiResult)
		{
			if (this._bufferBuilder.Length > 0)
			{
				if (multiResult != null)
				{
					multiResult.Enter();
					IAsyncResult asyncResult = this._stream.BeginWrite(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length, BaseWriter.s_onWrite, multiResult);
					if (asyncResult.CompletedSynchronously)
					{
						this._stream.EndWrite(asyncResult);
						multiResult.Leave();
					}
				}
				else
				{
					this._stream.Write(this._bufferBuilder.GetBuffer(), 0, this._bufferBuilder.Length);
				}
				this._bufferBuilder.Reset();
			}
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x000B12F8 File Offset: 0x000AF4F8
		protected static void OnWrite(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)result.AsyncState;
				BaseWriter baseWriter = (BaseWriter)multiAsyncResult.Context;
				try
				{
					baseWriter._stream.EndWrite(result);
					multiAsyncResult.Leave();
				}
				catch (Exception ex)
				{
					multiAsyncResult.Leave(ex);
				}
			}
		}

		// Token: 0x06003175 RID: 12661
		internal abstract void Close();

		// Token: 0x06003176 RID: 12662
		protected abstract void OnClose(object sender, EventArgs args);

		// Token: 0x06003177 RID: 12663 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void CheckBoundary()
		{
		}

		// Token: 0x04001E38 RID: 7736
		private const int DefaultLineLength = 76;

		// Token: 0x04001E39 RID: 7737
		private static readonly AsyncCallback s_onWrite = new AsyncCallback(BaseWriter.OnWrite);

		// Token: 0x04001E3A RID: 7738
		protected static readonly byte[] s_crlf = new byte[] { 13, 10 };

		// Token: 0x04001E3B RID: 7739
		protected readonly BufferBuilder _bufferBuilder;

		// Token: 0x04001E3C RID: 7740
		protected readonly Stream _stream;

		// Token: 0x04001E3D RID: 7741
		private readonly EventHandler _onCloseHandler;

		// Token: 0x04001E3E RID: 7742
		private readonly bool _shouldEncodeLeadingDots;

		// Token: 0x04001E3F RID: 7743
		private int _lineLength;

		// Token: 0x04001E40 RID: 7744
		protected Stream _contentStream;

		// Token: 0x04001E41 RID: 7745
		protected bool _isInContent;
	}
}
