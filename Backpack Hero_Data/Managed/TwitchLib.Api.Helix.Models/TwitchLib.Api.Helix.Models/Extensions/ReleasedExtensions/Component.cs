using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Extensions.ReleasedExtensions
{
	// Token: 0x0200007C RID: 124
	public class Component
	{
		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x00004337 File Offset: 0x00002537
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000433F File Offset: 0x0000253F
		[JsonProperty(PropertyName = "viewer_url")]
		public string ViewerUrl { get; protected set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x00004348 File Offset: 0x00002548
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x00004350 File Offset: 0x00002550
		[JsonProperty(PropertyName = "aspect_width")]
		public int AspectWidth { get; protected set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x00004359 File Offset: 0x00002559
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x00004361 File Offset: 0x00002561
		[JsonProperty(PropertyName = "aspect_height")]
		public int AspectHeight { get; protected set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000436A File Offset: 0x0000256A
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x00004372 File Offset: 0x00002572
		[JsonProperty(PropertyName = "aspect_ratio_x")]
		public int AspectRatioX { get; protected set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000437B File Offset: 0x0000257B
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x00004383 File Offset: 0x00002583
		[JsonProperty(PropertyName = "aspect_ratio_y")]
		public int AspectRatioY { get; protected set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000438C File Offset: 0x0000258C
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x00004394 File Offset: 0x00002594
		[JsonProperty(PropertyName = "autoscale")]
		public bool Autoscale { get; protected set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000439D File Offset: 0x0000259D
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x000043A5 File Offset: 0x000025A5
		[JsonProperty(PropertyName = "scale_pixels")]
		public int ScalePixels { get; protected set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000043AE File Offset: 0x000025AE
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x000043B6 File Offset: 0x000025B6
		[JsonProperty(PropertyName = "target_height")]
		public int TargetHeight { get; protected set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x000043BF File Offset: 0x000025BF
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x000043C7 File Offset: 0x000025C7
		[JsonProperty(PropertyName = "size")]
		public int Size { get; protected set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x000043D0 File Offset: 0x000025D0
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x000043D8 File Offset: 0x000025D8
		[JsonProperty(PropertyName = "zoom")]
		public bool Zoom { get; protected set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x000043E1 File Offset: 0x000025E1
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x000043E9 File Offset: 0x000025E9
		[JsonProperty(PropertyName = "zoom_pixels")]
		public int ZoomPixels { get; protected set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x000043F2 File Offset: 0x000025F2
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x000043FA File Offset: 0x000025FA
		[JsonProperty(PropertyName = "can_link_external_content")]
		public string CanLinkExternalContent { get; protected set; }
	}
}
