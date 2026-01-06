using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.Enums;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Helix.Models.ChannelPoints.CreateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.GetCustomRewardRedemption;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomReward;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateCustomRewardRedemptionStatus;
using TwitchLib.Api.Helix.Models.ChannelPoints.UpdateRedemptionStatus;

namespace TwitchLib.Api.Helix
{
	// Token: 0x0200000A RID: 10
	public class ChannelPoints : ApiBase
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002A23 File Offset: 0x00000C23
		public ChannelPoints(IApiSettings settings, IRateLimiter rateLimiter, IHttpCallHandler http)
			: base(settings, rateLimiter, http)
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002A30 File Offset: 0x00000C30
		public Task<CreateCustomRewardsResponse> CreateCustomRewardsAsync(string broadcasterId, CreateCustomRewardsRequest request, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPostGenericAsync<CreateCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonConvert.SerializeObject(request), list2, accessToken, null, null);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002A6C File Offset: 0x00000C6C
		public Task DeleteCustomRewardAsync(string broadcasterId, string rewardId, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("id", rewardId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchDeleteAsync("/channel_points/custom_rewards", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public Task<GetCustomRewardsResponse> GetCustomRewardAsync(string broadcasterId, List<string> rewardIds = null, bool onlyManageableRewards = false, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("only_manageable_rewards", onlyManageableRewards.ToString().ToLower()));
			List<KeyValuePair<string, string>> list2 = list;
			if (rewardIds != null && rewardIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(rewardIds, (string rewardId) => new KeyValuePair<string, string>("id", rewardId)));
			}
			return base.TwitchGetGenericAsync<GetCustomRewardsResponse>("/channel_points/custom_rewards", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002B3C File Offset: 0x00000D3C
		public Task<UpdateCustomRewardResponse> UpdateCustomRewardAsync(string broadcasterId, string rewardId, UpdateCustomRewardRequest request, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("id", rewardId));
			List<KeyValuePair<string, string>> list2 = list;
			return base.TwitchPatchGenericAsync<UpdateCustomRewardResponse>("/channel_points/custom_rewards", ApiVersion.Helix, JsonConvert.SerializeObject(request), list2, accessToken, null, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002B88 File Offset: 0x00000D88
		public Task<GetCustomRewardRedemptionResponse> GetCustomRewardRedemptionAsync(string broadcasterId, string rewardId, List<string> redemptionIds = null, string status = null, string sort = null, string after = null, string first = null, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("reward_id", rewardId));
			List<KeyValuePair<string, string>> list2 = list;
			if (redemptionIds != null && redemptionIds.Count > 0)
			{
				list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(redemptionIds, (string redemptionId) => new KeyValuePair<string, string>("id", redemptionId)));
			}
			if (!string.IsNullOrWhiteSpace(status))
			{
				list2.Add(new KeyValuePair<string, string>("status", status));
			}
			if (!string.IsNullOrWhiteSpace(sort))
			{
				list2.Add(new KeyValuePair<string, string>("sort", sort));
			}
			if (!string.IsNullOrWhiteSpace(after))
			{
				list2.Add(new KeyValuePair<string, string>("after", after));
			}
			if (!string.IsNullOrWhiteSpace(first))
			{
				list2.Add(new KeyValuePair<string, string>("first", first));
			}
			return base.TwitchGetGenericAsync<GetCustomRewardRedemptionResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, list2, accessToken, null, null);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C74 File Offset: 0x00000E74
		public Task<UpdateRedemptionStatusResponse> UpdateRedemptionStatusAsync(string broadcasterId, string rewardId, List<string> redemptionIds, UpdateCustomRewardRedemptionStatusRequest request, string accessToken = null)
		{
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			list.Add(new KeyValuePair<string, string>("broadcaster_id", broadcasterId));
			list.Add(new KeyValuePair<string, string>("reward_id", rewardId));
			List<KeyValuePair<string, string>> list2 = list;
			list2.AddRange(Enumerable.Select<string, KeyValuePair<string, string>>(redemptionIds, (string redemptionId) => new KeyValuePair<string, string>("id", redemptionId)));
			return base.TwitchPatchGenericAsync<UpdateRedemptionStatusResponse>("/channel_points/custom_rewards/redemptions", ApiVersion.Helix, JsonConvert.SerializeObject(request), list2, accessToken, null, null);
		}
	}
}
