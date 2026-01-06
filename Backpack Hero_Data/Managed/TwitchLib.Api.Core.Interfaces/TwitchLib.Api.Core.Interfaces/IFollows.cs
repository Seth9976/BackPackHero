using System;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000005 RID: 5
	public interface IFollows
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000013 RID: 19
		int Total { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000014 RID: 20
		string Cursor { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000015 RID: 21
		IFollow[] Follows { get; }
	}
}
