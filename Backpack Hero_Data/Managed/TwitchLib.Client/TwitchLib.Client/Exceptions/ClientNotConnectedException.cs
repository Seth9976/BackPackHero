using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001C RID: 28
	public class ClientNotConnectedException : Exception
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00007E0F File Offset: 0x0000600F
		public ClientNotConnectedException(string description)
			: base(description)
		{
		}
	}
}
