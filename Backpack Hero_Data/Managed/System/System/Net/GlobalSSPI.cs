using System;

namespace System.Net
{
	// Token: 0x0200035F RID: 863
	internal static class GlobalSSPI
	{
		// Token: 0x04000E90 RID: 3728
		internal static readonly SSPIInterface SSPIAuth = new SSPIAuthType();

		// Token: 0x04000E91 RID: 3729
		internal static readonly SSPIInterface SSPISecureChannel = new SSPISecureChannelType();
	}
}
