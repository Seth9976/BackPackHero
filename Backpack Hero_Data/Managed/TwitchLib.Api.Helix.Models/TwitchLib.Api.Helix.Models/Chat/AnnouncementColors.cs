using System;

namespace TwitchLib.Api.Helix.Models.Chat
{
	// Token: 0x02000098 RID: 152
	public class AnnouncementColors
	{
		// Token: 0x0600050F RID: 1295 RVA: 0x00004C0F File Offset: 0x00002E0F
		private AnnouncementColors(string value)
		{
			this.Value = value;
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00004C1E File Offset: 0x00002E1E
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00004C26 File Offset: 0x00002E26
		public string Value { get; private set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00004C2F File Offset: 0x00002E2F
		public static AnnouncementColors Blue
		{
			get
			{
				return new AnnouncementColors("blue");
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x00004C3B File Offset: 0x00002E3B
		public static AnnouncementColors Green
		{
			get
			{
				return new AnnouncementColors("green");
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00004C47 File Offset: 0x00002E47
		public static AnnouncementColors Orange
		{
			get
			{
				return new AnnouncementColors("orange");
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x00004C53 File Offset: 0x00002E53
		public static AnnouncementColors Purple
		{
			get
			{
				return new AnnouncementColors("purple");
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00004C5F File Offset: 0x00002E5F
		public static AnnouncementColors Primary
		{
			get
			{
				return new AnnouncementColors("primary");
			}
		}
	}
}
