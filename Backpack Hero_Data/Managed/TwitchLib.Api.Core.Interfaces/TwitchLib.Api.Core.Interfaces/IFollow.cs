using System;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000004 RID: 4
	public interface IFollow
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16
		DateTime CreatedAt { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17
		bool Notifications { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000012 RID: 18
		IUser User { get; }
	}
}
