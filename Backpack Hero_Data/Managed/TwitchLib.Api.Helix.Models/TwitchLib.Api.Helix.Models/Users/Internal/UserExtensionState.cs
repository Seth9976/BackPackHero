using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
	// Token: 0x0200000B RID: 11
	public class UserExtensionState
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002344 File Offset: 0x00000544
		// (set) Token: 0x06000059 RID: 89 RVA: 0x0000234C File Offset: 0x0000054C
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; protected set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00002355 File Offset: 0x00000555
		// (set) Token: 0x0600005B RID: 91 RVA: 0x0000235D File Offset: 0x0000055D
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00002366 File Offset: 0x00000566
		// (set) Token: 0x0600005D RID: 93 RVA: 0x0000236E File Offset: 0x0000056E
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x0600005E RID: 94 RVA: 0x00002377 File Offset: 0x00000577
		public UserExtensionState(bool active, string id, string version)
		{
			this.Active = active;
			this.Id = id;
			this.Version = version;
		}
	}
}
