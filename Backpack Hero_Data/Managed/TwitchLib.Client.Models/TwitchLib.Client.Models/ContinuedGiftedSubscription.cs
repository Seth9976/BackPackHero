using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200000B RID: 11
	public class ContinuedGiftedSubscription
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000063 RID: 99 RVA: 0x0000404D File Offset: 0x0000224D
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00004055 File Offset: 0x00002255
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000405D File Offset: 0x0000225D
		public string Color { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004065 File Offset: 0x00002265
		public string DisplayName { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000406D File Offset: 0x0000226D
		public string Emotes { get; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00004075 File Offset: 0x00002275
		public string Flags { get; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000407D File Offset: 0x0000227D
		public string Id { get; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004085 File Offset: 0x00002285
		public string Login { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600006B RID: 107 RVA: 0x0000408D File Offset: 0x0000228D
		public bool IsModerator { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00004095 File Offset: 0x00002295
		public string MsgId { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600006D RID: 109 RVA: 0x0000409D File Offset: 0x0000229D
		public string MsgParamSenderLogin { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000040A5 File Offset: 0x000022A5
		public string MsgParamSenderName { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000040AD File Offset: 0x000022AD
		public string RoomId { get; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000040B5 File Offset: 0x000022B5
		public bool IsSubscriber { get; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000040BD File Offset: 0x000022BD
		public string SystemMsg { get; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000040C5 File Offset: 0x000022C5
		public string TmiSentTs { get; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000073 RID: 115 RVA: 0x000040CD File Offset: 0x000022CD
		public string UserId { get; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000074 RID: 116 RVA: 0x000040D5 File Offset: 0x000022D5
		public UserType UserType { get; }

		// Token: 0x06000075 RID: 117 RVA: 0x000040E0 File Offset: 0x000022E0
		public ContinuedGiftedSubscription(IrcMessage ircMessage)
		{
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2624027180U)
					{
						if (num <= 926444256U)
						{
							if (num <= 666658920U)
							{
								if (num != 406370129U)
								{
									if (num == 666658920U)
									{
										if (text == "msg-param-sender-login")
										{
											this.MsgParamSenderLogin = text2;
										}
									}
								}
								else if (text == "badges")
								{
									this.Badges = Helpers.ParseBadges(text2);
								}
							}
							else if (num != 852145304U)
							{
								if (num == 926444256U)
								{
									if (text == "id")
									{
										this.Id = text2;
									}
								}
							}
							else if (text == "user-id")
							{
								this.UserId = text2;
							}
						}
						else if (num <= 1935600968U)
						{
							if (num != 1031692888U)
							{
								if (num == 1935600968U)
								{
									if (text == "msg-id")
									{
										this.MsgId = text2;
									}
								}
							}
							else if (text == "color")
							{
								this.Color = text2;
							}
						}
						else if (num != 2443343188U)
						{
							if (num != 2559532549U)
							{
								if (num == 2624027180U)
								{
									if (text == "flags")
									{
										this.Flags = text2;
									}
								}
							}
							else if (text == "user-type")
							{
								if (text2 != null)
								{
									if (text2 == "mod")
									{
										this.UserType = UserType.Moderator;
										continue;
									}
									if (text2 == "global_mod")
									{
										this.UserType = UserType.GlobalModerator;
										continue;
									}
									if (text2 == "admin")
									{
										this.UserType = UserType.Admin;
										continue;
									}
									if (text2 == "staff")
									{
										this.UserType = UserType.Staff;
										continue;
									}
								}
								this.UserType = UserType.Viewer;
							}
						}
						else if (text == "system-msg")
						{
							this.SystemMsg = text2;
						}
					}
					else if (num <= 3327543824U)
					{
						if (num <= 3103773643U)
						{
							if (num != 2778540304U)
							{
								if (num == 3103773643U)
								{
									if (text == "display-name")
									{
										this.DisplayName = text2;
									}
								}
							}
							else if (text == "emotes")
							{
								this.Emotes = text2;
							}
						}
						else if (num != 3158054904U)
						{
							if (num == 3327543824U)
							{
								if (text == "msg-param-sender-name")
								{
									this.MsgParamSenderName = text2;
								}
							}
						}
						else if (text == "tmi-sent-ts")
						{
							this.TmiSentTs = text2;
						}
					}
					else if (num <= 3751703171U)
					{
						if (num != 3380555570U)
						{
							if (num == 3751703171U)
							{
								if (text == "mod")
								{
									this.IsModerator = Helpers.ConvertToBool(text2);
								}
							}
						}
						else if (text == "login")
						{
							this.Login = text2;
						}
					}
					else if (num != 3803092688U)
					{
						if (num != 3863694779U)
						{
							if (num == 3878629347U)
							{
								if (text == "subscriber")
								{
									this.IsSubscriber = Helpers.ConvertToBool(text2);
								}
							}
						}
						else if (text == "badge-info")
						{
							this.BadgeInfo = Helpers.ParseBadges(text2);
						}
					}
					else if (text == "room-id")
					{
						this.RoomId = text2;
					}
				}
			}
		}
	}
}
