using System;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x02000027 RID: 39
	public class Content
	{
		// Token: 0x040000E1 RID: 225
		[JsonProperty(PropertyName = "text")]
		public string Text;

		// Token: 0x040000E2 RID: 226
		[JsonProperty(PropertyName = "fragments")]
		public Fragment[] Fragments;
	}
}
