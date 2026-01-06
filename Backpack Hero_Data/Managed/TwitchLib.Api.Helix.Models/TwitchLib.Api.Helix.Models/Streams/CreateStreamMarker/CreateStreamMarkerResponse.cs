using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Streams.CreateStreamMarker
{
	// Token: 0x0200002D RID: 45
	public class CreateStreamMarkerResponse
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00002CB6 File Offset: 0x00000EB6
		// (set) Token: 0x06000175 RID: 373 RVA: 0x00002CBE File Offset: 0x00000EBE
		[JsonProperty(PropertyName = "data")]
		public CreatedMarker[] Data { get; protected set; }
	}
}
