using System;

namespace System.Net.Security
{
	/// <summary>The EncryptionPolicy to use. </summary>
	// Token: 0x02000666 RID: 1638
	public enum EncryptionPolicy
	{
		/// <summary>Require encryption and never allow a NULL cipher.</summary>
		// Token: 0x04001FDC RID: 8156
		RequireEncryption,
		/// <summary>Prefer that full encryption be used, but allow a NULL cipher (no encryption) if the server agrees. </summary>
		// Token: 0x04001FDD RID: 8157
		AllowNoEncryption,
		/// <summary>Allow no encryption and request that a NULL cipher be used if the other endpoint can handle a NULL cipher.</summary>
		// Token: 0x04001FDE RID: 8158
		NoEncryption
	}
}
