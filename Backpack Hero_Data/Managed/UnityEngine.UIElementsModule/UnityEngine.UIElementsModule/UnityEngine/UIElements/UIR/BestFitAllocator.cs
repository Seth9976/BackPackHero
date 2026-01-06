using System;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200033A RID: 826
	internal class BestFitAllocator
	{
		// Token: 0x06001A6F RID: 6767 RVA: 0x00072B74 File Offset: 0x00070D74
		public BestFitAllocator(uint size)
		{
			this.totalSize = size;
			this.m_FirstBlock = (this.m_FirstAvailableBlock = this.m_BlockPool.Get());
			this.m_FirstAvailableBlock.end = size;
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x00072BC1 File Offset: 0x00070DC1
		public uint totalSize { get; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00072BCC File Offset: 0x00070DCC
		public uint highWatermark
		{
			get
			{
				return this.m_HighWatermark;
			}
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x00072BE4 File Offset: 0x00070DE4
		public Alloc Allocate(uint size)
		{
			BestFitAllocator.Block block = this.BestFitFindAvailableBlock(size);
			bool flag = block == null;
			Alloc alloc;
			if (flag)
			{
				alloc = default(Alloc);
			}
			else
			{
				Debug.Assert(block.size >= size);
				Debug.Assert(!block.allocated);
				bool flag2 = size != block.size;
				if (flag2)
				{
					this.SplitBlock(block, size);
				}
				Debug.Assert(block.size == size);
				bool flag3 = block.end > this.m_HighWatermark;
				if (flag3)
				{
					this.m_HighWatermark = block.end;
				}
				bool flag4 = block == this.m_FirstAvailableBlock;
				if (flag4)
				{
					this.m_FirstAvailableBlock = this.m_FirstAvailableBlock.nextAvailable;
				}
				bool flag5 = block.prevAvailable != null;
				if (flag5)
				{
					block.prevAvailable.nextAvailable = block.nextAvailable;
				}
				bool flag6 = block.nextAvailable != null;
				if (flag6)
				{
					block.nextAvailable.prevAvailable = block.prevAvailable;
				}
				block.allocated = true;
				block.prevAvailable = (block.nextAvailable = null);
				alloc = new Alloc
				{
					start = block.start,
					size = block.size,
					handle = block
				};
			}
			return alloc;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00072D24 File Offset: 0x00070F24
		public void Free(Alloc alloc)
		{
			BestFitAllocator.Block block = (BestFitAllocator.Block)alloc.handle;
			bool flag = !block.allocated;
			if (flag)
			{
				Debug.Assert(false, "Severe error: UIR allocation double-free");
			}
			else
			{
				Debug.Assert(block.allocated);
				Debug.Assert(block.start == alloc.start);
				Debug.Assert(block.size == alloc.size);
				bool flag2 = block.end == this.m_HighWatermark;
				if (flag2)
				{
					bool flag3 = block.prev != null;
					if (flag3)
					{
						this.m_HighWatermark = (block.prev.allocated ? block.prev.end : block.prev.start);
					}
					else
					{
						this.m_HighWatermark = 0U;
					}
				}
				block.allocated = false;
				BestFitAllocator.Block block2 = this.m_FirstAvailableBlock;
				BestFitAllocator.Block block3 = null;
				while (block2 != null && block2.start < block.start)
				{
					block3 = block2;
					block2 = block2.nextAvailable;
				}
				bool flag4 = block3 == null;
				if (flag4)
				{
					Debug.Assert(block.prevAvailable == null);
					block.nextAvailable = this.m_FirstAvailableBlock;
					this.m_FirstAvailableBlock = block;
				}
				else
				{
					block.prevAvailable = block3;
					block.nextAvailable = block3.nextAvailable;
					block3.nextAvailable = block;
				}
				bool flag5 = block.nextAvailable != null;
				if (flag5)
				{
					block.nextAvailable.prevAvailable = block;
				}
				bool flag6 = block.prevAvailable == block.prev && block.prev != null;
				if (flag6)
				{
					block = this.CoalesceBlockWithPrevious(block);
				}
				bool flag7 = block.nextAvailable == block.next && block.next != null;
				if (flag7)
				{
					block = this.CoalesceBlockWithPrevious(block.next);
				}
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x00072EE0 File Offset: 0x000710E0
		private BestFitAllocator.Block CoalesceBlockWithPrevious(BestFitAllocator.Block block)
		{
			Debug.Assert(block.prevAvailable.end == block.start);
			Debug.Assert(block.prev.nextAvailable == block);
			BestFitAllocator.Block prev = block.prev;
			prev.next = block.next;
			bool flag = block.next != null;
			if (flag)
			{
				block.next.prev = prev;
			}
			prev.nextAvailable = block.nextAvailable;
			bool flag2 = block.nextAvailable != null;
			if (flag2)
			{
				block.nextAvailable.prevAvailable = block.prevAvailable;
			}
			prev.end = block.end;
			this.m_BlockPool.Return(block);
			return prev;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x00072F90 File Offset: 0x00071190
		internal HeapStatistics GatherStatistics()
		{
			HeapStatistics heapStatistics = default(HeapStatistics);
			for (BestFitAllocator.Block block = this.m_FirstBlock; block != null; block = block.next)
			{
				bool allocated = block.allocated;
				if (allocated)
				{
					heapStatistics.numAllocs += 1U;
					heapStatistics.allocatedSize += block.size;
				}
				else
				{
					heapStatistics.freeSize += block.size;
					heapStatistics.availableBlocksCount += 1U;
					heapStatistics.largestAvailableBlock = Math.Max(heapStatistics.largestAvailableBlock, block.size);
				}
				heapStatistics.blockCount += 1U;
			}
			heapStatistics.totalSize = this.totalSize;
			heapStatistics.highWatermark = this.m_HighWatermark;
			bool flag = heapStatistics.freeSize > 0U;
			if (flag)
			{
				heapStatistics.fragmentation = (float)((heapStatistics.freeSize - heapStatistics.largestAvailableBlock) / heapStatistics.freeSize) * 100f;
			}
			return heapStatistics;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x00073084 File Offset: 0x00071284
		private BestFitAllocator.Block BestFitFindAvailableBlock(uint size)
		{
			BestFitAllocator.Block block = this.m_FirstAvailableBlock;
			BestFitAllocator.Block block2 = null;
			uint num = uint.MaxValue;
			while (block != null)
			{
				bool flag = block.size >= size && num > block.size;
				if (flag)
				{
					block2 = block;
					num = block.size;
				}
				block = block.nextAvailable;
			}
			return block2;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000730E0 File Offset: 0x000712E0
		private void SplitBlock(BestFitAllocator.Block block, uint size)
		{
			Debug.Assert(block.size > size);
			BestFitAllocator.Block block2 = this.m_BlockPool.Get();
			block2.next = block.next;
			block2.nextAvailable = block.nextAvailable;
			block2.prev = block;
			block2.prevAvailable = block;
			block2.start = block.start + size;
			block2.end = block.end;
			bool flag = block2.next != null;
			if (flag)
			{
				block2.next.prev = block2;
			}
			bool flag2 = block2.nextAvailable != null;
			if (flag2)
			{
				block2.nextAvailable.prevAvailable = block2;
			}
			block.next = block2;
			block.nextAvailable = block2;
			block.end = block2.start;
		}

		// Token: 0x04000C82 RID: 3202
		private BestFitAllocator.Block m_FirstBlock;

		// Token: 0x04000C83 RID: 3203
		private BestFitAllocator.Block m_FirstAvailableBlock;

		// Token: 0x04000C84 RID: 3204
		private BestFitAllocator.BlockPool m_BlockPool = new BestFitAllocator.BlockPool();

		// Token: 0x04000C85 RID: 3205
		private uint m_HighWatermark;

		// Token: 0x0200033B RID: 827
		private class BlockPool : LinkedPool<BestFitAllocator.Block>
		{
			// Token: 0x06001A78 RID: 6776 RVA: 0x00073198 File Offset: 0x00071398
			[MethodImpl(256)]
			private static BestFitAllocator.Block CreateBlock()
			{
				return new BestFitAllocator.Block();
			}

			// Token: 0x06001A79 RID: 6777 RVA: 0x000020E6 File Offset: 0x000002E6
			[MethodImpl(256)]
			private static void ResetBlock(BestFitAllocator.Block block)
			{
			}

			// Token: 0x06001A7A RID: 6778 RVA: 0x000731AF File Offset: 0x000713AF
			public BlockPool()
				: base(new Func<BestFitAllocator.Block>(BestFitAllocator.BlockPool.CreateBlock), new Action<BestFitAllocator.Block>(BestFitAllocator.BlockPool.ResetBlock), 10000)
			{
			}
		}

		// Token: 0x0200033C RID: 828
		private class Block : LinkedPoolItem<BestFitAllocator.Block>
		{
			// Token: 0x17000652 RID: 1618
			// (get) Token: 0x06001A7B RID: 6779 RVA: 0x000731D8 File Offset: 0x000713D8
			public uint size
			{
				get
				{
					return this.end - this.start;
				}
			}

			// Token: 0x04000C86 RID: 3206
			public uint start;

			// Token: 0x04000C87 RID: 3207
			public uint end;

			// Token: 0x04000C88 RID: 3208
			public BestFitAllocator.Block prev;

			// Token: 0x04000C89 RID: 3209
			public BestFitAllocator.Block next;

			// Token: 0x04000C8A RID: 3210
			public BestFitAllocator.Block prevAvailable;

			// Token: 0x04000C8B RID: 3211
			public BestFitAllocator.Block nextAvailable;

			// Token: 0x04000C8C RID: 3212
			public bool allocated;
		}
	}
}
