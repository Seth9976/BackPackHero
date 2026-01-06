using System;
using System.Collections.Generic;
using System.Threading;

namespace Pathfinding.Util
{
	// Token: 0x020000CB RID: 203
	public class ParallelWorkQueue<T>
	{
		// Token: 0x060008A1 RID: 2209 RVA: 0x0003A74F File Offset: 0x0003894F
		public ParallelWorkQueue(Queue<T> queue)
		{
			this.queue = queue;
			this.initialCount = queue.Count;
			this.threadCount = Math.Min(this.initialCount, Math.Max(1, AstarPath.CalculateThreadCount(ThreadCount.AutomaticHighLoad)));
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0003A788 File Offset: 0x00038988
		public IEnumerable<int> Run(int progressTimeoutMillis)
		{
			if (this.initialCount != this.queue.Count)
			{
				throw new InvalidOperationException("Queue has been modified since the constructor");
			}
			if (this.initialCount == 0)
			{
				yield break;
			}
			this.waitEvents = new ManualResetEvent[this.threadCount];
			for (int i = 0; i < this.waitEvents.Length; i++)
			{
				this.waitEvents[i] = new ManualResetEvent(false);
				ThreadPool.QueueUserWorkItem(delegate(object threadIndex)
				{
					this.RunTask((int)threadIndex);
				}, i);
			}
			for (;;)
			{
				WaitHandle[] array = this.waitEvents;
				if (WaitHandle.WaitAll(array, progressTimeoutMillis))
				{
					break;
				}
				Queue<T> queue = this.queue;
				int count;
				lock (queue)
				{
					count = this.queue.Count;
				}
				yield return this.initialCount - count;
			}
			if (this.innerException != null)
			{
				throw this.innerException;
			}
			yield break;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0003A7A0 File Offset: 0x000389A0
		private void RunTask(int threadIndex)
		{
			try
			{
				for (;;)
				{
					Queue<T> queue = this.queue;
					T t;
					lock (queue)
					{
						if (this.queue.Count == 0)
						{
							break;
						}
						t = this.queue.Dequeue();
					}
					this.action(t, threadIndex);
				}
			}
			catch (Exception ex)
			{
				this.innerException = ex;
				Queue<T> queue = this.queue;
				lock (queue)
				{
					this.queue.Clear();
				}
			}
			finally
			{
				this.waitEvents[threadIndex].Set();
			}
		}

		// Token: 0x040004FB RID: 1275
		public Action<T, int> action;

		// Token: 0x040004FC RID: 1276
		public readonly int threadCount;

		// Token: 0x040004FD RID: 1277
		private readonly Queue<T> queue;

		// Token: 0x040004FE RID: 1278
		private readonly int initialCount;

		// Token: 0x040004FF RID: 1279
		private ManualResetEvent[] waitEvents;

		// Token: 0x04000500 RID: 1280
		private Exception innerException;
	}
}
