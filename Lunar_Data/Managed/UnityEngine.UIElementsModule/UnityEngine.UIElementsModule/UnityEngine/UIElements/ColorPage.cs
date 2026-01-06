using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x02000258 RID: 600
	internal struct ColorPage
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x000461B4 File Offset: 0x000443B4
		public static ColorPage Init(RenderChain renderChain, BMPAlloc alloc)
		{
			bool flag = alloc.IsValid();
			return new ColorPage
			{
				isValid = flag,
				pageAndID = (flag ? renderChain.shaderInfoAllocator.ColorAllocToVertexData(alloc) : default(Color32))
			};
		}

		// Token: 0x0400081E RID: 2078
		public bool isValid;

		// Token: 0x0400081F RID: 2079
		public Color32 pageAndID;
	}
}
