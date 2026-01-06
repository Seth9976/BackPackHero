using System;

namespace UnityEngine
{
	// Token: 0x020001DE RID: 478
	[AttributeUsage(256, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x060015D7 RID: 5591 RVA: 0x00022FA0 File Offset: 0x000211A0
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00022FF8 File Offset: 0x000211F8
		public ColorUsageAttribute(bool showAlpha, bool hdr)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00023058 File Offset: 0x00021258
		[Obsolete("Brightness and exposure parameters are no longer used for anything. Use ColorUsageAttribute(bool showAlpha, bool hdr)")]
		public ColorUsageAttribute(bool showAlpha, bool hdr, float minBrightness, float maxBrightness, float minExposureValue, float maxExposureValue)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
			this.minBrightness = minBrightness;
			this.maxBrightness = maxBrightness;
			this.minExposureValue = minExposureValue;
			this.maxExposureValue = maxExposureValue;
		}

		// Token: 0x040007B4 RID: 1972
		public readonly bool showAlpha = true;

		// Token: 0x040007B5 RID: 1973
		public readonly bool hdr = false;

		// Token: 0x040007B6 RID: 1974
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minBrightness = 0f;

		// Token: 0x040007B7 RID: 1975
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxBrightness = 8f;

		// Token: 0x040007B8 RID: 1976
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minExposureValue = 0.125f;

		// Token: 0x040007B9 RID: 1977
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxExposureValue = 3f;
	}
}
