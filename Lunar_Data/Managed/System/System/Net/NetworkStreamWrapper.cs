using System;
using System.IO;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020003A6 RID: 934
	internal class NetworkStreamWrapper : Stream
	{
		// Token: 0x06001EE0 RID: 7904 RVA: 0x00071F06 File Offset: 0x00070106
		internal NetworkStreamWrapper(TcpClient client)
		{
			this._client = client;
			this._networkStream = client.GetStream();
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001EE1 RID: 7905 RVA: 0x00071F21 File Offset: 0x00070121
		protected bool UsingSecureStream
		{
			get
			{
				return this._networkStream is TlsStream;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x06001EE2 RID: 7906 RVA: 0x00071F31 File Offset: 0x00070131
		internal IPAddress ServerAddress
		{
			get
			{
				return ((IPEndPoint)this.Socket.RemoteEndPoint).Address;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x06001EE3 RID: 7907 RVA: 0x00071F48 File Offset: 0x00070148
		internal Socket Socket
		{
			get
			{
				return this._client.Client;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001EE4 RID: 7908 RVA: 0x00071F55 File Offset: 0x00070155
		// (set) Token: 0x06001EE5 RID: 7909 RVA: 0x00071F5D File Offset: 0x0007015D
		internal NetworkStream NetworkStream
		{
			get
			{
				return this._networkStream;
			}
			set
			{
				this._networkStream = value;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x06001EE6 RID: 7910 RVA: 0x00071F66 File Offset: 0x00070166
		public override bool CanRead
		{
			get
			{
				return this._networkStream.CanRead;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001EE7 RID: 7911 RVA: 0x00071F73 File Offset: 0x00070173
		public override bool CanSeek
		{
			get
			{
				return this._networkStream.CanSeek;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001EE8 RID: 7912 RVA: 0x00071F80 File Offset: 0x00070180
		public override bool CanWrite
		{
			get
			{
				return this._networkStream.CanWrite;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x00071F8D File Offset: 0x0007018D
		public override bool CanTimeout
		{
			get
			{
				return this._networkStream.CanTimeout;
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001EEA RID: 7914 RVA: 0x00071F9A File Offset: 0x0007019A
		// (set) Token: 0x06001EEB RID: 7915 RVA: 0x00071FA7 File Offset: 0x000701A7
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

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001EEC RID: 7916 RVA: 0x00071FB5 File Offset: 0x000701B5
		// (set) Token: 0x06001EED RID: 7917 RVA: 0x00071FC2 File Offset: 0x000701C2
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

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001EEE RID: 7918 RVA: 0x00071FD0 File Offset: 0x000701D0
		public override long Length
		{
			get
			{
				return this._networkStream.Length;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00071FDD File Offset: 0x000701DD
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x00071FEA File Offset: 0x000701EA
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

		// Token: 0x06001EF1 RID: 7921 RVA: 0x00071FF8 File Offset: 0x000701F8
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this._networkStream.Seek(offset, origin);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00072007 File Offset: 0x00070207
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._networkStream.Read(buffer, offset, size);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00072017 File Offset: 0x00070217
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._networkStream.Write(buffer, offset, size);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00072028 File Offset: 0x00070228
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					this.CloseSocket();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00072058 File Offset: 0x00070258
		internal void CloseSocket()
		{
			this._networkStream.Close();
			this._client.Dispose();
		}

		// Token: 0x06001EF6 RID: 7926 RVA: 0x00072070 File Offset: 0x00070270
		public void Close(int timeout)
		{
			this._networkStream.Close(timeout);
			this._client.Dispose();
		}

		// Token: 0x06001EF7 RID: 7927 RVA: 0x00072089 File Offset: 0x00070289
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginRead(buffer, offset, size, callback, state);
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0007209D File Offset: 0x0007029D
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._networkStream.EndRead(asyncResult);
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x000720AB File Offset: 0x000702AB
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.ReadAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x000720BD File Offset: 0x000702BD
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._networkStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06001EFB RID: 7931 RVA: 0x000720D1 File Offset: 0x000702D1
		public override void EndWrite(IAsyncResult asyncResult)
		{
			this._networkStream.EndWrite(asyncResult);
		}

		// Token: 0x06001EFC RID: 7932 RVA: 0x000720DF File Offset: 0x000702DF
		public override Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return this._networkStream.WriteAsync(buffer, offset, count, cancellationToken);
		}

		// Token: 0x06001EFD RID: 7933 RVA: 0x000720F1 File Offset: 0x000702F1
		public override void Flush()
		{
			this._networkStream.Flush();
		}

		// Token: 0x06001EFE RID: 7934 RVA: 0x000720FE File Offset: 0x000702FE
		public override Task FlushAsync(CancellationToken cancellationToken)
		{
			return this._networkStream.FlushAsync(cancellationToken);
		}

		// Token: 0x06001EFF RID: 7935 RVA: 0x0007210C File Offset: 0x0007030C
		public override void SetLength(long value)
		{
			this._networkStream.SetLength(value);
		}

		// Token: 0x06001F00 RID: 7936 RVA: 0x0007211A File Offset: 0x0007031A
		internal void SetSocketTimeoutOption(int timeout)
		{
			this._networkStream.ReadTimeout = timeout;
			this._networkStream.WriteTimeout = timeout;
		}

		// Token: 0x04001073 RID: 4211
		private TcpClient _client;

		// Token: 0x04001074 RID: 4212
		private NetworkStream _networkStream;
	}
}
