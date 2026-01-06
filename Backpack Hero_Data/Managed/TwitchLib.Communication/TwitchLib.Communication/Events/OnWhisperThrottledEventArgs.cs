using System;

namespace TwitchLib.Communication.Events
{
	// Token: 0x02000011 RID: 17
	public class OnWhisperThrottledEventArgs : EventArgs
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000026EA File Offset: 0x000008EA
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000026F2 File Offset: 0x000008F2
		public string Message { get; set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000026FB File Offset: 0x000008FB
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002703 File Offset: 0x00000903
		public int SentWhisperCount { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000270C File Offset: 0x0000090C
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002714 File Offset: 0x00000914
		public TimeSpan Period { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008E RID: 142 RVA: 0x0000271D File Offset: 0x0000091D
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00002725 File Offset: 0x00000925
		public int AllowedInPeriod { get; set; }
	}
}
