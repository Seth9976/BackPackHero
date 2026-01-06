using System;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200000A RID: 10
	public class Capabilities
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00004018 File Offset: 0x00002218
		public bool Membership { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00004020 File Offset: 0x00002220
		public bool Tags { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00004028 File Offset: 0x00002228
		public bool Commands { get; }

		// Token: 0x06000062 RID: 98 RVA: 0x00004030 File Offset: 0x00002230
		public Capabilities(bool membership = true, bool tags = true, bool commands = true)
		{
			this.Membership = membership;
			this.Tags = tags;
			this.Commands = commands;
		}
	}
}
