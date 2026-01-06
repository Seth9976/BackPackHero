using System;

namespace Pathfinding
{
	// Token: 0x02000038 RID: 56
	public class BinaryHeap
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000E32C File Offset: 0x0000C52C
		public bool isEmpty
		{
			get
			{
				return this.numberOfItems <= 0;
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E33A File Offset: 0x0000C53A
		private static int RoundUpToNextMultipleMod1(int v)
		{
			return v + (4 - (v - 1) % 4) % 4;
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000E347 File Offset: 0x0000C547
		public BinaryHeap(int capacity)
		{
			capacity = BinaryHeap.RoundUpToNextMultipleMod1(capacity);
			this.heap = new BinaryHeap.Tuple[capacity];
			this.numberOfItems = 0;
		}

		// Token: 0x06000292 RID: 658 RVA: 0x0000E378 File Offset: 0x0000C578
		public void Clear()
		{
			for (int i = 0; i < this.numberOfItems; i++)
			{
				this.heap[i].node.heapIndex = ushort.MaxValue;
			}
			this.numberOfItems = 0;
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		internal PathNode GetNode(int i)
		{
			return this.heap[i].node;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000E3CB File Offset: 0x0000C5CB
		internal void SetF(int i, uint f)
		{
			this.heap[i].F = f;
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000E3E0 File Offset: 0x0000C5E0
		private void Expand()
		{
			int num = BinaryHeap.RoundUpToNextMultipleMod1(Math.Max(this.heap.Length + 4, Math.Min(65533, (int)Math.Round((double)((float)this.heap.Length * this.growthFactor)))));
			if (num > 65534)
			{
				throw new Exception("Binary Heap Size really large (>65534). A heap size this large is probably the cause of pathfinding running in an infinite loop. ");
			}
			BinaryHeap.Tuple[] array = new BinaryHeap.Tuple[num];
			this.heap.CopyTo(array, 0);
			this.heap = array;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000E450 File Offset: 0x0000C650
		public void Add(PathNode node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (node.heapIndex != 65535)
			{
				this.DecreaseKey(this.heap[(int)node.heapIndex], node.heapIndex);
				return;
			}
			if (this.numberOfItems == this.heap.Length)
			{
				this.Expand();
			}
			this.DecreaseKey(new BinaryHeap.Tuple(0U, node), (ushort)this.numberOfItems);
			this.numberOfItems++;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		private void DecreaseKey(BinaryHeap.Tuple node, ushort index)
		{
			int num = (int)index;
			uint num2 = (node.F = node.node.F);
			uint g = node.node.G;
			while (num != 0)
			{
				int num3 = (num - 1) / 4;
				if (num2 >= this.heap[num3].F && (num2 != this.heap[num3].F || g <= this.heap[num3].node.G))
				{
					break;
				}
				this.heap[num] = this.heap[num3];
				this.heap[num].node.heapIndex = (ushort)num;
				num = num3;
			}
			this.heap[num] = node;
			node.node.heapIndex = (ushort)num;
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000E5A4 File Offset: 0x0000C7A4
		public PathNode Remove()
		{
			PathNode node = this.heap[0].node;
			node.heapIndex = ushort.MaxValue;
			this.numberOfItems--;
			if (this.numberOfItems == 0)
			{
				return node;
			}
			BinaryHeap.Tuple tuple = this.heap[this.numberOfItems];
			uint g = tuple.node.G;
			int num = 0;
			for (;;)
			{
				int num2 = num;
				uint num3 = tuple.F;
				int num4 = num2 * 4 + 1;
				if (num4 <= this.numberOfItems)
				{
					uint f = this.heap[num4].F;
					uint f2 = this.heap[num4 + 1].F;
					uint f3 = this.heap[num4 + 2].F;
					uint f4 = this.heap[num4 + 3].F;
					if (num4 < this.numberOfItems && (f < num3 || (f == num3 && this.heap[num4].node.G < g)))
					{
						num3 = f;
						num = num4;
					}
					if (num4 + 1 < this.numberOfItems && (f2 < num3 || (f2 == num3 && this.heap[num4 + 1].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f2;
						num = num4 + 1;
					}
					if (num4 + 2 < this.numberOfItems && (f3 < num3 || (f3 == num3 && this.heap[num4 + 2].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num3 = f3;
						num = num4 + 2;
					}
					if (num4 + 3 < this.numberOfItems && (f4 < num3 || (f4 == num3 && this.heap[num4 + 3].node.G < ((num == num2) ? g : this.heap[num].node.G))))
					{
						num = num4 + 3;
					}
				}
				if (num2 == num)
				{
					break;
				}
				this.heap[num2] = this.heap[num];
				this.heap[num2].node.heapIndex = (ushort)num2;
			}
			this.heap[num] = tuple;
			tuple.node.heapIndex = (ushort)num;
			return node;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000E818 File Offset: 0x0000CA18
		private void Validate()
		{
			for (int i = 1; i < this.numberOfItems; i++)
			{
				int num = (i - 1) / 4;
				if (this.heap[num].F > this.heap[i].F)
				{
					throw new Exception(string.Concat(new string[]
					{
						"Invalid state at ",
						i.ToString(),
						":",
						num.ToString(),
						" ( ",
						this.heap[num].F.ToString(),
						" > ",
						this.heap[i].F.ToString(),
						" ) "
					}));
				}
				if ((int)this.heap[i].node.heapIndex != i)
				{
					throw new Exception("Invalid heap index");
				}
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000E910 File Offset: 0x0000CB10
		public void Rebuild()
		{
			for (int i = 2; i < this.numberOfItems; i++)
			{
				int num = i;
				BinaryHeap.Tuple tuple = this.heap[i];
				uint f = tuple.F;
				while (num != 1)
				{
					int num2 = num / 4;
					if (f >= this.heap[num2].F)
					{
						break;
					}
					this.heap[num] = this.heap[num2];
					this.heap[num].node.heapIndex = (ushort)num;
					this.heap[num2] = tuple;
					this.heap[num2].node.heapIndex = (ushort)num2;
					num = num2;
				}
			}
		}

		// Token: 0x040001B4 RID: 436
		public int numberOfItems;

		// Token: 0x040001B5 RID: 437
		public float growthFactor = 2f;

		// Token: 0x040001B6 RID: 438
		private const int D = 4;

		// Token: 0x040001B7 RID: 439
		private const bool SortGScores = true;

		// Token: 0x040001B8 RID: 440
		public const ushort NotInHeap = 65535;

		// Token: 0x040001B9 RID: 441
		private BinaryHeap.Tuple[] heap;

		// Token: 0x020000FF RID: 255
		private struct Tuple
		{
			// Token: 0x06000A22 RID: 2594 RVA: 0x0004046B File Offset: 0x0003E66B
			public Tuple(uint f, PathNode node)
			{
				this.F = f;
				this.node = node;
			}

			// Token: 0x0400064C RID: 1612
			public PathNode node;

			// Token: 0x0400064D RID: 1613
			public uint F;
		}
	}
}
