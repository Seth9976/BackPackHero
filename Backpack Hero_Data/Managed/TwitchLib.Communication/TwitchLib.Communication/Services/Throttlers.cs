using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using TwitchLib.Communication.Interfaces;

namespace TwitchLib.Communication.Services
{
	// Token: 0x02000002 RID: 2
	public class Throttlers
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public bool Reconnecting { get; set; } = false;

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002061 File Offset: 0x00000261
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002069 File Offset: 0x00000269
		public bool ShouldDispose { get; set; } = false;

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002072 File Offset: 0x00000272
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207A File Offset: 0x0000027A
		public CancellationTokenSource TokenSource { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002084 File Offset: 0x00000284
		public Throttlers(IClient client, TimeSpan throttlingPeriod, TimeSpan whisperThrottlingPeriod)
		{
			this._throttlingPeriod = throttlingPeriod;
			this._whisperThrottlingPeriod = whisperThrottlingPeriod;
			this._client = client;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020E0 File Offset: 0x000002E0
		public void StartThrottlingWindowReset()
		{
			this.ResetThrottler = Task.Run<Task>(delegate
			{
				Throttlers.<<StartThrottlingWindowReset>b__24_0>d <<StartThrottlingWindowReset>b__24_0>d = new Throttlers.<<StartThrottlingWindowReset>b__24_0>d();
				<<StartThrottlingWindowReset>b__24_0>d.<>4__this = this;
				<<StartThrottlingWindowReset>b__24_0>d.<>t__builder = AsyncTaskMethodBuilder<Task>.Create();
				<<StartThrottlingWindowReset>b__24_0>d.<>1__state = -1;
				<<StartThrottlingWindowReset>b__24_0>d.<>t__builder.Start<Throttlers.<<StartThrottlingWindowReset>b__24_0>d>(ref <<StartThrottlingWindowReset>b__24_0>d);
				return <<StartThrottlingWindowReset>b__24_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020FA File Offset: 0x000002FA
		public void StartWhisperThrottlingWindowReset()
		{
			this.ResetWhisperThrottler = Task.Run<Task>(delegate
			{
				Throttlers.<<StartWhisperThrottlingWindowReset>b__25_0>d <<StartWhisperThrottlingWindowReset>b__25_0>d = new Throttlers.<<StartWhisperThrottlingWindowReset>b__25_0>d();
				<<StartWhisperThrottlingWindowReset>b__25_0>d.<>4__this = this;
				<<StartWhisperThrottlingWindowReset>b__25_0>d.<>t__builder = AsyncTaskMethodBuilder<Task>.Create();
				<<StartWhisperThrottlingWindowReset>b__25_0>d.<>1__state = -1;
				<<StartWhisperThrottlingWindowReset>b__25_0>d.<>t__builder.Start<Throttlers.<<StartWhisperThrottlingWindowReset>b__25_0>d>(ref <<StartWhisperThrottlingWindowReset>b__25_0>d);
				return <<StartWhisperThrottlingWindowReset>b__25_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002114 File Offset: 0x00000314
		public void IncrementSentCount()
		{
			Interlocked.Increment(ref this.SentCount);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002123 File Offset: 0x00000323
		public void IncrementWhisperCount()
		{
			Interlocked.Increment(ref this.WhispersSent);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		public Task StartSenderTask()
		{
			this.StartThrottlingWindowReset();
			return Task.Run(delegate
			{
				Throttlers.<<StartSenderTask>b__28_0>d <<StartSenderTask>b__28_0>d = new Throttlers.<<StartSenderTask>b__28_0>d();
				<<StartSenderTask>b__28_0>d.<>4__this = this;
				<<StartSenderTask>b__28_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<StartSenderTask>b__28_0>d.<>1__state = -1;
				<<StartSenderTask>b__28_0>d.<>t__builder.Start<Throttlers.<<StartSenderTask>b__28_0>d>(ref <<StartSenderTask>b__28_0>d);
				return <<StartSenderTask>b__28_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002160 File Offset: 0x00000360
		public Task StartWhisperSenderTask()
		{
			this.StartWhisperThrottlingWindowReset();
			return Task.Run(delegate
			{
				Throttlers.<<StartWhisperSenderTask>b__29_0>d <<StartWhisperSenderTask>b__29_0>d = new Throttlers.<<StartWhisperSenderTask>b__29_0>d();
				<<StartWhisperSenderTask>b__29_0>d.<>4__this = this;
				<<StartWhisperSenderTask>b__29_0>d.<>t__builder = AsyncTaskMethodBuilder.Create();
				<<StartWhisperSenderTask>b__29_0>d.<>1__state = -1;
				<<StartWhisperSenderTask>b__29_0>d.<>t__builder.Start<Throttlers.<<StartWhisperSenderTask>b__29_0>d>(ref <<StartWhisperSenderTask>b__29_0>d);
				return <<StartWhisperSenderTask>b__29_0>d.<>t__builder.Task;
			});
		}

		// Token: 0x04000001 RID: 1
		public readonly BlockingCollection<Tuple<DateTime, string>> SendQueue = new BlockingCollection<Tuple<DateTime, string>>();

		// Token: 0x04000002 RID: 2
		public readonly BlockingCollection<Tuple<DateTime, string>> WhisperQueue = new BlockingCollection<Tuple<DateTime, string>>();

		// Token: 0x04000006 RID: 6
		public bool ResetThrottlerRunning;

		// Token: 0x04000007 RID: 7
		public bool ResetWhisperThrottlerRunning;

		// Token: 0x04000008 RID: 8
		public int SentCount = 0;

		// Token: 0x04000009 RID: 9
		public int WhispersSent = 0;

		// Token: 0x0400000A RID: 10
		public Task ResetThrottler;

		// Token: 0x0400000B RID: 11
		public Task ResetWhisperThrottler;

		// Token: 0x0400000C RID: 12
		private readonly TimeSpan _throttlingPeriod;

		// Token: 0x0400000D RID: 13
		private readonly TimeSpan _whisperThrottlingPeriod;

		// Token: 0x0400000E RID: 14
		private readonly IClient _client;
	}
}
