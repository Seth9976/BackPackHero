using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x02000014 RID: 20
	public class BadParameterException : Exception
	{
		// Token: 0x0600006C RID: 108 RVA: 0x00003619 File Offset: 0x00001819
		public BadParameterException(string badParamData)
			: base(badParamData)
		{
		}
	}
}
