using System;

namespace System.Net
{
	// Token: 0x0200037F RID: 895
	internal static class TcpValidationHelpers
	{
		// Token: 0x06001D67 RID: 7527 RVA: 0x0006B5E7 File Offset: 0x000697E7
		public static bool ValidatePortNumber(int port)
		{
			return port >= 0 && port <= 65535;
		}
	}
}
