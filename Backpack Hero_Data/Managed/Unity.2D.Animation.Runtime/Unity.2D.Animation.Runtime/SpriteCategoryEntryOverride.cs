using System;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x0200001F RID: 31
	[Serializable]
	internal class SpriteCategoryEntryOverride : SpriteCategoryEntry
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000037BB File Offset: 0x000019BB
		// (set) Token: 0x0600008C RID: 140 RVA: 0x000037C3 File Offset: 0x000019C3
		public bool fromMain
		{
			get
			{
				return this.m_FromMain;
			}
			set
			{
				this.m_FromMain = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600008D RID: 141 RVA: 0x000037CC File Offset: 0x000019CC
		// (set) Token: 0x0600008E RID: 142 RVA: 0x000037D4 File Offset: 0x000019D4
		public Sprite spriteOverride
		{
			get
			{
				return this.m_SpriteOverride;
			}
			set
			{
				this.m_SpriteOverride = value;
			}
		}

		// Token: 0x0400003A RID: 58
		[SerializeField]
		private bool m_FromMain;

		// Token: 0x0400003B RID: 59
		[SerializeField]
		private Sprite m_SpriteOverride;
	}
}
