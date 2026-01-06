using System;
using System.ComponentModel;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Auth;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;
using TwitchLib.Api.Core.Undocumented;
using TwitchLib.Api.Helix;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.ThirdParty;

namespace TwitchLib.Api
{
	// Token: 0x02000002 RID: 2
	public class TwitchAPI : ITwitchAPI
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public IApiSettings Settings { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public Auth Auth { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public Helix Helix { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000004 RID: 4 RVA: 0x00002068 File Offset: 0x00000268
		public ThirdParty ThirdParty { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002070 File Offset: 0x00000270
		public Undocumented Undocumented { get; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002078 File Offset: 0x00000278
		public TwitchAPI(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
		{
			this._logger = ((loggerFactory != null) ? loggerFactory.CreateLogger<TwitchAPI>() : null);
			rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
			IHttpCallHandler httpCallHandler;
			if ((httpCallHandler = http) == null)
			{
				httpCallHandler = new TwitchHttpClient((loggerFactory != null) ? loggerFactory.CreateLogger<TwitchHttpClient>() : null);
			}
			http = httpCallHandler;
			this.Settings = settings ?? new ApiSettings();
			this.Auth = new Auth(this.Settings, rateLimiter, http);
			this.Helix = new Helix(loggerFactory, rateLimiter, this.Settings, http);
			this.ThirdParty = new ThirdParty(this.Settings, rateLimiter, http);
			this.Undocumented = new Undocumented(this.Settings, rateLimiter, http);
			this.Settings.PropertyChanged += new PropertyChangedEventHandler(this.SettingsPropertyChanged);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		private void SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			string propertyName = e.PropertyName;
			if (propertyName != null)
			{
				if (propertyName == "AccessToken")
				{
					this.Helix.Settings.AccessToken = this.Settings.AccessToken;
					return;
				}
				if (propertyName == "Secret")
				{
					this.Helix.Settings.Secret = this.Settings.Secret;
					return;
				}
				if (propertyName == "ClientId")
				{
					this.Helix.Settings.ClientId = this.Settings.ClientId;
					return;
				}
				if (propertyName == "SkipDynamicScopeValidation")
				{
					this.Helix.Settings.SkipDynamicScopeValidation = this.Settings.SkipDynamicScopeValidation;
					return;
				}
				if (propertyName == "SkipAutoServerTokenGeneration")
				{
					this.Helix.Settings.SkipAutoServerTokenGeneration = this.Settings.SkipAutoServerTokenGeneration;
					return;
				}
				if (!(propertyName == "Scopes"))
				{
					return;
				}
				this.Helix.Settings.Scopes = this.Settings.Scopes;
			}
		}

		// Token: 0x04000001 RID: 1
		private readonly ILogger<TwitchAPI> _logger;
	}
}
