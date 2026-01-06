using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x02000478 RID: 1144
	internal class ServerCertValidationCallback
	{
		// Token: 0x06002431 RID: 9265 RVA: 0x0008583C File Offset: 0x00083A3C
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06002432 RID: 9266 RVA: 0x00085856 File Offset: 0x00083A56
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x00085860 File Offset: 0x00083A60
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000858A0 File Offset: 0x00083AA0
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x04001522 RID: 5410
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x04001523 RID: 5411
		private readonly ExecutionContext m_Context;

		// Token: 0x02000479 RID: 1145
		private class CallbackContext
		{
			// Token: 0x06002435 RID: 9269 RVA: 0x000858F4 File Offset: 0x00083AF4
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x04001524 RID: 5412
			internal readonly object request;

			// Token: 0x04001525 RID: 5413
			internal readonly X509Certificate certificate;

			// Token: 0x04001526 RID: 5414
			internal readonly X509Chain chain;

			// Token: 0x04001527 RID: 5415
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x04001528 RID: 5416
			internal bool result;
		}
	}
}
