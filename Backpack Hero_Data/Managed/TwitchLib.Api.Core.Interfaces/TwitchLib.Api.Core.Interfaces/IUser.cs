using System;

namespace TwitchLib.Api.Core.Interfaces
{
	// Token: 0x02000009 RID: 9
	public interface IUser
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000023 RID: 35
		string Id { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000024 RID: 36
		string Bio { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37
		DateTime CreatedAt { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000026 RID: 38
		string DisplayName { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000027 RID: 39
		string Logo { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000028 RID: 40
		string Name { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000029 RID: 41
		string Type { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002A RID: 42
		DateTime UpdatedAt { get; }
	}
}
