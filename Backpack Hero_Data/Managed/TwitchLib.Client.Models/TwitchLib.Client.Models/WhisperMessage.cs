using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Extensions.NetCore;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000023 RID: 35
	public class WhisperMessage : TwitchLibMessage
	{
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000722E File Offset: 0x0000542E
		public string MessageId { get; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00007236 File Offset: 0x00005436
		public string ThreadId { get; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000723E File Offset: 0x0000543E
		public string Message { get; }

		// Token: 0x0600016A RID: 362 RVA: 0x00007248 File Offset: 0x00005448
		public WhisperMessage(List<KeyValuePair<string, string>> badges, string colorHex, Color color, string username, string displayName, EmoteSet emoteSet, string threadId, string messageId, string userId, bool isTurbo, string botUsername, string message, UserType userType)
		{
			base.Badges = badges;
			base.ColorHex = colorHex;
			base.Color = color;
			base.Username = username;
			base.DisplayName = displayName;
			base.EmoteSet = emoteSet;
			this.ThreadId = threadId;
			this.MessageId = messageId;
			base.UserId = userId;
			base.IsTurbo = isTurbo;
			base.BotUsername = botUsername;
			this.Message = message;
			base.UserType = userType;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000072C0 File Offset: 0x000054C0
		public WhisperMessage(IrcMessage ircMessage, string botUsername)
		{
			base.Username = ircMessage.User;
			base.BotUsername = botUsername;
			base.RawIrcMessage = ircMessage.ToString();
			this.Message = ircMessage.Message;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1031692888U)
					{
						if (num <= 647959782U)
						{
							if (num != 406370129U)
							{
								if (num == 647959782U)
								{
									if (text == "message-id")
									{
										this.MessageId = text2;
									}
								}
							}
							else if (text == "badges")
							{
								base.Badges = new List<KeyValuePair<string, string>>();
								if (Enumerable.Contains<char>(text2, '/'))
								{
									if (!text2.Contains(","))
									{
										base.Badges.Add(new KeyValuePair<string, string>(text2.Split(new char[] { '/' })[0], text2.Split(new char[] { '/' })[1]));
									}
									else
									{
										foreach (string text3 in text2.Split(new char[] { ',' }))
										{
											base.Badges.Add(new KeyValuePair<string, string>(text3.Split(new char[] { '/' })[0], text3.Split(new char[] { '/' })[1]));
										}
									}
								}
							}
						}
						else if (num != 852145304U)
						{
							if (num == 1031692888U)
							{
								if (text == "color")
								{
									base.ColorHex = text2;
									if (!string.IsNullOrEmpty(base.ColorHex))
									{
										base.Color = ColorTranslator.FromHtml(base.ColorHex);
									}
								}
							}
						}
						else if (text == "user-id")
						{
							base.UserId = text2;
						}
					}
					else if (num <= 2559532549U)
					{
						if (num != 1421206105U)
						{
							if (num == 2559532549U)
							{
								if (text == "user-type")
								{
									if (text2 != null)
									{
										if (text2 == "global_mod")
										{
											base.UserType = UserType.GlobalModerator;
											continue;
										}
										if (text2 == "admin")
										{
											base.UserType = UserType.Admin;
											continue;
										}
										if (text2 == "staff")
										{
											base.UserType = UserType.Staff;
											continue;
										}
									}
									base.UserType = UserType.Viewer;
								}
							}
						}
						else if (text == "thread-id")
						{
							this.ThreadId = text2;
						}
					}
					else if (num != 2778540304U)
					{
						if (num != 3103773643U)
						{
							if (num == 3477026043U)
							{
								if (text == "turbo")
								{
									base.IsTurbo = Helpers.ConvertToBool(text2);
								}
							}
						}
						else if (text == "display-name")
						{
							base.DisplayName = text2;
						}
					}
					else if (text == "emotes")
					{
						base.EmoteSet = new EmoteSet(text2, this.Message);
					}
				}
			}
			if (base.EmoteSet == null)
			{
				base.EmoteSet = new EmoteSet(null, this.Message);
			}
		}
	}
}
