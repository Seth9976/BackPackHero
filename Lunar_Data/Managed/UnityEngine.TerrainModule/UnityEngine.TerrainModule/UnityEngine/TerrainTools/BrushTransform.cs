using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.TerrainTools
{
	// Token: 0x02000021 RID: 33
	[MovedFrom("UnityEngine.Experimental.TerrainAPI")]
	public struct BrushTransform
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00004ECE File Offset: 0x000030CE
		public readonly Vector2 brushOrigin { get; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004ED6 File Offset: 0x000030D6
		public readonly Vector2 brushU { get; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00004EDE File Offset: 0x000030DE
		public readonly Vector2 brushV { get; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00004EE6 File Offset: 0x000030E6
		public readonly Vector2 targetOrigin { get; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00004EEE File Offset: 0x000030EE
		public readonly Vector2 targetX { get; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00004EF6 File Offset: 0x000030F6
		public readonly Vector2 targetY { get; }

		// Token: 0x060001AE RID: 430 RVA: 0x00004F00 File Offset: 0x00003100
		public BrushTransform(Vector2 brushOrigin, Vector2 brushU, Vector2 brushV)
		{
			float num = brushU.x * brushV.y - brushU.y * brushV.x;
			float num2 = (Mathf.Approximately(num, 0f) ? 1f : (1f / num));
			Vector2 vector = new Vector2(brushV.y, -brushU.y) * num2;
			Vector2 vector2 = new Vector2(-brushV.x, brushU.x) * num2;
			Vector2 vector3 = -brushOrigin.x * vector - brushOrigin.y * vector2;
			this.brushOrigin = brushOrigin;
			this.brushU = brushU;
			this.brushV = brushV;
			this.targetOrigin = vector3;
			this.targetX = vector;
			this.targetY = vector2;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00004FC4 File Offset: 0x000031C4
		public Rect GetBrushXYBounds()
		{
			Vector2 vector = this.brushOrigin + this.brushU;
			Vector2 vector2 = this.brushOrigin + this.brushV;
			Vector2 vector3 = this.brushOrigin + this.brushU + this.brushV;
			float num = Mathf.Min(Mathf.Min(this.brushOrigin.x, vector.x), Mathf.Min(vector2.x, vector3.x));
			float num2 = Mathf.Max(Mathf.Max(this.brushOrigin.x, vector.x), Mathf.Max(vector2.x, vector3.x));
			float num3 = Mathf.Min(Mathf.Min(this.brushOrigin.y, vector.y), Mathf.Min(vector2.y, vector3.y));
			float num4 = Mathf.Max(Mathf.Max(this.brushOrigin.y, vector.y), Mathf.Max(vector2.y, vector3.y));
			return Rect.MinMaxRect(num, num3, num2, num4);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000050DC File Offset: 0x000032DC
		public static BrushTransform FromRect(Rect brushRect)
		{
			Vector2 min = brushRect.min;
			Vector2 vector = new Vector2(brushRect.width, 0f);
			Vector2 vector2 = new Vector2(0f, brushRect.height);
			return new BrushTransform(min, vector, vector2);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00005124 File Offset: 0x00003324
		public Vector2 ToBrushUV(Vector2 targetXY)
		{
			return targetXY.x * this.targetX + targetXY.y * this.targetY + this.targetOrigin;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00005168 File Offset: 0x00003368
		public Vector2 FromBrushUV(Vector2 brushUV)
		{
			return brushUV.x * this.brushU + brushUV.y * this.brushV + this.brushOrigin;
		}
	}
}
