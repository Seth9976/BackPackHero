using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002B5 RID: 693
	internal interface ITextHandle
	{
		// Token: 0x06001739 RID: 5945
		Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling);

		// Token: 0x0600173A RID: 5946
		float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling);

		// Token: 0x0600173B RID: 5947
		float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling);

		// Token: 0x0600173C RID: 5948
		float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint);

		// Token: 0x0600173D RID: 5949
		TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint);

		// Token: 0x0600173E RID: 5950
		int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint);

		// Token: 0x0600173F RID: 5951
		ITextHandle New();

		// Token: 0x06001740 RID: 5952
		bool IsLegacy();

		// Token: 0x06001741 RID: 5953
		void SetDirty();

		// Token: 0x06001742 RID: 5954
		bool IsElided();

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001743 RID: 5955
		// (set) Token: 0x06001744 RID: 5956
		Vector2 MeasuredSizes { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001745 RID: 5957
		// (set) Token: 0x06001746 RID: 5958
		Vector2 RoundedSizes { get; set; }
	}
}
