using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using TwitchLib.Api.Interfaces;
using TwitchLib.Api.Services.Core.LiveStreamMonitor;
using TwitchLib.Api.Services.Events.LiveStreamMonitor;

namespace TwitchLib.Api.Services
{
	// Token: 0x02000011 RID: 17
	public class LiveStreamMonitorService : ApiService
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000333F File Offset: 0x0000153F
		public Dictionary<string, Stream> LiveStreams { get; } = new Dictionary<string, Stream>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003347 File Offset: 0x00001547
		public int MaxStreamRequestCountPerRequest { get; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003350 File Offset: 0x00001550
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

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000089 RID: 137 RVA: 0x0000337C File Offset: 0x0000157C
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

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600008A RID: 138 RVA: 0x000033A8 File Offset: 0x000015A8
		// (remove) Token: 0x0600008B RID: 139 RVA: 0x000033E0 File Offset: 0x000015E0
		public event EventHandler<OnStreamOnlineArgs> OnStreamOnline;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x0600008C RID: 140 RVA: 0x00003418 File Offset: 0x00001618
		// (remove) Token: 0x0600008D RID: 141 RVA: 0x00003450 File Offset: 0x00001650
		public event EventHandler<OnStreamOfflineArgs> OnStreamOffline;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x0600008E RID: 142 RVA: 0x00003488 File Offset: 0x00001688
		// (remove) Token: 0x0600008F RID: 143 RVA: 0x000034C0 File Offset: 0x000016C0
		public event EventHandler<OnStreamUpdateArgs> OnStreamUpdate;

		// Token: 0x06000090 RID: 144 RVA: 0x000034F5 File Offset: 0x000016F5
		public LiveStreamMonitorService(ITwitchAPI api, int checkIntervalInSeconds = 60, int maxStreamRequestCountPerRequest = 100)
			: base(api, checkIntervalInSeconds)
		{
			if (maxStreamRequestCountPerRequest < 1 || maxStreamRequestCountPerRequest > 100)
			{
				throw new ArgumentException("Twitch doesn't support less than 1 or more than 100 streams per request.", "maxStreamRequestCountPerRequest");
			}
			this.MaxStreamRequestCountPerRequest = maxStreamRequestCountPerRequest;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000352F File Offset: 0x0000172F
		public void ClearCache()
		{
			this.LiveStreams.Clear();
			NameBasedMonitor nameBasedMonitor = this._nameBasedMonitor;
			if (nameBasedMonitor != null)
			{
				nameBasedMonitor.ClearCache();
			}
			this._nameBasedMonitor = null;
			this._idBasedMonitor = null;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000355B File Offset: 0x0000175B
		public void SetChannelsById(List<string> channelsToMonitor)
		{
			this.SetChannels(channelsToMonitor);
			this._monitor = this.IdBasedMonitor;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003570 File Offset: 0x00001770
		public void SetChannelsByName(List<string> channelsToMonitor)
		{
			this.SetChannels(channelsToMonitor);
			this._monitor = this.NameBasedMonitor;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003588 File Offset: 0x00001788
		public Task UpdateLiveStreamersAsync(bool callEvents = true)
		{
			LiveStreamMonitorService.<UpdateLiveStreamersAsync>d__26 <UpdateLiveStreamersAsync>d__;
			<UpdateLiveStreamersAsync>d__.<>4__this = this;
			<UpdateLiveStreamersAsync>d__.callEvents = callEvents;
			<UpdateLiveStreamersAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<UpdateLiveStreamersAsync>d__.<>1__state = -1;
			<UpdateLiveStreamersAsync>d__.<>t__builder.Start<LiveStreamMonitorService.<UpdateLiveStreamersAsync>d__26>(ref <UpdateLiveStreamersAsync>d__);
			return <UpdateLiveStreamersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000035D4 File Offset: 0x000017D4
		protected override Task OnServiceTimerTick()
		{
			LiveStreamMonitorService.<OnServiceTimerTick>d__27 <OnServiceTimerTick>d__;
			<OnServiceTimerTick>d__.<>4__this = this;
			<OnServiceTimerTick>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<OnServiceTimerTick>d__.<>1__state = -1;
			<OnServiceTimerTick>d__.<>t__builder.Start<LiveStreamMonitorService.<OnServiceTimerTick>d__27>(ref <OnServiceTimerTick>d__);
			return <OnServiceTimerTick>d__.<>t__builder.Task;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00003618 File Offset: 0x00001818
		private void HandleLiveStreamUpdate(string channel, Stream liveStream, bool callEvents)
		{
			bool flag = this.LiveStreams.ContainsKey(channel);
			this.LiveStreams[channel] = liveStream;
			if (!callEvents)
			{
				return;
			}
			if (!flag)
			{
				EventHandler<OnStreamOnlineArgs> onStreamOnline = this.OnStreamOnline;
				if (onStreamOnline == null)
				{
					return;
				}
				onStreamOnline.Invoke(this, new OnStreamOnlineArgs
				{
					Channel = channel,
					Stream = liveStream
				});
				return;
			}
			else
			{
				EventHandler<OnStreamUpdateArgs> onStreamUpdate = this.OnStreamUpdate;
				if (onStreamUpdate == null)
				{
					return;
				}
				onStreamUpdate.Invoke(this, new OnStreamUpdateArgs
				{
					Channel = channel,
					Stream = liveStream
				});
				return;
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003690 File Offset: 0x00001890
		private void HandleOfflineStreamUpdate(string channel, bool callEvents)
		{
			Stream stream;
			if (!this.LiveStreams.TryGetValue(channel, ref stream))
			{
				return;
			}
			this.LiveStreams.Remove(channel);
			if (!callEvents)
			{
				return;
			}
			EventHandler<OnStreamOfflineArgs> onStreamOffline = this.OnStreamOffline;
			if (onStreamOffline == null)
			{
				return;
			}
			onStreamOffline.Invoke(this, new OnStreamOfflineArgs
			{
				Channel = channel,
				Stream = stream
			});
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000036E4 File Offset: 0x000018E4
		private Task<List<Stream>> GetLiveStreamersAsync()
		{
			LiveStreamMonitorService.<GetLiveStreamersAsync>d__30 <GetLiveStreamersAsync>d__;
			<GetLiveStreamersAsync>d__.<>4__this = this;
			<GetLiveStreamersAsync>d__.<>t__builder = AsyncTaskMethodBuilder<List<Stream>>.Create();
			<GetLiveStreamersAsync>d__.<>1__state = -1;
			<GetLiveStreamersAsync>d__.<>t__builder.Start<LiveStreamMonitorService.<GetLiveStreamersAsync>d__30>(ref <GetLiveStreamersAsync>d__);
			return <GetLiveStreamersAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000041 RID: 65
		private CoreMonitor _monitor;

		// Token: 0x04000042 RID: 66
		private IdBasedMonitor _idBasedMonitor;

		// Token: 0x04000043 RID: 67
		private NameBasedMonitor _nameBasedMonitor;
	}
}
