using System;

namespace TwitchLib.PubSub.Models
{
	// Token: 0x02000003 RID: 3
	public class LeaderBoard
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004FBA File Offset: 0x000031BA
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004FC2 File Offset: 0x000031C2
		public int Place { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004FCB File Offset: 0x000031CB
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00004FD3 File Offset: 0x000031D3
		public int Score { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004FDC File Offset: 0x000031DC
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00004FE4 File Offset: 0x000031E4
		public string UserId { get; set; }
	}
}
