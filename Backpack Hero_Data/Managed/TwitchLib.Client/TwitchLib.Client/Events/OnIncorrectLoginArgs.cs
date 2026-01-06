using System;
using TwitchLib.Client.Exceptions;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000038 RID: 56
	public class OnIncorrectLoginArgs : EventArgs
	{
		// Token: 0x0400007B RID: 123
		public ErrorLoggingInException Exception;
	}
}
