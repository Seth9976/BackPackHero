using System;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200001B RID: 27
	[Serializable]
	public class SpriteCharacter : TextElement
	{
		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000078E0 File Offset: 0x00005AE0
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x000078F8 File Offset: 0x00005AF8
		public string name
		{
			get
			{
				return this.m_Name;
			}
			set
			{
				bool flag = value == this.m_Name;
				if (!flag)
				{
					this.m_Name = value;
					this.m_HashCode = TextUtilities.GetHashCodeCaseSensitive(this.m_Name);
				}
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00007930 File Offset: 0x00005B30
		public int hashCode
		{
			get
			{
				return this.m_HashCode;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007948 File Offset: 0x00005B48
		public SpriteCharacter()
		{
			this.m_ElementType = TextElementType.Sprite;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00007959 File Offset: 0x00005B59
		public SpriteCharacter(uint unicode, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.glyphIndex = glyph.index;
			base.glyph = glyph;
			base.scale = 1f;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00007994 File Offset: 0x00005B94
		public SpriteCharacter(uint unicode, SpriteAsset spriteAsset, SpriteGlyph glyph)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = spriteAsset;
			base.glyph = glyph;
			base.glyphIndex = glyph.index;
			base.scale = 1f;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000079E1 File Offset: 0x00005BE1
		internal SpriteCharacter(uint unicode, uint glyphIndex)
		{
			this.m_ElementType = TextElementType.Sprite;
			base.unicode = unicode;
			base.textAsset = null;
			base.glyph = null;
			base.glyphIndex = glyphIndex;
			base.scale = 1f;
		}

		// Token: 0x040000B0 RID: 176
		[SerializeField]
		private string m_Name;

		// Token: 0x040000B1 RID: 177
		[SerializeField]
		private int m_HashCode;
	}
}
