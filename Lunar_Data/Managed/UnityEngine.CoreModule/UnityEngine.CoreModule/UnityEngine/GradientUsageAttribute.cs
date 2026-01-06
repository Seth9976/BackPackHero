using System;

namespace UnityEngine
{
	// Token: 0x020001DF RID: 479
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class GradientUsageAttribute : PropertyAttribute
	{
		// Token: 0x060015DA RID: 5594 RVA: 0x000230D4 File Offset: 0x000212D4
		public GradientUsageAttribute(bool hdr)
		{
			this.hdr = hdr;
			this.colorSpace = ColorSpace.Gamma;
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x000230FA File Offset: 0x000212FA
		public GradientUsageAttribute(bool hdr, ColorSpace colorSpace)
		{
			this.hdr = hdr;
			this.colorSpace = colorSpace;
		}

		// Token: 0x040007BA RID: 1978
		public readonly bool hdr = false;

		// Token: 0x040007BB RID: 1979
		public readonly ColorSpace colorSpace = ColorSpace.Gamma;
	}
}
