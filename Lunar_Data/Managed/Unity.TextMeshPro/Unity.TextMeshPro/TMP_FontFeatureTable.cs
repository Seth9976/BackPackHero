using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TMPro
{
	// Token: 0x0200003E RID: 62
	[Serializable]
	public class TMP_FontFeatureTable
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0001D057 File Offset: 0x0001B257
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0001D05F File Offset: 0x0001B25F
		public List<TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
		{
			get
			{
				return this.m_GlyphPairAdjustmentRecords;
			}
			set
			{
				this.m_GlyphPairAdjustmentRecords = value;
			}
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001D068 File Offset: 0x0001B268
		public TMP_FontFeatureTable()
		{
			this.m_GlyphPairAdjustmentRecords = new List<TMP_GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookupDictionary = new Dictionary<uint, TMP_GlyphPairAdjustmentRecord>();
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0001D088 File Offset: 0x0001B288
		public void SortGlyphPairAdjustmentRecords()
		{
			if (this.m_GlyphPairAdjustmentRecords.Count > 0)
			{
				this.m_GlyphPairAdjustmentRecords = (from s in this.m_GlyphPairAdjustmentRecords
					orderby s.firstAdjustmentRecord.glyphIndex, s.secondAdjustmentRecord.glyphIndex
					select s).ToList<TMP_GlyphPairAdjustmentRecord>();
			}
		}

		// Token: 0x040001FE RID: 510
		[SerializeField]
		internal List<TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x040001FF RID: 511
		internal Dictionary<uint, TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookupDictionary;
	}
}
