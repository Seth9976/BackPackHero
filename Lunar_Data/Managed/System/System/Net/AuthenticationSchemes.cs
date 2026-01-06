using System;

namespace System.Net
{
	/// <summary>Specifies protocols for authentication.</summary>
	// Token: 0x020003D3 RID: 979
	[Flags]
	public enum AuthenticationSchemes
	{
		/// <summary>No authentication is allowed. A client requesting an <see cref="T:System.Net.HttpListener" /> object with this flag set will always receive a 403 Forbidden status. Use this flag when a resource should never be served to a client.</summary>
		// Token: 0x04001123 RID: 4387
		None = 0,
		/// <summary>Specifies digest authentication.</summary>
		// Token: 0x04001124 RID: 4388
		Digest = 1,
		/// <summary>Negotiates with the client to determine the authentication scheme. If both client and server support Kerberos, it is used; otherwise, NTLM is used.</summary>
		// Token: 0x04001125 RID: 4389
		Negotiate = 2,
		/// <summary>Specifies NTLM authentication.</summary>
		// Token: 0x04001126 RID: 4390
		Ntlm = 4,
		/// <summary>Specifies basic authentication. </summary>
		// Token: 0x04001127 RID: 4391
		Basic = 8,
		/// <summary>Specifies anonymous authentication.</summary>
		// Token: 0x04001128 RID: 4392
		Anonymous = 32768,
		/// <summary>Specifies Windows authentication.</summary>
		// Token: 0x04001129 RID: 4393
		IntegratedWindowsAuthentication = 6
	}
}
