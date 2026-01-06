using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200000F RID: 15
	public class GiftedSubscription
	{
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000461B File Offset: 0x0000281B
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00004623 File Offset: 0x00002823
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000085 RID: 133 RVA: 0x0000462B File Offset: 0x0000282B
		public string Color { get; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00004633 File Offset: 0x00002833
		public string DisplayName { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000087 RID: 135 RVA: 0x0000463B File Offset: 0x0000283B
		public string Emotes { get; }

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00004643 File Offset: 0x00002843
		public string Id { get; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000464B File Offset: 0x0000284B
		public bool IsModerator { get; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004653 File Offset: 0x00002853
		public bool IsSubscriber { get; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000465B File Offset: 0x0000285B
		public bool IsTurbo { get; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004663 File Offset: 0x00002863
		public bool IsAnonymous { get; }

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600008D RID: 141 RVA: 0x0000466B File Offset: 0x0000286B
		public string Login { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004673 File Offset: 0x00002873
		public string MsgId { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600008F RID: 143 RVA: 0x0000467B File Offset: 0x0000287B
		public string MsgParamMonths { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004683 File Offset: 0x00002883
		public string MsgParamRecipientDisplayName { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000091 RID: 145 RVA: 0x0000468B File Offset: 0x0000288B
		public string MsgParamRecipientId { get; }

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004693 File Offset: 0x00002893
		public string MsgParamRecipientUserName { get; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000469B File Offset: 0x0000289B
		public string MsgParamSubPlanName { get; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000046A3 File Offset: 0x000028A3
		public SubscriptionPlan MsgParamSubPlan { get; }

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000046AB File Offset: 0x000028AB
		public string RoomId { get; }

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000046B3 File Offset: 0x000028B3
		public string SystemMsg { get; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000046BB File Offset: 0x000028BB
		public string SystemMsgParsed { get; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000046C3 File Offset: 0x000028C3
		public string TmiSentTs { get; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000046CB File Offset: 0x000028CB
		public string UserId { get; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000046D3 File Offset: 0x000028D3
		public UserType UserType { get; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000046DB File Offset: 0x000028DB
		public string MsgParamMultiMonthGiftDuration { get; }

		// Token: 0x0600009C RID: 156 RVA: 0x000046E4 File Offset: 0x000028E4
		public GiftedSubscription(IrcMessage ircMessage)
		{
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2559532549U)
					{
						if (num <= 926444256U)
						{
							if (num <= 406370129U)
							{
								if (num != 55742466U)
								{
									if (num == 406370129U)
									{
										if (text == "badges")
										{
											this.Badges = Helpers.ParseBadges(text2);
										}
									}
								}
								else if (text == "msg-param-recipient-id")
								{
									this.MsgParamRecipientId = text2;
								}
							}
							else if (num != 812236584U)
							{
								if (num != 852145304U)
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
									if (this.UserId == "274598607")
									{
										this.IsAnonymous = true;
									}
								}
							}
							else if (text == "msg-param-recipient-user-name")
							{
								this.MsgParamRecipientUserName = text2;
							}
						}
						else if (num <= 1417212354U)
						{
							if (num != 934351981U)
							{
								if (num != 1031692888U)
								{
									if (num == 1417212354U)
									{
										if (text == "msg-param-months")
										{
											this.MsgParamMonths = text2;
										}
									}
								}
								else if (text == "color")
								{
									this.Color = text2;
								}
							}
							else if (text == "msg-param-recipient-display-name")
							{
								this.MsgParamRecipientDisplayName = text2;
							}
						}
						else if (num != 1935600968U)
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
						else if (text == "msg-id")
						{
							this.MsgId = text2;
						}
					}
					else if (num <= 3380555570U)
					{
						if (num <= 3158054904U)
						{
							if (num != 2778540304U)
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
							else if (text == "emotes")
							{
								this.Emotes = text2;
							}
						}
						else if (num != 3176240347U)
						{
							if (num != 3379711929U)
							{
								if (num == 3380555570U)
								{
									if (text == "login")
									{
										this.Login = text2;
									}
								}
							}
							else if (text == "msg-param-gift-months")
							{
								this.MsgParamMultiMonthGiftDuration = text2;
							}
						}
						else if (text == "msg-param-sub-plan")
						{
							if (text2 != null)
							{
								if (text2 == "prime")
								{
									this.MsgParamSubPlan = SubscriptionPlan.Prime;
									continue;
								}
								if (text2 == "1000")
								{
									this.MsgParamSubPlan = SubscriptionPlan.Tier1;
									continue;
								}
								if (text2 == "2000")
								{
									this.MsgParamSubPlan = SubscriptionPlan.Tier2;
									continue;
								}
								if (text2 == "3000")
								{
									this.MsgParamSubPlan = SubscriptionPlan.Tier3;
									continue;
								}
							}
							throw new ArgumentOutOfRangeException("ToLower");
						}
					}
					else if (num <= 3803092688U)
					{
						if (num != 3477026043U)
						{
							if (num != 3751703171U)
							{
								if (num == 3803092688U)
								{
									if (text == "room-id")
									{
										this.RoomId = text2;
									}
								}
							}
							else if (text == "mod")
							{
								this.IsModerator = Helpers.ConvertToBool(text2);
							}
						}
						else if (text == "turbo")
						{
							this.IsTurbo = Helpers.ConvertToBool(text2);
						}
					}
					else if (num != 3863694779U)
					{
						if (num != 3878629347U)
						{
							if (num == 4083234825U)
							{
								if (text == "msg-param-sub-plan-name")
								{
									this.MsgParamSubPlanName = text2;
								}
							}
						}
						else if (text == "subscriber")
						{
							this.IsSubscriber = Helpers.ConvertToBool(text2);
						}
					}
					else if (text == "badge-info")
					{
						this.BadgeInfo = Helpers.ParseBadges(text2);
					}
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004CCC File Offset: 0x00002ECC
		public GiftedSubscription(List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string color, string displayName, string emotes, string id, string login, bool isModerator, string msgId, string msgParamMonths, string msgParamRecipientDisplayName, string msgParamRecipientId, string msgParamRecipientUserName, string msgParamSubPlanName, string msgMultiMonthDuration, SubscriptionPlan msgParamSubPlan, string roomId, bool isSubscriber, string systemMsg, string systemMsgParsed, string tmiSentTs, bool isTurbo, UserType userType, string userId)
		{
			this.Badges = badges;
			this.BadgeInfo = badgeInfo;
			this.Color = color;
			this.DisplayName = displayName;
			this.Emotes = emotes;
			this.Id = id;
			this.Login = login;
			this.IsModerator = isModerator;
			this.MsgId = msgId;
			this.MsgParamMonths = msgParamMonths;
			this.MsgParamRecipientDisplayName = msgParamRecipientDisplayName;
			this.MsgParamRecipientId = msgParamRecipientId;
			this.MsgParamRecipientUserName = msgParamRecipientUserName;
			this.MsgParamSubPlanName = msgParamSubPlanName;
			this.MsgParamSubPlan = msgParamSubPlan;
			this.MsgParamMultiMonthGiftDuration = msgMultiMonthDuration;
			this.RoomId = roomId;
			this.IsSubscriber = isSubscriber;
			this.SystemMsg = systemMsg;
			this.SystemMsgParsed = systemMsgParsed;
			this.TmiSentTs = tmiSentTs;
			this.IsTurbo = isTurbo;
			this.UserType = userType;
			this.UserId = userId;
		}

		// Token: 0x04000080 RID: 128
		private const string AnonymousGifterUserId = "274598607";
	}
}
