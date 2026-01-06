using System;
using System.Globalization;

namespace System.Net
{
	// Token: 0x020003DA RID: 986
	internal class CredentialKey
	{
		// Token: 0x0600206B RID: 8299 RVA: 0x00076D41 File Offset: 0x00074F41
		internal CredentialKey(Uri uriPrefix, string authenticationType)
		{
			this.UriPrefix = uriPrefix;
			this.UriPrefixLength = this.UriPrefix.ToString().Length;
			this.AuthenticationType = authenticationType;
		}

		// Token: 0x0600206C RID: 8300 RVA: 0x00076D74 File Offset: 0x00074F74
		internal bool Match(Uri uri, string authenticationType)
		{
			return !(uri == null) && authenticationType != null && string.Compare(authenticationType, this.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.IsPrefix(uri, this.UriPrefix);
		}

		// Token: 0x0600206D RID: 8301 RVA: 0x00076DA4 File Offset: 0x00074FA4
		internal bool IsPrefix(Uri uri, Uri prefixUri)
		{
			if (prefixUri.Scheme != uri.Scheme || prefixUri.Host != uri.Host || prefixUri.Port != uri.Port)
			{
				return false;
			}
			int num = prefixUri.AbsolutePath.LastIndexOf('/');
			return num <= uri.AbsolutePath.LastIndexOf('/') && string.Compare(uri.AbsolutePath, 0, prefixUri.AbsolutePath, 0, num, StringComparison.OrdinalIgnoreCase) == 0;
		}

		// Token: 0x0600206E RID: 8302 RVA: 0x00076E1F File Offset: 0x0007501F
		public override int GetHashCode()
		{
			if (!this.m_ComputedHashCode)
			{
				this.m_HashCode = this.AuthenticationType.ToUpperInvariant().GetHashCode() + this.UriPrefixLength + this.UriPrefix.GetHashCode();
				this.m_ComputedHashCode = true;
			}
			return this.m_HashCode;
		}

		// Token: 0x0600206F RID: 8303 RVA: 0x00076E60 File Offset: 0x00075060
		public override bool Equals(object comparand)
		{
			CredentialKey credentialKey = comparand as CredentialKey;
			return comparand != null && string.Compare(this.AuthenticationType, credentialKey.AuthenticationType, StringComparison.OrdinalIgnoreCase) == 0 && this.UriPrefix.Equals(credentialKey.UriPrefix);
		}

		// Token: 0x06002070 RID: 8304 RVA: 0x00076EA0 File Offset: 0x000750A0
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[",
				this.UriPrefixLength.ToString(NumberFormatInfo.InvariantInfo),
				"]:",
				ValidationHelper.ToString(this.UriPrefix),
				":",
				ValidationHelper.ToString(this.AuthenticationType)
			});
		}

		// Token: 0x0400113E RID: 4414
		internal Uri UriPrefix;

		// Token: 0x0400113F RID: 4415
		internal int UriPrefixLength = -1;

		// Token: 0x04001140 RID: 4416
		internal string AuthenticationType;

		// Token: 0x04001141 RID: 4417
		private int m_HashCode;

		// Token: 0x04001142 RID: 4418
		private bool m_ComputedHashCode;
	}
}
