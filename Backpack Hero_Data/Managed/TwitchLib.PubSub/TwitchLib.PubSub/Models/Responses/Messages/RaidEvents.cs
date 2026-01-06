using System;
using Newtonsoft.Json.Linq;
using TwitchLib.PubSub.Enums;

namespace TwitchLib.PubSub.Models.Responses.Messages
{
	// Token: 0x02000016 RID: 22
	public class RaidEvents : MessageData
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006858 File Offset: 0x00004A58
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00006860 File Offset: 0x00004A60
		public RaidType Type { get; protected set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006869 File Offset: 0x00004A69
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006871 File Offset: 0x00004A71
		public Guid Id { get; protected set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000687A File Offset: 0x00004A7A
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00006882 File Offset: 0x00004A82
		public string ChannelId { get; protected set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000688B File Offset: 0x00004A8B
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00006893 File Offset: 0x00004A93
		public string TargetChannelId { get; protected set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000689C File Offset: 0x00004A9C
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000068A4 File Offset: 0x00004AA4
		public string TargetLogin { get; protected set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000124 RID: 292 RVA: 0x000068AD File Offset: 0x00004AAD
		// (set) Token: 0x06000125 RID: 293 RVA: 0x000068B5 File Offset: 0x00004AB5
		public string TargetDisplayName { get; protected set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000068BE File Offset: 0x00004ABE
		// (set) Token: 0x06000127 RID: 295 RVA: 0x000068C6 File Offset: 0x00004AC6
		public string TargetProfileImage { get; protected set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000128 RID: 296 RVA: 0x000068CF File Offset: 0x00004ACF
		// (set) Token: 0x06000129 RID: 297 RVA: 0x000068D7 File Offset: 0x00004AD7
		public DateTime AnnounceTime { get; protected set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600012A RID: 298 RVA: 0x000068E0 File Offset: 0x00004AE0
		// (set) Token: 0x0600012B RID: 299 RVA: 0x000068E8 File Offset: 0x00004AE8
		public DateTime RaidTime { get; protected set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600012C RID: 300 RVA: 0x000068F1 File Offset: 0x00004AF1
		// (set) Token: 0x0600012D RID: 301 RVA: 0x000068F9 File Offset: 0x00004AF9
		public int RemainigDurationSeconds { get; protected set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006902 File Offset: 0x00004B02
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000690A File Offset: 0x00004B0A
		public int ViewerCount { get; protected set; }

		// Token: 0x06000130 RID: 304 RVA: 0x00006914 File Offset: 0x00004B14
		public RaidEvents(string jsonStr)
		{
			JToken jtoken = JObject.Parse(jsonStr);
			string text = jtoken.SelectToken("type").ToString();
			if (text != null)
			{
				if (!(text == "raid_update"))
				{
					if (!(text == "raid_update_v2"))
					{
						if (text == "raid_go_v2")
						{
							this.Type = RaidType.RaidGo;
						}
					}
					else
					{
						this.Type = RaidType.RaidUpdateV2;
					}
				}
				else
				{
					this.Type = RaidType.RaidUpdate;
				}
			}
			switch (this.Type)
			{
			case RaidType.RaidUpdate:
				this.Id = Guid.Parse(jtoken.SelectToken("raid.id").ToString());
				this.ChannelId = jtoken.SelectToken("raid.source_id").ToString();
				this.TargetChannelId = jtoken.SelectToken("raid.target_id").ToString();
				this.AnnounceTime = DateTime.Parse(jtoken.SelectToken("raid.announce_time").ToString());
				this.RaidTime = DateTime.Parse(jtoken.SelectToken("raid.raid_time").ToString());
				this.RemainigDurationSeconds = int.Parse(jtoken.SelectToken("raid.remaining_duration_seconds").ToString());
				this.ViewerCount = int.Parse(jtoken.SelectToken("raid.viewer_count").ToString());
				return;
			case RaidType.RaidUpdateV2:
				this.Id = Guid.Parse(jtoken.SelectToken("raid.id").ToString());
				this.ChannelId = jtoken.SelectToken("raid.source_id").ToString();
				this.TargetChannelId = jtoken.SelectToken("raid.target_id").ToString();
				this.TargetLogin = jtoken.SelectToken("raid.target_login").ToString();
				this.TargetDisplayName = jtoken.SelectToken("raid.target_display_name").ToString();
				this.TargetProfileImage = jtoken.SelectToken("raid.target_profile_image").ToString();
				this.ViewerCount = int.Parse(jtoken.SelectToken("raid.viewer_count").ToString());
				return;
			case RaidType.RaidGo:
				this.Id = Guid.Parse(jtoken.SelectToken("raid.id").ToString());
				this.ChannelId = jtoken.SelectToken("raid.source_id").ToString();
				this.TargetChannelId = jtoken.SelectToken("raid.target_id").ToString();
				this.TargetLogin = jtoken.SelectToken("raid.target_login").ToString();
				this.TargetDisplayName = jtoken.SelectToken("raid.target_display_name").ToString();
				this.TargetProfileImage = jtoken.SelectToken("raid.target_profile_image").ToString();
				this.ViewerCount = int.Parse(jtoken.SelectToken("raid.viewer_count").ToString());
				return;
			default:
				return;
			}
		}
	}
}
