using System;
using System.Collections;
using System.Collections.Generic;

namespace Unity.VisualScripting.Antlr3.Runtime
{
	// Token: 0x0200002E RID: 46
	public class ANTLRStringStream : ICharStream, IIntStream
	{
		// Token: 0x060001EF RID: 495 RVA: 0x00006144 File Offset: 0x00005144
		protected ANTLRStringStream()
		{
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00006153 File Offset: 0x00005153
		public ANTLRStringStream(string input)
		{
			this.data = input.ToCharArray();
			this.n = input.Length;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000617A File Offset: 0x0000517A
		public ANTLRStringStream(char[] data, int numberOfActualCharsInArray)
		{
			this.data = data;
			this.n = numberOfActualCharsInArray;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001F2 RID: 498 RVA: 0x00006197 File Offset: 0x00005197
		// (set) Token: 0x060001F3 RID: 499 RVA: 0x0000619F File Offset: 0x0000519F
		public virtual int Line
		{
			get
			{
				return this.line;
			}
			set
			{
				this.line = value;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x000061A8 File Offset: 0x000051A8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x000061B0 File Offset: 0x000051B0
		public virtual int CharPositionInLine
		{
			get
			{
				return this.charPositionInLine;
			}
			set
			{
				this.charPositionInLine = value;
			}
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000061B9 File Offset: 0x000051B9
		public virtual void Reset()
		{
			this.p = 0;
			this.line = 1;
			this.charPositionInLine = 0;
			this.markDepth = 0;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000061D8 File Offset: 0x000051D8
		public virtual void Consume()
		{
			if (this.p < this.n)
			{
				this.charPositionInLine++;
				if (this.data[this.p] == '\n')
				{
					this.line++;
					this.charPositionInLine = 0;
				}
				this.p++;
			}
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00006238 File Offset: 0x00005238
		public virtual int LA(int i)
		{
			if (i == 0)
			{
				return 0;
			}
			if (i < 0)
			{
				i++;
				if (this.p + i - 1 < 0)
				{
					return -1;
				}
			}
			if (this.p + i - 1 >= this.n)
			{
				return -1;
			}
			return (int)this.data[this.p + i - 1];
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00006287 File Offset: 0x00005287
		public virtual int LT(int i)
		{
			return this.LA(i);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00006290 File Offset: 0x00005290
		public virtual int Index()
		{
			return this.p;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006298 File Offset: 0x00005298
		[Obsolete("Please use property Count instead.")]
		public virtual int Size()
		{
			return this.Count;
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000062A0 File Offset: 0x000052A0
		public virtual int Count
		{
			get
			{
				return this.n;
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000062A8 File Offset: 0x000052A8
		public virtual int Mark()
		{
			if (this.markers == null)
			{
				this.markers = new List<object>();
				this.markers.Add(null);
			}
			this.markDepth++;
			CharStreamState charStreamState;
			if (this.markDepth >= this.markers.Count)
			{
				charStreamState = new CharStreamState();
				this.markers.Add(charStreamState);
			}
			else
			{
				charStreamState = (CharStreamState)this.markers[this.markDepth];
			}
			charStreamState.p = this.p;
			charStreamState.line = this.line;
			charStreamState.charPositionInLine = this.charPositionInLine;
			this.lastMarker = this.markDepth;
			return this.markDepth;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000635C File Offset: 0x0000535C
		public virtual void Rewind(int m)
		{
			CharStreamState charStreamState = (CharStreamState)this.markers[m];
			this.Seek(charStreamState.p);
			this.line = charStreamState.line;
			this.charPositionInLine = charStreamState.charPositionInLine;
			this.Release(m);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000063A6 File Offset: 0x000053A6
		public virtual void Rewind()
		{
			this.Rewind(this.lastMarker);
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000063B4 File Offset: 0x000053B4
		public virtual void Release(int marker)
		{
			this.markDepth = marker;
			this.markDepth--;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000063CB File Offset: 0x000053CB
		public virtual void Seek(int index)
		{
			if (index <= this.p)
			{
				this.p = index;
				return;
			}
			while (this.p < index)
			{
				this.Consume();
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000063ED File Offset: 0x000053ED
		public virtual string Substring(int start, int stop)
		{
			return new string(this.data, start, stop - start + 1);
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00006400 File Offset: 0x00005400
		// (set) Token: 0x06000204 RID: 516 RVA: 0x00006408 File Offset: 0x00005408
		public virtual string SourceName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000074 RID: 116
		protected internal char[] data;

		// Token: 0x04000075 RID: 117
		protected int n;

		// Token: 0x04000076 RID: 118
		protected internal int p;

		// Token: 0x04000077 RID: 119
		protected internal int line = 1;

		// Token: 0x04000078 RID: 120
		protected internal int charPositionInLine;

		// Token: 0x04000079 RID: 121
		protected internal int markDepth;

		// Token: 0x0400007A RID: 122
		protected internal IList markers;

		// Token: 0x0400007B RID: 123
		protected int lastMarker;

		// Token: 0x0400007C RID: 124
		protected string name;
	}
}
