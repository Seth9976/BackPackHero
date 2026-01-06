using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Profiling;

namespace Pathfinding
{
	// Token: 0x020000B1 RID: 177
	public class PathProcessor
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x0600059F RID: 1439 RVA: 0x0001ACFC File Offset: 0x00018EFC
		// (remove) Token: 0x060005A0 RID: 1440 RVA: 0x0001AD34 File Offset: 0x00018F34
		public event Action<Path> OnPathPreSearch;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060005A1 RID: 1441 RVA: 0x0001AD6C File Offset: 0x00018F6C
		// (remove) Token: 0x060005A2 RID: 1442 RVA: 0x0001ADA4 File Offset: 0x00018FA4
		public event Action<Path> OnPathPostSearch;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060005A3 RID: 1443 RVA: 0x0001ADDC File Offset: 0x00018FDC
		// (remove) Token: 0x060005A4 RID: 1444 RVA: 0x0001AE14 File Offset: 0x00019014
		public event Action OnQueueUnblocked;

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0001AE49 File Offset: 0x00019049
		public int NumThreads
		{
			get
			{
				return this.pathHandlers.Length;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001AE53 File Offset: 0x00019053
		public bool IsUsingMultithreading
		{
			get
			{
				return this.multithreaded;
			}
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001AE5C File Offset: 0x0001905C
		internal PathProcessor(AstarPath astar, PathReturnQueue returnQueue, int processors, bool multithreaded)
		{
			this.astar = astar;
			this.returnQueue = returnQueue;
			this.queue = new BlockableChannel<Path>();
			this.threads = null;
			this.threadCoroutine = null;
			this.pathHandlers = new PathHandler[0];
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001AEB0 File Offset: 0x000190B0
		public void SetThreadCount(int processors, bool multithreaded)
		{
			if (this.threads != null || this.threadCoroutine != null || this.pathHandlers.Length != 0)
			{
				throw new Exception("Call StopThreads before setting the thread count");
			}
			if (processors < 1)
			{
				throw new ArgumentOutOfRangeException("processors");
			}
			if (!multithreaded && processors != 1)
			{
				throw new Exception("Only a single non-multithreaded processor is allowed");
			}
			this.pathHandlers = new PathHandler[processors];
			this.multithreaded = multithreaded;
			for (int i = 0; i < processors; i++)
			{
				this.pathHandlers[i] = new PathHandler(this.astar.nodeStorage, i, processors);
			}
			this.astar.nodeStorage.SetThreadCount(processors);
			this.StartThreads();
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001AF54 File Offset: 0x00019154
		private void StartThreads()
		{
			if (this.threads != null || this.threadCoroutine != null)
			{
				throw new Exception("Call StopThreads before starting threads");
			}
			this.queue.Reopen();
			this.astar.nodeStorage.SetThreadCount(this.pathHandlers.Length);
			if (this.multithreaded)
			{
				this.threads = new Thread[this.pathHandlers.Length];
				for (int i = 0; i < this.pathHandlers.Length; i++)
				{
					PathHandler pathHandler = this.pathHandlers[i];
					BlockableChannel<Path>.Receiver receiver = this.queue.AddReceiver();
					this.threads[i] = new Thread(delegate
					{
						this.CalculatePathsThreaded(pathHandler, receiver);
					});
					this.threads[i].Name = "Pathfinding Thread " + i.ToString();
					this.threads[i].IsBackground = true;
					this.threads[i].Start();
				}
				return;
			}
			this.coroutineReceiver = this.queue.AddReceiver();
			this.threadCoroutine = this.CalculatePaths(this.pathHandlers[0]);
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001B07C File Offset: 0x0001927C
		private int Lock(bool block)
		{
			this.queue.isBlocked = true;
			if (block)
			{
				while (!this.queue.allReceiversBlocked)
				{
					if (this.IsUsingMultithreading)
					{
						Thread.Sleep(1);
					}
					else
					{
						this.TickNonMultithreaded();
					}
				}
			}
			this.nextLockID++;
			this.locks.Add(this.nextLockID);
			return this.nextLockID;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001B0E4 File Offset: 0x000192E4
		private void Unlock(int id)
		{
			if (!this.locks.Remove(id))
			{
				throw new ArgumentException("This lock has already been released");
			}
			if (this.locks.Count == 0)
			{
				if (this.OnQueueUnblocked != null)
				{
					this.OnQueueUnblocked();
				}
				this.queue.isBlocked = false;
			}
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x0001B136 File Offset: 0x00019336
		public PathProcessor.GraphUpdateLock PausePathfinding(bool block)
		{
			return new PathProcessor.GraphUpdateLock(this, block);
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001B140 File Offset: 0x00019340
		public void TickNonMultithreaded()
		{
			if (this.threadCoroutine == null)
			{
				throw new InvalidOperationException("Cannot tick non-multithreaded pathfinding when no coroutine has been started");
			}
			try
			{
				if (!this.threadCoroutine.MoveNext())
				{
					this.threadCoroutine = null;
					this.coroutineReceiver.Close();
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
				Debug.LogError("Unhandled exception during pathfinding. Terminating.");
				this.queue.Close();
				this.threadCoroutine = null;
				this.coroutineReceiver.Close();
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001B1C0 File Offset: 0x000193C0
		public void StopThreads()
		{
			this.queue.Close();
			if (this.threads != null)
			{
				for (int i = 0; i < this.threads.Length; i++)
				{
					if (!this.threads[i].Join(200))
					{
						Debug.LogError("Could not terminate pathfinding thread[" + i.ToString() + "] in 200ms, trying Thread.Abort");
						this.threads[i].Abort();
					}
				}
				this.threads = null;
			}
			if (this.threadCoroutine != null)
			{
				while (this.queue.numReceivers > 0)
				{
					this.TickNonMultithreaded();
				}
			}
			for (int j = 0; j < this.pathHandlers.Length; j++)
			{
				this.pathHandlers[j].Dispose();
			}
			this.pathHandlers = new PathHandler[0];
		}

		// Token: 0x060005AF RID: 1455 RVA: 0x0001B280 File Offset: 0x00019480
		public void Dispose()
		{
			this.StopThreads();
		}

		// Token: 0x060005B0 RID: 1456 RVA: 0x0001B288 File Offset: 0x00019488
		private void CalculatePathsThreaded(PathHandler pathHandler, BlockableChannel<Path>.Receiver receiver)
		{
			try
			{
				long num = 100000L;
				long num2 = DateTime.UtcNow.Ticks + num;
				Path path;
				while (receiver.Receive(out path) != BlockableChannel<Path>.PopState.Closed)
				{
					IPathInternals pathInternals = path;
					pathInternals.PrepareBase(pathHandler);
					pathInternals.AdvanceState(PathState.Processing);
					if (this.OnPathPreSearch != null)
					{
						this.OnPathPreSearch(path);
					}
					long ticks = DateTime.UtcNow.Ticks;
					pathInternals.Prepare();
					if (path.CompleteState == PathCompleteState.NotCalculated)
					{
						this.astar.debugPathData = pathInternals.PathHandler;
						this.astar.debugPathID = path.pathID;
						while (path.CompleteState == PathCompleteState.NotCalculated)
						{
							pathInternals.CalculateStep(num2);
							num2 = DateTime.UtcNow.Ticks + num;
							if (this.queue.isClosed)
							{
								path.FailWithError("AstarPath object destroyed");
							}
						}
						path.duration = (float)(DateTime.UtcNow.Ticks - ticks) * 0.0001f;
					}
					pathInternals.Cleanup();
					pathHandler.heap.Clear(pathHandler.pathNodes);
					if (path.immediateCallback != null)
					{
						path.immediateCallback(path);
					}
					if (this.OnPathPostSearch != null)
					{
						this.OnPathPostSearch(path);
					}
					this.returnQueue.Enqueue(path);
					pathInternals.AdvanceState(PathState.ReturnQueue);
				}
				if (this.astar.logPathResults == PathLog.Heavy)
				{
					Debug.LogWarning("Shutting down pathfinding thread #" + pathHandler.threadID.ToString());
				}
				receiver.Close();
				return;
			}
			catch (Exception ex)
			{
				if (ex is ThreadAbortException)
				{
					if (this.astar.logPathResults == PathLog.Heavy)
					{
						Debug.LogWarning("Shutting down pathfinding thread #" + pathHandler.threadID.ToString());
					}
					receiver.Close();
					return;
				}
				Debug.LogException(ex);
				Debug.LogError("Unhandled exception during pathfinding. Terminating.");
				this.queue.Close();
			}
			finally
			{
				Profiler.EndThreadProfiling();
			}
			Debug.LogError("Error : This part should never be reached.");
			receiver.Close();
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x0001B4AC File Offset: 0x000196AC
		private IEnumerator CalculatePaths(PathHandler pathHandler)
		{
			long maxTicks = (long)(this.astar.maxFrameTime * 10000f);
			long targetTick = DateTime.UtcNow.Ticks + maxTicks;
			for (;;)
			{
				Path p = null;
				bool blockedBefore = false;
				while (p == null)
				{
					switch (this.coroutineReceiver.ReceiveNoBlock(blockedBefore, out p))
					{
					case BlockableChannel<Path>.PopState.Wait:
						blockedBefore = true;
						yield return null;
						break;
					case BlockableChannel<Path>.PopState.Closed:
						goto IL_00BD;
					}
				}
				IPathInternals ip = p;
				maxTicks = (long)(this.astar.maxFrameTime * 10000f);
				ip.PrepareBase(pathHandler);
				ip.AdvanceState(PathState.Processing);
				Action<Path> onPathPreSearch = this.OnPathPreSearch;
				if (onPathPreSearch != null)
				{
					onPathPreSearch(p);
				}
				long num = DateTime.UtcNow.Ticks;
				long totalTicks = 0L;
				ip.Prepare();
				if (p.CompleteState == PathCompleteState.NotCalculated)
				{
					this.astar.debugPathData = ip.PathHandler;
					this.astar.debugPathID = p.pathID;
					while (p.CompleteState == PathCompleteState.NotCalculated)
					{
						ip.CalculateStep(targetTick);
						if (p.CompleteState != PathCompleteState.NotCalculated)
						{
							break;
						}
						totalTicks += DateTime.UtcNow.Ticks - num;
						yield return null;
						num = DateTime.UtcNow.Ticks;
						if (this.queue.isClosed)
						{
							p.FailWithError("AstarPath object destroyed");
						}
						targetTick = DateTime.UtcNow.Ticks + maxTicks;
					}
					totalTicks += DateTime.UtcNow.Ticks - num;
					p.duration = (float)totalTicks * 0.0001f;
				}
				ip.Cleanup();
				pathHandler.heap.Clear(pathHandler.pathNodes);
				OnPathDelegate immediateCallback = p.immediateCallback;
				if (immediateCallback != null)
				{
					immediateCallback(p);
				}
				Action<Path> onPathPostSearch = this.OnPathPostSearch;
				if (onPathPostSearch != null)
				{
					onPathPostSearch(p);
				}
				this.returnQueue.Enqueue(p);
				ip.AdvanceState(PathState.ReturnQueue);
				if (DateTime.UtcNow.Ticks > targetTick)
				{
					yield return null;
					targetTick = DateTime.UtcNow.Ticks + maxTicks;
				}
				p = null;
				ip = null;
			}
			IL_00BD:
			yield break;
			yield break;
		}

		// Token: 0x040003B9 RID: 953
		internal BlockableChannel<Path> queue;

		// Token: 0x040003BA RID: 954
		private readonly AstarPath astar;

		// Token: 0x040003BB RID: 955
		private readonly PathReturnQueue returnQueue;

		// Token: 0x040003BC RID: 956
		private PathHandler[] pathHandlers;

		// Token: 0x040003BD RID: 957
		private Thread[] threads;

		// Token: 0x040003BE RID: 958
		private bool multithreaded;

		// Token: 0x040003BF RID: 959
		private IEnumerator threadCoroutine;

		// Token: 0x040003C0 RID: 960
		private BlockableChannel<Path>.Receiver coroutineReceiver;

		// Token: 0x040003C1 RID: 961
		private readonly List<int> locks = new List<int>();

		// Token: 0x040003C2 RID: 962
		private int nextLockID;

		// Token: 0x040003C3 RID: 963
		private static readonly ProfilerMarker MarkerCalculatePath = new ProfilerMarker("Calculating Path");

		// Token: 0x040003C4 RID: 964
		private static readonly ProfilerMarker MarkerPreparePath = new ProfilerMarker("Prepare Path");

		// Token: 0x020000B2 RID: 178
		public struct GraphUpdateLock
		{
			// Token: 0x060005B3 RID: 1459 RVA: 0x0001B4E2 File Offset: 0x000196E2
			public GraphUpdateLock(PathProcessor pathProcessor, bool block)
			{
				this.pathProcessor = pathProcessor;
				this.id = pathProcessor.Lock(block);
			}

			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0001B4F8 File Offset: 0x000196F8
			public bool Held
			{
				get
				{
					return this.pathProcessor != null && this.pathProcessor.locks.Contains(this.id);
				}
			}

			// Token: 0x060005B5 RID: 1461 RVA: 0x0001B51A File Offset: 0x0001971A
			public void Release()
			{
				this.pathProcessor.Unlock(this.id);
			}

			// Token: 0x040003C5 RID: 965
			private PathProcessor pathProcessor;

			// Token: 0x040003C6 RID: 966
			private int id;
		}
	}
}
