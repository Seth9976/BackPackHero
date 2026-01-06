using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core;
using TwitchLib.Api.Services.Events;

namespace TwitchLib.Api.Services
{
	// Token: 0x0200000F RID: 15
	public class ApiService
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002D51 File Offset: 0x00000F51
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002D59 File Offset: 0x00000F59
		public List<string> ChannelsToMonitor { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002D62 File Offset: 0x00000F62
		public int IntervalInSeconds
		{
			get
			{
				return this._serviceTimer.IntervalInSeconds;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002D6F File Offset: 0x00000F6F
		public bool Enabled
		{
			get
			{
				return this._serviceTimer.Enabled;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600006A RID: 106 RVA: 0x00002D7C File Offset: 0x00000F7C
		// (remove) Token: 0x0600006B RID: 107 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public event EventHandler<OnServiceStartedArgs> OnServiceStarted;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600006C RID: 108 RVA: 0x00002DEC File Offset: 0x00000FEC
		// (remove) Token: 0x0600006D RID: 109 RVA: 0x00002E24 File Offset: 0x00001024
		public event EventHandler<OnServiceStoppedArgs> OnServiceStopped;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600006E RID: 110 RVA: 0x00002E5C File Offset: 0x0000105C
		// (remove) Token: 0x0600006F RID: 111 RVA: 0x00002E94 File Offset: 0x00001094
		public event EventHandler<OnServiceTickArgs> OnServiceTick;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000070 RID: 112 RVA: 0x00002ECC File Offset: 0x000010CC
		// (remove) Token: 0x06000071 RID: 113 RVA: 0x00002F04 File Offset: 0x00001104
		public event EventHandler<OnChannelsSetArgs> OnChannelsSet;

		// Token: 0x06000072 RID: 114 RVA: 0x00002F3C File Offset: 0x0000113C
		protected ApiService(ITwitchAPI api, int checkIntervalInSeconds)
		{
			if (checkIntervalInSeconds < 1)
			{
				throw new ArgumentException("The interval must be 1 second or more.", "checkIntervalInSeconds");
			}
			if (api == null)
			{
				throw new ArgumentNullException("api");
			}
			this._api = api;
			this._serviceTimer = new ServiceTimer(new ServiceTimer.ServiceTimerTick(this.OnServiceTimerTick), checkIntervalInSeconds);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002F94 File Offset: 0x00001194
		public virtual void Start()
		{
			if (this.ChannelsToMonitor == null)
			{
				throw new InvalidOperationException("You must atleast add 1 channel to service before starting it.");
			}
			if (this._serviceTimer.Enabled)
			{
				throw new InvalidOperationException("The service has already been started.");
			}
			this._serviceTimer.Start();
			EventHandler<OnServiceStartedArgs> onServiceStarted = this.OnServiceStarted;
			if (onServiceStarted == null)
			{
				return;
			}
			onServiceStarted.Invoke(this, new OnServiceStartedArgs());
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002FED File Offset: 0x000011ED
		public virtual void Stop()
		{
			if (!this._serviceTimer.Enabled)
			{
				throw new InvalidOperationException("The service hasn't started yet, or has already been stopped.");
			}
			this._serviceTimer.Stop();
			EventHandler<OnServiceStoppedArgs> onServiceStopped = this.OnServiceStopped;
			if (onServiceStopped == null)
			{
				return;
			}
			onServiceStopped.Invoke(this, new OnServiceStoppedArgs());
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003028 File Offset: 0x00001228
		protected virtual void SetChannels(List<string> channelsToMonitor)
		{
			if (channelsToMonitor == null)
			{
				throw new ArgumentNullException("channelsToMonitor");
			}
			if (channelsToMonitor.Count == 0)
			{
				throw new ArgumentException("The provided list is empty.", "channelsToMonitor");
			}
			this.ChannelsToMonitor = channelsToMonitor;
			EventHandler<OnChannelsSetArgs> onChannelsSet = this.OnChannelsSet;
			if (onChannelsSet == null)
			{
				return;
			}
			onChannelsSet.Invoke(this, new OnChannelsSetArgs
			{
				Channels = channelsToMonitor
			});
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000307F File Offset: 0x0000127F
		protected virtual Task OnServiceTimerTick()
		{
			EventHandler<OnServiceTickArgs> onServiceTick = this.OnServiceTick;
			if (onServiceTick != null)
			{
				onServiceTick.Invoke(this, new OnServiceTickArgs());
			}
			return Task.CompletedTask;
		}

		// Token: 0x04000031 RID: 49
		protected readonly ITwitchAPI _api;

		// Token: 0x04000032 RID: 50
		private readonly ServiceTimer _serviceTimer;
	}
}
