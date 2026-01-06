using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Teams
{
	// Token: 0x02000018 RID: 24
	public abstract class TeamBase
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00002636 File Offset: 0x00000836
		// (set) Token: 0x060000B0 RID: 176 RVA: 0x0000263E File Offset: 0x0000083E
		[JsonProperty(PropertyName = "banner")]
		public string Banner { get; protected set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00002647 File Offset: 0x00000847
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000264F File Offset: 0x0000084F
		[JsonProperty(PropertyName = "background_image_url")]
		public string BackgroundImageUrl { get; protected set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002658 File Offset: 0x00000858
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00002660 File Offset: 0x00000860
		[JsonProperty(PropertyName = "created_at")]
		public string CreatedAt { get; protected set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002669 File Offset: 0x00000869
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00002671 File Offset: 0x00000871
		[JsonProperty(PropertyName = "updated_at")]
		public string UpdatedAt { get; protected set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000267A File Offset: 0x0000087A
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x00002682 File Offset: 0x00000882
		public string Info { get; protected set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000268B File Offset: 0x0000088B
		// (set) Token: 0x060000BA RID: 186 RVA: 0x00002693 File Offset: 0x00000893
		[JsonProperty(PropertyName = "thumbnail_url")]
		public string ThumbnailUrl { get; protected set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000269C File Offset: 0x0000089C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x000026A4 File Offset: 0x000008A4
		[JsonProperty(PropertyName = "team_name")]
		public string TeamName { get; protected set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000BD RID: 189 RVA: 0x000026AD File Offset: 0x000008AD
		// (set) Token: 0x060000BE RID: 190 RVA: 0x000026B5 File Offset: 0x000008B5
		[JsonProperty(PropertyName = "team_display_name")]
		public string TeamDisplayName { get; protected set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000BF RID: 191 RVA: 0x000026BE File Offset: 0x000008BE
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x000026C6 File Offset: 0x000008C6
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }
	}
}
