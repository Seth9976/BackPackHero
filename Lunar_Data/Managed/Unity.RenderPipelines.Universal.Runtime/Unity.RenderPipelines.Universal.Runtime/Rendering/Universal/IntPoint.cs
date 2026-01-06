using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000B RID: 11
	internal struct IntPoint
	{
		// Token: 0x06000057 RID: 87 RVA: 0x00003039 File Offset: 0x00001239
		public IntPoint(long X, long Y)
		{
			this.X = X;
			this.Y = Y;
			this.NX = 0.0;
			this.NY = 0.0;
			this.N = -1L;
			this.D = 0L;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003077 File Offset: 0x00001277
		public IntPoint(double x, double y)
		{
			this.X = (long)x;
			this.Y = (long)y;
			this.NX = 0.0;
			this.NY = 0.0;
			this.N = -1L;
			this.D = 0L;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000030B8 File Offset: 0x000012B8
		public IntPoint(IntPoint pt)
		{
			this.X = pt.X;
			this.Y = pt.Y;
			this.NX = pt.NX;
			this.NY = pt.NY;
			this.N = pt.N;
			this.D = pt.D;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000310D File Offset: 0x0000130D
		public static bool operator ==(IntPoint a, IntPoint b)
		{
			return a.X == b.X && a.Y == b.Y;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000312D File Offset: 0x0000132D
		public static bool operator !=(IntPoint a, IntPoint b)
		{
			return a.X != b.X || a.Y != b.Y;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003150 File Offset: 0x00001350
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

		// Token: 0x0600005D RID: 93 RVA: 0x00003191 File Offset: 0x00001391
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000023 RID: 35
		public long N;

		// Token: 0x04000024 RID: 36
		public long X;

		// Token: 0x04000025 RID: 37
		public long Y;

		// Token: 0x04000026 RID: 38
		public long D;

		// Token: 0x04000027 RID: 39
		public double NX;

		// Token: 0x04000028 RID: 40
		public double NY;
	}
}
