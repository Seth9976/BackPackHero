using System;
using Newtonsoft.Json;
using TwitchLib.Api.Core.Enums;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x02000081 RID: 129
	public class ReleasedExtension
	{
		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x000044B3 File Offset: 0x000026B3
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x000044BB File Offset: 0x000026BB
		[JsonProperty(PropertyName = "author_name")]
		public string AuthorName { get; protected set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x000044C4 File Offset: 0x000026C4
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x000044CC File Offset: 0x000026CC
		[JsonProperty(PropertyName = "bits_enabled")]
		public bool BitsEnabled { get; protected set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x000044D5 File Offset: 0x000026D5
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x000044DD File Offset: 0x000026DD
		[JsonProperty(PropertyName = "can_install")]
		public bool CanInstall { get; protected set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x000044E6 File Offset: 0x000026E6
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x000044EE File Offset: 0x000026EE
		[JsonProperty(PropertyName = "configuration_location")]
		public string ConfigurationLocation { get; protected set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000044F7 File Offset: 0x000026F7
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000044FF File Offset: 0x000026FF
		[JsonProperty(PropertyName = "description")]
		public string Description { get; protected set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00004508 File Offset: 0x00002708
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x00004510 File Offset: 0x00002710
		[JsonProperty(PropertyName = "eula_tos_url")]
		public string EulaTosUrl { get; protected set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00004519 File Offset: 0x00002719
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x00004521 File Offset: 0x00002721
		[JsonProperty(PropertyName = "has_chat_support")]
		public bool HasChatSupport { get; protected set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000452A File Offset: 0x0000272A
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x00004532 File Offset: 0x00002732
		[JsonProperty(PropertyName = "icon_url")]
		public string IconUrl { get; protected set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000453B File Offset: 0x0000273B
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x00004543 File Offset: 0x00002743
		[JsonProperty(PropertyName = "icon_urls")]
		public IconUrls IconUrls { get; protected set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000454C File Offset: 0x0000274C
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x00004554 File Offset: 0x00002754
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000455D File Offset: 0x0000275D
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x00004565 File Offset: 0x00002765
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000456E File Offset: 0x0000276E
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x00004576 File Offset: 0x00002776
		[JsonProperty(PropertyName = "privacy_policy_url")]
		public string PrivacyPolicyUrl { get; protected set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000457F File Offset: 0x0000277F
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x00004587 File Offset: 0x00002787
		[JsonProperty(PropertyName = "request_identity_link")]
		public bool RequestIdentityLink { get; protected set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x00004590 File Offset: 0x00002790
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x00004598 File Offset: 0x00002798
		[JsonProperty(PropertyName = "screenshot_urls")]
		public string[] ScreenshotUrls { get; protected set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x000045A1 File Offset: 0x000027A1
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x000045A9 File Offset: 0x000027A9
		[JsonProperty(PropertyName = "state")]
		public ExtensionState State { get; protected set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x000045B2 File Offset: 0x000027B2
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x000045BA File Offset: 0x000027BA
		[JsonProperty(PropertyName = "subscriptions_support_level")]
		public string SubscriptionsSupportLevel { get; protected set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x000045C3 File Offset: 0x000027C3
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x000045CB File Offset: 0x000027CB
		[JsonProperty(PropertyName = "summary")]
		public string Summary { get; protected set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000045D4 File Offset: 0x000027D4
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000045DC File Offset: 0x000027DC
		[JsonProperty(PropertyName = "support_email")]
		public string SupportEmail { get; protected set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000045E5 File Offset: 0x000027E5
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x000045ED File Offset: 0x000027ED
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x000045F6 File Offset: 0x000027F6
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x000045FE File Offset: 0x000027FE
		[JsonProperty(PropertyName = "viewer_summary")]
		public string ViewerSummary { get; protected set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x00004607 File Offset: 0x00002807
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000460F File Offset: 0x0000280F
		[JsonProperty(PropertyName = "views")]
		public Views Views { get; protected set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x00004618 File Offset: 0x00002818
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x00004620 File Offset: 0x00002820
		[JsonProperty(PropertyName = "allowlisted_config_urls")]
		public string[] AllowlistedConfigUrls { get; protected set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x00004629 File Offset: 0x00002829
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00004631 File Offset: 0x00002831
		[JsonProperty(PropertyName = "allowlisted_panel_urls")]
		public string[] AllowlistedPanelUrls { get; protected set; }
	}
}
