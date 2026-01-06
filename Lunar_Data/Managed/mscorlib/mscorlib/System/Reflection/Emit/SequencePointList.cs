using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x02000930 RID: 2352
	internal class SequencePointList
	{
		// Token: 0x060050E0 RID: 20704 RVA: 0x000FD4E0 File Offset: 0x000FB6E0
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x17000D4D RID: 3405
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x000FD4EF File Offset: 0x000FB6EF
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x060050E2 RID: 20706 RVA: 0x000FD4F8 File Offset: 0x000FB6F8
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x000FD538 File Offset: 0x000FB738
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x000FD578 File Offset: 0x000FB778
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x000FD5B8 File Offset: 0x000FB7B8
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x000FD5F8 File Offset: 0x000FB7F8
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x17000D4E RID: 3406
		// (get) Token: 0x060050E7 RID: 20711 RVA: 0x000FD637 File Offset: 0x000FB837
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x17000D4F RID: 3407
		// (get) Token: 0x060050E8 RID: 20712 RVA: 0x000FD64A File Offset: 0x000FB84A
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x060050E9 RID: 20713 RVA: 0x000FD664 File Offset: 0x000FB864
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x17000D51 RID: 3409
		// (get) Token: 0x060050EA RID: 20714 RVA: 0x000FD677 File Offset: 0x000FB877
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x000FD694 File Offset: 0x000FB894
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] array = new SequencePoint[this.count + 10];
				Array.Copy(this.points, array, this.points.Length);
				this.points = array;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x040031A9 RID: 12713
		private ISymbolDocumentWriter doc;

		// Token: 0x040031AA RID: 12714
		private SequencePoint[] points;

		// Token: 0x040031AB RID: 12715
		private int count;

		// Token: 0x040031AC RID: 12716
		private const int arrayGrow = 10;
	}
}
