using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000013 RID: 19
	public class BadGatewayException : Exception
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00003610 File Offset: 0x00001810
		public BadGatewayException(string data)
			: base(data)
		{
		}
	}
}
