using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001D RID: 29
	public class ClientNotInitializedException : Exception
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00007E18 File Offset: 0x00006018
		public ClientNotInitializedException(string description)
			: base(description)
		{
		}
	}
}
