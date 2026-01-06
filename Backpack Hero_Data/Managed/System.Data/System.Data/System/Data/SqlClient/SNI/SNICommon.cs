using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace System.Data.SqlClient.SNI
{
	// Token: 0x0200023D RID: 573
	internal class SNICommon
	{
		// Token: 0x06001A3B RID: 6715 RVA: 0x000836F0 File Offset: 0x000818F0
		internal static bool ValidateSslServerCertificate(string targetServerName, object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors policyErrors)
		{
			if (policyErrors == SslPolicyErrors.None)
			{
				return true;
			}
			if ((policyErrors & SslPolicyErrors.RemoteCertificateNameMismatch) == SslPolicyErrors.None)
			{
				return false;
			}
			string text = cert.Subject.Substring(cert.Subject.IndexOf('=') + 1);
			if (targetServerName.Length > text.Length)
			{
				return false;
			}
			if (targetServerName.Length == text.Length)
			{
				if (!targetServerName.Equals(text, StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
			}
			else
			{
				if (string.Compare(targetServerName, 0, text, 0, targetServerName.Length, StringComparison.OrdinalIgnoreCase) != 0)
				{
					return false;
				}
				if (text[targetServerName.Length] != '.')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001A3C RID: 6716 RVA: 0x00083777 File Offset: 0x00081977
		internal static uint ReportSNIError(SNIProviders provider, uint nativeError, uint sniError, string errorMessage)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, nativeError, sniError, errorMessage));
		}

		// Token: 0x06001A3D RID: 6717 RVA: 0x00083787 File Offset: 0x00081987
		internal static uint ReportSNIError(SNIProviders provider, uint sniError, Exception sniException)
		{
			return SNICommon.ReportSNIError(new SNIError(provider, sniError, sniException));
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x00083796 File Offset: 0x00081996
		internal static uint ReportSNIError(SNIError error)
		{
			SNILoadHandle.SingletonInstance.LastError = error;
			return 1U;
		}

		// Token: 0x040012D4 RID: 4820
		internal const int ConnTerminatedError = 2;

		// Token: 0x040012D5 RID: 4821
		internal const int InvalidParameterError = 5;

		// Token: 0x040012D6 RID: 4822
		internal const int ProtocolNotSupportedError = 8;

		// Token: 0x040012D7 RID: 4823
		internal const int ConnTimeoutError = 11;

		// Token: 0x040012D8 RID: 4824
		internal const int ConnNotUsableError = 19;

		// Token: 0x040012D9 RID: 4825
		internal const int InvalidConnStringError = 25;

		// Token: 0x040012DA RID: 4826
		internal const int HandshakeFailureError = 31;

		// Token: 0x040012DB RID: 4827
		internal const int InternalExceptionError = 35;

		// Token: 0x040012DC RID: 4828
		internal const int ConnOpenFailedError = 40;

		// Token: 0x040012DD RID: 4829
		internal const int ErrorSpnLookup = 44;

		// Token: 0x040012DE RID: 4830
		internal const int LocalDBErrorCode = 50;

		// Token: 0x040012DF RID: 4831
		internal const int MultiSubnetFailoverWithMoreThan64IPs = 47;

		// Token: 0x040012E0 RID: 4832
		internal const int MultiSubnetFailoverWithInstanceSpecified = 48;

		// Token: 0x040012E1 RID: 4833
		internal const int MultiSubnetFailoverWithNonTcpProtocol = 49;

		// Token: 0x040012E2 RID: 4834
		internal const int MaxErrorValue = 50157;

		// Token: 0x040012E3 RID: 4835
		internal const int LocalDBNoInstanceName = 51;

		// Token: 0x040012E4 RID: 4836
		internal const int LocalDBNoInstallation = 52;

		// Token: 0x040012E5 RID: 4837
		internal const int LocalDBInvalidConfig = 53;

		// Token: 0x040012E6 RID: 4838
		internal const int LocalDBNoSqlUserInstanceDllPath = 54;

		// Token: 0x040012E7 RID: 4839
		internal const int LocalDBInvalidSqlUserInstanceDllPath = 55;

		// Token: 0x040012E8 RID: 4840
		internal const int LocalDBFailedToLoadDll = 56;

		// Token: 0x040012E9 RID: 4841
		internal const int LocalDBBadRuntime = 57;
	}
}
