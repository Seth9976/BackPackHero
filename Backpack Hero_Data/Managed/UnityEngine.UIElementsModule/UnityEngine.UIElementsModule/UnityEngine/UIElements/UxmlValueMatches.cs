using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EA RID: 746
	public class UxmlValueMatches : UxmlTypeRestriction
	{
		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x00061C3A File Offset: 0x0005FE3A
		// (set) Token: 0x06001886 RID: 6278 RVA: 0x00061C42 File Offset: 0x0005FE42
		public string regex { get; set; }

		// Token: 0x06001887 RID: 6279 RVA: 0x00061C4C File Offset: 0x0005FE4C
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueMatches uxmlValueMatches = other as UxmlValueMatches;
			bool flag = uxmlValueMatches == null;
			return !flag && this.regex == uxmlValueMatches.regex;
		}
	}
}
