using System;

namespace TwitchLib.Communication.Events
{
	// Token: 0x0200000A RID: 10
	public class OnErrorEventArgs : EventArgs
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002656 File Offset: 0x00000856
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000265E File Offset: 0x0000085E
		public Exception Exception { get; set; }
	}
}
