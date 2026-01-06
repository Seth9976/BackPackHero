using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;
using TwitchLib.PubSub.Models.Responses.Messages.Redemption;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x0200000D RID: 13
	public class ChannelPointsChannel : MessageData
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005818 File Offset: 0x00003A18
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00005820 File Offset: 0x00003A20
		public ChannelPointsChannelType Type { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00005829 File Offset: 0x00003A29
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005831 File Offset: 0x00003A31
		public ChannelPointsData Data { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000583A File Offset: 0x00003A3A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005842 File Offset: 0x00003A42
		public string RawData { get; private set; }

		// Token: 0x060000C0 RID: 192 RVA: 0x0000584C File Offset: 0x00003A4C
		public ChannelPointsChannel(string jsonStr)
		{
			this.RawData = jsonStr;
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null && text == "reward-redeemed")
			{
				this.Type = ChannelPointsChannelType.RewardRedeemed;
				this.Data = JsonConvert.DeserializeObject<RewardRedeemed>(jtoken.SelectToken("data").ToString());
				return;
			}
			this.Type = ChannelPointsChannelType.Unknown;
		}
	}
}
