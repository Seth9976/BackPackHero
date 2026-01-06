using System;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000033 RID: 51
	public class FromIrcMessageBuilderDataObject
	{
		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x000080CF File Offset: 0x000062CF
		// (set) Token: 0x060001C3 RID: 451 RVA: 0x000080D7 File Offset: 0x000062D7
		public IrcMessage Message { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000080E0 File Offset: 0x000062E0
		// (set) Token: 0x060001C5 RID: 453 RVA: 0x000080E8 File Offset: 0x000062E8
		public object AditionalData { get; set; }
	}
}
