using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Timers;

namespace TwitchLib.Api.Services.Core
{
	// Token: 0x0200001A RID: 26
	internal class ServiceTimer : Timer
	{
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x0000376F File Offset: 0x0000196F
		public int IntervalInSeconds { get; }

		// Token: 0x060000A3 RID: 163 RVA: 0x00003777 File Offset: 0x00001977
		public ServiceTimer(ServiceTimer.ServiceTimerTick serviceTimerTickAsyncCallback, int intervalInSeconds = 60)
		{
			this._serviceTimerTickAsyncCallback = serviceTimerTickAsyncCallback;
			base.Interval = (double)(intervalInSeconds * 1000);
			this.IntervalInSeconds = intervalInSeconds;
			base.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				ServiceTimer.<<-ctor>b__5_0>d <<-ctor>b__5_0>d;
				<<-ctor>b__5_0>d.<>4__this = this;
				<<-ctor>b__5_0>d.sender = sender;
				<<-ctor>b__5_0>d.e = e;
				<<-ctor>b__5_0>d.<>t__builder = AsyncVoidMethodBuilder.Create();
				<<-ctor>b__5_0>d.<>1__state = -1;
				<<-ctor>b__5_0>d.<>t__builder.Start<ServiceTimer.<<-ctor>b__5_0>d>(ref <<-ctor>b__5_0>d);
			};
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000037B0 File Offset: 0x000019B0
		private Task TimerElapsedAsync(object sender, ElapsedEventArgs e)
		{
			ServiceTimer.<TimerElapsedAsync>d__6 <TimerElapsedAsync>d__;
			<TimerElapsedAsync>d__.<>4__this = this;
			<TimerElapsedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<TimerElapsedAsync>d__.<>1__state = -1;
			<TimerElapsedAsync>d__.<>t__builder.Start<ServiceTimer.<TimerElapsedAsync>d__6>(ref <TimerElapsedAsync>d__);
			return <TimerElapsedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000053 RID: 83
		private readonly ServiceTimer.ServiceTimerTick _serviceTimerTickAsyncCallback;

		// Token: 0x02000034 RID: 52
		// (Invoke) Token: 0x06000115 RID: 277
		public delegate Task ServiceTimerTick();
	}
}
