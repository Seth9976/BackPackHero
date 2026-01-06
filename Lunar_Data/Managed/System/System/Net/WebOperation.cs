using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020004D9 RID: 1241
	internal class WebOperation
	{
		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06002841 RID: 10305 RVA: 0x00095A66 File Offset: 0x00093C66
		public HttpWebRequest Request { get; }

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06002842 RID: 10306 RVA: 0x00095A6E File Offset: 0x00093C6E
		// (set) Token: 0x06002843 RID: 10307 RVA: 0x00095A76 File Offset: 0x00093C76
		public WebConnection Connection { get; private set; }

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06002844 RID: 10308 RVA: 0x00095A7F File Offset: 0x00093C7F
		// (set) Token: 0x06002845 RID: 10309 RVA: 0x00095A87 File Offset: 0x00093C87
		public ServicePoint ServicePoint { get; private set; }

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06002846 RID: 10310 RVA: 0x00095A90 File Offset: 0x00093C90
		public BufferOffsetSize WriteBuffer { get; }

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06002847 RID: 10311 RVA: 0x00095A98 File Offset: 0x00093C98
		public bool IsNtlmChallenge { get; }

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x06002848 RID: 10312 RVA: 0x00002F6A File Offset: 0x0000116A
		internal string ME
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002849 RID: 10313 RVA: 0x00095AA0 File Offset: 0x00093CA0
		public WebOperation(HttpWebRequest request, BufferOffsetSize writeBuffer, bool isNtlmChallenge, CancellationToken cancellationToken)
		{
			this.Request = request;
			this.WriteBuffer = writeBuffer;
			this.IsNtlmChallenge = isNtlmChallenge;
			this.cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
			this.requestTask = new WebCompletionSource<WebRequestStream>(true);
			this.requestWrittenTask = new WebCompletionSource<WebRequestStream>(true);
			this.responseTask = new WebCompletionSource<WebResponseStream>(true);
			this.finishedTask = new WebCompletionSource<ValueTuple<bool, WebOperation>>(true);
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600284A RID: 10314 RVA: 0x00095B05 File Offset: 0x00093D05
		public bool Aborted
		{
			get
			{
				return this.disposedInfo != null || this.Request.Aborted || (this.cts != null && this.cts.IsCancellationRequested);
			}
		}

		// Token: 0x1700087E RID: 2174
		// (get) Token: 0x0600284B RID: 10315 RVA: 0x00095B36 File Offset: 0x00093D36
		public bool Closed
		{
			get
			{
				return this.Aborted || this.closedInfo != null;
			}
		}

		// Token: 0x0600284C RID: 10316 RVA: 0x00095B4B File Offset: 0x00093D4B
		public void Abort()
		{
			if (!this.SetDisposed(ref this.disposedInfo).Item2)
			{
				return;
			}
			CancellationTokenSource cancellationTokenSource = this.cts;
			if (cancellationTokenSource != null)
			{
				cancellationTokenSource.Cancel();
			}
			this.SetCanceled();
			this.Close();
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x00095B80 File Offset: 0x00093D80
		public void Close()
		{
			if (!this.SetDisposed(ref this.closedInfo).Item2)
			{
				return;
			}
			WebRequestStream webRequestStream = Interlocked.Exchange<WebRequestStream>(ref this.writeStream, null);
			if (webRequestStream != null)
			{
				try
				{
					webRequestStream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x00095BCC File Offset: 0x00093DCC
		private void SetCanceled()
		{
			OperationCanceledException ex = new OperationCanceledException();
			this.requestTask.TrySetCanceled(ex);
			this.requestWrittenTask.TrySetCanceled(ex);
			this.responseTask.TrySetCanceled(ex);
			this.Finish(false, ex);
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x00095C0E File Offset: 0x00093E0E
		private void SetError(Exception error)
		{
			this.requestTask.TrySetException(error);
			this.requestWrittenTask.TrySetException(error);
			this.responseTask.TrySetException(error);
			this.Finish(false, error);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x00095C40 File Offset: 0x00093E40
		private ValueTuple<ExceptionDispatchInfo, bool> SetDisposed(ref ExceptionDispatchInfo field)
		{
			ExceptionDispatchInfo exceptionDispatchInfo = ExceptionDispatchInfo.Capture(new WebException(SR.GetString("The request was canceled"), WebExceptionStatus.RequestCanceled));
			ExceptionDispatchInfo exceptionDispatchInfo2 = Interlocked.CompareExchange<ExceptionDispatchInfo>(ref field, exceptionDispatchInfo, null);
			return new ValueTuple<ExceptionDispatchInfo, bool>(exceptionDispatchInfo2 ?? exceptionDispatchInfo, exceptionDispatchInfo2 == null);
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x00095C7B File Offset: 0x00093E7B
		internal ExceptionDispatchInfo CheckDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				return this.CheckThrowDisposed(false, ref this.disposedInfo);
			}
			return null;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x00095C9D File Offset: 0x00093E9D
		internal void ThrowIfDisposed()
		{
			this.ThrowIfDisposed(CancellationToken.None);
		}

		// Token: 0x06002853 RID: 10323 RVA: 0x00095CAA File Offset: 0x00093EAA
		internal void ThrowIfDisposed(CancellationToken cancellationToken)
		{
			if (this.Aborted || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.disposedInfo);
			}
		}

		// Token: 0x06002854 RID: 10324 RVA: 0x00095CCB File Offset: 0x00093ECB
		internal void ThrowIfClosedOrDisposed()
		{
			this.ThrowIfClosedOrDisposed(CancellationToken.None);
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00095CD8 File Offset: 0x00093ED8
		internal void ThrowIfClosedOrDisposed(CancellationToken cancellationToken)
		{
			if (this.Closed || cancellationToken.IsCancellationRequested)
			{
				this.CheckThrowDisposed(true, ref this.closedInfo);
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x00095CFC File Offset: 0x00093EFC
		private ExceptionDispatchInfo CheckThrowDisposed(bool throwIt, ref ExceptionDispatchInfo field)
		{
			ValueTuple<ExceptionDispatchInfo, bool> valueTuple = this.SetDisposed(ref field);
			ExceptionDispatchInfo item = valueTuple.Item1;
			if (valueTuple.Item2)
			{
				CancellationTokenSource cancellationTokenSource = this.cts;
				if (cancellationTokenSource != null)
				{
					cancellationTokenSource.Cancel();
				}
			}
			if (throwIt)
			{
				item.Throw();
			}
			return item;
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00095D3C File Offset: 0x00093F3C
		internal void RegisterRequest(ServicePoint servicePoint, WebConnection connection)
		{
			if (servicePoint == null)
			{
				throw new ArgumentNullException("servicePoint");
			}
			if (connection == null)
			{
				throw new ArgumentNullException("connection");
			}
			lock (this)
			{
				if (Interlocked.CompareExchange(ref this.requestSent, 1, 0) != 0)
				{
					throw new InvalidOperationException("Invalid nested call.");
				}
				this.ServicePoint = servicePoint;
				this.Connection = connection;
			}
			this.cts.Token.Register(delegate
			{
				this.Request.FinishedReading = true;
				this.SetDisposed(ref this.disposedInfo);
			});
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00095DD8 File Offset: 0x00093FD8
		public void SetPriorityRequest(WebOperation operation)
		{
			lock (this)
			{
				if (this.requestSent != 1 || this.ServicePoint == null || this.finished != 0)
				{
					throw new InvalidOperationException("Should never happen.");
				}
				if (Interlocked.CompareExchange<WebOperation>(ref this.priorityRequest, operation, null) != null)
				{
					throw new InvalidOperationException("Invalid nested request.");
				}
			}
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00095E4C File Offset: 0x0009404C
		public async Task<Stream> GetRequestStream()
		{
			return await this.requestTask.WaitForCompletion().ConfigureAwait(false);
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x00095E8F File Offset: 0x0009408F
		internal Task<WebRequestStream> GetRequestStreamInternal()
		{
			return this.requestTask.WaitForCompletion();
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00095E9C File Offset: 0x0009409C
		public Task WaitUntilRequestWritten()
		{
			return this.requestWrittenTask.WaitForCompletion();
		}

		// Token: 0x1700087F RID: 2175
		// (get) Token: 0x0600285C RID: 10332 RVA: 0x00095EA9 File Offset: 0x000940A9
		public WebRequestStream WriteStream
		{
			get
			{
				this.ThrowIfDisposed();
				return this.writeStream;
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x00095EB7 File Offset: 0x000940B7
		public Task<WebResponseStream> GetResponseStream()
		{
			return this.responseTask.WaitForCompletion();
		}

		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x0600285E RID: 10334 RVA: 0x00095EC4 File Offset: 0x000940C4
		internal WebCompletionSource<ValueTuple<bool, WebOperation>> Finished
		{
			get
			{
				return this.finishedTask;
			}
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x00095ECC File Offset: 0x000940CC
		internal async void Run()
		{
			try
			{
				this.ThrowIfClosedOrDisposed();
				WebRequestStream webRequestStream = await this.Connection.InitConnection(this, this.cts.Token).ConfigureAwait(false);
				WebRequestStream requestStream = webRequestStream;
				this.ThrowIfClosedOrDisposed();
				this.writeStream = requestStream;
				await requestStream.Initialize(this.cts.Token).ConfigureAwait(false);
				this.ThrowIfClosedOrDisposed();
				this.requestTask.TrySetCompleted(requestStream);
				WebResponseStream stream = new WebResponseStream(requestStream);
				this.responseStream = stream;
				await stream.InitReadAsync(this.cts.Token).ConfigureAwait(false);
				this.responseTask.TrySetCompleted(stream);
				requestStream = null;
				stream = null;
			}
			catch (OperationCanceledException)
			{
				this.SetCanceled();
			}
			catch (Exception ex)
			{
				this.SetError(ex);
			}
		}

		// Token: 0x06002860 RID: 10336 RVA: 0x00095F03 File Offset: 0x00094103
		internal void CompleteRequestWritten(WebRequestStream stream, Exception error = null)
		{
			if (error != null)
			{
				this.SetError(error);
				return;
			}
			this.requestWrittenTask.TrySetCompleted(stream);
		}

		// Token: 0x06002861 RID: 10337 RVA: 0x00095F20 File Offset: 0x00094120
		internal void Finish(bool ok, Exception error = null)
		{
			if (Interlocked.CompareExchange(ref this.finished, 1, 0) != 0)
			{
				return;
			}
			WebResponseStream webResponseStream;
			WebOperation webOperation;
			lock (this)
			{
				webResponseStream = Interlocked.Exchange<WebResponseStream>(ref this.responseStream, null);
				webOperation = Interlocked.Exchange<WebOperation>(ref this.priorityRequest, null);
				this.Request.FinishedReading = true;
			}
			if (error != null)
			{
				if (webOperation != null)
				{
					webOperation.SetError(error);
				}
				this.finishedTask.TrySetException(error);
				return;
			}
			bool flag2 = !this.Aborted && ok && webResponseStream != null && webResponseStream.KeepAlive;
			if (webOperation != null && webOperation.Aborted)
			{
				webOperation = null;
				flag2 = false;
			}
			this.finishedTask.TrySetCompleted(new ValueTuple<bool, WebOperation>(flag2, webOperation));
		}

		// Token: 0x04001779 RID: 6009
		internal readonly int ID;

		// Token: 0x0400177A RID: 6010
		private CancellationTokenSource cts;

		// Token: 0x0400177B RID: 6011
		private WebCompletionSource<WebRequestStream> requestTask;

		// Token: 0x0400177C RID: 6012
		private WebCompletionSource<WebRequestStream> requestWrittenTask;

		// Token: 0x0400177D RID: 6013
		private WebCompletionSource<WebResponseStream> responseTask;

		// Token: 0x0400177E RID: 6014
		private WebCompletionSource<ValueTuple<bool, WebOperation>> finishedTask;

		// Token: 0x0400177F RID: 6015
		private WebRequestStream writeStream;

		// Token: 0x04001780 RID: 6016
		private WebResponseStream responseStream;

		// Token: 0x04001781 RID: 6017
		private ExceptionDispatchInfo disposedInfo;

		// Token: 0x04001782 RID: 6018
		private ExceptionDispatchInfo closedInfo;

		// Token: 0x04001783 RID: 6019
		private WebOperation priorityRequest;

		// Token: 0x04001784 RID: 6020
		private int requestSent;

		// Token: 0x04001785 RID: 6021
		private int finished;
	}
}
