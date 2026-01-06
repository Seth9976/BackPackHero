using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000016 RID: 22
	public class BadResourceException : Exception
	{
		// Token: 0x0600006E RID: 110 RVA: 0x0000362B File Offset: 0x0000182B
		public BadResourceException(string apiData)
			: base(apiData)
		{
		}
	}
}
