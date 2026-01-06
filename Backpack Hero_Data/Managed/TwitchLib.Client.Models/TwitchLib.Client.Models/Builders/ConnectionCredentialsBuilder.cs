using System;

namespace TwitchLib.Client.Models.Builders
{
	// Token: 0x02000030 RID: 48
	public sealed class ConnectionCredentialsBuilder : IBuilder<ConnectionCredentials>
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00007FEB File Offset: 0x000061EB
		private ConnectionCredentialsBuilder()
		{
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007FFE File Offset: 0x000061FE
		public ConnectionCredentialsBuilder WithTwitchUsername(string twitchUsername)
		{
			this._twitchUsername = twitchUsername;
			return this;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008008 File Offset: 0x00006208
		public ConnectionCredentialsBuilder WithTwitchOAuth(string twitchOAuth)
		{
			this._twitchOAuth = twitchOAuth;
			return this;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008012 File Offset: 0x00006212
		public ConnectionCredentialsBuilder WithTwitchWebSocketUri(string twitchWebSocketUri)
		{
			this._twitchWebsocketURI = twitchWebSocketUri;
			return this;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000801C File Offset: 0x0000621C
		public ConnectionCredentialsBuilder WithDisableUsernameCheck(bool disableUsernameCheck)
		{
			this._disableUsernameCheck = disableUsernameCheck;
			return this;
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008026 File Offset: 0x00006226
		public static ConnectionCredentialsBuilder Create()
		{
			return new ConnectionCredentialsBuilder();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000802D File Offset: 0x0000622D
		public ConnectionCredentials Build()
		{
			return new ConnectionCredentials(this._twitchUsername, this._twitchOAuth, this._twitchWebsocketURI, this._disableUsernameCheck, null);
		}

		// Token: 0x040001C9 RID: 457
		private string _twitchUsername;

		// Token: 0x040001CA RID: 458
		private string _twitchOAuth;

		// Token: 0x040001CB RID: 459
		private string _twitchWebsocketURI = "wss://irc-ws.chat.twitch.tv:443";

		// Token: 0x040001CC RID: 460
		private bool _disableUsernameCheck;
	}
}
