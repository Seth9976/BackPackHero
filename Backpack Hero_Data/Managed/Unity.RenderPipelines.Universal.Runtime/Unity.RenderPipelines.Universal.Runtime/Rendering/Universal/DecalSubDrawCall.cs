using System;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x02000065 RID: 101
	internal struct DecalSubDrawCall
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x000154BD File Offset: 0x000136BD
		public int count
		{
			get
			{
				return this.end - this.start;
			}
		}

		// Token: 0x040002A1 RID: 673
		public int start;

		// Token: 0x040002A2 RID: 674
		public int end;
	}
}
