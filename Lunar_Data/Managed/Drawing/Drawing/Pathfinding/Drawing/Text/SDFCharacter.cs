using System;
using Unity.Mathematics;

namespace Pathfinding.Drawing.Text
{
	// Token: 0x02000058 RID: 88
	internal struct SDFCharacter
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000F323 File Offset: 0x0000D523
		public float2 uvTopLeft
		{
			get
			{
				return this.uvtopleft;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600029C RID: 668 RVA: 0x0000F32B File Offset: 0x0000D52B
		public float2 uvTopRight
		{
			get
			{
				return new float2(this.uvbottomright.x, this.uvtopleft.y);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000F348 File Offset: 0x0000D548
		public float2 uvBottomLeft
		{
			get
			{
				return new float2(this.uvtopleft.x, this.uvbottomright.y);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000F365 File Offset: 0x0000D565
		public float2 uvBottomRight
		{
			get
			{
				return this.uvbottomright;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000F36D File Offset: 0x0000D56D
		public float2 vertexTopLeft
		{
			get
			{
				return this.vtopleft;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000F375 File Offset: 0x0000D575
		public float2 vertexTopRight
		{
			get
			{
				return new float2(this.vbottomright.x, this.vtopleft.y);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000F392 File Offset: 0x0000D592
		public float2 vertexBottomLeft
		{
			get
			{
				return new float2(this.vtopleft.x, this.vbottomright.y);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000F3AF File Offset: 0x0000D5AF
		public float2 vertexBottomRight
		{
			get
			{
				return this.vbottomright;
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public SDFCharacter(char codePoint, int x, int y, int width, int height, int originX, int originY, int advance, int textureWidth, int textureHeight, float defaultSize)
		{
			float2 @float = new float2((float)textureWidth, (float)textureHeight);
			this.codePoint = codePoint;
			float2 float2 = new float2((float)x, (float)y) / @float;
			float2 float3 = new float2((float)(x + width), (float)(y + height)) / @float;
			this.uvtopleft = new float2(float2.x, 1f - float2.y);
			this.uvbottomright = new float2(float3.x, 1f - float3.y);
			float2 float4 = new float2((float)(-(float)originX), (float)originY);
			this.vtopleft = (float4 + new float2(0f, 0f)) / defaultSize;
			this.vbottomright = (float4 + new float2((float)width, (float)(-(float)height))) / defaultSize;
			this.advance = (float)advance / defaultSize;
		}

		// Token: 0x04000167 RID: 359
		public char codePoint;

		// Token: 0x04000168 RID: 360
		private float2 uvtopleft;

		// Token: 0x04000169 RID: 361
		private float2 uvbottomright;

		// Token: 0x0400016A RID: 362
		private float2 vtopleft;

		// Token: 0x0400016B RID: 363
		private float2 vbottomright;

		// Token: 0x0400016C RID: 364
		public float advance;
	}
}
