using System;
using System.Text.RegularExpressions;

namespace TwitchLib.Client.Models
{
	// Token: 0x02000009 RID: 9
	public class ConnectionCredentials
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600005A RID: 90 RVA: 0x00003F5C File Offset: 0x0000215C
		public string TwitchWebsocketURI { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003F64 File Offset: 0x00002164
		public string TwitchOAuth { get; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00003F6C File Offset: 0x0000216C
		public string TwitchUsername { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003F74 File Offset: 0x00002174
		public Capabilities Capabilities { get; }

		// Token: 0x0600005E RID: 94 RVA: 0x00003F7C File Offset: 0x0000217C
		public ConnectionCredentials(string twitchUsername, string twitchOAuth, string twitchWebsocketURI = "wss://irc-ws.chat.twitch.tv:443", bool disableUsernameCheck = false, Capabilities capabilities = null)
		{
			if (!disableUsernameCheck && !new Regex("^([a-zA-Z0-9][a-zA-Z0-9_]{3,25})$").Match(twitchUsername).Success)
			{
				throw new Exception("Twitch username does not appear to be valid. " + twitchUsername);
			}
			this.TwitchUsername = twitchUsername.ToLower();
			this.TwitchOAuth = twitchOAuth;
			if (!twitchOAuth.Contains(":"))
			{
				this.TwitchOAuth = "oauth:" + twitchOAuth.Replace("oauth", "");
			}
			this.TwitchWebsocketURI = twitchWebsocketURI;
			if (capabilities == null)
			{
				capabilities = new Capabilities(true, true, true);
			}
			this.Capabilities = capabilities;
		}

		// Token: 0x0400005E RID: 94
		public const string DefaultWebSocketUri = "wss://irc-ws.chat.twitch.tv:443";
	}
}
