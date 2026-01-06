using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x02000023 RID: 35
	public class Reward
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007090 File Offset: 0x00005290
		// (set) Token: 0x0600017A RID: 378 RVA: 0x00007098 File Offset: 0x00005298
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600017B RID: 379 RVA: 0x000070A1 File Offset: 0x000052A1
		// (set) Token: 0x0600017C RID: 380 RVA: 0x000070A9 File Offset: 0x000052A9
		[JsonProperty(PropertyName = "channel_id")]
		public string ChannelId { get; protected set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600017D RID: 381 RVA: 0x000070B2 File Offset: 0x000052B2
		// (set) Token: 0x0600017E RID: 382 RVA: 0x000070BA File Offset: 0x000052BA
		[JsonProperty(PropertyName = "title")]
		public string Title { get; protected set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600017F RID: 383 RVA: 0x000070C3 File Offset: 0x000052C3
		// (set) Token: 0x06000180 RID: 384 RVA: 0x000070CB File Offset: 0x000052CB
		[JsonProperty(PropertyName = "prompt")]
		public string Prompt { get; protected set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000181 RID: 385 RVA: 0x000070D4 File Offset: 0x000052D4
		// (set) Token: 0x06000182 RID: 386 RVA: 0x000070DC File Offset: 0x000052DC
		[JsonProperty(PropertyName = "cost")]
		public int Cost { get; protected set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000070E5 File Offset: 0x000052E5
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000070ED File Offset: 0x000052ED
		[JsonProperty(PropertyName = "is_user_input_required")]
		public bool IsUserInputRequired { get; protected set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000070F6 File Offset: 0x000052F6
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000070FE File Offset: 0x000052FE
		[JsonProperty(PropertyName = "is_sub_only")]
		public bool IsSubOnly { get; protected set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007107 File Offset: 0x00005307
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000710F File Offset: 0x0000530F
		[JsonProperty(PropertyName = "image")]
		public RedemptionImage Image { get; protected set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00007118 File Offset: 0x00005318
		// (set) Token: 0x0600018A RID: 394 RVA: 0x00007120 File Offset: 0x00005320
		[JsonProperty(PropertyName = "default_image")]
		public RedemptionImage DefaultImage { get; protected set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00007129 File Offset: 0x00005329
		// (set) Token: 0x0600018C RID: 396 RVA: 0x00007131 File Offset: 0x00005331
		[JsonProperty(PropertyName = "background_color")]
		public string BackgroundColor { get; protected set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600018D RID: 397 RVA: 0x0000713A File Offset: 0x0000533A
		// (set) Token: 0x0600018E RID: 398 RVA: 0x00007142 File Offset: 0x00005342
		[JsonProperty(PropertyName = "is_enabled")]
		public bool IsEnabled { get; protected set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600018F RID: 399 RVA: 0x0000714B File Offset: 0x0000534B
		// (set) Token: 0x06000190 RID: 400 RVA: 0x00007153 File Offset: 0x00005353
		[JsonProperty(PropertyName = "is_paused")]
		public bool IsPaused { get; protected set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000715C File Offset: 0x0000535C
		// (set) Token: 0x06000192 RID: 402 RVA: 0x00007164 File Offset: 0x00005364
		[JsonProperty(PropertyName = "is_in_stock")]
		public bool IsInStock { get; protected set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000193 RID: 403 RVA: 0x0000716D File Offset: 0x0000536D
		// (set) Token: 0x06000194 RID: 404 RVA: 0x00007175 File Offset: 0x00005375
		[JsonProperty(PropertyName = "max_per_stream")]
		public MaxPerStream MaxPerStream { get; protected set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000717E File Offset: 0x0000537E
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00007186 File Offset: 0x00005386
		[JsonProperty(PropertyName = "should_redemptions_skip_request_queue")]
		public bool ShouldRedemptionsSkipRequestQueue { get; protected set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000718F File Offset: 0x0000538F
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00007197 File Offset: 0x00005397
		[JsonProperty(PropertyName = "template_id")]
		public string TemplateId { get; protected set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000199 RID: 409 RVA: 0x000071A0 File Offset: 0x000053A0
		// (set) Token: 0x0600019A RID: 410 RVA: 0x000071A8 File Offset: 0x000053A8
		[JsonProperty(PropertyName = "updated_for_indicator_at")]
		public DateTime UpdatedForIndicatorAt { get; protected set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000071B1 File Offset: 0x000053B1
		// (set) Token: 0x0600019C RID: 412 RVA: 0x000071B9 File Offset: 0x000053B9
		[JsonProperty(PropertyName = "max_per_user_per_stream")]
		public MaxPerUserPerStream MaxPerUserPerStream { get; protected set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000071C2 File Offset: 0x000053C2
		// (set) Token: 0x0600019E RID: 414 RVA: 0x000071CA File Offset: 0x000053CA
		[JsonProperty(PropertyName = "global_cooldown")]
		public GlobalCooldown GlobalCooldown { get; protected set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000071D3 File Offset: 0x000053D3
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x000071DB File Offset: 0x000053DB
		[JsonProperty(PropertyName = "cooldown_expires_at")]
		public DateTime? CooldownExpiresAt { get; protected set; }
	}
}
