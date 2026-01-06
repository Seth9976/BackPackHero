using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x02000343 RID: 835
	internal class DrawParams
	{
		// Token: 0x06001A94 RID: 6804 RVA: 0x00073B3C File Offset: 0x00071D3C
		public void Reset()
		{
			this.view.Clear();
			this.view.Push(Matrix4x4.identity);
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
			this.renderTexture.Clear();
			this.defaultMaterial.Clear();
		}

		// Token: 0x04000CBA RID: 3258
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		// Token: 0x04000CBB RID: 3259
		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		// Token: 0x04000CBC RID: 3260
		internal readonly Stack<Matrix4x4> view = new Stack<Matrix4x4>(8);

		// Token: 0x04000CBD RID: 3261
		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);

		// Token: 0x04000CBE RID: 3262
		internal readonly List<RenderTexture> renderTexture = new List<RenderTexture>(8);

		// Token: 0x04000CBF RID: 3263
		internal readonly List<Material> defaultMaterial = new List<Material>(8);
	}
}
