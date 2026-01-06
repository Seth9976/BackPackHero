using System;

namespace TwitchLib.Api.Events
{
	// Token: 0x02000023 RID: 35
	public class OnAuthorizationFlowErrorArgs
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003A97 File Offset: 0x00001C97
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003A9F File Offset: 0x00001C9F
		public int Error { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003AA8 File Offset: 0x00001CA8
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003AB0 File Offset: 0x00001CB0
		public string Message { get; set; }
	}
}
