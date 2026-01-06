using System;
using System.Collections.Generic;

namespace UnityEngine.U2D.Animation
{
	// Token: 0x02000020 RID: 32
	[Serializable]
	internal class SpriteLibCategoryOverride : SpriteLibCategory
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000037E5 File Offset: 0x000019E5
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000037ED File Offset: 0x000019ED
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

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000037F6 File Offset: 0x000019F6
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000037FE File Offset: 0x000019FE
		public int entryOverrideCount
		{
			get
			{
				return this.m_EntryOverrideCount;
			}
			set
			{
				this.m_EntryOverrideCount = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003807 File Offset: 0x00001A07
		// (set) Token: 0x06000095 RID: 149 RVA: 0x0000380F File Offset: 0x00001A0F
		public List<SpriteCategoryEntryOverride> overrideEntries
		{
			get
			{
				return this.m_OverrideEntries;
			}
			set
			{
				this.m_OverrideEntries = value;
			}
		}

		// Token: 0x0400003C RID: 60
		[SerializeField]
		private List<SpriteCategoryEntryOverride> m_OverrideEntries;

		// Token: 0x0400003D RID: 61
		[SerializeField]
		private bool m_FromMain;

		// Token: 0x0400003E RID: 62
		[SerializeField]
		private int m_EntryOverrideCount;
	}
}
