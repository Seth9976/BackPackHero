using System;
using Pathfinding.Jobs;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200008B RID: 139
	internal class WorkItemProcessor : IWorkItemContext, IGraphUpdateContext
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x0600044C RID: 1100 RVA: 0x000168F0 File Offset: 0x00014AF0
		// (remove) Token: 0x0600044D RID: 1101 RVA: 0x00016928 File Offset: 0x00014B28
		public event Action OnGraphsUpdated;

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0001695D File Offset: 0x00014B5D
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x00016965 File Offset: 0x00014B65
		public bool workItemsInProgressRightNow { get; private set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0001696E File Offset: 0x00014B6E
		public bool anyQueued
		{
			get
			{
				return this.workItems.Count > 0;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0001697E File Offset: 0x00014B7E
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x00016986 File Offset: 0x00014B86
		public bool workItemsInProgress { get; private set; }

		// Token: 0x06000453 RID: 1107 RVA: 0x000033F6 File Offset: 0x000015F6
		void IWorkItemContext.QueueFloodFill()
		{
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0001698F File Offset: 0x00014B8F
		void IWorkItemContext.PreUpdate()
		{
			if (!this.preUpdateEventSent && !this.astar.isScanning)
			{
				this.preUpdateEventSent = true;
				GraphModifier.TriggerEvent(GraphModifier.EventType.PreUpdate);
			}
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x000169B3 File Offset: 0x00014BB3
		void IWorkItemContext.SetGraphDirty(NavGraph graph)
		{
			this.astar.DirtyBounds(graph.bounds);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x000169C6 File Offset: 0x00014BC6
		void IGraphUpdateContext.DirtyBounds(Bounds bounds)
		{
			this.astar.DirtyBounds(bounds);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000169D4 File Offset: 0x00014BD4
		internal void DirtyGraphs()
		{
			this.anyGraphsDirty = true;
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000169DD File Offset: 0x00014BDD
		public void EnsureValidFloodFill()
		{
			this.astar.hierarchicalGraph.RecalculateIfNecessary();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000169EF File Offset: 0x00014BEF
		public WorkItemProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x00016A10 File Offset: 0x00014C10
		public void AddWorkItem(AstarWorkItem item)
		{
			this.workItems.Enqueue(item);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x00016A20 File Offset: 0x00014C20
		private bool ProcessWorkItems(bool force, bool sendEvents)
		{
			if (this.workItemsInProgressRightNow)
			{
				throw new Exception("Processing work items recursively. Please do not wait for other work items to be completed inside work items. If you think this is not caused by any of your scripts, this might be a bug.");
			}
			RWLock.LockSync lockSync = this.astar.LockGraphDataForWritingSync();
			this.astar.data.LockGraphStructure(true);
			Physics.SyncTransforms();
			Physics2D.SyncTransforms();
			this.workItemsInProgressRightNow = true;
			try
			{
				bool flag = false;
				bool flag2 = false;
				while (this.workItems.Count > 0)
				{
					if (!this.workItemsInProgress)
					{
						this.workItemsInProgress = true;
					}
					AstarWorkItem astarWorkItem = this.workItems[0];
					bool flag3;
					try
					{
						if (astarWorkItem.init != null)
						{
							astarWorkItem.init();
							astarWorkItem.init = null;
						}
						if (astarWorkItem.initWithContext != null)
						{
							astarWorkItem.initWithContext(this);
							astarWorkItem.initWithContext = null;
						}
						this.workItems[0] = astarWorkItem;
						if (astarWorkItem.update != null)
						{
							flag3 = astarWorkItem.update(force);
						}
						else
						{
							flag3 = astarWorkItem.updateWithContext == null || astarWorkItem.updateWithContext(this, force);
						}
					}
					catch
					{
						this.workItems.Dequeue();
						throw;
					}
					if (!flag3)
					{
						if (force)
						{
							Debug.LogError("Misbehaving WorkItem. 'force'=true but the work item did not complete.\nIf force=true is passed to a WorkItem it should always return true.");
						}
						flag = true;
						break;
					}
					this.workItems.Dequeue();
					flag2 = true;
				}
				if (sendEvents && flag2)
				{
					if (this.anyGraphsDirty)
					{
						GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdateBeforeAreaRecalculation);
					}
					this.astar.offMeshLinks.Refresh();
					this.EnsureValidFloodFill();
					if (this.anyGraphsDirty)
					{
						GraphModifier.TriggerEvent(GraphModifier.EventType.PostUpdate);
						if (this.OnGraphsUpdated != null)
						{
							this.OnGraphsUpdated();
						}
					}
				}
				if (flag)
				{
					return false;
				}
			}
			finally
			{
				lockSync.Unlock();
				this.astar.data.UnlockGraphStructure();
				this.workItemsInProgressRightNow = false;
			}
			this.anyGraphsDirty = false;
			this.preUpdateEventSent = false;
			this.workItemsInProgress = false;
			return true;
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00016C14 File Offset: 0x00014E14
		public bool ProcessWorkItemsForScan(bool force)
		{
			return this.ProcessWorkItems(force, false);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x00016C1E File Offset: 0x00014E1E
		public bool ProcessWorkItemsForUpdate(bool force)
		{
			return this.ProcessWorkItems(force, true);
		}

		// Token: 0x040002F2 RID: 754
		private readonly AstarPath astar;

		// Token: 0x040002F3 RID: 755
		private readonly WorkItemProcessor.IndexedQueue<AstarWorkItem> workItems = new WorkItemProcessor.IndexedQueue<AstarWorkItem>();

		// Token: 0x040002F4 RID: 756
		private bool anyGraphsDirty = true;

		// Token: 0x040002F5 RID: 757
		private bool preUpdateEventSent;

		// Token: 0x0200008C RID: 140
		private class IndexedQueue<T>
		{
			// Token: 0x170000C5 RID: 197
			public T this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					return this.buffer[(this.start + index) % this.buffer.Length];
				}
				set
				{
					if (index < 0 || index >= this.Count)
					{
						throw new IndexOutOfRangeException();
					}
					this.buffer[(this.start + index) % this.buffer.Length] = value;
				}
			}

			// Token: 0x170000C6 RID: 198
			// (get) Token: 0x06000460 RID: 1120 RVA: 0x00016C8B File Offset: 0x00014E8B
			// (set) Token: 0x06000461 RID: 1121 RVA: 0x00016C93 File Offset: 0x00014E93
			public int Count { get; private set; }

			// Token: 0x06000462 RID: 1122 RVA: 0x00016C9C File Offset: 0x00014E9C
			public void Enqueue(T item)
			{
				if (this.Count == this.buffer.Length)
				{
					T[] array = new T[this.buffer.Length * 2];
					for (int i = 0; i < this.Count; i++)
					{
						array[i] = this[i];
					}
					this.buffer = array;
					this.start = 0;
				}
				this.buffer[(this.start + this.Count) % this.buffer.Length] = item;
				int count = this.Count;
				this.Count = count + 1;
			}

			// Token: 0x06000463 RID: 1123 RVA: 0x00016D28 File Offset: 0x00014F28
			public T Dequeue()
			{
				if (this.Count == 0)
				{
					throw new InvalidOperationException();
				}
				T t = this.buffer[this.start];
				this.start = (this.start + 1) % this.buffer.Length;
				int count = this.Count;
				this.Count = count - 1;
				return t;
			}

			// Token: 0x040002F7 RID: 759
			private T[] buffer = new T[4];

			// Token: 0x040002F8 RID: 760
			private int start;
		}
	}
}
