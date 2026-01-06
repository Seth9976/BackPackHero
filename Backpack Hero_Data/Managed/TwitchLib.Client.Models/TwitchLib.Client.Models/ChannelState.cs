using System;
using TwitchLib.Client.Models.Common;
using TwitchLib.Client.Models.Internal;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000003 RID: 3
	public class ChannelState
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000019 RID: 25 RVA: 0x0000261C File Offset: 0x0000081C
		public string BroadcasterLanguage { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002624 File Offset: 0x00000824
		public string Channel { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600001B RID: 27 RVA: 0x0000262C File Offset: 0x0000082C
		public bool? EmoteOnly { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002634 File Offset: 0x00000834
		public TimeSpan? FollowersOnly { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000263C File Offset: 0x0000083C
		public bool Mercury { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002644 File Offset: 0x00000844
		public bool? R9K { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600001F RID: 31 RVA: 0x0000264C File Offset: 0x0000084C
		public bool? Rituals { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002654 File Offset: 0x00000854
		public string RoomId { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000265C File Offset: 0x0000085C
		public int? SlowMode { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002664 File Offset: 0x00000864
		public bool? SubOnly { get; }

		// Token: 0x06000023 RID: 35 RVA: 0x0000266C File Offset: 0x0000086C
		public ChannelState(IrcMessage ircMessage)
		{
			foreach (string text in ircMessage.Tags.Keys)
			{
				string text2 = ircMessage.Tags[text];
				if (text != null)
				{
					uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
					if (num <= 793511933U)
					{
						if (num <= 277652223U)
						{
							if (num != 38109070U)
							{
								if (num == 277652223U)
								{
									if (text == "subs-only")
									{
										this.SubOnly = new bool?(Helpers.ConvertToBool(text2));
										continue;
									}
								}
							}
							else if (text == "mercury")
							{
								this.Mercury = Helpers.ConvertToBool(text2);
								continue;
							}
						}
						else if (num != 358027581U)
						{
							if (num == 793511933U)
							{
								if (text == "r9k")
								{
									this.R9K = new bool?(Helpers.ConvertToBool(text2));
									continue;
								}
							}
						}
						else if (text == "followers-only")
						{
							int num2;
							if (int.TryParse(text2, ref num2) && num2 > -1)
							{
								this.FollowersOnly = new TimeSpan?(TimeSpan.FromMinutes((double)num2));
								continue;
							}
							continue;
						}
					}
					else if (num <= 1838498488U)
					{
						if (num != 1083073900U)
						{
							if (num == 1838498488U)
							{
								if (text == "slow")
								{
									int num3;
									this.SlowMode = (int.TryParse(text2, ref num3) ? new int?(num3) : default(int?));
									continue;
								}
							}
						}
						else if (text == "emote-only")
						{
							this.EmoteOnly = new bool?(Helpers.ConvertToBool(text2));
							continue;
						}
					}
					else if (num != 3803092688U)
					{
						if (num != 3880305606U)
						{
							if (num == 4035009971U)
							{
								if (text == "rituals")
								{
									this.Rituals = new bool?(Helpers.ConvertToBool(text2));
									continue;
								}
							}
						}
						else if (text == "broadcaster-lang")
						{
							this.BroadcasterLanguage = text2;
							continue;
						}
					}
					else if (text == "room-id")
					{
						this.RoomId = text2;
						continue;
					}
				}
				Console.WriteLine("[TwitchLib][ChannelState] Unaccounted for: " + text);
			}
			this.Channel = ircMessage.Channel;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002918 File Offset: 0x00000B18
		public ChannelState(bool r9k, bool rituals, bool subonly, int slowMode, bool emoteOnly, string broadcasterLanguage, string channel, TimeSpan followersOnly, bool mercury, string roomId)
		{
			this.R9K = new bool?(r9k);
			this.Rituals = new bool?(rituals);
			this.SubOnly = new bool?(subonly);
			this.SlowMode = new int?(slowMode);
			this.EmoteOnly = new bool?(emoteOnly);
			this.BroadcasterLanguage = broadcasterLanguage;
			this.Channel = channel;
			this.FollowersOnly = new TimeSpan?(followersOnly);
			this.Mercury = mercury;
			this.RoomId = roomId;
		}
	}
}
