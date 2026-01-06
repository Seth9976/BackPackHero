using System;

namespace TwitchLib.Api.Core.Exceptions
{
	// Token: 0x0200001B RID: 27
	public class InternalServerErrorException : Exception
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00003658 File Offset: 0x00001858
		public InternalServerErrorException(string data)
			: base(data)
		{
		}
	}
}
