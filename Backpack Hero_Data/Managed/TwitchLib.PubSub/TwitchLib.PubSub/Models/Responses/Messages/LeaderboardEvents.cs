using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000013 RID: 19
	public class LeaderboardEvents : MessageData
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000061CC File Offset: 0x000043CC
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000061D4 File Offset: 0x000043D4
		public LeaderBoardType Type { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000061DD File Offset: 0x000043DD
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000061E5 File Offset: 0x000043E5
		public string ChannelId { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000061EE File Offset: 0x000043EE
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000061F6 File Offset: 0x000043F6
		public List<LeaderBoard> Top { get; private set; } = new List<LeaderBoard>();

		// Token: 0x06000101 RID: 257 RVA: 0x00006200 File Offset: 0x00004400
		public LeaderboardEvents(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("identifier.domain").ToString();
			if (text != null)
			{
				if (!(text == "bits-usage-by-channel-v1"))
				{
					if (text == "sub-gift-sent")
					{
						this.Type = LeaderBoardType.SubGiftSent;
					}
				}
				else
				{
					this.Type = LeaderBoardType.BitsUsageByChannel;
				}
			}
			LeaderBoardType type = this.Type;
			if (type != LeaderBoardType.BitsUsageByChannel)
			{
				if (type != LeaderBoardType.SubGiftSent)
				{
					return;
				}
			}
			else
			{
				this.ChannelId = jtoken.SelectToken("identifier.grouping_key").ToString();
				using (IEnumerator<JToken> enumerator = jtoken["top"].Children().GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						JToken jtoken2 = enumerator.Current;
						this.Top.Add(new LeaderBoard
						{
							Place = int.Parse(jtoken2.SelectToken("rank").ToString()),
							Score = int.Parse(jtoken2.SelectToken("score").ToString()),
							UserId = jtoken2.SelectToken("entry_key").ToString()
						});
					}
					return;
				}
			}
			this.ChannelId = jtoken.SelectToken("identifier.grouping_key").ToString();
			foreach (JToken jtoken3 in jtoken["top"].Children())
			{
				this.Top.Add(new LeaderBoard
				{
					Place = int.Parse(jtoken3.SelectToken("rank").ToString()),
					Score = int.Parse(jtoken3.SelectToken("score").ToString()),
					UserId = jtoken3.SelectToken("entry_key").ToString()
				});
			}
		}
	}
}
