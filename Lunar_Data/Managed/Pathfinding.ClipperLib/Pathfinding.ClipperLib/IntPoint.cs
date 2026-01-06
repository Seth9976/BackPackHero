using System;

namespace Pathfinding.ClipperLib
{
	// Token: 0x02000006 RID: 6
	public struct IntPoint
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000029C8 File Offset: 0x00000BC8
		public IntPoint(long X, long Y)
		{
			this.X = X;
			this.Y = Y;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000029D8 File Offset: 0x00000BD8
		public IntPoint(double x, double y)
		{
			this.X = (long)x;
			this.Y = (long)y;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000029EC File Offset: 0x00000BEC
		public IntPoint(IntPoint pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A08 File Offset: 0x00000C08
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is IntPoint)
			{
				IntPoint intPoint = (IntPoint)obj;
				return this.X == intPoint.X && this.Y == intPoint.Y;
			}
			return false;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A58 File Offset: 0x00000C58
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static bool operator ==(IntPoint a, IntPoint b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public static bool operator !=(IntPoint a, IntPoint b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x0400000B RID: 11
		public long X;

		// Token: 0x0400000C RID: 12
		public long Y;
	}
}
