using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Users.Internal
{
	// Token: 0x02000009 RID: 9
	public class UserActiveExtension
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002279 File Offset: 0x00000479
		// (set) Token: 0x06000041 RID: 65 RVA: 0x00002281 File Offset: 0x00000481
		[JsonProperty(PropertyName = "active")]
		public bool Active { get; protected set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000228A File Offset: 0x0000048A
		// (set) Token: 0x06000043 RID: 67 RVA: 0x00002292 File Offset: 0x00000492
		[JsonProperty(PropertyName = "id")]
		public string Id { get; protected set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x06000045 RID: 69 RVA: 0x000022A3 File Offset: 0x000004A3
		[JsonProperty(PropertyName = "version")]
		public string Version { get; protected set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000022AC File Offset: 0x000004AC
		// (set) Token: 0x06000047 RID: 71 RVA: 0x000022B4 File Offset: 0x000004B4
		[JsonProperty(PropertyName = "name")]
		public string Name { get; protected set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000048 RID: 72 RVA: 0x000022BD File Offset: 0x000004BD
		// (set) Token: 0x06000049 RID: 73 RVA: 0x000022C5 File Offset: 0x000004C5
		[JsonProperty(PropertyName = "x")]
		public int X { get; protected set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600004A RID: 74 RVA: 0x000022CE File Offset: 0x000004CE
		// (set) Token: 0x0600004B RID: 75 RVA: 0x000022D6 File Offset: 0x000004D6
		[JsonProperty(PropertyName = "y")]
		public int Y { get; protected set; }
	}
}
