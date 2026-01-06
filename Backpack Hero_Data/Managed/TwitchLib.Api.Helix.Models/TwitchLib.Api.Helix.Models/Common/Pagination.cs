using System;
using Newtonsoft.Json;

namespace TwitchLib.Api.Helix.Models.Common
{
	// Token: 0x02000092 RID: 146
	public class Pagination
	{
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x00004A25 File Offset: 0x00002C25
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x00004A2D File Offset: 0x00002C2D
		[JsonProperty(PropertyName = "cursor")]
		public string Cursor { get; protected set; }
	}
}
