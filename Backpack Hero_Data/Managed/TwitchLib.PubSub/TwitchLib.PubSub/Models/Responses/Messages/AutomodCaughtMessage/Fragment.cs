using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x02000029 RID: 41
	public class Fragment
	{
		// Token: 0x040000E5 RID: 229
		[JsonProperty(PropertyName = "text")]
		public string Text;

		// Token: 0x040000E6 RID: 230
		[JsonProperty(PropertyName = "automod")]
		public Automod Automod;
	}
}
