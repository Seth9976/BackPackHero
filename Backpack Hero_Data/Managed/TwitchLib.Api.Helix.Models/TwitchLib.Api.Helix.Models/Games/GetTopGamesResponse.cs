using System;
using Newtonsoft.Json;
using TwitchLib.Api.Helix.Models.Common;

namespace TwitchLib.Api.Helix.Models.Games
{
	// Token: 0x02000077 RID: 119
	public class GetTopGamesResponse
	{
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00004188 File Offset: 0x00002388
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00004190 File Offset: 0x00002390
		[JsonProperty(PropertyName = "data")]
		public Game[] Data { get; protected set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00004199 File Offset: 0x00002399
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000041A1 File Offset: 0x000023A1
		[JsonProperty(PropertyName = "pagination")]
		public Pagination Pagination { get; protected set; }
	}
}
