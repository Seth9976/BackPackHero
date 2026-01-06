using System;
using Unity.Profiling;
using UnityEngine.Assertions;

namespace UnityEngine.UIElements
{
	// Token: 0x02000250 RID: 592
	internal class UIRAtlasAllocator : IDisposable
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0004474A File Offset: 0x0004294A
		public int maxAtlasSize { get; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x060011B6 RID: 4534 RVA: 0x00044752 File Offset: 0x00042952
		public int maxImageWidth { get; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x0004475A File Offset: 0x0004295A
		public int maxImageHeight { get; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x00044762 File Offset: 0x00042962
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x0004476A File Offset: 0x0004296A
		public int virtualWidth { get; private set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00044773 File Offset: 0x00042973
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x0004477B File Offset: 0x0004297B
		public int virtualHeight { get; private set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x00044784 File Offset: 0x00042984
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0004478C File Offset: 0x0004298C
		public int physicalWidth { get; private set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00044795 File Offset: 0x00042995
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0004479D File Offset: 0x0004299D
		public int physicalHeight { get; private set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x000447A6 File Offset: 0x000429A6
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x000447AE File Offset: 0x000429AE
		private protected bool disposed { protected get; private set; }

		// Token: 0x060011C2 RID: 4546 RVA: 0x000447B7 File Offset: 0x000429B7
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000447CC File Offset: 0x000429CC
		protected virtual void Dispose(bool disposing)
		{
			bool disposed = this.disposed;
			if (!disposed)
			{
				if (disposing)
				{
					for (int i = 0; i < this.m_OpenRows.Length; i++)
					{
						UIRAtlasAllocator.Row row = this.m_OpenRows[i];
						bool flag = row != null;
						if (flag)
						{
							row.Release();
						}
					}
					this.m_OpenRows = null;
					UIRAtlasAllocator.AreaNode next;
					for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = next)
					{
						next = areaNode.next;
						areaNode.Release();
					}
					this.m_FirstUnpartitionedArea = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x00044864 File Offset: 0x00042A64
		private static int GetLog2OfNextPower(int n)
		{
			float num = (float)Mathf.NextPowerOfTwo(n);
			float num2 = Mathf.Log(num, 2f);
			return Mathf.RoundToInt(num2);
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00044890 File Offset: 0x00042A90
		public UIRAtlasAllocator(int initialAtlasSize, int maxAtlasSize, int sidePadding = 1)
		{
			Assert.IsTrue(initialAtlasSize > 0 && initialAtlasSize <= maxAtlasSize);
			Assert.IsTrue(initialAtlasSize == Mathf.NextPowerOfTwo(initialAtlasSize));
			Assert.IsTrue(maxAtlasSize == Mathf.NextPowerOfTwo(maxAtlasSize));
			this.m_1SidePadding = sidePadding;
			this.m_2SidePadding = sidePadding << 1;
			this.maxAtlasSize = maxAtlasSize;
			this.maxImageWidth = maxAtlasSize;
			this.maxImageHeight = ((initialAtlasSize == maxAtlasSize) ? (maxAtlasSize / 2 + this.m_2SidePadding) : (maxAtlasSize / 4 + this.m_2SidePadding));
			this.virtualWidth = initialAtlasSize;
			this.virtualHeight = initialAtlasSize;
			int num = UIRAtlasAllocator.GetLog2OfNextPower(maxAtlasSize) + 1;
			this.m_OpenRows = new UIRAtlasAllocator.Row[num];
			RectInt rectInt = new RectInt(0, 0, initialAtlasSize, initialAtlasSize);
			this.m_FirstUnpartitionedArea = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
			this.BuildAreas();
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00044958 File Offset: 0x00042B58
		public bool TryAllocate(int width, int height, out RectInt location)
		{
			bool flag;
			using (UIRAtlasAllocator.s_MarkerTryAllocate.Auto())
			{
				location = default(RectInt);
				bool disposed = this.disposed;
				if (disposed)
				{
					flag = false;
				}
				else
				{
					bool flag2 = width < 1 || height < 1;
					if (flag2)
					{
						flag = false;
					}
					else
					{
						bool flag3 = width > this.maxImageWidth || height > this.maxImageHeight;
						if (flag3)
						{
							flag = false;
						}
						else
						{
							int log2OfNextPower = UIRAtlasAllocator.GetLog2OfNextPower(Mathf.Max(height - this.m_2SidePadding, 1));
							int num = (1 << log2OfNextPower) + this.m_2SidePadding;
							UIRAtlasAllocator.Row row = this.m_OpenRows[log2OfNextPower];
							bool flag4 = row != null && row.width - row.Cursor < width;
							if (flag4)
							{
								row = null;
							}
							bool flag5 = row == null;
							if (flag5)
							{
								for (UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea; areaNode != null; areaNode = areaNode.next)
								{
									bool flag6 = this.TryPartitionArea(areaNode, log2OfNextPower, num, width);
									if (flag6)
									{
										row = this.m_OpenRows[log2OfNextPower];
										break;
									}
								}
								bool flag7 = row == null;
								if (flag7)
								{
									return false;
								}
							}
							location = new RectInt(row.offsetX + row.Cursor, row.offsetY, width, height);
							row.Cursor += width;
							Assert.IsTrue(row.Cursor <= row.width);
							this.physicalWidth = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalWidth, location.xMax));
							this.physicalHeight = Mathf.NextPowerOfTwo(Mathf.Max(this.physicalHeight, location.yMax));
							flag = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00044B20 File Offset: 0x00042D20
		private bool TryPartitionArea(UIRAtlasAllocator.AreaNode areaNode, int rowIndex, int rowHeight, int minWidth)
		{
			RectInt rect = areaNode.rect;
			bool flag = rect.height < rowHeight || rect.width < minWidth;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				UIRAtlasAllocator.Row row = this.m_OpenRows[rowIndex];
				bool flag3 = row != null;
				if (flag3)
				{
					row.Release();
				}
				row = UIRAtlasAllocator.Row.Acquire(rect.x, rect.y, rect.width, rowHeight);
				this.m_OpenRows[rowIndex] = row;
				rect.y += rowHeight;
				rect.height -= rowHeight;
				bool flag4 = rect.height == 0;
				if (flag4)
				{
					bool flag5 = areaNode == this.m_FirstUnpartitionedArea;
					if (flag5)
					{
						this.m_FirstUnpartitionedArea = areaNode.next;
					}
					areaNode.RemoveFromChain();
					areaNode.Release();
				}
				else
				{
					areaNode.rect = rect;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00044C00 File Offset: 0x00042E00
		private void BuildAreas()
		{
			UIRAtlasAllocator.AreaNode areaNode = this.m_FirstUnpartitionedArea;
			while (this.virtualWidth < this.maxAtlasSize || this.virtualHeight < this.maxAtlasSize)
			{
				bool flag = this.virtualWidth > this.virtualHeight;
				RectInt rectInt;
				if (flag)
				{
					rectInt = new RectInt(0, this.virtualHeight, this.virtualWidth, this.virtualHeight);
					this.virtualHeight *= 2;
				}
				else
				{
					rectInt = new RectInt(this.virtualWidth, 0, this.virtualWidth, this.virtualHeight);
					this.virtualWidth *= 2;
				}
				UIRAtlasAllocator.AreaNode areaNode2 = UIRAtlasAllocator.AreaNode.Acquire(rectInt);
				areaNode2.AddAfter(areaNode);
				areaNode = areaNode2;
			}
		}

		// Token: 0x040007DF RID: 2015
		private UIRAtlasAllocator.AreaNode m_FirstUnpartitionedArea;

		// Token: 0x040007E0 RID: 2016
		private UIRAtlasAllocator.Row[] m_OpenRows;

		// Token: 0x040007E1 RID: 2017
		private int m_1SidePadding;

		// Token: 0x040007E2 RID: 2018
		private int m_2SidePadding;

		// Token: 0x040007E3 RID: 2019
		private static ProfilerMarker s_MarkerTryAllocate = new ProfilerMarker("UIRAtlasAllocator.TryAllocate");

		// Token: 0x02000251 RID: 593
		private class Row
		{
			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x060011CA RID: 4554 RVA: 0x00044CCD File Offset: 0x00042ECD
			// (set) Token: 0x060011CB RID: 4555 RVA: 0x00044CD5 File Offset: 0x00042ED5
			public int offsetX { get; private set; }

			// Token: 0x17000401 RID: 1025
			// (get) Token: 0x060011CC RID: 4556 RVA: 0x00044CDE File Offset: 0x00042EDE
			// (set) Token: 0x060011CD RID: 4557 RVA: 0x00044CE6 File Offset: 0x00042EE6
			public int offsetY { get; private set; }

			// Token: 0x17000402 RID: 1026
			// (get) Token: 0x060011CE RID: 4558 RVA: 0x00044CEF File Offset: 0x00042EEF
			// (set) Token: 0x060011CF RID: 4559 RVA: 0x00044CF7 File Offset: 0x00042EF7
			public int width { get; private set; }

			// Token: 0x17000403 RID: 1027
			// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00044D00 File Offset: 0x00042F00
			// (set) Token: 0x060011D1 RID: 4561 RVA: 0x00044D08 File Offset: 0x00042F08
			public int height { get; private set; }

			// Token: 0x060011D2 RID: 4562 RVA: 0x00044D14 File Offset: 0x00042F14
			public static UIRAtlasAllocator.Row Acquire(int offsetX, int offsetY, int width, int height)
			{
				UIRAtlasAllocator.Row row = UIRAtlasAllocator.Row.s_Pool.Get();
				row.offsetX = offsetX;
				row.offsetY = offsetY;
				row.width = width;
				row.height = height;
				row.Cursor = 0;
				return row;
			}

			// Token: 0x060011D3 RID: 4563 RVA: 0x00044D59 File Offset: 0x00042F59
			public void Release()
			{
				UIRAtlasAllocator.Row.s_Pool.Release(this);
				this.offsetX = -1;
				this.offsetY = -1;
				this.width = -1;
				this.height = -1;
				this.Cursor = -1;
			}

			// Token: 0x040007E5 RID: 2021
			private static ObjectPool<UIRAtlasAllocator.Row> s_Pool = new ObjectPool<UIRAtlasAllocator.Row>(100);

			// Token: 0x040007EA RID: 2026
			public int Cursor;
		}

		// Token: 0x02000252 RID: 594
		private class AreaNode
		{
			// Token: 0x060011D6 RID: 4566 RVA: 0x00044DA0 File Offset: 0x00042FA0
			public static UIRAtlasAllocator.AreaNode Acquire(RectInt rect)
			{
				UIRAtlasAllocator.AreaNode areaNode = UIRAtlasAllocator.AreaNode.s_Pool.Get();
				areaNode.rect = rect;
				areaNode.previous = null;
				areaNode.next = null;
				return areaNode;
			}

			// Token: 0x060011D7 RID: 4567 RVA: 0x00044DD3 File Offset: 0x00042FD3
			public void Release()
			{
				UIRAtlasAllocator.AreaNode.s_Pool.Release(this);
			}

			// Token: 0x060011D8 RID: 4568 RVA: 0x00044DE4 File Offset: 0x00042FE4
			public void RemoveFromChain()
			{
				bool flag = this.previous != null;
				if (flag)
				{
					this.previous.next = this.next;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this.previous;
				}
				this.previous = null;
				this.next = null;
			}

			// Token: 0x060011D9 RID: 4569 RVA: 0x00044E3C File Offset: 0x0004303C
			public void AddAfter(UIRAtlasAllocator.AreaNode previous)
			{
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.previous);
				Assert.IsNull<UIRAtlasAllocator.AreaNode>(this.next);
				this.previous = previous;
				bool flag = previous != null;
				if (flag)
				{
					this.next = previous.next;
					previous.next = this;
				}
				bool flag2 = this.next != null;
				if (flag2)
				{
					this.next.previous = this;
				}
			}

			// Token: 0x040007EB RID: 2027
			private static ObjectPool<UIRAtlasAllocator.AreaNode> s_Pool = new ObjectPool<UIRAtlasAllocator.AreaNode>(100);

			// Token: 0x040007EC RID: 2028
			public RectInt rect;

			// Token: 0x040007ED RID: 2029
			public UIRAtlasAllocator.AreaNode previous;

			// Token: 0x040007EE RID: 2030
			public UIRAtlasAllocator.AreaNode next;
		}
	}
}
