using System;
using System.Threading;

namespace Pathfinding
{
	// Token: 0x02000047 RID: 71
	internal class ThreadControlQueue
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00012BF8 File Offset: 0x00010DF8
		public ThreadControlQueue(int numReceivers)
		{
			this.numReceivers = numReceivers;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00012C1E File Offset: 0x00010E1E
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00012C29 File Offset: 0x00010E29
		public bool IsTerminating
		{
			get
			{
				return this.terminate;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00012C34 File Offset: 0x00010E34
		public void Block()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = true;
				this.block.Reset();
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00012C84 File Offset: 0x00010E84
		public void Unblock()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.blocked = false;
				this.block.Set();
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00012CD4 File Offset: 0x00010ED4
		public void Lock()
		{
			Monitor.Enter(this.lockObj);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00012CE1 File Offset: 0x00010EE1
		public void Unlock()
		{
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00012CF0 File Offset: 0x00010EF0
		public bool AllReceiversBlocked
		{
			get
			{
				object obj = this.lockObj;
				bool flag2;
				lock (obj)
				{
					flag2 = this.blocked && this.blockedReceivers == this.numReceivers;
				}
				return flag2;
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00012D48 File Offset: 0x00010F48
		public void PushFront(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						path.next = this.head;
						this.head = path;
					}
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00012DE4 File Offset: 0x00010FE4
		public void Push(Path path)
		{
			object obj = this.lockObj;
			lock (obj)
			{
				if (!this.terminate)
				{
					if (this.tail == null)
					{
						this.head = path;
						this.tail = path;
						if (this.starving && !this.blocked)
						{
							this.starving = false;
							this.block.Set();
						}
						else
						{
							this.starving = false;
						}
					}
					else
					{
						this.tail.next = path;
						this.tail = path;
					}
				}
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00012E80 File Offset: 0x00011080
		private void Starving()
		{
			this.starving = true;
			this.block.Reset();
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00012E98 File Offset: 0x00011098
		public void TerminateReceivers()
		{
			object obj = this.lockObj;
			lock (obj)
			{
				this.terminate = true;
				this.block.Set();
			}
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00012EE8 File Offset: 0x000110E8
		public Path Pop()
		{
			Monitor.Enter(this.lockObj);
			Path path2;
			try
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				while (this.blocked || this.starving)
				{
					this.blockedReceivers++;
					if (this.blockedReceivers > this.numReceivers)
					{
						throw new InvalidOperationException(string.Concat(new string[]
						{
							"More receivers are blocked than specified in constructor (",
							this.blockedReceivers.ToString(),
							" > ",
							this.numReceivers.ToString(),
							")"
						}));
					}
					Monitor.Exit(this.lockObj);
					this.block.WaitOne();
					Monitor.Enter(this.lockObj);
					if (this.terminate)
					{
						throw new ThreadControlQueue.QueueTerminationException();
					}
					this.blockedReceivers--;
					if (this.head == null)
					{
						this.Starving();
					}
				}
				Path path = this.head;
				Path next = this.head.next;
				if (next == null)
				{
					this.tail = null;
				}
				this.head.next = null;
				this.head = next;
				path2 = path;
			}
			finally
			{
				if (Monitor.IsEntered(this.lockObj))
				{
					Monitor.Exit(this.lockObj);
				}
			}
			return path2;
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00013060 File Offset: 0x00011260
		public void ReceiverTerminated()
		{
			Monitor.Enter(this.lockObj);
			this.blockedReceivers++;
			Monitor.Exit(this.lockObj);
		}

		// Token: 0x06000363 RID: 867 RVA: 0x00013088 File Offset: 0x00011288
		public Path PopNoBlock(bool blockedBefore)
		{
			Path path;
			lock (this.lockObj)
			{
				if (this.terminate)
				{
					this.blockedReceivers++;
					throw new ThreadControlQueue.QueueTerminationException();
				}
				if (this.head == null)
				{
					this.Starving();
				}
				if (this.blocked || this.starving)
				{
					if (!blockedBefore)
					{
						this.blockedReceivers++;
						if (this.terminate)
						{
							throw new ThreadControlQueue.QueueTerminationException();
						}
						if (this.blockedReceivers != this.numReceivers && this.blockedReceivers > this.numReceivers)
						{
							throw new InvalidOperationException(string.Concat(new string[]
							{
								"More receivers are blocked than specified in constructor (",
								this.blockedReceivers.ToString(),
								" > ",
								this.numReceivers.ToString(),
								")"
							}));
						}
					}
					path = null;
				}
				else
				{
					if (blockedBefore)
					{
						this.blockedReceivers--;
					}
					Path path2 = this.head;
					Path next = this.head.next;
					if (next == null)
					{
						this.tail = null;
					}
					this.head.next = null;
					this.head = next;
					path = path2;
				}
			}
			return path;
		}

		// Token: 0x0400021B RID: 539
		private Path head;

		// Token: 0x0400021C RID: 540
		private Path tail;

		// Token: 0x0400021D RID: 541
		private readonly object lockObj = new object();

		// Token: 0x0400021E RID: 542
		private readonly int numReceivers;

		// Token: 0x0400021F RID: 543
		private bool blocked;

		// Token: 0x04000220 RID: 544
		private int blockedReceivers;

		// Token: 0x04000221 RID: 545
		private bool starving;

		// Token: 0x04000222 RID: 546
		private bool terminate;

		// Token: 0x04000223 RID: 547
		private ManualResetEvent block = new ManualResetEvent(true);

		// Token: 0x02000108 RID: 264
		public class QueueTerminationException : Exception
		{
		}
	}
}
