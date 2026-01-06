using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Videos.DeleteVideos
{
	// Token: 0x02000005 RID: 5
	public class DeleteVideosResponse
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000021CD File Offset: 0x000003CD
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000021D5 File Offset: 0x000003D5
		[JsonProperty(PropertyName = "data")]
		public string[] Data { get; protected set; }
	}
}
