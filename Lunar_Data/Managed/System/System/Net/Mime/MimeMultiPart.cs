using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;

namespace System.Net.Mime
{
	// Token: 0x02000613 RID: 1555
	internal class MimeMultiPart : MimeBasePart
	{
		// Token: 0x060031DE RID: 12766 RVA: 0x000B2BC3 File Offset: 0x000B0DC3
		internal MimeMultiPart(MimeMultiPartType type)
		{
			this.MimeMultiPartType = type;
		}

		// Token: 0x17000B99 RID: 2969
		// (set) Token: 0x060031DF RID: 12767 RVA: 0x000B2BD2 File Offset: 0x000B0DD2
		internal MimeMultiPartType MimeMultiPartType
		{
			set
			{
				if (value > MimeMultiPartType.Related || value < MimeMultiPartType.Mixed)
				{
					throw new NotSupportedException(value.ToString());
				}
				this.SetType(value);
			}
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000B2BF6 File Offset: 0x000B0DF6
		private void SetType(MimeMultiPartType type)
		{
			base.ContentType.MediaType = "multipart/" + type.ToString().ToLower(CultureInfo.InvariantCulture);
			base.ContentType.Boundary = this.GetNextBoundary();
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000B2C35 File Offset: 0x000B0E35
		internal Collection<MimeBasePart> Parts
		{
			get
			{
				if (this._parts == null)
				{
					this._parts = new Collection<MimeBasePart>();
				}
				return this._parts;
			}
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x000B2C50 File Offset: 0x000B0E50
		internal void Complete(IAsyncResult result, Exception e)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			if (mimePartContext._completed)
			{
				ExceptionDispatchInfo.Throw(e);
			}
			try
			{
				mimePartContext._outputStream.Close();
			}
			catch (Exception ex)
			{
				if (e == null)
				{
					e = ex;
				}
			}
			mimePartContext._completed = true;
			mimePartContext._result.InvokeCallback(e);
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000B2CB4 File Offset: 0x000B0EB4
		internal void MimeWriterCloseCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimeWriterCloseCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000B2D00 File Offset: 0x000B0F00
		private void MimeWriterCloseCallbackHandler(IAsyncResult result)
		{
			((MimeWriter)((MimeMultiPart.MimePartContext)result.AsyncState)._writer).EndClose(result);
			this.Complete(result, null);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000B2D28 File Offset: 0x000B0F28
		internal void MimePartSentCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.MimePartSentCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000B2D74 File Offset: 0x000B0F74
		private void MimePartSentCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._partsEnumerator.Current.EndSend(result);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				IAsyncResult asyncResult = mimePartContext._partsEnumerator.Current.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000B2E0C File Offset: 0x000B100C
		internal void ContentStreamCallback(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			((MimeMultiPart.MimePartContext)result.AsyncState)._completedSynchronously = false;
			try
			{
				this.ContentStreamCallbackHandler(result);
			}
			catch (Exception ex)
			{
				this.Complete(result, ex);
			}
		}

		// Token: 0x060031E8 RID: 12776 RVA: 0x000B2E58 File Offset: 0x000B1058
		private void ContentStreamCallbackHandler(IAsyncResult result)
		{
			MimeMultiPart.MimePartContext mimePartContext = (MimeMultiPart.MimePartContext)result.AsyncState;
			mimePartContext._outputStream = mimePartContext._writer.EndGetContentStream(result);
			mimePartContext._writer = new MimeWriter(mimePartContext._outputStream, base.ContentType.Boundary);
			if (mimePartContext._partsEnumerator.MoveNext())
			{
				MimeBasePart mimeBasePart = mimePartContext._partsEnumerator.Current;
				this._mimePartSentCallback = new AsyncCallback(this.MimePartSentCallback);
				IAsyncResult asyncResult = mimeBasePart.BeginSend(mimePartContext._writer, this._mimePartSentCallback, this._allowUnicode, mimePartContext);
				if (asyncResult.CompletedSynchronously)
				{
					this.MimePartSentCallbackHandler(asyncResult);
				}
				return;
			}
			IAsyncResult asyncResult2 = ((MimeWriter)mimePartContext._writer).BeginClose(new AsyncCallback(this.MimeWriterCloseCallback), mimePartContext);
			if (asyncResult2.CompletedSynchronously)
			{
				this.MimeWriterCloseCallbackHandler(asyncResult2);
			}
		}

		// Token: 0x060031E9 RID: 12777 RVA: 0x000B2F20 File Offset: 0x000B1120
		internal override IAsyncResult BeginSend(BaseWriter writer, AsyncCallback callback, bool allowUnicode, object state)
		{
			this._allowUnicode = allowUnicode;
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			MimeBasePart.MimePartAsyncResult mimePartAsyncResult = new MimeBasePart.MimePartAsyncResult(this, state, callback);
			MimeMultiPart.MimePartContext mimePartContext = new MimeMultiPart.MimePartContext(writer, mimePartAsyncResult, this.Parts.GetEnumerator());
			IAsyncResult asyncResult = writer.BeginGetContentStream(new AsyncCallback(this.ContentStreamCallback), mimePartContext);
			if (asyncResult.CompletedSynchronously)
			{
				this.ContentStreamCallbackHandler(asyncResult);
			}
			return mimePartAsyncResult;
		}

		// Token: 0x060031EA RID: 12778 RVA: 0x000B2F8C File Offset: 0x000B118C
		internal override void Send(BaseWriter writer, bool allowUnicode)
		{
			base.PrepareHeaders(allowUnicode);
			writer.WriteHeaders(base.Headers, allowUnicode);
			Stream contentStream = writer.GetContentStream();
			MimeWriter mimeWriter = new MimeWriter(contentStream, base.ContentType.Boundary);
			foreach (MimeBasePart mimeBasePart in this.Parts)
			{
				mimeBasePart.Send(mimeWriter, allowUnicode);
			}
			mimeWriter.Close();
			contentStream.Close();
		}

		// Token: 0x060031EB RID: 12779 RVA: 0x000B3014 File Offset: 0x000B1214
		internal string GetNextBoundary()
		{
			return "--boundary_" + (Interlocked.Increment(ref MimeMultiPart.s_boundary) - 1).ToString(CultureInfo.InvariantCulture) + "_" + Guid.NewGuid().ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x04001E73 RID: 7795
		private Collection<MimeBasePart> _parts;

		// Token: 0x04001E74 RID: 7796
		private static int s_boundary;

		// Token: 0x04001E75 RID: 7797
		private AsyncCallback _mimePartSentCallback;

		// Token: 0x04001E76 RID: 7798
		private bool _allowUnicode;

		// Token: 0x02000614 RID: 1556
		internal class MimePartContext
		{
			// Token: 0x060031EC RID: 12780 RVA: 0x000B305C File Offset: 0x000B125C
			internal MimePartContext(BaseWriter writer, LazyAsyncResult result, IEnumerator<MimeBasePart> partsEnumerator)
			{
				this._writer = writer;
				this._result = result;
				this._partsEnumerator = partsEnumerator;
			}

			// Token: 0x04001E77 RID: 7799
			internal IEnumerator<MimeBasePart> _partsEnumerator;

			// Token: 0x04001E78 RID: 7800
			internal Stream _outputStream;

			// Token: 0x04001E79 RID: 7801
			internal LazyAsyncResult _result;

			// Token: 0x04001E7A RID: 7802
			internal BaseWriter _writer;

			// Token: 0x04001E7B RID: 7803
			internal bool _completed;

			// Token: 0x04001E7C RID: 7804
			internal bool _completedSynchronously = true;
		}
	}
}
