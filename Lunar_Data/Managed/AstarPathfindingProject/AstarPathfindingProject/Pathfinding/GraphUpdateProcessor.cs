using System;
using System.Collections.Generic;
using System.Threading;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200009C RID: 156
	internal class GraphUpdateProcessor
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x00018168 File Offset: 0x00016368
		public bool IsAnyGraphUpdateQueued
		{
			get
			{
				return this.graphUpdateQueue.Count > 0;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00018178 File Offset: 0x00016378
		public bool IsAnyGraphUpdateInProgress
		{
			get
			{
				return this.anyGraphUpdateInProgress;
			}
		}

		// Token: 0x060004EB RID: 1259 RVA: 0x00018180 File Offset: 0x00016380
		public GraphUpdateProcessor(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000181B0 File Offset: 0x000163B0
		public AstarWorkItem GetWorkItem()
		{
			return new AstarWorkItem(new Action<IWorkItemContext>(this.QueueGraphUpdatesInternal), new Func<IWorkItemContext, bool, bool>(this.ProcessGraphUpdates));
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000181CF File Offset: 0x000163CF
		public void AddToQueue(GraphUpdateObject ob)
		{
			this.graphUpdateQueue.Enqueue(ob);
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x000181DD File Offset: 0x000163DD
		public void DiscardQueued()
		{
			while (this.graphUpdateQueue.Count > 0)
			{
				this.graphUpdateQueue.Dequeue().internalStage = -3;
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00018204 File Offset: 0x00016404
		private void QueueGraphUpdatesInternal(IWorkItemContext context)
		{
			while (this.graphUpdateQueue.Count > 0)
			{
				GraphUpdateObject graphUpdateObject = this.graphUpdateQueue.Dequeue();
				this.pendingGraphUpdates.Add(graphUpdateObject);
				if (graphUpdateObject.internalStage != -2)
				{
					Debug.LogError("Expected remaining graph update to be pending");
				}
			}
			foreach (object obj in this.astar.data.GetUpdateableGraphs())
			{
				IUpdatableGraph updatableGraph = (IUpdatableGraph)obj;
				NavGraph navGraph = updatableGraph as NavGraph;
				List<GraphUpdateObject> list = ListPool<GraphUpdateObject>.Claim();
				for (int i = 0; i < this.pendingGraphUpdates.Count; i++)
				{
					GraphUpdateObject graphUpdateObject2 = this.pendingGraphUpdates[i];
					if (graphUpdateObject2.nnConstraint == null || graphUpdateObject2.nnConstraint.SuitableGraph((int)navGraph.graphIndex, navGraph))
					{
						list.Add(graphUpdateObject2);
					}
				}
				if (list.Count > 0)
				{
					IGraphUpdatePromise graphUpdatePromise = updatableGraph.ScheduleGraphUpdates(list);
					if (graphUpdatePromise != null)
					{
						IEnumerator<JobHandle> enumerator2 = graphUpdatePromise.Prepare();
						this.pendingPromises.Add(new ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>(graphUpdatePromise, enumerator2));
					}
					else
					{
						ListPool<GraphUpdateObject>.Release(ref list);
					}
				}
				else
				{
					ListPool<GraphUpdateObject>.Release(ref list);
				}
			}
			context.PreUpdate();
			this.anyGraphUpdateInProgress = true;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00018358 File Offset: 0x00016558
		private bool ProcessGraphUpdates(IWorkItemContext context, bool force)
		{
			if (this.pendingPromises.Count > 0)
			{
				if (!GraphUpdateProcessor.ProcessGraphUpdatePromises(this.pendingPromises, context, force))
				{
					return false;
				}
				this.pendingPromises.Clear();
			}
			this.anyGraphUpdateInProgress = false;
			for (int i = 0; i < this.pendingGraphUpdates.Count; i++)
			{
				this.pendingGraphUpdates[i].internalStage = 0;
			}
			this.pendingGraphUpdates.Clear();
			return true;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x000183CC File Offset: 0x000165CC
		public static bool ProcessGraphUpdatePromises(List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>> promises, IGraphUpdateContext context, bool force = false)
		{
			TimeSlice timeSlice = TimeSlice.MillisFromNow(2f);
			TimeSlice timeSlice2 = default(TimeSlice);
			for (;;)
			{
				int num = -1;
				bool flag = false;
				for (int i = 0; i < promises.Count; i++)
				{
					ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>> valueTuple = promises[i];
					IGraphUpdatePromise item = valueTuple.Item1;
					IEnumerator<JobHandle> item2 = valueTuple.Item2;
					if (item2 != null)
					{
						if (!force)
						{
							JobHandle jobHandle = item2.Current;
							if (jobHandle.IsCompleted)
							{
								jobHandle = item2.Current;
								jobHandle.Complete();
							}
							else
							{
								if (num == -1)
								{
									num = i;
									goto IL_00C7;
								}
								goto IL_00C7;
							}
						}
						else
						{
							JobHandle jobHandle = item2.Current;
							jobHandle.Complete();
						}
						flag = true;
						try
						{
							if (item2.MoveNext())
							{
								if (num == -1)
								{
									num = i;
								}
							}
							else
							{
								promises[i] = new ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>(item, null);
							}
						}
						catch (Exception ex)
						{
							Debug.LogError(new Exception("Error while updating graphs.", ex));
							promises[i] = new ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>(null, null);
						}
					}
					IL_00C7:;
				}
				if (num == -1)
				{
					goto IL_011E;
				}
				if (!force)
				{
					if (timeSlice.expired)
					{
						break;
					}
					if (flag)
					{
						timeSlice2 = TimeSlice.MillisFromNow(0.1f);
					}
					else
					{
						if (timeSlice2.expired)
						{
							return false;
						}
						if (!flag)
						{
							Thread.Yield();
						}
					}
				}
			}
			return false;
			IL_011E:
			for (int j = 0; j < promises.Count; j++)
			{
				IGraphUpdatePromise item3 = promises[j].Item1;
				if (item3 != null)
				{
					try
					{
						item3.Apply(context);
					}
					catch (Exception ex2)
					{
						Debug.LogError(new Exception("Error while updating graphs.", ex2));
					}
				}
			}
			return true;
		}

		// Token: 0x04000339 RID: 825
		private readonly AstarPath astar;

		// Token: 0x0400033A RID: 826
		private bool anyGraphUpdateInProgress;

		// Token: 0x0400033B RID: 827
		private readonly Queue<GraphUpdateObject> graphUpdateQueue = new Queue<GraphUpdateObject>();

		// Token: 0x0400033C RID: 828
		private readonly List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>> pendingPromises = new List<ValueTuple<IGraphUpdatePromise, IEnumerator<JobHandle>>>();

		// Token: 0x0400033D RID: 829
		private readonly List<GraphUpdateObject> pendingGraphUpdates = new List<GraphUpdateObject>();

		// Token: 0x0400033E RID: 830
		private static readonly ProfilerMarker MarkerSleep = new ProfilerMarker(ProfilerCategory.Loading, "Sleep");

		// Token: 0x0400033F RID: 831
		private static readonly ProfilerMarker MarkerCalculate = new ProfilerMarker("Calculating Graph Update");

		// Token: 0x04000340 RID: 832
		private static readonly ProfilerMarker MarkerApply = new ProfilerMarker("Applying Graph Update");
	}
}
