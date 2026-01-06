using System;
using System.Threading;
using Pathfinding.Util;

namespace Pathfinding
{
	// Token: 0x02000093 RID: 147
	internal class BlockableChannel<T> where T : class
	{
		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000175F7 File Offset: 0x000157F7
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x000175FF File Offset: 0x000157FF
		public int numReceivers { get; private set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x00017608 File Offset: 0x00015808
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x00017610 File Offset: 0x00015810
		public bool isClosed { get; private set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0001761C File Offset: 0x0001581C
		public bool isEmpty
		{
			get
			{
				object obj = this.lockObj;
				bool flag2;
				lock (obj)
				{
					flag2 = this.queue.Length == 0;
				}
				return flag2;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00017668 File Offset: 0x00015868
		public bool allReceiversBlocked
		{
			get
			{
				return this.blocked && this.waitingReceivers == this.numReceivers;
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00017684 File Offset: 0x00015884
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0001768C File Offset: 0x0001588C
		public bool isBlocked
		{
			get
			{
				return this.blocked;
			}
			set
			{
				object obj = this.lockObj;
				lock (obj)
				{
					this.blocked = value;
					if (!this.isClosed)
					{
						this.isStarving = value || this.queue.Length == 0;
					}
				}
			}
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x000176F4 File Offset: 0x000158F4
		public void Close()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.isClosed = true;
				this.isStarving = false;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x0001773C File Offset: 0x0001593C
		// (set) Token: 0x060004C7 RID: 1223 RVA: 0x0001774D File Offset: 0x0001594D
		private bool isStarving
		{
			get
			{
				return !this.starving.WaitOne(0);
			}
			set
			{
				if (value)
				{
					this.starving.Reset();
					return;
				}
				this.starving.Set();
			}
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001776C File Offset: 0x0001596C
		public void Reopen()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.numReceivers != 0)
				{
					throw new InvalidOperationException("Can only reopen a channel after Close has been called on all receivers");
				}
				this.isClosed = false;
				this.isBlocked = false;
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000177C8 File Offset: 0x000159C8
		public BlockableChannel<T>.Receiver AddReceiver()
		{
			object obj = this.lockObj;
			BlockableChannel<T>.Receiver receiver;
			lock (obj)
			{
				if (this.isClosed)
				{
					throw new InvalidOperationException("Channel is closed");
				}
				int numReceivers = this.numReceivers;
				this.numReceivers = numReceivers + 1;
				receiver = new BlockableChannel<T>.Receiver(this);
			}
			return receiver;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x00017830 File Offset: 0x00015A30
		public void PushFront(T path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.isClosed)
				{
					throw new InvalidOperationException("Channel is closed");
				}
				this.queue.PushStart(path);
				if (!this.blocked)
				{
					this.isStarving = false;
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00017898 File Offset: 0x00015A98
		public void Push(T path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (this.isClosed)
				{
					throw new InvalidOperationException("Channel is closed");
				}
				this.queue.PushEnd(path);
				if (!this.blocked)
				{
					this.isStarving = false;
				}
			}
		}

		// Token: 0x0400031C RID: 796
		private readonly object lockObj = new object();

		// Token: 0x0400031D RID: 797
		private CircularBuffer<T> queue = new CircularBuffer<T>(16);

		// Token: 0x0400031F RID: 799
		private volatile int waitingReceivers;

		// Token: 0x04000320 RID: 800
		private ManualResetEvent starving = new ManualResetEvent(false);

		// Token: 0x04000321 RID: 801
		private bool blocked;

		// Token: 0x02000094 RID: 148
		public enum PopState
		{
			// Token: 0x04000324 RID: 804
			Ok,
			// Token: 0x04000325 RID: 805
			Wait,
			// Token: 0x04000326 RID: 806
			Closed
		}

		// Token: 0x02000095 RID: 149
		public struct Receiver
		{
			// Token: 0x060004CD RID: 1229 RVA: 0x0001792C File Offset: 0x00015B2C
			public Receiver(BlockableChannel<T> channel)
			{
				this.channel = channel;
			}

			// Token: 0x060004CE RID: 1230 RVA: 0x00017938 File Offset: 0x00015B38
			public void Close()
			{
				object lockObj = this.channel.lockObj;
				lock (lockObj)
				{
					BlockableChannel<T> blockableChannel = this.channel;
					int numReceivers = blockableChannel.numReceivers;
					blockableChannel.numReceivers = numReceivers - 1;
				}
				this.channel = null;
			}

			// Token: 0x060004CF RID: 1231 RVA: 0x00017994 File Offset: 0x00015B94
			public BlockableChannel<T>.PopState Receive(out T item)
			{
				Interlocked.Increment(ref this.channel.waitingReceivers);
				BlockableChannel<T>.PopState popState;
				for (;;)
				{
					this.channel.starving.WaitOne();
					object lockObj = this.channel.lockObj;
					lock (lockObj)
					{
						if (this.channel.isClosed)
						{
							Interlocked.Decrement(ref this.channel.waitingReceivers);
							item = default(T);
							popState = BlockableChannel<T>.PopState.Closed;
						}
						else
						{
							if (this.channel.queue.Length == 0)
							{
								this.channel.isStarving = true;
							}
							if (this.channel.isStarving)
							{
								continue;
							}
							Interlocked.Decrement(ref this.channel.waitingReceivers);
							item = this.channel.queue.PopStart();
							popState = BlockableChannel<T>.PopState.Ok;
						}
					}
					break;
				}
				return popState;
			}

			// Token: 0x060004D0 RID: 1232 RVA: 0x00017A78 File Offset: 0x00015C78
			public BlockableChannel<T>.PopState ReceiveNoBlock(bool blockedBefore, out T item)
			{
				item = default(T);
				if (!blockedBefore)
				{
					Interlocked.Increment(ref this.channel.waitingReceivers);
				}
				while (!this.channel.isStarving)
				{
					object lockObj = this.channel.lockObj;
					BlockableChannel<T>.PopState popState;
					lock (lockObj)
					{
						if (this.channel.isClosed)
						{
							Interlocked.Decrement(ref this.channel.waitingReceivers);
							popState = BlockableChannel<T>.PopState.Closed;
						}
						else
						{
							if (this.channel.queue.Length == 0)
							{
								this.channel.isStarving = true;
							}
							if (this.channel.isStarving)
							{
								continue;
							}
							Interlocked.Decrement(ref this.channel.waitingReceivers);
							item = this.channel.queue.PopStart();
							popState = BlockableChannel<T>.PopState.Ok;
						}
					}
					return popState;
				}
				return BlockableChannel<T>.PopState.Wait;
			}

			// Token: 0x04000327 RID: 807
			private BlockableChannel<T> channel;
		}
	}
}
