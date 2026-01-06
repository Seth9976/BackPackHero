using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000015 RID: 21
	public class BadRequestException : Exception
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00003622 File Offset: 0x00001822
		public BadRequestException(string apiData)
			: base(apiData)
		{
		}
	}
}
