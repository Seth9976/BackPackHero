using System;
using System.Text;

namespace Pathfinding
{
	// Token: 0x02000053 RID: 83
	public class PathHandler
	{
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x000149AB File Offset: 0x00012BAB
		public ushort PathID
		{
			get
			{
				return this.pathID;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000149B3 File Offset: 0x00012BB3
		public PathHandler(int threadID, int totalThreadCount)
		{
			this.threadID = threadID;
			this.totalThreadCount = totalThreadCount;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000149F0 File Offset: 0x00012BF0
		public void InitializeForPath(Path p)
		{
			this.pathID = p.pathID;
			this.heap.Clear();
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00014A09 File Offset: 0x00012C09
		public void DestroyNode(GraphNode node)
		{
			PathNode pathNode = this.GetPathNode(node);
			pathNode.node = null;
			pathNode.parent = null;
			pathNode.pathID = 0;
			pathNode.G = 0U;
			pathNode.H = 0U;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x00014A34 File Offset: 0x00012C34
		public void InitializeNode(GraphNode node)
		{
			int nodeIndex = node.NodeIndex;
			if (nodeIndex >= this.nodes.Length)
			{
				PathNode[] array = new PathNode[Math.Max(128, this.nodes.Length * 2)];
				this.nodes.CopyTo(array, 0);
				for (int i = this.nodes.Length; i < array.Length; i++)
				{
					array[i] = new PathNode();
				}
				this.nodes = array;
			}
			this.nodes[nodeIndex].node = node;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00014AAC File Offset: 0x00012CAC
		public PathNode GetPathNode(int nodeIndex)
		{
			return this.nodes[nodeIndex];
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00014AB6 File Offset: 0x00012CB6
		public PathNode GetPathNode(GraphNode node)
		{
			return this.nodes[node.NodeIndex];
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x00014AC8 File Offset: 0x00012CC8
		public void ClearPathIDs()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i] != null)
				{
					this.nodes[i].pathID = 0;
				}
			}
		}

		// Token: 0x0400026F RID: 623
		private ushort pathID;

		// Token: 0x04000270 RID: 624
		public readonly int threadID;

		// Token: 0x04000271 RID: 625
		public readonly int totalThreadCount;

		// Token: 0x04000272 RID: 626
		public readonly BinaryHeap heap = new BinaryHeap(128);

		// Token: 0x04000273 RID: 627
		public PathNode[] nodes = new PathNode[0];

		// Token: 0x04000274 RID: 628
		public readonly StringBuilder DebugStringBuilder = new StringBuilder();
	}
}
