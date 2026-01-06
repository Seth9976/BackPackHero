using System;

namespace UnityEngine.Rendering.Universal.LibTessDotNet
{
	// Token: 0x020000FC RID: 252
	internal struct ContourVertex
	{
		// Token: 0x0600075C RID: 1884 RVA: 0x0002B214 File Offset: 0x00029414
		public override string ToString()
		{
			return string.Format("{0}, {1}", this.Position, this.Data);
		}

		// Token: 0x040006E2 RID: 1762
		public Vec3 Position;

		// Token: 0x040006E3 RID: 1763
		public object Data;
	}
}
