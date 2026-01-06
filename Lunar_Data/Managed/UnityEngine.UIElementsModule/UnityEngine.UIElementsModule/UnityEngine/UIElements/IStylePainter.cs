using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007C RID: 124
	internal interface IStylePainter
	{
		// Token: 0x06000317 RID: 791
		MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags);

		// Token: 0x06000318 RID: 792
		void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint);

		// Token: 0x06000319 RID: 793
		void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams);

		// Token: 0x0600031A RID: 794
		void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams);

		// Token: 0x0600031B RID: 795
		void DrawImmediate(Action callback, bool cullingEnabled);

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600031C RID: 796
		VisualElement visualElement { get; }
	}
}
