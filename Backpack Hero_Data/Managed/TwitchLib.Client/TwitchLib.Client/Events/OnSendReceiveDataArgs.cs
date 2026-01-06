using System;
using TwitchLib.Client.Enums;

namespace TwitchLib.Client.Events
{
	// Token: 0x0200004B RID: 75
	public class OnSendReceiveDataArgs : EventArgs
	{
		// Token: 0x040000A1 RID: 161
		public SendReceiveDirection Direction;

		// Token: 0x040000A2 RID: 162
		public string Data;
	}
}
