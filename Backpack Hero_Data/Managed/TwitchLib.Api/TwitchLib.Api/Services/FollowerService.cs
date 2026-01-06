using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Users.GetUserFollows;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core.FollowerService;
using TwitchLib.Api.Services.Events.FollowerService;

namespace TwitchLib.Api.Services
{
	// Token: 0x02000010 RID: 16
	public class FollowerService : ApiService
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000309D File Offset: 0x0000129D
		public Dictionary<string, List<Follow>> KnownFollowers { get; } = new Dictionary<string, List<Follow>>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000078 RID: 120 RVA: 0x000030A5 File Offset: 0x000012A5
		public int QueryCountPerRequest { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000079 RID: 121 RVA: 0x000030AD File Offset: 0x000012AD
		public int CacheSize { get; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000030B8 File Offset: 0x000012B8
		private IdBasedMonitor IdBasedMonitor
		{
			get
			{
				IdBasedMonitor idBasedMonitor;
				if ((idBasedMonitor = this._idBasedMonitor) == null)
				{
					idBasedMonitor = (this._idBasedMonitor = new IdBasedMonitor(this._api));
				}
				return idBasedMonitor;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600007B RID: 123 RVA: 0x000030E4 File Offset: 0x000012E4
		private NameBasedMonitor NameBasedMonitor
		{
			get
			{
				NameBasedMonitor nameBasedMonitor;
				if ((nameBasedMonitor = this._nameBasedMonitor) == null)
				{
					nameBasedMonitor = (this._nameBasedMonitor = new NameBasedMonitor(this._api));
				}
				return nameBasedMonitor;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600007C RID: 124 RVA: 0x00003110 File Offset: 0x00001310
		// (remove) Token: 0x0600007D RID: 125 RVA: 0x00003148 File Offset: 0x00001348
		public event EventHandler<OnNewFollowersDetectedArgs> OnNewFollowersDetected;

		// Token: 0x0600007E RID: 126 RVA: 0x00003180 File Offset: 0x00001380
		public FollowerService(ITwitchAPI api, int checkIntervalInSeconds = 60, int queryCountPerRequest = 100, int cacheSize = 1000, bool invokeEventsOnStartup = false)
			: base(api, checkIntervalInSeconds)
		{
			if (queryCountPerRequest < 1 || queryCountPerRequest > 100)
			{
				throw new ArgumentException("Twitch doesn't support less than 1 or more than 100 followers per request.", "queryCountPerRequest");
			}
			if (cacheSize < queryCountPerRequest)
			{
				throw new ArgumentException("The cache size must be at least the size of the queryCountPerRequest parameter.", "cacheSize");
			}
			this.QueryCountPerRequest = queryCountPerRequest;
			this.CacheSize = cacheSize;
			this._invokeEventsOnStartup = invokeEventsOnStartup;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000031FA File Offset: 0x000013FA
		public void ClearCache()
		{
			this.KnownFollowers.Clear();
			this._lastFollowerDates.Clear();
			NameBasedMonitor nameBasedMonitor = this._nameBasedMonitor;
			if (nameBasedMonitor != null)
			{
				nameBasedMonitor.ClearCache();
			}
			this._nameBasedMonitor = null;
			this._idBasedMonitor = null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003231 File Offset: 0x00001431
		public void SetChannelsById(List<string> channelsToMonitor)
		{
			this.SetChannels(channelsToMonitor);
			this._monitor = this.IdBasedMonitor;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003246 File Offset: 0x00001446
		public void SetChannelsByName(List<string> channelsToMonitor)
		{
			this.SetChannels(channelsToMonitor);
			this._monitor = this.NameBasedMonitor;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000325C File Offset: 0x0000145C
		public Task UpdateLatestFollowersAsync(bool callEvents = true)
		{
			FollowerService.<UpdateLatestFollowersAsync>d__25 <UpdateLatestFollowersAsync>d__;
			<UpdateLatestFollowersAsync>d__.<>4__this = this;
			<UpdateLatestFollowersAsync>d__.callEvents = callEvents;
			<UpdateLatestFollowersAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateLatestFollowersAsync>d__.<>1__state = -1;
			<UpdateLatestFollowersAsync>d__.<>t__builder.Start<FollowerService.<UpdateLatestFollowersAsync>d__25>(ref <UpdateLatestFollowersAsync>d__);
			return <UpdateLatestFollowersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000032A8 File Offset: 0x000014A8
		protected override Task OnServiceTimerTick()
		{
			FollowerService.<OnServiceTimerTick>d__26 <OnServiceTimerTick>d__;
			<OnServiceTimerTick>d__.<>4__this = this;
			<OnServiceTimerTick>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnServiceTimerTick>d__.<>1__state = -1;
			<OnServiceTimerTick>d__.<>t__builder.Start<FollowerService.<OnServiceTimerTick>d__26>(ref <OnServiceTimerTick>d__);
			return <OnServiceTimerTick>d__.<>t__builder.Task;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000032EC File Offset: 0x000014EC
		private Task<List<Follow>> GetLatestFollowersAsync(string channel)
		{
			FollowerService.<GetLatestFollowersAsync>d__27 <GetLatestFollowersAsync>d__;
			<GetLatestFollowersAsync>d__.<>4__this = this;
			<GetLatestFollowersAsync>d__.channel = channel;
			<GetLatestFollowersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<Follow>>.Create();
			<GetLatestFollowersAsync>d__.<>1__state = -1;
			<GetLatestFollowersAsync>d__.<>t__builder.Start<FollowerService.<GetLatestFollowersAsync>d__27>(ref <GetLatestFollowersAsync>d__);
			return <GetLatestFollowersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000038 RID: 56
		private readonly Dictionary<string, DateTime> _lastFollowerDates = new Dictionary<string, DateTime>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000039 RID: 57
		private readonly bool _invokeEventsOnStartup;

		// Token: 0x0400003A RID: 58
		private CoreMonitor _monitor;

		// Token: 0x0400003B RID: 59
		private IdBasedMonitor _idBasedMonitor;

		// Token: 0x0400003C RID: 60
		private NameBasedMonitor _nameBasedMonitor;
	}
}
