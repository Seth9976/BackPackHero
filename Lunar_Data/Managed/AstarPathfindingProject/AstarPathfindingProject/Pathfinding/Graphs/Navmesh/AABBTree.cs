using System;
using System.Collections.Generic;
using System.Diagnostics;
using Pathfinding.Util;
using Unity.Collections;
using UnityEngine;

namespace Pathfinding.Graphs.Navmesh
{
	// Token: 0x0200019C RID: 412
	public class AABBTree<T>
	{
		// Token: 0x06000B0B RID: 2827 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		private static float ExpansionRequired(Bounds b, Bounds b2)
		{
			Bounds bounds = b;
			bounds.Encapsulate(b2);
			return bounds.size.x * bounds.size.y * bounds.size.z - b.size.x * b.size.y * b.size.z;
		}

		// Token: 0x17000196 RID: 406
		public T this[AABBTree<T>.Key key]
		{
			get
			{
				return this.nodes[key.node].value;
			}
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x0003E468 File Offset: 0x0003C668
		public Bounds GetBounds(AABBTree<T>.Key key)
		{
			if (!key.isValid)
			{
				throw new ArgumentException("Key is not valid");
			}
			AABBTree<T>.Node node = this.nodes[key.node];
			if (!node.isAllocated)
			{
				throw new ArgumentException("Key does not point to an allocated node");
			}
			if (!node.isLeaf)
			{
				throw new ArgumentException("Key does not point to a leaf node");
			}
			return node.bounds;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x0003E4CC File Offset: 0x0003C6CC
		private int AllocNode()
		{
			int num;
			if (!this.freeNodes.TryPop(out num))
			{
				int num2 = this.nodes.Length;
				Memory.Realloc<AABBTree<T>.Node>(ref this.nodes, Mathf.Max(8, this.nodes.Length * 2));
				for (int i = this.nodes.Length - 1; i >= num2; i--)
				{
					this.FreeNode(i);
				}
				num = this.freeNodes.Pop();
			}
			return num;
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x0003E535 File Offset: 0x0003C735
		private void FreeNode(int node)
		{
			this.nodes[node].isAllocated = false;
			this.nodes[node].value = default(T);
			this.freeNodes.Push(node);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x0003E56C File Offset: 0x0003C76C
		public unsafe void Rebuild()
		{
			UnsafeSpan<int> unsafeSpan = new UnsafeSpan<int>(Allocator.Temp, this.nodes.Length);
			int num = 0;
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i].isAllocated)
				{
					if (this.nodes[i].isLeaf)
					{
						*unsafeSpan[num++] = i;
					}
					else
					{
						this.FreeNode(i);
					}
				}
			}
			this.root = this.Rebuild(unsafeSpan.Slice(0, num), 536870911);
			this.rebuildCounter = Mathf.Max(64, num / 3);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x0003E608 File Offset: 0x0003C808
		public void Clear()
		{
			for (int i = 0; i < this.nodes.Length; i++)
			{
				if (this.nodes[i].isAllocated)
				{
					this.FreeNode(i);
				}
			}
			this.root = -1;
			this.rebuildCounter = 64;
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x0003E654 File Offset: 0x0003C854
		private static int ArgMax(Vector3 v)
		{
			float num = Mathf.Max(v.x, Mathf.Max(v.y, v.z));
			if (num == v.x)
			{
				return 0;
			}
			if (num != v.y)
			{
				return 2;
			}
			return 1;
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x0003E698 File Offset: 0x0003C898
		private unsafe int Rebuild(UnsafeSpan<int> leaves, int parent)
		{
			if (leaves.Length == 0)
			{
				return -1;
			}
			if (leaves.Length == 1)
			{
				this.nodes[*leaves[0]].parent = parent;
				return *leaves[0];
			}
			Bounds bounds = this.nodes[*leaves[0]].bounds;
			for (int i = 1; i < leaves.Length; i++)
			{
				bounds.Encapsulate(this.nodes[*leaves[i]].bounds);
			}
			leaves.Sort(new AABBTree<T>.AABBComparer
			{
				nodes = this.nodes,
				dim = AABBTree<T>.ArgMax(bounds.extents)
			});
			int num = this.AllocNode();
			this.nodes[num] = new AABBTree<T>.Node
			{
				bounds = bounds,
				left = this.Rebuild(leaves.Slice(0, leaves.Length / 2), num),
				right = this.Rebuild(leaves.Slice(leaves.Length / 2), num),
				parent = parent,
				isAllocated = true
			};
			return num;
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		public void Move(AABBTree<T>.Key key, Bounds bounds)
		{
			T value = this.nodes[key.node].value;
			this.Remove(key);
			this.Add(bounds, value);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x0003E804 File Offset: 0x0003CA04
		[Conditional("VALIDATE_AABB_TREE")]
		private void Validate(int node)
		{
			if (node == -1)
			{
				return;
			}
			AABBTree<T>.Node node2 = this.nodes[node];
			int num = this.root;
			bool isLeaf = node2.isLeaf;
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0003E834 File Offset: 0x0003CA34
		public Bounds Remove(AABBTree<T>.Key key)
		{
			if (!key.isValid)
			{
				throw new ArgumentException("Key is not valid");
			}
			AABBTree<T>.Node node = this.nodes[key.node];
			if (!node.isAllocated)
			{
				throw new ArgumentException("Key does not point to an allocated node");
			}
			if (!node.isLeaf)
			{
				throw new ArgumentException("Key does not point to a leaf node");
			}
			if (key.node == this.root)
			{
				this.root = -1;
				this.FreeNode(key.node);
				return node.bounds;
			}
			int parent = node.parent;
			AABBTree<T>.Node node2 = this.nodes[parent];
			int num = ((node2.left == key.node) ? node2.right : node2.left);
			this.FreeNode(parent);
			this.FreeNode(key.node);
			this.nodes[num].parent = node2.parent;
			if (node2.parent == 536870911)
			{
				this.root = num;
			}
			else if (this.nodes[node2.parent].left == parent)
			{
				this.nodes[node2.parent].left = num;
			}
			else
			{
				this.nodes[node2.parent].right = num;
			}
			ref AABBTree<T>.Node ptr;
			for (int num2 = this.nodes[num].parent; num2 != 536870911; num2 = ptr.parent)
			{
				ptr = ref this.nodes[num2];
				Bounds bounds = this.nodes[ptr.left].bounds;
				bounds.Encapsulate(this.nodes[ptr.right].bounds);
				ptr.bounds = bounds;
				ptr.subtreePartiallyTagged = this.nodes[ptr.left].subtreePartiallyTagged | this.nodes[ptr.right].subtreePartiallyTagged;
			}
			return node.bounds;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x0003EA34 File Offset: 0x0003CC34
		public AABBTree<T>.Key Add(Bounds bounds, T value)
		{
			int num = this.AllocNode();
			this.nodes[num] = new AABBTree<T>.Node
			{
				bounds = bounds,
				parent = 536870911,
				left = -1,
				right = -1,
				value = value,
				isAllocated = true
			};
			if (this.root == -1)
			{
				this.root = num;
				return new AABBTree<T>.Key(num);
			}
			int num2 = this.root;
			AABBTree<T>.Node node;
			for (;;)
			{
				node = this.nodes[num2];
				this.nodes[num2].wholeSubtreeTagged = false;
				if (node.isLeaf)
				{
					break;
				}
				this.nodes[num2].bounds.Encapsulate(bounds);
				float num3 = AABBTree<T>.ExpansionRequired(this.nodes[node.left].bounds, bounds);
				float num4 = AABBTree<T>.ExpansionRequired(this.nodes[node.right].bounds, bounds);
				num2 = ((num3 < num4) ? node.left : node.right);
			}
			int num5 = this.AllocNode();
			if (node.parent != 536870911)
			{
				if (this.nodes[node.parent].left == num2)
				{
					this.nodes[node.parent].left = num5;
				}
				else
				{
					this.nodes[node.parent].right = num5;
				}
			}
			bounds.Encapsulate(node.bounds);
			this.nodes[num5] = new AABBTree<T>.Node
			{
				bounds = bounds,
				left = num2,
				right = num,
				parent = node.parent,
				isAllocated = true
			};
			this.nodes[num].parent = (this.nodes[num2].parent = num5);
			if (this.root == num2)
			{
				this.root = num5;
			}
			int num6 = this.rebuildCounter;
			this.rebuildCounter = num6 - 1;
			if (num6 <= 0)
			{
				this.Rebuild();
			}
			return new AABBTree<T>.Key(num);
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x0003EC55 File Offset: 0x0003CE55
		public void Query(Bounds bounds, List<T> buffer)
		{
			this.QueryNode(this.root, bounds, buffer);
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x0003EC68 File Offset: 0x0003CE68
		private void QueryNode(int node, Bounds bounds, List<T> buffer)
		{
			if (node == -1 || !bounds.Intersects(this.nodes[node].bounds))
			{
				return;
			}
			if (this.nodes[node].isLeaf)
			{
				buffer.Add(this.nodes[node].value);
				return;
			}
			this.QueryNode(this.nodes[node].left, bounds, buffer);
			this.QueryNode(this.nodes[node].right, bounds, buffer);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x0003ECF1 File Offset: 0x0003CEF1
		public void QueryTagged(List<T> buffer, bool clearTags = false)
		{
			this.QueryTaggedNode(this.root, clearTags, buffer);
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x0003ED04 File Offset: 0x0003CF04
		private void QueryTaggedNode(int node, bool clearTags, List<T> buffer)
		{
			if (node == -1 || !this.nodes[node].subtreePartiallyTagged)
			{
				return;
			}
			if (clearTags)
			{
				this.nodes[node].wholeSubtreeTagged = false;
				this.nodes[node].subtreePartiallyTagged = false;
			}
			if (this.nodes[node].isLeaf)
			{
				buffer.Add(this.nodes[node].value);
				return;
			}
			this.QueryTaggedNode(this.nodes[node].left, clearTags, buffer);
			this.QueryTaggedNode(this.nodes[node].right, clearTags, buffer);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x0003EDB0 File Offset: 0x0003CFB0
		public void Tag(AABBTree<T>.Key key)
		{
			if (!key.isValid)
			{
				throw new ArgumentException("Key is not valid");
			}
			if (key.node < 0 || key.node >= this.nodes.Length)
			{
				throw new ArgumentException("Key does not point to a valid node");
			}
			AABBTree<T>.Node[] array = this.nodes;
			int node = key.node;
			if (!array[node].isAllocated)
			{
				throw new ArgumentException("Key does not point to an allocated node");
			}
			if (!array[node].isLeaf)
			{
				throw new ArgumentException("Key does not point to a leaf node");
			}
			array[node].wholeSubtreeTagged = true;
			for (int num = key.node; num != 536870911; num = this.nodes[num].parent)
			{
				this.nodes[num].subtreePartiallyTagged = true;
			}
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x0003EE6B File Offset: 0x0003D06B
		public void Tag(Bounds bounds)
		{
			this.TagNode(this.root, bounds);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x0003EE7C File Offset: 0x0003D07C
		private bool TagNode(int node, Bounds bounds)
		{
			if (node == -1 || this.nodes[node].wholeSubtreeTagged)
			{
				return true;
			}
			if (!bounds.Intersects(this.nodes[node].bounds))
			{
				return false;
			}
			this.nodes[node].subtreePartiallyTagged = true;
			if (this.nodes[node].isLeaf)
			{
				return this.nodes[node].wholeSubtreeTagged = true;
			}
			return this.nodes[node].wholeSubtreeTagged = this.TagNode(this.nodes[node].left, bounds) & this.TagNode(this.nodes[node].right, bounds);
		}

		// Token: 0x0400079C RID: 1948
		private AABBTree<T>.Node[] nodes = new AABBTree<T>.Node[0];

		// Token: 0x0400079D RID: 1949
		private int root = -1;

		// Token: 0x0400079E RID: 1950
		private readonly Stack<int> freeNodes = new Stack<int>();

		// Token: 0x0400079F RID: 1951
		private int rebuildCounter = 64;

		// Token: 0x040007A0 RID: 1952
		private const int NoNode = -1;

		// Token: 0x0200019D RID: 413
		private struct Node
		{
			// Token: 0x17000197 RID: 407
			// (get) Token: 0x06000B20 RID: 2848 RVA: 0x0003EF6C File Offset: 0x0003D16C
			// (set) Token: 0x06000B21 RID: 2849 RVA: 0x0003EF7D File Offset: 0x0003D17D
			public bool wholeSubtreeTagged
			{
				get
				{
					return (this.flags & 1073741824U) > 0U;
				}
				set
				{
					this.flags = (this.flags & 3221225471U) | (value ? 1073741824U : 0U);
				}
			}

			// Token: 0x17000198 RID: 408
			// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0003EF9D File Offset: 0x0003D19D
			// (set) Token: 0x06000B23 RID: 2851 RVA: 0x0003EFAE File Offset: 0x0003D1AE
			public bool subtreePartiallyTagged
			{
				get
				{
					return (this.flags & 2147483648U) > 0U;
				}
				set
				{
					this.flags = (this.flags & 2147483647U) | (value ? 2147483648U : 0U);
				}
			}

			// Token: 0x17000199 RID: 409
			// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0003EFCE File Offset: 0x0003D1CE
			// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0003EFDF File Offset: 0x0003D1DF
			public bool isAllocated
			{
				get
				{
					return (this.flags & 536870912U) > 0U;
				}
				set
				{
					this.flags = (this.flags & 3758096383U) | (value ? 536870912U : 0U);
				}
			}

			// Token: 0x1700019A RID: 410
			// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0003EFFF File Offset: 0x0003D1FF
			public bool isLeaf
			{
				get
				{
					return this.left == -1;
				}
			}

			// Token: 0x1700019B RID: 411
			// (get) Token: 0x06000B27 RID: 2855 RVA: 0x0003F00A File Offset: 0x0003D20A
			// (set) Token: 0x06000B28 RID: 2856 RVA: 0x0003F018 File Offset: 0x0003D218
			public int parent
			{
				get
				{
					return (int)(this.flags & 536870911U);
				}
				set
				{
					this.flags = (this.flags & 3758096384U) | (uint)value;
				}
			}

			// Token: 0x040007A1 RID: 1953
			public Bounds bounds;

			// Token: 0x040007A2 RID: 1954
			public uint flags;

			// Token: 0x040007A3 RID: 1955
			private const uint TagInsideBit = 1073741824U;

			// Token: 0x040007A4 RID: 1956
			private const uint TagPartiallyInsideBit = 2147483648U;

			// Token: 0x040007A5 RID: 1957
			private const uint AllocatedBit = 536870912U;

			// Token: 0x040007A6 RID: 1958
			private const uint ParentMask = 536870911U;

			// Token: 0x040007A7 RID: 1959
			public const int InvalidParent = 536870911;

			// Token: 0x040007A8 RID: 1960
			public int left;

			// Token: 0x040007A9 RID: 1961
			public int right;

			// Token: 0x040007AA RID: 1962
			public T value;
		}

		// Token: 0x0200019E RID: 414
		public readonly struct Key
		{
			// Token: 0x1700019C RID: 412
			// (get) Token: 0x06000B29 RID: 2857 RVA: 0x0003F02E File Offset: 0x0003D22E
			public int node
			{
				get
				{
					return this.value - 1;
				}
			}

			// Token: 0x1700019D RID: 413
			// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0003F038 File Offset: 0x0003D238
			public bool isValid
			{
				get
				{
					return this.value != 0;
				}
			}

			// Token: 0x06000B2B RID: 2859 RVA: 0x0003F043 File Offset: 0x0003D243
			internal Key(int node)
			{
				this.value = node + 1;
			}

			// Token: 0x040007AB RID: 1963
			internal readonly int value;
		}

		// Token: 0x0200019F RID: 415
		private struct AABBComparer : IComparer<int>
		{
			// Token: 0x06000B2C RID: 2860 RVA: 0x0003F050 File Offset: 0x0003D250
			public int Compare(int a, int b)
			{
				return this.nodes[a].bounds.center[this.dim].CompareTo(this.nodes[b].bounds.center[this.dim]);
			}

			// Token: 0x040007AC RID: 1964
			public AABBTree<T>.Node[] nodes;

			// Token: 0x040007AD RID: 1965
			public int dim;
		}
	}
}
