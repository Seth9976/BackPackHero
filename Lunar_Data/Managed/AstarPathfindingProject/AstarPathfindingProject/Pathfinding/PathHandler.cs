using System;
using System.Runtime.CompilerServices;
using System.Text;
using Pathfinding.Util;
using Unity.Collections;

namespace Pathfinding
{
	// Token: 0x020000B0 RID: 176
	public class PathHandler
	{
		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x0001AAC3 File Offset: 0x00018CC3
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x0001AACB File Offset: 0x00018CCB
		public int numTemporaryNodes { get; private set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0001AAD4 File Offset: 0x00018CD4
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x0001AADC File Offset: 0x00018CDC
		public uint temporaryNodeStartIndex { get; private set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001AAE5 File Offset: 0x00018CE5
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x06000595 RID: 1429 RVA: 0x0001AAF0 File Offset: 0x00018CF0
		internal PathHandler(GlobalNodeStorage nodeStorage, int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
			this.nodeStorage = nodeStorage;
			this.temporaryNodes = new UnsafeSpan<TemporaryNode>(Allocator.Persistent, 256);
		}

		// Token: 0x06000596 RID: 1430 RVA: 0x0001AB44 File Offset: 0x00018D44
		public void InitializeForPath(Path p)
		{
			ushort num = this.pathID;
			this.pathID = p.pathID;
			this.numTemporaryNodes = 0;
			this.temporaryNodeStartIndex = this.nodeStorage.nextNodeIndex;
			this.pathNodes = this.nodeStorage.pathfindingThreadData[this.threadID].pathNodes;
			if (this.pathID < num)
			{
				this.ClearPathIDs();
			}
		}

		// Token: 0x06000597 RID: 1431 RVA: 0x0001ABAC File Offset: 0x00018DAC
		public ref PathNode GetPathNode(GraphNode node, uint variant = 0U)
		{
			return this.pathNodes[node.NodeIndex + variant];
		}

		// Token: 0x06000598 RID: 1432 RVA: 0x0001ABC1 File Offset: 0x00018DC1
		public bool IsTemporaryNode(uint pathNodeIndex)
		{
			return pathNodeIndex >= this.temporaryNodeStartIndex;
		}

		// Token: 0x06000599 RID: 1433 RVA: 0x0001ABD0 File Offset: 0x00018DD0
		public unsafe uint AddTemporaryNode(TemporaryNode node)
		{
			if (this.numTemporaryNodes >= 256)
			{
				throw new InvalidOperationException("Cannot create more than " + 256.ToString() + " temporary nodes. You can enable ASTAR_MORE_MULTI_TARGET_PATH_TARGETS in the A* Inspector optimizations tab to increase this limit.");
			}
			uint num = this.temporaryNodeStartIndex + (uint)this.numTemporaryNodes;
			*this.temporaryNodes[this.numTemporaryNodes] = node;
			*this.pathNodes[num] = PathNode.Default;
			int numTemporaryNodes = this.numTemporaryNodes;
			this.numTemporaryNodes = numTemporaryNodes + 1;
			return num;
		}

		// Token: 0x0600059A RID: 1434 RVA: 0x0001AC58 File Offset: 0x00018E58
		public GraphNode GetNode(uint nodeIndex)
		{
			return this.nodeStorage.GetNode(nodeIndex);
		}

		// Token: 0x0600059B RID: 1435 RVA: 0x0001AC66 File Offset: 0x00018E66
		public ref TemporaryNode GetTemporaryNode(uint nodeIndex)
		{
			if (nodeIndex < this.temporaryNodeStartIndex || (ulong)nodeIndex >= (ulong)this.temporaryNodeStartIndex + (ulong)((long)this.numTemporaryNodes))
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.temporaryNodes[(int)(nodeIndex - this.temporaryNodeStartIndex)];
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x000033F6 File Offset: 0x000015F6
		[MethodImpl(256)]
		public void LogVisitedNode(uint pathNodeIndex, uint h, uint g)
		{
		}

		// Token: 0x0600059D RID: 1437 RVA: 0x0001ACA0 File Offset: 0x00018EA0
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.pathNodes.Length; i++)
			{
				this.pathNodes[i].pathID = 0;
			}
		}

		// Token: 0x0600059E RID: 1438 RVA: 0x0001ACD5 File Offset: 0x00018ED5
		public void Dispose()
		{
			this.heap.Dispose();
			this.temporaryNodes.Free(Allocator.Persistent);
			this.pathNodes = default(UnsafeSpan<PathNode>);
		}

		// Token: 0x040003AC RID: 940
		private ushort pathID;

		// Token: 0x040003AD RID: 941
		public readonly int threadID;

		// Token: 0x040003AE RID: 942
		public readonly int totalThreadCount;

		// Token: 0x040003AF RID: 943
		internal readonly GlobalNodeStorage nodeStorage;

		// Token: 0x040003B2 RID: 946
		private UnsafeSpan<TemporaryNode> temporaryNodes;

		// Token: 0x040003B3 RID: 947
		public UnsafeSpan<PathNode> pathNodes;

		// Token: 0x040003B4 RID: 948
		public BinaryHeap heap = new BinaryHeap(128);

		// Token: 0x040003B5 RID: 949
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
