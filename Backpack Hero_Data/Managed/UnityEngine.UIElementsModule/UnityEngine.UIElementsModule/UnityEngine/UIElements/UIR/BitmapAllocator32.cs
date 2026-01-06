using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200032A RID: 810
	internal struct BitmapAllocator32
	{
		// Token: 0x06001A0F RID: 6671 RVA: 0x0006ECB8 File Offset: 0x0006CEB8
		public void Construct(int pageHeight, int entryWidth = 1, int entryHeight = 1)
		{
			this.m_PageHeight = pageHeight;
			this.m_Pages = new List<BitmapAllocator32.Page>(1);
			this.m_AllocMap = new List<uint>(this.m_PageHeight * this.m_Pages.Capacity);
			this.m_EntryWidth = entryWidth;
			this.m_EntryHeight = entryHeight;
		}

		// Token: 0x06001A10 RID: 6672 RVA: 0x0006ED04 File Offset: 0x0006CF04
		public void ForceFirstAlloc(ushort firstPageX, ushort firstPageY)
		{
			this.m_AllocMap.Add(4294967294U);
			for (int i = 1; i < this.m_PageHeight; i++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = firstPageX,
				y = firstPageY,
				freeSlots = 32 * this.m_PageHeight - 1
			});
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x0006ED7C File Offset: 0x0006CF7C
		public BMPAlloc Allocate(BaseShaderInfoStorage storage)
		{
			int count = this.m_Pages.Count;
			for (int i = 0; i < count; i++)
			{
				BitmapAllocator32.Page page = this.m_Pages[i];
				bool flag = page.freeSlots == 0;
				if (!flag)
				{
					int j = i * this.m_PageHeight;
					int num = j + this.m_PageHeight;
					while (j < num)
					{
						uint num2 = this.m_AllocMap[j];
						bool flag2 = num2 == 0U;
						if (!flag2)
						{
							byte b = BitmapAllocator32.CountTrailingZeroes(num2);
							this.m_AllocMap[j] = num2 & ~(1U << (int)b);
							page.freeSlots--;
							this.m_Pages[i] = page;
							return new BMPAlloc
							{
								page = i,
								pageLine = (ushort)(j - i * this.m_PageHeight),
								bitIndex = b,
								ownedState = OwnedState.Owned
							};
						}
						j++;
					}
				}
			}
			RectInt rectInt;
			bool flag3 = storage == null || !storage.AllocateRect(32 * this.m_EntryWidth, this.m_PageHeight * this.m_EntryHeight, out rectInt);
			if (flag3)
			{
				return BMPAlloc.Invalid;
			}
			this.m_AllocMap.Capacity += this.m_PageHeight;
			this.m_AllocMap.Add(4294967294U);
			for (int k = 1; k < this.m_PageHeight; k++)
			{
				this.m_AllocMap.Add(uint.MaxValue);
			}
			this.m_Pages.Add(new BitmapAllocator32.Page
			{
				x = (ushort)rectInt.xMin,
				y = (ushort)rectInt.yMin,
				freeSlots = 32 * this.m_PageHeight - 1
			});
			return new BMPAlloc
			{
				page = this.m_Pages.Count - 1,
				ownedState = OwnedState.Owned
			};
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x0006EF90 File Offset: 0x0006D190
		public void Free(BMPAlloc alloc)
		{
			Debug.Assert(alloc.ownedState == OwnedState.Owned);
			int num = alloc.page * this.m_PageHeight + (int)alloc.pageLine;
			this.m_AllocMap[num] = this.m_AllocMap[num] | (1U << (int)alloc.bitIndex);
			BitmapAllocator32.Page page = this.m_Pages[alloc.page];
			page.freeSlots++;
			this.m_Pages[alloc.page] = page;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x0006F018 File Offset: 0x0006D218
		public int entryWidth
		{
			get
			{
				return this.m_EntryWidth;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001A14 RID: 6676 RVA: 0x0006F030 File Offset: 0x0006D230
		public int entryHeight
		{
			get
			{
				return this.m_EntryHeight;
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x0006F048 File Offset: 0x0006D248
		internal void GetAllocPageAtlasLocation(int page, out ushort x, out ushort y)
		{
			BitmapAllocator32.Page page2 = this.m_Pages[page];
			x = page2.x;
			y = page2.y;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x0006F074 File Offset: 0x0006D274
		private static byte CountTrailingZeroes(uint val)
		{
			byte b = 0;
			bool flag = (val & 65535U) == 0U;
			if (flag)
			{
				val >>= 16;
				b = 16;
			}
			bool flag2 = (val & 255U) == 0U;
			if (flag2)
			{
				val >>= 8;
				b += 8;
			}
			bool flag3 = (val & 15U) == 0U;
			if (flag3)
			{
				val >>= 4;
				b += 4;
			}
			bool flag4 = (val & 3U) == 0U;
			if (flag4)
			{
				val >>= 2;
				b += 2;
			}
			bool flag5 = (val & 1U) == 0U;
			if (flag5)
			{
				b += 1;
			}
			return b;
		}

		// Token: 0x04000BF2 RID: 3058
		public const int kPageWidth = 32;

		// Token: 0x04000BF3 RID: 3059
		private int m_PageHeight;

		// Token: 0x04000BF4 RID: 3060
		private List<BitmapAllocator32.Page> m_Pages;

		// Token: 0x04000BF5 RID: 3061
		private List<uint> m_AllocMap;

		// Token: 0x04000BF6 RID: 3062
		private int m_EntryWidth;

		// Token: 0x04000BF7 RID: 3063
		private int m_EntryHeight;

		// Token: 0x0200032B RID: 811
		private struct Page
		{
			// Token: 0x04000BF8 RID: 3064
			public ushort x;

			// Token: 0x04000BF9 RID: 3065
			public ushort y;

			// Token: 0x04000BFA RID: 3066
			public int freeSlots;
		}
	}
}
