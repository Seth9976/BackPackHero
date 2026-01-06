using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x0200001A RID: 26
	public class RitualNewChatter
	{
		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00005AF8 File Offset: 0x00003CF8
		public List<KeyValuePair<string, string>> Badges { get; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00005B00 File Offset: 0x00003D00
		public List<KeyValuePair<string, string>> BadgeInfo { get; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00005B08 File Offset: 0x00003D08
		public string Color { get; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005B10 File Offset: 0x00003D10
		public string DisplayName { get; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00005B18 File Offset: 0x00003D18
		public string Emotes { get; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00005B20 File Offset: 0x00003D20
		public string Id { get; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00005B28 File Offset: 0x00003D28
		public bool IsModerator { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00005B30 File Offset: 0x00003D30
		public bool IsSubscriber { get; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005B38 File Offset: 0x00003D38
		public bool IsTurbo { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00005B40 File Offset: 0x00003D40
		public string Login { get; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00005B48 File Offset: 0x00003D48
		public string Message { get; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00005B50 File Offset: 0x00003D50
		public string MsgId { get; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00005B58 File Offset: 0x00003D58
		public string MsgParamRitualName { get; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005B60 File Offset: 0x00003D60
		public string RoomId { get; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005B68 File Offset: 0x00003D68
		public string SystemMsgParsed { get; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005B70 File Offset: 0x00003D70
		public string SystemMsg { get; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005B78 File Offset: 0x00003D78
		public string TmiSentTs { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005B80 File Offset: 0x00003D80
		public string UserId { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00005B88 File Offset: 0x00003D88
		public UserType UserType { get; }

		// Token: 0x0600010C RID: 268 RVA: 0x00005B90 File Offset: 0x00003D90
		public RitualNewChatter(IrcMessage ircMessage)
		{
			this.Message = ircMessage.Message;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 2559532549U)
					{
						if (num <= 1031692888U)
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
							else if (num != 926444256U)
							{
								if (num == 1031692888U)
								{
									if (text == "color")
									{
										this.Color = text2;
									}
								}
							}
							else if (text == "id")
							{
								this.Id = text2;
							}
						}
						else if (num <= 1935600968U)
						{
							if (num != 1826650670U)
							{
								if (num == 1935600968U)
								{
									if (text == "msg-id")
									{
										this.MsgId = text2;
									}
								}
							}
							else if (text == "msg-param-ritual-name")
							{
								this.MsgParamRitualName = text2;
							}
						}
						else if (num != 2443343188U)
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
					else if (num <= 3380555570U)
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
							if (num == 3380555570U)
							{
								if (text == "login")
								{
									this.Login = text2;
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
	}
}
