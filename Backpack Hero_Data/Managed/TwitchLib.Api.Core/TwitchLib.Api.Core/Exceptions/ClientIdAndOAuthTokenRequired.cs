using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000019 RID: 25
	public class ClientIdAndOAuthTokenRequired : Exception
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00003646 File Offset: 0x00001846
		public ClientIdAndOAuthTokenRequired(string explanation)
			: base(explanation)
		{
		}
	}
}
