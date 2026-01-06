using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001A RID: 26
	public class GatewayTimeoutException : Exception
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000364F File Offset: 0x0000184F
		public GatewayTimeoutException(string data)
			: base(data)
		{
		}
	}
}
