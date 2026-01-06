using System;
using Newtonsoft.Json.Linq;

namespace TwitchLib.PubSub.Extensions
{
	// Token: 0x0200002D RID: 45
	public static class JSONObjectExtensions
	{
		// Token: 0x06000216 RID: 534 RVA: 0x000072B4 File Offset: 0x000054B4
		public static bool IsEmpty(this JToken token)
		{
			return token == null || (token.Type == 2 && !token.HasValues) || (token.Type == 1 && !token.HasValues) || (token.Type == 8 && token.ToString() == string.Empty) || token.Type == 10;
		}
	}
}
