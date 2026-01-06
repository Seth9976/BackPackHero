using System;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000011 RID: 17
	public class CommunityPointsChannel : MessageData
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00005D54 File Offset: 0x00003F54
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00005D5C File Offset: 0x00003F5C
		public CommunityPointsChannelType Type { get; protected set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00005D65 File Offset: 0x00003F65
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00005D6D File Offset: 0x00003F6D
		public DateTime TimeStamp { get; protected set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00005D76 File Offset: 0x00003F76
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00005D7E File Offset: 0x00003F7E
		public string ChannelId { get; protected set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00005D87 File Offset: 0x00003F87
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00005D8F File Offset: 0x00003F8F
		public string Login { get; protected set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00005D98 File Offset: 0x00003F98
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00005DA0 File Offset: 0x00003FA0
		public string DisplayName { get; protected set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00005DA9 File Offset: 0x00003FA9
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00005DB1 File Offset: 0x00003FB1
		public string Message { get; protected set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00005DBA File Offset: 0x00003FBA
		// (set) Token: 0x060000E9 RID: 233 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public Guid RewardId { get; protected set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005DCB File Offset: 0x00003FCB
		// (set) Token: 0x060000EB RID: 235 RVA: 0x00005DD3 File Offset: 0x00003FD3
		public string RewardTitle { get; protected set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00005DDC File Offset: 0x00003FDC
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public string RewardPrompt { get; protected set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00005DED File Offset: 0x00003FED
		// (set) Token: 0x060000EF RID: 239 RVA: 0x00005DF5 File Offset: 0x00003FF5
		public int RewardCost { get; protected set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00005DFE File Offset: 0x00003FFE
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00005E06 File Offset: 0x00004006
		public string Status { get; protected set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005E0F File Offset: 0x0000400F
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005E17 File Offset: 0x00004017
		public Guid RedemptionId { get; protected set; }

		// Token: 0x060000F4 RID: 244 RVA: 0x00005E20 File Offset: 0x00004020
		public CommunityPointsChannel(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null)
			{
				if (text == "reward-redeemed" || text == "redemption-status-update")
				{
					this.Type = CommunityPointsChannelType.RewardRedeemed;
					goto IL_009C;
				}
				if (text == "custom-reward-created")
				{
					this.Type = CommunityPointsChannelType.CustomRewardCreated;
					goto IL_009C;
				}
				if (text == "custom-reward-updated")
				{
					this.Type = CommunityPointsChannelType.CustomRewardUpdated;
					goto IL_009C;
				}
				if (text == "custom-reward-deleted")
				{
					this.Type = CommunityPointsChannelType.CustomRewardDeleted;
					goto IL_009C;
				}
				if (!(text == "update-redemption-statuses-progress"))
				{
				}
			}
			this.Type = (CommunityPointsChannelType)(-1);
			IL_009C:
			this.TimeStamp = DateTime.Parse(jtoken.SelectToken("data.timestamp").ToString());
			switch (this.Type)
			{
			case CommunityPointsChannelType.RewardRedeemed:
			{
				this.ChannelId = jtoken.SelectToken("data.redemption.channel_id").ToString();
				this.Login = jtoken.SelectToken("data.redemption.user.login").ToString();
				this.DisplayName = jtoken.SelectToken("data.redemption.user.display_name").ToString();
				this.RewardId = Guid.Parse(jtoken.SelectToken("data.redemption.reward.id").ToString());
				this.RewardTitle = jtoken.SelectToken("data.redemption.reward.title").ToString();
				this.RewardPrompt = jtoken.SelectToken("data.redemption.reward.prompt").ToString();
				this.RewardCost = int.Parse(jtoken.SelectToken("data.redemption.reward.cost").ToString());
				JToken jtoken2 = jtoken.SelectToken("data.redemption.user_input");
				this.Message = ((jtoken2 != null) ? jtoken2.ToString() : null);
				this.Status = jtoken.SelectToken("data.redemption.status").ToString();
				this.RedemptionId = Guid.Parse(jtoken.SelectToken("data.redemption.id").ToString());
				return;
			}
			case CommunityPointsChannelType.CustomRewardUpdated:
				this.ChannelId = jtoken.SelectToken("data.updated_reward.channel_id").ToString();
				this.RewardId = Guid.Parse(jtoken.SelectToken("data.updated_reward.id").ToString());
				this.RewardTitle = jtoken.SelectToken("data.updated_reward.title").ToString();
				this.RewardPrompt = jtoken.SelectToken("data.updated_reward.prompt").ToString();
				this.RewardCost = int.Parse(jtoken.SelectToken("data.updated_reward.cost").ToString());
				return;
			case CommunityPointsChannelType.CustomRewardCreated:
				this.ChannelId = jtoken.SelectToken("data.new_reward.channel_id").ToString();
				this.RewardId = Guid.Parse(jtoken.SelectToken("data.new_reward.id").ToString());
				this.RewardTitle = jtoken.SelectToken("data.new_reward.title").ToString();
				this.RewardPrompt = jtoken.SelectToken("data.new_reward.prompt").ToString();
				this.RewardCost = int.Parse(jtoken.SelectToken("data.new_reward.cost").ToString());
				return;
			case CommunityPointsChannelType.CustomRewardDeleted:
				this.ChannelId = jtoken.SelectToken("data.deleted_reward.channel_id").ToString();
				this.RewardId = Guid.Parse(jtoken.SelectToken("data.deleted_reward.id").ToString());
				this.RewardTitle = jtoken.SelectToken("data.deleted_reward.title").ToString();
				this.RewardPrompt = jtoken.SelectToken("data.deleted_reward.prompt").ToString();
				return;
			default:
				return;
			}
		}
	}
}
