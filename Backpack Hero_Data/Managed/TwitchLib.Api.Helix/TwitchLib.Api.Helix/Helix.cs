using System;
using Microsoft.Extensions.Logging;
using TwitchLib.Api.Core;
using TwitchLib.Api.Core.HttpCallHandlers;
using TwitchLib.Api.Core.Interfaces;
using TwitchLib.Api.Core.RateLimiter;

namespace TwitchLib.Api.Helix
{
	// Token: 0x02000014 RID: 20
	public class Helix
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003E13 File Offset: 0x00002013
		public IApiSettings Settings { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003E1B File Offset: 0x0000201B
		public Analytics Analytics { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003E23 File Offset: 0x00002023
		public Ads Ads { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003E2B File Offset: 0x0000202B
		public Bits Bits { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003E33 File Offset: 0x00002033
		public Chat Chat { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00003E3B File Offset: 0x0000203B
		public Channels Channels { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003E43 File Offset: 0x00002043
		public ChannelPoints ChannelPoints { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003E4B File Offset: 0x0000204B
		public Charity Charity { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003E53 File Offset: 0x00002053
		public Clips Clips { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003E5B File Offset: 0x0000205B
		public Entitlements Entitlements { get; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003E63 File Offset: 0x00002063
		public EventSub EventSub { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00003E6B File Offset: 0x0000206B
		public Extensions Extensions { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003E73 File Offset: 0x00002073
		public Games Games { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00003E7B File Offset: 0x0000207B
		public Goals Goals { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003E83 File Offset: 0x00002083
		public HypeTrain HypeTrain { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00003E8B File Offset: 0x0000208B
		public Moderation Moderation { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003E93 File Offset: 0x00002093
		public Polls Polls { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003E9B File Offset: 0x0000209B
		public Predictions Predictions { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003EA3 File Offset: 0x000020A3
		public Raids Raids { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00003EAB File Offset: 0x000020AB
		public Schedule Schedule { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003EB3 File Offset: 0x000020B3
		public Search Search { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003EBB File Offset: 0x000020BB
		public Soundtrack Soundtrack { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003EC3 File Offset: 0x000020C3
		public Streams Streams { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003ECB File Offset: 0x000020CB
		public Subscriptions Subscriptions { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003ED3 File Offset: 0x000020D3
		public Tags Tags { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00003EDB File Offset: 0x000020DB
		public Teams Teams { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003EE3 File Offset: 0x000020E3
		public Videos Videos { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003EEB File Offset: 0x000020EB
		public Users Users { get; }

		// Token: 0x06000079 RID: 121 RVA: 0x00003EF4 File Offset: 0x000020F4
		public Helix(ILoggerFactory loggerFactory = null, IRateLimiter rateLimiter = null, IApiSettings settings = null, IHttpCallHandler http = null)
		{
			this._logger = ((loggerFactory != null) ? loggerFactory.CreateLogger<Helix>() : null);
			rateLimiter = rateLimiter ?? BypassLimiter.CreateLimiterBypassInstance();
			IHttpCallHandler httpCallHandler;
			if ((httpCallHandler = http) == null)
			{
				httpCallHandler = new TwitchHttpClient((loggerFactory != null) ? loggerFactory.CreateLogger<TwitchHttpClient>() : null);
			}
			http = httpCallHandler;
			this.Settings = settings ?? new ApiSettings();
			this.Analytics = new Analytics(this.Settings, rateLimiter, http);
			this.Ads = new Ads(this.Settings, rateLimiter, http);
			this.Bits = new Bits(this.Settings, rateLimiter, http);
			this.Chat = new Chat(this.Settings, rateLimiter, http);
			this.Channels = new Channels(this.Settings, rateLimiter, http);
			this.ChannelPoints = new ChannelPoints(this.Settings, rateLimiter, http);
			this.Charity = new Charity(this.Settings, rateLimiter, http);
			this.Clips = new Clips(this.Settings, rateLimiter, http);
			this.Entitlements = new Entitlements(this.Settings, rateLimiter, http);
			this.EventSub = new EventSub(this.Settings, rateLimiter, http);
			this.Extensions = new Extensions(this.Settings, rateLimiter, http);
			this.Games = new Games(this.Settings, rateLimiter, http);
			this.Goals = new Goals(settings, rateLimiter, http);
			this.HypeTrain = new HypeTrain(this.Settings, rateLimiter, http);
			this.Moderation = new Moderation(this.Settings, rateLimiter, http);
			this.Polls = new Polls(this.Settings, rateLimiter, http);
			this.Predictions = new Predictions(this.Settings, rateLimiter, http);
			this.Raids = new Raids(settings, rateLimiter, http);
			this.Schedule = new Schedule(this.Settings, rateLimiter, http);
			this.Search = new Search(this.Settings, rateLimiter, http);
			this.Soundtrack = new Soundtrack(this.Settings, rateLimiter, http);
			this.Streams = new Streams(this.Settings, rateLimiter, http);
			this.Subscriptions = new Subscriptions(this.Settings, rateLimiter, http);
			this.Tags = new Tags(this.Settings, rateLimiter, http);
			this.Teams = new Teams(this.Settings, rateLimiter, http);
			this.Users = new Users(this.Settings, rateLimiter, http);
			this.Videos = new Videos(this.Settings, rateLimiter, http);
		}

		// Token: 0x0400000D RID: 13
		private readonly ILogger<Helix> _logger;
	}
}
