using System;
using System.Collections.Generic;
using System.Drawing;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Extensions.NetCore;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000002 RID: 2
	public class Announcement
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public string Id { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public string SystemMessage { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public string SystemMessageParsed { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public bool IsBroadcaster { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002080 File Offset: 0x00000280
		public bool IsModerator { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002088 File Offset: 0x00000288
		public bool IsPartner { get; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002090 File Offset: 0x00000290
		public bool IsSubscriber { get; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002098 File Offset: 0x00000298
		public bool IsStaff { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020A0 File Offset: 0x000002A0
		public bool IsTurbo { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000C RID: 12 RVA: 0x000020A8 File Offset: 0x000002A8
		public string Login { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020B0 File Offset: 0x000002B0
		public string UserId { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000020B8 File Offset: 0x000002B8
		public string RoomId { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000020C0 File Offset: 0x000002C0
		public UserType UserType { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000020C8 File Offset: 0x000002C8
		public string TmiSentTs { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000020D0 File Offset: 0x000002D0
		public string EmoteSet { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000020D8 File Offset: 0x000002D8
		public string RawIrc { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000013 RID: 19 RVA: 0x000020E0 File Offset: 0x000002E0
		public string MsgId { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000020E8 File Offset: 0x000002E8
		public string MsgParamColor { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000020F0 File Offset: 0x000002F0
		public string ColorHex { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000020F8 File Offset: 0x000002F8
		public Color Color { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002100 File Offset: 0x00000300
		public string Message { get; }

		// Token: 0x06000018 RID: 24 RVA: 0x00002108 File Offset: 0x00000308
		public Announcement(IrcMessage ircMessage)
		{
			this.RawIrc = ircMessage.ToString();
			this.Message = ircMessage.Message;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1935600968U)
					{
						if (num <= 852145304U)
						{
							if (num != 406370129U)
							{
								if (num != 682509400U)
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
									if (!(text == "msg-param-color"))
									{
										continue;
									}
									this.MsgParamColor = text2;
									continue;
								}
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
										string key = keyValuePair.Key;
										if (key != null)
										{
											num = <PrivateImplementationDetails>.ComputeStringHash(key);
											if (num <= 1674645177U)
											{
												if (num != 885536276U)
												{
													if (num != 1231559683U)
													{
														if (num == 1674645177U)
														{
															if (key == "partner")
															{
																this.IsPartner = true;
															}
														}
													}
													else if (key == "broadcaster")
													{
														this.IsBroadcaster = true;
													}
												}
												else if (key == "admin")
												{
													this.IsStaff = true;
												}
											}
											else if (num <= 2289789988U)
											{
												if (num != 1734124753U)
												{
													if (num == 2289789988U)
													{
														if (key == "moderator")
														{
															this.IsModerator = true;
														}
													}
												}
												else if (key == "staff")
												{
													this.IsStaff = true;
												}
											}
											else if (num != 3477026043U)
											{
												if (num == 3878629347U)
												{
													if (key == "subscriber")
													{
														this.IsSubscriber = true;
													}
												}
											}
											else if (key == "turbo")
											{
												this.IsTurbo = true;
											}
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
								if (num != 1935600968U)
								{
									continue;
								}
								if (!(text == "msg-id"))
								{
									continue;
								}
								this.MsgId = text2;
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
					else if (num <= 2778540304U)
					{
						if (num != 2443343188U)
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
					else if (num <= 3380555570U)
					{
						if (num != 3158054904U)
						{
							if (num != 3380555570U)
							{
								continue;
							}
							if (!(text == "login"))
							{
								continue;
							}
							this.Login = text2;
							continue;
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
					else if (num != 3803092688U)
					{
						if (num != 3863694779U)
						{
							continue;
						}
						if (!(text == "badge-info"))
						{
							continue;
						}
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
					this.BadgeInfo = Helpers.ParseBadges(text2);
				}
			}
		}
	}
}
