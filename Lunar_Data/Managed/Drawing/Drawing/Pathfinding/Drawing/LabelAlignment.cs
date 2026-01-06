using System;
using Unity.Mathematics;

namespace Pathfinding.Drawing
{
	// Token: 0x02000006 RID: 6
	public struct LabelAlignment
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020C8 File Offset: 0x000002C8
		public LabelAlignment withPixelOffset(float x, float y)
		{
			return new LabelAlignment
			{
				relativePivot = this.relativePivot,
				pixelOffset = new float2(x, y)
			};
		}

		// Token: 0x04000002 RID: 2
		public float2 relativePivot;

		// Token: 0x04000003 RID: 3
		public float2 pixelOffset;

		// Token: 0x04000004 RID: 4
		public static readonly LabelAlignment TopLeft = new LabelAlignment
		{
			relativePivot = new float2(0f, 1f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x04000005 RID: 5
		public static readonly LabelAlignment MiddleLeft = new LabelAlignment
		{
			relativePivot = new float2(0f, 0.5f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x04000006 RID: 6
		public static readonly LabelAlignment BottomLeft = new LabelAlignment
		{
			relativePivot = new float2(0f, 0f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x04000007 RID: 7
		public static readonly LabelAlignment BottomCenter = new LabelAlignment
		{
			relativePivot = new float2(0.5f, 0f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x04000008 RID: 8
		public static readonly LabelAlignment BottomRight = new LabelAlignment
		{
			relativePivot = new float2(1f, 0f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x04000009 RID: 9
		public static readonly LabelAlignment MiddleRight = new LabelAlignment
		{
			relativePivot = new float2(1f, 0.5f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x0400000A RID: 10
		public static readonly LabelAlignment TopRight = new LabelAlignment
		{
			relativePivot = new float2(1f, 1f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x0400000B RID: 11
		public static readonly LabelAlignment TopCenter = new LabelAlignment
		{
			relativePivot = new float2(0.5f, 1f),
			pixelOffset = new float2(0f, 0f)
		};

		// Token: 0x0400000C RID: 12
		public static readonly LabelAlignment Center = new LabelAlignment
		{
			relativePivot = new float2(0.5f, 0.5f),
			pixelOffset = new float2(0f, 0f)
		};
	}
}
