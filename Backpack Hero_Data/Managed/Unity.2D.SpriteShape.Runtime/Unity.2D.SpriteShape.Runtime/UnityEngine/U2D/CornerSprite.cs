using System;
using System.Collections.Generic;

namespace UnityEngine.U2D
{
	// Token: 0x0200001D RID: 29
	[Serializable]
	public class CornerSprite : ICloneable
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00007435 File Offset: 0x00005635
		// (set) Token: 0x060000CB RID: 203 RVA: 0x0000743D File Offset: 0x0000563D
		public CornerType cornerType
		{
			get
			{
				return this.m_CornerType;
			}
			set
			{
				this.m_CornerType = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00007446 File Offset: 0x00005646
		// (set) Token: 0x060000CD RID: 205 RVA: 0x0000744E File Offset: 0x0000564E
		public List<Sprite> sprites
		{
			get
			{
				return this.m_Sprites;
			}
			set
			{
				this.m_Sprites = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00007457 File Offset: 0x00005657
		public object Clone()
		{
			CornerSprite cornerSprite = base.MemberwiseClone() as CornerSprite;
			cornerSprite.sprites = new List<Sprite>(cornerSprite.sprites);
			return cornerSprite;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00007478 File Offset: 0x00005678
		public override bool Equals(object obj)
		{
			CornerSprite cornerSprite = obj as CornerSprite;
			if (cornerSprite == null)
			{
				return false;
			}
			if (!this.cornerType.Equals(cornerSprite.cornerType))
			{
				return false;
			}
			if (this.sprites.Count != cornerSprite.sprites.Count)
			{
				return false;
			}
			for (int i = 0; i < this.sprites.Count; i++)
			{
				if (this.sprites[i] != cornerSprite.sprites[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00007508 File Offset: 0x00005708
		public override int GetHashCode()
		{
			int num = this.cornerType.GetHashCode();
			if (this.sprites != null)
			{
				for (int i = 0; i < this.sprites.Count; i++)
				{
					Sprite sprite = this.sprites[i];
					if (sprite)
					{
						num ^= i + 1;
						num ^= sprite.GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x0400007E RID: 126
		[SerializeField]
		private CornerType m_CornerType;

		// Token: 0x0400007F RID: 127
		[SerializeField]
		private List<Sprite> m_Sprites;
	}
}
