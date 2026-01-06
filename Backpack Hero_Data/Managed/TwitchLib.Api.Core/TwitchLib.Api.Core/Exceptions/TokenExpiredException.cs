using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001D RID: 29
	public class TokenExpiredException : Exception
	{
		// Token: 0x06000075 RID: 117 RVA: 0x0000366A File Offset: 0x0000186A
		public TokenExpiredException(string data)
			: base(data)
		{
		}
	}
}
