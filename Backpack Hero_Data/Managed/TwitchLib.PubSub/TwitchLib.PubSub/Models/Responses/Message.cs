using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Models.Responses.Messages;
using TwitchLib.PubSub.Models.Responses.Messages.UserModerationNotifications;

namespace TwitchLib.PubSub.Models.Responses
{
	// Token: 0x02000006 RID: 6
	public class Message
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000050A3 File Offset: 0x000032A3
		public string Topic { get; }

		// Token: 0x0600008C RID: 140 RVA: 0x000050AC File Offset: 0x000032AC
		public Message(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr).SelectToken("data");
			JToken jtoken2 = jtoken.SelectToken("topic");
			this.Topic = ((jtoken2 != null) ? jtoken2.ToString() : null);
			string text = jtoken.SelectToken("message").ToString();
			string topic = this.Topic;
			string text2 = ((topic != null) ? topic.Split(new char[] { '.' })[0] : null);
			if (text2 != null)
			{
				uint num = <PrivateImplementationDetails>.ComputeStringHash(text2);
				if (num <= 1266735245U)
				{
					if (num <= 539594034U)
					{
						if (num != 450000440U)
						{
							if (num != 522816415U)
							{
								if (num != 539594034U)
								{
									return;
								}
								if (!(text2 == "channel-bits-events-v2"))
								{
									return;
								}
								text = text.Replace("\\", "");
								string text3 = JObject.Parse(text)["data"].ToString();
								this.MessageData = JsonConvert.DeserializeObject<ChannelBitsEventsV2>(text3);
								return;
							}
							else
							{
								if (!(text2 == "channel-bits-events-v1"))
								{
									return;
								}
								this.MessageData = new ChannelBitsEvents(text);
								return;
							}
						}
						else
						{
							if (!(text2 == "automod-queue"))
							{
								return;
							}
							this.MessageData = new AutomodQueue(text);
							return;
						}
					}
					else if (num <= 1212123455U)
					{
						if (num != 778451386U)
						{
							if (num != 1212123455U)
							{
								return;
							}
							if (!(text2 == "predictions-channel-v1"))
							{
								return;
							}
							this.MessageData = new PredictionEvents(text);
						}
						else
						{
							if (!(text2 == "whispers"))
							{
								return;
							}
							this.MessageData = new Whisper(text);
							return;
						}
					}
					else if (num != 1248430604U)
					{
						if (num != 1266735245U)
						{
							return;
						}
						if (!(text2 == "channel-subscribe-events-v1"))
						{
							return;
						}
						this.MessageData = new ChannelSubscription(text);
						return;
					}
					else
					{
						if (!(text2 == "leaderboard-events-v1"))
						{
							return;
						}
						this.MessageData = new LeaderboardEvents(text);
						return;
					}
				}
				else if (num <= 2476983697U)
				{
					if (num <= 2101714332U)
					{
						if (num != 1970825802U)
						{
							if (num != 2101714332U)
							{
								return;
							}
							if (!(text2 == "channel-points-channel-v1"))
							{
								return;
							}
							this.MessageData = new ChannelPointsChannel(text);
							return;
						}
						else
						{
							if (!(text2 == "video-playback-by-id"))
							{
								return;
							}
							this.MessageData = new VideoPlayback(text);
							return;
						}
					}
					else if (num != 2157984858U)
					{
						if (num != 2476983697U)
						{
							return;
						}
						if (!(text2 == "raid"))
						{
							return;
						}
						this.MessageData = new RaidEvents(text);
						return;
					}
					else
					{
						if (!(text2 == "community-points-channel-v1"))
						{
							return;
						}
						this.MessageData = new CommunityPointsChannel(text);
						return;
					}
				}
				else if (num <= 2643987228U)
				{
					if (num != 2535512472U)
					{
						if (num != 2643987228U)
						{
							return;
						}
						if (!(text2 == "user-moderation-notifications"))
						{
							return;
						}
						this.MessageData = new UserModerationNotifications(text);
						return;
					}
					else
					{
						if (!(text2 == "following"))
						{
							return;
						}
						this.MessageData = new Following(text);
						return;
					}
				}
				else if (num != 3075833323U)
				{
					if (num != 3729941884U)
					{
						return;
					}
					if (!(text2 == "channel-ext-v1"))
					{
						return;
					}
					this.MessageData = new ChannelExtensionBroadcast(text);
					return;
				}
				else
				{
					if (!(text2 == "chat_moderator_actions"))
					{
						return;
					}
					this.MessageData = new ChatModeratorActions(text);
					return;
				}
			}
		}

		// Token: 0x04000041 RID: 65
		public readonly MessageData MessageData;
	}
}
