using System;

namespace TwitchLib.Client.Exceptions
{
	// Token: 0x02000022 RID: 34
	public class InvalidParameterException : Exception
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00007EB1 File Offset: 0x000060B1
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00007EB9 File Offset: 0x000060B9
		public string Username { get; protected set; }

		// Token: 0x06000209 RID: 521 RVA: 0x00007EC2 File Offset: 0x000060C2
		public InvalidParameterException(string reasoning, string twitchUsername)
			: base(reasoning)
		{
			this.Username = twitchUsername;
		}
	}
}
