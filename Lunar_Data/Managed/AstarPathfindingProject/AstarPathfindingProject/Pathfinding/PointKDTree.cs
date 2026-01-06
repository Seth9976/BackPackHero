using System;
using System.Collections.Generic;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x020000ED RID: 237
	public class PointKDTree
	{
		// Token: 0x060007D5 RID: 2005 RVA: 0x0002885C File Offset: 0x00026A5C
		public PointKDTree()
		{
			this.tree[1] = new PointKDTree.Node
			{
				data = this.GetOrCreateList()
			};
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000288B4 File Offset: 0x00026AB4
		public void Add(GraphNode node)
		{
			this.numNodes++;
			this.Add(node, 1, 0);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000288D0 File Offset: 0x00026AD0
		public void Rebuild(GraphNode[] nodes, int start, int end)
		{
			if (start < 0 || end < start || end > nodes.Length)
			{
				throw new ArgumentException();
			}
			for (int i = 0; i < this.tree.Length; i++)
			{
				GraphNode[] data = this.tree[i].data;
				if (data != null)
				{
					for (int j = 0; j < 21; j++)
					{
						data[j] = null;
					}
					this.arrayCache.Push(data);
					this.tree[i].data = null;
				}
			}
			this.numNodes = end - start;
			this.Build(1, new List<GraphNode>(nodes), start, end);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00028960 File Offset: 0x00026B60
		private GraphNode[] GetOrCreateList()
		{
			if (this.arrayCache.Count <= 0)
			{
				return new GraphNode[21];
			}
			return this.arrayCache.Pop();
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00028983 File Offset: 0x00026B83
		private int Size(int index)
		{
			if (this.tree[index].data == null)
			{
				return this.Size(2 * index) + this.Size(2 * index + 1);
			}
			return (int)this.tree[index].count;
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x000289C0 File Offset: 0x00026BC0
		private void CollectAndClear(int index, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			ushort count = this.tree[index].count;
			if (data != null)
			{
				this.tree[index] = default(PointKDTree.Node);
				for (int i = 0; i < (int)count; i++)
				{
					buffer.Add(data[i]);
					data[i] = null;
				}
				this.arrayCache.Push(data);
				return;
			}
			this.CollectAndClear(index * 2, buffer);
			this.CollectAndClear(index * 2 + 1, buffer);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00028A42 File Offset: 0x00026C42
		private static int MaxAllowedSize(int numNodes, int depth)
		{
			return Math.Min(5 * numNodes / 2 >> depth, 3 * numNodes / 4);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00028A58 File Offset: 0x00026C58
		private void Rebalance(int index)
		{
			this.CollectAndClear(index, this.largeList);
			this.Build(index, this.largeList, 0, this.largeList.Count);
			this.largeList.ClearFast<GraphNode>();
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00028A8C File Offset: 0x00026C8C
		private void EnsureSize(int index)
		{
			if (index >= this.tree.Length)
			{
				PointKDTree.Node[] array = new PointKDTree.Node[Math.Max(index + 1, this.tree.Length * 2)];
				this.tree.CopyTo(array, 0);
				this.tree = array;
			}
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00028AD0 File Offset: 0x00026CD0
		private void Build(int index, List<GraphNode> nodes, int start, int end)
		{
			this.EnsureSize(index);
			if (end - start <= 10)
			{
				GraphNode[] array = (this.tree[index].data = this.GetOrCreateList());
				this.tree[index].count = (ushort)(end - start);
				for (int i = start; i < end; i++)
				{
					array[i - start] = nodes[i];
				}
				return;
			}
			Int3 position;
			Int3 @int = (position = nodes[start].position);
			for (int j = start; j < end; j++)
			{
				Int3 position2 = nodes[j].position;
				position = new Int3(Math.Min(position.x, position2.x), Math.Min(position.y, position2.y), Math.Min(position.z, position2.z));
				@int = new Int3(Math.Max(@int.x, position2.x), Math.Max(@int.y, position2.y), Math.Max(@int.z, position2.z));
			}
			Int3 int2 = @int - position;
			int num = ((int2.x > int2.y) ? ((int2.x > int2.z) ? 0 : 2) : ((int2.y > int2.z) ? 1 : 2));
			nodes.Sort(start, end - start, PointKDTree.comparers[num]);
			int num2 = (start + end) / 2;
			this.tree[index].split = (nodes[num2 - 1].position[num] + nodes[num2].position[num] + 1) / 2;
			this.tree[index].splitAxis = (byte)num;
			this.Build(index * 2, nodes, start, num2);
			this.Build(index * 2 + 1, nodes, num2, end);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00028CBC File Offset: 0x00026EBC
		private void Add(GraphNode point, int index, int depth = 0)
		{
			while (this.tree[index].data == null)
			{
				index = 2 * index + ((point.position[(int)this.tree[index].splitAxis] < this.tree[index].split) ? 0 : 1);
				depth++;
			}
			GraphNode[] data = this.tree[index].data;
			PointKDTree.Node[] array = this.tree;
			int num = index;
			ushort count = array[num].count;
			array[num].count = count + 1;
			data[(int)count] = point;
			if (this.tree[index].count >= 21)
			{
				int num2 = 0;
				while (depth - num2 > 0 && this.Size(index >> num2) > PointKDTree.MaxAllowedSize(this.numNodes, depth - num2))
				{
					num2++;
				}
				this.Rebalance(index >> num2);
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00028D94 File Offset: 0x00026F94
		public GraphNode GetNearest(Int3 point, NNConstraint constraint, ref float distanceSqr)
		{
			GraphNode graphNode = null;
			long num = ((distanceSqr < float.PositiveInfinity) ? ((long)(1000000f * distanceSqr)) : long.MaxValue);
			this.GetNearestInternal(1, point, constraint, ref graphNode, ref num);
			distanceSqr = ((graphNode != null) ? (1.0000001E-06f * (float)num) : float.PositiveInfinity);
			return graphNode;
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00028DE4 File Offset: 0x00026FE4
		private void GetNearestInternal(int index, Int3 point, NNConstraint constraint, ref GraphNode best, ref long bestSqrDist)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					long sqrMagnitudeLong = (data[i].position - point).sqrMagnitudeLong;
					if (sqrMagnitudeLong < bestSqrDist && (constraint == null || constraint.Suitable(data[i])))
					{
						bestSqrDist = sqrMagnitudeLong;
						best = data[i];
					}
				}
				return;
			}
			long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
			int num2 = 2 * index + ((num < 0L) ? 0 : 1);
			this.GetNearestInternal(num2, point, constraint, ref best, ref bestSqrDist);
			if (num * num < bestSqrDist)
			{
				this.GetNearestInternal(num2 ^ 1, point, constraint, ref best, ref bestSqrDist);
			}
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00028EC0 File Offset: 0x000270C0
		public GraphNode GetNearestConnection(Int3 point, NNConstraint constraint, long maximumSqrConnectionLength)
		{
			GraphNode graphNode = null;
			long maxValue = long.MaxValue;
			long num = (maximumSqrConnectionLength + 3L) / 4L;
			this.GetNearestConnectionInternal(1, point, constraint, ref graphNode, ref maxValue, num);
			return graphNode;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00028EF0 File Offset: 0x000270F0
		private void GetNearestConnectionInternal(int index, Int3 point, NNConstraint constraint, ref GraphNode best, ref long bestSqrDist, long distanceThresholdOffset)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				Vector3 vector = (Vector3)point;
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					long sqrMagnitudeLong = (data[i].position - point).sqrMagnitudeLong;
					if (sqrMagnitudeLong - distanceThresholdOffset < bestSqrDist && (constraint == null || constraint.Suitable(data[i])))
					{
						Connection[] connections = (data[i] as PointNode).connections;
						if (connections != null)
						{
							Vector3 vector2 = (Vector3)data[i].position;
							for (int j = 0; j < connections.Length; j++)
							{
								Vector3 vector3 = ((Vector3)connections[j].node.position + vector2) * 0.5f;
								long num = (long)(VectorMath.SqrDistancePointSegment(vector2, vector3, vector) * 1000f * 1000f);
								if (num < bestSqrDist)
								{
									bestSqrDist = num;
									best = data[i];
								}
							}
						}
						if (sqrMagnitudeLong < bestSqrDist)
						{
							bestSqrDist = sqrMagnitudeLong;
							best = data[i];
						}
					}
				}
				return;
			}
			long num2 = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
			int num3 = 2 * index + ((num2 < 0L) ? 0 : 1);
			this.GetNearestConnectionInternal(num3, point, constraint, ref best, ref bestSqrDist, distanceThresholdOffset);
			if (num2 * num2 - distanceThresholdOffset < bestSqrDist)
			{
				this.GetNearestConnectionInternal(num3 ^ 1, point, constraint, ref best, ref bestSqrDist, distanceThresholdOffset);
			}
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x0002907D File Offset: 0x0002727D
		public void GetInRange(Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			this.GetInRangeInternal(1, point, sqrRadius, buffer);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x0002908C File Offset: 0x0002728C
		private void GetInRangeInternal(int index, Int3 point, long sqrRadius, List<GraphNode> buffer)
		{
			GraphNode[] data = this.tree[index].data;
			if (data != null)
			{
				for (int i = (int)(this.tree[index].count - 1); i >= 0; i--)
				{
					if ((data[i].position - point).sqrMagnitudeLong < sqrRadius)
					{
						buffer.Add(data[i]);
					}
				}
				return;
			}
			long num = (long)(point[(int)this.tree[index].splitAxis] - this.tree[index].split);
			int num2 = 2 * index + ((num < 0L) ? 0 : 1);
			this.GetInRangeInternal(num2, point, sqrRadius, buffer);
			if (num * num < sqrRadius)
			{
				this.GetInRangeInternal(num2 ^ 1, point, sqrRadius, buffer);
			}
		}

		// Token: 0x040004E1 RID: 1249
		public const int LeafSize = 10;

		// Token: 0x040004E2 RID: 1250
		public const int LeafArraySize = 21;

		// Token: 0x040004E3 RID: 1251
		private PointKDTree.Node[] tree = new PointKDTree.Node[16];

		// Token: 0x040004E4 RID: 1252
		private int numNodes;

		// Token: 0x040004E5 RID: 1253
		private readonly List<GraphNode> largeList = new List<GraphNode>();

		// Token: 0x040004E6 RID: 1254
		private readonly Stack<GraphNode[]> arrayCache = new Stack<GraphNode[]>();

		// Token: 0x040004E7 RID: 1255
		private static readonly IComparer<GraphNode>[] comparers = new IComparer<GraphNode>[]
		{
			new PointKDTree.CompareX(),
			new PointKDTree.CompareY(),
			new PointKDTree.CompareZ()
		};

		// Token: 0x020000EE RID: 238
		private struct Node
		{
			// Token: 0x040004E8 RID: 1256
			public GraphNode[] data;

			// Token: 0x040004E9 RID: 1257
			public int split;

			// Token: 0x040004EA RID: 1258
			public ushort count;

			// Token: 0x040004EB RID: 1259
			public byte splitAxis;
		}

		// Token: 0x020000EF RID: 239
		private class CompareX : IComparer<GraphNode>
		{
			// Token: 0x060007E7 RID: 2023 RVA: 0x00029170 File Offset: 0x00027370
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.x.CompareTo(rhs.position.x);
			}
		}

		// Token: 0x020000F0 RID: 240
		private class CompareY : IComparer<GraphNode>
		{
			// Token: 0x060007E9 RID: 2025 RVA: 0x0002918D File Offset: 0x0002738D
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.y.CompareTo(rhs.position.y);
			}
		}

		// Token: 0x020000F1 RID: 241
		private class CompareZ : IComparer<GraphNode>
		{
			// Token: 0x060007EB RID: 2027 RVA: 0x000291AA File Offset: 0x000273AA
			public int Compare(GraphNode lhs, GraphNode rhs)
			{
				return lhs.position.z.CompareTo(rhs.position.z);
			}
		}
	}
}
