using System;
using Pathfinding.Util;
using UnityEngine;

namespace Pathfinding
{
	// Token: 0x0200001C RID: 28
	[Serializable]
	public struct IntRect
	{
		// Token: 0x060001B8 RID: 440 RVA: 0x00008ADE File Offset: 0x00006CDE
		public IntRect(int xmin, int ymin, int xmax, int ymax)
		{
			this.xmin = xmin;
			this.xmax = xmax;
			this.ymin = ymin;
			this.ymax = ymax;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00008AFD File Offset: 0x00006CFD
		public bool Contains(int x, int y)
		{
			return x >= this.xmin && y >= this.ymin && x <= this.xmax && y <= this.ymax;
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00008B28 File Offset: 0x00006D28
		public Int2 Min
		{
			get
			{
				return new Int2(this.xmin, this.ymin);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00008B3B File Offset: 0x00006D3B
		public Int2 Max
		{
			get
			{
				return new Int2(this.xmax, this.ymax);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008B4E File Offset: 0x00006D4E
		public int Width
		{
			get
			{
				return this.xmax - this.xmin + 1;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00008B5F File Offset: 0x00006D5F
		public int Height
		{
			get
			{
				return this.ymax - this.ymin + 1;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008B70 File Offset: 0x00006D70
		public int Area
		{
			get
			{
				return this.Width * this.Height;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008B7F File Offset: 0x00006D7F
		public bool IsValid()
		{
			return this.xmin <= this.xmax && this.ymin <= this.ymax;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00008BA2 File Offset: 0x00006DA2
		public static bool operator ==(IntRect a, IntRect b)
		{
			return a.xmin == b.xmin && a.xmax == b.xmax && a.ymin == b.ymin && a.ymax == b.ymax;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00008BDE File Offset: 0x00006DDE
		public static bool operator !=(IntRect a, IntRect b)
		{
			return a.xmin != b.xmin || a.xmax != b.xmax || a.ymin != b.ymin || a.ymax != b.ymax;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00008C20 File Offset: 0x00006E20
		public override bool Equals(object obj)
		{
			IntRect intRect = (IntRect)obj;
			return this.xmin == intRect.xmin && this.xmax == intRect.xmax && this.ymin == intRect.ymin && this.ymax == intRect.ymax;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008C6E File Offset: 0x00006E6E
		public override int GetHashCode()
		{
			return (this.xmin * 131071) ^ (this.xmax * 3571) ^ (this.ymin * 3109) ^ (this.ymax * 7);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public static IntRect Intersection(IntRect a, IntRect b)
		{
			return new IntRect(Math.Max(a.xmin, b.xmin), Math.Max(a.ymin, b.ymin), Math.Min(a.xmax, b.xmax), Math.Min(a.ymax, b.ymax));
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008CF6 File Offset: 0x00006EF6
		public static bool Intersects(IntRect a, IntRect b)
		{
			return a.xmin <= b.xmax && a.ymin <= b.ymax && a.xmax >= b.xmin && a.ymax >= b.ymin;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00008D38 File Offset: 0x00006F38
		public static IntRect Union(IntRect a, IntRect b)
		{
			return new IntRect(Math.Min(a.xmin, b.xmin), Math.Min(a.ymin, b.ymin), Math.Max(a.xmax, b.xmax), Math.Max(a.ymax, b.ymax));
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008D8E File Offset: 0x00006F8E
		public IntRect ExpandToContain(int x, int y)
		{
			return new IntRect(Math.Min(this.xmin, x), Math.Min(this.ymin, y), Math.Max(this.xmax, x), Math.Max(this.ymax, y));
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008DC5 File Offset: 0x00006FC5
		public IntRect Expand(int range)
		{
			return new IntRect(this.xmin - range, this.ymin - range, this.xmax + range, this.ymax + range);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008DEC File Offset: 0x00006FEC
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

		// Token: 0x060001CA RID: 458 RVA: 0x00008E68 File Offset: 0x00007068
		public void DebugDraw(GraphTransform transform, Color color)
		{
			Vector3 vector = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymin));
			Vector3 vector2 = transform.Transform(new Vector3((float)this.xmin, 0f, (float)this.ymax));
			Vector3 vector3 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymax));
			Vector3 vector4 = transform.Transform(new Vector3((float)this.xmax, 0f, (float)this.ymin));
			Debug.DrawLine(vector, vector2, color);
			Debug.DrawLine(vector2, vector3, color);
			Debug.DrawLine(vector3, vector4, color);
			Debug.DrawLine(vector4, vector, color);
		}

		// Token: 0x04000111 RID: 273
		public int xmin;

		// Token: 0x04000112 RID: 274
		public int ymin;

		// Token: 0x04000113 RID: 275
		public int xmax;

		// Token: 0x04000114 RID: 276
		public int ymax;
	}
}
