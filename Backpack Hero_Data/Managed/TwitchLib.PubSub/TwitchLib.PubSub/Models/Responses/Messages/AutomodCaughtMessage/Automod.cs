using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TwitchLib.PubSub.Models.Responses.Messages.AutomodCaughtMessage
{
	// Token: 0x02000025 RID: 37
	public class Automod
	{
		// Token: 0x040000DA RID: 218
		[JsonProperty(PropertyName = "topics")]
		public Dictionary<string, int> Topics;
	}
}
