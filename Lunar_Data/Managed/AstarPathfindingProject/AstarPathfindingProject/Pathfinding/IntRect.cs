using System;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x02000031 RID: 49
	[Serializable]
	public struct IntRect
	{
		// Token: 0x060001F9 RID: 505 RVA: 0x00009FE5 File Offset: 0x000081E5
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000A004 File Offset: 0x00008204
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000A02F File Offset: 0x0000822F
		public bool Contains(IntRect other)
		{
			return this.xmin <= other.xmin && this.xmax >= other.xmax && this.ymin <= other.ymin && this.ymax >= other.ymax;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000A06E File Offset: 0x0000826E
		public Int2 Min
		{
			get
			{
				return new Int2(this.xmin, this.ymin);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001FD RID: 509 RVA: 0x0000A081 File Offset: 0x00008281
		public Int2 Max
		{
			get
			{
				return new Int2(this.xmax, this.ymax);
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A094 File Offset: 0x00008294
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000A0A5 File Offset: 0x000082A5
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000A0B6 File Offset: 0x000082B6
		public int Area
		{
			get
			{
				return this.Width * this.Height;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A0C5 File Offset: 0x000082C5
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A0E8 File Offset: 0x000082E8
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A124 File Offset: 0x00008324
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A163 File Offset: 0x00008363
		public static explicit operator Rect(IntRect r)
		{
			return new Rect((float)r.xmin, (float)r.ymin, (float)r.Width, (float)r.Height);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000A188 File Offset: 0x00008388
		public override bool Equals(object obj)
		{
			if (!(obj is IntRect))
			{
				return false;
			}
			IntRect intRect = (IntRect)obj;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x0000A1E0 File Offset: 0x000083E0
		public override int GetHashCode()
		{
			return (this.xmin * 131071) ^ (this.xmax * 3571) ^ (this.ymin * 3109) ^ (this.ymax * 7);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000A214 File Offset: 0x00008414
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			return new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000A26A File Offset: 0x0000846A
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000A2AC File Offset: 0x000084AC
		public static IntRect Union(IntRect a, IntRect b)
		{
			return new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x0000A304 File Offset: 0x00008504
		public static IntRect Exclude(IntRect a, IntRect b)
		{
			if (!b.IsValid() || !a.IsValid())
			{
				return a;
			}
			IntRect intRect = IntRect.Intersection(a, b);
			if (!intRect.IsValid())
			{
				return a;
			}
			if (a.xmin == intRect.xmin && a.xmax == intRect.xmax)
			{
				if (a.ymin == intRect.ymin)
				{
					a.ymin = intRect.ymax + 1;
					return a;
				}
				if (a.ymax == intRect.ymax)
				{
					a.ymax = intRect.ymin - 1;
					return a;
				}
				throw new ArgumentException("B splits A into two disjoint parts");
			}
			else
			{
				if (a.ymin != intRect.ymin || a.ymax != intRect.ymax)
				{
					throw new ArgumentException("B covers either a corner of A, or does not touch the edges of A at all");
				}
				if (a.xmin == intRect.xmin)
				{
					a.xmin = intRect.xmax + 1;
					return a;
				}
				if (a.xmax == intRect.xmax)
				{
					a.xmax = intRect.xmin - 1;
					return a;
				}
				throw new ArgumentException("B splits A into two disjoint parts");
			}
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000A40C File Offset: 0x0000860C
		public IntRect ExpandToContain(int x, int y)
		{
			return new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000A443 File Offset: 0x00008643
		public IntRect Offset(Int2 offset)
		{
			return new IntRect(this.xmin + offset.x, this.ymin + offset.y, this.xmax + offset.x, this.ymax + offset.y);
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000A47E File Offset: 0x0000867E
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000A4A8 File Offset: 0x000086A8
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"[x: ",
				this.xmin.ToString(),
				"...",
				this.xmax.ToString(),
				", y: ",
				this.ymin.ToString(),
				"...",
				this.ymax.ToString(),
				"]"
			});
		}

		// Token: 0x0400016C RID: 364
		public int xmin;

		// Token: 0x0400016D RID: 365
		public int ymin;

		// Token: 0x0400016E RID: 366
		public int xmax;

		// Token: 0x0400016F RID: 367
		public int ymax;
	}
}
