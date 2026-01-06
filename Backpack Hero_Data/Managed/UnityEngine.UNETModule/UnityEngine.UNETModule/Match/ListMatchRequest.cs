using System;
using System.Collections.Generic;

namespace UnityEngine.Networking.Match
{
	// Token: 0x0200002C RID: 44
	internal class ListMatchRequest : Request
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00005D12 File Offset: 0x00003F12
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00005D1A File Offset: 0x00003F1A
		public int pageSize { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060001DC RID: 476 RVA: 0x00005D23 File Offset: 0x00003F23
		// (set) Token: 0x060001DD RID: 477 RVA: 0x00005D2B File Offset: 0x00003F2B
		public int pageNum { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00005D34 File Offset: 0x00003F34
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00005D3C File Offset: 0x00003F3C
		public string nameFilter { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001E0 RID: 480 RVA: 0x00005D45 File Offset: 0x00003F45
		// (set) Token: 0x060001E1 RID: 481 RVA: 0x00005D4D File Offset: 0x00003F4D
		public bool filterOutPrivateMatches { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001E2 RID: 482 RVA: 0x00005D56 File Offset: 0x00003F56
		// (set) Token: 0x060001E3 RID: 483 RVA: 0x00005D5E File Offset: 0x00003F5E
		public int eloScore { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00005D67 File Offset: 0x00003F67
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00005D6F File Offset: 0x00003F6F
		public Dictionary<string, long> matchAttributeFilterLessThan { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00005D78 File Offset: 0x00003F78
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00005D80 File Offset: 0x00003F80
		public Dictionary<string, long> matchAttributeFilterEqualTo { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00005D89 File Offset: 0x00003F89
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x00005D91 File Offset: 0x00003F91
		public Dictionary<string, long> matchAttributeFilterGreaterThan { get; set; }

		// Token: 0x060001EA RID: 490 RVA: 0x00005D9C File Offset: 0x00003F9C
		public override string ToString()
		{
			return UnityString.Format("[{0}]-pageSize:{1},pageNum:{2},nameFilter:{3}, filterOutPrivateMatches:{4}, eloScore:{5}, matchAttributeFilterLessThan.Count:{6}, matchAttributeFilterEqualTo.Count:{7}, matchAttributeFilterGreaterThan.Count:{8}", new object[]
			{
				base.ToString(),
				this.pageSize,
				this.pageNum,
				this.nameFilter,
				this.filterOutPrivateMatches,
				this.eloScore,
				(this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count,
				(this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count,
				(this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count
			});
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00005E64 File Offset: 0x00004064
		public override bool IsValid()
		{
			int num = ((this.matchAttributeFilterLessThan == null) ? 0 : this.matchAttributeFilterLessThan.Count);
			num += ((this.matchAttributeFilterEqualTo == null) ? 0 : this.matchAttributeFilterEqualTo.Count);
			num += ((this.matchAttributeFilterGreaterThan == null) ? 0 : this.matchAttributeFilterGreaterThan.Count);
			return base.IsValid() && this.pageSize >= 1 && this.pageSize <= 1000 && num <= 10;
		}

		// Token: 0x040000C8 RID: 200
		[Obsolete("This bool is deprecated in favor of filterOutPrivateMatches")]
		public bool includePasswordMatches;
	}
}
