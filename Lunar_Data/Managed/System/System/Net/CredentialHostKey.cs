using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020003D9 RID: 985
	internal class CredentialHostKey
	{
		// Token: 0x06002066 RID: 8294 RVA: 0x00076BC0 File Offset: 0x00074DC0
		internal CredentialHostKey(string host, int port, string authenticationType)
		{
			this.Host = host;
			this.Port = port;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x06002067 RID: 8295 RVA: 0x00076BDD File Offset: 0x00074DDD
		internal bool Match(string host, int port, string authenticationType)
		{
			return host != null && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, host, StringComparison.OrdinalIgnoreCase) == 0 && port == this.Port;
		}

		// Token: 0x06002068 RID: 8296 RVA: 0x00076C18 File Offset: 0x00074E18
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.Host.ToUpperInvariant().GetHashCode() + this.Port.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x06002069 RID: 8297 RVA: 0x00076C70 File Offset: 0x00074E70
		public override bool Equals(object comparand)
		{
			CredentialHostKey credentialHostKey = comparand as CredentialHostKey;
			return comparand != null && (string.Compare(this.AuthenticationType, credentialHostKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && string.Compare(this.Host, credentialHostKey.Host, StringComparison.OrdinalIgnoreCase) == 0) && this.Port == credentialHostKey.Port;
		}

		// Token: 0x0600206A RID: 8298 RVA: 0x00076CC4 File Offset: 0x00074EC4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.Host.Length.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				this.Host,
				":",
				this.Port.ToString(NumberFormatInfo.InvariantInfo),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x04001139 RID: 4409
		internal string Host;

		// Token: 0x0400113A RID: 4410
		internal string AuthenticationType;

		// Token: 0x0400113B RID: 4411
		internal int Port;

		// Token: 0x0400113C RID: 4412
		private int m_HashCode;

		// Token: 0x0400113D RID: 4413
		private bool m_ComputedHashCode;
	}
}
