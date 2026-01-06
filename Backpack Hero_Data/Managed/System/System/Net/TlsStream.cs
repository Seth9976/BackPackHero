using System;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000380 RID: 896
	internal class TlsStream : NetworkStream
	{
		// Token: 0x06001D68 RID: 7528 RVA: 0x0006B5FA File Offset: 0x000697FA
		public TlsStream(NetworkStream stream, Socket socket, string host, X509CertificateCollection clientCertificates)
			: base(socket)
		{
			this._sslStream = new SslStream(stream, false, ServicePointManager.ServerCertificateValidationCallback);
			this._host = host;
			this._clientCertificates = clientCertificates;
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0006B624 File Offset: 0x00069824
		public void AuthenticateAsClient()
		{
			this._sslStream.AuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0006B647 File Offset: 0x00069847
		public IAsyncResult BeginAuthenticateAsClient(AsyncCallback asyncCallback, object state)
		{
			return this._sslStream.BeginAuthenticateAsClient(this._host, this._clientCertificates, (SslProtocols)ServicePointManager.SecurityProtocol, ServicePointManager.CheckCertificateRevocationList, asyncCallback, state);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0006B66C File Offset: 0x0006986C
		public void EndAuthenticateAsClient(IAsyncResult asyncResult)
		{
			this._sslStream.EndAuthenticateAsClient(asyncResult);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0006B67A File Offset: 0x0006987A
		public override IAsyncResult BeginWrite(byte[] buffer, int offset, int size, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginWrite(buffer, offset, size, callback, state);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0006B68E File Offset: 0x0006988E
		public override void EndWrite(IAsyncResult result)
		{
			this._sslStream.EndWrite(result);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0006B69C File Offset: 0x0006989C
		public override void Write(byte[] buffer, int offset, int size)
		{
			this._sslStream.Write(buffer, offset, size);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0006B6AC File Offset: 0x000698AC
		public override int Read(byte[] buffer, int offset, int size)
		{
			return this._sslStream.Read(buffer, offset, size);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0006B6BC File Offset: 0x000698BC
		public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
		{
			return this._sslStream.BeginRead(buffer, offset, count, callback, state);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0006B6D0 File Offset: 0x000698D0
		public override int EndRead(IAsyncResult asyncResult)
		{
			return this._sslStream.EndRead(asyncResult);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0006B6DE File Offset: 0x000698DE
		public override void Close()
		{
			base.Close();
			if (this._sslStream != null)
			{
				this._sslStream.Close();
			}
		}

		// Token: 0x04000F3E RID: 3902
		private SslStream _sslStream;

		// Token: 0x04000F3F RID: 3903
		private string _host;

		// Token: 0x04000F40 RID: 3904
		private X509CertificateCollection _clientCertificates;
	}
}
