using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001F RID: 31
	public class UnexpectedResponseException : Exception
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000036A8 File Offset: 0x000018A8
		public UnexpectedResponseException(string data)
			: base(data)
		{
		}
	}
}
