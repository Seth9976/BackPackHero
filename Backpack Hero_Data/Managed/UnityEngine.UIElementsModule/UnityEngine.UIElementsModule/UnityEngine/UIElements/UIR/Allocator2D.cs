using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x020002F9 RID: 761
	internal class Allocator2D
	{
		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001903 RID: 6403 RVA: 0x00063ACD File Offset: 0x00061CCD
		public Vector2Int minSize
		{
			get
			{
				return this.m_MinSize;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001904 RID: 6404 RVA: 0x00063AD5 File Offset: 0x00061CD5
		public Vector2Int maxSize
		{
			get
			{
				return this.m_MaxSize;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001905 RID: 6405 RVA: 0x00063ADD File Offset: 0x00061CDD
		public Vector2Int maxAllocSize
		{
			get
			{
				return this.m_MaxAllocSize;
			}
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x00063AE5 File Offset: 0x00061CE5
		public Allocator2D(int minSize, int maxSize, int rowHeightBias)
			: this(new Vector2Int(minSize, minSize), new Vector2Int(maxSize, maxSize), rowHeightBias)
		{
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x00063B00 File Offset: 0x00061D00
		public Allocator2D(Vector2Int minSize, Vector2Int maxSize, int rowHeightBias)
		{
			Debug.Assert(minSize.x > 0 && minSize.x <= maxSize.x && minSize.y > 0 && minSize.y <= maxSize.y);
			Debug.Assert(minSize.x == UIRUtility.GetNextPow2(minSize.x) && minSize.y == UIRUtility.GetNextPow2(minSize.y) && maxSize.x == UIRUtility.GetNextPow2(maxSize.x) && maxSize.y == UIRUtility.GetNextPow2(maxSize.y));
			Debug.Assert(rowHeightBias >= 0);
			this.m_MinSize = minSize;
			this.m_MaxSize = maxSize;
			this.m_RowHeightBias = rowHeightBias;
			Allocator2D.BuildAreas(this.m_Areas, minSize, maxSize);
			this.m_MaxAllocSize = Allocator2D.ComputeMaxAllocSize(this.m_Areas, rowHeightBias);
			this.m_Rows = Allocator2D.BuildRowArray(this.m_MaxAllocSize.y, rowHeightBias);
		}

		// Token: 0x06001908 RID: 6408 RVA: 0x00063C18 File Offset: 0x00061E18
		public bool TryAllocate(int width, int height, out Allocator2D.Alloc2D alloc2D)
		{
			bool flag = width < 1 || width > this.m_MaxAllocSize.x || height < 1 || height > this.m_MaxAllocSize.y;
			bool flag2;
			if (flag)
			{
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			else
			{
				int nextPow2Exp = UIRUtility.GetNextPow2Exp(Mathf.Max(height - this.m_RowHeightBias, 1));
				for (Allocator2D.Row row = this.m_Rows[nextPow2Exp]; row != null; row = row.next)
				{
					bool flag3 = row.rect.width >= width;
					if (flag3)
					{
						Alloc alloc = row.allocator.Allocate((uint)width);
						bool flag4 = alloc.size > 0U;
						if (flag4)
						{
							alloc2D = new Allocator2D.Alloc2D(row, alloc, width, height);
							return true;
						}
					}
				}
				int num = (1 << nextPow2Exp) + this.m_RowHeightBias;
				Debug.Assert(num >= height);
				for (int i = 0; i < this.m_Areas.Count; i++)
				{
					Allocator2D.Area area = this.m_Areas[i];
					bool flag5 = area.rect.height >= num && area.rect.width >= width;
					if (flag5)
					{
						Alloc alloc2 = area.allocator.Allocate((uint)num);
						bool flag6 = alloc2.size > 0U;
						if (flag6)
						{
							Allocator2D.Row row = Allocator2D.Row.pool.Get();
							row.alloc = alloc2;
							row.allocator = new BestFitAllocator((uint)area.rect.width);
							row.area = area;
							row.next = this.m_Rows[nextPow2Exp];
							row.rect = new RectInt(area.rect.xMin, area.rect.yMin + (int)alloc2.start, area.rect.width, num);
							this.m_Rows[nextPow2Exp] = row;
							Alloc alloc3 = row.allocator.Allocate((uint)width);
							Debug.Assert(alloc3.size > 0U);
							alloc2D = new Allocator2D.Alloc2D(row, alloc3, width, height);
							return true;
						}
					}
				}
				alloc2D = default(Allocator2D.Alloc2D);
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06001909 RID: 6409 RVA: 0x00063E54 File Offset: 0x00062054
		public void Free(Allocator2D.Alloc2D alloc2D)
		{
			bool flag = alloc2D.alloc.size == 0U;
			if (!flag)
			{
				Allocator2D.Row row = alloc2D.row;
				row.allocator.Free(alloc2D.alloc);
				bool flag2 = row.allocator.highWatermark == 0U;
				if (flag2)
				{
					row.area.allocator.Free(row.alloc);
					int nextPow2Exp = UIRUtility.GetNextPow2Exp(row.rect.height - this.m_RowHeightBias);
					Allocator2D.Row row2 = this.m_Rows[nextPow2Exp];
					bool flag3 = row2 == row;
					if (flag3)
					{
						this.m_Rows[nextPow2Exp] = row.next;
					}
					else
					{
						Allocator2D.Row row3 = row2;
						while (row3.next != row)
						{
							row3 = row3.next;
						}
						row3.next = row.next;
					}
					Allocator2D.Row.pool.Return(row);
				}
			}
		}

		// Token: 0x0600190A RID: 6410 RVA: 0x00063F3C File Offset: 0x0006213C
		private static void BuildAreas(List<Allocator2D.Area> areas, Vector2Int minSize, Vector2Int maxSize)
		{
			int num = Mathf.Min(minSize.x, minSize.y);
			int num2 = num;
			areas.Add(new Allocator2D.Area(new RectInt(0, 0, num, num2)));
			while (num < maxSize.x || num2 < maxSize.y)
			{
				bool flag = num < maxSize.x;
				if (flag)
				{
					areas.Add(new Allocator2D.Area(new RectInt(num, 0, num, num2)));
					num *= 2;
				}
				bool flag2 = num2 < maxSize.y;
				if (flag2)
				{
					areas.Add(new Allocator2D.Area(new RectInt(0, num2, num, num2)));
					num2 *= 2;
				}
			}
		}

		// Token: 0x0600190B RID: 6411 RVA: 0x00063FE8 File Offset: 0x000621E8
		private static Vector2Int ComputeMaxAllocSize(List<Allocator2D.Area> areas, int rowHeightBias)
		{
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < areas.Count; i++)
			{
				Allocator2D.Area area = areas[i];
				num = Mathf.Max(area.rect.width, num);
				num2 = Mathf.Max(area.rect.height, num2);
			}
			return new Vector2Int(num, UIRUtility.GetPrevPow2(num2 - rowHeightBias) + rowHeightBias);
		}

		// Token: 0x0600190C RID: 6412 RVA: 0x00064058 File Offset: 0x00062258
		private static Allocator2D.Row[] BuildRowArray(int maxRowHeight, int rowHeightBias)
		{
			int num = UIRUtility.GetNextPow2Exp(maxRowHeight - rowHeightBias) + 1;
			return new Allocator2D.Row[num];
		}

		// Token: 0x04000AAE RID: 2734
		private readonly Vector2Int m_MinSize;

		// Token: 0x04000AAF RID: 2735
		private readonly Vector2Int m_MaxSize;

		// Token: 0x04000AB0 RID: 2736
		private readonly Vector2Int m_MaxAllocSize;

		// Token: 0x04000AB1 RID: 2737
		private readonly int m_RowHeightBias;

		// Token: 0x04000AB2 RID: 2738
		private readonly Allocator2D.Row[] m_Rows;

		// Token: 0x04000AB3 RID: 2739
		private readonly List<Allocator2D.Area> m_Areas = new List<Allocator2D.Area>();

		// Token: 0x020002FA RID: 762
		public class Area
		{
			// Token: 0x0600190D RID: 6413 RVA: 0x0006407B File Offset: 0x0006227B
			public Area(RectInt rect)
			{
				this.rect = rect;
				this.allocator = new BestFitAllocator((uint)rect.height);
			}

			// Token: 0x04000AB4 RID: 2740
			public RectInt rect;

			// Token: 0x04000AB5 RID: 2741
			public BestFitAllocator allocator;
		}

		// Token: 0x020002FB RID: 763
		public class Row : LinkedPoolItem<Allocator2D.Row>
		{
			// Token: 0x0600190E RID: 6414 RVA: 0x0006409E File Offset: 0x0006229E
			[MethodImpl(256)]
			private static Allocator2D.Row Create()
			{
				return new Allocator2D.Row();
			}

			// Token: 0x0600190F RID: 6415 RVA: 0x000640A5 File Offset: 0x000622A5
			[MethodImpl(256)]
			private static void Reset(Allocator2D.Row row)
			{
				row.rect = default(RectInt);
				row.area = null;
				row.allocator = null;
				row.alloc = default(Alloc);
				row.next = null;
			}

			// Token: 0x04000AB6 RID: 2742
			public RectInt rect;

			// Token: 0x04000AB7 RID: 2743
			public Allocator2D.Area area;

			// Token: 0x04000AB8 RID: 2744
			public BestFitAllocator allocator;

			// Token: 0x04000AB9 RID: 2745
			public Alloc alloc;

			// Token: 0x04000ABA RID: 2746
			public Allocator2D.Row next;

			// Token: 0x04000ABB RID: 2747
			public static readonly LinkedPool<Allocator2D.Row> pool = new LinkedPool<Allocator2D.Row>(new Func<Allocator2D.Row>(Allocator2D.Row.Create), new Action<Allocator2D.Row>(Allocator2D.Row.Reset), 256);
		}

		// Token: 0x020002FC RID: 764
		public struct Alloc2D
		{
			// Token: 0x06001912 RID: 6418 RVA: 0x00064107 File Offset: 0x00062307
			public Alloc2D(Allocator2D.Row row, Alloc alloc, int width, int height)
			{
				this.alloc = alloc;
				this.row = row;
				this.rect = new RectInt(row.rect.xMin + (int)alloc.start, row.rect.yMin, width, height);
			}

			// Token: 0x04000ABC RID: 2748
			public RectInt rect;

			// Token: 0x04000ABD RID: 2749
			public Allocator2D.Row row;

			// Token: 0x04000ABE RID: 2750
			public Alloc alloc;
		}
	}
}
