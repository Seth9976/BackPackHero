using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000018 RID: 24
	public class RaidNotification
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x000054C4 File Offset: 0x000036C4
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x000054CC File Offset: 0x000036CC
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x000054D4 File Offset: 0x000036D4
		public string Color { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000054DC File Offset: 0x000036DC
		public string DisplayName { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x000054E4 File Offset: 0x000036E4
		public string Emotes { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000054EC File Offset: 0x000036EC
		public string Id { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x000054F4 File Offset: 0x000036F4
		public string Login { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000054FC File Offset: 0x000036FC
		public bool Moderator { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005504 File Offset: 0x00003704
		public string MsgId { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000550C File Offset: 0x0000370C
		public string MsgParamDisplayName { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005514 File Offset: 0x00003714
		public string MsgParamLogin { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000551C File Offset: 0x0000371C
		public string MsgParamViewerCount { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005524 File Offset: 0x00003724
		public string RoomId { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000552C File Offset: 0x0000372C
		public bool Subscriber { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005534 File Offset: 0x00003734
		public string SystemMsg { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000553C File Offset: 0x0000373C
		public string SystemMsgParsed { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005544 File Offset: 0x00003744
		public string TmiSentTs { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000554C File Offset: 0x0000374C
		public bool Turbo { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005554 File Offset: 0x00003754
		public string UserId { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x0000555C File Offset: 0x0000375C
		public UserType UserType { get; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00005564 File Offset: 0x00003764
		public RaidNotification(IrcMessage ircMessage)
		{
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 3031372082U)
					{
						if (num <= 1935600968U)
						{
							if (num <= 852145304U)
							{
								if (num != 406370129U)
								{
									if (num == 852145304U)
									{
										if (text == "user-id")
										{
											this.UserId = text2;
										}
									}
								}
								else if (text == "badges")
								{
									this.Badges = Helpers.ParseBadges(text2);
								}
							}
							else if (num != 1031692888U)
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
						else if (num <= 2559532549U)
						{
							if (num != 2443343188U)
							{
								if (num == 2559532549U)
								{
									if (text == "user-type")
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
							}
							else if (text == "system-msg")
							{
								this.SystemMsg = text2;
								this.SystemMsgParsed = text2.Replace("\\s", " ").Replace("\\n", "");
							}
						}
						else if (num != 2572642902U)
						{
							if (num != 2778540304U)
							{
								if (num == 3031372082U)
								{
									if (text == "msg-param-login")
									{
										this.MsgParamLogin = text2;
									}
								}
							}
							else if (text == "emotes")
							{
								this.Emotes = text2;
							}
						}
						else if (text == "msg-param-viewerCount")
						{
							this.MsgParamViewerCount = text2;
						}
					}
					else if (num <= 3466737750U)
					{
						if (num <= 3158054904U)
						{
							if (num != 3103773643U)
							{
								if (num == 3158054904U)
								{
									if (text == "tmi-sent-ts")
									{
										this.TmiSentTs = text2;
									}
								}
							}
							else if (text == "display-name")
							{
								this.DisplayName = text2;
							}
						}
						else if (num != 3380555570U)
						{
							if (num == 3466737750U)
							{
								if (text == "msg-param-displayName")
								{
									this.MsgParamDisplayName = text2;
								}
							}
						}
						else if (text == "login")
						{
							this.Login = text2;
						}
					}
					else if (num <= 3751703171U)
					{
						if (num != 3477026043U)
						{
							if (num == 3751703171U)
							{
								if (text == "mod")
								{
									this.Moderator = Helpers.ConvertToBool(text2);
								}
							}
						}
						else if (text == "turbo")
						{
							this.Turbo = Helpers.ConvertToBool(text2);
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
									this.Subscriber = Helpers.ConvertToBool(text2);
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

		// Token: 0x060000F5 RID: 245 RVA: 0x000059EC File Offset: 0x00003BEC
		public RaidNotification(List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool moderator, string msgId, string msgParamDisplayName, string msgParamLogin, string msgParamViewerCount, string roomId, bool subscriber, string systemMsg, string systemMsgParsed, string tmiSentTs, bool turbo, UserType userType, string userId)
		{
			this.Badges = badges;
			this.BadgeInfo = badgeInfo;
			this.Color = color;
			this.DisplayName = displayName;
			this.Emotes = emotes;
			this.Id = id;
			this.Login = login;
			this.Moderator = moderator;
			this.MsgId = msgId;
			this.MsgParamDisplayName = msgParamDisplayName;
			this.MsgParamLogin = msgParamLogin;
			this.MsgParamViewerCount = msgParamViewerCount;
			this.RoomId = roomId;
			this.Subscriber = subscriber;
			this.SystemMsg = systemMsg;
			this.SystemMsgParsed = systemMsgParsed;
			this.TmiSentTs = tmiSentTs;
			this.Turbo = turbo;
			this.UserType = userType;
			this.UserId = userId;
		}
	}
}
