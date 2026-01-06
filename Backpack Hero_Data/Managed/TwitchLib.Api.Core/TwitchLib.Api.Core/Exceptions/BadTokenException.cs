using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000018 RID: 24
	public class BadTokenException : Exception
	{
		// Token: 0x06000070 RID: 112 RVA: 0x0000363D File Offset: 0x0000183D
		public BadTokenException(string data)
			: base(data)
		{
		}
	}
}
