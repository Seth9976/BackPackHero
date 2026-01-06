using System;

namespace TwitchLib.Communication.Events
{
	// Token: 0x0200000F RID: 15
	public class OnSendFailedEventArgs : EventArgs
	{
		// Token: 0x04000029 RID: 41
		public string Data;

		// Token: 0x0400002A RID: 42
		public Exception Exception;
	}
}
