using System;

namespace TwitchLib.Communication.Events
{
	// Token: 0x0200000D RID: 13
	public class OnMessageThrottledEventArgs : EventArgs
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00002682 File Offset: 0x00000882
		// (set) Token: 0x0600007D RID: 125 RVA: 0x0000268A File Offset: 0x0000088A
		public string Message { get; set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002693 File Offset: 0x00000893
		// (set) Token: 0x0600007F RID: 127 RVA: 0x0000269B File Offset: 0x0000089B
		public int SentMessageCount { get; set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000026A4 File Offset: 0x000008A4
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000026AC File Offset: 0x000008AC
		public TimeSpan Period { get; set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000026B5 File Offset: 0x000008B5
		// (set) Token: 0x06000083 RID: 131 RVA: 0x000026BD File Offset: 0x000008BD
		public int AllowedInPeriod { get; set; }
	}
}
