using System;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Core.Models.Undocumented.Chatters
{
	// Token: 0x02000003 RID: 3
	public class ChatterFormatted
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public string Username { get; protected set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		// (set) Token: 0x06000005 RID: 5 RVA: 0x00002071 File Offset: 0x00000271
		public UserType UserType { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public ChatterFormatted(string username, UserType userType)
		{
			this.Username = username;
			this.UserType = userType;
		}
	}
}
