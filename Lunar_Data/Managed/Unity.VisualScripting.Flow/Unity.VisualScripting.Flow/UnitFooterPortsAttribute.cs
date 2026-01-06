using System;

namespace Unity.VisualScripting
{
	// Token: 0x0200000F RID: 15
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class UnitFooterPortsAttribute : Attribute
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000027C4 File Offset: 0x000009C4
		// (set) Token: 0x06000055 RID: 85 RVA: 0x000027CC File Offset: 0x000009CC
		public bool ControlInputs { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000027D5 File Offset: 0x000009D5
		// (set) Token: 0x06000057 RID: 87 RVA: 0x000027DD File Offset: 0x000009DD
		public bool ControlOutputs { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000027E6 File Offset: 0x000009E6
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000027EE File Offset: 0x000009EE
		public bool ValueInputs { get; set; } = true;

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000027F7 File Offset: 0x000009F7
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000027FF File Offset: 0x000009FF
		public bool ValueOutputs { get; set; } = true;
	}
}
