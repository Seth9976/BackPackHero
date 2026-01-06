using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Extensions.NetCore;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000005 RID: 5
	public class ChatMessage : TwitchLibMessage
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002BBE File Offset: 0x00000DBE
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002BC6 File Offset: 0x00000DC6
		public int Bits { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002BCE File Offset: 0x00000DCE
		public double BitsInDollars { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002BD6 File Offset: 0x00000DD6
		public string Channel { get; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002BDE File Offset: 0x00000DDE
		public CheerBadge CheerBadge { get; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public string CustomRewardId { get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002BEE File Offset: 0x00000DEE
		public string EmoteReplacedMessage { get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002BF6 File Offset: 0x00000DF6
		public string Id { get; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002BFE File Offset: 0x00000DFE
		public bool IsBroadcaster { get; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002C06 File Offset: 0x00000E06
		public bool IsFirstMessage { get; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002C0E File Offset: 0x00000E0E
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002C16 File Offset: 0x00000E16
		public bool IsHighlighted { get; internal set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002C1F File Offset: 0x00000E1F
		public bool IsMe { get; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002C27 File Offset: 0x00000E27
		public bool IsModerator { get; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002C2F File Offset: 0x00000E2F
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002C37 File Offset: 0x00000E37
		public bool IsSkippingSubMode { get; internal set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002C40 File Offset: 0x00000E40
		public bool IsSubscriber { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002C48 File Offset: 0x00000E48
		public bool IsVip { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002C50 File Offset: 0x00000E50
		public bool IsStaff { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002C58 File Offset: 0x00000E58
		public bool IsPartner { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002C60 File Offset: 0x00000E60
		public string Message { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002C68 File Offset: 0x00000E68
		public Noisy Noisy { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002C70 File Offset: 0x00000E70
		public string RoomId { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002C78 File Offset: 0x00000E78
		public int SubscribedMonthCount { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000044 RID: 68 RVA: 0x00002C80 File Offset: 0x00000E80
		public string TmiSentTs { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002C88 File Offset: 0x00000E88
		public ChatReply ChatReply { get; }

		// Token: 0x06000046 RID: 70 RVA: 0x00002C90 File Offset: 0x00000E90
		public ChatMessage(string botUsername, IrcMessage ircMessage, ref MessageEmoteCollection emoteCollection, bool replaceEmotes = false)
		{
			base.BotUsername = botUsername;
			base.RawIrcMessage = ircMessage.ToString();
			this.Message = ircMessage.Message;
			if (this.Message.Length > 0 && (byte)this.Message.get_Chars(0) == 1 && (byte)this.Message.get_Chars(this.Message.Length - 1) == 1 && this.Message.StartsWith("\u0001ACTION ") && this.Message.EndsWith("\u0001"))
			{
				this.Message = this.Message.Trim(new char[] { '\u0001' }).Substring(7);
				this.IsMe = true;
			}
			this._emoteCollection = emoteCollection;
			base.Username = ircMessage.User;
			this.Channel = ircMessage.Channel;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2621712821U)
					{
						if (num <= 1031692888U)
						{
							if (num <= 406370129U)
							{
								if (num != 320519876U)
								{
									if (num != 406370129U)
									{
										continue;
									}
									if (!(text == "badges"))
									{
										continue;
									}
									base.Badges = Helpers.ParseBadges(text2);
									using (List<KeyValuePair<string, string>>.Enumerator enumerator2 = base.Badges.GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											KeyValuePair<string, string> keyValuePair = enumerator2.Current;
											string key = keyValuePair.Key;
											if (key != null)
											{
												if (!(key == "bits"))
												{
													if (!(key == "subscriber"))
													{
														if (!(key == "vip"))
														{
															if (!(key == "admin"))
															{
																if (!(key == "staff"))
																{
																	if (key == "partner")
																	{
																		this.IsPartner = true;
																	}
																}
																else
																{
																	this.IsStaff = true;
																}
															}
															else
															{
																this.IsStaff = true;
															}
														}
														else
														{
															this.IsVip = true;
														}
													}
													else if (this.SubscribedMonthCount == 0)
													{
														this.SubscribedMonthCount = int.Parse(keyValuePair.Value);
													}
												}
												else
												{
													this.CheerBadge = new CheerBadge(int.Parse(keyValuePair.Value));
												}
											}
										}
										continue;
									}
								}
								else
								{
									if (!(text == "reply-parent-user-id"))
									{
										continue;
									}
									if (this.ChatReply == null)
									{
										this.ChatReply = new ChatReply();
									}
									this.ChatReply.ParentUserId = text2;
									continue;
								}
							}
							else if (num != 852145304U)
							{
								if (num != 926444256U)
								{
									if (num != 1031692888U)
									{
										continue;
									}
									if (!(text == "color"))
									{
										continue;
									}
									base.ColorHex = text2;
									if (!string.IsNullOrWhiteSpace(base.ColorHex))
									{
										base.Color = ColorTranslator.FromHtml(base.ColorHex);
										continue;
									}
									continue;
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
							else
							{
								if (!(text == "user-id"))
								{
									continue;
								}
								base.UserId = text2;
								continue;
							}
						}
						else if (num <= 1790204028U)
						{
							if (num != 1070752198U)
							{
								if (num != 1546905519U)
								{
									if (num != 1790204028U)
									{
										continue;
									}
									if (!(text == "reply-parent-msg-id"))
									{
										continue;
									}
									if (this.ChatReply == null)
									{
										this.ChatReply = new ChatReply();
									}
									this.ChatReply.ParentMsgId = text2;
									continue;
								}
								else
								{
									if (!(text == "reply-parent-display-name"))
									{
										continue;
									}
									if (this.ChatReply == null)
									{
										this.ChatReply = new ChatReply();
									}
									this.ChatReply.ParentDisplayName = text2;
									continue;
								}
							}
							else
							{
								if (!(text == "reply-parent-user-login"))
								{
									continue;
								}
								if (this.ChatReply == null)
								{
									this.ChatReply = new ChatReply();
								}
								this.ChatReply.ParentUserLogin = text2;
								continue;
							}
						}
						else if (num != 1935600968U)
						{
							if (num != 2559532549U)
							{
								if (num != 2621712821U)
								{
									continue;
								}
								if (!(text == "noisy"))
								{
									continue;
								}
								this.Noisy = (Helpers.ConvertToBool(text2) ? Noisy.True : Noisy.False);
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
										base.UserType = UserType.Moderator;
										continue;
									}
									if (text2 == "global_mod")
									{
										base.UserType = UserType.GlobalModerator;
										continue;
									}
									if (text2 == "admin")
									{
										base.UserType = UserType.Admin;
										this.IsStaff = true;
										continue;
									}
									if (text2 == "staff")
									{
										base.UserType = UserType.Staff;
										this.IsStaff = true;
										continue;
									}
								}
								base.UserType = UserType.Viewer;
								continue;
							}
						}
						else
						{
							if (!(text == "msg-id"))
							{
								continue;
							}
							this.handleMsgId(text2);
							continue;
						}
					}
					else if (num <= 3712472925U)
					{
						if (num <= 3103773643U)
						{
							if (num != 2724343673U)
							{
								if (num != 2778540304U)
								{
									if (num != 3103773643U)
									{
										continue;
									}
									if (!(text == "display-name"))
									{
										continue;
									}
									base.DisplayName = text2;
									continue;
								}
								else
								{
									if (!(text == "emotes"))
									{
										continue;
									}
									base.EmoteSet = new EmoteSet(text2, this.Message);
									continue;
								}
							}
							else
							{
								if (!(text == "bits"))
								{
									continue;
								}
								this.Bits = int.Parse(text2);
								this.BitsInDollars = ChatMessage.ConvertBitsToUsd(this.Bits);
								continue;
							}
						}
						else if (num != 3158054904U)
						{
							if (num != 3477026043U)
							{
								if (num != 3712472925U)
								{
									continue;
								}
								if (!(text == "first-msg"))
								{
									continue;
								}
								this.IsFirstMessage = text2 == "1";
								continue;
							}
							else
							{
								if (!(text == "turbo"))
								{
									continue;
								}
								base.IsTurbo = Helpers.ConvertToBool(text2);
								continue;
							}
						}
						else
						{
							if (!(text == "tmi-sent-ts"))
							{
								continue;
							}
							this.TmiSentTs = text2;
							continue;
						}
					}
					else if (num <= 3803092688U)
					{
						if (num != 3728142436U)
						{
							if (num != 3751703171U)
							{
								if (num != 3803092688U)
								{
									continue;
								}
								if (!(text == "room-id"))
								{
									continue;
								}
								this.RoomId = text2;
								continue;
							}
							else
							{
								if (!(text == "mod"))
								{
									continue;
								}
								this.IsModerator = Helpers.ConvertToBool(text2);
								continue;
							}
						}
						else
						{
							if (!(text == "custom-reward-id"))
							{
								continue;
							}
							this.CustomRewardId = text2;
							continue;
						}
					}
					else if (num != 3863694779U)
					{
						if (num != 3878629347U)
						{
							if (num != 4041728137U)
							{
								continue;
							}
							if (!(text == "reply-parent-msg-body"))
							{
								continue;
							}
							if (this.ChatReply == null)
							{
								this.ChatReply = new ChatReply();
							}
							this.ChatReply.ParentMsgBody = text2;
							continue;
						}
						else
						{
							if (!(text == "subscriber"))
							{
								continue;
							}
							this.IsSubscriber = this.IsSubscriber || Helpers.ConvertToBool(text2);
							continue;
						}
					}
					else if (!(text == "badge-info"))
					{
						continue;
					}
					this.BadgeInfo = Helpers.ParseBadges(text2);
					KeyValuePair<string, string> keyValuePair2 = this.BadgeInfo.Find((KeyValuePair<string, string> b) => b.Key == "founder");
					if (!keyValuePair2.Equals(default(KeyValuePair<string, string>)))
					{
						this.IsSubscriber = true;
						this.SubscribedMonthCount = int.Parse(keyValuePair2.Value);
					}
					else
					{
						KeyValuePair<string, string> keyValuePair3 = this.BadgeInfo.Find((KeyValuePair<string, string> b) => b.Key == "subscriber");
						if (!keyValuePair3.Equals(default(KeyValuePair<string, string>)))
						{
							this.SubscribedMonthCount = int.Parse(keyValuePair3.Value);
						}
					}
				}
			}
			if (base.EmoteSet != null && this.Message != null && base.EmoteSet.Emotes.Count > 0)
			{
				foreach (string text3 in base.EmoteSet.RawEmoteSetString.Split(new char[] { '/' }))
				{
					int num2 = text3.IndexOf(':');
					int num3 = text3.IndexOf(',');
					if (num3 == -1)
					{
						num3 = text3.Length;
					}
					int num4 = text3.IndexOf('-');
					int num5;
					int num6;
					if (num2 > 0 && num4 > num2 && num3 > num4 && int.TryParse(text3.Substring(num2 + 1, num4 - num2 - 1), ref num5) && int.TryParse(text3.Substring(num4 + 1, num3 - num4 - 1), ref num6) && num5 >= 0 && num5 < num6 && num6 < this.Message.Length)
					{
						string text4 = text3.Substring(0, num2);
						string text5 = this.Message.Substring(num5, num6 - num5 + 1);
						this._emoteCollection.Add(new MessageEmote(text4, text5, MessageEmote.EmoteSource.Twitch, MessageEmote.EmoteSize.Small, null));
					}
				}
				if (replaceEmotes)
				{
					this.EmoteReplacedMessage = this._emoteCollection.ReplaceEmotes(this.Message, null);
				}
			}
			if (base.EmoteSet == null)
			{
				base.EmoteSet = new EmoteSet(null, this.Message);
			}
			if (string.IsNullOrEmpty(base.DisplayName))
			{
				base.DisplayName = base.Username;
			}
			if (string.Equals(this.Channel, base.Username, 3))
			{
				base.UserType = UserType.Broadcaster;
				this.IsBroadcaster = true;
			}
			if (this.Channel.Split(new char[] { ':' }).Length == 3 && string.Equals(this.Channel.Split(new char[] { ':' })[1], base.UserId, 3))
			{
				base.UserType = UserType.Broadcaster;
				this.IsBroadcaster = true;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000376C File Offset: 0x0000196C
		public ChatMessage(string botUsername, string userId, string userName, string displayName, string colorHex, Color color, EmoteSet emoteSet, string message, UserType userType, string channel, string id, bool isSubscriber, int subscribedMonthCount, string roomId, bool isTurbo, bool isModerator, bool isMe, bool isBroadcaster, bool isVip, bool isPartner, bool isStaff, Noisy noisy, string rawIrcMessage, string emoteReplacedMessage, List<KeyValuePair<string, string>> badges, CheerBadge cheerBadge, int bits, double bitsInDollars)
		{
			base.BotUsername = botUsername;
			base.UserId = userId;
			base.DisplayName = displayName;
			base.ColorHex = colorHex;
			base.Color = color;
			base.EmoteSet = emoteSet;
			this.Message = message;
			base.UserType = userType;
			this.Channel = channel;
			this.Id = id;
			this.IsSubscriber = isSubscriber;
			this.SubscribedMonthCount = subscribedMonthCount;
			this.RoomId = roomId;
			base.IsTurbo = isTurbo;
			this.IsModerator = isModerator;
			this.IsMe = isMe;
			this.IsBroadcaster = isBroadcaster;
			this.IsVip = isVip;
			this.IsPartner = isPartner;
			this.IsStaff = isStaff;
			this.Noisy = noisy;
			base.RawIrcMessage = rawIrcMessage;
			this.EmoteReplacedMessage = emoteReplacedMessage;
			base.Badges = badges;
			this.CheerBadge = cheerBadge;
			this.Bits = bits;
			this.BitsInDollars = bitsInDollars;
			base.Username = userName;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000385C File Offset: 0x00001A5C
		private void handleMsgId(string val)
		{
			if (val != null)
			{
				if (val == "highlighted-message")
				{
					this.IsHighlighted = true;
					return;
				}
				if (!(val == "skip-subs-mode-message"))
				{
					return;
				}
				this.IsSkippingSubMode = true;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000388C File Offset: 0x00001A8C
		private static double ConvertBitsToUsd(int bits)
		{
			if (bits < 1500)
			{
				return (double)bits / 100.0 * 1.4;
			}
			if (bits < 5000)
			{
				return (double)bits / 1500.0 * 19.95;
			}
			if (bits < 10000)
			{
				return (double)bits / 5000.0 * 64.4;
			}
			if (bits < 25000)
			{
				return (double)bits / 10000.0 * 126.0;
			}
			return (double)bits / 25000.0 * 308.0;
		}

		// Token: 0x04000027 RID: 39
		protected readonly MessageEmoteCollection _emoteCollection;
	}
}
