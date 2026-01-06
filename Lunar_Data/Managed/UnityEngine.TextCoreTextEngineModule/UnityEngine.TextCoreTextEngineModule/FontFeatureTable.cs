using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.TextCore.LowLevel;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x0200000F RID: 15
	[Serializable]
	public class FontFeatureTable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00005F00 File Offset: 0x00004100
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00005F18 File Offset: 0x00004118
		internal List<GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords
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

		// Token: 0x06000098 RID: 152 RVA: 0x00005F22 File Offset: 0x00004122
		public FontFeatureTable()
		{
			this.m_GlyphPairAdjustmentRecords = new List<GlyphPairAdjustmentRecord>();
			this.m_GlyphPairAdjustmentRecordLookup = new Dictionary<uint, GlyphPairAdjustmentRecord>();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005F44 File Offset: 0x00004144
		public void SortGlyphPairAdjustmentRecords()
		{
			bool flag = this.m_GlyphPairAdjustmentRecords.Count > 0;
			if (flag)
			{
				this.m_GlyphPairAdjustmentRecords = Enumerable.ToList<GlyphPairAdjustmentRecord>(Enumerable.ThenBy<GlyphPairAdjustmentRecord, uint>(Enumerable.OrderBy<GlyphPairAdjustmentRecord, uint>(this.m_GlyphPairAdjustmentRecords, (GlyphPairAdjustmentRecord s) => s.firstAdjustmentRecord.glyphIndex), (GlyphPairAdjustmentRecord s) => s.secondAdjustmentRecord.glyphIndex));
			}
		}

		// Token: 0x04000066 RID: 102
		[SerializeField]
		internal List<GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;

		// Token: 0x04000067 RID: 103
		internal Dictionary<uint, GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;
	}
}
