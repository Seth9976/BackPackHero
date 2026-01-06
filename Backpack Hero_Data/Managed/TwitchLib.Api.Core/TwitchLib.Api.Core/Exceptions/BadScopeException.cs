using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000017 RID: 23
	public class BadScopeException : Exception
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003634 File Offset: 0x00001834
		public BadScopeException(string data)
			: base(data)
		{
		}
	}
}
