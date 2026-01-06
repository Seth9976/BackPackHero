using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Clips.CreateClip
{
	// Token: 0x02000097 RID: 151
	public class CreatedClipResponse
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x00004BF6 File Offset: 0x00002DF6
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x00004BFE File Offset: 0x00002DFE
		[JsonProperty(PropertyName = "data")]
		public CreatedClip[] CreatedClips { get; protected set; }
	}
}
