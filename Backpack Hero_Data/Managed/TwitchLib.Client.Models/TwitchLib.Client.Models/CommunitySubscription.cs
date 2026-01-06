using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000008 RID: 8
	public class CommunitySubscription
	{
		// Token: 0x06000059 RID: 89 RVA: 0x000039EC File Offset: 0x00001BEC
		public CommunitySubscription(IrcMessage ircMessage)
		{
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2778540304U)
					{
						if (num <= 926444256U)
						{
							if (num <= 406370129U)
							{
								if (num != 164455116U)
								{
									if (num == 406370129U)
									{
										if (text == "badges")
										{
											this.Badges = Helpers.ParseBadges(text2);
										}
									}
								}
								else if (text == "msg-param-mass-gift-count")
								{
									this.MsgParamMassGiftCount = int.Parse(text2);
								}
							}
							else if (num != 434825306U)
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
							else if (text == "msg-param-sender-count")
							{
								this.MsgParamSenderCount = int.Parse(text2);
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
								if (num == 2778540304U)
								{
									if (text == "emotes")
									{
										this.Emotes = text2;
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
							this.SystemMsgParsed = text2.Replace("\\s", " ").Replace("\\n", "");
						}
					}
					else if (num <= 3380555570U)
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
					else if (num <= 3751703171U)
					{
						if (num != 3477026043U)
						{
							if (num == 3751703171U)
							{
								if (text == "mod")
								{
									this.IsModerator = Helpers.ConvertToBool(text2);
								}
							}
						}
						else if (text == "turbo")
						{
							this.IsTurbo = Helpers.ConvertToBool(text2);
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

		// Token: 0x04000047 RID: 71
		private const string AnonymousGifterUserId = "274598607";

		// Token: 0x04000048 RID: 72
		public List<KeyValuePair<string, string>> Badges;

		// Token: 0x04000049 RID: 73
		public List<KeyValuePair<string, string>> BadgeInfo;

		// Token: 0x0400004A RID: 74
		public string Color;

		// Token: 0x0400004B RID: 75
		public string DisplayName;

		// Token: 0x0400004C RID: 76
		public string Emotes;

		// Token: 0x0400004D RID: 77
		public string Id;

		// Token: 0x0400004E RID: 78
		public string Login;

		// Token: 0x0400004F RID: 79
		public bool IsModerator;

		// Token: 0x04000050 RID: 80
		public bool IsAnonymous;

		// Token: 0x04000051 RID: 81
		public string MsgId;

		// Token: 0x04000052 RID: 82
		public int MsgParamMassGiftCount;

		// Token: 0x04000053 RID: 83
		public int MsgParamSenderCount;

		// Token: 0x04000054 RID: 84
		public SubscriptionPlan MsgParamSubPlan;

		// Token: 0x04000055 RID: 85
		public string RoomId;

		// Token: 0x04000056 RID: 86
		public bool IsSubscriber;

		// Token: 0x04000057 RID: 87
		public string SystemMsg;

		// Token: 0x04000058 RID: 88
		public string SystemMsgParsed;

		// Token: 0x04000059 RID: 89
		public string TmiSentTs;

		// Token: 0x0400005A RID: 90
		public bool IsTurbo;

		// Token: 0x0400005B RID: 91
		public string UserId;

		// Token: 0x0400005C RID: 92
		public UserType UserType;

		// Token: 0x0400005D RID: 93
		public string MsgParamMultiMonthGiftDuration;
	}
}
