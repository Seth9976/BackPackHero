using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Mono.Net.Security
{
	// Token: 0x0200008B RID: 139
	internal abstract class AsyncProtocolRequest
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00006382 File Offset: 0x00004582
		public MobileAuthenticatedStream Parent { get; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000638A File Offset: 0x0000458A
		public bool RunSynchronously { get; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00006392 File Offset: 0x00004592
		public int ID
		{
			get
			{
				return ++AsyncProtocolRequest.next_id;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000225 RID: 549 RVA: 0x000063A1 File Offset: 0x000045A1
		public string Name
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000226 RID: 550 RVA: 0x000063AE File Offset: 0x000045AE
		// (set) Token: 0x06000227 RID: 551 RVA: 0x000063B6 File Offset: 0x000045B6
		public int UserResult { get; protected set; }

		// Token: 0x06000228 RID: 552 RVA: 0x000063BF File Offset: 0x000045BF
		public AsyncProtocolRequest(MobileAuthenticatedStream parent, bool sync)
		{
			this.Parent = parent;
			this.RunSynchronously = sync;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00003917 File Offset: 0x00001B17
		[Conditional("MONO_TLS_DEBUG")]
		protected void Debug(string message, params object[] args)
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000063E0 File Offset: 0x000045E0
		internal void RequestRead(int size)
		{
			object obj = this.locker;
			lock (obj)
			{
				this.RequestedSize += size;
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00006428 File Offset: 0x00004628
		internal void RequestWrite()
		{
			this.WriteRequested = 1;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00006434 File Offset: 0x00004634
		internal async Task<AsyncProtocolResult> StartOperation(CancellationToken cancellationToken)
		{
			if (Interlocked.CompareExchange(ref this.Started, 1, 0) != 0)
			{
				throw new InvalidOperationException();
			}
			AsyncProtocolResult asyncProtocolResult;
			try
			{
				await this.ProcessOperation(cancellationToken).ConfigureAwait(false);
				asyncProtocolResult = new AsyncProtocolResult(this.UserResult);
			}
			catch (Exception ex)
			{
				asyncProtocolResult = new AsyncProtocolResult(this.Parent.SetException(ex));
			}
			return asyncProtocolResult;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00006480 File Offset: 0x00004680
		private async Task ProcessOperation(CancellationToken cancellationToken)
		{
			AsyncOperationStatus status = AsyncOperationStatus.Initialize;
			while (status != AsyncOperationStatus.Complete)
			{
				cancellationToken.ThrowIfCancellationRequested();
				int? num = await this.InnerRead(cancellationToken).ConfigureAwait(false);
				if (num != null)
				{
					int? num2 = num;
					int num3 = 0;
					if ((num2.GetValueOrDefault() == num3) & (num2 != null))
					{
						status = AsyncOperationStatus.ReadDone;
					}
					else
					{
						num2 = num;
						num3 = 0;
						if ((num2.GetValueOrDefault() < num3) & (num2 != null))
						{
							throw new IOException("Remote prematurely closed connection.");
						}
					}
				}
				if (status <= AsyncOperationStatus.ReadDone)
				{
					AsyncOperationStatus newStatus;
					try
					{
						newStatus = this.Run(status);
						goto IL_011C;
					}
					catch (Exception ex)
					{
						throw MobileAuthenticatedStream.GetSSPIException(ex);
					}
					goto IL_0116;
					IL_011C:
					if (Interlocked.Exchange(ref this.WriteRequested, 0) != 0)
					{
						await this.Parent.InnerWrite(this.RunSynchronously, cancellationToken).ConfigureAwait(false);
					}
					status = newStatus;
					continue;
				}
				IL_0116:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000064CC File Offset: 0x000046CC
		private async Task<int?> InnerRead(CancellationToken cancellationToken)
		{
			int? totalRead = null;
			int num2;
			for (int requestedSize = Interlocked.Exchange(ref this.RequestedSize, 0); requestedSize > 0; requestedSize += num2)
			{
				int num = await this.Parent.InnerRead(this.RunSynchronously, requestedSize, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					return new int?(num);
				}
				if (num > requestedSize)
				{
					throw new InvalidOperationException();
				}
				totalRead += num;
				requestedSize -= num;
				num2 = Interlocked.Exchange(ref this.RequestedSize, 0);
			}
			return totalRead;
		}

		// Token: 0x0600022F RID: 559
		protected abstract AsyncOperationStatus Run(AsyncOperationStatus status);

		// Token: 0x06000230 RID: 560 RVA: 0x00006517 File Offset: 0x00004717
		public override string ToString()
		{
			return string.Format("[{0}]", this.Name);
		}

		// Token: 0x0400020A RID: 522
		private int Started;

		// Token: 0x0400020B RID: 523
		private int RequestedSize;

		// Token: 0x0400020C RID: 524
		private int WriteRequested;

		// Token: 0x0400020D RID: 525
		private readonly object locker = new object();

		// Token: 0x0400020E RID: 526
		private static int next_id;
	}
}
