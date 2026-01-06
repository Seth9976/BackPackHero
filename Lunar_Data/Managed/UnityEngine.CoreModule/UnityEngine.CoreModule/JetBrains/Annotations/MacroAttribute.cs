using System;

namespace JetBrains.Annotations
{
	// Token: 0x020000CC RID: 204
	[AttributeUsage(2112, AllowMultiple = true)]
	public sealed class MacroAttribute : Attribute
	{
		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600036B RID: 875 RVA: 0x00005DA9 File Offset: 0x00003FA9
		// (set) Token: 0x0600036C RID: 876 RVA: 0x00005DB1 File Offset: 0x00003FB1
		[CanBeNull]
		public string Expression { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600036D RID: 877 RVA: 0x00005DBA File Offset: 0x00003FBA
		// (set) Token: 0x0600036E RID: 878 RVA: 0x00005DC2 File Offset: 0x00003FC2
		public int Editable { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600036F RID: 879 RVA: 0x00005DCB File Offset: 0x00003FCB
		// (set) Token: 0x06000370 RID: 880 RVA: 0x00005DD3 File Offset: 0x00003FD3
		[CanBeNull]
		public string Target { get; set; }
	}
}
