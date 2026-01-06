using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;

namespace System.Net
{
	// Token: 0x0200039B RID: 923
	internal class FtpDataStream : Stream, ICloseEx
	{
		// Token: 0x06001E5E RID: 7774 RVA: 0x0006FA18 File Offset: 0x0006DC18
		internal FtpDataStream(NetworkStream networkStream, FtpWebRequest request, TriState writeOnly)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, ".ctor");
			}
			this._readable = true;
			this._writeable = true;
			if (writeOnly == TriState.True)
			{
				this._readable = false;
			}
			else if (writeOnly == TriState.False)
			{
				this._writeable = false;
			}
			this._networkStream = networkStream;
			this._request = request;
		}

		// Token: 0x06001E5F RID: 7775 RVA: 0x0006FA74 File Offset: 0x0006DC74
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					((ICloseEx)this).CloseEx(CloseExState.Normal);
				}
				else
				{
					((ICloseEx)this).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001E60 RID: 7776 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
		void ICloseEx.CloseEx(CloseExState closeState)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state = {0}", new object[] { closeState }), "CloseEx");
			}
			lock (this)
			{
				if (this._closing)
				{
					return;
				}
				this._closing = true;
				this._writeable = false;
				this._readable = false;
			}
			try
			{
				try
				{
					if ((closeState & CloseExState.Abort) == CloseExState.Normal)
					{
						this._networkStream.Close(-1);
					}
					else
					{
						this._networkStream.Close(0);
					}
				}
				finally
				{
					this._request.DataStreamClosed(closeState);
				}
			}
			catch (Exception ex)
			{
				bool flag2 = true;
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					FtpWebResponse ftpWebResponse = ex2.Response as FtpWebResponse;
					if (ftpWebResponse != null && !this._isFullyRead && ftpWebResponse.StatusCode == FtpStatusCode.ConnectionClosed)
					{
						flag2 = false;
					}
				}
				if (flag2 && (closeState & CloseExState.Silent) == CloseExState.Normal)
				{
					throw;
				}
			}
		}

		// Token: 0x06001E61 RID: 7777 RVA: 0x0006FBBC File Offset: 0x0006DDBC
		private void CheckError()
		{
			if (this._request.Aborted)
			{
				throw ExceptionHelper.RequestAbortedException;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001E62 RID: 7778 RVA: 0x0006FBD1 File Offset: 0x0006DDD1
		public override bool CanRead
		{
			get
			{
				return this._readable;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001E63 RID: 7779 RVA: 0x0006FBD9 File Offset: 0x0006DDD9
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x0006FBE6 File Offset: 0x0006DDE6
		public override bool CanWrite
		{
			get
			{
				return this._writeable;
			}
		}

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x06001E65 RID: 7781 RVA: 0x0006FBEE File Offset: 0x0006DDEE
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x06001E66 RID: 7782 RVA: 0x0006FBFB File Offset: 0x0006DDFB
		// (set) Token: 0x06001E67 RID: 7783 RVA: 0x0006FC08 File Offset: 0x0006DE08
		public override long Position
		{
			get
			{
				return this._networkStream.Position;
			}
			set
			{
				this._networkStream.Position = value;
			}
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x0006FC18 File Offset: 0x0006DE18
		public override long Seek(long offset, SeekOrigin origin)
		{
			this.CheckError();
			long num;
			try
			{
				num = this._networkStream.Seek(offset, origin);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return num;
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0006FC58 File Offset: 0x0006DE58
		public override int Read(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			int num;
			try
			{
				num = this._networkStream.Read(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			if (num == 0)
			{
				this._isFullyRead = true;
				this.Close();
			}
			return num;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
		public override void Write(byte[] buffer, int offset, int size)
		{
			this.CheckError();
			try
			{
				this._networkStream.Write(buffer, offset, size);
			}
			catch
			{
				this.CheckError();
				throw;
			}
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x0006FCE4 File Offset: 0x0006DEE4
		private void AsyncReadCallback(IAsyncResult ar)
		{
			LazyAsyncResult lazyAsyncResult = (LazyAsyncResult)ar.AsyncState;
			try
			{
				try
				{
					int num = this._networkStream.EndRead(ar);
					if (num == 0)
					{
						this._isFullyRead = true;
						this.Close();
					}
					lazyAsyncResult.InvokeCallback(num);
				}
				catch (Exception ex)
				{
					if (!lazyAsyncResult.IsCompleted)
					{
						lazyAsyncResult.InvokeCallback(ex);
					}
				}
			}
			catch
			{
			}
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x0006FD5C File Offset: 0x0006DF5C
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			LazyAsyncResult lazyAsyncResult = new LazyAsyncResult(this, state, callback);
			try
			{
				this._networkStream.BeginRead(buffer, offset, size, new AsyncCallback(this.AsyncReadCallback), lazyAsyncResult);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return lazyAsyncResult;
		}

		// Token: 0x06001E6D RID: 7789 RVA: 0x0006FDB4 File Offset: 0x0006DFB4
		public override int EndRead(IAsyncResult ar)
		{
			int num;
			try
			{
				object obj = ((LazyAsyncResult)ar).InternalWaitForCompletion();
				Exception ex = obj as Exception;
				if (ex != null)
				{
					ExceptionDispatchInfo.Throw(ex);
				}
				num = (int)obj;
			}
			finally
			{
				this.CheckError();
			}
			return num;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x0006FDFC File Offset: 0x0006DFFC
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			this.CheckError();
			IAsyncResult asyncResult;
			try
			{
				asyncResult = this._networkStream.BeginWrite(buffer, offset, size, callback, state);
			}
			catch
			{
				this.CheckError();
				throw;
			}
			return asyncResult;
		}

		// Token: 0x06001E6F RID: 7791 RVA: 0x0006FE40 File Offset: 0x0006E040
		public override void EndWrite(IAsyncResult asyncResult)
		{
			try
			{
				this._networkStream.EndWrite(asyncResult);
			}
			finally
			{
				this.CheckError();
			}
		}

		// Token: 0x06001E70 RID: 7792 RVA: 0x0006FE74 File Offset: 0x0006E074
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06001E71 RID: 7793 RVA: 0x0006FE81 File Offset: 0x0006E081
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x17000606 RID: 1542
		// (get) Token: 0x06001E72 RID: 7794 RVA: 0x0006FE8F File Offset: 0x0006E08F
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001E73 RID: 7795 RVA: 0x0006FE9C File Offset: 0x0006E09C
		// (set) Token: 0x06001E74 RID: 7796 RVA: 0x0006FEA9 File Offset: 0x0006E0A9
		public override int ReadTimeout
		{
			get
			{
				return this._networkStream.ReadTimeout;
			}
			set
			{
				this._networkStream.ReadTimeout = value;
			}
		}

		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x06001E75 RID: 7797 RVA: 0x0006FEB7 File Offset: 0x0006E0B7
		// (set) Token: 0x06001E76 RID: 7798 RVA: 0x0006FEC4 File Offset: 0x0006E0C4
		public override int WriteTimeout
		{
			get
			{
				return this._networkStream.WriteTimeout;
			}
			set
			{
				this._networkStream.WriteTimeout = value;
			}
		}

		// Token: 0x06001E77 RID: 7799 RVA: 0x0006FED2 File Offset: 0x0006E0D2
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04001011 RID: 4113
		private FtpWebRequest _request;

		// Token: 0x04001012 RID: 4114
		private NetworkStream _networkStream;

		// Token: 0x04001013 RID: 4115
		private bool _writeable;

		// Token: 0x04001014 RID: 4116
		private bool _readable;

		// Token: 0x04001015 RID: 4117
		private bool _isFullyRead;

		// Token: 0x04001016 RID: 4118
		private bool _closing;

		// Token: 0x04001017 RID: 4119
		private const int DefaultCloseTimeout = -1;
	}
}
