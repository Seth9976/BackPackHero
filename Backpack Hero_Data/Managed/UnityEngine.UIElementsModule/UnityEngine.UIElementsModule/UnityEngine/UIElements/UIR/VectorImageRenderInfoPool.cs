using System;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000321 RID: 801
	internal class VectorImageRenderInfoPool : LinkedPool<VectorImageRenderInfo>
	{
		// Token: 0x060019F0 RID: 6640 RVA: 0x0006E520 File Offset: 0x0006C720
		public VectorImageRenderInfoPool()
			: base(() => new VectorImageRenderInfo(), delegate(VectorImageRenderInfo vectorImageInfo)
			{
				vectorImageInfo.Reset();
			}, 10000)
		{
		}
	}
}
