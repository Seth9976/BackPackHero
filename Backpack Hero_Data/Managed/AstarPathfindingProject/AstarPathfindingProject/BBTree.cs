using System;
using System.Diagnostics;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200006A RID: 106
	public class BBTree : IAstarPooledObject
	{
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x000213FC File Offset: 0x0001F5FC
		public Rect Size
		{
			get
			{
				if (this.count == 0)
				{
					return new Rect(0f, 0f, 0f, 0f);
				}
				IntRect rect = this.tree[0].rect;
				return Rect.MinMaxRect((float)rect.xmin * 0.001f, (float)rect.ymin * 0.001f, (float)rect.xmax * 0.001f, (float)rect.ymax * 0.001f);
			}
		}

		// Token: 0x060005CC RID: 1484 RVA: 0x00021478 File Offset: 0x0001F678
		public void Clear()
		{
			this.count = 0;
			this.leafNodes = 0;
			if (this.tree != null)
			{
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
			}
			if (this.nodeLookup != null)
			{
				for (int i = 0; i < this.nodeLookup.Length; i++)
				{
					this.nodeLookup[i] = null;
				}
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
			}
			this.tree = ArrayPool<BBTree.BBTreeBox>.Claim(0);
			this.nodeLookup = ArrayPool<TriangleMeshNode>.Claim(0);
		}

		// Token: 0x060005CD RID: 1485 RVA: 0x000214EF File Offset: 0x0001F6EF
		void IAstarPooledObject.OnEnterPool()
		{
			this.Clear();
		}

		// Token: 0x060005CE RID: 1486 RVA: 0x000214F8 File Offset: 0x0001F6F8
		private void EnsureCapacity(int c)
		{
			if (c > this.tree.Length)
			{
				BBTree.BBTreeBox[] array = ArrayPool<BBTree.BBTreeBox>.Claim(c);
				this.tree.CopyTo(array, 0);
				ArrayPool<BBTree.BBTreeBox>.Release(ref this.tree, false);
				this.tree = array;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x00021538 File Offset: 0x0001F738
		private void EnsureNodeCapacity(int c)
		{
			if (c > this.nodeLookup.Length)
			{
				TriangleMeshNode[] array = ArrayPool<TriangleMeshNode>.Claim(c);
				this.nodeLookup.CopyTo(array, 0);
				ArrayPool<TriangleMeshNode>.Release(ref this.nodeLookup, false);
				this.nodeLookup = array;
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x00021578 File Offset: 0x0001F778
		private int GetBox(IntRect rect)
		{
			if (this.count >= this.tree.Length)
			{
				this.EnsureCapacity(this.count + 1);
			}
			this.tree[this.count] = new BBTree.BBTreeBox(rect);
			this.count++;
			return this.count - 1;
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000215D0 File Offset: 0x0001F7D0
		public void RebuildFrom(TriangleMeshNode[] nodes)
		{
			this.Clear();
			if (nodes.Length == 0)
			{
				return;
			}
			this.EnsureCapacity(Mathf.CeilToInt((float)nodes.Length * 2.1f));
			this.EnsureNodeCapacity(Mathf.CeilToInt((float)nodes.Length * 1.1f));
			int[] array = ArrayPool<int>.Claim(nodes.Length);
			for (int i = 0; i < nodes.Length; i++)
			{
				array[i] = i;
			}
			IntRect[] array2 = ArrayPool<IntRect>.Claim(nodes.Length);
			for (int j = 0; j < nodes.Length; j++)
			{
				Int3 @int;
				Int3 int2;
				Int3 int3;
				nodes[j].GetVertices(out @int, out int2, out int3);
				IntRect intRect = new IntRect(@int.x, @int.z, @int.x, @int.z);
				intRect = intRect.ExpandToContain(int2.x, int2.z);
				intRect = intRect.ExpandToContain(int3.x, int3.z);
				array2[j] = intRect;
			}
			this.RebuildFromInternal(nodes, array, array2, 0, nodes.Length, false);
			ArrayPool<int>.Release(ref array, false);
			ArrayPool<IntRect>.Release(ref array2, false);
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000216CC File Offset: 0x0001F8CC
		private static int SplitByX(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.x > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00021714 File Offset: 0x0001F914
		private static int SplitByZ(TriangleMeshNode[] nodes, int[] permutation, int from, int to, int divider)
		{
			int num = to;
			for (int i = from; i < num; i++)
			{
				if (nodes[permutation[i]].position.z > divider)
				{
					num--;
					int num2 = permutation[num];
					permutation[num] = permutation[i];
					permutation[i] = num2;
					i--;
				}
			}
			return num;
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x0002175C File Offset: 0x0001F95C
		private int RebuildFromInternal(TriangleMeshNode[] nodes, int[] permutation, IntRect[] nodeBounds, int from, int to, bool odd)
		{
			IntRect intRect = BBTree.NodeBounds(permutation, nodeBounds, from, to);
			int box = this.GetBox(intRect);
			if (to - from <= 4)
			{
				int num = (this.tree[box].nodeOffset = this.leafNodes * 4);
				this.EnsureNodeCapacity(num + 4);
				this.leafNodes++;
				for (int i = 0; i < 4; i++)
				{
					this.nodeLookup[num + i] = ((i < to - from) ? nodes[permutation[from + i]] : null);
				}
				return box;
			}
			int num3;
			if (odd)
			{
				int num2 = (intRect.xmin + intRect.xmax) / 2;
				num3 = BBTree.SplitByX(nodes, permutation, from, to, num2);
			}
			else
			{
				int num4 = (intRect.ymin + intRect.ymax) / 2;
				num3 = BBTree.SplitByZ(nodes, permutation, from, to, num4);
			}
			if (num3 == from || num3 == to)
			{
				if (!odd)
				{
					int num5 = (intRect.xmin + intRect.xmax) / 2;
					num3 = BBTree.SplitByX(nodes, permutation, from, to, num5);
				}
				else
				{
					int num6 = (intRect.ymin + intRect.ymax) / 2;
					num3 = BBTree.SplitByZ(nodes, permutation, from, to, num6);
				}
				if (num3 == from || num3 == to)
				{
					num3 = (from + to) / 2;
				}
			}
			this.tree[box].left = this.RebuildFromInternal(nodes, permutation, nodeBounds, from, num3, !odd);
			this.tree[box].right = this.RebuildFromInternal(nodes, permutation, nodeBounds, num3, to, !odd);
			return box;
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x000218D8 File Offset: 0x0001FAD8
		private static IntRect NodeBounds(int[] permutation, IntRect[] nodeBounds, int from, int to)
		{
			IntRect intRect = nodeBounds[permutation[from]];
			for (int i = from + 1; i < to; i++)
			{
				IntRect intRect2 = nodeBounds[permutation[i]];
				intRect.xmin = Math.Min(intRect.xmin, intRect2.xmin);
				intRect.ymin = Math.Min(intRect.ymin, intRect2.ymin);
				intRect.xmax = Math.Max(intRect.xmax, intRect2.xmax);
				intRect.ymax = Math.Max(intRect.ymax, intRect2.ymax);
			}
			return intRect;
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00021968 File Offset: 0x0001FB68
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugRect(IntRect rect)
		{
			Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymin), Color.white);
			Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymax), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
			Debug.DrawLine(new Vector3((float)rect.xmin, 0f, (float)rect.ymin), new Vector3((float)rect.xmin, 0f, (float)rect.ymax), Color.white);
			Debug.DrawLine(new Vector3((float)rect.xmax, 0f, (float)rect.ymin), new Vector3((float)rect.xmax, 0f, (float)rect.ymax), Color.white);
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00021A60 File Offset: 0x0001FC60
		[Conditional("ASTARDEBUG")]
		private static void DrawDebugNode(TriangleMeshNode node, float yoffset, Color color)
		{
			Debug.DrawLine((Vector3)node.GetVertex(1) + Vector3.up * yoffset, (Vector3)node.GetVertex(2) + Vector3.up * yoffset, color);
			Debug.DrawLine((Vector3)node.GetVertex(0) + Vector3.up * yoffset, (Vector3)node.GetVertex(1) + Vector3.up * yoffset, color);
			Debug.DrawLine((Vector3)node.GetVertex(2) + Vector3.up * yoffset, (Vector3)node.GetVertex(0) + Vector3.up * yoffset, color);
		}

		// Token: 0x060005D8 RID: 1496 RVA: 0x00021B27 File Offset: 0x0001FD27
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, out float distance)
		{
			distance = float.PositiveInfinity;
			return this.QueryClosest(p, constraint, ref distance, new NNInfoInternal(null));
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x00021B40 File Offset: 0x0001FD40
		public NNInfoInternal QueryClosestXZ(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosestXZ(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00021B94 File Offset: 0x0001FD94
		private void SearchBoxClosestXZ(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (constraint == null || constraint.Suitable(triangleMeshNode))
					{
						Vector3 vector = triangleMeshNode.ClosestPointOnNodeXZ(p);
						float num = (vector.x - p.x) * (vector.x - p.x) + (vector.z - p.z) * (vector.z - p.z);
						if (nnInfo.constrainedNode == null || num < closestSqrDist - 1E-06f || (num <= closestSqrDist + 1E-06f && Mathf.Abs(vector.y - p.y) < Mathf.Abs(nnInfo.constClampedPosition.y - p.y)))
						{
							nnInfo.constrainedNode = triangleMeshNode;
							nnInfo.constClampedPosition = vector;
							closestSqrDist = num;
						}
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num2;
				float num3;
				this.GetOrderedChildren(ref left, ref right, out num2, out num3, p);
				if (num2 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num3 <= closestSqrDist)
				{
					this.SearchBoxClosestXZ(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00021CEC File Offset: 0x0001FEEC
		public NNInfoInternal QueryClosest(Vector3 p, NNConstraint constraint, ref float distance, NNInfoInternal previous)
		{
			float num = distance * distance;
			float num2 = num;
			if (this.count > 0 && BBTree.SquaredRectPointDistance(this.tree[0].rect, p) < num)
			{
				this.SearchBoxClosest(0, p, ref num, constraint, ref previous);
				if (num < num2)
				{
					distance = Mathf.Sqrt(num);
				}
			}
			return previous;
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x00021D40 File Offset: 0x0001FF40
		private void SearchBoxClosest(int boxi, Vector3 p, ref float closestSqrDist, NNConstraint constraint, ref NNInfoInternal nnInfo)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						return;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					Vector3 vector = triangleMeshNode.ClosestPointOnNode(p);
					float sqrMagnitude = (vector - p).sqrMagnitude;
					if (sqrMagnitude < closestSqrDist && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						nnInfo.constrainedNode = triangleMeshNode;
						nnInfo.constClampedPosition = vector;
						closestSqrDist = sqrMagnitude;
					}
				}
			}
			else
			{
				int left = bbtreeBox.left;
				int right = bbtreeBox.right;
				float num;
				float num2;
				this.GetOrderedChildren(ref left, ref right, out num, out num2, p);
				if (num < closestSqrDist)
				{
					this.SearchBoxClosest(left, p, ref closestSqrDist, constraint, ref nnInfo);
				}
				if (num2 < closestSqrDist)
				{
					this.SearchBoxClosest(right, p, ref closestSqrDist, constraint, ref nnInfo);
				}
			}
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00021E1C File Offset: 0x0002001C
		private void GetOrderedChildren(ref int first, ref int second, out float firstDist, out float secondDist, Vector3 p)
		{
			firstDist = BBTree.SquaredRectPointDistance(this.tree[first].rect, p);
			secondDist = BBTree.SquaredRectPointDistance(this.tree[second].rect, p);
			if (secondDist < firstDist)
			{
				int num = first;
				first = second;
				second = num;
				float num2 = firstDist;
				firstDist = secondDist;
				secondDist = num2;
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00021E7D File Offset: 0x0002007D
		public TriangleMeshNode QueryInside(Vector3 p, NNConstraint constraint)
		{
			if (this.count == 0 || !this.tree[0].Contains(p))
			{
				return null;
			}
			return this.SearchBoxInside(0, p, constraint);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x00021EA8 File Offset: 0x000200A8
		private TriangleMeshNode SearchBoxInside(int boxi, Vector3 p, NNConstraint constraint)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			if (bbtreeBox.IsLeaf)
			{
				TriangleMeshNode[] array = this.nodeLookup;
				for (int i = 0; i < 4; i++)
				{
					if (array[bbtreeBox.nodeOffset + i] == null)
					{
						break;
					}
					TriangleMeshNode triangleMeshNode = array[bbtreeBox.nodeOffset + i];
					if (triangleMeshNode.ContainsPoint((Int3)p) && (constraint == null || constraint.Suitable(triangleMeshNode)))
					{
						return triangleMeshNode;
					}
				}
			}
			else
			{
				if (this.tree[bbtreeBox.left].Contains(p))
				{
					TriangleMeshNode triangleMeshNode2 = this.SearchBoxInside(bbtreeBox.left, p, constraint);
					if (triangleMeshNode2 != null)
					{
						return triangleMeshNode2;
					}
				}
				if (this.tree[bbtreeBox.right].Contains(p))
				{
					TriangleMeshNode triangleMeshNode3 = this.SearchBoxInside(bbtreeBox.right, p, constraint);
					if (triangleMeshNode3 != null)
					{
						return triangleMeshNode3;
					}
				}
			}
			return null;
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00021F74 File Offset: 0x00020174
		public void OnDrawGizmos()
		{
			Gizmos.color = new Color(1f, 1f, 1f, 0.5f);
			if (this.count == 0)
			{
				return;
			}
			this.OnDrawGizmos(0, 0);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00021FA8 File Offset: 0x000201A8
		private void OnDrawGizmos(int boxi, int depth)
		{
			BBTree.BBTreeBox bbtreeBox = this.tree[boxi];
			Vector3 vector = (Vector3)new Int3(bbtreeBox.rect.xmin, 0, bbtreeBox.rect.ymin);
			Vector3 vector2 = (Vector3)new Int3(bbtreeBox.rect.xmax, 0, bbtreeBox.rect.ymax);
			Vector3 vector3 = (vector + vector2) * 0.5f;
			Vector3 vector4 = (vector2 - vector3) * 2f;
			vector4 = new Vector3(vector4.x, 1f, vector4.z);
			vector3.y += (float)(depth * 2);
			Gizmos.color = AstarMath.IntToColor(depth, 1f);
			Gizmos.DrawCube(vector3, vector4);
			if (!bbtreeBox.IsLeaf)
			{
				this.OnDrawGizmos(bbtreeBox.left, depth + 1);
				this.OnDrawGizmos(bbtreeBox.right, depth + 1);
			}
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00022090 File Offset: 0x00020290
		private static bool NodeIntersectsCircle(TriangleMeshNode node, Vector3 p, float radius)
		{
			return float.IsPositiveInfinity(radius) || (p - node.ClosestPointOnNode(p)).sqrMagnitude < radius * radius;
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x000220C4 File Offset: 0x000202C4
		private static bool RectIntersectsCircle(IntRect r, Vector3 p, float radius)
		{
			if (float.IsPositiveInfinity(radius))
			{
				return true;
			}
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z) < radius * radius;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00022198 File Offset: 0x00020398
		private static float SquaredRectPointDistance(IntRect r, Vector3 p)
		{
			Vector3 vector = p;
			p.x = Math.Max(p.x, (float)r.xmin * 0.001f);
			p.x = Math.Min(p.x, (float)r.xmax * 0.001f);
			p.z = Math.Max(p.z, (float)r.ymin * 0.001f);
			p.z = Math.Min(p.z, (float)r.ymax * 0.001f);
			return (p.x - vector.x) * (p.x - vector.x) + (p.z - vector.z) * (p.z - vector.z);
		}

		// Token: 0x04000342 RID: 834
		private BBTree.BBTreeBox[] tree;

		// Token: 0x04000343 RID: 835
		private TriangleMeshNode[] nodeLookup;

		// Token: 0x04000344 RID: 836
		private int count;

		// Token: 0x04000345 RID: 837
		private int leafNodes;

		// Token: 0x04000346 RID: 838
		private const int MaximumLeafSize = 4;

		// Token: 0x02000128 RID: 296
		private struct BBTreeBox
		{
			// Token: 0x1700018A RID: 394
			// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0004300B File Offset: 0x0004120B
			public bool IsLeaf
			{
				get
				{
					return this.nodeOffset >= 0;
				}
			}

			// Token: 0x06000AAD RID: 2733 RVA: 0x0004301C File Offset: 0x0004121C
			public BBTreeBox(IntRect rect)
			{
				this.nodeOffset = -1;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06000AAE RID: 2734 RVA: 0x00043048 File Offset: 0x00041248
			public BBTreeBox(int nodeOffset, IntRect rect)
			{
				this.nodeOffset = nodeOffset;
				this.rect = rect;
				this.left = (this.right = -1);
			}

			// Token: 0x06000AAF RID: 2735 RVA: 0x00043074 File Offset: 0x00041274
			public bool Contains(Vector3 point)
			{
				Int3 @int = (Int3)point;
				return this.rect.Contains(@int.x, @int.z);
			}

			// Token: 0x040006E3 RID: 1763
			public IntRect rect;

			// Token: 0x040006E4 RID: 1764
			public int nodeOffset;

			// Token: 0x040006E5 RID: 1765
			public int left;

			// Token: 0x040006E6 RID: 1766
			public int right;
		}
	}
}
