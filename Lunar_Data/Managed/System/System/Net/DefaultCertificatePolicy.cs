using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000484 RID: 1156
	internal class DefaultCertificatePolicy : ICertificatePolicy
	{
		// Token: 0x06002465 RID: 9317 RVA: 0x000865B1 File Offset: 0x000847B1
		public bool CheckValidationResult(ServicePoint point, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return ServicePointManager.ServerCertificateValidationCallback != null || (certificateProblem == -2146762495 || certificateProblem == 0);
		}
	}
}
