using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common
{
	// Token: 0x02000093 RID: 147
	public class Tag
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060004D8 RID: 1240 RVA: 0x00004A3E File Offset: 0x00002C3E
		// (set) Token: 0x060004D9 RID: 1241 RVA: 0x00004A46 File Offset: 0x00002C46
		[JsonProperty(PropertyName = "tag_id")]
		public string TagId { get; protected set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060004DA RID: 1242 RVA: 0x00004A4F File Offset: 0x00002C4F
		// (set) Token: 0x060004DB RID: 1243 RVA: 0x00004A57 File Offset: 0x00002C57
		[JsonProperty(PropertyName = "is_auto")]
		public bool IsAuto { get; protected set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060004DC RID: 1244 RVA: 0x00004A60 File Offset: 0x00002C60
		// (set) Token: 0x060004DD RID: 1245 RVA: 0x00004A68 File Offset: 0x00002C68
		[JsonProperty(PropertyName = "localization_names")]
		public Dictionary<string, string> LocalizationNames { get; protected set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060004DE RID: 1246 RVA: 0x00004A71 File Offset: 0x00002C71
		// (set) Token: 0x060004DF RID: 1247 RVA: 0x00004A79 File Offset: 0x00002C79
		[JsonProperty(PropertyName = "localization_descriptions")]
		public Dictionary<string, string> LocalizationDescriptions { get; protected set; }
	}
}
