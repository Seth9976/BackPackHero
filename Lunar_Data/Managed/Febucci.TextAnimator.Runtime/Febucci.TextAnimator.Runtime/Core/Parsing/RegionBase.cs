using System;
using UnityEngine;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000050 RID: 80
	public abstract class RegionBase
	{
		// Token: 0x06000199 RID: 409 RVA: 0x00007A0A File Offset: 0x00005C0A
		public RegionBase(string tagId)
		{
			this.tagId = tagId;
			this.ranges = Array.Empty<TagRange>();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007A24 File Offset: 0x00005C24
		public RegionBase(string tagId, params TagRange[] ranges)
		{
			this.tagId = tagId;
			this.ranges = ranges;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007A3C File Offset: 0x00005C3C
		public RegionBase(string tagId, params Vector2Int[] ranges)
		{
			this.tagId = tagId;
			int length = tagId.Length;
			this.ranges = new TagRange[ranges.Length];
			for (int i = 0; i < this.ranges.Length; i++)
			{
				this.ranges[i] = new TagRange(ranges[i], Array.Empty<ModifierInfo>());
			}
		}

		// Token: 0x0400011E RID: 286
		public readonly string tagId;

		// Token: 0x0400011F RID: 287
		public TagRange[] ranges;
	}
}
