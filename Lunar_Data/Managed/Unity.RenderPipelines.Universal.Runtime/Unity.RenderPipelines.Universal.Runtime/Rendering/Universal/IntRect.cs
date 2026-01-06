using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200000C RID: 12
	internal struct IntRect
	{
		// Token: 0x0600005E RID: 94 RVA: 0x000031A3 File Offset: 0x000013A3
		public IntRect(long l, long t, long r, long b)
		{
			this.left = l;
			this.top = t;
			this.right = r;
			this.bottom = b;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000031C2 File Offset: 0x000013C2
		public IntRect(IntRect ir)
		{
			this.left = ir.left;
			this.top = ir.top;
			this.right = ir.right;
			this.bottom = ir.bottom;
		}

		// Token: 0x04000029 RID: 41
		public long left;

		// Token: 0x0400002A RID: 42
		public long top;

		// Token: 0x0400002B RID: 43
		public long right;

		// Token: 0x0400002C RID: 44
		public long bottom;
	}
}
