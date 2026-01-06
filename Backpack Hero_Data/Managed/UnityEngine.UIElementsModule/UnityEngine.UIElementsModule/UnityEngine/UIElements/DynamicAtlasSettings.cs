using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200023F RID: 575
	[Serializable]
	public class DynamicAtlasSettings
	{
		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x000422B4 File Offset: 0x000404B4
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x000422BC File Offset: 0x000404BC
		public int minAtlasSize
		{
			get
			{
				return this.m_MinAtlasSize;
			}
			set
			{
				this.m_MinAtlasSize = value;
			}
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06001125 RID: 4389 RVA: 0x000422C5 File Offset: 0x000404C5
		// (set) Token: 0x06001126 RID: 4390 RVA: 0x000422CD File Offset: 0x000404CD
		public int maxAtlasSize
		{
			get
			{
				return this.m_MaxAtlasSize;
			}
			set
			{
				this.m_MaxAtlasSize = value;
			}
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001127 RID: 4391 RVA: 0x000422D6 File Offset: 0x000404D6
		// (set) Token: 0x06001128 RID: 4392 RVA: 0x000422DE File Offset: 0x000404DE
		public int maxSubTextureSize
		{
			get
			{
				return this.m_MaxSubTextureSize;
			}
			set
			{
				this.m_MaxSubTextureSize = value;
			}
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001129 RID: 4393 RVA: 0x000422E7 File Offset: 0x000404E7
		// (set) Token: 0x0600112A RID: 4394 RVA: 0x000422EF File Offset: 0x000404EF
		public DynamicAtlasFilters activeFilters
		{
			get
			{
				return (DynamicAtlasFilters)this.m_ActiveFilters;
			}
			set
			{
				this.m_ActiveFilters = (DynamicAtlasFiltersInternal)value;
			}
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x0600112B RID: 4395 RVA: 0x000422F8 File Offset: 0x000404F8
		public static DynamicAtlasFilters defaultFilters
		{
			get
			{
				return DynamicAtlas.defaultFilters;
			}
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600112C RID: 4396 RVA: 0x000422FF File Offset: 0x000404FF
		// (set) Token: 0x0600112D RID: 4397 RVA: 0x00042307 File Offset: 0x00040507
		public DynamicAtlasCustomFilter customFilter
		{
			get
			{
				return this.m_CustomFilter;
			}
			set
			{
				this.m_CustomFilter = value;
			}
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x00042310 File Offset: 0x00040510
		public static DynamicAtlasSettings defaults
		{
			get
			{
				return new DynamicAtlasSettings
				{
					minAtlasSize = 64,
					maxAtlasSize = 4096,
					maxSubTextureSize = 64,
					activeFilters = DynamicAtlasSettings.defaultFilters,
					customFilter = null
				};
			}
		}

		// Token: 0x0400078B RID: 1931
		[HideInInspector]
		[SerializeField]
		private int m_MinAtlasSize;

		// Token: 0x0400078C RID: 1932
		[HideInInspector]
		[SerializeField]
		private int m_MaxAtlasSize;

		// Token: 0x0400078D RID: 1933
		[SerializeField]
		[HideInInspector]
		private int m_MaxSubTextureSize;

		// Token: 0x0400078E RID: 1934
		[SerializeField]
		[HideInInspector]
		private DynamicAtlasFiltersInternal m_ActiveFilters;

		// Token: 0x0400078F RID: 1935
		private DynamicAtlasCustomFilter m_CustomFilter;
	}
}
