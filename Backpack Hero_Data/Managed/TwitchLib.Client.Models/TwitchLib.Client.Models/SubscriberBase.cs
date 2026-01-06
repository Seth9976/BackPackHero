using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Extensions.NetCore;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200001D RID: 29
	public class SubscriberBase
	{
		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006167 File Offset: 0x00004367
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000616F File Offset: 0x0000436F
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006177 File Offset: 0x00004377
		public string ColorHex { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000617F File Offset: 0x0000437F
		public Color Color { get; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006187 File Offset: 0x00004387
		public string DisplayName { get; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000618F File Offset: 0x0000438F
		public string EmoteSet { get; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006197 File Offset: 0x00004397
		public string Id { get; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000619F File Offset: 0x0000439F
		public bool IsModerator { get; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000061A7 File Offset: 0x000043A7
		public bool IsPartner { get; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000061AF File Offset: 0x000043AF
		public bool IsSubscriber { get; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000061B7 File Offset: 0x000043B7
		public bool IsTurbo { get; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000125 RID: 293 RVA: 0x000061BF File Offset: 0x000043BF
		public string Login { get; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000061C7 File Offset: 0x000043C7
		public string MsgId { get; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000127 RID: 295 RVA: 0x000061CF File Offset: 0x000043CF
		public string MsgParamCumulativeMonths { get; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000061D7 File Offset: 0x000043D7
		public bool MsgParamShouldShareStreak { get; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000129 RID: 297 RVA: 0x000061DF File Offset: 0x000043DF
		public string MsgParamStreakMonths { get; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000061E7 File Offset: 0x000043E7
		public string RawIrc { get; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600012B RID: 299 RVA: 0x000061EF File Offset: 0x000043EF
		public string ResubMessage { get; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000061F7 File Offset: 0x000043F7
		public string RoomId { get; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600012D RID: 301 RVA: 0x000061FF File Offset: 0x000043FF
		public SubscriptionPlan SubscriptionPlan { get; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006207 File Offset: 0x00004407
		public string SubscriptionPlanName { get; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000620F File Offset: 0x0000440F
		public string SystemMessage { get; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006217 File Offset: 0x00004417
		public string SystemMessageParsed { get; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000621F File Offset: 0x0000441F
		public string TmiSentTs { get; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006227 File Offset: 0x00004427
		public string UserId { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000622F File Offset: 0x0000442F
		public UserType UserType { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006237 File Offset: 0x00004437
		public string Channel { get; }

		// Token: 0x06000135 RID: 309 RVA: 0x00006240 File Offset: 0x00004440
		protected SubscriberBase(IrcMessage ircMessage)
		{
			this.RawIrc = ircMessage.ToString();
			this.ResubMessage = ircMessage.Message;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2778540304U)
					{
						if (num <= 1152615314U)
						{
							if (num <= 852145304U)
							{
								if (num != 406370129U)
								{
									if (num != 852145304U)
									{
										continue;
									}
									if (!(text == "user-id"))
									{
										continue;
									}
									this.UserId = text2;
									continue;
								}
								else
								{
									if (!(text == "badges"))
									{
										continue;
									}
									this.Badges = Helpers.ParseBadges(text2);
									using (List<KeyValuePair<string, string>>.Enumerator enumerator2 = this.Badges.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											KeyValuePair<string, string> keyValuePair = enumerator2.Current;
											if (keyValuePair.Key == "partner")
											{
												this.IsPartner = true;
											}
										}
										continue;
									}
								}
							}
							else if (num != 926444256U)
							{
								if (num != 1031692888U)
								{
									if (num != 1152615314U)
									{
										continue;
									}
									if (!(text == "msg-param-cumulative-months"))
									{
										continue;
									}
									this.MsgParamCumulativeMonths = text2;
									continue;
								}
								else
								{
									if (!(text == "color"))
									{
										continue;
									}
									this.ColorHex = text2;
									if (!string.IsNullOrEmpty(this.ColorHex))
									{
										this.Color = ColorTranslator.FromHtml(this.ColorHex);
										continue;
									}
									continue;
								}
							}
							else
							{
								if (!(text == "id"))
								{
									continue;
								}
								this.Id = text2;
								continue;
							}
						}
						else if (num <= 2212455049U)
						{
							if (num != 1935600968U)
							{
								if (num != 2212455049U)
								{
									continue;
								}
								if (!(text == "msg-param-should-share-streak"))
								{
									continue;
								}
								this.MsgParamShouldShareStreak = Helpers.ConvertToBool(text2);
								continue;
							}
							else
							{
								if (!(text == "msg-id"))
								{
									continue;
								}
								this.MsgId = text2;
								continue;
							}
						}
						else if (num != 2443343188U)
						{
							if (num != 2559532549U)
							{
								if (num != 2778540304U)
								{
									continue;
								}
								if (!(text == "emotes"))
								{
									continue;
								}
								this.EmoteSet = text2;
								continue;
							}
							else
							{
								if (!(text == "user-type"))
								{
									continue;
								}
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
								continue;
							}
						}
						else
						{
							if (!(text == "system-msg"))
							{
								continue;
							}
							this.SystemMessage = text2;
							this.SystemMessageParsed = text2.Replace("\\s", " ");
							continue;
						}
					}
					else if (num <= 3477026043U)
					{
						if (num <= 3158054904U)
						{
							if (num != 3103773643U)
							{
								if (num != 3158054904U)
								{
									continue;
								}
								if (!(text == "tmi-sent-ts"))
								{
									continue;
								}
								this.TmiSentTs = text2;
								continue;
							}
							else
							{
								if (!(text == "display-name"))
								{
									continue;
								}
								this.DisplayName = text2;
								continue;
							}
						}
						else if (num != 3176240347U)
						{
							if (num != 3380555570U)
							{
								if (num != 3477026043U)
								{
									continue;
								}
								if (!(text == "turbo"))
								{
									continue;
								}
								this.IsTurbo = SubscriberBase.ConvertToBool(text2);
								continue;
							}
							else
							{
								if (!(text == "login"))
								{
									continue;
								}
								this.Login = text2;
								continue;
							}
						}
						else
						{
							if (!(text == "msg-param-sub-plan"))
							{
								continue;
							}
							string text3 = text2.ToLower();
							if (text3 != null)
							{
								if (text3 == "prime")
								{
									this.SubscriptionPlan = SubscriptionPlan.Prime;
									continue;
								}
								if (text3 == "1000")
								{
									this.SubscriptionPlan = SubscriptionPlan.Tier1;
									continue;
								}
								if (text3 == "2000")
								{
									this.SubscriptionPlan = SubscriptionPlan.Tier2;
									continue;
								}
								if (text3 == "3000")
								{
									this.SubscriptionPlan = SubscriptionPlan.Tier3;
									continue;
								}
							}
							throw new ArgumentOutOfRangeException("ToLower");
						}
					}
					else if (num <= 3836866301U)
					{
						if (num != 3751703171U)
						{
							if (num != 3803092688U)
							{
								if (num != 3836866301U)
								{
									continue;
								}
								if (!(text == "msg-param-streak-months"))
								{
									continue;
								}
								this.MsgParamStreakMonths = text2;
								continue;
							}
							else
							{
								if (!(text == "room-id"))
								{
									continue;
								}
								this.RoomId = text2;
								continue;
							}
						}
						else
						{
							if (!(text == "mod"))
							{
								continue;
							}
							this.IsModerator = SubscriberBase.ConvertToBool(text2);
							continue;
						}
					}
					else if (num != 3863694779U)
					{
						if (num != 3878629347U)
						{
							if (num != 4083234825U)
							{
								continue;
							}
							if (!(text == "msg-param-sub-plan-name"))
							{
								continue;
							}
							this.SubscriptionPlanName = text2.Replace("\\s", " ");
							continue;
						}
						else
						{
							if (!(text == "subscriber"))
							{
								continue;
							}
							this.IsSubscriber = SubscriberBase.ConvertToBool(text2);
							continue;
						}
					}
					else if (!(text == "badge-info"))
					{
						continue;
					}
					this.BadgeInfo = Helpers.ParseBadges(text2);
				}
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006860 File Offset: 0x00004A60
		internal SubscriberBase(List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, Color color, string displayName, string emoteSet, string id, string login, string systemMessage, string msgId, string msgParamCumulativeMonths, string msgParamStreakMonths, bool msgParamShouldShareStreak, string systemMessageParsed, string resubMessage, SubscriptionPlan subscriptionPlan, string subscriptionPlanName, string roomId, string userId, bool isModerator, bool isTurbo, bool isSubscriber, bool isPartner, string tmiSentTs, UserType userType, string rawIrc, string channel, int months)
		{
			this.Badges = badges;
			this.BadgeInfo = badgeInfo;
			this.ColorHex = colorHex;
			this.Color = color;
			this.DisplayName = displayName;
			this.EmoteSet = emoteSet;
			this.Id = id;
			this.Login = login;
			this.MsgId = msgId;
			this.MsgParamCumulativeMonths = msgParamCumulativeMonths;
			this.MsgParamStreakMonths = msgParamStreakMonths;
			this.MsgParamShouldShareStreak = msgParamShouldShareStreak;
			this.SystemMessage = systemMessage;
			this.SystemMessageParsed = systemMessageParsed;
			this.ResubMessage = resubMessage;
			this.SubscriptionPlan = subscriptionPlan;
			this.SubscriptionPlanName = subscriptionPlanName;
			this.RoomId = roomId;
			this.UserId = this.UserId;
			this.IsModerator = isModerator;
			this.IsTurbo = isTurbo;
			this.IsSubscriber = isSubscriber;
			this.IsPartner = isPartner;
			this.TmiSentTs = tmiSentTs;
			this.UserType = userType;
			this.RawIrc = rawIrc;
			this.monthsInternal = months;
			this.UserId = userId;
			this.Channel = channel;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000695C File Offset: 0x00004B5C
		private static bool ConvertToBool(string data)
		{
			return data == "1";
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000696C File Offset: 0x00004B6C
		public override string ToString()
		{
			return string.Format("Badges: {0}, color hex: {1}, display name: {2}, emote set: {3}, login: {4}, system message: {5}, msgId: {6}, msgParamCumulativeMonths: {7}", new object[]
			{
				this.Badges.Count,
				this.ColorHex,
				this.DisplayName,
				this.EmoteSet,
				this.Login,
				this.SystemMessage,
				this.MsgId,
				this.MsgParamCumulativeMonths
			}) + string.Format("msgParamStreakMonths: {0}, msgParamShouldShareStreak: {1}, resub message: {2}, months: {3}, room id: {4}, user id: {5}, mod: {6}, turbo: {7}, sub: {8}, user type: {9}, raw irc: {10}", new object[]
			{
				this.MsgParamStreakMonths, this.MsgParamShouldShareStreak, this.ResubMessage, this.monthsInternal, this.RoomId, this.UserId, this.IsModerator, this.IsTurbo, this.IsSubscriber, this.UserType,
				this.RawIrc
			});
		}

		// Token: 0x04000108 RID: 264
		protected readonly int monthsInternal;
	}
}
