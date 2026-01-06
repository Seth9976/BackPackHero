using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020002EB RID: 747
	public class UxmlValueBounds : UxmlTypeRestriction
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00061C8C File Offset: 0x0005FE8C
		// (set) Token: 0x0600188A RID: 6282 RVA: 0x00061C94 File Offset: 0x0005FE94
		public string min { get; set; }

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x00061C9D File Offset: 0x0005FE9D
		// (set) Token: 0x0600188C RID: 6284 RVA: 0x00061CA5 File Offset: 0x0005FEA5
		public string max { get; set; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x0600188D RID: 6285 RVA: 0x00061CAE File Offset: 0x0005FEAE
		// (set) Token: 0x0600188E RID: 6286 RVA: 0x00061CB6 File Offset: 0x0005FEB6
		public bool excludeMin { get; set; }

		// Token: 0x17000605 RID: 1541
		// (get) Token: 0x0600188F RID: 6287 RVA: 0x00061CBF File Offset: 0x0005FEBF
		// (set) Token: 0x06001890 RID: 6288 RVA: 0x00061CC7 File Offset: 0x0005FEC7
		public bool excludeMax { get; set; }

		// Token: 0x06001891 RID: 6289 RVA: 0x00061CD0 File Offset: 0x0005FED0
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueBounds uxmlValueBounds = other as UxmlValueBounds;
			bool flag = uxmlValueBounds == null;
			return !flag && (this.min == uxmlValueBounds.min && this.max == uxmlValueBounds.max && this.excludeMin == uxmlValueBounds.excludeMin) && this.excludeMax == uxmlValueBounds.excludeMax;
		}
	}
}
