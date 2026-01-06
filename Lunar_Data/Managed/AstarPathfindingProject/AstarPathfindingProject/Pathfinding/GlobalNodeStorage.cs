using System;
using Pathfinding.Jobs;
using Pathfinding.Util;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Pathfinding
{
	// Token: 0x02000096 RID: 150
	internal class GlobalNodeStorage
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00017B5C File Offset: 0x00015D5C
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x00017B64 File Offset: 0x00015D64
		public uint destroyedNodesVersion { get; private set; }

		// Token: 0x060004D3 RID: 1235 RVA: 0x00017B70 File Offset: 0x00015D70
		public GlobalNodeStorage(AstarPath astar)
		{
			this.astar = astar;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00017BCD File Offset: 0x00015DCD
		public GraphNode GetNode(uint nodeIndex)
		{
			return this.nodes[(int)nodeIndex];
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00017BD8 File Offset: 0x00015DD8
		private void DisposeThreadData()
		{
			if (this.pathfindingThreadData.Length != 0)
			{
				for (int i = 0; i < this.pathfindingThreadData.Length; i++)
				{
					this.pathfindingThreadData[i].pathNodes.Free(Allocator.Persistent);
				}
				this.pathfindingThreadData = new GlobalNodeStorage.PathfindingThreadData[0];
			}
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00017C24 File Offset: 0x00015E24
		public void SetThreadCount(int threadCount)
		{
			if (this.pathfindingThreadData.Length != threadCount)
			{
				this.DisposeThreadData();
				this.pathfindingThreadData = new GlobalNodeStorage.PathfindingThreadData[threadCount];
				for (int i = 0; i < this.pathfindingThreadData.Length; i++)
				{
					this.pathfindingThreadData[i].pathNodes = new UnsafeSpan<PathNode>(Allocator.Persistent, (int)(this.reservedPathNodeData + 256U));
					this.pathfindingThreadData[i].pathNodes.Fill(PathNode.Default);
				}
			}
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00017CA0 File Offset: 0x00015EA0
		public void InitializeNode(GraphNode node)
		{
			int pathNodeVariants = node.PathNodeVariants;
			lock (this)
			{
				if (this.nodeIndexPools[pathNodeVariants - 1].Count > 0)
				{
					node.NodeIndex = this.nodeIndexPools[pathNodeVariants - 1].Pop();
				}
				else
				{
					node.NodeIndex = this.nextNodeIndex;
					this.nextNodeIndex += (uint)pathNodeVariants;
					this.ReserveNodeIndices(this.nextNodeIndex);
				}
				for (int i = 0; i < pathNodeVariants; i++)
				{
					checked
					{
						this.nodes[(int)((IntPtr)(unchecked((ulong)node.NodeIndex + (ulong)((long)i))))] = node;
					}
				}
				this.astar.hierarchicalGraph.OnCreatedNode(node);
			}
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00017D5C File Offset: 0x00015F5C
		private void ReserveNodeIndices(uint nextNodeIndex)
		{
			if (nextNodeIndex <= this.reservedPathNodeData)
			{
				return;
			}
			this.reservedPathNodeData = math.ceilpow2(nextNodeIndex);
			this.astar.hierarchicalGraph.ReserveNodeIndices(this.reservedPathNodeData);
			int num = this.pathfindingThreadData.Length;
			this.DisposeThreadData();
			this.SetThreadCount(num);
			Memory.Realloc<GraphNode>(ref this.nodes, (int)this.reservedPathNodeData);
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00017DBC File Offset: 0x00015FBC
		public void DestroyNode(GraphNode node)
		{
			uint nodeIndex = node.NodeIndex;
			if (nodeIndex == 268435454U)
			{
				return;
			}
			uint destroyedNodesVersion = this.destroyedNodesVersion;
			this.destroyedNodesVersion = destroyedNodesVersion + 1U;
			int pathNodeVariants = node.PathNodeVariants;
			this.nodeIndexPools[pathNodeVariants - 1].Push(nodeIndex);
			for (int i = 0; i < pathNodeVariants; i++)
			{
				checked
				{
					this.nodes[(int)((IntPtr)(unchecked((ulong)nodeIndex + (ulong)((long)i))))] = null;
				}
			}
			for (int j = 0; j < this.pathfindingThreadData.Length; j++)
			{
				GlobalNodeStorage.PathfindingThreadData pathfindingThreadData = this.pathfindingThreadData[j];
				uint num = 0U;
				while ((ulong)num < (ulong)((long)pathNodeVariants))
				{
					pathfindingThreadData.pathNodes[nodeIndex + num].pathID = 0;
					num += 1U;
				}
			}
			this.astar.hierarchicalGraph.OnDestroyedNode(node);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00017E7C File Offset: 0x0001607C
		public void OnDisable()
		{
			this.lastAllocationJob.Complete();
			this.nextNodeIndex = 1U;
			this.reservedPathNodeData = 0U;
			for (int i = 0; i < this.nodeIndexPools.Length; i++)
			{
				this.nodeIndexPools[i].Clear();
			}
			this.nodes = new GraphNode[0];
			this.DisposeThreadData();
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00017ED4 File Offset: 0x000160D4
		public JobHandle AllocateNodesJob<T>(T[] result, int count, Func<T> createNode, uint variantsPerNode) where T : GraphNode
		{
			this.lastAllocationJob = new GlobalNodeStorage.JobAllocateNodes<T>
			{
				result = result,
				count = count,
				nodeStorage = this,
				variantsPerNode = variantsPerNode,
				createNode = createNode
			}.ScheduleManaged(this.lastAllocationJob);
			return this.lastAllocationJob;
		}

		// Token: 0x04000328 RID: 808
		private readonly AstarPath astar;

		// Token: 0x04000329 RID: 809
		private JobHandle lastAllocationJob;

		// Token: 0x0400032A RID: 810
		public uint nextNodeIndex = 1U;

		// Token: 0x0400032B RID: 811
		private uint reservedPathNodeData;

		// Token: 0x0400032D RID: 813
		public const int MaxTemporaryNodes = 256;

		// Token: 0x0400032E RID: 814
		private readonly GlobalNodeStorage.IndexedStack<uint>[] nodeIndexPools = new GlobalNodeStorage.IndexedStack<uint>[]
		{
			new GlobalNodeStorage.IndexedStack<uint>(),
			new GlobalNodeStorage.IndexedStack<uint>(),
			new GlobalNodeStorage.IndexedStack<uint>()
		};

		// Token: 0x0400032F RID: 815
		public GlobalNodeStorage.PathfindingThreadData[] pathfindingThreadData = new GlobalNodeStorage.PathfindingThreadData[0];

		// Token: 0x04000330 RID: 816
		private GraphNode[] nodes = new GraphNode[0];

		// Token: 0x02000097 RID: 151
		public struct PathfindingThreadData
		{
			// Token: 0x04000331 RID: 817
			public UnsafeSpan<PathNode> pathNodes;
		}

		// Token: 0x02000098 RID: 152
		private class IndexedStack<T>
		{
			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060004DC RID: 1244 RVA: 0x00017F2A File Offset: 0x0001612A
			// (set) Token: 0x060004DD RID: 1245 RVA: 0x00017F32 File Offset: 0x00016132
			public int Count { get; private set; }

			// Token: 0x060004DE RID: 1246 RVA: 0x00017F3C File Offset: 0x0001613C
			public void Push(T v)
			{
				if (this.Count == this.buffer.Length)
				{
					Memory.Realloc<T>(ref this.buffer, this.buffer.Length * 2);
				}
				this.buffer[this.Count] = v;
				int count = this.Count;
				this.Count = count + 1;
			}

			// Token: 0x060004DF RID: 1247 RVA: 0x00017F90 File Offset: 0x00016190
			public void Clear()
			{
				this.Count = 0;
			}

			// Token: 0x060004E0 RID: 1248 RVA: 0x00017F9C File Offset: 0x0001619C
			public T Pop()
			{
				int count = this.Count;
				this.Count = count - 1;
				return this.buffer[this.Count];
			}

			// Token: 0x060004E1 RID: 1249 RVA: 0x00017FCA File Offset: 0x000161CA
			public void PopMany(T[] resultBuffer, int popCount)
			{
				if (popCount > this.Count)
				{
					throw new IndexOutOfRangeException();
				}
				Array.Copy(this.buffer, this.Count - popCount, resultBuffer, 0, popCount);
				this.Count -= popCount;
			}

			// Token: 0x04000332 RID: 818
			private T[] buffer = new T[4];
		}

		// Token: 0x02000099 RID: 153
		private struct JobAllocateNodes<T> : IJob where T : GraphNode
		{
			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060004E3 RID: 1251 RVA: 0x00018013 File Offset: 0x00016213
			public bool allowBoundsChecks
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060004E4 RID: 1252 RVA: 0x00018018 File Offset: 0x00016218
			public void Execute()
			{
				HierarchicalGraph hierarchicalGraph = this.nodeStorage.astar.hierarchicalGraph;
				GlobalNodeStorage globalNodeStorage = this.nodeStorage;
				lock (globalNodeStorage)
				{
					GlobalNodeStorage.IndexedStack<uint> indexedStack = this.nodeStorage.nodeIndexPools[(int)(this.variantsPerNode - 1U)];
					uint num = this.nodeStorage.nextNodeIndex;
					uint num2 = 0U;
					while ((ulong)num2 < (ulong)((long)this.count))
					{
						T t = (this.result[(int)num2] = this.createNode());
						if (indexedStack.Count > 0)
						{
							t.NodeIndex = indexedStack.Pop();
						}
						else
						{
							t.NodeIndex = num;
							num += this.variantsPerNode;
						}
						num2 += 1U;
					}
					this.nodeStorage.ReserveNodeIndices(num);
					this.nodeStorage.nextNodeIndex = num;
					for (int i = 0; i < this.count; i++)
					{
						T t2 = this.result[i];
						hierarchicalGraph.AddDirtyNode(t2);
						this.nodeStorage.nodes[(int)t2.NodeIndex] = t2;
					}
				}
			}

			// Token: 0x04000334 RID: 820
			public T[] result;

			// Token: 0x04000335 RID: 821
			public int count;

			// Token: 0x04000336 RID: 822
			public GlobalNodeStorage nodeStorage;

			// Token: 0x04000337 RID: 823
			public uint variantsPerNode;

			// Token: 0x04000338 RID: 824
			public Func<T> createNode;
		}
	}
}
