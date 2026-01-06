using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001E RID: 30
	public sealed class TooManyRequestsException : Exception
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003674 File Offset: 0x00001874
		public TooManyRequestsException(string data, string resetTime)
			: base(data)
		{
			double num;
			if (double.TryParse(resetTime, ref num))
			{
				this.Data.Add("Ratelimit-Reset", num);
			}
		}
	}
}
