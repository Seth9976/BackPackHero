using System;

namespace System.Net
{
	/// <summary>Specifies the security protocols that are supported by the Schannel security package.</summary>
	// Token: 0x020003A7 RID: 935
	[Flags]
	public enum SecurityProtocolType
	{
		// Token: 0x04001076 RID: 4214
		SystemDefault = 0,
		/// <summary>Specifies the Secure Socket Layer (SSL) 3.0 security protocol.</summary>
		// Token: 0x04001077 RID: 4215
		Ssl3 = 48,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.0 security protocol.</summary>
		// Token: 0x04001078 RID: 4216
		Tls = 192,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.1 security protocol.</summary>
		// Token: 0x04001079 RID: 4217
		Tls11 = 768,
		/// <summary>Specifies the Transport Layer Security (TLS) 1.2 security protocol.</summary>
		// Token: 0x0400107A RID: 4218
		Tls12 = 3072,
		// Token: 0x0400107B RID: 4219
		Tls13 = 12288
	}
}
