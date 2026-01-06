using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x0200001E RID: 30
	public class ErrorLoggingInException : Exception
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00007E21 File Offset: 0x00006021
		// (set) Token: 0x060001FE RID: 510 RVA: 0x00007E29 File Offset: 0x00006029
		public string Username { get; protected set; }

		// Token: 0x060001FF RID: 511 RVA: 0x00007E32 File Offset: 0x00006032
		public ErrorLoggingInException(string ircData, string twitchUsername)
			: base(ircData)
		{
			this.Username = twitchUsername;
		}
	}
}
