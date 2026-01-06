using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the level of access allowed to a Simple Mail Transport Protocol (SMTP) server.</summary>
	// Token: 0x0200063B RID: 1595
	public enum SmtpAccess
	{
		/// <summary>No access to an SMTP host.</summary>
		// Token: 0x04001F2B RID: 7979
		None,
		/// <summary>Connection to an SMTP host on the default port (port 25).</summary>
		// Token: 0x04001F2C RID: 7980
		Connect,
		/// <summary>Connection to an SMTP host on any port.</summary>
		// Token: 0x04001F2D RID: 7981
		ConnectToUnrestrictedPort
	}
}
