using System;

namespace System.Net.Security
{
	/// <summary>Indicates the security services requested for an authenticated stream.</summary>
	// Token: 0x02000665 RID: 1637
	public enum ProtectionLevel
	{
		/// <summary>Authentication only.</summary>
		// Token: 0x04001FD8 RID: 8152
		None,
		/// <summary>Sign data to help ensure the integrity of transmitted data.</summary>
		// Token: 0x04001FD9 RID: 8153
		Sign,
		/// <summary>Encrypt and sign data to help ensure the confidentiality and integrity of transmitted data.</summary>
		// Token: 0x04001FDA RID: 8154
		EncryptAndSign
	}
}
