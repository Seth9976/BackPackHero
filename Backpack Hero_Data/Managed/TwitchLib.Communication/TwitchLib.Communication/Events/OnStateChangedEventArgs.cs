using System;

namespace TwitchLib.Communication.Events
{
	// Token: 0x02000010 RID: 16
	public class OnStateChangedEventArgs : EventArgs
	{
		// Token: 0x0400002B RID: 43
		public bool IsConnected;

		// Token: 0x0400002C RID: 44
		public bool WasConnected;
	}
}
