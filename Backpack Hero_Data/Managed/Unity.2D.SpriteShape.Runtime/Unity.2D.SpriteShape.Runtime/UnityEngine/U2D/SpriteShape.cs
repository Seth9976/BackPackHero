using System;
using System.Collections.Generic;

namespace UnityEngine.U2D
{
	// Token: 0x0200001E RID: 30
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.2d.spriteshape@latest/index.html?subfolder=/manual/SSProfile.html")]
	public class SpriteShape : ScriptableObject
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00007575 File Offset: 0x00005775
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x0000757D File Offset: 0x0000577D
		public List<AngleRange> angleRanges
		{
			get
			{
				return this.m_Angles;
			}
			set
			{
				this.m_Angles = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00007586 File Offset: 0x00005786
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x0000758E File Offset: 0x0000578E
		public Texture2D fillTexture
		{
			get
			{
				return this.m_FillTexture;
			}
			set
			{
				this.m_FillTexture = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00007597 File Offset: 0x00005797
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x0000759F File Offset: 0x0000579F
		public List<CornerSprite> cornerSprites
		{
			get
			{
				return this.m_CornerSprites;
			}
			set
			{
				this.m_CornerSprites = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000075A8 File Offset: 0x000057A8
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000075B0 File Offset: 0x000057B0
		public float fillOffset
		{
			get
			{
				return this.m_FillOffset;
			}
			set
			{
				this.m_FillOffset = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000075B9 File Offset: 0x000057B9
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000075C1 File Offset: 0x000057C1
		public bool useSpriteBorders
		{
			get
			{
				return this.m_UseSpriteBorders;
			}
			set
			{
				this.m_UseSpriteBorders = value;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000075CA File Offset: 0x000057CA
		private CornerSprite GetCornerSprite(CornerType cornerType)
		{
			CornerSprite cornerSprite = new CornerSprite();
			cornerSprite.cornerType = cornerType;
			cornerSprite.sprites = new List<Sprite>();
			cornerSprite.sprites.Insert(0, null);
			return cornerSprite;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000075F0 File Offset: 0x000057F0
		private void ResetCornerList()
		{
			this.m_CornerSprites.Clear();
			this.m_CornerSprites.Insert(0, this.GetCornerSprite(CornerType.OuterTopLeft));
			this.m_CornerSprites.Insert(1, this.GetCornerSprite(CornerType.OuterTopRight));
			this.m_CornerSprites.Insert(2, this.GetCornerSprite(CornerType.OuterBottomLeft));
			this.m_CornerSprites.Insert(3, this.GetCornerSprite(CornerType.OuterBottomRight));
			this.m_CornerSprites.Insert(4, this.GetCornerSprite(CornerType.InnerTopLeft));
			this.m_CornerSprites.Insert(5, this.GetCornerSprite(CornerType.InnerTopRight));
			this.m_CornerSprites.Insert(6, this.GetCornerSprite(CornerType.InnerBottomLeft));
			this.m_CornerSprites.Insert(7, this.GetCornerSprite(CornerType.InnerBottomRight));
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000076A0 File Offset: 0x000058A0
		private void OnValidate()
		{
			if (this.m_CornerSprites.Count != 8)
			{
				this.ResetCornerList();
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000076B6 File Offset: 0x000058B6
		private void Reset()
		{
			this.m_Angles.Clear();
			this.ResetCornerList();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000076CC File Offset: 0x000058CC
		internal static int GetSpriteShapeHashCode(SpriteShape spriteShape)
		{
			int num = -2128831035;
			num = (num * 16777619) ^ spriteShape.angleRanges.Count;
			for (int i = 0; i < spriteShape.angleRanges.Count; i++)
			{
				num = (num * 16777619) ^ (spriteShape.angleRanges[i].GetHashCode() + i);
			}
			num = (num * 16777619) ^ spriteShape.cornerSprites.Count;
			for (int j = 0; j < spriteShape.cornerSprites.Count; j++)
			{
				num = (num * 16777619) ^ (spriteShape.cornerSprites[j].GetHashCode() + j);
			}
			return num;
		}

		// Token: 0x04000080 RID: 128
		[SerializeField]
		private List<AngleRange> m_Angles = new List<AngleRange>();

		// Token: 0x04000081 RID: 129
		[SerializeField]
		private Texture2D m_FillTexture;

		// Token: 0x04000082 RID: 130
		[SerializeField]
		private List<CornerSprite> m_CornerSprites = new List<CornerSprite>();

		// Token: 0x04000083 RID: 131
		[SerializeField]
		private float m_FillOffset;

		// Token: 0x04000084 RID: 132
		[SerializeField]
		private bool m_UseSpriteBorders = true;
	}
}
