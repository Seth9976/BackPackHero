using System;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200001A RID: 26
	public class VideoPlayback : MessageData
	{
		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006C7B File Offset: 0x00004E7B
		public VideoPlaybackType Type { get; }

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006C83 File Offset: 0x00004E83
		public string ServerTime { get; }

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006C8B File Offset: 0x00004E8B
		public int PlayDelay { get; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006C93 File Offset: 0x00004E93
		public int Viewers { get; }

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006C9B File Offset: 0x00004E9B
		public int Length { get; }

		// Token: 0x06000141 RID: 321 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public VideoPlayback(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null)
			{
				if (!(text == "stream-up"))
				{
					if (!(text == "stream-down"))
					{
						if (!(text == "viewcount"))
						{
							if (text == "commercial")
							{
								this.Type = VideoPlaybackType.Commercial;
							}
						}
						else
						{
							this.Type = VideoPlaybackType.ViewCount;
						}
					}
					else
					{
						this.Type = VideoPlaybackType.StreamDown;
					}
				}
				else
				{
					this.Type = VideoPlaybackType.StreamUp;
				}
			}
			JToken jtoken2 = jtoken.SelectToken("server_time");
			this.ServerTime = ((jtoken2 != null) ? jtoken2.ToString() : null);
			switch (this.Type)
			{
			case VideoPlaybackType.StreamUp:
				this.PlayDelay = int.Parse(jtoken.SelectToken("play_delay").ToString());
				return;
			case VideoPlaybackType.StreamDown:
				break;
			case VideoPlaybackType.ViewCount:
				this.Viewers = int.Parse(jtoken.SelectToken("viewers").ToString());
				return;
			case VideoPlaybackType.Commercial:
				this.Length = int.Parse(jtoken.SelectToken("length").ToString());
				break;
			default:
				return;
			}
		}
	}
}
