using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000007 RID: 7
	internal struct DoublePoint
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002B31 File Offset: 0x00000D31
		public DoublePoint(double x = 0.0, double y = 0.0)
		{
			this.X = x;
			this.Y = y;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B41 File Offset: 0x00000D41
		public DoublePoint(DoublePoint dp)
		{
			this.X = dp.X;
			this.Y = dp.Y;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B5B File Offset: 0x00000D5B
		public DoublePoint(IntPoint ip)
		{
			this.X = (double)ip.X;
			this.Y = (double)ip.Y;
		}

		// Token: 0x04000017 RID: 23
		public double X;

		// Token: 0x04000018 RID: 24
		public double Y;
	}
}
