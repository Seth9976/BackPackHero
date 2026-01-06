using System;

namespace System.Configuration
{
	// Token: 0x020001A9 RID: 425
	internal class SectionData
	{
		// Token: 0x06000B1F RID: 2847 RVA: 0x0002F447 File Offset: 0x0002D647
		public SectionData(string sectionName, string typeName, bool allowLocation, AllowDefinition allowDefinition, bool requirePermission)
		{
			this.SectionName = sectionName;
			this.TypeName = typeName;
			this.AllowLocation = allowLocation;
			this.AllowDefinition = allowDefinition;
			this.RequirePermission = requirePermission;
		}

		// Token: 0x04000749 RID: 1865
		public readonly string SectionName;

		// Token: 0x0400074A RID: 1866
		public readonly string TypeName;

		// Token: 0x0400074B RID: 1867
		public readonly bool AllowLocation;

		// Token: 0x0400074C RID: 1868
		public readonly AllowDefinition AllowDefinition;

		// Token: 0x0400074D RID: 1869
		public string FileName;

		// Token: 0x0400074E RID: 1870
		public readonly bool RequirePermission;
	}
}
