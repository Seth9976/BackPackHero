using System;
using TwitchLib.Client.Exceptions;

namespace TwitchLib.Client.Events
{
	// Token: 0x02000035 RID: 53
	public class OnFailureToReceiveJoinConfirmationArgs : EventArgs
	{
		// Token: 0x04000076 RID: 118
		public FailureToReceiveJoinConfirmationException Exception;
	}
}
