using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200002F RID: 47
	[AttributeUsage(AttributeTargets.Class)]
	public class DisableAnnotationAttribute : Attribute
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00004BDF File Offset: 0x00002DDF
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00004BE7 File Offset: 0x00002DE7
		public bool disableIcon { get; set; } = true;

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00004BF0 File Offset: 0x00002DF0
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00004BF8 File Offset: 0x00002DF8
		public bool disableGizmo { get; set; }
	}
}
