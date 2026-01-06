using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.Redemption
{
	// Token: 0x02000020 RID: 32
	public class MaxPerUserPerStream
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00006FAC File Offset: 0x000051AC
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00006FB4 File Offset: 0x000051B4
		[JsonProperty(PropertyName = "is_enabled")]
		public string IsEnabled { get; protected set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00006FBD File Offset: 0x000051BD
		// (set) Token: 0x06000161 RID: 353 RVA: 0x00006FC5 File Offset: 0x000051C5
		[JsonProperty(PropertyName = "max_per_user_per_stream")]
		public int MaxPerUserPerStreamValue { get; protected set; }
	}
}
