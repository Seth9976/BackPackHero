using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
	// Token: 0x0200000A RID: 10
	public class UserExtension
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000022E7 File Offset: 0x000004E7
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000022EF File Offset: 0x000004EF
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000022F8 File Offset: 0x000004F8
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002300 File Offset: 0x00000500
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002309 File Offset: 0x00000509
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002311 File Offset: 0x00000511
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000231A File Offset: 0x0000051A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002322 File Offset: 0x00000522
		[JsonProperty(PropertyName = "can_activate")]
		public bool CanActivate { get; protected set; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000232B File Offset: 0x0000052B
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002333 File Offset: 0x00000533
		[JsonProperty(PropertyName = "type")]
		public string[] Type { get; protected set; }
	}
}
