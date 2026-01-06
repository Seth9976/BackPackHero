using System;
using System.Collections.Generic;
using TwitchLib.Client.Enums;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000020 RID: 32
	public class UserState
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00006BE1 File Offset: 0x00004DE1
		public List<KeyValuePair<string, string>> Badges { get; } = new List<KeyValuePair<string, string>>();

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00006BE9 File Offset: 0x00004DE9
		public List<KeyValuePair<string, string>> BadgeInfo { get; } = new List<KeyValuePair<string, string>>();

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00006BF1 File Offset: 0x00004DF1
		public string Channel { get; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00006BF9 File Offset: 0x00004DF9
		public string ColorHex { get; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00006C01 File Offset: 0x00004E01
		public string DisplayName { get; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00006C09 File Offset: 0x00004E09
		public string EmoteSet { get; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00006C11 File Offset: 0x00004E11
		public string Id { get; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00006C19 File Offset: 0x00004E19
		public bool IsModerator { get; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00006C21 File Offset: 0x00004E21
		public bool IsSubscriber { get; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00006C29 File Offset: 0x00004E29
		public UserType UserType { get; }

		// Token: 0x0600015C RID: 348 RVA: 0x00006C34 File Offset: 0x00004E34
		public UserState(IrcMessage ircMessage)
		{
			this.Channel = ircMessage.Channel;
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 1031692888U)
					{
						if (num <= 406370129U)
						{
							if (num != 163015643U)
							{
								if (num == 406370129U)
								{
									if (text == "badges")
									{
										this.Badges = Helpers.ParseBadges(text2);
										continue;
									}
								}
							}
							else if (text == "emote-sets")
							{
								this.EmoteSet = text2;
								continue;
							}
						}
						else if (num != 926444256U)
						{
							if (num == 1031692888U)
							{
								if (text == "color")
								{
									this.ColorHex = text2;
									continue;
								}
							}
						}
						else if (text == "id")
						{
							this.Id = text2;
							continue;
						}
					}
					else if (num <= 3103773643U)
					{
						if (num != 2559532549U)
						{
							if (num == 3103773643U)
							{
								if (text == "display-name")
								{
									this.DisplayName = text2;
									continue;
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
							continue;
						}
					}
					else if (num != 3751703171U)
					{
						if (num != 3863694779U)
						{
							if (num == 3878629347U)
							{
								if (text == "subscriber")
								{
									this.IsSubscriber = Helpers.ConvertToBool(text2);
									continue;
								}
							}
						}
						else if (text == "badge-info")
						{
							this.BadgeInfo = Helpers.ParseBadges(text2);
							continue;
						}
					}
					else if (text == "mod")
					{
						this.IsModerator = Helpers.ConvertToBool(text2);
						continue;
					}
				}
				Console.WriteLine("Unaccounted for [UserState]: " + text);
			}
			if (string.Equals(ircMessage.User, this.Channel, 3))
			{
				this.UserType = UserType.Broadcaster;
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00006F14 File Offset: 0x00005114
		public UserState(List<KeyValuePair<string, string>> badges, List<KeyValuePair<string, string>> badgeInfo, string colorHex, string displayName, string emoteSet, string channel, string id, bool isSubscriber, bool isModerator, UserType userType)
		{
			this.Badges = badges;
			this.BadgeInfo = badgeInfo;
			this.ColorHex = colorHex;
			this.DisplayName = displayName;
			this.EmoteSet = emoteSet;
			this.Channel = channel;
			this.Id = id;
			this.IsSubscriber = isSubscriber;
			this.IsModerator = isModerator;
			this.UserType = userType;
		}
	}
}
