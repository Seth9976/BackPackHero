using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001B RID: 27
	public class BadStateException : Exception
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00007E06 File Offset: 0x00006006
		public BadStateException(string details)
			: base(details)
		{
		}
	}
}
